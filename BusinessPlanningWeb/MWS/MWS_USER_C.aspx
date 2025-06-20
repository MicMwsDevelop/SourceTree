<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="MWS_USER_C.aspx.vb" Inherits="BusinessPerformance_BusinessPerformance" title="営業実績(簡易版)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            color: #0033CC;
        }
        .style2
        {
            font-weight: bold;
            color: #006600;
        }
        .style3
        {
            font-weight: bold;
            color: #6600CC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  ＭＷＳユーザー数" Font-Italic="True" 
            Width="500px"></asp:Label>
    <br />
    
<P>
    <span class="style1">&nbsp;&nbsp; MWS 総ユーザー数
     
     </span>
    <br />
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

    <p>
    <span class="style1">&nbsp;</span><span class="style3">&nbsp; MWS バリューバックユーザー数   </span>
        <asp:GridView ID="GridView2" runat="server" CellPadding="3" 
            DataSourceID="SqlDataSource2" GridLines="Horizontal" BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    <p>
    <span class="style1">&nbsp;&nbsp; MWS アップグレードユーザー数   </span>
        <asp:GridView ID="GridView3" runat="server" CellPadding="4" 
            DataSourceID="SqlDataSource3" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#EFF3FB" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server"></asp:SqlDataSource>
    <p>
    <span class="style1">&nbsp;&nbsp;</span><span class="style2"> MWS 月額課金ユーザー数   </span>
        <asp:GridView ID="GridView7" runat="server" CellPadding="4" 
            DataSourceID="SqlDataSource4" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#E3EAEB" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server"></asp:SqlDataSource>
    <p>
        &nbsp;<p>
        &nbsp;</asp:Content>

