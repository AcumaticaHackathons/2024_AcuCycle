using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCycle
{
    #region Messages

    [PXLocalizable]
    public class Messages
    {
        public const string OVER_CAPACITY = "Location will go over capacity";
    }

    #endregion

    #region BQL Constants
    public class string_ar : PX.Data.BQL.BqlString.Constant<string_ar>
    {
        public string_ar()
          : base("AR")
        {
        }
    }
    public class string_ap : PX.Data.BQL.BqlString.Constant<string_ap>
    {
        public string_ap()
          : base("AP")
        {
        }
    }
    public class decimal_0 : PX.Data.BQL.BqlDecimal.Constant<decimal_0>
    {
        public decimal_0()
          : base(0m)
        {
        }
    }
    public class decimal_dot80 : PX.Data.BQL.BqlDecimal.Constant<decimal_dot80>
    {
        public decimal_dot80()
          : base(0.8m)
        {
        }
    }
    #endregion
}
