<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="ItemManagementRegister.aspx.vb" Inherits="ItemManagement_Default" title="無題のページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
    <br />
    <asp:Label ID="Label1" runat="server" Text="登録月"></asp:Label>
        　　　　<asp:DropDownList ID="D_登録月" runat="server">
        <asp:ListItem>2009/01</asp:ListItem>
        <asp:ListItem>2009/02</asp:ListItem>
        <asp:ListItem>2009/03</asp:ListItem>
        <asp:ListItem>2009/04</asp:ListItem>
        <asp:ListItem>2009/05</asp:ListItem>
        <asp:ListItem>2009/06</asp:ListItem>
        <asp:ListItem>2009/07</asp:ListItem>
        <asp:ListItem>2009/08</asp:ListItem>
        <asp:ListItem>2009/09</asp:ListItem>
        <asp:ListItem>2009/10</asp:ListItem>
        <asp:ListItem>2009/11</asp:ListItem>
        <asp:ListItem>2009/12</asp:ListItem>
    </asp:DropDownList>
        <asp:Label ID="Label10" runat="server" ForeColor="Red" Text="※"></asp:Label>
</p>
<p>
    <asp:Label ID="Label2" runat="server" Text="担当者"></asp:Label>
    　　　　<asp:DropDownList ID="D_担当者" runat="server">
        <asp:ListItem>今村 元樹</asp:ListItem>
        <asp:ListItem>今村 元樹</asp:ListItem>
        <asp:ListItem>今村 元樹</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="※"></asp:Label>
</p>
<p>
    <asp:Label ID="Label3" runat="server" Text="医院名"></asp:Label>
    　　　　<asp:TextBox ID="T_医院名" runat="server"></asp:TextBox>
    <asp:Button ID="B_顧客呼出" runat="server" Text="顧客呼出" />
    <asp:Label ID="Label12" runat="server" ForeColor="Red" Text="※"></asp:Label>
    <asp:TextBox ID="T_顧客No" runat="server" Visible="False"></asp:TextBox>
</p>
<p>
    <asp:Label ID="Label4" runat="server" Text="ステータス"></asp:Label>
    　　　<asp:DropDownList ID="D_ステータス" runat="server">
        <asp:ListItem>A</asp:ListItem>
        <asp:ListItem>B</asp:ListItem>
        <asp:ListItem>C</asp:ListItem>
        <asp:ListItem>D</asp:ListItem>
    </asp:DropDownList>
</p>
    <p>
        <asp:Label ID="Label9" runat="server" Text="地域"></asp:Label>
        　　　　　<asp:TextBox ID="T_地域" runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="Label5" runat="server" Text="販売店"></asp:Label>
    　　　　<asp:TextBox ID="T_販売店" runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="Label6" runat="server" Text="商材"></asp:Label>
    　　　　　<asp:DropDownList ID="D_商材" runat="server">
        <asp:ListItem>U-BOX</asp:ListItem>
        <asp:ListItem>QURIA</asp:ListItem>
        <asp:ListItem>未定</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="※"></asp:Label>
</p>
<p>
    <asp:Label ID="Label7" runat="server" Text="金額"></asp:Label>
    　　　　　<asp:TextBox ID="T_金額" runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="Label8" runat="server" Text="備考"></asp:Label>
    　　　　　<asp:TextBox ID="T_備考" runat="server" TextMode="MultiLine" Height="91px" 
        Width="374px"></asp:TextBox>
    　　　　　</p>
    <p>
        <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="※"></asp:Label>
        <asp:Label ID="Label14" runat="server" Text="入力必須項目"></asp:Label>
    </p>
<p>
    <asp:Button ID="Button2" runat="server" Text="新規登録" />
    　<asp:Button ID="Button3" runat="server" Text="キャンセル" />
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

