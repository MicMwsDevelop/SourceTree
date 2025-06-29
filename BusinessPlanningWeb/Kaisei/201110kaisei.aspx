﻿<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="201110kaisei.aspx.vb" Inherits="Hosyu_Hosyu_Info" title="保守未リプレース先ユーザー" %>

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
            Font-Size="XX-Large" ForeColor="White" Text="2011/10金パラ対応" Font-Italic="True" 
            Width="500px"></asp:Label>
    </p>
    
    <span class="style1">【フィールド説明】
     </span>
    <br class="style1"/>
     <span class="style1">■保守加入{ check有=保守加入希望} 
     </span>
     <br class="style1"/>
     <span class="style1">■代引発送{ check有=保守には加入せず、代引にて改正ディスク発送} </span>
     <br class="style1"/>
     <span class="style1">■口振発送{ check有=保守には加入せず、自動引落にて改正ディスク発送} </span>
     <br class="style1"/>
     <span class="style1">■後請求発送{ check有=保守には加入せず、請求書支払いにて改正ディスク発送} </span>
     <br class="style1"/>
     <span class="style1">■口振申込{ check有=口振もあわせて加入したいと要望あり} </span>
     <br class="style1"/>
     <span class="style1">■終了        { 0:未終了    1:終了ﾕｰｻﾞｰ} </span>
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
                <asp:BoundField DataField="支店" HeaderText="支店" SortExpression="支店" />
                <asp:BoundField DataField="得意先No" HeaderText="得意先No" SortExpression="得意先No" />
                <asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" >
                <ItemStyle Width="180px" />
                </asp:BoundField>
                <asp:BoundField DataField="ＦＡＸ返信日" 
                    HeaderText="ＦＡＸ返信日" SortExpression="ＦＡＸ返信日" 
                    DataFormatString="{0:yyyy/MM/dd}" />
                <asp:CheckBoxField DataField="保守加入" HeaderText="保守加入" SortExpression="保守加入" />
                <asp:CheckBoxField DataField="口振発送" HeaderText="口振発送" SortExpression="口振発送" />
                <asp:TemplateField HeaderText="代引発送" SortExpression="代引発送" ItemStyle-ForeColor="Blue">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("代引発送") %>' 
                            Enabled="false" AutoCheck="false" ForeColor="Black"/>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="True" />
                </asp:TemplateField>
                <asp:CheckBoxField DataField="後請求発送" HeaderText="後請求発送" 
                    SortExpression="後請求発送" />
                <asp:CheckBoxField DataField="口振申込" HeaderText="口振申込" SortExpression="口振申込" />
                <asp:BoundField DataField="終了" HeaderText="終了" SortExpression="終了" />
                <asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考" >
                <ItemStyle Width="150px" />
                </asp:BoundField>
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            
            
            
            SelectCommand="SELECT * FROM [Kaisei201110_View] WHERE (([支店] LIKE '%' + @支店 + '%') AND ([得意先No] LIKE '%' + @得意先No + '%')) ORDER BY [支店]">
            <SelectParameters>
                <asp:ControlParameter ControlID="D_拠点" Name="支店" PropertyName="SelectedValue" 
                    Type="String" />
                <asp:ControlParameter ControlID="T_UserNo" Name="得意先No" PropertyName="Text" 
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
</asp:Content>

