<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="41Campaign.aspx.vb" Inherits="Distributor_Distributor" title="営業報奨企画" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="2016 年5 月　営業報奨企画" Font-Italic="True" 
            Width="500px"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" Text="更新" />
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="＜ 新規獲得 ～ ＯＮＥ ＦＯＲ ＡＬＬ ～ ＞" style="color: #FF0000"></asp:Label>
        <br /> 支店所属の社員（支店長除く）を対象にした個人戦の報奨企画です。バリューパック（新規、新開、他社）販売本数の順位に応
        <br /> じて、最高で￥70,000 相当の報奨を8 月に支給します。
        <br /> 条件
        <br /> ・対象者：支店所属員（支店長除く）
        <br /> ・対象期間内の売上分
        <br /> ※同数の場合は売上金額の高額順で順位を決める
        
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="支店名" HeaderText="支店名" />
                <asp:BoundField DataField="担当者" HeaderText="担当者" />
                <asp:BoundField DataField="本数" HeaderText="本数" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="Gainsboro" />
        </asp:GridView>
    </p>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <p style="height: 66px">
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="＜ 自社リプレース獲得 ～ ＡＬＬ ＦＯＲ ＯＮＥ ～ ＞" style="color: #FF0000"></asp:Label>
            <br /> ※インスト、サポートの社員を対象にしたチーム戦の報奨企画です。自社リプレースのバリューパック販売本数に応じて、一人
            <br /> 当たり最高で￥20,000 の報奨を8月に支給します。
            <br /> 条件
                        
            </p>
    <p style="height: 66px">
        ・チーム数：12 チーム（対象者はインスト・サポート・サポーター）
            <br /> ・エントリー条件：本企画向けに設定した自社リプレースのバリューパック目標販売数を達成すること
            <br /> ・目標販売数を上回った分から報奨対象とし、8 月売上の受注残も対象
            <br /> ・順位は平等に行うため、ポイントの合計数で決定
            <br />   ※QURIA パックは一件を0.5 本で計算
            <br />   ※同ポイントの場合は売上金額の高額順で順位を決める</p>
    <p style="height: 34px">
        &nbsp;</p>
    <p>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource2" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="支店名" HeaderText="支店名" />
                <asp:BoundField DataField="41目標数" HeaderText="目標数" />
                <asp:BoundField DataField="本数" 
                    HeaderText="本数" />
                <asp:BoundField DataField="ポイント" HeaderText="ポイント" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    </p>
</asp:Content>

