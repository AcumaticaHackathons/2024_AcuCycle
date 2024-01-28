<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false"
	CodeFile="AC501000.aspx.cs" Inherits="Page_AC501000" Title="Untitled Page" %>

	<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
		<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
			<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="AcuCycle.ACProcBinCap"
				PrimaryView="Filter" />
		</asp:Content>

		<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
			<px:PXFormView ID="formPrintSettings" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
				DataMember="Filter">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M"
						StartGroup="True" GroupCaption="Filter" />
					<px:PXSelector CommitChanges="True" ID="edLocationID" runat="server" DataField="LocationID" />
					<px:PXSelector CommitChanges="True" ID="edInventoryID" runat="server" DataField="InventoryID" />
					<px:PXCheckBox CommitChanges="True" ID="edShowAllInv" runat="server" DataField="ShowAllInv" />
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M"
						StartGroup="True" GroupCaption="Settings" />
					<px:PXDropDown CommitChanges="True" ID="edProcessType" runat="server" DataField="ProcessType" />
					<px:PXSelector CommitChanges="True" ID="edBAccountID" runat="server" DataField="BAccountID" />
				</Template>
			</px:PXFormView>
		</asp:Content>

		<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
			<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100" Width="100%"
				AllowPaging="true" AdjustPageSize="Auto" SkinID="PrimaryInquire" BatchUpdate="true" SyncPosition="True">
				<Levels>
					<px:PXGridLevel DataMember="FullBins">
						<RowTemplate>
							<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="M">
							</px:PXLayoutRule>
							<px:PXCheckBox ID="chkSelected" runat="server" DataField="Selected"></px:PXCheckBox>
							<px:PXSelector ID="edInventoryID" runat="server" AllowNull="False" DataField="InventoryID"
								Enabled="False"></px:PXSelector>
							<px:PXSegmentMask ID="edSiteID" runat="server" DataField="SiteID" Enabled="False">
							</px:PXSegmentMask>
							<px:PXDropDown ID="edSiteStatus" runat="server" DataField="SiteStatus" Enabled="False">
							</px:PXDropDown>
						</RowTemplate>
						<Columns>
							<px:PXGridColumn AllowNull="False" DataField="Selected" TextAlign="Center" Type="CheckBox"
								AllowCheckAll="True" AllowSort="False" AllowMove="False"></px:PXGridColumn>
							<px:PXGridColumn AllowUpdate="False" DataField="InventoryID" LinkCommand="ViewItem">
							</px:PXGridColumn>
							<px:PXGridColumn AllowUpdate="False" DataField="SiteID" LinkCommand="ViewSite">
							</px:PXGridColumn>
							<px:PXGridColumn AllowUpdate="False" DataField="InventoryItem__Descr"></px:PXGridColumn>
						</Columns>
					</px:PXGridLevel>
				</Levels>
				<AutoSize Container="Window" Enabled="True" MinHeight="200" />
			</px:PXGrid>
		</asp:Content>