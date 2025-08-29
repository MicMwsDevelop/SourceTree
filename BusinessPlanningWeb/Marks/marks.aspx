<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="marks.aspx.vb" Inherits="marks_marks" title="販売管理－備考抽出" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
	<asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True"
		ForeColor="#3333FF">
	</asp:Label>
	<br />
	<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large"
		Text="販売管理－備考抽出">
	</asp:Label>
	&nbsp;&nbsp;&nbsp;&nbsp;
	</p>
	<p>
	<asp:Label ID="Label5" runat="server" Text="エリア">
	</asp:Label>
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
	<asp:Label ID="Label3" runat="server" Text="承認">
	</asp:Label>
	<asp:DropDownList ID="D_承認" runat="server" AutoPostBack="True">
		<asp:ListItem Value="%">すべて</asp:ListItem>
		<asp:ListItem Value="×">受注未承認</asp:ListItem>
		<asp:ListItem Value="△">受注承認済(出荷未完了)</asp:ListItem>
		<asp:ListItem Value="○">受注承認済(出荷完了)</asp:ListItem>
	</asp:DropDownList>
	&nbsp;
	<asp:Label ID="label納期" runat="server" Text="納期">
	</asp:Label>
	<asp:DropDownList ID="D_納期" runat="server" AutoPostBack="True">
	</asp:DropDownList>
	&nbsp
	<asp:Label ID="labelMarks" runat="server" Text="備考">
	</asp:Label>
	<asp:TextBox ID="textBoxMarks" runat="server" Text="%">
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
	<asp:GridView ID="GridViewMarks" runat="server" AutoGenerateColumns="False"
		BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
		CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSource1" Font-Size="Small"
		ShowFooter="True" AllowSorting="true">
		<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
		<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
		<Columns>
			<asp:BoundField DataField="拠点名" HeaderText="担当部署" SortExpression="拠点名" />
			<asp:BoundField DataField="担当者" HeaderText="担当者" SortExpression="担当者" />
			<asp:BoundField DataField="受注No" HeaderText="受注No" SortExpression="受注No" />
			<asp:BoundField DataField="受注日" DataFormatString="{0:yyyy/MM/dd}"
				HeaderText="受注日" SortExpression="受注日" />
			<asp:BoundField DataField="販売先" HeaderText="販売先" SortExpression="販売先" />
			<asp:BoundField DataField="ユーザー" HeaderText="ユーザー" SortExpression="ユーザー" />
			<asp:BoundField DataField="納期" HeaderText="納期" SortExpression="納期" />
			<asp:BoundField DataField="受注金額" DataFormatString="{0:c}" HeaderText="受注金額"
				SortExpression="受注金額" />
			<asp:BoundField DataField="受注承認日" DataFormatString="{0:yyyy/MM/dd}"
				HeaderText="受注承認日" SortExpression="受注承認日" />
			<asp:BoundField DataField="出荷完了日" DataFormatString="{0:yyyy/MM/dd}"
				HeaderText="出荷完了日" SortExpression="出荷完了日" />
			<asp:BoundField DataField="入金予定日" DataFormatString="{0:yyyy/MM/dd}"
				HeaderText="入金予定日" SortExpression="入金予定日" />
			<asp:BoundField DataField="件名" HeaderText="件名" SortExpression="件名" />
			<asp:BoundField DataField="ｻｰﾋﾞｽ利用期間" HeaderText="ｻｰﾋﾞｽ利用期間" SortExpression="ｻｰﾋﾞｽ利用期間" />
			<asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考" />
			<asp:BoundField DataField="拠点コード" Visible="False" />
			<asp:BoundField DataField="承認" Visible="False" />
			<asp:BoundField DataField="営業部コード" Visible="False" />
			<asp:BoundField DataField="エリア" Visible="False" />
		</Columns>
		<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server"
		ConnectionString="<%$ ConnectionStrings:JunpDBConnectionString %>"
		SelectCommand="SELECT [拠点名], [担当者], [納期], [販売先], [ユーザー], [受注金額], [受注No], [受注日], [受注承認日], [出荷完了日], [入金予定日], [件名], [ｻｰﾋﾞｽ利用期間], [備考] FROM [vMarks] WHERE (([拠点名] LIKE '%' + @拠点名 + '%') AND ([承認] LIKE '%' + @承認 + '%') AND ([エリア] LIKE '%' + @エリア + '%') AND ([納期年月] LIKE '%' + @納期年月 + '%') AND ([備考] LIKE '%' + @備考 + '%')) ORDER BY [拠点コード] ASC, [受注No] ASC">
		<SelectParameters>
			<asp:ControlParameter ControlID="D_Tanto" Name="拠点名"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="D_承認" Name="承認" PropertyName="SelectedValue"
				Type="String" />
			<asp:ControlParameter ControlID="D_Area" Name="エリア"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="D_納期" Name="納期年月"
				PropertyName="SelectedValue" Type="String" />
			<asp:ControlParameter ControlID="textBoxMarks" Name="備考"
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
