using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;

namespace AcuCycle
{
    [Serializable]
    [PXCacheName("ACRecycleDetails")]
    public class ACRecycleDetails : IBqlTable
    {
        #region DocType
        [PXDBString(5, IsKey = true)]
        [PXDefault(typeof(ACRecycleHeader.docType))]
        [PXUIField(DisplayName = "Doc Type")]
        public virtual string DocType { get; set; }
        public abstract class docType : PX.Data.BQL.BqlString.Field<docType> { }
        #endregion

        #region RefNbr
        [PXDBString(15, IsKey = true)]
        [PXDBDefault(typeof(ACRecycleHeader.refNbr), DefaultForUpdate = false)]
        [PXParent(typeof(Select<ACRecycleHeader, Where<ACRecycleHeader.refNbr.IsEqual<ACRecycleDetails.refNbr.FromCurrent>.And<ACRecycleHeader.docType.IsEqual<ACRecycleDetails.docType.FromCurrent>>>>))]
        [PXUIField(DisplayName = "Ref Nbr")]
        public virtual string RefNbr { get; set; }
        public abstract class refNbr : PX.Data.BQL.BqlString.Field<refNbr> { }
        #endregion

        #region LineNbr
        [PXDBInt(IsKey = true)]
        [PXLineNbr(typeof(ACRecycleHeader.lineCntr))]
        [PXUIField(DisplayName = "Line Nbr")]
        public virtual int? LineNbr { get; set; }
        public abstract class lineNbr : PX.Data.BQL.BqlInt.Field<lineNbr> { }
        #endregion

        #region LineType
        [PXDBString(5)]
        [PXUIField(DisplayName = "Line Type")]
        public virtual string LineType { get; set; }
        public abstract class lineType : PX.Data.BQL.BqlString.Field<lineType> { }
        #endregion

        #region InventoryID
        [Inventory]
        [PXUIField(DisplayName = "Inventory ID")]
        public virtual int? InventoryID { get; set; }
        public abstract class inventoryID : PX.Data.BQL.BqlInt.Field<inventoryID> { }
        #endregion

        #region SiteID
        [Site]
        [PXUIField(DisplayName = "Warehouse")]
        public virtual int? SiteID { get; set; }
        public abstract class siteID : PX.Data.BQL.BqlInt.Field<siteID> { }
        #endregion

        #region LocationID
        [PXDBInt()]
        [PXSelector(typeof(Search<INLocation.locationID, Where<INLocation.siteID.IsEqual<ACRecycleDetails.siteID.FromCurrent>>>), SubstituteKey = typeof(INLocation.locationCD))]
        [PXUIField(DisplayName = "Location ID")]
        public virtual int? LocationID { get; set; }
        public abstract class locationID : PX.Data.BQL.BqlInt.Field<locationID> { }
        #endregion

        #region Qty
        [PXDBQuantity]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty")]
        public virtual decimal? Qty { get; set; }
        public abstract class qty : PX.Data.BQL.BqlDecimal.Field<qty> { }
        #endregion

        #region AssemblyDocType
        [PXDBString(5)]
        [PXUIField(DisplayName = "Assembly Doc Type", Enabled = false)]
        public virtual string AssemblyDocType { get; set; }
        public abstract class assemblyDocType : PX.Data.BQL.BqlString.Field<assemblyDocType> { }
        #endregion

        #region AssemblyRefNbr
        [PXDBString(15)]
        [PXUIField(DisplayName = "Assembly Ref Nbr", Enabled = false)]
        public virtual string AssemblyRefNbr { get; set; }
        public abstract class assemblyRefNbr : PX.Data.BQL.BqlString.Field<assemblyRefNbr> { }
        #endregion

        #region INDocType
        [PXDBString(5)]
        [PXUIField(DisplayName = "IN Doc Type", Enabled = false)]
        public virtual string INDocType { get; set; }
        public abstract class iNDocType : PX.Data.BQL.BqlString.Field<iNDocType> { }
        #endregion

        #region INRefNbr
        [PXDBString(15)]
        [PXUIField(DisplayName = "IN Ref Nbr", Enabled = false)]
        public virtual string INRefNbr { get; set; }
        public abstract class iNRefNbr : PX.Data.BQL.BqlString.Field<iNRefNbr> { }
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