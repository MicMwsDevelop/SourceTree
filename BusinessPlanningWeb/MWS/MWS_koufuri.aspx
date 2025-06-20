<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="MWS_koufuri.aspx.vb" Inherits="BusinessPerformance_BusinessPerformance" title="営業実績(簡易版)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            color: #0033CC;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  ＭＷＳ 口座振替未加入ユーザーリスト" Font-Italic="True" 
            Width="561px"></asp:Label>
    <br />
    
<P>
    <span class="style1">&nbsp;&nbsp;
     
     種別について　　ＶＰ：バリューパック　　　ＵＧ：アップグレード　　　月額：月額課金</span><P>
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
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label11" runat="server" Text="種別:"></asp:Label>
        <asp:DropDownList ID="D_種別" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem Value="1">VP</asp:ListItem>
            <asp:ListItem Value="2">UG</asp:ListItem>
            <asp:ListItem Value="3">月額</asp:ListItem>
        </asp:DropDownList>
        　　<asp:Button ID="Button2" runat="server" Text="クリア" />
        <P>
    <br />
    <asp:Label ID="T_検索結果" runat="server" Font-Bold="True"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        AutoGenerateColumns="False">
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="支店名" HeaderText="支店名" SortExpression="支店名" />
                <asp:BoundField DataField="システム名称" HeaderText="システム名称" 
                    SortExpression="システム名称" />
                <asp:BoundField DataField="顧客No" HeaderText="顧客No" SortExpression="顧客No" />
                <asp:BoundField DataField="得意先No" HeaderText="得意先No" SortExpression="得意先No" />
                <asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" />
                <asp:BoundField DataField="納品月" HeaderText="納品月" SortExpression="納品月" />
                <asp:BoundField DataField="種別" HeaderText="種別" 
                    SortExpression="種別" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:JunpDBConnectionString %>" 
        SelectCommand="SELECT [支店名], [システム名称], [顧客No], [得意先No], [顧客名１] + [顧客名２] AS '顧客名', [納品月], case when [MWS_申込種別] = '1' then 'VP' when [MWS_申込種別] = '2' then 'UG' when [MWS_申込種別] = '3' then '月額' else 'エラー' end as 種別 FROM [vMic全ユーザー2] WHERE ((([代行回収状態] <> '継続') OR ([代行回収状態] IS NULL)) AND ([終了フラグ] = @終了フラグ) AND ([請求回収日] <> '027') AND ([支店名] LIKE '%' + @支店名 + '%') AND ([MWS_申込種別] LIKE '%' + @種別 + '%') AND ([システム名称] LIKE '%' + @システム名称 + '%')) ORDER BY [支店名]">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="終了フラグ" Type="String" />
            <asp:Parameter DefaultValue="palette" Name="システム名称" Type="String" />
            <asp:ControlParameter ControlID="D_拠点" Name="支店名" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="D_種別" Name="種別" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <p>
        &nbsp;<p>
        &nbsp;</asp:Content>

