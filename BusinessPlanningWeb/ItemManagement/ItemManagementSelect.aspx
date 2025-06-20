<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="ItemManagementSelect.aspx.vb" Inherits="ItemManagement_ItemManagementSelect" title="無題のページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
    <br />
    <asp:Label ID="Label1" runat="server" Text="ユーザー/見込み客選択"></asp:Label>
</p>
<p>
    <asp:Label ID="Label2" runat="server" Text="顧客番号検索"></asp:Label>
    　　　　<asp:TextBox ID="T_顧客No" runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="Label3" runat="server" Text="電話番号検索"></asp:Label>
    　　　　<asp:TextBox ID="T_電話番号" runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="Label4" runat="server" Text="医院名検索"></asp:Label>
    　　　 　&nbsp;&nbsp; <asp:TextBox ID="T_医院名" runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="Label5" runat="server">住所検索</asp:Label>
    　　　　　&nbsp;&nbsp; 　<asp:TextBox ID="T_住所" runat="server"></asp:TextBox>
</p>
    <p>
        <asp:Button ID="B_検索" runat="server" Text="検索" />
        　　<asp:Label ID="L_Error" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    </p>
<p>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataSourceID="SqlDataSource1" Font-Size="Small" ForeColor="Black" 
        GridLines="Horizontal">
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <Columns>
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
            <asp:BoundField DataField="支店名" HeaderText="支店名" SortExpression="支店名" />
            <asp:BoundField DataField="顧客No" HeaderText="顧客No" />
            <asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" />
            <asp:BoundField DataField="住所" HeaderText="住所" SortExpression="住所" />
            <asp:BoundField DataField="電話番号" HeaderText="電話番号" SortExpression="電話番号" />
            <asp:BoundField DataField="都道府県名" HeaderText="都道府県名" SortExpression="都道府県名" />
        </Columns>
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
</p>
<p>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </p>
<p>
        <asp:Button ID="Button1" runat="server" Text="決定" />
        &nbsp; <asp:Button ID="Button2" runat="server" Text="キャンセル" />
    </p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
</asp:Content>

