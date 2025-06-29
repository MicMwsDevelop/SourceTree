﻿<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Online_CheckEnd.aspx.vb" Inherits="Online_Onlinet" title="電子レセプト対応進捗状況" %>

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
            Font-Size="XX-Large" ForeColor="White" Text=" 電子レセプト_設定作業未終了一覧" Font-Italic="True" 
            Width="500px"></asp:Label>
    </p>
     <span class="style1">【抽出条件】
     
     </span>

     <br class="style1"/>
     <span class="style1">■2009/11/26以降のシステム販売先ユーザー </span>  
     <br class="style1"/>
     <span class="style1">■[作業完了フラグ]が"未終了"→　終了チェックなし </span>
     </span>
     <br class="style1"/>

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
            <asp:BoundField DataField="支店名" HeaderText="支店名" SortExpression="支店名" >
            <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="売伝No" HeaderText="売伝No" SortExpression="売伝No" />
            <asp:BoundField DataField="顧客NO" HeaderText="顧客NO" SortExpression="顧客NO" />
            <asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" 
                ReadOnly="True" />
            <asp:BoundField DataField="都道府県名" HeaderText="都道府県名" 
                SortExpression="都道府県名" >
            </asp:BoundField>
            <asp:BoundField DataField="システム名称" HeaderText="システム名称" 
                SortExpression="システム名称" >
            </asp:BoundField>
            <asp:BoundField DataField="作業区分" HeaderText="作業区分" 
                SortExpression="作業区分" >
            </asp:BoundField>
            <asp:BoundField DataField="f担当者名" HeaderText="販売担当" SortExpression="f担当者名">
            <ItemStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField DataField="作業担当者" HeaderText="作業担当者" 
                SortExpression="作業担当者" >
            <ItemStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField DataField="受付担当者" HeaderText="受付担当者" SortExpression="受付担当者" >
            <ItemStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField DataField="納品月" HeaderText="納品月" SortExpression="納品月" />
            <asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考">
            <ItemStyle Width="170px" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
        
        
        
        
        
        
        
        
        
        SelectCommand="SELECT * FROM [Online_CheckEndUser_View] WHERE ([支店名] LIKE '%' + @支店名 + '%') ORDER BY [売伝No]">
        <SelectParameters>
            <asp:ControlParameter ControlID="D_拠点" Name="支店名" PropertyName="SelectedValue" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>

