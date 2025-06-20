<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="OnlineLicenseProgressList.aspx.vb" Inherits="OnlineLicenseProgressList_OnlineLicenseProgressList" title="オン資進捗管理顧客一覧" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
	<asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True"
		ForeColor="#3333FF">
	</asp:Label>
	<br />
	<asp:Label ID="labelPageTitle" runat="server" Font-Bold="True" Font-Size="Large"
		Text="オン資進捗管理顧客一覧　/">
	</asp:Label>
	&nbsp;&nbsp;&nbsp;&nbsp;
	<asp:HyperLink ID="HyperLink1" runat="server"
		NavigateUrl="http://wwsv/businessplanningweb/OnlineLicenseProgressTable/OnlineLicenseProgressTable.aspx"
		style="font-weight: 700">オン資進捗管理表
	</asp:HyperLink>
	</p>
	<p>
	<asp:Label ID="labelSale" runat="server" Text="担当営業">
	</asp:Label>
	<asp:DropDownList ID="comboBoxSale" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="東日本営業部">東日本営業部</asp:ListItem>
		<asp:ListItem Value="西日本営業部">西日本営業部</asp:ListItem>
	</asp:DropDownList>
	&nbsp
	<asp:Label ID="labelSC" runat="server" Text="SC">
	</asp:Label>
	<asp:DropDownList ID="comboBoxSC" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="東日本SC">東日本SC</asp:ListItem>
		<asp:ListItem Value="首都圏SC">首都圏SC</asp:ListItem>
		<asp:ListItem Value="中日本SC">中日本SC</asp:ListItem>
		<asp:ListItem Value="関西SC">関西SC</asp:ListItem>
		<asp:ListItem Value="西日本SC">西日本SC</asp:ListItem>
	</asp:DropDownList>
	&nbsp
	<asp:Label ID="labelOffice" runat="server" Text="オフィス">
	</asp:Label>
	<asp:DropDownList ID="comboBoxOffice" runat="server" AutoPostBack="True">
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
	&nbsp
	<asp:Label ID="labelCustomerNo" runat="server" Text="顧客No" />
	<asp:TextBox ID="textBoxCustomerNo" runat="server" Text="" />
	<asp:Button ID="buttonSearch" runat="server" Text="検索" />
	&nbsp;
	<asp:Button ID="buttonClear" runat="server" Text="クリア" />
	&nbsp;
	<asp:Button ID="buttonExcel" runat="server" Text="EXCEL出力" />
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	</p>
	<p>
	<asp:Label ID="labelResult" runat="server" Font-Bold="True">
	</asp:Label>
	</p>
	<p>
	<asp:GridView ID="GridViewProgressList" runat="server" AutoGenerateColumns="False"
		BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
		CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSourceProgressList" Font-Size="Small"
		ShowFooter="False" AllowSorting="true" AllowPaging="true" PageSize=1000>
		<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
		<Columns>
			<asp:BoundField DataField="営業部" HeaderText="担当営業" SortExpression="担当営業" />
			<asp:BoundField DataField="SC名" HeaderText="SC" SortExpression="SC" />
			<asp:BoundField DataField="拠点名" HeaderText="オフィス" SortExpression="オフィス" />
			<asp:BoundField DataField="顧客No" HeaderText="顧客No" SortExpression="顧客No" />
			<asp:BoundField DataField="得意先No" HeaderText="得意先No" SortExpression="得意先No" />
			<asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" />
			<asp:BoundField DataField="都道府県" HeaderText="都道府県" SortExpression="都道府県" />
			<asp:BoundField DataField="進捗管理ステータス" HeaderText="進捗管理ステータス" SortExpression="進捗管理ステータス" />
			<asp:BoundField DataField="オン資担当" HeaderText="オン資担当" SortExpression="オン資担当" />
			<asp:BoundField DataField="導入意思" HeaderText="導入意思" SortExpression="導入意思" />
			<asp:BoundField DataField="工事種別" HeaderText="工事種別" SortExpression="工事種別" />
			<asp:BoundField DataField="ステータス" HeaderText="ステータス" SortExpression="ステータス" />
			<asp:BoundField DataField="現調完了月" HeaderText="現調完了月" SortExpression="現調完了月" />
			<asp:BoundField DataField="導入月" HeaderText="導入月" SortExpression="導入月" />
			<asp:BoundField DataField="価格帯" HeaderText="価格帯" SortExpression="価格帯" />
		</Columns>
		<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSourceProgressList" runat="server"
		ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>"
		SelectCommand="SELECT [顧客No],[得意先No],[顧客名],[オン資担当],[導入意思],[工事種別],[ステータス],[現調完了月],[導入月],[価格帯],[部署],[都道府県],[営業部],[SC名],[拠点名],[進捗管理ステータス] FROM [vオンライン資格確認進捗管理情報] WHERE ([営業部] LIKE '%' + @担当営業 + '%') AND ([SC名] LIKE '%' + @SC + '%') AND ([拠点名] LIKE '%' + @オフィス + '%') ORDER BY [営業部コード], [SCコード], [拠点コード], [顧客No]">
		<SelectParameters>
			<asp:ControlParameter ControlID="comboBoxSale" Name="担当営業"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="comboBoxSC" Name="SC"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="comboBoxOffice" Name="オフィス"
				PropertyName="SelectedValue" Type="String" />
		</SelectParameters>
	</asp:SqlDataSource>

	<asp:GridView ID="GridViewUnvisible" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor=""
		BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
		CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSourceUnvisible" Font-Size="Small"
		ShowFooter="False" AllowSorting="False"  Visible="False" >
		<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
		<Columns>
			<asp:BoundField DataField="営業部" HeaderText="担当営業" />
			<asp:BoundField DataField="SC名" HeaderText="SC" />
			<asp:BoundField DataField="拠点名" HeaderText="オフィス" />
			<asp:BoundField DataField="顧客No" HeaderText="顧客No" />
			<asp:BoundField DataField="得意先No" HeaderText="得意先No" />
			<asp:BoundField DataField="顧客名" HeaderText="顧客名" />
			<asp:BoundField DataField="都道府県" HeaderText="都道府県" />
			<asp:BoundField DataField="オン資担当" HeaderText="オン資担当" />
			<asp:BoundField DataField="進捗管理ステータス" HeaderText="進捗管理ステータス" />
			<asp:BoundField DataField="導入意思" HeaderText="導入意思" />
			<asp:BoundField DataField="工事種別" HeaderText="工事種別" />
			<asp:BoundField DataField="ステータス" HeaderText="ステータス" />
			<asp:BoundField DataField="現調完了月" HeaderText="現調完了月" />
			<asp:BoundField DataField="導入月" HeaderText="導入月" />
			<asp:BoundField DataField="部署" HeaderText="部署" />
			<asp:BoundField DataField="価格帯" HeaderText="価格帯" />
			<asp:BoundField DataField="郵便番号" HeaderText="郵便番号" />
			<asp:BoundField DataField="住所" HeaderText="住所" />
			<asp:BoundField DataField="電話番号" HeaderText="電話番号" />
			<asp:BoundField DataField="FAX番号" HeaderText="FAX番号" />
		</Columns>
		<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSourceUnvisible" runat="server"
		ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>">
	</asp:SqlDataSource>

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
