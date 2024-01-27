using System;
using PX.Data;

namespace AcuCycle
{
    public class RecycleSetupMaint : PXGraph<RecycleSetupMaint>
    {
        #region Views
        public PXSave<RecycleSetup> Save;
        public PXCancel<RecycleSetup> Cancel;

        public PXFilter<RecycleSetup> Setup;
        #endregion
    }
}