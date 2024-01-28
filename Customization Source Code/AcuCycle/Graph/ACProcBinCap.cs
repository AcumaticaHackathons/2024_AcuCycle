using System;
using System.Collections.Generic;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.IN;

namespace AcuCycle
{
    public class ACProcBinCap : PXGraph<ACProcBinCap>
    {
        #region Filters & Views
        public PXCancel<ProcBinCapFilter> Cancel;

        public PXFilter<ProcBinCapFilter> Filter;

        // TODO: Add in conversion for UOM differences (maybe change to IEnumerable)
        // TODO: Let user define capacity percentage rather than constant
        [PXFilterable]
        public PXFilteredProcessingJoin<INLocationStatus, ProcBinCapFilter,
            InnerJoin<INLocation, On<INLocation.siteID.IsEqual<INLocationStatus.siteID>.
                And<INLocation.locationID.IsEqual<INLocationStatus.locationID>>>,
            InnerJoin<InventoryItem, On<InventoryItem.inventoryID.IsEqual<INLocationStatus.inventoryID>>>>,
            Where<INLocationExt.usrCapacity.IsNotNull
                .And<InventoryItem.kitItem.IsEqual<False>
                .And<INLocationStatus.qtyHardAvail.IsGreater<decimal_0>
                .And<Brackets<INLocationStatus.qtyHardAvail.IsGreaterEqual<Mult<INLocationExt.usrCapacity, decimal_dot80>>.Or<ProcBinCapFilter.showAllInv.FromCurrent.IsEqual<True>>>
                .And<Brackets<INLocationStatus.locationID.IsEqual<ProcBinCapFilter.locationID.FromCurrent>.Or<ProcBinCapFilter.locationID.FromCurrent.IsNull>>>
                .And<Brackets<INLocationStatus.inventoryID.IsEqual<ProcBinCapFilter.inventoryID.FromCurrent>.Or<ProcBinCapFilter.inventoryID.FromCurrent.IsNull>>>>>>>> FullBins;

        #endregion

        public ACProcBinCap()
        {

            FullBins.Cache.AllowInsert = false;
            FullBins.Cache.AllowDelete = false;

            FullBins.SetSelected<INLocationStatusExt.selected>();
            ProcBinCapFilter settings = Filter.Current;
            FullBins.SetProcessDelegate(
                delegate (List<INLocationStatus> list)
                {
                    ProcBins(list, settings);
                }
            );

            FullBins.SetProcessCaption("Process Full Bin(s)");
            FullBins.SetProcessAllCaption("Process All Full Bins");
        }

        #region Methods
        public static void ProcBins(List<INLocationStatus> binsToProcess, ProcBinCapFilter settings)
        {
            ARInvoiceEntry arGraph = PXGraph.CreateInstance<ARInvoiceEntry>();
            APInvoiceEntry apGraph = PXGraph.CreateInstance<APInvoiceEntry>();

            foreach (INLocationStatus bin in binsToProcess)
            {
                INLocationStatusExt binExt = bin.GetExtension<INLocationStatusExt>();
                if (binExt.Selected == true)
                {
                    try
                    {
                        PXFilteredProcessing<INLocationStatus, ProcBinCapFilter>.SetCurrentItem(bin);
                        
                        if (settings.ProcessType == "AR")
                        {
                            // Make AR
                            ACRecycleSetup recycleSetup = SelectFrom<ACRecycleSetup>.View.Select(arGraph);
                            ARInvoice invoice = arGraph.Document.Insert();
                            BAccount account = BAccount.PK.Find(arGraph, settings.BAccountID);
                            invoice.CustomerID = account?.BAccountID;
                            invoice.CustomerLocationID = account?.DefLocationID;
                            invoice.DocDesc = "Generated from Process Bin Capacity for " + account?.AcctName;
                            arGraph.Document.Update(invoice);

                            ARTran tran = arGraph.Transactions.Current = arGraph.Transactions.Insert();
                            tran.InventoryID = recycleSetup.ChargeFeeID;
                            tran.Qty = bin.QtyHardAvail;

                            arGraph.Transactions.Update(tran);
                            arGraph.Actions.PressSave();

                            // Make IN
                            INIssueEntry inGraph = PXGraph.CreateInstance<INIssueEntry>();
                            INRegister issue = inGraph.issue.Insert();
                            INRegisterExt issueExt = issue.GetExtension<INRegisterExt>();
                            
                            issue.TranDesc = "Generated from Process Bin Capacity for " + account?.AcctName;
                            issueExt.UsrACDocType = invoice.DocType;
                            issueExt.UsrACRefNbr = invoice.RefNbr;

                            inGraph.issue.Update(issue);
                            inGraph.Actions.PressSave();

                            INTran inTran = inGraph.transactions.Insert();
                            inTran.InventoryID = bin.InventoryID;
                            inTran.Qty = bin.QtyHardAvail;
                            inTran.SiteID = bin.SiteID;
                            inTran.LocationID = bin.LocationID;

                            inGraph.transactions.Update(inTran);
                            inGraph.Actions.PressSave();

                            PXFilteredProcessing<INLocationStatus, ProcBinCapFilter>.SetInfo(binsToProcess.IndexOf(bin), $"Created: {invoice.RefNbr}");
                        }
                        else
                        {
                            // Make AP
                            ACRecycleSetup recycleSetup = SelectFrom<ACRecycleSetup>.View.Select(apGraph);
                            APInvoice invoice = apGraph.Document.Insert();
                            BAccount account = BAccount.PK.Find(apGraph, settings.BAccountID);
                            invoice.VendorID = account?.BAccountID;
                            invoice.VendorLocationID = account?.DefLocationID;
                            invoice.DocDesc = "Generated from Process Bin Capacity for " + account?.AcctName;
                            apGraph.Document.Update(invoice);

                            APTran tran = apGraph.Transactions.Current = apGraph.Transactions.Insert();
                            tran.InventoryID = recycleSetup.ChargeFeeID;
                            tran.Qty = bin.QtyHardAvail;

                            apGraph.Transactions.Update(tran);
                            apGraph.Actions.PressSave();

                            // Make IN
                            INIssueEntry inGraph = PXGraph.CreateInstance<INIssueEntry>();
                            INRegister issue = inGraph.issue.Insert();
                            INRegisterExt issueExt = issue.GetExtension<INRegisterExt>();

                            issue.TranDesc = "Generated from Process Bin Capacity for " + account?.AcctName;
                            issueExt.UsrACDocType = invoice.DocType;
                            issueExt.UsrACRefNbr = invoice.RefNbr;

                            inGraph.issue.Update(issue);
                            inGraph.Actions.PressSave();

                            INTran inTran = inGraph.transactions.Insert();
                            inTran.InventoryID = bin.InventoryID;
                            inTran.Qty = bin.QtyHardAvail;
                            inTran.SiteID = bin.SiteID;
                            inTran.LocationID = bin.LocationID;

                            inGraph.transactions.Update(inTran);
                            inGraph.Actions.PressSave();

                            PXFilteredProcessing<INLocationStatus, ProcBinCapFilter>.SetInfo(binsToProcess.IndexOf(bin), $"Created: {invoice.RefNbr}");
                        }
                    }
                    catch (Exception ex)
                    {
                        string errMsg = ex.Message;

                        PXFilteredProcessing<INLocationStatus, ProcBinCapFilter>.SetError(binsToProcess.IndexOf(bin), $"Failed: {errMsg}");
                    }

                }
            }
            if (settings.ProcessType == "AR")
            {
                throw new PXRedirectRequiredException(arGraph, "AR Invoice");
            }
            else
            {
                throw new PXRedirectRequiredException(apGraph, "AP Invoice");
            }
        }
        #endregion

        #region Classes
        [PXHidden]
        [Serializable]
        public partial class ProcBinCapFilter : PX.Data.IBqlTable
        {

            #region LocationID
            [PXDBInt()]
            [PXSelector(typeof(Search<INLocation.locationID, Where<INLocation.siteID.IsEqual<ACRecycleDetails.siteID.FromCurrent>>>), SubstituteKey = typeof(INLocation.locationCD))]
            [PXUIField(DisplayName = "Location ID")]
            public virtual int? LocationID { get; set; }
            public abstract class locationID : PX.Data.BQL.BqlInt.Field<locationID> { }
            #endregion

            #region InventoryID
            [Inventory()]
            [PXUIField(DisplayName = "Inventory ID")]
            public virtual int? InventoryID { get; set; }
            public abstract class inventoryID : PX.Data.BQL.BqlInt.Field<inventoryID> { }
            #endregion

            #region ProcessType
            [PXString(2)]
            [PXDefault("AR", PersistingCheck = PXPersistingCheck.Nothing)]
            [PXStringList(new string[] { "AR", "AP" }, new string[] { "Sell to Customer", "By from Vendor" })]
            [PXUIField(DisplayName = "Process Type")]
            public string ProcessType { get; set; }
            public abstract class processType : PX.Data.BQL.BqlString.Field<processType> { }
            #endregion

            #region BAccountID
            [PXInt()]
            [PXSelector(typeof(Search<BAccount.bAccountID,
                Where<
                    Brackets<BAccount.status.IsEqual<CustomerStatus.active>.
                        Or<BAccount.vStatus.IsEqual<VendorStatus.active>>>
                    .And<Brackets<
                        Brackets<BAccount.type.IsEqual<BAccountType.customerType>.
                            Or<BAccount.type.IsEqual<BAccountType.combinedType>>>.
                        And<ProcBinCapFilter.processType.FromCurrent.IsEqual<string_ar>>>.
                    Or<Brackets<
                        Brackets<BAccount.type.IsEqual<BAccountType.vendorType>.
                            Or<BAccount.type.IsEqual<BAccountType.combinedType>>>.
                        And<ProcBinCapFilter.processType.FromCurrent.IsEqual<string_ap>>>>>>>),
                            typeof(BAccount.acctCD),
                            typeof(BAccount.acctName),
                            SubstituteKey = typeof(BAccount.acctCD),
                            DescriptionField = typeof(BAccount.acctName))]
            [PXUIField(DisplayName = "Customer/Vendor ID")]
            public int? BAccountID { get; set; }
            public abstract class bAccountID : PX.Data.BQL.BqlInt.Field<bAccountID> { }
            #endregion

            #region ShowAllInv
            [PXDBBool()]
            [PXUIField(DisplayName = "Show All Inventory")]
            public virtual bool? ShowAllInv { get; set; }
            public abstract class showAllInv : PX.Data.BQL.BqlBool.Field<showAllInv> { }
            #endregion
        }
        #endregion
    }
}
