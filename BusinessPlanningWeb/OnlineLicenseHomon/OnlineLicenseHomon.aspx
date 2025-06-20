<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="OnlineLicenseHomon.aspx.vb" Inherits="OnlineLicenseHomon_OnlineLicenseHomon" title="オン資訪問診療連携費顧客一覧" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
	<asp:Label ID="labelLoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF" />
	<br />
	<asp:Label ID="LabelTitle" runat="server" Font-Bold="True" Font-Size="Large" Text="オン資訪問診療連携費顧客一覧" />
	&nbsp;&nbsp;&nbsp;&nbsp;
	</p>
	<p>
	<asp:Label ID="Label担当部署" runat="server" Text="担当部署" />
	<asp:DropDownList ID="comboBoxSC" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="東日本サポートセンター">東日本サポートセンター</asp:ListItem>
		<asp:ListItem Value="首都圏サポートセンター">首都圏サポートセンター</asp:ListItem>
		<asp:ListItem Value="中日本サポートセンター">中日本サポートセンター</asp:ListItem>
		<asp:ListItem Value="関西サポートセンター">関西サポートセンター</asp:ListItem>
		<asp:ListItem Value="西日本サポートセンター">西日本サポートセンター</asp:ListItem>
	</asp:DropDownList>
	&nbsp;
	<asp:Label ID="Labelオフィス" runat="server" Text="オフィス" />
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
	&nbsp;
	<asp:Label ID="label申込月" runat="server" Text="申込月" />
	<asp:TextBox ID="textBoxAcceptMonth" runat="server" Width="55px" ToolTip="yyyy/MM" />
	&nbsp;
	<asp:Label ID="label売上月" runat="server" Text="売上月" />
	<asp:TextBox ID="textBoxSaleMonth" runat="server" Width="55px" ToolTip="yyyy/MM" />
	<br /><br />
	<asp:Label ID="label顧客No" runat="server" Text="顧客No" />
	<asp:TextBox ID="textBoxCustomerNo" runat="server" Text="" />
	<asp:Button ID="buttonSearch" runat="server" Text="検索" />
	&nbsp;
	<asp:Button ID="buttonClear" runat="server" Text="クリア" />
	&nbsp;
	<asp:Button ID="buttonExport" runat="server" Text="EXCEL出力" />
	<br /><br />
	<asp:Label ID="labelMsg1" runat="server" ForeColor="Red" Text="カードリーダーの情報は、オンライン資格確認訪問診療連携費の申込時点のものになります。" />
	<br />
	<asp:Label ID="labelMsg2" runat="server" ForeColor="Red" Text="申込後カードリーダーをキャンセルした場合や、別途 MIC e store にてカードリーダーを購入した場合は実態と異なりますのでご注意ください。" />
	<br /><br />
	<asp:Label ID="labelResult" runat="server" Font-Bold="True" />
	</p>
	<p>
	<asp:GridView ID="GridViewOnlineLicenseHomon" runat="server" AutoGenerateColumns="False"
		BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
		CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSourceOnlineLicenseHomon" Font-Size="Small"
		ShowFooter="False" AllowSorting="true" AllowPaging="true" PageSize=1000>
		<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="担当部署" SortExpression="営業部コード" />
			<asp:BoundField DataField="拠点名" HeaderText="オフィス" SortExpression="拠点コード" />
			<asp:BoundField DataField="顧客No" HeaderText="顧客No" SortExpression="顧客No" />
			<asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" SortExpression="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" SortExpression="商品名" />
			<asp:BoundField DataField="カードリーダー" HeaderText="カードリーダー" SortExpression="カードリーダー" />
			<asp:BoundField DataField="売上金額" DataFormatString="{0:c}" HeaderText="売上金額" SortExpression="売上金額" />
			<asp:BoundField DataField="申込日" DataFormatString="{0:yyyy/MM/dd}" HeaderText="申込日" SortExpression="申込日" />
			<asp:BoundField DataField="売上日" DataFormatString="{0:yyyy/MM/dd}" HeaderText="売上日" SortExpression="売上日" />
			<asp:BoundField DataField="営業部コード" Visible="False" />
			<asp:BoundField DataField="拠点コード" Visible="False" />
		</Columns>
		<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSourceOnlineLicenseHomon" runat="server"
		ConnectionString="<%$ ConnectionStrings:CharlieDBConnectionString %>"
		SelectCommand="SELECT [営業部コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[商品コード],[商品名],iif([カードリーダー申込No] is null,'無','有') as カードリーダー,[売上金額],CONVERT(Date, [申込日時]) as 申込日,CONVERT(Date, [売上日時]) as 売上日 FROM [vBPW_オン資訪問診療連携費顧客一覧] WHERE ([営業部名] LIKE '%' + @SC + '%') AND ([拠点名] LIKE '%' + @オフィス + '%') ORDER BY [営業部コード],[拠点コード],[申込日],[顧客No]">
		<SelectParameters>
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
			<asp:BoundField DataField="営業部コード" HeaderText="担当部署コード" />
			<asp:BoundField DataField="営業部名" HeaderText="担当部署名" />
			<asp:BoundField DataField="拠点コード" HeaderText="拠点コード" />
			<asp:BoundField DataField="拠点名" HeaderText="拠点名"  />
			<asp:BoundField DataField="顧客No" HeaderText="顧客No"  />
			<asp:BoundField DataField="顧客名" HeaderText="顧客名"  />
			<asp:BoundField DataField="得意先No" HeaderText="得意先No"  />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード"  />
			<asp:BoundField DataField="商品名" HeaderText="商品名"  />
			<asp:BoundField DataField="カードリーダー" HeaderText="カードリーダー"  />
			<asp:BoundField DataField="売上金額" HeaderText="売上金額"  />
			<asp:BoundField DataField="申込日" DataFormatString="{0:yyyy/MM/dd}" HeaderText="申込日"  />
			<asp:BoundField DataField="売上日" DataFormatString="{0:yyyy/MM/dd}" HeaderText="売上日" />
		</Columns>
		<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSourceUnvisible" runat="server"
		ConnectionString="<%$ ConnectionStrings:CharlieDBConnectionString %>">
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
