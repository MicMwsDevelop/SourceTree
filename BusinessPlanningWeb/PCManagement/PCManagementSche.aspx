<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="PCManagementSche.aspx.vb" Inherits="schedule_Scheduleselect" title="案件新規登録" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="案件新規登録" 
        Font-Size="Large"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label9" runat="server" Text="案件名称："></asp:Label>
    <br />
    <asp:TextBox ID="T_案件名" runat="server" Width="400px"></asp:TextBox>
    <br />
    
    <br />
    <asp:Label ID="Label5" runat="server" Text="作業予定日"></asp:Label>
    <asp:Label ID="Label7" runat="server" ForeColor="Red" 
        Text="※ 入力形式はYYYY/MM/DD (カレンダーの日付をクリックしてください)"></asp:Label>
    <br />
    <asp:TextBox ID="T_作業予定日" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label10" runat="server" 
        Text="～"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="T_作業終了日" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;<br />
    <asp:CheckBox ID="C_Target" runat="server" Text="終了期間" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:RegularExpressionValidator 
        ID="RegularExpressionValidator1" runat="server" ControlToValidate="T_作業予定日" 
        ValidationExpression="\d{4}/\d{2}/\d{2}">※入力形式が違います</asp:RegularExpressionValidator>
    &nbsp;
    <asp:RegularExpressionValidator 
        ID="RegularExpressionValidator2" runat="server" ControlToValidate="T_作業終了日" 
        ValidationExpression="\d{4}/\d{2}/\d{2}">※入力形式が違います</asp:RegularExpressionValidator>
    　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
    <asp:Calendar ID="Calendar1" runat="server" Width="367px"></asp:Calendar>
    <br />
    使用ハード：<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" DataSourceID="SqlDataSource1" ForeColor="Black" 
        GridLines="Vertical">
        <FooterStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="種別" HeaderText="種別" />
            <asp:BoundField DataField="総数" HeaderText="総数" />
            <asp:TemplateField HeaderText="使用数">
                <ItemTemplate>
                    <asp:TextBox ID="T_使用数量" runat="server" Height="19px" Width="36px"></asp:TextBox>
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
        
        SelectCommand="SELECT [Code], [種別], [名称] FROM [PCManagement_master] ORDER BY [Code]">
    </asp:SqlDataSource>
    登録者：<asp:DropDownList ID="D_登録者" runat="server">
        <asp:ListItem>若杉 昇</asp:ListItem>
        <asp:ListItem>長谷川 敬之</asp:ListItem>
        <asp:ListItem>植松 秀一</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="Label8" runat="server" Text="用途："></asp:Label>
    <br />
    <asp:TextBox ID="T_備考" runat="server" Height="102px" TextMode="MultiLine" 
        Width="311px"></asp:TextBox>
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="True" 
        Font-Size="Large"></asp:Label>
    <br />
    <asp:Button ID="B_新規登録" runat="server" Text="新規登録" style="height: 21px" />
    &nbsp;<asp:Button ID="B_クリア" runat="server" Text="クリア" />
    &nbsp;<asp:Button ID="B_キャンセル" runat="server" Text="戻る" />
    <br />
    <br />
    <br />
</asp:Content>

