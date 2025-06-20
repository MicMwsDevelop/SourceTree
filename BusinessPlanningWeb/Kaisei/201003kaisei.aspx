<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="201003kaisei.aspx.vb" Inherits="Hosyu_Hosyu_Info" title="保守未リプレース先ユーザー" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    
    <style type="text/css">
        .style1
        {
            color: #FF3300;
        }
    </style>
    
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" 
            ForeColor="#3333FF"></asp:Label>
    </p>
    <p>
    <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="2010/03改正対応" Font-Italic="True" 
            Width="500px"></asp:Label>
    </p>
    
    <span class="style1">【フィールド説明】
     </span>
    <br class="style1"/>
     <span class="style1">■保守契約書返送{ check有=契約書受理} 
     </span>
     <br class="style1"/>
     <span class="style1">■代引発送{ check有=保守には加入せず、代引にて改正ディスク発送} </span>
     <br class="style1"/>
     <span class="style1">■口振発送{ check有=保守には加入せず、自動引落にて改正ディスク発送} </span>
     <br class="style1"/>
     <span class="style1">■入金発送{ check有=保守には加入せず、事前入金にて改正ディスク発送} </span>
     <br class="style1"/>
     <span class="style1">■口振申込{ check有=口振もあわせて加入したいと要望あり} </span>
     <br class="style1"/>
     <span class="style1">■終了        { 0:未終了    1:終了ﾕｰｻﾞｰ} </span>
     <br class="style1"/>
     <span class="style1">■伝票        {保守伝票の起票を確認した場合には表示されます} </span>
     <br class="style1"/>
    <br class="style1"/>
    
    
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
        <asp:Label ID="Label11" runat="server" Text="得意先No検索:"></asp:Label>
        <asp:TextBox ID="T_UserNo" runat="server" Width="84px">%</asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="検索" />
        &nbsp;<asp:Button ID="Button2" runat="server" Text="クリア" />
        <br />
    </p>
    <p>
        <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            DataSourceID="SqlDataSource1" GridLines="Vertical" Font-Size="Small" 
            AutoGenerateColumns="False" AllowPaging="True" PageSize="100">
            <PagerSettings Position="TopAndBottom" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="得意先No" HeaderText="得意先No" SortExpression="得意先No" />
                <asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" />
                <asp:BoundField DataField="書類返送日" DataFormatString="{0:yyyy/MM/dd}" 
                    HeaderText="書類返送日" SortExpression="書類返送日" />
                <asp:CheckBoxField DataField="保守契約書返送" HeaderText="保守契約書返送" 
                    SortExpression="保守契約書返送" />
                <asp:CheckBoxField DataField="代引発送" HeaderText="代引発送" SortExpression="代引発送" />
                <asp:CheckBoxField DataField="口振発送" HeaderText="口振発送" SortExpression="口振発送" />
                <asp:CheckBoxField DataField="入金発送" HeaderText="入金発送" SortExpression="入金発送" />
                <asp:CheckBoxField DataField="口振申込" HeaderText="口振申込" SortExpression="口振申込" />
                <asp:BoundField DataField="終了" HeaderText="終了" SortExpression="終了" />
                <asp:BoundField DataField="支店" HeaderText="支店" SortExpression="支店" />
                <asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考" />
                <asp:BoundField DataField="改正備考" HeaderText="改正備考" SortExpression="改正備考" />
                <asp:BoundField DataField="伝票" HeaderText="伝票" SortExpression="伝票" />
                <asp:BoundField DataField="保守開始" HeaderText="保守開始" SortExpression="保守開始" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            
            SelectCommand="SELECT [得意先No], [顧客名], [書類返送日], [口振申込], [保守契約書返送], [代引発送], [口振発送], [入金発送], [終了], [支店], [備考], [改正備考], [伝票], [保守開始] FROM [Kaisei201003_View] WHERE (([支店] LIKE '%' + @支店 + '%') AND ([得意先No] LIKE '%' + @得意先No + '%'))">
            <SelectParameters>
                <asp:ControlParameter ControlID="D_拠点" Name="支店" PropertyName="SelectedValue" 
                    Type="String" />
                <asp:ControlParameter ControlID="T_UserNo" Name="得意先No" PropertyName="Text" 
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
</asp:Content>

