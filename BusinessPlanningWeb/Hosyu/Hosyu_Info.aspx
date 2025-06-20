<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Hosyu_Info.aspx.vb" Inherits="Hosyu_Hosyu_Info" title="保守未リプレース先ユーザー" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" 
            ForeColor="#3333FF"></asp:Label>
    </p>
    <p>
    <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  保守未リプレース先ユーザー" Font-Italic="True" 
            Width="500px"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label4" runat="server" Text="担当支店："></asp:Label>
    <asp:DropDownList ID="D_拠点" runat="server" AutoPostBack="True">
        <asp:ListItem Value="%">すべて</asp:ListItem>
        <asp:ListItem Value="札幌支店">札幌支店</asp:ListItem>
        <asp:ListItem Value="仙台支店">仙台支店</asp:ListItem>
        <asp:ListItem Value="首都圏営業部">首都圏営業部</asp:ListItem>
        <asp:ListItem Value="名古屋支店">名古屋支店</asp:ListItem>
        <asp:ListItem Value="大阪支店">大阪支店</asp:ListItem>
        <asp:ListItem Value="金沢営業所">金沢営業所</asp:ListItem>
        <asp:ListItem Value="広島営業所">広島営業所</asp:ListItem>
        <asp:ListItem>福岡支店</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" Text="前システム種別："></asp:Label>
    <asp:DropDownList ID="D_前システム" runat="server" AutoPostBack="True">
        <asp:ListItem Value="%">すべて</asp:ListItem>
        <asp:ListItem Value="QURIA">QURIA</asp:ListItem>
        <asp:ListItem Value="LAPEC">LAPEC</asp:ListItem>
        <asp:ListItem>PREST</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
    </p>
    <p>
        <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            DataSourceID="SqlDataSource1" GridLines="Vertical" Font-Size="Small">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </p>
</asp:Content>

