using PX.Data;
using PX.Data.BQL.Fluent;
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
                InventoryItemExt itemExt = PXCache<InventoryItem>.GetExtension<InventoryItemExt>(item);
                if(itemExt?.UsrRecycleCategory != null)
                {
                    INSetup inSetup = new SelectFrom<INSetup>.View(Base).Select().FirstOrDefault();
                    INSetupExt setupExt = PXCache<INSetup>.GetExtension<INSetupExt>(inSetup);
                    e.Cache.SetValueExt<INTran.reasonCode>(e.Row, setupExt?.UsrRecycleReason);
                }
            }
        }

        #endregion


    }
}