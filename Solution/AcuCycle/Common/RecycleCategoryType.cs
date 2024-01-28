using PX.Data;

namespace AcuCycle
{
    public abstract class RecycleCategoryType : PX.Data.IBqlField
    {
        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute()
                : base(
                    Pair(null, ""),
                    Pair(Waste, "Waste"),
                    Pair(Recycle, "Recycle"),
                    Pair(Refurbish, "Refurbish"),
                    Pair(Toxic, "Toxic")
                )
            { }
        }
        public const string Waste = "W";
        public class waste : PX.Data.BQL.BqlString.Constant<waste> { public waste() : base(Waste) { } }

        public const string Recycle = "R";
        public class recycle : PX.Data.BQL.BqlString.Constant<recycle> { public recycle() : base(Recycle) { } }

        public const string Refurbish = "F";
        public class refurbish : PX.Data.BQL.BqlString.Constant<refurbish> { public refurbish() : base(Refurbish) { } }

        public const string Toxic = "T";
        public class toxic : PX.Data.BQL.BqlString.Constant<toxic> { public toxic() : base(Toxic) { } }
    }
}
