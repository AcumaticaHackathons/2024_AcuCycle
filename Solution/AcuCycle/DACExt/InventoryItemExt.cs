using PX.Data;
using PX.Objects.IN;

namespace AcuCycle
{
    public sealed class InventoryItemExt : PXCacheExtension<InventoryItem>
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