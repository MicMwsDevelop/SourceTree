<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Distributor.aspx.vb" Inherits="Distributor_Distributor" title="販売店実績" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  販売店実績" Font-Italic="True" 
            Width="500px"></asp:Label>
        <br />
        <asp:TextBox ID="T_開始" runat="server" Width="87px" ToolTip="YYYY/MM/DD"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="～"></asp:Label>
        <asp:TextBox ID="T_終了" runat="server" Width="87px" ToolTip="YYYY/MM/DD"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="更新" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="T_開始" ErrorMessage="検索開始年月の入力方法に誤りがあります。(YYYY/MM/DD)" 
            ValidationExpression="\d{4}/\d{2}/\d{2}">※</asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="T_終了" ErrorMessage="検索終了年月の入力方法に誤りがあります。(YYYY/MM/DD)" 
            ValidationExpression="\d{4}/\d{2}/\d{2}">※</asp:RegularExpressionValidator>
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="■代理店/特約店販売実績"></asp:Label>
        <br /> ※表示条件：特約店または代理店
        <br /> ・売上承認ベース
        <br /> ・売価条件:値引
        <br /> ・対象商品:ソフト
        
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="顧客NoまたはグループNo" HeaderText="顧客NoまたはグループNo" />
                <asp:BoundField DataField="販売店名またはグループ名" HeaderText="販売店名またはグループ名" />
                <asp:BoundField DataField="数量の合計" HeaderText="数量の合計" />
                <asp:BoundField DataField="提供価格の合計" DataFormatString="{0:c}" 
                    HeaderText="提供価格の合計" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="Gainsboro" />
        </asp:GridView>
    </p>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <p>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="■販売促進店実績"></asp:Label>
            <br /> ※表示条件：販売促進店
            <br /> ・売上承認ベース
            <br /> ・販売促進店への販売手数料を含む伝票
            <br /> ・対象商品:保守契約料またはリース解約金以外の商品

    </p>
    <p>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource2" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="顧客NoまたはグループNo" HeaderText="顧客NoまたはグループNo" />
                <asp:BoundField DataField="販売店名またはグループ名" HeaderText="販売店名またはグループ名" />
                <asp:BoundField DataField="提供価格の合計" DataFormatString="{0:c}" 
                    HeaderText="提供価格の合計" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    </p>
</asp:Content>

