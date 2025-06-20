<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="YosanMem.aspx.vb" Inherits="YosanMem" title="予算管理管理者ページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="予算管理管理者ページ"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" DataKeyNames="ｆUsrID" DataSourceID="SqlDataSource1" 
        ForeColor="Black" GridLines="Vertical">
        <FooterStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
            <asp:BoundField DataField="ｆUsrID" HeaderText="ｆUsrID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ｆUsrID" Visible="False" />
            <asp:BoundField DataField="fUsrBushoCode" HeaderText="fUsrBushoCode" 
                SortExpression="fUsrBushoCode" Visible="False" />
            <asp:BoundField DataField="fUsrBusho" HeaderText="部署名" 
                SortExpression="fUsrBusho" />
            <asp:BoundField DataField="fUsrArea" HeaderText="fUsrArea" 
                SortExpression="fUsrArea" Visible="False" />
            <asp:BoundField DataField="fUsrNo" HeaderText="fUsrNo" SortExpression="fUsrNo" 
                Visible="False" />
            <asp:BoundField DataField="fUsrName" HeaderText="氏名" 
                SortExpression="fUsrName" />
            <asp:CheckBoxField DataField="fUsrTop" HeaderText="fUsrTop" 
                SortExpression="fUsrTop" Visible="False" />
            <asp:CheckBoxField DataField="fUsrLockFlg" HeaderText="fUsrLockFlg" 
                SortExpression="fUsrLockFlg" Visible="False" />
            <asp:TemplateField HeaderText="編集可否">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" 
                        Checked='<%# Bind("fUsrLockFlg") %>' Enabled="False" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
        SelectCommand="SELECT * FROM [Yosan_tUser]"></asp:SqlDataSource>
    <br />
    <br />
    </asp:Content>

