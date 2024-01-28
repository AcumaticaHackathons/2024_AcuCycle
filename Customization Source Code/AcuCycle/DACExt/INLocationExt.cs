using PX.Data;
using PX.Objects.IN;

namespace AcuCycle
{
    public sealed class INLocationExt : PXCacheExtension<INLocation>
    {
        #region UsrCapacity
        [PXDBDecimal]
        [PXUIField(DisplayName = "Capacity")]
        public decimal? UsrCapacity { get; set; }
        public abstract class usrCapacity : PX.Data.BQL.BqlString.Field<usrCapacity> { }
        #endregion

        #region UsrUOM
        [INUnit(DisplayName = "UOM", Visibility = PXUIVisibility.SelectorVisible)]
        public string UsrUOM { get; set; }
        public abstract class usrUOM : PX.Data.BQL.BqlString.Field<usrUOM> { }
        #endregion
    }
}