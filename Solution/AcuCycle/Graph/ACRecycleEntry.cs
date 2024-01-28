using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using static PX.SM.EMailAccount;

namespace AcuCycle
{
    public class ACRecycleEntry : PXGraph<ACRecycleEntry, ACRecycleHeader>
    {
        #region Views
        public PXSetup<ACRecycleSetup> Setup;

        public PXSelect<ACRecycleHeader> Document;
        public SelectFrom<ACRecycleDetails>.
            Where<ACRecycleDetails.docType.IsEqual<ACRecycleHeader.docType.FromCurrent>.
            And<ACRecycleDetails.refNbr.IsEqual<ACRecycleHeader.refNbr.FromCurrent>>>.View Transactions;
        #endregion

        #region Event Handler
        protected virtual void _(Events.RowPersisting<ACRecycleHeader> e)
        {
            AutoNumberAttribute.SetNumberingId<ACRecycleHeader.refNbr>(e.Cache, Setup.Current.RefNumberingID);
        }
        #endregion
    }
}