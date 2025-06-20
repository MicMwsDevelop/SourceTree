<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Loan.aspx.vb" Inherits="Loan_Loan" title="無題のページ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  ローン案件抽出" Font-Italic="True" 
            Width="500px"></asp:Label>
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
    &nbsp;<asp:Label ID="L_エラー" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <br />
    </p>
    <asp:Label ID="T_検索結果" runat="server" Font-Bold="True"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" ForeColor="Black" GridLines="Vertical" 
        AutoGenerateColumns="False" Font-Size="Small">
        <FooterStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="f受注番号" HeaderText="伝票No" SortExpression="f受注番号" />
            <asp:BoundField DataField="f販売先" HeaderText="販売先" SortExpression="f販売先" />
            <asp:BoundField DataField="fユーザー" HeaderText="ユーザー" SortExpression="fユーザー" />
            <asp:BoundField DataField="f件名" HeaderText="件名" SortExpression="f件名" />
            <asp:BoundField DataField="f受注日" DataFormatString="{0:yyyy/MM/dd}" 
                HeaderText="受注日" SortExpression="f受注日" />
            <asp:BoundField DataField="f担当者名" HeaderText="担当者" SortExpression="f担当者名" />
            <asp:BoundField DataField="f担当支店名" HeaderText="担当支店" SortExpression="f担当支店名" />
            <asp:BoundField DataField="f売上承認日" DataFormatString="{0:yyyy/MM/dd}" 
                HeaderText="売上承認日" SortExpression="f売上承認日" />
            <asp:TemplateField HeaderText="備考" SortExpression="f備考">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("f備考") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("f備考") %>'></asp:Label>
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
        SelectCommand="SELECT [f受注番号], [f販売先], [fユーザー], [f件名], [f受注日], [f担当者名], [f担当支店名], [f売上承認日], [f備考] FROM [JunpMih受注ヘッダ] WHERE (([f備考] LIKE '%' + @f備考 + '%') AND ([f担当支店名] LIKE '%' + @f担当支店名 + '%'))">
        <SelectParameters>
            <asp:Parameter DefaultValue="ローン契約" Name="f備考" Type="String" />
            <asp:ControlParameter ControlID="D_拠点" Name="f担当支店名" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
</asp:Content>

