using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;

namespace AcuCycle
{
    public sealed class INSetupExt : PXCacheExtension<INSetup>
    {
        #region UsrRecycleReason
        [PXDBString(20)]
        [PXUIField(DisplayName = "Recycle Reason Code")]
        [PXSelector(typeof(Search<ReasonCode.reasonCodeID, Where<ReasonCode.usage, Equal<ReasonCodeUsages.assemblyDisassembly>>>), DescriptionField = typeof(ReasonCode.descr))]
        public string UsrRecycleReason { get; set; }
        public abstract class usrRecycleReason : PX.Data.BQL.BqlString.Field<usrRecycleReason> { }
        #endregion
    }
}