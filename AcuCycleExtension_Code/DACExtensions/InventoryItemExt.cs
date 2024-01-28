using PX.Data;

namespace PX.Objects.IN
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class InventoryItemExt : PXCacheExtension<PX.Objects.IN.InventoryItem>
    {
        #region UsrRecycleCategory
        [PXDBString]
        [PXUIField(DisplayName = "Recycle Category")]
        [AcuCycle.RecycleCategoryType.List]
        public string UsrRecycleCategory { get; set; }
        public abstract class usrRecycleCategory : PX.Data.BQL.BqlString.Field<usrRecycleCategory> { }
        #endregion
    }
}