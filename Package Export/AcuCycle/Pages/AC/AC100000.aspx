<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false"
	CodeFile="AC100000.aspx.cs" Inherits="Page_AC100000" Title="Untitled Page" %>
	<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

		<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
			<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="AcuCycle.ACRecycleSetupMaint"
				PrimaryView="Setup">
				<CallbackCommands>

				</CallbackCommands>
			</px:PXDataSource>
		</asp:Content>
		<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
			<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Setup" Width="100%"
				AllowAutoHide="false">
				<Template>
					<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
					<px:PXLayoutRule runat="server" ID="CstPXLayoutRule2" StartGroup="True"
						GroupCaption="General Settings" LabelsWidth="M" />
					<px:PXSelector CommitChanges="True" runat="server" ID="CstPXSelector1" DataField="RefNumberingID">
					</px:PXSelector>
					<px:PXSelector CommitChanges="True" runat="server" ID="CstPXSelector2" DataField="SiteID">
					</px:PXSelector>
					<px:PXSelector CommitChanges="True" runat="server" ID="CstPXSelector3" DataField="ChargeFeeID">
					</px:PXSelector>
				</Template>
				<AutoSize Container="Window" Enabled="True" MinHeight="200"></AutoSize>
			</px:PXFormView>
		</asp:Content>