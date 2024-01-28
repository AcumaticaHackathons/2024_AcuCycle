using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.FA;
using PX.Objects.PM;
using PX.Objects.RQ;
using System.Linq;

namespace PX.Objects.IN
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class INIssueEntry_Extension : PXGraphExtension<INIssueEntry>
    {
        #region Event Handlers
        protected void _(Events.FieldUpdated<INTran, INTran.inventoryID> e)
        {
            if (e.Row == null)
                return;

            InventoryItem item = InventoryItem.PK.Find(Base, e.Row.InventoryID);
            if (item != null)
            {
                InventoryItemExt itemExt = Base.Caches[typeof(InventoryItem)].GetExtension<InventoryItemExt>(item);
                if(itemExt?.UsrRecycleCategory != null)
                {
                    INSetup inSetup = new SelectFrom<INSetup>.View(Base).Select().FirstOrDefault();
                    INSetupExt setupExt = PXCache<INSetup>.GetExtension<INSetupExt>(inSetup);
                    e.Cache.SetValueExt<INTran.reasonCode>(e.Row, setupExt?.UsrRecycleReason);
                }
            }
        }

        protected void _(Events.RowPersisting<INTran> e)
        {
            if (e.Row == null)
                return;

            PXEntryStatus entryStatus = Base.transactions.Cache.GetStatus(e.Row);
            if (entryStatus == PXEntryStatus.Inserted || entryStatus == PXEntryStatus.Updated)
            {
                InventoryItem item = InventoryItem.PK.Find(Base, e.Row.InventoryID);
                InventoryItemExt itemExt = Base.Caches[typeof(InventoryItem)].GetExtension<InventoryItemExt>(item);

                if (itemExt.UsrRecycleCategory == null)
                    return;

                var Q = new SelectFrom<INLocation>
                    .Where<Use<INLocation.siteID>.AsInt.IsEqual<@P.AsInt>.
                        And<Use<INLocation.locationID>.AsInt.IsEqual<@P.AsInt>>>
                    .View(Base);

                INLocation location = Q.Select(e.Row.SiteID, e.Row.LocationID).FirstOrDefault();
                if (location != null)
                {
                    INLocationExt locExt = Base.Caches[typeof(INLocation)].GetExtension<INLocationExt>(location);
                    string uom = locExt?.UsrUOM ?? item?.BaseUnit;

                    if (locExt.UsrCapacity != null)
                    {
                        INLocationStatus siteStatus = INLocationStatus.PK.Find(Base, e.Row.InventoryID, e.Row.SubItemID, location.SiteID, location.LocationID);

                        decimal baseQty = INUnitAttribute.ConvertToBase(e.Cache, item.InventoryID, e.Row.UOM, e.Row.Qty ?? 0m,INPrecision.QUANTITY); 
                        decimal capacityQty = INUnitAttribute.ConvertToBase(e.Cache,item.InventoryID, uom, locExt.UsrCapacity ?? 0, INPrecision.QUANTITY);

                        if(baseQty + siteStatus.QtyOnHand > capacityQty)
                        {
                            e.Cache.RaiseExceptionHandling<INTran.qty>(e.Row, e.Row.Qty,
                            new PXSetPropertyException(Messages.OVER_CAPACITY, PXErrorLevel.Error));

                            throw new PXException(Messages.OVER_CAPACITY);
                        }
                    }
                }
            }
        }
        #endregion

        #region Messages

        [PXLocalizable]
        public class Messages
        {
            public const string OVER_CAPACITY = "Location will go over capacity";
        }

        #endregion
    }
}