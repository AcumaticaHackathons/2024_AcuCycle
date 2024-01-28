using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;

namespace AcuCycle
{
    [Serializable]
    [PXCacheName("AC Recycle Header")]
    public class ACRecycleHeader : IBqlTable
    {
        #region DocType
        [PXDBString(5, InputMask = "", IsKey = true)]
        [PXDefault("RY")]
        [PXUIField(DisplayName = "Doc Type", Enabled = false, Required = true)]
        public virtual string DocType { get; set; }
        public abstract class docType : PX.Data.BQL.BqlString.Field<docType> { }
        #endregion

        #region RefNbr
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCC")]
        [PXDefault()]
        [PXUIField(DisplayName = "Ref Nbr", Required = true)]
        [AutoNumber(typeof(Search<ACRecycleSetup.refNumberingID>), typeof(AccessInfo.businessDate))]
        [PXSelector(typeof(Search<ACRecycleHeader.refNbr, Where<ACRecycleHeader.docType.IsEqual<ACRecycleHeader.docType.FromCurrent>>>))]
        public virtual string RefNbr { get; set; }
        public abstract class refNbr : PX.Data.BQL.BqlString.Field<refNbr> { }
        #endregion

        #region Behavior
        [PXDBString(2, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Behavior")]
        public virtual string Behavior { get; set; }
        public abstract class behavior : PX.Data.BQL.BqlString.Field<behavior> { }
        #endregion

        #region LineCntr
        [PXDBInt()]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Line Counter")]
        public virtual int? LineCntr { get; set; }
        public abstract class lineCntr : PX.Data.BQL.BqlInt.Field<lineCntr> { }
        #endregion

        #region Desc
        [PXDBString(15, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Description", Required = true)]
        public virtual string Desc { get; set; }
        public abstract class desc : PX.Data.BQL.BqlString.Field<desc> { }
        #endregion

        #region IsRecycled
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Is Recycled")]
        public virtual bool? IsRecycled { get; set; }
        public abstract class isRecycled : PX.Data.BQL.BqlBool.Field<isRecycled> { }
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