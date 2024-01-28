using PX.Data;
using PX.Objects.IN;

namespace AcuCycle
{
    public sealed class INRegisterExt : PXCacheExtension<INRegister>
    {
        #region UsrACDocType
        [PXDBString(5, InputMask = "")]
        [PXUIField(DisplayName = "Recycle Doc Type")]
        public string UsrACDocType { get; set; }
        public abstract class usrACDocType : PX.Data.BQL.BqlString.Field<usrACDocType> { }
        #endregion

        #region UsrACRefNbr
        [PXDBString(15, InputMask = "")]
        [PXUIField(DisplayName = "Recycle Ref Nbr")]
        public string UsrACRefNbr { get; set; }
        public abstract class usrACRefNbr : PX.Data.BQL.BqlString.Field<usrACRefNbr> { }
        #endregion
    }
}