using PX.Data;

namespace PX.Objects.CR
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class BAccountExt : PXCacheExtension<PX.Objects.CR.BAccount>
    {
        #region UsrBuyRecycle
        [PXDBBool]
        [PXUIField(DisplayName = "Buy Recycle")]

        public bool? UsrBuyRecycle { get; set; }
        public abstract class usrBuyRecycle : PX.Data.BQL.BqlBool.Field<usrBuyRecycle> { }
        #endregion

        #region UsrSaleRecycle
        [PXDBBool]
        [PXUIField(DisplayName = "Sale Recycle")]

        public bool? UsrSaleRecycle { get; set; }
        public abstract class usrSaleRecycle : PX.Data.BQL.BqlBool.Field<usrSaleRecycle> { }
        #endregion
    }
}