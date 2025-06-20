<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="ScheduleMenu.aspx.vb" Inherits="schedule_scheduleMenu" title="ScheduleMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Size="Large" 
            ForeColor="#3333CC" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" Text="新規登録" />
        　<asp:Button ID="Button3" runat="server" Text="作業者未決定一覧" />
        &nbsp;&nbsp;&nbsp; <asp:Button ID="Button2" runat="server" Text="登録データ一覧" />
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="作業予定一覧"></asp:Label>
    </p>
    <p>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
            DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
            ForeColor="#003399" Height="300px" Width="330px">
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
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataKeyNames="ID" DataSourceID="SqlDataSource1" 
            GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="顧客No" HeaderText="顧客No" SortExpression="顧客No" 
                    ReadOnly="True" />
                <asp:BoundField DataField="顧客名" HeaderText="顧客名" SortExpression="顧客名" 
                    ReadOnly="True" />
                <asp:BoundField DataField="登録日" DataFormatString="{0:yyyy/MM/dd}" 
                    HeaderText="登録日" SortExpression="登録日" ReadOnly="True" />
                <asp:TemplateField HeaderText="作業予定日" SortExpression="作業予定日">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" 
                            Text='<%# Bind("作業予定日", "{0:yyyy/MM/dd}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# Bind("作業予定日", "{0:yyyy/MM/dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="作業方法" SortExpression="作業方法">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                            SelectedValue='<%# Bind("作業方法") %>'>
                            <asp:ListItem>訪問対応</asp:ListItem>
                            <asp:ListItem>電話対応</asp:ListItem>
                            <asp:ListItem>その他</asp:ListItem>
                            <asp:ListItem>未定</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("作業方法") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="登録担当ID" 
                    HeaderText="登録担当ID" SortExpression="登録担当ID" ReadOnly="True" 
                    Visible="False" />
                <asp:BoundField DataField="登録担当者" HeaderText="登録担当者" SortExpression="登録担当者" 
                    ReadOnly="True" />
                <asp:BoundField DataField="作業担当ID" HeaderText="作業担当ID" SortExpression="作業担当ID" 
                    Visible="False" ReadOnly="True" />
                <asp:BoundField DataField="作業担当者" HeaderText="作業担当者" SortExpression="作業担当者" 
                    ReadOnly="True" />
                <asp:TemplateField HeaderText="備考">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("備考") %>' 
                            TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("備考") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="終了フラグ" SortExpression="終了フラグ">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("終了フラグ") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("終了フラグ") %>' 
                            Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            OldValuesParameterFormatString="original_{0}" 
            
            
            SelectCommand="SELECT [ID], [顧客No], [顧客名], [登録担当ID], [登録担当者], [作業担当ID], [作業担当者], [登録日], [作業予定日], [作業方法],[備考], [終了フラグ] FROM [schedule] WHERE (([終了フラグ] = @終了フラグ) AND ([作業担当者] = @作業担当者)) ORDER BY [作業予定日]" 
            ConflictDetection="CompareAllValues" 
            DeleteCommand="DELETE FROM [schedule] WHERE [ID] = @original_ID AND [顧客No] = @original_顧客No AND [顧客名] = @original_顧客名 AND [登録担当ID] = @original_登録担当ID AND [登録担当者] = @original_登録担当者 AND [作業担当ID] = @original_作業担当ID AND [作業担当者] = @original_作業担当者 AND [登録日] = @original_登録日 AND [作業予定日] = @original_作業予定日 AND [作業方法] = @original_作業方法 AND [終了フラグ] = @original_終了フラグ" 
            InsertCommand="INSERT INTO [schedule] ([顧客No], [顧客名], [登録担当ID], [登録担当者], [作業担当ID], [作業担当者], [登録日], [作業予定日], [作業方法], [終了フラグ]) VALUES (@顧客No, @顧客名, @登録担当ID, @登録担当者, @作業担当ID, @作業担当者, @登録日, @作業予定日, @作業方法, @終了フラグ)" 
            UpdateCommand="UPDATE [schedule] SET [作業予定日] = @作業予定日, [作業方法] = @作業方法, [備考] = @備考, [終了フラグ] = @終了フラグ WHERE [ID] = @original_ID AND ([作業予定日] IS NULL OR [作業予定日] = @original_作業予定日) AND ([作業方法] IS NULL OR [作業方法] = @original_作業方法) AND ([備考] IS NULL OR [備考] = @original_備考) AND ([終了フラグ] IS NULL OR [終了フラグ] = @original_終了フラグ)">
            
            <SelectParameters>
                <asp:Parameter DefaultValue="false" Name="終了フラグ" Type="Boolean" />
                <asp:SessionParameter Name="作業担当者" SessionField="VISITORNAME" Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_顧客No" Type="Int32" />
                <asp:Parameter Name="original_顧客名" Type="String" />
                <asp:Parameter Name="original_登録担当ID" Type="String" />
                <asp:Parameter Name="original_登録担当者" Type="String" />
                <asp:Parameter Name="original_作業担当ID" Type="String" />
                <asp:Parameter Name="original_作業担当者" Type="String" />
                <asp:Parameter Name="original_登録日" Type="DateTime" />
                <asp:Parameter Name="original_作業予定日" Type="DateTime" />
                <asp:Parameter Name="original_作業方法" Type="String" />
                <asp:Parameter Name="original_終了フラグ" Type="Boolean" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="顧客No" Type="Int32" />
                <asp:Parameter Name="顧客名" Type="String" />
                <asp:Parameter Name="登録担当ID" Type="String" />
                <asp:Parameter Name="登録担当者" Type="String" />
                <asp:Parameter Name="作業担当ID" Type="String" />
                <asp:Parameter Name="作業担当者" Type="String" />
                <asp:Parameter Name="登録日" Type="DateTime" />
                <asp:Parameter Name="作業予定日" Type="DateTime" />
                <asp:Parameter Name="作業方法" Type="String" />
                <asp:Parameter Name="備考" Type="String" />
                <asp:Parameter Name="終了フラグ" Type="Boolean" />
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_顧客No" Type="Int32" />
                <asp:Parameter Name="original_顧客名" Type="String" />
                <asp:Parameter Name="original_登録担当ID" Type="String" />
                <asp:Parameter Name="original_登録担当者" Type="String" />
                <asp:Parameter Name="original_作業担当ID" Type="String" />
                <asp:Parameter Name="original_作業担当者" Type="String" />
                <asp:Parameter Name="original_登録日" Type="DateTime" />
                <asp:Parameter Name="original_作業予定日" Type="DateTime" />
                <asp:Parameter Name="original_作業方法" Type="String" />
                <asp:Parameter Name="original_備考" Type="String" />
                <asp:Parameter Name="original_終了フラグ" Type="Boolean" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="顧客No" Type="Int32" />
                <asp:Parameter Name="顧客名" Type="String" />
                <asp:Parameter Name="登録担当ID" Type="String" />
                <asp:Parameter Name="登録担当者" Type="String" />
                <asp:Parameter Name="作業担当ID" Type="String" />
                <asp:Parameter Name="作業担当者" Type="String" />
                <asp:Parameter Name="登録日" Type="DateTime" />
                <asp:Parameter Name="作業予定日" Type="DateTime" />
                <asp:Parameter Name="作業方法" Type="String" />
                <asp:Parameter Name="終了フラグ" Type="Boolean" />
            </InsertParameters>
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

