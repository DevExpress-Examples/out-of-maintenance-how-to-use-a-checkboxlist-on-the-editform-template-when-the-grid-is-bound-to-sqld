<%-- BeginRegion Page setup --%>
<%@ Page Language="vb" AutoEventWireup="true"  CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.3.0, Culture=neutral, PublicKeyToken=B88D1754D700E49A"
	Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.3.0, Culture=neutral, PublicKeyToken=B88D1754D700E49A"
	Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%-- EndRegion --%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>How to use standard ListControl with multi-select feature in the ASPxGridView</title>
</head>
<body>
	<form id="form1" runat="server">
		<dxwgv:ASPxGridView ID="Grid" runat="server" KeyFieldName="ID" AutoGenerateColumns="False"
			OnRowUpdating="Grid_RowUpdating" DataSourceID="SqlDataSource1" OnRowInserting="Grid_RowInserting" >
			<SettingsEditing Mode="Inline" />
			<Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButton="True"/>
				<dxwgv:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" />
				<dxwgv:GridViewDataTextColumn FieldName="Categories" Caption="Categories" VisibleIndex="2" >
					<EditItemTemplate>
						<asp:CheckBoxList ID="List" runat="server" DataValueField="CategoryName" OnDataBound="List_DataBound" DataSourceID="SqlDataSource2" />
					</EditItemTemplate>
				</dxwgv:GridViewDataTextColumn>
			</Columns>
		</dxwgv:ASPxGridView>
		&nbsp;
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DemoDataConnectionString %>"
			DeleteCommand="DELETE FROM [Persons] WHERE [ID] = @ID" InsertCommand="INSERT INTO [Persons] ([Name], [Categories]) VALUES (@Name, @Categories)"
			SelectCommand="SELECT * FROM [Persons]" UpdateCommand="UPDATE [Persons] SET [Name] = @Name, [Categories] = @Categories WHERE [ID] = @ID">
			<DeleteParameters>
				<asp:Parameter Name="ID" Type="Int32" />
			</DeleteParameters>
			<UpdateParameters>
				<asp:Parameter Name="Name" Type="String" />
				<asp:Parameter Name="Categories" Type="String" />
				<asp:Parameter Name="ID" Type="Int32" />
			</UpdateParameters>
			<InsertParameters>
				<asp:Parameter Name="Name" Type="String" />
				<asp:Parameter Name="Categories" Type="String" />
			</InsertParameters>
		</asp:SqlDataSource>
		<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DemoDataConnectionString %>"
			SelectCommand="SELECT [CategoryName] FROM [Categories]"></asp:SqlDataSource>
		&nbsp;
	</form>
</body>
</html>
