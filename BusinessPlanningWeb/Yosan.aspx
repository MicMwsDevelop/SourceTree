<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="Yosan.aspx.vb" Inherits="Yosan" title="予算管理試作" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">



        <asp:Label ID="Label2" runat="server" Text="予算管理試作" Font-Size="X-Large"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="予算種別"></asp:Label>
        <asp:DropDownList ID="D_kind" runat="server" 
            DataSourceID="SqlDataSource2" DataTextField="項目名称" DataValueField="項目名称" 
            AutoPostBack="True">
            <asp:ListItem>%</asp:ListItem>
            <asp:ListItem>システム本数</asp:ListItem>
            <asp:ListItem>システム金額</asp:ListItem>
            <asp:ListItem>その他金額</asp:ListItem>
            <asp:ListItem>保守金額</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="L_Busho" runat="server" Text="部署"></asp:Label>
        <asp:DropDownList ID="D_Busho" runat="server" AutoPostBack="True">
            <asp:ListItem>%</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="L_Member" runat="server" Text="Member"></asp:Label>
        &nbsp;<asp:DropDownList ID="D_User" runat="server" AutoPostBack="True">
            <asp:ListItem>%</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            SelectCommand="SELECT [項目名称] FROM [Yosan_System]"></asp:SqlDataSource>
        <asp:TextBox ID="T_Area" runat="server" Visible="False"></asp:TextBox>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="3" DataKeyNames="ID" DataSourceID="SqlDataSource1" 
            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
            CellSpacing="2" ShowFooter="True" Font-Size="Small">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" Visible="False" />
                <asp:BoundField DataField="所属" HeaderText="所属" ReadOnly="True" 
                    SortExpression="所属" />
                <asp:BoundField DataField="氏名" HeaderText="氏名" ReadOnly="True" 
                    SortExpression="氏名" />
                <asp:BoundField DataField="名称" HeaderText="名称" ReadOnly="True" 
                    SortExpression="名称" />
                <asp:BoundField DataField="column1" HeaderText="8月" SortExpression="column1" />
                <asp:BoundField DataField="column2" HeaderText="9月" SortExpression="column2" />
                <asp:BoundField DataField="column3" HeaderText="10月" SortExpression="column3" />
                <asp:BoundField DataField="column4" HeaderText="11月" SortExpression="column4" />
                <asp:BoundField DataField="column5" HeaderText="12月" SortExpression="column5" />
                <asp:BoundField DataField="column6" HeaderText="1月" SortExpression="column6" />
                <asp:BoundField DataField="column7" HeaderText="2月" SortExpression="column7" />
                <asp:BoundField DataField="column8" HeaderText="3月" SortExpression="column8" />
                <asp:BoundField DataField="column9" HeaderText="4月" SortExpression="column9" />
                <asp:BoundField DataField="column10" HeaderText="5月" 
                    SortExpression="column10" />
                <asp:BoundField DataField="column11" HeaderText="6月" 
                    SortExpression="column11" />
                <asp:BoundField DataField="column12" HeaderText="7月" 
                    SortExpression="column12" />
                <asp:TemplateField HeaderText="合計">
                    <ItemTemplate>
                        <asp:Label ID="合計" runat="server" 
                            Text='<%# Eval("column1"）+Eval("column2"）+Eval("column3"）+Eval("column4"）+Eval("column5"）+Eval("column6"）+Eval("column7"）+Eval("column8"）+Eval("column9"）+Eval("column10"）+Eval("column11"）+Eval("column12"） %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="編集可否">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("編集ロック") %>' 
                            Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            DeleteCommand="DELETE FROM [YosanTable] WHERE [ID] = @ID" 
            InsertCommand="INSERT INTO [YosanTable] ([所属], [氏名], [名称], [8月], [9月], [10月], [11月], [12月], [1月], [2月], [3月], [4月], [5月], [6月], [7月]) VALUES (@所属, @氏名, @名称, @column1, @column2, @column3, @column4, @column5, @column6, @column7, @column8, @column9, @column10, @column11, @column12)" 
            SelectCommand="SELECT [ID], [所属], [氏名], [名称], [8月] AS column1, [9月] AS column2, [10月] AS column3, [11月] AS column4, [12月] AS column5, [1月] AS column6, [2月] AS column7, [3月] AS column8, [4月] AS column9, [5月] AS column10, [6月] AS column11, [7月] AS column12 ,[編集ロック] FROM [YosanTable] WHERE (([氏名] LIKE '%' + @氏名 + '%') AND ([所属] LIKE '%' + @所属 + '%') AND ([名称] LIKE '%' + @名称 + '%') AND ([エリア] LIKE '%' + @エリア + '%'))" 
            UpdateCommand="UPDATE [YosanTable] SET [8月] = @column1, [9月] = @column2, [10月] = @column3, [11月] = @column4, [12月] = @column5, [1月] = @column6, [2月] = @column7, [3月] = @column8, [4月] = @column9, [5月] = @column10, [6月] = @column11, [7月] = @column12 WHERE [ID] = @ID">
            <SelectParameters>
                <asp:ControlParameter ControlID="D_User" Name="氏名" PropertyName="SelectedValue" 
                    Type="String" />
                <asp:ControlParameter ControlID="D_Busho" Name="所属" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="D_Kind" Name="名称" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="T_Area" Name="エリア" 
                    PropertyName="text" Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="所属" Type="String" />
                <asp:Parameter Name="氏名" Type="String" />
                <asp:Parameter Name="名称" Type="String" />
                <asp:Parameter Name="column1" Type="Int32" />
                <asp:Parameter Name="column2" Type="Int32" />
                <asp:Parameter Name="column3" Type="Int32" />
                <asp:Parameter Name="column4" Type="Int32" />
                <asp:Parameter Name="column5" Type="Int32" />
                <asp:Parameter Name="column6" Type="Int32" />
                <asp:Parameter Name="column7" Type="Int32" />
                <asp:Parameter Name="column8" Type="Int32" />
                <asp:Parameter Name="column9" Type="Int32" />
                <asp:Parameter Name="column10" Type="Int32" />
                <asp:Parameter Name="column11" Type="Int32" />
                <asp:Parameter Name="column12" Type="Int32" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="所属" Type="String" />
                <asp:Parameter Name="氏名" Type="String" />
                <asp:Parameter Name="名称" Type="String" />
                <asp:Parameter Name="column1" Type="Int32" />
                <asp:Parameter Name="column2" Type="Int32" />
                <asp:Parameter Name="column3" Type="Int32" />
                <asp:Parameter Name="column4" Type="Int32" />
                <asp:Parameter Name="column5" Type="Int32" />
                <asp:Parameter Name="column6" Type="Int32" />
                <asp:Parameter Name="column7" Type="Int32" />
                <asp:Parameter Name="column8" Type="Int32" />
                <asp:Parameter Name="column9" Type="Int32" />
                <asp:Parameter Name="column10" Type="Int32" />
                <asp:Parameter Name="column11" Type="Int32" />
                <asp:Parameter Name="column12" Type="Int32" />
            </InsertParameters>
        </asp:SqlDataSource>
        <br />
</asp:Content>    


