<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Campaign.aspx.vb" Inherits="Campaign_Campaign" title="キャンペーン伝票抽出" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  キャンペーン伝票抽出" Font-Italic="True" 
            Width="500px"></asp:Label>
    <br />

    &nbsp;<asp:DropDownList ID="D_キャンペーンコード" runat="server">
        <asp:ListItem Value="%">指定なし</asp:ListItem>
            <asp:ListItem Value="09170">CN:09170</asp:ListItem>
            <asp:ListItem Value="09117">CN:09117</asp:ListItem>
            <asp:ListItem Value="09111">CN:09111</asp:ListItem>
            <asp:ListItem Value="09110">CN:09110</asp:ListItem>
            <asp:ListItem Value="09103">CN:09103</asp:ListItem>
            <asp:ListItem Value="09094">CN:09094</asp:ListItem>
            <asp:ListItem Value="09093">CN:09093</asp:ListItem>
            <asp:ListItem Value="09063">CN:09063</asp:ListItem>
            <asp:ListItem Value="09058">CN:09058</asp:ListItem>
            <asp:ListItem Value="09048">CN:09048</asp:ListItem>
            <asp:ListItem Value="09042">CN:09042</asp:ListItem>
            <asp:ListItem Value="09031">CN:09031</asp:ListItem>
            <asp:ListItem Value="09020">CN:09020</asp:ListItem>
            <asp:ListItem Value="09002">CN:09002</asp:ListItem>
        </asp:DropDownList>
&nbsp;又は
    <asp:TextBox ID="T_検索文字列" runat="server"></asp:TextBox>
    <asp:Button ID="Button5" runat="server" Text="抽出" />
    <asp:Button ID="クリア" runat="server" Text="クリア" />
    &nbsp;<asp:Label ID="L_エラー" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <asp:Label ID="T_検索結果" runat="server" Font-Bold="True"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" ForeColor="Black" GridLines="Vertical" 
        AutoGenerateColumns="False" Font-Size="Small">
        <FooterStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="受注No" HeaderText="受注No" />
            <asp:BoundField DataField="販売先" HeaderText="販売先" />
            <asp:BoundField DataField="ユーザー名" HeaderText="ユーザー名" />
            <asp:BoundField DataField="件名" HeaderText="件名" />
            <asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
            <asp:BoundField DataField="受注日" DataFormatString="{0:yyyy/MM/dd}" 
                HeaderText="受注日" />
            <asp:BoundField DataField="売上承認日" DataFormatString="{0:yyyy/MM/dd}" 
                HeaderText="売上承認日" />
            <asp:BoundField DataField="担当" HeaderText="担当" />
            <asp:BoundField DataField="担当支店" HeaderText="担当支店" />
            <asp:TemplateField HeaderText="備考">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("備考") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("備考") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <br />
    <br />
</asp:Content>

