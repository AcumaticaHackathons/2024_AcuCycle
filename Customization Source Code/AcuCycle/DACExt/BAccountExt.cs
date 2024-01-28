using PX.Data;
using PX.Objects.CR;

namespace AcuCycle
{
    public sealed class BAccountExt : PXCacheExtension<BAccount>
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