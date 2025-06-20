<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Online_Start.aspx.vb" Inherits="Online_Onlinet" title="電子レセプト対応進捗状況" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            color: #FF3300;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" Text="Label" 
            ForeColor="#3333FF"></asp:Label>
        </p>
    <p>
        <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text=" 電子レセプト_情報不備" Font-Italic="True" 
            Width="500px"></asp:Label>
    </p>
     <span class="style1">【抽出条件】
     
     </span>
     
     <br class="style1"/>
     <span class="style1">■[作業完了フラグ]が"終了済"→　終了チェックあり </span>
     <br class="style1"/>
     <span class="style1">■[オンライン請求開始]が空欄
     </span>
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
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label11" runat="server" Text="顧客No検索:"></asp:Label>
        <asp:TextBox ID="T_UserNo" runat="server" AutoPostBack="True" Width="84px">%</asp:TextBox>
        　　<asp:Button ID="Button1" runat="server" Text="検索" />
        <asp:Button ID="Button2" runat="server" Text="クリア" />
        <br />
    </p>
    <p>
        <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
    </p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" DataSourceID="SqlDataSource1" Font-Size="Small" 
        GridLines="Vertical" PageSize="50">
        <PagerSettings Position="TopAndBottom" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:BoundField DataField="支店名" HeaderText="支店名" SortExpression="支店名" />
            <asp:BoundField DataField="顧客NO" HeaderText="顧客NO" SortExpression="顧客NO" 
                ReadOnly="True" />
            <asp:BoundField DataField="顧客名１" HeaderText="顧客名" SortExpression="顧客名１" />
            <asp:BoundField DataField="都道府県名" HeaderText="都道府県名" SortExpression="都道府県名" />
            <asp:BoundField DataField="レセ電算確認試験" HeaderText="レセ電算確認試験" 
                SortExpression="レセ電算確認試験" >
            <ItemStyle Width="65px" />
            </asp:BoundField>
            <asp:BoundField DataField="オンライン確認試験" HeaderText="オンライン確認試験" 
                SortExpression="オンライン確認試験" >
            <ItemStyle Width="65px" />
            </asp:BoundField>
            <asp:BoundField DataField="レセ電算請求開始" HeaderText="レセ電算請求開始" 
                SortExpression="レセ電算請求開始" >
            <ItemStyle Width="65px" />
            </asp:BoundField>
            <asp:BoundField DataField="オンライン請求開始" HeaderText="オンライン請求開始" 
                SortExpression="オンライン請求開始" >
            <ItemStyle Width="65px" />
            </asp:BoundField>
            <asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考" >
            <ItemStyle Width="200px" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
        
        
        
        
        
        
        
        
        SelectCommand="SELECT [支店名], [顧客NO], [顧客名１], [都道府県名], [レセ電算確認試験], [オンライン確認試験], [レセ電算請求開始], [オンライン請求開始], [備考] FROM [Online_OnlineStart_View] WHERE (([支店名] LIKE '%' + @支店名 + '%') AND ([顧客NO] LIKE '%' + @顧客NO + '%'))">
        <SelectParameters>
            <asp:ControlParameter ControlID="D_拠点" Name="支店名" PropertyName="SelectedValue" 
                Type="String" />
            <asp:ControlParameter ControlID="T_UserNo" Name="顧客NO" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>

