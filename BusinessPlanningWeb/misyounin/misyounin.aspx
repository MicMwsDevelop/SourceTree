<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="misyounin.aspx.vb" Inherits="misyounin_misyounin" title="WonderWeb販売管理－受注残一覧" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
	<asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True"
		ForeColor="#3333FF">
	</asp:Label>
	<br />
	<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large"
		Text="WonderWeb販売管理－受注残(受注未承認/受注承認)一覧　/">
	</asp:Label>
	&nbsp;&nbsp;&nbsp;&nbsp;
	<asp:HyperLink ID="HyperLink1" runat="server"
		NavigateUrl="http://wwsv/businessplanningweb/uriagesyounin/uriagesyounin.aspx"
		style="font-weight: 700">WonderWeb販売管理－当月売上承認一覧
	</asp:HyperLink>
	</p>
	<p>
	<asp:Label ID="Label2" runat="server" Text="担当部署">
	</asp:Label>
	<asp:DropDownList ID="D_Tanto" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="営業1課">営業1課</asp:ListItem>
		<asp:ListItem Value="営業2課">営業2課</asp:ListItem>
		<asp:ListItem Value="関東エリア1課">関東エリア1課</asp:ListItem>
		<asp:ListItem Value="関東エリア2課">関東エリア2課</asp:ListItem>
		<asp:ListItem Value="関西エリア1課">関西エリア1課</asp:ListItem>
		<asp:ListItem Value="九州エリア">九州エリア</asp:ListItem>
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
	<asp:Label ID="Label3" runat="server" Text="承認"></asp:Label>
	<asp:DropDownList ID="D_承認" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="×">受注未承認</asp:ListItem>
		<asp:ListItem Value="△">受注承認済(出荷未完了)</asp:ListItem>
		<asp:ListItem Value="○">受注承認済(出荷完了)</asp:ListItem>
	</asp:DropDownList>
	&nbsp;
	<asp:Label ID="Label4" runat="server" Text="伝票種別">
	</asp:Label>
	<asp:DropDownList ID="D_システム" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="月額課金">月額課金</asp:ListItem>
		<asp:ListItem Value="まとめ">まとめ</asp:ListItem>
		<asp:ListItem Value="ES">ES</asp:ListItem>
		<asp:ListItem Value="その他">その他</asp:ListItem>
	</asp:DropDownList>
	</p>
	<p>
	&nbsp;
	<asp:Label ID="Label5" runat="server" Text="エリア"></asp:Label>
	<asp:DropDownList ID="D_Area" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="プロモーション営業部">プロモーション営業部</asp:ListItem>
		<asp:ListItem Value="ソリューション営業部">ソリューション営業部</asp:ListItem>
		<asp:ListItem Value="東日本サポートセンター">東日本サポートセンター</asp:ListItem>
		<asp:ListItem Value="首都圏サポートセンター">首都圏サポートセンター</asp:ListItem>
		<asp:ListItem Value="中日本サポートセンター">中日本サポートセンター</asp:ListItem>
		<asp:ListItem Value="関西サポートセンター">関西サポートセンター</asp:ListItem>
		<asp:ListItem Value="西日本サポートセンター">西日本サポートセンター</asp:ListItem>
	</asp:DropDownList>
	&nbsp;
	<asp:Label ID="label納期" runat="server" Text="納期">
	</asp:Label>
	<asp:DropDownList ID="D_納期" runat="server" AutoPostBack="True">
	</asp:DropDownList>
	&nbsp
	<asp:Label ID="labelReplace" runat="server" Text="リプレース">
	</asp:Label>
	<asp:DropDownList ID="comboBoxReplace" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="なし">なし</asp:ListItem>
		<asp:ListItem Value="あり">あり</asp:ListItem>
	</asp:DropDownList>
	&nbsp
	<asp:Label ID="labelKenmei" runat="server" Text="件名">
	</asp:Label>
	<asp:TextBox ID="textBoxKenmei" runat="server" Text="%">
	</asp:TextBox>
	&nbsp;
	<asp:Button ID="B_クリア" runat="server" Text="クリア" />
	&nbsp;
	<asp:Button ID="B_エクスポート" runat="server" Text="EXCEL出力" />
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	</p>
	<p>
	<asp:Label ID="L_検索結果" runat="server" Font-Bold="True">
	</asp:Label>
	</p>
	<p>
	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
		BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
		CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSource1" Font-Size="Small"
		ShowFooter="True" AllowSorting="true">
		<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
		<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
		<Columns>
			<asp:BoundField DataField="拠点名" HeaderText="担当部署" SortExpression="拠点名" />
			<asp:BoundField DataField="担当者" HeaderText="担当者" SortExpression="担当者" />
			<asp:BoundField DataField="納期" HeaderText="納期" SortExpression="納期" />
			<asp:BoundField DataField="販売先" HeaderText="販売先" SortExpression="販売先" />
			<asp:BoundField DataField="ユーザー" HeaderText="ユーザー" SortExpression="ユーザー" />
			<asp:BoundField DataField="受注金額" DataFormatString="{0:c}" HeaderText="受注金額"
				SortExpression="受注金額" />
			<asp:BoundField DataField="受注No" HeaderText="受注No" SortExpression="受注No" />
			<asp:BoundField DataField="受注日" DataFormatString="{0:yyyy/MM/dd}"
				HeaderText="受注日" SortExpression="受注日" />
			<asp:BoundField DataField="受注承認日" DataFormatString="{0:yyyy/MM/dd}"
				HeaderText="受注承認日" SortExpression="受注承認日" />
			<asp:BoundField DataField="出荷完了日" DataFormatString="{0:yyyy/MM/dd}"
				HeaderText="出荷完了日" SortExpression="出荷完了日" />
			<asp:BoundField DataField="入金予定日" DataFormatString="{0:yyyy/MM/dd}"
				HeaderText="入金予定日" SortExpression="入金予定日" />
			<asp:BoundField DataField="件名" HeaderText="件名" SortExpression="件名" />
			<asp:BoundField DataField="リプレース" HeaderText="リプレース" SortExpression="リプレース" />
			<asp:BoundField DataField="ES本数" HeaderText="ES本数" SortExpression="ES本数" />
			<asp:BoundField DataField="ｻｰﾋﾞｽ利用期間" HeaderText="ｻｰﾋﾞｽ利用期間" SortExpression="ｻｰﾋﾞｽ利用期間" />
			<asp:BoundField DataField="拠点コード" Visible="False" />
			<asp:BoundField DataField="承認" Visible="False" />
			<asp:BoundField DataField="伝票種別" Visible="False" />
			<asp:BoundField DataField="エリア" Visible="False" />
			<asp:BoundField DataField="営業部コード" Visible="False" />
			<asp:BoundField DataField="システム商品コード" Visible="False" />
			<asp:BoundField DataField="システム商品名" Visible="False" />
			<asp:BoundField DataField="リプレース有無" Visible="False" />
		</Columns>
		<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server"
		ConnectionString="<%$ ConnectionStrings:JunpDBConnectionString %>"
		SelectCommand="SELECT [拠点名], [担当者], [納期], [販売先], [ユーザー], [受注金額], [受注No], [受注日], [受注承認日], [出荷完了日], [入金予定日], [件名], [リプレース], [ES本数], [ｻｰﾋﾞｽ利用期間] FROM [vMisyounin] WHERE (([拠点名] LIKE '%' + @拠点名 + '%') AND ([承認] LIKE '%' + @承認 + '%') AND ([伝票種別] LIKE '%' + @伝票種別 + '%') AND ([エリア] LIKE '%' + @エリア + '%') AND ([納期年月] LIKE '%' + @納期年月 + '%') AND ([リプレース有無] LIKE '%' + @リプレース有無 + '%') AND ([件名] LIKE '%' + @件名 + '%')) ORDER BY [拠点コード], [受注日]">
		<SelectParameters>
			<asp:ControlParameter ControlID="D_Tanto" Name="拠点名"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="D_承認" Name="承認" PropertyName="SelectedValue"
				Type="String" />
			<asp:ControlParameter ControlID="D_システム" Name="伝票種別"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="D_Area" Name="エリア"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="D_納期" Name="納期年月"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="comboBoxReplace" Name="リプレース有無"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="textBoxKenmei" Name="件名"
				PropertyName="Text" Type="String" />
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
