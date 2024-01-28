<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false"
	CodeFile="AC301000.aspx.cs" Inherits="Page_AC301000" Title="Untitled Page" %>
	<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

		<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
			<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="AcuCycle.ACRecycleEntry"
				PrimaryView="Document">
				<CallbackCommands>
				</CallbackCommands>
			</px:PXDataSource>
		</asp:Content>
		<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
			<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Document" Width="100%" Height="100px"
				AllowAutoHide="false">
				<Template>
					<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
					<px:PXTextEdit CommitChanges="True" runat="server" ID="CstPXTextEdit1" DataField="DocType" />
					<px:PXSelector CommitChanges="True" runat="server" ID="CstPXSelector2" DataField="RefNbr">
					</px:PXSelector>
					<px:PXTextEdit CommitChanges="True" runat="server" ID="CstPXTextEdit2" DataField="Desc"
						Width="300px" />
				</Template>
			</px:PXFormView>
		</asp:Content>
		<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
			<px:PXTab Width="100%" runat="server" ID="CstPXTab3">
				<Items>
					<px:PXTabItem Visible="True" Text="Details">
						<Template>
							<px:PXGrid Width="100%" runat="server" ID="tranGrid" Height="150px" SkinID="Details">
								<Levels>
									<px:PXGridLevel DataMember="Transactions">
										<Columns>
											<px:PXGridColumn DataField="LineNbr" Width="70"></px:PXGridColumn>
											<px:PXGridColumn DataField="InventoryID" Width="70" CommitChanges="True" />
											<px:PXGridColumn DataField="Qty" Width="70" CommitChanges="True">
											</px:PXGridColumn>
											<px:PXGridColumn DataField="SiteID" CommitChanges="True" Width="70" />
											<px:PXGridColumn DataField="LocationID" CommitChanges="True" Width="70" />
											<px:PXGridColumn DataField="INRefNbr" Width="70"></px:PXGridColumn>
											<px:PXGridColumn DataField="INDocType" Width="70"></px:PXGridColumn>
											<px:PXGridColumn DataField="AssemblyRefNbr" Width="70"></px:PXGridColumn>
											<px:PXGridColumn DataField="AssemblyDocType" Width="70"></px:PXGridColumn>
										</Columns>
									</px:PXGridLevel>
								</Levels>
							</px:PXGrid>
						</Template>
					</px:PXTabItem>
				</Items>
			</px:PXTab>
		</asp:Content>