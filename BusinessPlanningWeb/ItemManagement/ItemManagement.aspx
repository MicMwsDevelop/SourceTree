<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="ItemManagement.aspx.vb" Inherits="ItemManagement" title="案件管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Text="部署"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>札幌支店</asp:ListItem>
            <asp:ListItem>仙台支店</asp:ListItem>
            <asp:ListItem>首都圏営業部 営業１課</asp:ListItem>
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="氏名"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="新規登録" />
&nbsp;&nbsp;&nbsp;
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID" DataSourceID="SqlDataSource1" CellPadding="4" 
            Font-Size="Small" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" Visible="False" />
                <asp:BoundField DataField="登録月" HeaderText="年月" />
                <asp:BoundField DataField="医院名" HeaderText="医院名" SortExpression="医院名" 
                    ReadOnly="True" />
                <asp:BoundField DataField="担当者" HeaderText="担当者" SortExpression="担当者" />
                <asp:TemplateField HeaderText="ステータス" SortExpression="ステータス">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList4" runat="server" 
                            SelectedValue='<%# Bind("ステータス") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                            <asp:ListItem>C</asp:ListItem>
                            <asp:ListItem>D</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ステータス") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="地域" HeaderText="地域" SortExpression="地域" 
                    ReadOnly="True" />
                <asp:BoundField DataField="販売店" HeaderText="販売店" SortExpression="販売店" />
                <asp:TemplateField HeaderText="商材" SortExpression="商材">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList5" runat="server" 
                            SelectedValue='<%# Bind("商材") %>'>
                            <asp:ListItem>U-BOX</asp:ListItem>
                            <asp:ListItem>QURIA</asp:ListItem>
                            <asp:ListItem>未定</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("商材") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="金額" HeaderText="金額" SortExpression="金額" />
                <asp:TemplateField HeaderText="12月" SortExpression="12月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_12" runat="server" SelectedValue='<%# Bind("12月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("12月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="1月" SortExpression="1月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_1" runat="server" SelectedValue='<%# Bind("1月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("1月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="2月" SortExpression="2月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_2" runat="server" SelectedValue='<%# Bind("2月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("2月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="3月" SortExpression="3月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_3" runat="server" SelectedValue='<%# Bind("3月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("3月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="4月" SortExpression="4月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_4" runat="server" SelectedValue='<%# Bind("4月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("4月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="5月" SortExpression="5月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_5" runat="server" SelectedValue='<%# Bind("5月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("5月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="6月" SortExpression="6月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_6" runat="server" SelectedValue='<%# Bind("6月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("6月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="7月" SortExpression="7月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_7" runat="server" SelectedValue='<%# Bind("7月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("7月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="8月" SortExpression="8月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_8" runat="server" SelectedValue='<%# Bind("8月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("8月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="9月" SortExpression="9月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_9" runat="server" SelectedValue='<%# Bind("9月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("9月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="10月" SortExpression="10月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_10" runat="server" SelectedValue='<%# Bind("10月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("10月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="11月" SortExpression="11月">
                    <EditItemTemplate>
                        <asp:DropDownList ID="D_11" runat="server" SelectedValue='<%# Bind("11月") %>'>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>●</asp:ListItem>
                            <asp:ListItem>★</asp:ListItem>
                            <asp:ListItem>☆</asp:ListItem>
                            <asp:ListItem>×</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("11月") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="備考" HeaderText="備考" SortExpression="備考" />
                <asp:CheckBoxField DataField="Endflg" HeaderText="Endflg" 
                    SortExpression="Endflg" Visible="False" />
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConflictDetection="CompareAllValues" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            DeleteCommand="DELETE FROM [ItemManagement] WHERE [ID] = @original_ID AND [医院名] = @original_医院名 AND [担当者] = @original_担当者 AND [ステータス] = @original_ステータス AND [地域] = @original_地域 AND [販売店] = @original_販売店 AND [商材] = @original_商材 AND [金額] = @original_金額 AND [12月] = @original_column1 AND [1月] = @original_column2 AND [2月] = @original_column3 AND [3月] = @original_column4 AND [4月] = @original_column5 AND [5月] = @original_column6 AND [6月] = @original_column7 AND [7月] = @original_column8 AND [8月] = @original_column9 AND [9月] = @original_column10 AND [10月] = @original_column11 AND [11月] = @original_column12 AND [備考] = @original_備考 AND [Endflg] = @original_Endflg" 
            InsertCommand="INSERT INTO [ItemManagement] ([医院名], [担当者], [ステータス], [地域], [販売店], [商材], [金額], [12月], [1月], [2月], [3月], [4月], [5月], [6月], [7月], [8月], [9月], [10月], [11月], [備考], [Endflg]) VALUES (@医院名, @担当者, @ステータス, @地域, @販売店, @商材, @金額, @column1, @column2, @column3, @column4, @column5, @column6, @column7, @column8, @column9, @column10, @column11, @column12, @備考, @Endflg)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT * FROM [ItemManagement] ORDER BY [ID]" 
            UpdateCommand="UPDATE [ItemManagement] SET [担当者] = @担当者, [ステータス] = @ステータス, [販売店] = @販売店, [商材] = @商材, [金額] = @金額, [12月] = @12月, [1月] = @1月, [2月] = @2月, [3月] = @3月, [4月] = @4月, [5月] = @5月, [6月] = @6月, [7月] = @7月, [8月] = @8月, [9月] = @9月, [10月] = @10月, [11月] = @11月, [備考] = @備考 WHERE [ID] = @original_ID AND ([医院名] IS NULL OR [医院名] = @original_医院名) AND ([担当者] IS NULL OR [担当者] = @original_担当者) AND ([ステータス] IS NULL OR [ステータス] = @original_ステータス) AND ([地域] IS NULL OR [地域] = @original_地域) AND ([販売店] IS NULL OR [販売店] = @original_販売店) AND ([商材] IS NULL OR [商材] = @original_商材) AND ([金額] IS NULL OR [金額] = @original_金額) AND ([12月] IS NULL OR [12月] = @original_12月) AND ([1月] IS NULL OR [1月] = @original_1月) AND ([2月] IS NULL OR [2月] = @original_2月) AND ([3月] IS NULL OR [3月] = @original_3月) AND ([4月] IS NULL OR [4月] = @original_4月) AND ([5月] IS NULL OR [5月] = @original_5月) AND ([6月] IS NULL OR [6月] = @original_6月) AND ([7月] IS NULL OR [7月] = @original_7月) AND ([8月] IS NULL OR [8月] = @original_8月) AND ([9月] IS NULL OR [9月] = @original_9月) AND ([10月] IS NULL OR [10月] = @original_10月) AND ([11月] IS NULL OR [11月] = @original_11月) AND ([備考] IS NULL OR [備考] = @original_備考)">
            
            
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_医院名" Type="String" />
                <asp:Parameter Name="original_担当者" Type="String" />
                <asp:Parameter Name="original_ステータス" Type="String" />
                <asp:Parameter Name="original_地域" Type="String" />
                <asp:Parameter Name="original_販売店" Type="String" />
                <asp:Parameter Name="original_商材" Type="String" />
                <asp:Parameter Name="original_金額" Type="Decimal" />
                <asp:Parameter Name="original_column1" Type="String" />
                <asp:Parameter Name="original_column2" Type="String" />
                <asp:Parameter Name="original_column3" Type="String" />
                <asp:Parameter Name="original_column4" Type="String" />
                <asp:Parameter Name="original_column5" Type="String" />
                <asp:Parameter Name="original_column6" Type="String" />
                <asp:Parameter Name="original_column7" Type="String" />
                <asp:Parameter Name="original_column8" Type="String" />
                <asp:Parameter Name="original_column9" Type="String" />
                <asp:Parameter Name="original_column10" Type="String" />
                <asp:Parameter Name="original_column11" Type="String" />
                <asp:Parameter Name="original_column12" Type="String" />
                <asp:Parameter Name="original_備考" Type="String" />
                <asp:Parameter Name="original_Endflg" Type="Boolean" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="医院名" Type="String" />
                <asp:Parameter Name="担当者" Type="String" />
                <asp:Parameter Name="ステータス" Type="String" />
                <asp:Parameter Name="地域" Type="String" />
                <asp:Parameter Name="販売店" Type="String" />
                <asp:Parameter Name="商材" Type="String" />
                <asp:Parameter Name="金額" Type="Decimal" />
                <asp:Parameter Name="12月" Type="String" />
                <asp:Parameter Name="1月" Type="String" />
                <asp:Parameter Name="2月" Type="String" />
                <asp:Parameter Name="3月" Type="String" />
                <asp:Parameter Name="4月" Type="String" />
                <asp:Parameter Name="5月" Type="String" />
                <asp:Parameter Name="6月" Type="String" />
                <asp:Parameter Name="7月" Type="String" />
                <asp:Parameter Name="8月" Type="String" />
                <asp:Parameter Name="9月" Type="String" />
                <asp:Parameter Name="10月" Type="String" />
                <asp:Parameter Name="11月" Type="String" />
                <asp:Parameter Name="備考" Type="String" />
                <asp:Parameter Name="Endflg" Type="Boolean" />
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_医院名" Type="String" />
                <asp:Parameter Name="original_担当者" Type="String" />
                <asp:Parameter Name="original_ステータス" Type="String" />
                <asp:Parameter Name="original_地域" Type="String" />
                <asp:Parameter Name="original_販売店" Type="String" />
                <asp:Parameter Name="original_商材" Type="String" />
                <asp:Parameter Name="original_金額" Type="Decimal" />
                <asp:Parameter Name="original_12月" Type="String" />
                <asp:Parameter Name="original_1月" Type="String" />
                <asp:Parameter Name="original_2月" Type="String" />
                <asp:Parameter Name="original_3月" Type="String" />
                <asp:Parameter Name="original_4月" Type="String" />
                <asp:Parameter Name="original_5月" Type="String" />
                <asp:Parameter Name="original_6月" Type="String" />
                <asp:Parameter Name="original_7月" Type="String" />
                <asp:Parameter Name="original_8月" Type="String" />
                <asp:Parameter Name="original_9月" Type="String" />
                <asp:Parameter Name="original_10月" Type="String" />
                <asp:Parameter Name="original_11月" Type="String" />
                <asp:Parameter Name="original_備考" Type="String" />
                <asp:Parameter Name="original_Endflg" Type="Boolean" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="医院名" Type="String" />
                <asp:Parameter Name="担当者" Type="String" />
                <asp:Parameter Name="ステータス" Type="String" />
                <asp:Parameter Name="地域" Type="String" />
                <asp:Parameter Name="販売店" Type="String" />
                <asp:Parameter Name="商材" Type="String" />
                <asp:Parameter Name="金額" Type="Decimal" />
                <asp:Parameter Name="column1" Type="String" />
                <asp:Parameter Name="column2" Type="String" />
                <asp:Parameter Name="column3" Type="String" />
                <asp:Parameter Name="column4" Type="String" />
                <asp:Parameter Name="column5" Type="String" />
                <asp:Parameter Name="column6" Type="String" />
                <asp:Parameter Name="column7" Type="String" />
                <asp:Parameter Name="column8" Type="String" />
                <asp:Parameter Name="column9" Type="String" />
                <asp:Parameter Name="column10" Type="String" />
                <asp:Parameter Name="column11" Type="String" />
                <asp:Parameter Name="column12" Type="String" />
                <asp:Parameter Name="備考" Type="String" />
                <asp:Parameter Name="Endflg" Type="Boolean" />
            </InsertParameters>
        </asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
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

