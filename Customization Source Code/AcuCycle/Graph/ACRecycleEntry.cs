using System;
using System.Collections;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;

namespace AcuCycle
{
    public class ACRecycleEntry : PXGraph<ACRecycleEntry, ACRecycleHeader>
    {
        #region Views
        public PXSetup<ACRecycleSetup> Setup;

        public PXSelect<ACRecycleHeader> Document;
        public SelectFrom<ACRecycleDetails>.
            Where<ACRecycleDetails.docType.IsEqual<ACRecycleHeader.docType.FromCurrent>.
            And<ACRecycleDetails.refNbr.IsEqual<ACRecycleHeader.refNbr.FromCurrent>>>.View Transactions;
        #endregion

        #region Event Handler
        protected virtual void _(Events.RowPersisting<ACRecycleHeader> e)
        {
            AutoNumberAttribute.SetNumberingId<ACRecycleHeader.refNbr>(e.Cache, Setup.Current.RefNumberingID);
        }
        #endregion

        #region Actions
        public PXAction<ACRecycleHeader> ProcRecycle;
        [PXUIField(DisplayName = "Process Recycling")]
        [PXUIEnabled(typeof(Where<ACRecycleHeader.isRecycled.IsEqual<False>>))]
        [PXButton()]
        public virtual IEnumerable procRecycle(PXAdapter adapter)
        {
            ACRecycleHeader current = Document.Current;
            if (current == null) return adapter.Get();

            Actions.PressSave();

            PXException error = null;

            PXLongOperation.StartOperation(this, delegate ()
            {
                ACRecycleEntry recycleGraph = PXGraph.CreateInstance<ACRecycleEntry>();
                recycleGraph.Document.Current = current;

                foreach (ACRecycleDetails tran in recycleGraph.Transactions.Select())
                {
                    if (tran?.AssemblyRefNbr == null)
                    {
                        try
                        {
                            KitAssemblyEntry kitGraph = PXGraph.CreateInstance<KitAssemblyEntry>();
                            INKitRegister register = kitGraph.Document.Insert(new INKitRegister()
                            {
                                DocType = INDocType.Disassembly
                            });

                            InventoryItem item = InventoryItem.PK.Find(kitGraph, tran.InventoryID);
                            INSetup inSetup = kitGraph.Setup.Current;
                            INSetupExt inSetupExt = inSetup.GetExtension<INSetupExt>();

                            register.InventoryID = tran.InventoryID;
                            register.ReasonCode = inSetupExt.UsrRecycleReason;
                            register.SiteID = tran.SiteID;
                            register.Qty = tran.Qty;
                            register.TranDesc = "Recycled Entry Generated - " + PX.Common.PXTimeZoneInfo.Now;
                            kitGraph.Document.Update(register);

                            foreach (INComponentTran component in kitGraph.Components.Select())
                            {
                                kitGraph.Components.Current = component;

                                INLocation location = SelectFrom<INLocation>.
                                    Where<INLocation.siteID.IsEqual<P.AsInt>.
                                    And<INLocation.primaryItemID.IsEqual<P.AsInt>>>.View.
                                Select(kitGraph, tran.SiteID, component.InventoryID).TopFirst;

                                if (location?.LocationID != null)
                                {
                                    component.SiteID = location.SiteID;
                                    kitGraph.Components.Update(component);

                                    component.LocationID = location.LocationID;
                                    kitGraph.Components.Update(component);
                                }
                            }
                            kitGraph.Actions.PressSave();

                            kitGraph.releaseFromHold.Press();
                            kitGraph.release.Press();

                            tran.AssemblyDocType = kitGraph.Document.Current.DocType;
                            tran.AssemblyRefNbr = kitGraph.Document.Current.RefNbr;

                            recycleGraph.Transactions.Update(tran);
                            recycleGraph.Actions.PressSave();
                        }
                        catch (PXException ex)
                        {
                            if (error == null)
                            {
                                error = ex;
                            }
                        }
                    }
                }

                if (error == null)
                {
                    current.IsRecycled = true;
                    recycleGraph.Document.Update(current);
                    recycleGraph.Actions.PressSave();
                }
                else
                {
                    current.IsRecycled = false;
                }
            });

            if (error != null)
            {
                throw error;
            }
            else
            {
                return adapter.Get();
            }
        }
        #endregion
    }
}