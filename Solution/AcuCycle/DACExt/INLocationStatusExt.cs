﻿using PX.Data;
using PX.Objects.IN;
using System;

namespace AcuCycle
{
    public sealed class INLocationStatusExt : PXCacheExtension<INLocationStatus>
    {
        public static bool IsActive() { return true; }

        #region Selected
        [PXBool()]
        [PXUIField(DisplayName = "Selected")]
        public bool? Selected { get; set; }
        public abstract class selected : PX.Data.BQL.BqlBool.Field<selected> { }
        #endregion
    }
}