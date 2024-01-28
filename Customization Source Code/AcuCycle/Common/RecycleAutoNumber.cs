using System;
using PX.Data;
using PX.Common;

namespace AcuCycle
{
    public class RecycleAutoNumber : PX.Objects.CS.AutoNumberAttribute
    {
        public RecycleAutoNumber(Type doctypeField, Type dateField)
          : base(doctypeField, dateField)
        {
        }
        public RecycleAutoNumber(Type doctypeField, Type dateField, string[] doctypeValues, Type[] setupFields)
          : base(doctypeField, dateField, doctypeValues, setupFields)
        {
        }

        public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
        {
            base.RowPersisting(sender, e);

            string generated = (string)sender.GetValue(e.Row, _FieldName);
            generated += "AA";
            sender.SetValue(e.Row, _FieldName, generated);
        }
    }
}