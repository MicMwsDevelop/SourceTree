<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="UPG.aspx.vb" Inherits="UPG_UPG" title="QURIA306F→309F アップグレード申し込み状況" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="Large" ForeColor="White" Text="QURIA306F→309F アップグレード申し込み状況"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="担当支店: "></asp:Label>
        <asp:DropDownList ID="D_担当支店" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem>札幌支店</asp:ListItem>
            <asp:ListItem>仙台支店</asp:ListItem>
            <asp:ListItem>首都圏営業部</asp:ListItem>
            <asp:ListItem>名古屋支店</asp:ListItem>
            <asp:ListItem>大阪支店</asp:ListItem>
            <asp:ListItem>金沢営業所</asp:ListItem>
            <asp:ListItem>広島営業所</asp:ListItem>
            <asp:ListItem>福岡支店</asp:ListItem>
        </asp:DropDownList>
    &nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="出荷状況："></asp:Label>
        <asp:DropDownList ID="D_出荷状況" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem Value="6月出荷">6月15日出荷</asp:ListItem>
            <asp:ListItem Value="7月出荷">7月13日出荷</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource1" Font-Size="Small" 
            ForeColor="Black" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="担当支店" HeaderText="担当支店" SortExpression="担当支店" />
                <asp:BoundField DataField="顧客No" HeaderText="顧客No" SortExpression="顧客No" />
                <asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" />
                <asp:BoundField DataField="申込受理日" DataFormatString="{0:yyyy/MM/dd}" 
                    HeaderText="申込受理日" SortExpression="申込受理日" />
                <asp:CheckBoxField DataField="OCR注文有" HeaderText="OCR注文有" 
                    SortExpression="OCR注文有" />
                <asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考" />
                <asp:BoundField DataField="拠点連絡日" HeaderText="拠点連絡日" SortExpression="拠点連絡日" />
                <asp:BoundField DataField="旧販売店" HeaderText="旧販売店" SortExpression="旧販売店" />
                <asp:BoundField DataField="起票済" HeaderText="起票済" SortExpression="起票済">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="受注承認日" DataFormatString="{0:yyyy/MM/dd}" 
                    HeaderText="受注承認日" SortExpression="受注承認日" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            
            
            
            SelectCommand="SELECT [担当支店], [顧客No], [顧客名], [申込受理日], [OCR注文有], [備考], [拠点連絡日], [旧販売店], [起票済], [受注承認日] FROM [UPG_View] WHERE (([担当支店] LIKE '%' + @担当支店 + '%') AND ([出荷] LIKE '%' + @出荷 + '%')) ORDER BY [担当支店], [申込受理日]">
            <SelectParameters>
                <asp:ControlParameter ControlID="D_担当支店" Name="担当支店" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="D_出荷状況" Name="出荷" PropertyName="SelectedValue" 
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    <p>
    </p>
</asp:Content>

