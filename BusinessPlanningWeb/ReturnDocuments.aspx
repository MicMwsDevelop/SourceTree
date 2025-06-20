<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="ReturnDocuments.aspx.vb" Inherits="_Default" title="ProspectiveCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-size: small;
            font-weight: bold;
        }
        .style2
        {
            font-size: small;
        }
        .style3
        {
            color: #FF3300;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Bold="True" Text="Label" 
            ForeColor="#3333FF"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" BackColor="#0033CC" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="White" Text="  10月改正案内返送書類処理" Font-Italic="True" 
            Width="500px"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label4" runat="server" Text="得意先No検索:"></asp:Label>
        <asp:TextBox ID="T_UserName" runat="server" AutoPostBack="True">%</asp:TextBox>
        　　<asp:Label ID="Label6" runat="server" Text="担当支店検索:"></asp:Label>
        <asp:DropDownList ID="D_Tanto" runat="server" AutoPostBack="True">
            <asp:ListItem>%</asp:ListItem>
            <asp:ListItem Value="札幌支店">札幌支店</asp:ListItem>
            <asp:ListItem Value="仙台支店">仙台支店</asp:ListItem>
            <asp:ListItem Value="首都圏営業部">首都圏営業部</asp:ListItem>
            <asp:ListItem Value="名古屋支店">名古屋支店</asp:ListItem>
            <asp:ListItem Value="大阪支店">大阪支店</asp:ListItem>
            <asp:ListItem Value="福岡支店">福岡支店</asp:ListItem>
            <asp:ListItem Value="広島支店">広島支店</asp:ListItem>
            <asp:ListItem Value="盛岡営業所">盛岡営業所</asp:ListItem>
            <asp:ListItem Value="金沢営業所">金沢営業所</asp:ListItem>
        </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <p>
        <asp:Label ID="Label5" runat="server" Text="ステータス検索" Visible="False"></asp:Label>
        <asp:DropDownList ID="D_Check" runat="server" AutoPostBack="True" 
            style="height: 19px; width: 81px" Visible="False">
            <asp:ListItem Value="False">返送なし</asp:ListItem>
            <asp:ListItem Value="True">返送あり</asp:ListItem>
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" 
            runat="server" Text="クリア" onclick="Button2_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblResult" runat="server" 
            Font-Bold="True" ForeColor="Red"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
    <p class="style1">
        <font face="MS UI Gothic">（区分１）：保守加入促進案内を送付した先で2008/04　改正を\81900で購入した先<span 
            class="style3">(保守金額:\31,500)</span></font></p>
    <p class="style1">
        <font face="MS UI Gothic">（区分２）：区分１以外で、保守加入促進案内を送付した先で、2007/10以降に新規購入した先</font></p>
    <p>
        <b><font face="MS UI Gothic"><span class="style2">（区分３）：区分１、２以外で、保守加入促進案内を送付した先</span></font><br 
            class="style2" />
        </b>
    </p>
    <p>
        <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="No" 
            DataSourceID="SqlDataSource1" Font-Size="Small" ForeColor="Black" 
            GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:BoundField DataField="No" HeaderText="No" InsertVisible="False" 
                    ReadOnly="True" SortExpression="No" />
                <asp:BoundField DataField="区分" HeaderText="区分" ReadOnly="True" />
                <asp:BoundField DataField="得意先No" HeaderText="得意先No" ReadOnly="True" 
                    SortExpression="得意先No" />
                <asp:BoundField DataField="顧客No" HeaderText="顧客No" ReadOnly="True" 
                    SortExpression="顧客No" />
                <asp:BoundField DataField="顧客名" HeaderText="顧客名" ReadOnly="True" 
                    SortExpression="顧客名" />
                <asp:BoundField DataField="支店名" HeaderText="支店名" ReadOnly="True" 
                    SortExpression="支店名" />
                <asp:BoundField DataField="システム略称" HeaderText="システム略称" ReadOnly="True" 
                    SortExpression="システム略称" />
                <asp:BoundField DataField="電話番号" HeaderText="電話番号" ReadOnly="True" 
                    SortExpression="電話番号" />
                <asp:CheckBoxField DataField="保守契約書返送有無" HeaderText="保守契約書返送" 
                    SortExpression="保守契約書返送有無" />
                <asp:CheckBoxField DataField="代引発送先" HeaderText="代引発送先" 
                    SortExpression="代引発送先" />
                <asp:CheckBoxField DataField="口座振替" HeaderText="口座振替" SortExpression="口座振替" />
                <asp:CheckBoxField DataField="書類不備の有無" HeaderText="書類不備の有無" 
                    SortExpression="書類不備の有無" />
                <asp:TemplateField HeaderText="備考" SortExpression="備考">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Height="39px" 
                            Text='<%# Bind("備考") %>' TextMode="MultiLine" Width="227px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("備考") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="代行回収状態" HeaderText="口振状態" ReadOnly="True" />
                <asp:BoundField DataField="処理日" HeaderText="処理日" SortExpression="処理日" 
                    ReadOnly="True" />
                <asp:BoundField DataField="処理者" HeaderText="処理者" SortExpression="処理者" 
                    ReadOnly="True" />
            </Columns>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            
            ConnectionString="<%$ ConnectionStrings:BusinessPlanningWebConnectionString2 %>" 
            DeleteCommand="DELETE FROM [ReturnDocuments] WHERE [No] = @original_No AND [得意先No] = @original_得意先No AND [顧客No] = @original_顧客No AND [顧客名] = @original_顧客名 AND [支店名] = @original_支店名 AND [システム略称] = @original_システム略称 AND [電話番号] = @original_電話番号 AND [保守契約書返送有無] = @original_保守契約書返送有無 AND [代引発送先] = @original_代引発送先 AND [口座振替] = @original_口座振替 AND [書類不備の有無] = @original_書類不備の有無 AND [備考] = @original_備考 AND [処理日] = @original_処理日 AND [処理者] = @original_処理者" 
            InsertCommand="INSERT INTO [ReturnDocuments] ([得意先No], [顧客No], [顧客名], [支店名], [システム略称], [電話番号], [保守契約書返送有無], [代引発送先], [口座振替], [書類不備の有無], [備考], [処理日], [処理者]) VALUES (@得意先No, @顧客No, @顧客名, @支店名, @システム略称, @電話番号, @保守契約書返送有無, @代引発送先, @口座振替, @書類不備の有無, @備考, @処理日, @処理者)" 
            OldValuesParameterFormatString="original_{0}" 
            
            SelectCommand="SELECT [No], [得意先No], [顧客No], [顧客名], [支店名], [システム略称], [電話番号],[保守契約書返送有無], [代引発送先], [口座振替], [書類不備の有無], [備考], [処理日], [処理者], [区分] , [代行回収状態] FROM [ReturnDocuments] WHERE (([支店名]  LIKE '%' + @支店名 + '%') AND ([得意先No] LIKE '%' + @得意先No + '%')) ORDER BY [No]"
            UpdateCommand="UPDATE [ReturnDocuments] SET [保守契約書返送有無] = @保守契約書返送有無, [代引発送先] = @代引発送先, [口座振替] = @口座振替, [書類不備の有無] = @書類不備の有無, [備考] = @備考, [処理日] = GetDate(), [処理者] = @処理者 WHERE [No] = @original_No AND ([保守契約書返送有無] IS NULL OR [保守契約書返送有無] = @original_保守契約書返送有無) AND ([代引発送先] IS NULL OR [代引発送先] = @original_代引発送先) AND ([口座振替] IS NULL OR [口座振替] = @original_口座振替) AND ([書類不備の有無] IS NULL OR [書類不備の有無] = @original_書類不備の有無) AND ([備考] IS NULL OR [備考] = @original_備考) AND ([処理者] IS NULL OR [処理者] = @original_処理者)"
            ConflictDetection="CompareAllValues">

            <SelectParameters>
                <asp:ControlParameter ControlID="T_UserName" Name="得意先No" PropertyName="Text" 
                    Type="String" />
                <asp:ControlParameter ControlID="D_Tanto" Name="支店名" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="D_Check" Name="保守契約書返送有無" 
                    PropertyName="SelectedValue" Type="Boolean" />
            </SelectParameters>
            
            <DeleteParameters>
                <asp:Parameter Name="original_No" Type="Int32" />
                <asp:Parameter Name="original_得意先No" Type="String" />
                <asp:Parameter Name="original_顧客No" Type="Int32" />
                <asp:Parameter Name="original_顧客名" Type="String" />
                <asp:Parameter Name="original_支店名" Type="String" />
                <asp:Parameter Name="original_システム略称" Type="String" />
                <asp:Parameter Name="original_電話番号" Type="String" />
                <asp:Parameter Name="original_保守契約書返送有無" Type="Boolean" />
                <asp:Parameter Name="original_代引発送先" Type="Boolean" />
                <asp:Parameter Name="original_口座振替" Type="Boolean" />
                <asp:Parameter Name="original_書類不備の有無" Type="Boolean" />
                <asp:Parameter Name="original_備考" Type="String" />
                <asp:Parameter Name="original_処理日" Type="DateTime" />
                <asp:Parameter Name="original_処理者" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="得意先No" Type="String" />
                <asp:Parameter Name="顧客No" Type="Int32" />
                <asp:Parameter Name="顧客名" Type="String" />
                <asp:Parameter Name="支店名" Type="String" />
                <asp:Parameter Name="システム略称" Type="String" />
                <asp:Parameter Name="電話番号" Type="String" />
                <asp:Parameter Name="保守契約書返送有無" Type="Boolean" />
                <asp:Parameter Name="代引発送先" Type="Boolean" />
                <asp:Parameter Name="口座振替" Type="Boolean" />
                <asp:Parameter Name="書類不備の有無" Type="Boolean" />
                <asp:Parameter Name="備考" Type="String" />
                <asp:Parameter Name="処理日" Type="DateTime" />
                
                
                <asp:SessionParameter Name="処理者" SessionField="VISITORNAME" Type="String" />
                
                <asp:Parameter Name="original_No" Type="Int32" />
                <asp:Parameter Name="original_得意先No" Type="String" />
                <asp:Parameter Name="original_顧客No" Type="Int32" />
                <asp:Parameter Name="original_顧客名" Type="String" />
                <asp:Parameter Name="original_支店名" Type="String" />
                <asp:Parameter Name="original_システム略称" Type="String" />
                <asp:Parameter Name="original_電話番号" Type="String" />
                <asp:Parameter Name="original_保守契約書返送有無" Type="Boolean" />
                <asp:Parameter Name="original_代引発送先" Type="Boolean" />
                <asp:Parameter Name="original_口座振替" Type="Boolean" />
                <asp:Parameter Name="original_書類不備の有無" Type="Boolean" />
                <asp:Parameter Name="original_備考" Type="String" />
                <asp:Parameter Name="original_処理日" Type="DateTime" />
                <asp:Parameter Name="original_処理者" Type="String" />
                
                
                
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="得意先No" Type="String" />
                <asp:Parameter Name="顧客No" Type="Int32" />
                <asp:Parameter Name="顧客名" Type="String" />
                <asp:Parameter Name="支店名" Type="String" />
                <asp:Parameter Name="システム略称" Type="String" />
                <asp:Parameter Name="電話番号" Type="String" />
                <asp:Parameter Name="保守契約書返送有無" Type="Boolean" />
                <asp:Parameter Name="代引発送先" Type="Boolean" />
                <asp:Parameter Name="口座振替" Type="Boolean" />
                <asp:Parameter Name="書類不備の有無" Type="Boolean" />
                <asp:Parameter Name="備考" Type="String" />
                <asp:Parameter Name="処理日" Type="DateTime" />
                <asp:Parameter Name="処理者" Type="String" />
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

