using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;

namespace AcuCycle
{
    [Serializable]
    [PXCacheName("Recycle Preferences")]
    public class ACRecycleSetup : IBqlTable
    {
        #region RefNumberingID
        [PXDBString(10, IsUnicode = true)]
        [PXDefault]
        [PXSelector(typeof(Search<Numbering.numberingID>))]
        [PXUIField(DisplayName = "Reference Numbering Sequence")]
        public virtual string RefNumberingID { get; set; }
        public abstract class refNumberingID : PX.Data.BQL.BqlString.Field<refNumberingID> { }
        #endregion

        #region SiteID
        [Site]
        [PXDefault]
        [PXUIField(DisplayName = "Recycle Warehouse")]
        public virtual int? SiteID { get; set; }
        public abstract class siteID : PX.Data.BQL.BqlInt.Field<siteID> { }
        #endregion

        #region ChargeFeeID
        [NonStockItem]
        [PXUIField(DisplayName = "Recycle Charge Fee Item", Required = true)]
        public int? ChargeFeeID { get; set; }
        public abstract class chargeFeeID : PX.Data.BQL.BqlInt.Field<chargeFeeID> { }
        #endregion

        #region Tstamp
        [PXDBTimestamp()]
        [PXUIField(DisplayName = "Tstamp")]
        public virtual byte[] Tstamp { get; set; }
        public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
        #endregion

        #region CreatedByID
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID { get; set; }
        public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
        #endregion

        #region CreatedByScreenID
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID { get; set; }
        public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
        #endregion

        #region CreatedDateTime
        [PXDBCreatedDateTime()]
        public virtual DateTime? CreatedDateTime { get; set; }
        public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
        #endregion

        #region LastModifiedByID
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID { get; set; }
        public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
        #endregion

        #region LastModifiedByScreenID
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID { get; set; }
        public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
        #endregion

        #region LastModifiedDateTime
        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
        #endregion

        #region Noteid
        [PXNote()]
        public virtual Guid? Noteid { get; set; }
        public abstract class noteid : PX.Data.BQL.BqlGuid.Field<noteid> { }
        #endregion
    }
}