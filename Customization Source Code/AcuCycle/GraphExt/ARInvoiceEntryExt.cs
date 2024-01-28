using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.IN;
using PX.Objects.SO;
using System.Collections;
using System.Linq;

namespace AcuCycle
{
    public class ARInvoiceEntryExt : PXGraphExtension<ARInvoiceEntry>
    {
        #region Actions
        public delegate IEnumerable ReleaseDelegate(PXAdapter adapter);
        [PXOverride]
        public IEnumerable Release(PXAdapter adapter, ReleaseDelegate baseMethod)
        {
            // TODO: Support Mass Processing
            INIssueEntry inGraph = PXGraph.CreateInstance<INIssueEntry>();
            INRegister issue = inGraph.issue.Current = SelectFrom<INRegister>.Where<INRegisterExt.usrACDocType.IsEqual<P.AsString>.And<INRegisterExt.usrACRefNbr.IsEqual<P.AsString>>>.View.Select(inGraph, Base.Document.Current.DocType, Base.Document.Current.RefNbr);
            if (issue?.RefNbr != null)
            {
                inGraph.release.Press(adapter);
                //PXAutomation.CompleteAction(inGraph);
                PXLongOperation.WaitCompletion(inGraph.UID);
                PXLongOperation.ClearStatus(inGraph.UID);
            }
            return baseMethod(adapter);
        }
        #endregion
    }
}