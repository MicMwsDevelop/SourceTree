<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="SoftwareMainteLimit.aspx.vb" Inherits="SoftwareMainteLimit_SoftwareMainteLimit" title="palette ES ソフトウェア保守料更新対象一覧" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
	<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
	<asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True"
		ForeColor="#3333FF">
	</asp:Label>
	<br />
	<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large"
		Text="palette ES ソフトウェア保守料 更新対象一覧">
	</asp:Label>
	&nbsp;&nbsp;&nbsp;&nbsp;
	</p>
	<p>
	<asp:Label ID="Label2" runat="server" Text="担当部署">
	</asp:Label>
	<asp:DropDownList ID="D_拠点" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="札幌オフィス">札幌オフィス</asp:ListItem>
		<asp:ListItem Value="仙台オフィス">仙台オフィス</asp:ListItem>
		<asp:ListItem Value="さいたまオフィス">さいたまオフィス</asp:ListItem>
		<asp:ListItem Value="東京オフィス">東京オフィス</asp:ListItem>
		<asp:ListItem Value="横浜オフィス">横浜オフィス</asp:ListItem>
		<asp:ListItem Value="名古屋オフィス">名古屋オフィス</asp:ListItem>
		<asp:ListItem Value="金沢オフィス">金沢オフィス</asp:ListItem>
		<asp:ListItem Value="大阪オフィス">大阪オフィス</asp:ListItem>
		<asp:ListItem Value="広島オフィス">広島オフィス</asp:ListItem>
		<asp:ListItem Value="福岡オフィス">福岡オフィス</asp:ListItem>
	</asp:DropDownList>
	&nbsp;
	<asp:Label ID="label利用終了" runat="server" Text="利用終了">
	</asp:Label>
	<asp:DropDownList ID="D_利用終了" runat="server" AutoPostBack="True">
	</asp:DropDownList>
	&nbsp;
	<asp:Button ID="B_クリア" runat="server" Text="クリア" />
	&nbsp;
	<asp:Button ID="B_エクスポート" runat="server" Text="EXCEL出力" />
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	</p>
	<p>
	<asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
	</p>
	<p>
	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
		BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
		CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSource1" Font-Size="Small"
		ShowFooter="False" AllowSorting="true">
		<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
		<Columns>
			<asp:BoundField DataField="拠点名" HeaderText="担当部署" SortExpression="拠点名" />
			<asp:BoundField DataField="顧客No" HeaderText="顧客No" SortExpression="顧客No" />
			<asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" />
			<asp:BoundField DataField="営業担当" HeaderText="営業担当" SortExpression="営業担当" />
			<asp:BoundField DataField="サービスID" HeaderText="サービスID" SortExpression="サービスID" />
			<asp:BoundField DataField="サービス名" HeaderText="サービス名" SortExpression="サービス名" />
			<asp:BoundField DataField="利用開始" HeaderText="利用開始" SortExpression="利用開始" />
			<asp:BoundField DataField="利用終了" HeaderText="利用終了" SortExpression="利用終了" />
			<asp:BoundField DataField="終了" HeaderText="終了" SortExpression="終了" />
			<asp:BoundField DataField="拠点コード" Visible="False" />
		</Columns>
		<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server"
		ConnectionString="<%$ ConnectionStrings:JunpDBConnectionString %>"
		SelectCommand="SELECT [拠点名], [顧客No], [顧客名], [営業担当], [サービスID], [サービス名], [利用開始], [利用終了], [終了] FROM [vSoftwareMainteLimit] WHERE (([拠点名] LIKE '%' + @拠点名 + '%') AND ([利用終了] LIKE '%' + @利用終了 + '%')) ORDER BY [利用終了], [拠点コード], [顧客No]">
		<SelectParameters>
			<asp:ControlParameter ControlID="D_拠点" Name="拠点名"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="D_利用終了" Name="利用終了"
				PropertyName="SelectedValue" Type="String" />
		</SelectParameters>
	</asp:SqlDataSource>
	</p>
	<p>
	&nbsp;</p>
	<p>
	&nbsp;</p>
	<p>
	&nbsp;</p>
	<p>
	</p>
	<p>
	</p>
</asp:Content>
