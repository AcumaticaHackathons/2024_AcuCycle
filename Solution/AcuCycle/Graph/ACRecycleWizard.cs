using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.Objects.PO;

namespace AcuCycle
{
    public class ACRecycleWizard : PXGraph<ACRecycleWizard>
    {
        #region Views
        //public PXSave<MasterTable> Save;
        public PXCancel<ACRecycleWizardFilter> Cancel;
        public PXFilter<ACRecycleWizardFilter> Filter;

        public PXSetup<ACRecycleSetup> Setup;

        public SelectFrom<INLocationStatus>.
            InnerJoin<InventoryItem>.
                On<InventoryItem.inventoryID.IsEqual<INLocationStatus.inventoryID>>.
            Where<INLocationStatus.siteID.IsEqual<ACRecycleSetup.siteID.FromCurrent>
                .And<INLocationStatus.qtyHardAvail.IsGreater<decimal_0>>
                .And<InventoryItem.kitItem.IsEqual<True>>
                .And<Brackets<INLocationStatus.inventoryID.IsEqual<ACRecycleWizardFilter.inventoryID.FromCurrent>
                    .Or<ACRecycleWizardFilter.inventoryID.FromCurrent.IsNull>>>>.View Results;
        #endregion

        #region Actions
        public PXAction<ACRecycleWizardFilter> CreateRecycleDoc;
        [PXUIField(DisplayName = "Create Recycle Document")]
        [PXButton()]
        public virtual IEnumerable createRecycleDoc(PXAdapter adapter)
        {
            List<INLocationStatus> results = Results.Select().RowCast<INLocationStatus>().ToList();
            
            ACRecycleEntry graph = PXGraph.CreateInstance<ACRecycleEntry>();
            ACRecycleHeader header = graph.Document.Insert();
            graph.Document.Current = header;
            header.Desc = "Wizard Created Entry - " + PX.Common.PXTimeZoneInfo.Now;
            graph.Document.Update(header);
            graph.Actions.PressSave();

            foreach (INLocationStatus result in results)
            {
                INLocationStatusExt resultExt = result.GetExtension<INLocationStatusExt>();
                if (resultExt.Selected ?? false)
                {
                    ACRecycleDetails details = graph.Transactions.Insert();
                    graph.Transactions.Current = details;

                    details.InventoryID = result.InventoryID;
                    details.Qty = result.QtyHardAvail;

                    graph.Transactions.Update(details);
                    graph.Actions.PressSave();
                }
            }

            throw new PXRedirectRequiredException(graph, "Recycle Entry");
            //return adapter.Get();
        }
        #endregion

        #region Classes
        [PXHidden]
        [Serializable]
        public partial class ACRecycleWizardFilter : PX.Data.IBqlTable
        {
            #region InventoryID
            [Inventory()]
            [PXUIField(DisplayName = "Inventory ID")]
            public virtual int? InventoryID { get; set; }
            public abstract class inventoryID : PX.Data.BQL.BqlInt.Field<inventoryID> { }
            #endregion

            #region ItemClass
            [PXDimensionSelector(INItemClass.Dimension, typeof(Search<INItemClass.itemClassID>), typeof(INItemClass.itemClassCD), DescriptionField = typeof(INItemClass.descr), CacheGlobal = true)]
            [PXUIField(DisplayName = "Item Class")]
            public virtual int? ItemClass { get; set; }
            public abstract class itemClass : PX.Data.BQL.BqlInt.Field<itemClass> { }
            #endregion
        }
        #endregion

    }
}