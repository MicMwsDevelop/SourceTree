<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="42Campaign.aspx.vb" Inherits="Distributor_Distributor" title="営業報奨企画" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="42期上期社内報賞企画" Font-Italic="True" 
            Width="500px"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" Text="更新" />
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="＜ 新規販売（個人戦） ＞" style="color: #FF0000"></asp:Label>
        <br /> 条件
        <br /> ・バリューパック新規(新規、新開、他社）3ヵ月で5本以上売上
        <br /> ・対象者：支店所属員（支店長除く）
        <br /> ・対象期間：2016/10～12月
        <br /> ※同数の場合は売上金額の高額順で順位を決める
        
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
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
    <p style="height: 172px">
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="＜ 自社リプレース（チーム戦） ＞" style="color: #FF0000"></asp:Label>                      
            <br />条件 <br />・チーム数：12 チーム（対象者はインスト・サポート）
            <br /> ・エントリー条件：目標の70%を達成すること
            <br /> ・対象期間：2016/10～12月
            <br /> ・12月起票1月売上の受注残も対象とする
            <br /> ・順位は目標の達成率で判定する
            <br /> ・販売区分：自社リプレース（バリューパック）　  ※QURIAパックの販売はVPの1/2で計算する
            <br />   ※同ポイントの場合は売上金額の高額順で順位を決める</p>
            <br />   ※下記の本数は、起票ベースでカウントしています</p>
    </p>
    <p>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource2" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="支店名" HeaderText="支店名" />
                <asp:BoundField DataField="目標数" HeaderText="目標数" />
                <asp:BoundField DataField="エントリーライン" HeaderText="エントリーライン" />
                <asp:BoundField DataField="本数" HeaderText="本数" />
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

