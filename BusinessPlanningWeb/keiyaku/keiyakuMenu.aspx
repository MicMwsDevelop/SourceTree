<%@ Page Language="VB" MaintainScrollPositionOnPostback="True" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="keiyakuMenu.aspx.vb" Inherits="schedule_scheduleMenu" title="ScheduleMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <p>
        <asp:Label ID="L_LoginInfo" runat="server" Font-Size="Large" 
            ForeColor="#3333CC" Text="助成制度申請書類管理（管理者メニュー）" Font-Bold="True"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" Text="新規登録" />
    </p>
    <p>
        <asp:Label ID="L_検索結果" runat="server" Font-Bold="True"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" 
            GridLines="None" Font-Size="Small">
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:BoundField DataField="No" HeaderText="No" InsertVisible="False" 
                    SortExpression="No" ReadOnly="True" />
                <asp:BoundField DataField="伝票No" HeaderText="伝票No" ReadOnly="True" 
                    SortExpression="伝票No" />
                <asp:BoundField DataField="顧客No" HeaderText="顧客No" ReadOnly="True" 
                    SortExpression="顧客No" />
                <asp:BoundField DataField="顧客名" HeaderText="顧客名" ReadOnly="True" 
                    SortExpression="顧客名" />
                <asp:BoundField DataField="担当部署名" HeaderText="担当部署名" ReadOnly="True" 
                    SortExpression="担当部署名" />
                <asp:BoundField DataField="担当者名" HeaderText="担当者名" ReadOnly="True" 
                    SortExpression="担当者名" />
                <asp:BoundField DataField="登録日" HeaderText="登録日" ReadOnly="True" 
                    SortExpression="登録日" />
                <asp:BoundField DataField="契約書捺印完了日" HeaderText="契約書捺印完了日" 
                    SortExpression="契約書捺印完了日" />
                <asp:BoundField DataField="納品書作成完了日" HeaderText="納品書作成完了日" 
                    SortExpression="納品書作成完了日" />
                <asp:BoundField DataField="領収書作成完了日" HeaderText="領収書作成完了日" 
                    SortExpression="領収書作成完了日" />
                <asp:TemplateField HeaderText="備考" SortExpression="備考">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Height="45px" 
                            Text='<%# Bind("備考") %>' TextMode="MultiLine" Width="222px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("備考") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="特定記録郵便番号" HeaderText="特定記録郵便番号" 
                    SortExpression="特定記録郵便番号" />
                <asp:BoundField DataField="ユーザー発送日" HeaderText="ユーザー発送日" 
                    SortExpression="ユーザー発送日" />
            </Columns>
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SalesDBConnectionString2 %>" 
            OldValuesParameterFormatString="original_{0}" 
        
            SelectCommand="SELECT * FROM [keiyaku_table] ORDER BY [No]" 
            ConflictDetection="CompareAllValues" 
            DeleteCommand="DELETE FROM [keiyaku_table] WHERE [伝票No] = @original_伝票No AND [No] = @original_No AND [顧客No] = @original_顧客No AND [顧客名] = @original_顧客名 AND [担当部署コード] = @original_担当部署コード AND [担当部署名] = @original_担当部署名 AND [担当者名] = @original_担当者名 AND [登録日] = @original_登録日 AND [契約書捺印完了日] = @original_契約書捺印完了日 AND [納品書作成完了日] = @original_納品書作成完了日 AND [領収書作成完了日] = @original_領収書作成完了日 AND [備考] = @original_備考 AND [特定記録郵便番号] = @original_特定記録郵便番号 AND [ユーザー発送日] = @original_ユーザー発送日" 
            InsertCommand="INSERT INTO [keiyaku_table] ([伝票No], [顧客No], [顧客名], [担当部署コード], [担当部署名], [担当者名], [登録日], [契約書捺印完了日], [納品書作成完了日], [領収書作成完了日], [備考], [特定記録郵便番号], [ユーザー発送日]) VALUES (@伝票No, @顧客No, @顧客名, @担当部署コード, @担当部署名, @担当者名, @登録日, @契約書捺印完了日, @納品書作成完了日, @領収書作成完了日, @備考, @特定記録郵便番号, @ユーザー発送日)"             
 
            UpdateCommand="UPDATE [keiyaku_table] SET [契約書捺印完了日] = @契約書捺印完了日, [納品書作成完了日] = @納品書作成完了日, [領収書作成完了日] = @領収書作成完了日, [備考] = @備考,[特定記録郵便番号] = @特定記録郵便番号, [ユーザー発送日] = @ユーザー発送日 WHERE ([No] = @original_No) AND ([契約書捺印完了日] IS NULL OR [契約書捺印完了日] = @original_契約書捺印完了日) AND ([納品書作成完了日] IS NULL OR [納品書作成完了日] = @original_納品書作成完了日) AND ([領収書作成完了日] IS NULL OR [領収書作成完了日] = @original_領収書作成完了日) AND ([備考] IS NULL OR [備考] = @original_備考) AND ([特定記録郵便番号] IS NULL OR [特定記録郵便番号] = @original_特定記録郵便番号) AND ([ユーザー発送日] IS NULL OR [ユーザー発送日] = @original_ユーザー発送日)">
            
            <DeleteParameters>
                <asp:Parameter Name="original_伝票No" Type="Int32" />
                <asp:Parameter Name="original_No" Type="Int32" />
                <asp:Parameter Name="original_顧客No" Type="String" />
                <asp:Parameter Name="original_顧客名" Type="String" />
                <asp:Parameter Name="original_担当部署コード" Type="String" />
                <asp:Parameter Name="original_担当部署名" Type="String" />
                <asp:Parameter Name="original_担当者名" Type="String" />
                <asp:Parameter Name="original_登録日" Type="String" />
                <asp:Parameter Name="original_契約書捺印完了日" Type="String" />
                <asp:Parameter Name="original_納品書作成完了日" Type="String" />
                <asp:Parameter Name="original_領収書作成完了日" Type="String" />
                <asp:Parameter Name="original_備考" Type="String" />
                <asp:Parameter Name="original_特定記録郵便番号" Type="String" />
                <asp:Parameter Name="original_ユーザー発送日" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="No" Type="Int32" />
                <asp:Parameter Name="顧客No" Type="String" />
                <asp:Parameter Name="顧客名" Type="String" />
                <asp:Parameter Name="担当部署コード" Type="String" />
                <asp:Parameter Name="担当部署名" Type="String" />
                <asp:Parameter Name="担当者名" Type="String" />
                <asp:Parameter Name="登録日" Type="String" />
                <asp:Parameter Name="契約書捺印完了日" Type="String" />
                <asp:Parameter Name="納品書作成完了日" Type="String" />
                <asp:Parameter Name="領収書作成完了日" Type="String" />
                <asp:Parameter Name="備考" Type="String" />
                <asp:Parameter Name="特定記録郵便番号" Type="String" />
                <asp:Parameter Name="ユーザー発送日" Type="String" />
                <asp:Parameter Name="original_伝票No" Type="Int32" />
                <asp:Parameter Name="original_No" Type="Int32" />
                <asp:Parameter Name="original_顧客No" Type="String" />
                <asp:Parameter Name="original_顧客名" Type="String" />
                <asp:Parameter Name="original_担当部署コード" Type="String" />
                <asp:Parameter Name="original_担当部署名" Type="String" />
                <asp:Parameter Name="original_担当者名" Type="String" />
                <asp:Parameter Name="original_登録日" Type="String" />
                <asp:Parameter Name="original_契約書捺印完了日" Type="String" />
                <asp:Parameter Name="original_納品書作成完了日" Type="String" />
                <asp:Parameter Name="original_領収書作成完了日" Type="String" />
                <asp:Parameter Name="original_備考" Type="String" />
                <asp:Parameter Name="original_特定記録郵便番号" Type="String" />
                <asp:Parameter Name="original_ユーザー発送日" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="伝票No" Type="Int32" />
                <asp:Parameter Name="顧客No" Type="String" />
                <asp:Parameter Name="顧客名" Type="String" />
                <asp:Parameter Name="担当部署コード" Type="String" />
                <asp:Parameter Name="担当部署名" Type="String" />
                <asp:Parameter Name="担当者名" Type="String" />
                <asp:Parameter Name="登録日" Type="String" />
                <asp:Parameter Name="契約書捺印完了日" Type="String" />
                <asp:Parameter Name="納品書作成完了日" Type="String" />
                <asp:Parameter Name="領収書作成完了日" Type="String" />
                <asp:Parameter Name="備考" Type="String" />
                <asp:Parameter Name="特定記録郵便番号" Type="String" />
                <asp:Parameter Name="ユーザー発送日" Type="String" />
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

