using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects;
using System.Collections.Generic;
using System;
using static PX.Objects.IN.INSetupExt;

namespace PX.Objects.IN
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class INSetupExt : PXCacheExtension<PX.Objects.IN.INSetup>
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