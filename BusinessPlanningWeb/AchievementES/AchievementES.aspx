<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="AchievementES.aspx.vb" Inherits="AchievementES_AchievementES" title="新規獲得個人販売実績" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
	<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
	<asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF">
	</asp:Label>
	<br />
	<asp:Label ID="label販売実績" runat="server" BackColor="#666666" Font-Bold="True" 
		Font-Size="XX-Large" ForeColor="White" Text="  新規獲得個人販売実績" Font-Italic="True" 
		Width="500px">
	</asp:Label>
	<br />
	&nbsp;
	<asp:Label ID="labelエリア" runat="server" Text="エリア">
	</asp:Label>
	<asp:DropDownList ID="dropListエリア" runat="server" AutoPostBack="False">
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
	<asp:Label ID="label集計期間" runat="server" Text="集計期間"></asp:Label>
	<asp:TextBox ID="textBox開始日" runat="server" Width="87px" ToolTip="YYYY/MM/DD"></asp:TextBox>
	<asp:Label ID="LabelKara" runat="server" Text="～"></asp:Label>
	<asp:TextBox ID="textBox終了日" runat="server" Width="87px" ToolTip="YYYY/MM/DD"></asp:TextBox>
	<asp:Button ID="button集計実行" runat="server" Text="集計実行" />
	&nbsp;
	<asp:Button ID="buttonOutputExcel" runat="server" Text="EXCEL出力" />
	<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
		ControlToValidate="textBox開始日" ErrorMessage="開始日の入力に誤りがあります。(YYYY/MM/DD)" 
		ValidationExpression="\d{4}/\d{2}/\d{2}">※
	</asp:RegularExpressionValidator>
	<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
		ControlToValidate="textBox終了日" ErrorMessage="終了日の入力に誤りがあります。(YYYY/MM/DD)" 
		ValidationExpression="\d{4}/\d{2}/\d{2}">※
	</asp:RegularExpressionValidator>
	</p>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" />
	<p>
	<asp:GridView ID="GridViewES" runat="server" DataSourceID="SqlDataSource" 
		AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
		BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="True" >
		<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
		<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
		<Columns>
			<asp:BoundField DataField="営業部名" HeaderText="営業部/事業部" />
			<asp:BoundField DataField="拠点名" HeaderText="担当部署" />
			<asp:BoundField DataField="担当者名" HeaderText="担当者" />
			<asp:BoundField DataField="ES本数" HeaderText="ES本数" />
			<asp:BoundField DataField="課金本数" HeaderText="課金本数" />
			<asp:BoundField DataField="まとめ本数" HeaderText="まとめ本数" />
			<asp:BoundField DataField="合計本数" HeaderText="合計本数" />
			<asp:BoundField DataField="営業部コード" Visible="False" />
			<asp:BoundField DataField="拠点コード" Visible="False" />
			<asp:BoundField DataField="担当者コード" Visible="False" />
		</Columns>
		<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
		<AlternatingRowStyle BackColor="Gainsboro" />
	</asp:GridView>
	</p>
	<asp:SqlDataSource ID="SqlDataSource" runat="server">
	</asp:SqlDataSource>
</asp:Content>
