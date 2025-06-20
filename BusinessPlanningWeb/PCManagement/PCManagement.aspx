<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="PCManagement.aspx.vb" Inherits="schedule_scheduleMenu" title="ScheduleMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Button ID="Button1" runat="server" Text="新規スケジュール登録" />
        　<asp:Button ID="Button3" runat="server" Text="新規ハード登録" />
        &nbsp;&nbsp;&nbsp; 
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="作業予定一覧"></asp:Label>
    </p>
    <p>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
            DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
            ForeColor="#003399" Height="391px" Width="635px">
            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
            <WeekendDayStyle BackColor="#CCCCFF" />
            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
        </asp:Calendar>
    </p>
    <p>
        <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
        <asp:GridView ID="GridView2" runat="server" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            DataSourceID="SqlDataSource2" ForeColor="Black" GridLines="Horizontal">
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource1" 
            GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                <asp:BoundField DataField="AutoNo" HeaderText="案件No" InsertVisible="False" 
                    SortExpression="AutoNo" />
                <asp:BoundField DataField="案件名" HeaderText="案件名" SortExpression="案件名" />
                <asp:BoundField DataField="開始日" HeaderText="開始日" SortExpression="開始日" />
                <asp:BoundField DataField="終了日" HeaderText="終了日" SortExpression="終了日" />
                <asp:TemplateField HeaderText="備考" SortExpression="備考">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("備考") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("備考") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="登録者" HeaderText="登録者" SortExpression="登録者" />
                <asp:ButtonField ButtonType="Button" CommandName="削除" HeaderText="削除" 
                    Text="削除" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
        <asp:Label ID="L_基準" runat="server" Font-Bold="True" ForeColor="#0033CC" 
            Text="Label"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="以降のデータを表示しています"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            OldValuesParameterFormatString="original_{0}" 
            
            
            
            
            SelectCommand="SELECT [AutoNo], [案件名], [開始日], [終了日], [備考], [登録者] FROM [PCManagement_Header] WHERE (([出力フラグ] = @出力フラグ) AND ([開始日] &gt;= @開始日))">
            <SelectParameters>
                <asp:Parameter DefaultValue="True" Name="出力フラグ" Type="Boolean" />
                <asp:ControlParameter ControlID="L_基準" Name="開始日" PropertyName="Text" 
                    Type="String" />
            </SelectParameters>
            
        </asp:SqlDataSource>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

