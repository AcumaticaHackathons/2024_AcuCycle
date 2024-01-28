using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace AcuCycle
{
    public class ACRecycleSetupMaint : PXGraph<ACRecycleSetupMaint>
    {
        #region Views
        public PXSave<ACRecycleSetup> Save;
        public PXCancel<ACRecycleSetup> Cancel;

        public SelectFrom<ACRecycleSetup>.View Setup;
        #endregion
    }
}