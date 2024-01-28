using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System.Linq;

namespace AcuCycle
{
    public class KitAssemblyEntryExt : PXGraphExtension<KitAssemblyEntry>
    {
        #region Event Handlers

        protected void _(Events.FieldUpdated<INComponentTran, INComponentTran.inventoryID> e)
        {
            if (e.Row == null)
                return;

            if (Base.Document.Current.DocType != INDocType.Disassembly)
                return;

            InventoryItem item = InventoryItem.PK.Find(Base, e.Row.InventoryID);
            if (item != null)
            {
                InventoryItemExt itemExt = PXCache<InventoryItem>.GetExtension<InventoryItemExt>(item);
                if (itemExt?.UsrRecycleCategory != null)
                {
                    INSetup inSetup = new SelectFrom<INSetup>.View(Base).Select().FirstOrDefault();
                    INSetupExt setupExt = PXCache<INSetup>.GetExtension<INSetupExt>(inSetup);
                    e.Cache.SetValueExt<INComponentTran.reasonCode>(e.Row, setupExt?.UsrRecycleReason);
                }
            }
        }

        protected void _(Events.RowPersisting<INComponentTran> e)
        {
            if (e.Row == null)
                return;

            if (Base.Document.Current.DocType != INDocType.Disassembly)
                return;

            PXEntryStatus entryStatus = Base.Components.Cache.GetStatus(e.Row);
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
                    INLocationExt locExt = PXCache<INLocation>.GetExtension<INLocationExt>(location);
                    string uom = locExt?.UsrUOM ?? item?.BaseUnit;

                    if (locExt.UsrCapacity != null)
                    {
                        INLocationStatus siteLocStatus = INLocationStatus.PK.Find(Base, e.Row.InventoryID, e.Row.SubItemID, location.SiteID, location.LocationID);

                        decimal baseQty = INUnitAttribute.ConvertToBase(e.Cache, item.InventoryID, e.Row.UOM, e.Row.Qty ?? 0m, INPrecision.QUANTITY);
                        decimal capacityQty = INUnitAttribute.ConvertToBase(e.Cache, item.InventoryID, uom, locExt.UsrCapacity ?? 0, INPrecision.QUANTITY);

                        if ((baseQty + (siteLocStatus?.QtyOnHand ?? 0m)) > capacityQty)
                        {
                            e.Cache.RaiseExceptionHandling<INComponentTran.qty>(e.Row, e.Row.Qty,
                            new PXSetPropertyException(Messages.OVER_CAPACITY, PXErrorLevel.Error));

                            throw new PXException(Messages.OVER_CAPACITY);
                        }
                    }
                }
            }
        }
        #endregion
    }
}