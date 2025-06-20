<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="OnlineLicenseProgressTable.aspx.vb" Inherits="OnlineLicenseProgressTable_OnlineLicenseProgressTable" title="オン資進捗管理表" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
	<asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True"
		ForeColor="#3333FF">
	</asp:Label>
	<br />
	<asp:Label ID="labelPageTitle" runat="server" Font-Bold="True" Font-Size="Large"
		Text="オン資進捗管理表　/">
	</asp:Label>
	&nbsp;&nbsp;&nbsp;&nbsp;
	<asp:HyperLink ID="HyperLink1" runat="server"
		NavigateUrl="http://wwsv/businessplanningweb/OnlineLicenseProgressList/OnlineLicenseProgressList.aspx"
		style="font-weight: 700">オン資進捗管理顧客一覧
	</asp:HyperLink>
	</p>
	<p>
	<asp:Button ID="buttonExcel" runat="server" Text="EXCEL出力" />
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	</p>
	<p>
	<asp:GridView ID="GridViewTable" runat="server" AutoGenerateColumns="False"
		BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
		CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSourceTable" Font-Size="Small"
		ShowFooter="True" AllowSorting="true">
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="価格帯" HeaderText="価格帯" SortExpression="価格帯" />
			<asp:BoundField DataField="営業部" HeaderText="担当営業" SortExpression="担当営業" />
			<asp:BoundField DataField="集計対象外件数" HeaderText="集計対象外件数" SortExpression="集計対象外件数" DataFormatString="{0:N0}" HtmlEncode=False  />
			<asp:BoundField DataField="集計対象外比率" HeaderText="集計対象外比率" SortExpression="集計対象外比率" DataFormatString="{0:#0.#%}" HtmlEncode=False />
			<asp:BoundField DataField="営業活動中件数" HeaderText="営業活動中件数" SortExpression="営業活動中件数" DataFormatString="{0:N0}" HtmlEncode=False />
			<asp:BoundField DataField="営業活動中比率" HeaderText="営業活動中比率" SortExpression="営業活動中比率" DataFormatString="{0:#0.#%}" HtmlEncode=False />
			<asp:BoundField DataField="現調予定件数" HeaderText="現調予定件数" SortExpression="現調予定件数" DataFormatString="{0:N0}" HtmlEncode=False />
			<asp:BoundField DataField="現調予定比率" HeaderText="現調予定比率" SortExpression="現調予定比率" DataFormatString="{0:#0.#%}" HtmlEncode=False />
			<asp:BoundField DataField="現調済件数" HeaderText="現調済件数" SortExpression="現調済件数" DataFormatString="{0:N0}" HtmlEncode=False />
			<asp:BoundField DataField="現調済比率" HeaderText="現調済比率" SortExpression="現調済比率" DataFormatString="{0:#0.#%}" HtmlEncode=False />
			<asp:BoundField DataField="導入済件数" HeaderText="導入済件数" SortExpression="導入済件数" DataFormatString="{0:N0}" HtmlEncode=False />
			<asp:BoundField DataField="導入済比率" HeaderText="導入済比率" SortExpression="導入済比率" DataFormatString="{0:#0.#%}" HtmlEncode=False />
			<asp:BoundField DataField="全体の件数" HeaderText="全体件数" SortExpression="全体件数" DataFormatString="{0:N0}" HtmlEncode=False />
			<asp:BoundField DataField="全体の比率" HeaderText="全体比率" SortExpression="全体比率" DataFormatString="{0:#0.#%}" HtmlEncode=False />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSourceTable" runat="server"
		ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>"
		SelectCommand="SELECT [価格帯],[営業部],[集計対象外件数],[集計対象外比率],[営業活動中件数],[営業活動中比率],[現調予定件数],[現調予定比率],[現調済件数],[現調済比率],[導入済件数],[導入済比率],[全体の件数],[全体の比率] FROM [SalesDB].[dbo].[vオンライン資格確認進捗管理表] ORDER BY [価格帯コード], [営業部コード]">
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
