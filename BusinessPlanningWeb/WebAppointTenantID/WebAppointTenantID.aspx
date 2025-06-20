<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="WebAppointTenantID.aspx.vb" Inherits="WebAppointTenantID_WebAppointTenantID" title="Web予約受付テナントID検索" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
        <br />
        <asp:Label ID="labelWeb予約受付テナントID検索" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  Web予約受付テナントID検索" Font-Italic="True" 
            Width="500px"></asp:Label>
        <br />
		&nbsp;
        <asp:Label ID="label顧客No" runat="server" Text="顧客No"></asp:Label>
        <asp:TextBox ID="textBox顧客No" runat="server" Width="87px" ToolTip="YYYY/MM/DD"></asp:TextBox>
        <asp:Button ID="button検索" runat="server" Text="検索" />
        &nbsp;
		<asp:Button ID="buttonOutputExcel" runat="server" Text="EXCEL出力" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="textBox顧客No" ErrorMessage="顧客No入力に誤りがあります。(数字８桁)" 
            ValidationExpression="\d{8}">※</asp:RegularExpressionValidator>
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <p>
        <asp:GridView ID="GridViewTenantID" runat="server" DataSourceID="SqlDataSource" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="False" >
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="拠点名" HeaderText="拠点名" />
                <asp:BoundField DataField="顧客No" HeaderText="顧客No" />
                <asp:BoundField DataField="顧客名" HeaderText="顧客名" />
                <asp:BoundField DataField="テナントID" HeaderText="テナントID" />
                <asp:BoundField DataField="拠点コード" Visible="False" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </p>
<asp:SqlDataSource ID="SqlDataSource" runat="server"></asp:SqlDataSource>
</asp:Content>

