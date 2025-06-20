<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="OnlineInquiry.aspx.vb" Inherits="OnlineInquiry_OnlineInquiry" title="オンライン資格問い合わせフォーム" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True"
            ForeColor="#3333FF"></asp:Label>
        <asp:Label ID="labelTitle" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  オンライン資格問い合わせフォーム" Font-Italic="True" 
            Width="500px"></asp:Label>
        <br />
    </p>
    <p>
        <asp:Label ID="label拠点名" runat="server" Text="拠点名"></asp:Label>
		<asp:DropDownList ID="dropDownList拠点名" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">すべて</asp:ListItem>
            <asp:ListItem Value="札幌">札幌</asp:ListItem>
            <asp:ListItem Value="仙台">仙台</asp:ListItem>
            <asp:ListItem Value="さいたま">さいたま</asp:ListItem>
            <asp:ListItem Value="横浜">横浜</asp:ListItem>
            <asp:ListItem Value="首都圏">首都圏</asp:ListItem>
            <asp:ListItem Value="名古屋">名古屋</asp:ListItem>
            <asp:ListItem Value="金沢">金沢</asp:ListItem>
            <asp:ListItem Value="大阪">大阪</asp:ListItem>
            <asp:ListItem Value="広島">広島</asp:ListItem>
            <asp:ListItem Value="福岡">福岡</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="buttonClear" runat="server" Text="クリア" />
    <p>
        <asp:Label ID="label検索結果" runat="server" Font-Bold="True"></asp:Label>
        </p>
    <p>
        <asp:GridView ID="GridViewOnlineInquiry" runat="server" AutoGenerateColumns="False"
            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
            CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSourceOnlineInquiry" Font-Size="Small"
            ShowFooter="False" AllowSorting="true">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField DataField="拠点名" HeaderText="拠点名" SortExpression="拠点名" />
                <asp:BoundField DataField="顧客No" HeaderText="顧客No" SortExpression="顧客No" />
                <asp:BoundField DataField="MWS_ID" HeaderText="MWS_ID" SortExpression="MWS_ID" />
                <asp:BoundField DataField="Q1" HeaderText="Q1:導入開始時期" />
                <asp:BoundField DataField="Q2" HeaderText="Q2:ユーザー登録" />
                <asp:BoundField DataField="Q3" HeaderText="Q3:カードリーダー申込" />
                <asp:BoundField DataField="Q4" HeaderText="Q4:インターネット回線" />
                <asp:BoundField DataField="Q5" HeaderText="Q5:連絡先" />
                <asp:BoundField DataField="問い合わせ日時" HeaderText="問い合わせ日時" />
                <asp:BoundField DataField="拠点ID" Visible="False" />
            </Columns>
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceOnlineInquiry" runat="server"
            ConnectionString="<%$ ConnectionStrings:CharlieDBConnectionString %>"
            SelectCommand="SELECT U.支店ＩＤ as 拠点ID, U.支店名 as 拠点名, U.顧客ＩＤ as 顧客No, U.顧客名１ + U.顧客名２ as 顧客名, C.CP_ID as MWS_ID, case C.ans_1 when 1 then 'なるべく早めに導入を検討している ' when 2 then '急いではいないが導入を検討している ' when 3 then '導入時期は未定だが検討している ' else '導入しない ' end as Q1, case C.ans_2 when 1 then '未登録 ' else '登録済み ' end as Q2, iif(ISNUMERIC(C.ans_3)=0, REPLACE(C.ans_3, '1;', '申込時期：'), iif(C.ans_3 = 1, '未申込', '到着済み')) as Q3, iif(ISNUMERIC(C.ans_4)=0, REPLACE(C.ans_4, '2;', 'それ以外：'), 'NTTフレッツ光ネクスト') as Q4, iif(ISNUMERIC(C.ans_5)=0, REPLACE(C.ans_5, '2;', '連絡先〈医院名・TEL〉：'), '必要ない ') as Q5, FORMAT(C.create_date, 'yyyy/MM/dd HH:mm') as 問い合わせ日時 FROM charlieDB.dbo.T_COUPLER_ENQ_ANSER as C LEFT JOIN charlieDB.dbo.T_PRODUCT_CONTROL as PC on C.CP_ID = PC.PRODUCT_ID LEFT JOIN charlieDB.dbo.顧客マスタ参照ビュー as U on PC.CUSTOMER_ID = U.顧客ＩＤ WHERE C.enq_id = 11 AND C.CP_ID <> '' AND U.支店名 LIKE '%' + @拠点名 + '%' ORDER BY U.顧客ＩＤ">
            <SelectParameters>
                <asp:ControlParameter ControlID="dropDownList拠点名" Name="拠点名"
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

