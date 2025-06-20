<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="BusinessPerformance.aspx.vb" Inherits="BusinessPerformance_BusinessPerformance" title="営業実績(簡易版)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  営業実績(簡易版)" Font-Italic="True" 
            Width="500px"></asp:Label>
    <br />
        <asp:TextBox ID="T_開始" runat="server" Width="87px" ToolTip="YYYY/MM/DD" 
        Visible="False">2010/08/01</asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="～" Visible="False"></asp:Label>
        <asp:TextBox ID="T_終了" runat="server" Width="87px" ToolTip="YYYY/MM/DD" 
        Visible="False">2011/07/31</asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="T_開始" ErrorMessage="検索開始年月の入力方法に誤りがあります。(YYYY/MM/DD)" 
            ValidationExpression="\d{4}/\d{2}/\d{2}">※</asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="T_終了" ErrorMessage="検索終了年月の入力方法に誤りがあります。(YYYY/MM/DD)" 
            ValidationExpression="\d{4}/\d{2}/\d{2}">※</asp:RegularExpressionValidator>
    <br />
    <asp:DropDownList ID="D_kikan" runat="server">
        <asp:ListItem Value="36期通期">36期:通期(2010/08/01 ～ 2011/07/31)</asp:ListItem>
        <asp:ListItem Value="36期上半期">36期:上半期(2010/08/01 ～ 2011/01/31)</asp:ListItem>
        <asp:ListItem Value="36期下半期">36期:下半期(2011/02/01 ～ 2011/07/31)</asp:ListItem>
        <asp:ListItem Value="35期通期">35期:通期(2009/08/01 ～ 2010/07/31)</asp:ListItem>
        <asp:ListItem Value="35期上半期">35期:上半期(2009/08/01 ～ 2010/01/31)</asp:ListItem>
        <asp:ListItem Value="35期下半期">35期:下半期(2010/02/01 ～ 2010/07/31)</asp:ListItem>
    </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="更新" />
    　　<asp:Button ID="B_エクスポート" runat="server" Text="EXCEL出力" Visible="False" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" DataSourceID="SqlDataSource1" GridLines="Vertical">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:BoundField DataField="現在支店名" HeaderText="支店" />
            <asp:BoundField DataField="担当者名" HeaderText="担当者" />
            <asp:BoundField DataField="実績金額" DataFormatString="{0:c}" HeaderText="実績" />
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <br />
    <br />
    <br />
    <br />
</asp:Content>

