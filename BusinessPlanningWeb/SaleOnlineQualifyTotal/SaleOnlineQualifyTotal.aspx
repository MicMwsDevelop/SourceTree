<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="SaleOnlineQualifyTotal.aspx.vb" Inherits="SaleOnlineQualifyTotal_SaleOnlineQualifyTotal" title="オンライン資格確認売上集計" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
	<asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF">
	</asp:Label>
	<br />
	<asp:Label ID="label売上集計" runat="server" Font-Bold="True" Font-Size="Large"
		Text="オンライン資格確認売上集計　/">
	</asp:Label>
	&nbsp;&nbsp;&nbsp;&nbsp;
	<asp:HyperLink ID="HyperLink1" runat="server"
		NavigateUrl="http://wwsv/businessplanningweb/SaleOnlineQualifyList/SaleOnlineQualifyList.aspx"
		style="font-weight: 700">オンライン資格確認売上一覧
	</asp:HyperLink>
	</p>
	<p>
	<asp:Label ID="label集計単位" runat="server" Text="集計単位">
	</asp:Label>
	<asp:DropDownList ID="dropList集計単位" runat="server" AutoPostBack="False">
		<asp:ListItem Value="営業部">部</asp:ListItem>
		<asp:ListItem Value="都道府県">都道府県</asp:ListItem>
	</asp:DropDownList>
	&nbsp;
	<asp:Label ID="label集計期間" runat="server" Text="集計期間"></asp:Label>
	<asp:TextBox ID="textBox開始日" runat="server" Width="87px" ToolTip="YYYY/MM/DD">
	</asp:TextBox>
	<asp:Label ID="LabelKara" runat="server" Text="～"></asp:Label>
	<asp:TextBox ID="textBox終了日" runat="server" Width="87px" ToolTip="YYYY/MM/DD">
	</asp:TextBox>
	</p>
	<p>
	<asp:Label ID="label商品名" runat="server" Text="検索商品" Font-Bold="True" /><br>
	<asp:ListBox ID="GoodsListBox" runat="server" Rows=6 Width="350px" SelectionMode="Multiple" DataSourceID="SqlDataSourceGoods" DataTextField="商品名" DataValueField="sms_scd"/>
	<asp:SqlDataSource ID="SqlDataSourceGoods" runat="server">
	</asp:SqlDataSource>
	<asp:Button ID="button集計実行" runat="server" Text="集計実行" />
	&nbsp;
	<asp:Button ID="buttonOutputExcel" runat="server" Text="EXCEL出力" />
	<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
		ControlToValidate="textBox開始日" ErrorMessage="開始日の入力に誤りがあります。(YYYY/MM/DD)" 
		ValidationExpression="\d{4}/\d{2}/\d{2}">※</asp:RegularExpressionValidator>
	<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
		ControlToValidate="textBox終了日" ErrorMessage="終了日の入力に誤りがあります。(YYYY/MM/DD)" 
		ValidationExpression="\d{4}/\d{2}/\d{2}">※</asp:RegularExpressionValidator>
	</p>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" />

	<p>
	<asp:Label ID="label東日本営業部" runat="server" Text="東日本営業部" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView東日本営業部" runat="server" DataSourceID="SqlDataSource東日本営業部" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
	<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
	<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
	<Columns>
		<asp:BoundField DataField="都道府県名" HeaderText="都道府県" />
		<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
		<asp:BoundField DataField="商品名" HeaderText="商品名" />
		<asp:BoundField DataField="数量" HeaderText="数量" />
		<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
	</Columns>
	<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
	<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource東日本営業部" runat="server">
	</asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label西日本営業部" runat="server" Text="西日本営業部" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView西日本営業部" runat="server" DataSourceID="SqlDataSource西日本営業部" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
	<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
	<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
	<Columns>
		<asp:BoundField DataField="都道府県名" HeaderText="都道府県" />
		<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
		<asp:BoundField DataField="商品名" HeaderText="商品名" />
		<asp:BoundField DataField="数量" HeaderText="数量" />
		<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
	</Columns>
	<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
	<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource西日本営業部" runat="server">
	</asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label東日本サポートセンター" runat="server" Text="東日本サポートセンター" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView東日本サポートセンター" runat="server" DataSourceID="SqlDataSource東日本サポートセンター" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
	<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
	<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
	<Columns>
		<asp:BoundField DataField="都道府県名" HeaderText="都道府県" />
		<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
		<asp:BoundField DataField="商品名" HeaderText="商品名" />
		<asp:BoundField DataField="数量" HeaderText="数量" />
		<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
	</Columns>
	<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
	<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource東日本サポートセンター" runat="server">
	</asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label首都圏サポートセンター" runat="server" Text="首都圏サポートセンター" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView首都圏サポートセンター" runat="server" DataSourceID="SqlDataSource首都圏サポートセンター" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="都道府県名" HeaderText="都道府県" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource首都圏サポートセンター" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label中日本サポートセンター" runat="server" Text="中日本サポートセンター" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView中日本サポートセンター" runat="server" DataSourceID="SqlDataSource中日本サポートセンター" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="都道府県名" HeaderText="都道府県" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource中日本サポートセンター" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label関西サポートセンター" runat="server" Text="関西サポートセンター" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView関西サポートセンター" runat="server" DataSourceID="SqlDataSource関西サポートセンター" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="都道府県名" HeaderText="都道府県" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource関西サポートセンター" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label西日本サポートセンター" runat="server" Text="西日本サポートセンター" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView西日本サポートセンター" runat="server" DataSourceID="SqlDataSource西日本サポートセンター" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="都道府県名" HeaderText="都道府県" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource西日本サポートセンター" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label01" runat="server" Text="北海道" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView01" runat="server" DataSourceID="SqlDataSource01" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource01" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label02" runat="server" Text="青森県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView02" runat="server" DataSourceID="SqlDataSource02" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource02" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label03" runat="server" Text="岩手県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView03" runat="server" DataSourceID="SqlDataSource03" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource03" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label04" runat="server" Text="宮城県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView04" runat="server" DataSourceID="SqlDataSource04" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource04" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label05" runat="server" Text="秋田県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView05" runat="server" DataSourceID="SqlDataSource05" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource05" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label06" runat="server" Text="山形県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView06" runat="server" DataSourceID="SqlDataSource06" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource06" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label07" runat="server" Text="福島県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView07" runat="server" DataSourceID="SqlDataSource07" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource07" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label08" runat="server" Text="茨城県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView08" runat="server" DataSourceID="SqlDataSource08" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource08" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label09" runat="server" Text="栃木県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView09" runat="server" DataSourceID="SqlDataSource09" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource09" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label10" runat="server" Text="群馬県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView10" runat="server" DataSourceID="SqlDataSource10" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource10" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label11" runat="server" Text="埼玉県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView11" runat="server" DataSourceID="SqlDataSource11" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource11" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label12" runat="server" Text="千葉県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView12" runat="server" DataSourceID="SqlDataSource12" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource12" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label13" runat="server" Text="東京都" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView13" runat="server" DataSourceID="SqlDataSource13" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource13" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label14" runat="server" Text="神奈川県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView14" runat="server" DataSourceID="SqlDataSource14" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource14" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label15" runat="server" Text="新潟県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView15" runat="server" DataSourceID="SqlDataSource15" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource15" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label16" runat="server" Text="富山県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView16" runat="server" DataSourceID="SqlDataSource16" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource16" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label17" runat="server" Text="石川県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView17" runat="server" DataSourceID="SqlDataSource17" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource17" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label18" runat="server" Text="福井県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView18" runat="server" DataSourceID="SqlDataSource18" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource18" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label19" runat="server" Text="山梨県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView19" runat="server" DataSourceID="SqlDataSource19" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource19" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label20" runat="server" Text="長野県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView20" runat="server" DataSourceID="SqlDataSource20" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource20" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label21" runat="server" Text="岐阜県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView21" runat="server" DataSourceID="SqlDataSource21" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource21" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label22" runat="server" Text="静岡県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView22" runat="server" DataSourceID="SqlDataSource22" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource22" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label23" runat="server" Text="愛知県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView23" runat="server" DataSourceID="SqlDataSource23" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource23" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label24" runat="server" Text="三重県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView24" runat="server" DataSourceID="SqlDataSource24" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource24" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label25" runat="server" Text="滋賀県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView25" runat="server" DataSourceID="SqlDataSource25" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource25" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label26" runat="server" Text="京都府" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView26" runat="server" DataSourceID="SqlDataSource26" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource26" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label27" runat="server" Text="大阪府" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView27" runat="server" DataSourceID="SqlDataSource27" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource27" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label28" runat="server" Text="兵庫県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView28" runat="server" DataSourceID="SqlDataSource28" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource28" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label29" runat="server" Text="奈良県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView29" runat="server" DataSourceID="SqlDataSource29" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource29" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label30" runat="server" Text="和歌山県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView30" runat="server" DataSourceID="SqlDataSource30" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource30" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label31" runat="server" Text="鳥取県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView31" runat="server" DataSourceID="SqlDataSource31" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource31" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label32" runat="server" Text="島根県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView32" runat="server" DataSourceID="SqlDataSource32" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource32" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label33" runat="server" Text="岡山県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView33" runat="server" DataSourceID="SqlDataSource33" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource33" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label34" runat="server" Text="広島県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView34" runat="server" DataSourceID="SqlDataSource34" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource34" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label35" runat="server" Text="山口県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView35" runat="server" DataSourceID="SqlDataSource35" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource35" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label36" runat="server" Text="徳島県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView36" runat="server" DataSourceID="SqlDataSource36" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource36" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label37" runat="server" Text="香川県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView37" runat="server" DataSourceID="SqlDataSource37" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource37" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label38" runat="server" Text="愛媛県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView38" runat="server" DataSourceID="SqlDataSource38" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource38" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label39" runat="server" Text="高知県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView39" runat="server" DataSourceID="SqlDataSource39" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource39" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label40" runat="server" Text="福岡県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView40" runat="server" DataSourceID="SqlDataSource40" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource40" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label41" runat="server" Text="佐賀県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView41" runat="server" DataSourceID="SqlDataSource41" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource41" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label42" runat="server" Text="長崎県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView42" runat="server" DataSourceID="SqlDataSource42" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource42" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label43" runat="server" Text="熊本県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView43" runat="server" DataSourceID="SqlDataSource43" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource43" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label44" runat="server" Text="大分県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView44" runat="server" DataSourceID="SqlDataSource44" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource44" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label45" runat="server" Text="宮崎県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView45" runat="server" DataSourceID="SqlDataSource45" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource45" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label46" runat="server" Text="鹿児島県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView46" runat="server" DataSourceID="SqlDataSource46" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource46" runat="server"></asp:SqlDataSource>
	</p>
	<p>
	<asp:Label ID="label47" runat="server" Text="沖縄県" Font-Bold="True" Visible=False />
	<asp:GridView ID="GridView47" runat="server" DataSourceID="SqlDataSource47" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" Visible=False>
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" Headertext="事業部" />
			<asp:BoundField DataField="商品コード" HeaderText="商品コード" />
			<asp:BoundField DataField="商品名" HeaderText="商品名" />
			<asp:BoundField DataField="数量" HeaderText="数量" />
			<asp:BoundField DataField="金額" DataFormatString="{0:c}" HeaderText="金額" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource47" runat="server"></asp:SqlDataSource>
	</p>
</asp:Content>
