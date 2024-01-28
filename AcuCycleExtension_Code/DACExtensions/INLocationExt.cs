using PX.Data;

namespace PX.Objects.IN
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class INLocationExt : PXCacheExtension<PX.Objects.IN.INLocation>
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