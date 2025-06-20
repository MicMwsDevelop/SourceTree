<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Eset.aspx.vb" Inherits="Eset_Eset" title="ESET契約状態検索" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
	<p>
		<asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
		<br />
			<asp:Label ID="labelESET契約状態検索" runat="server" BackColor="#666666" Font-Bold="True" 
			Font-Size="XX-Large" ForeColor="White" Text="  ESET契約状態検索" Font-Italic="True" Width="500px"></asp:Label>
		<br />
		&nbsp;
		<br />
		<asp:Label ID="label顧客No" runat="server" Text="顧客No"></asp:Label>
		<asp:TextBox ID="textBox顧客No" runat="server" Width="87px" ToolTip="YYYY/MM/DD"></asp:TextBox>
		<asp:Button ID="button検索" runat="server" Text="検索" />
		<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
				ControlToValidate="textBox顧客No" ErrorMessage="顧客No入力に誤りがあります。(数字８桁)" 
				ValidationExpression="\d{8}">※</asp:RegularExpressionValidator>
				&nbsp;
		<asp:ValidationSummary ID="ValidationSummary1" runat="server" />

		<asp:Label ID="label顧客名" runat="server" Text="顧客名"></asp:Label>
		<asp:TextBox ID="textBox顧客名" runat="server" Width="300px" ReadOnly="true" ></asp:TextBox>
	</p>
		<p> 1.「月額版契約状態」の表示があり、状態が「利用中」となっている場合は、月額版が有効<br>
		    2.「6年パック受注伝票」の表示がある場合は、「サービス利用開始」「サービス利用終了」の日付、<br>
		    　「売上承認日」からの経過を見て、6年パックが有効かどうか判断する<br>
		    ※「6年パック受注伝票」には、ESETの商品コードが含まれる伝票がすべて表示される</p>
	<p>
		<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Text="■ESET月額版契約状態"></asp:Label>
		<asp:GridView ID="GridViewMonthly" runat="server" DataSourceID="SqlDataSourceMonthly" Font-Size="Small"
				AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
				BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="False" >
			<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
			<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
			<Columns>
				<asp:BoundField DataField="サービス名" HeaderText="サービス名" />
				<asp:BoundField DataField="シリアル番号" HeaderText="シリアル番号" />
				<asp:BoundField DataField="ユーザー名" HeaderText="ユーザー名" />
				<asp:BoundField DataField="パスワード" HeaderText="パスワード" />
				<asp:BoundField DataField="製品認証キー" HeaderText="製品認証キー" />
				<asp:BoundField DataField="利用開始日時" HeaderText="利用開始日時" />
				<asp:BoundField DataField="利用終了日時" HeaderText="利用終了日時" />
				<asp:BoundField DataField="状態" HeaderText="状態" />
			</Columns>
			<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
			<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
			<HeaderStyle BackColor="#fff9b8" Font-Bold="True" ForeColor="Black" />
		</asp:GridView>
	</p>
	<asp:SqlDataSource ID="SqlDataSourceMonthly" runat="server"></asp:SqlDataSource>
	<p>
		<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="■ESET６年パック受注伝票"></asp:Label>
		<asp:GridView ID="GridViewPack" runat="server" DataSourceID="SqlDataSourcePack" Font-Size="Small"
				AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
				BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="False" >
			<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
			<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
			<Columns>
				<asp:BoundField DataField="受注番号" HeaderText="受注番号" />
				<asp:BoundField DataField="販売種別" HeaderText="販売種別" />
				<asp:BoundField DataField="金額" HeaderText="金額" />
				<asp:BoundField DataField="数量" HeaderText="数量" />
				<asp:BoundField DataField="売上承認日" HeaderText="売上承認日" />
				<asp:BoundField DataField="サービス利用開始" HeaderText="サービス利用開始" />
				<asp:BoundField DataField="サービス利用終了" HeaderText="サービス利用終了" />
				<asp:BoundField DataField="備考" HeaderText="備考" />
				<asp:BoundField DataField="件名" HeaderText="件名" />
			</Columns>
			<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
			<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
			<HeaderStyle BackColor="#fff9b8" Font-Bold="True" ForeColor="Black" />
		</asp:GridView>
	</p>
	<asp:SqlDataSource ID="SqlDataSourcePack" runat="server"></asp:SqlDataSource>
</asp:Content>

