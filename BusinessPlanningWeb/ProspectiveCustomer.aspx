<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="ProspectiveCustomer.aspx.vb" Inherits="_Default" title="ProspectiveCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" Text="Label" 
            ForeColor="#3333FF"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" BackColor="#666666" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  見込客管理" Font-Italic="True" 
            Width="500px"></asp:Label>
        <a href="htmlPage\見込客管理WEB操作手引書.pdf" target="_blank" ><span style="font-size: 10.0pt">●見込客管理マニュアル●</span></a>
    </p>
    <p>
        <asp:Label ID="Label12" runat="server" Font-Bold="True" 
            Text="2009/05/07時点でステータスが『済』（ＷＷ登録済）となっているものは非表示としました。"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label4" runat="server" Text="医院名検索:"></asp:Label>
        <asp:TextBox ID="T_UserName" runat="server" AutoPostBack="True">%</asp:TextBox>
        　　<asp:Label ID="Label6" runat="server" Text="担当支店検索:"></asp:Label>
        <asp:DropDownList ID="D_Tanto" runat="server" AutoPostBack="True">
            <asp:ListItem>%</asp:ListItem>
            <asp:ListItem Value="01">札幌支店</asp:ListItem>
            <asp:ListItem Value="02">仙台支店</asp:ListItem>
            <asp:ListItem Value="26">首都圏営業部</asp:ListItem>
            <asp:ListItem Value="07">名古屋支店</asp:ListItem>
            <asp:ListItem Value="08">大阪支店</asp:ListItem>
            <asp:ListItem Value="09">福岡支店</asp:ListItem>
            <asp:ListItem Value="12">広島営業所</asp:ListItem>
            <asp:ListItem Value="14">金沢営業所</asp:ListItem>
        </asp:DropDownList>
        　　<asp:Label ID="Label11" runat="server" Text="顧客No検索:"></asp:Label>
        <asp:TextBox ID="T_UserNo" runat="server" AutoPostBack="True" Width="84px">%</asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label5" runat="server" Text="ステータス検索"></asp:Label>
        <asp:DropDownList ID="D_Check" runat="server" AutoPostBack="True">
            <asp:ListItem>%</asp:ListItem>
            <asp:ListItem>未</asp:ListItem>
            <asp:ListItem>済</asp:ListItem>
            <asp:ListItem>保留</asp:ListItem>
            <asp:ListItem>不可</asp:ListItem>
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label9" runat="server" 
            Text="住所検索"></asp:Label>
        &nbsp;<asp:TextBox ID="T_UserAdress" runat="server" AutoPostBack="True">%</asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="Label10" runat="server" 
            Text="電話番号検索(ﾊｲﾌﾝなし)"></asp:Label>
        &nbsp;<asp:TextBox ID="T_UserTel" runat="server" AutoPostBack="True" 
            Width="103px">%</asp:TextBox>
        　　　<asp:Button ID="Button2" runat="server" Text="クリア" style="height: 21px" />
        &nbsp;&nbsp;&nbsp;</p>
    <p>
        &nbsp;&nbsp;<asp:Label ID="lblResult" runat="server" 
            Font-Bold="True" ForeColor="Red"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
    <p>
        <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource1" 
            AutoGenerateColumns="False" ForeColor="Black" 
            Font-Size="Small" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:TemplateField HeaderText="ｽﾃｰﾀｽ" SortExpression="Check">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("Check") %>'>
                            <asp:ListItem>未</asp:ListItem>
                            <asp:ListItem>済</asp:ListItem>
                            <asp:ListItem>保留</asp:ListItem>
                            <asp:ListItem>不可</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Check") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="顧客No">
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("UserNo") %>' 
                            Font-Underline="False" ForeColor="Black"></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <a onclick='javascript:WinOpen("<%# DataBinder.Eval(Container.DataItem, "UserNo") %>")';>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("UserNo") %>' 
                            Font-Underline="True"></asp:Label>
                        </a>
                    </ItemTemplate>
                    <ItemStyle Font-Underline="False" ForeColor="#3333FF" />
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="医院名" 
                    SortExpression="UserName" />
                <asp:BoundField DataField="Adress" HeaderText="住所" 
                    SortExpression="Adress" />
                <asp:BoundField DataField="Tel" HeaderText="TEL" 
                    SortExpression="Tel" ReadOnly="True" />
                <asp:TemplateField HeaderText="System" SortExpression="System">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" 
                            DataSourceID="SqlDataSource1" DataTextField="UseSystem" 
                            DataValueField="UseSystem" SelectedValue='<%# Bind("System") %>'>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>"
                             
                            SelectCommand="SELECT UseSystem FROM [System] ORDER BY [makerID]">
                        </asp:SqlDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("System") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LeaseUp">
                    <EditItemTemplate>
                        <asp:Label ID="Label7" runat="server" Font-Size="XX-Small" ForeColor="Red" 
                            Text="入力形式:YYYY/MM"></asp:Label>
                        <br />
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("LeaseUpdate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("LeaseUpdate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="備考" SortExpression="Bikou">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Height="111px" 
                            Text='<%# Bind("Bikou") %>' TextMode="MultiLine" Width="234px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Bikou") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Update">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Update") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Update") %>'></asp:Label>
                        <br />
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("UpdateMan") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            DeleteCommand="DELETE FROM [ProspectiveCustomer] WHERE [ID] = @original_ID AND [UserNo] = @original_UserNo AND [UserName] = @original_UserName AND [Adress] = @original_Adress AND [Tel] = @original_Tel AND [UserClass] = @original_UserClass AND [System] = @original_System AND [Bikou] = @original_Bikou AND [Check] = @original_Check AND [todofuken] = @original_todofuken AND [tanto] = @original_tanto AND [Update] = @original_Update AND [UpdateMan] = @original_UpdateMan" 
            InsertCommand="INSERT INTO [ProspectiveCustomer] ([UserNo], [UserName], [Adress], [Tel], [UserClass], [System], [Bikou], [Check], [todofuken], [tanto], [Update], [UpdateMan]) VALUES (@UserNo, @UserName, @Adress, @Tel, @UserClass, @System, @Bikou, @Check, @todofuken, @tanto, @Update, @UpdateMan)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT * FROM [ProspectiveCustomer] WHERE (([Check] LIKE '%' + @Check + '%') AND ([UserName] LIKE '%' + @UserName + '%') AND ([Tanto] LIKE '%' + @Tanto + '%') AND ([Adress] LIKE '%' + @UserAdress + '%') AND ([NoBarTel] LIKE '%' + @NoBarTel + '%') AND ([UserNo] LIKE '%' + @UserNo + '%') AND ([EndFlg] = 0)) ORDER BY [ID]" 
            
            UpdateCommand="UPDATE [ProspectiveCustomer] SET [UserName] = @UserName, [Adress] = @Adress, [System] = @System, [Bikou] = @Bikou,[LeaseUpDate] = @LeaseUpDate, [Check] = @Check, [Update] = GetDate(), [UpdateMan] = @UpdateMan WHERE [UserName] = @original_UserName AND ([Adress] IS NULL OR [Adress] = @original_Adress) AND ([System] IS NULL OR [System] = @original_System) AND ([Bikou] IS NULL OR [Bikou] = @original_Bikou) AND ([LeaseUpDate] IS NULL OR [LeaseUpDate] = @original_LeaseUpDate) AND ([Check] IS NULL OR [Check] = @original_Check)"
            ConflictDetection="CompareAllValues">
            <SelectParameters>
                <asp:ControlParameter ControlID="D_Check" Name="Check" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="D_Tanto" Name="Tanto" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="T_UserName" Name="UserName" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="T_UserAdress" Name="UserAdress" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="T_UserTel" Name="NoBarTel" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="T_UserNo" Name="UserNo" 
                    PropertyName="Text" Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_UserNo" Type="Int32" />
                <asp:Parameter Name="original_UserName" Type="String" />
                <asp:Parameter Name="original_Adress" Type="String" />
                <asp:Parameter Name="original_Tel" Type="String" />
                <asp:Parameter Name="original_UserClass" Type="String" />
                <asp:Parameter Name="original_System" Type="String" />
                <asp:Parameter Name="original_Bikou" Type="String" />
                <asp:Parameter Name="original_Check" Type="String" />
                <asp:Parameter Name="original_todofuken" Type="String" />
                <asp:Parameter Name="original_tanto" Type="String" />
                <asp:Parameter Name="original_Update" Type="DateTime" />
                <asp:Parameter Name="original_UpdateMan" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="UserNo" Type="Int32" />
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="Adress" Type="String" />
                <asp:Parameter Name="Tel" Type="String" />
                <asp:Parameter Name="UserClass" Type="String" />
                <asp:Parameter Name="System" Type="String" />
                <asp:Parameter Name="Bikou" Type="String" />
                <asp:Parameter Name="LeaseUpDate" Type="String" />
                <asp:Parameter Name="Check" Type="String" />
                <asp:Parameter Name="todofuken" Type="String" />
                <asp:Parameter Name="tanto" Type="String" />
                <asp:Parameter Name="Update" Type="DateTime" />
                
                <asp:SessionParameter Name="UpdateMan" SessionField="VISITORNAME" Type="String" />
                
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_UserNo" Type="Int32" />
                <asp:Parameter Name="original_UserName" Type="String" />
                <asp:Parameter Name="original_Adress" Type="String" />
                <asp:Parameter Name="original_Tel" Type="String" />
                <asp:Parameter Name="original_UserClass" Type="String" />
                <asp:Parameter Name="original_System" Type="String" />
                <asp:Parameter Name="original_Bikou" Type="String" />
                <asp:Parameter Name="original_LeaseUpDate" Type="String" />
                <asp:Parameter Name="original_Check" Type="String" />
                <asp:Parameter Name="original_todofuken" Type="String" />
                <asp:Parameter Name="original_tanto" Type="String" />
                <asp:Parameter Name="original_Update" Type="DateTime" />
                <asp:Parameter Name="original_UpdateMan" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="UserNo" Type="Int32" />
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="Adress" Type="String" />
                <asp:Parameter Name="Tel" Type="String" />
                <asp:Parameter Name="UserClass" Type="String" />
                <asp:Parameter Name="System" Type="String" />
                <asp:Parameter Name="Bikou" Type="String" />
                <asp:Parameter Name="Check" Type="String" />
                <asp:Parameter Name="todofuken" Type="String" />
                <asp:Parameter Name="tanto" Type="String" />
                <asp:Parameter Name="Update" Type="DateTime" />
                <asp:Parameter Name="UpdateMan" Type="String" />
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
    <p>
    </p>
</asp:Content>

