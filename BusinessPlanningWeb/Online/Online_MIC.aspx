<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Online_MIC.aspx.vb" Inherits="Online_Onlinet" title="電子レセプト対応進捗状況" %>

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
            Font-Size="XX-Large" ForeColor="White" Text=" 電子レセプト対応進捗状況_ミック作業" Font-Italic="True" 
            Width="608px"></asp:Label>
    </p>
     <span class="style1">【抽出条件】
     </span>
     <br class="style1"/>
     <span class="style1">■エントリーチェックがされていること<br/>
     </span>
     <span class="style1">■[作業区分]が"ミック"
     </span>
     <br class="style1"/>
     <span class="style1">■[作業完了フラグ]が"□(未終了)"→　終了チェックなし または　■伝票未起票
     </span>
     <br/><br/><br/>

    <p>
        <asp:Label ID="Label4" runat="server" Text="担当支店："></asp:Label>
        <asp:DropDownList ID="D_拠点" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem Value="札幌支店">札幌支店</asp:ListItem>
            <asp:ListItem Value="仙台支店">仙台支店</asp:ListItem>
            <asp:ListItem Value="関東第一支店">関東第一支店</asp:ListItem>
            <asp:ListItem Value="関東第二支店">関東第二支店</asp:ListItem>
            <asp:ListItem Value="関東第三支店">関東第三支店</asp:ListItem>
            <asp:ListItem Value="関東第四支店">関東第四支店</asp:ListItem>
            <asp:ListItem Value="関東第五支店">関東第五支店</asp:ListItem>
            <asp:ListItem Value="名古屋支店">名古屋支店</asp:ListItem>
            <asp:ListItem Value="関西第一支店">関西第一支店</asp:ListItem>
            <asp:ListItem Value="関西第二支店">関西第二支店</asp:ListItem>
            <asp:ListItem Value="関西第三支店">関西第三支店</asp:ListItem>
            <asp:ListItem Value="金沢営業所">金沢営業所</asp:ListItem>
            <asp:ListItem Value="広島支店">広島支店</asp:ListItem>
	　　    <asp:ListItem Value="福岡支店">福岡支店</asp:ListItem>
	　　</asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" Text="伝票起票の有無"></asp:Label>
        <asp:DropDownList ID="D_エントリー" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem Value="○">起票済</asp:ListItem>
            <asp:ListItem Value="×">未起票</asp:ListItem>
        </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label12" runat="server" Text="作業担当"></asp:Label>
        <asp:DropDownList ID="D_作業担当" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem Value="○">決定</asp:ListItem>
            <asp:ListItem Value="×">未決定</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <br/>
        
        <asp:Label ID="Label11" runat="server" Text="顧客No検索:"></asp:Label>
        <asp:TextBox ID="T_UserNo" runat="server" AutoPostBack="True" Width="84px">%</asp:TextBox>
        　　<asp:Label ID="Label13" runat="server" Text="作業担当者検索:"></asp:Label>
        <asp:TextBox ID="T_作業担当" runat="server" AutoPostBack="True" Width="84px">%</asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label14" runat="server" 
            Text="営業担当者検索:"></asp:Label>
        <asp:TextBox ID="T_営業担当" runat="server" AutoPostBack="True" Width="84px">%</asp:TextBox>
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
        GridLines="Vertical" AllowPaging="True" PageSize="50">
        <PagerSettings Position="TopAndBottom" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:BoundField DataField="支店名" HeaderText="支店名" SortExpression="支店名" />
            <asp:BoundField DataField="顧客NO" HeaderText="顧客No" SortExpression="顧客NO" />
            <asp:BoundField DataField="顧客名１" HeaderText="顧客名" SortExpression="顧客名１" />
            <asp:BoundField DataField="都道府県名" HeaderText="都道府県名" SortExpression="都道府県名" />
            <asp:BoundField DataField="伝票No" HeaderText="伝票No" SortExpression="伝票No" />
            <asp:BoundField DataField="作業担当者" HeaderText="作業担当者" SortExpression="作業担当者" />
            <asp:BoundField DataField="営業担当" HeaderText="営業担当者" SortExpression="営業担当" />
            <asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考" >
            <ItemStyle Width="220px" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
        
        
        
        
        
        SelectCommand="SELECT [支店名], [顧客NO], [顧客名１], [都道府県名], [伝票No], [営業担当], [作業担当者], [備考] FROM [Online_View] WHERE (([支店名] LIKE '%' + @支店名 + '%') AND ([作業区分] LIKE '%' + @作業区分 + '%') AND ([登録] LIKE '%' + @登録 + '%') AND ([顧客NO] LIKE '%' + @顧客NO + '%') AND ([担当] LIKE '%' + @担当 + '%') AND ([作業担当] LIKE '%' + @作業担当 + '%') AND ([営業担当者] LIKE '%' + @営業担当者 + '%')) ORDER BY [受付日]">
        <SelectParameters>
            <asp:ControlParameter ControlID="D_拠点" Name="支店名" PropertyName="SelectedValue" 
                Type="String" />
            <asp:Parameter DefaultValue="ミック" Name="作業区分" Type="String" />
            <asp:ControlParameter ControlID="D_エントリー" Name="登録" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="T_UserNo" Name="顧客NO" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="D_作業担当" Name="担当" PropertyName="SelectedValue" 
                Type="String" />
            <asp:ControlParameter ControlID="T_作業担当" Name="作業担当" PropertyName="Text" 
                Type="String" />
           <asp:ControlParameter ControlID="T_営業担当" Name="営業担当者" PropertyName="Text" 
                Type="String" />

        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>

