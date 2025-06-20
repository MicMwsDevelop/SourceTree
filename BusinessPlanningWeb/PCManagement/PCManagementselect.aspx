<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="PCManagementselect.aspx.vb" Inherits="schedule_Scheduleselect" title="案件新規登録" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="案件新規登録" 
        Font-Size="Large"></asp:Label>
    <br />
    <br />
    <br />
    <asp:Label ID="Label9" runat="server" Text="種別"></asp:Label>
    :<br />
    <asp:DropDownList ID="D_種別" runat="server">
        <asp:ListItem>PC</asp:ListItem>
        <asp:ListItem>モニタ</asp:ListItem>
        <asp:ListItem>プリンタ</asp:ListItem>
        <asp:ListItem>アクセスポイント</asp:ListItem>
        <asp:ListItem>スキャナ</asp:ListItem>
        <asp:ListItem>モバイルノートPC</asp:ListItem>
        <asp:ListItem>皮バインダー</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    名称:<br />
    <asp:TextBox ID="T_名称" runat="server" BackColor="White" Width="172px"></asp:TextBox>
    <br />
    <br />
    Code:<br />
    <asp:TextBox ID="T_Code" runat="server" BackColor="White" Width="172px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label8" runat="server" Text="備考"></asp:Label>
    <br />
    <asp:TextBox ID="T_備考" runat="server" Height="102px" TextMode="MultiLine" 
        Width="311px"></asp:TextBox>
    <br />
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="True" 
        Font-Size="Large"></asp:Label>
    <br />
        <asp:Button ID="B_新規登録" runat="server" Text="新規登録" />
    &nbsp;<asp:Button ID="B_クリア" runat="server" Text="クリア" />
    &nbsp;<asp:Button ID="B_キャンセル" runat="server" Text="戻る" />
    <br />
    <br />
    <asp:DropDownList ID="D種別" runat="server" AutoPostBack="True">
        <asp:ListItem Value="%">すべて</asp:ListItem>
        <asp:ListItem>PC</asp:ListItem>
        <asp:ListItem>モニタ</asp:ListItem>
        <asp:ListItem>プリンタ</asp:ListItem>
        <asp:ListItem>アクセスポイント</asp:ListItem>
        <asp:ListItem>スキャナ</asp:ListItem>
        <asp:ListItem>モバイルノートPC</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" DataSourceID="SqlDataSource1" ForeColor="Black" 
        GridLines="Vertical">
        <FooterStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="種別" HeaderText="種別" SortExpression="種別" />
            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
            <asp:BoundField DataField="名称" HeaderText="名称" SortExpression="名称" />
            <asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考" />
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
        SelectCommand="SELECT [種別], [Code], [名称], [備考] FROM [PCManagement_master] WHERE ([種別] LIKE '%' + @種別 + '%') ORDER BY [更新日時]">
        <SelectParameters>
            <asp:ControlParameter ControlID="D種別" Name="種別" PropertyName="SelectedValue" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>

