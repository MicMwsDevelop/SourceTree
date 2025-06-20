<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="ScheduleDecide.aspx.vb" Inherits="schedule_ScheduleDecide" title="案件担当決定" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="Label" runat="server" Font-Bold="True" Font-Size="Large" 
        Text="作業担当未決定一覧"></asp:Label>
    <br />
    <br />
    <asp:DropDownList ID="D_担当支店" runat="server" AutoPostBack="True">
        <asp:ListItem Value="%">すべて</asp:ListItem>
        <asp:ListItem>札幌支店</asp:ListItem>
        <asp:ListItem>仙台支店</asp:ListItem>
        <asp:ListItem>首都圏営業部</asp:ListItem>
        <asp:ListItem>名古屋支店</asp:ListItem>
        <asp:ListItem>大阪支店</asp:ListItem>
        <asp:ListItem>金沢営業所</asp:ListItem>
        <asp:ListItem>広島営業所</asp:ListItem>
        <asp:ListItem>福岡支店</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Button ID="B_Back" runat="server" Text="戻る" />
    <br />
    <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataKeyNames="ID" DataSourceID="SqlDataSource1" 
        Font-Size="Small">
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <RowStyle BackColor="White" ForeColor="#330099" />
        <Columns>
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" Visible="False" />
            <asp:BoundField DataField="顧客No" HeaderText="顧客No" ReadOnly="True" 
                SortExpression="顧客No" />
            <asp:BoundField DataField="顧客名" HeaderText="顧客名" ReadOnly="True" 
                SortExpression="顧客名" />
            <asp:BoundField DataField="担当部署コード" HeaderText="担当部署コード" 
                SortExpression="担当部署コード" Visible="False" />
            <asp:BoundField DataField="担当部署名" HeaderText="担当部署名" ReadOnly="True" 
                SortExpression="担当部署名" />
            <asp:BoundField DataField="登録担当ID" HeaderText="登録担当ID" SortExpression="登録担当ID" 
                Visible="False" />
            <asp:BoundField DataField="登録担当者" HeaderText="登録担当者" ReadOnly="True" 
                SortExpression="登録担当者" />
            <asp:BoundField DataField="作業担当ID" HeaderText="作業担当ID" SortExpression="作業担当ID" 
                Visible="False" />
            <asp:TemplateField HeaderText="作業担当者" SortExpression="作業担当者">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="fUsrName" 
                        DataValueField="fUsrName" AppendDataBoundItems="True" 
                        SelectedValue='<%# Bind("作業担当者") %>'>
                        <asp:ListItem Value="未決定">未決定</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:JunpDBConnectionString %>" 
                        SelectCommand="SELECT [fUsrName], [fUsrID] FROM [vMic担当者] WHERE (([fBshCode2] = @fBshCode2) AND ([fPca部門コード] = @fBshCode3)) ORDER BY [fUsrID]">
                        <SelectParameters>
                            <asp:SessionParameter Name="fBshCode2" SessionField="LOGINBUSHO2" 
                                Type="String" />
                            <asp:SessionParameter Name="fBshCode3" SessionField="LOGINBUSHO3" 
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("作業担当者") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="登録日" DataFormatString="{0:yyyy/MM/dd}" 
                HeaderText="登録日" ReadOnly="True" SortExpression="登録日" />
            <asp:TemplateField HeaderText="作業予定日" SortExpression="作業予定日">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" 
                        Text='<%# Bind("作業予定日", "{0:yyyy/MM/dd}") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" 
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
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("作業方法") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="備考" SortExpression="備考">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("備考") %>' 
                        TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("備考") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="終了フラグ" HeaderText="終了フラグ" SortExpression="終了フラグ" 
                Visible="False" />
        </Columns>
        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
        DeleteCommand="DELETE FROM [schedule] WHERE [ID] = @original_ID AND [顧客No] = @original_顧客No AND [顧客名] = @original_顧客名 AND [担当部署コード] = @original_担当部署コード AND [担当部署名] = @original_担当部署名 AND [登録担当ID] = @original_登録担当ID AND [登録担当者] = @original_登録担当者 AND [作業担当ID] = @original_作業担当ID AND [作業担当者] = @original_作業担当者 AND [登録日] = @original_登録日 AND [作業予定日] = @original_作業予定日 AND [作業方法] = @original_作業方法 AND [備考] = @original_備考 AND [終了フラグ] = @original_終了フラグ" 
        InsertCommand="INSERT INTO [schedule] ([顧客No], [顧客名], [担当部署コード], [担当部署名], [登録担当ID], [登録担当者], [作業担当ID], [作業担当者], [登録日], [作業予定日], [作業方法], [備考], [終了フラグ]) VALUES (@顧客No, @顧客名, @担当部署コード, @担当部署名, @登録担当ID, @登録担当者, @作業担当ID, @作業担当者, @登録日, @作業予定日, @作業方法, @備考, @終了フラグ)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT [ID], [顧客No], [顧客名], [担当部署コード], [担当部署名], [登録担当ID], [登録担当者], [作業担当ID], [作業担当者], [登録日], [作業予定日], [作業方法], [備考], [終了フラグ] FROM [schedule] WHERE (([作業担当者] = @作業担当者) AND ([担当部署名] LIKE '%' + @担当部署名 + '%')) ORDER BY [作業予定日]" 
        
        UpdateCommand="UPDATE [schedule] SET [作業担当者] = @作業担当者, [作業予定日] = @作業予定日, [作業方法] = @作業方法, [備考] = @備考 WHERE [ID] = @original_ID AND ([作業担当者] IS NULL OR [作業担当者] = @original_作業担当者) AND ([作業予定日] IS NULL OR [作業予定日] = @original_作業予定日) AND ([作業方法] IS NULL OR [作業方法] = @original_作業方法) AND ([備考] IS NULL OR [備考] = @original_備考)">
        <SelectParameters>
            <asp:Parameter DefaultValue="未決定" Name="作業担当者" Type="String" />
            <asp:ControlParameter ControlID="D_担当支店" Name="担当部署名" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_顧客No" Type="Int32" />
            <asp:Parameter Name="original_顧客名" Type="String" />
            <asp:Parameter Name="original_担当部署コード" Type="String" />
            <asp:Parameter Name="original_担当部署名" Type="String" />
            <asp:Parameter Name="original_登録担当ID" Type="String" />
            <asp:Parameter Name="original_登録担当者" Type="String" />
            <asp:Parameter Name="original_作業担当ID" Type="String" />
            <asp:Parameter Name="original_作業担当者" Type="String" />
            <asp:Parameter Name="original_登録日" Type="DateTime" />
            <asp:Parameter Name="original_作業予定日" Type="DateTime" />
            <asp:Parameter Name="original_作業方法" Type="String" />
            <asp:Parameter Name="original_備考" Type="String" />
            <asp:Parameter Name="original_終了フラグ" Type="Boolean" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="顧客No" Type="Int32" />
            <asp:Parameter Name="顧客名" Type="String" />
            <asp:Parameter Name="担当部署コード" Type="String" />
            <asp:Parameter Name="担当部署名" Type="String" />
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
            <asp:Parameter Name="original_担当部署コード" Type="String" />
            <asp:Parameter Name="original_担当部署名" Type="String" />
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
            <asp:Parameter Name="担当部署コード" Type="String" />
            <asp:Parameter Name="担当部署名" Type="String" />
            <asp:Parameter Name="登録担当ID" Type="String" />
            <asp:Parameter Name="登録担当者" Type="String" />
            <asp:Parameter Name="作業担当ID" Type="String" />
            <asp:Parameter Name="作業担当者" Type="String" />
            <asp:Parameter Name="登録日" Type="DateTime" />
            <asp:Parameter Name="作業予定日" Type="DateTime" />
            <asp:Parameter Name="作業方法" Type="String" />
            <asp:Parameter Name="備考" Type="String" />
            <asp:Parameter Name="終了フラグ" Type="Boolean" />
        </InsertParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

