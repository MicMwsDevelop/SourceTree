<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="42_2Campaign.aspx.vb" Inherits="Distributor_Distributor" title="営業報奨企画" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            color: #3333CC;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="４２期第４四半期 社内キャンペーン" Font-Italic="True" 
            Width="500px"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" Text="更新" />
    </p>
    <p style="width: 660px">
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="＜ チーム戦（月） ＞" style="color: #FF0000"></asp:Label>
        <br /> 条件
        <br /> ・対象期間：2017/5～7月
        <br /> ・対象：12チーム
        <br /> ・目標値:42期の毎月の自社リプレースの目標値
        <br /> ・対象販売区分：バリューパックおよびQURIAパック（自社リプレース、新規獲得のすべて）
        <br /> ・エントリー条件：目標の70%を達成すること（調整あり）
        <br /> ・達成率ポイント：人数/（エントリー超過）本数
        <br /> ・順位は目標の達成率ポイントで判定する
        <br /> ・同ポイントの場合は売上金額の高額順で順位を決める
        <br /> ・毎月〆後に発表し、月毎に表彰
        
        
    </p>
                                        <p style="width: 660px; font-weight: 700; color: #0000FF;">
                                            &lt;2017年7月&gt;</p>
                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource4" GridLines="Vertical" style="font-weight: 700">
                                            <Columns>
                                                <asp:BoundField DataField="支店名" HeaderText="支店名" />
                                                <asp:BoundField DataField="目標数" HeaderText="目標数" />
                                                <asp:BoundField DataField="エントリーライン" HeaderText="エントリーライン" />
                                                <asp:BoundField DataField="本数" HeaderText="本数" />
                                                <asp:BoundField DataField="ポイント" HeaderText="ポイント" />
                                            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server"></asp:SqlDataSource>
    <p class="style1">
        &lt;2017年6月&gt;<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource3" GridLines="Vertical">
            <Columns>
                <asp:BoundField DataField="支店名" HeaderText="支店名" />
                <asp:BoundField DataField="目標数" HeaderText="目標数" />
                <asp:BoundField DataField="エントリーライン" HeaderText="エントリーライン" />
                <asp:BoundField DataField="本数" HeaderText="本数" />
                <asp:BoundField DataField="ポイント" HeaderText="ポイント" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
                                        
    <asp:SqlDataSource ID="SqlDataSource3" runat="server"></asp:SqlDataSource>
    <p class="style1">
        
        &lt;2017年5月&gt;<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
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
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <p style="height: 131px">
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="＜ チーム戦（四半期） ＞" style="color: #FF0000"></asp:Label>                      
            <br />条件 
            <br /> ・対象期間：2017/05～07
            <br /> ・対象：チーム
            <br /> ・エントリー数は『エントリー/第４』を利用する以外は（月）と同じ
            <br /> ・7月起票8月売上の受注残案件も対象とする(注文書有9月納品も可　※要報告相談)
            <br /> ・四半期での１位～３位&nbsp;
    </p>
    <p style="height: 30px" class="style1">
        納品月:2017/05/01～2017/08/31</p>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
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
            <AlternatingRowStyle BackColor="Gainsboro" />
        </asp:GridView>
    <p>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    </p>
</asp:Content>

