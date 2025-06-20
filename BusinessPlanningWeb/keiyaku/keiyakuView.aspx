<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="keiyakuView.aspx.vb" Inherits="keiyaku_keiyakuView" title="助成申請書管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" Text="Label" 
            ForeColor="#3333FF"></asp:Label>
        </p>
    <p>
        <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text=" 助成申請書管理" Font-Italic="True" 
            Width="500px"></asp:Label>
    </p>
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
        <asp:Label ID="Label5" runat="server" Text="発送済/未発送："></asp:Label>
        <asp:DropDownList ID="D_前システム" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem Value="○">発送済み</asp:ListItem>
            <asp:ListItem Value="×">未発送</asp:ListItem>
        </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label11" runat="server" Text="顧客No検索:"></asp:Label>
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
        GridLines="Vertical">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:BoundField DataField="伝票No" HeaderText="伝票No" ReadOnly="True" 
                SortExpression="伝票No" />
            <asp:BoundField DataField="顧客No" HeaderText="顧客No" SortExpression="顧客No" />
            <asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" />
            <asp:BoundField DataField="担当部署名" HeaderText="担当部署名" SortExpression="担当部署名" />
            <asp:BoundField DataField="担当者名" HeaderText="担当者名" SortExpression="担当者名" />
            <asp:BoundField DataField="登録日" HeaderText="登録日" SortExpression="登録日" />
            <asp:BoundField DataField="契約書捺印完了日" HeaderText="契約書捺印完了日" 
                SortExpression="契約書捺印完了日" />
            <asp:BoundField DataField="納品書作成完了日" HeaderText="納品書作成完了日" 
                SortExpression="納品書作成完了日" />
            <asp:BoundField DataField="領収書作成完了日" HeaderText="領収書作成完了日" 
                SortExpression="領収書作成完了日" />
            <asp:TemplateField HeaderText="備考" SortExpression="備考">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("備考") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("備考") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ユーザー発送日" HeaderText="ユーザー発送日" 
                SortExpression="ユーザー発送日" />
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
        SelectCommand="SELECT [伝票No], [顧客No], [顧客名], [担当部署名], [担当者名], [登録日], [契約書捺印完了日], [納品書作成完了日], [領収書作成完了日], [備考], [ユーザー発送日], [発送] FROM [Keiyaku_View] WHERE (([担当部署名] LIKE '%' + @担当部署名 + '%') AND ([発送] LIKE '%' + @発送 + '%') AND ([顧客No] LIKE '%' + @顧客No + '%')) ORDER BY [No]">
        <SelectParameters>
            <asp:ControlParameter ControlID="D_拠点" Name="担当部署名" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="D_前システム" Name="発送" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="T_UserNo" Name="顧客No" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>

