<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="SpecialPrice.aspx.vb" Inherits="SpecialPrice_SpecialPrice" title="U-BOX2009謝恩特別価格販売" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
    <br />
    c<br />&nbsp;<asp:Label ID="Label4" runat="server" Text="担当支店:"></asp:Label>
    <asp:DropDownList ID="D_拠点" runat="server" AutoPostBack="True">
        <asp:ListItem Value="%">すべて</asp:ListItem>
        <asp:ListItem Value="札幌支店">札幌支店</asp:ListItem>
        <asp:ListItem Value="仙台支店">仙台支店</asp:ListItem>
        <asp:ListItem Value="首都圏営業部 営業1課">首都圏営業部 営業1課</asp:ListItem>
        <asp:ListItem Value="首都圏営業部 営業2課">首都圏営業部 営業2課</asp:ListItem>
        <asp:ListItem Value="首都圏営業部 営業3課">首都圏営業部 営業3課</asp:ListItem>
        <asp:ListItem Value="名古屋支店">名古屋支店</asp:ListItem>
        <asp:ListItem Value="大阪支店">大阪支店</asp:ListItem>
        <asp:ListItem Value="金沢営業所">金沢営業所</asp:ListItem>
        <asp:ListItem Value="広島営業所">広島営業所</asp:ListItem>
        <asp:ListItem>福岡支店</asp:ListItem>
    </asp:DropDownList>
    &nbsp;　<asp:Label ID="Label5" runat="server" Text="承認:"></asp:Label>
&nbsp;<asp:DropDownList ID="D_承認" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem Value="○">売上承認済</asp:ListItem>
            <asp:ListItem Value="×">売上未承認</asp:ListItem>
        </asp:DropDownList>
        　 
     
    <asp:Label ID="Label6" runat="server" Text="L/Q:"></asp:Label>
    <asp:DropDownList ID="D_LQ" runat="server" AutoPostBack="True">
        <asp:ListItem Value="%">すべて</asp:ListItem>
        <asp:ListItem>L特別</asp:ListItem>
        <asp:ListItem>Q特別</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="クリア" runat="server" Text="クリア" />
    &nbsp;<br />
    <br />
    <asp:Label ID="T_検索結果" runat="server" Font-Bold="True"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" ForeColor="Black" GridLines="Vertical" 
        AutoGenerateColumns="False" Font-Size="Small">
        <FooterStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="受注番号" HeaderText="受注番号" SortExpression="受注番号" />
            <asp:BoundField DataField="ユーザー" HeaderText="ユーザー" SortExpression="ユーザー" />
            <asp:BoundField DataField="担当者名" HeaderText="担当者名" SortExpression="担当者名" />
            <asp:BoundField DataField="伝票支店名" HeaderText="伝票支店名" SortExpression="伝票支店名" />
            <asp:BoundField DataField="受注日" DataFormatString="{0:yyyy/MM/dd}" 
                HeaderText="受注日" SortExpression="受注日" />
            <asp:BoundField DataField="受注承認日" DataFormatString="{0:yyyy/MM/dd}" 
                HeaderText="受注承認日" SortExpression="受注承認日" />
            <asp:BoundField DataField="売上承認日" DataFormatString="{0:yyyy/MM/dd}" 
                HeaderText="売上承認日" SortExpression="売上承認日" />
            <asp:TemplateField HeaderText="備考" SortExpression="備考">
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
        
        
        SelectCommand="SELECT [受注番号], [ユーザー], [担当者名], [伝票支店名], [受注日], [売上承認日], [備考], [受注承認日] FROM [vSpecialPrice] WHERE (([伝票支店名] LIKE '%' + @伝票支店名 + '%') AND ([承認] LIKE '%' + @承認 + '%') AND ([備考] LIKE '%' + @備考 + '%')) ORDER BY [表示順]">
        <SelectParameters>
            <asp:ControlParameter ControlID="D_拠点" Name="伝票支店名" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="D_承認" Name="承認" PropertyName="SelectedValue" 
                Type="String" />
            <asp:ControlParameter ControlID="D_LQ" Name="備考" PropertyName="SelectedValue" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
</asp:Content>

