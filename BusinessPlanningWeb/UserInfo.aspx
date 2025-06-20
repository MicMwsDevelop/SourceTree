<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserInfo.aspx.vb" Inherits="UserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UserInfo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <b>【見込客詳細情報】<br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="顧客No:"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="L_顧客No" runat="server" Text="Label" BackColor="#FFFF66"></asp:Label>
        <br />
        ﾌﾘｶﾞﾅ：&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="L_フリガナ" runat="server" Text="Label" BackColor="#FFFF66"></asp:Label>
        <br />
        顧客名:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="L_顧客名" runat="server" Text="Label" BackColor="#FFFF66"></asp:Label>
        <br />
        〒:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="L_郵便番号" runat="server" Text="Label" BackColor="#FFFF66"></asp:Label>
        <br />
        ﾌﾘｶﾞﾅ：&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="L_住所フリガナ" runat="server" Text="Label" BackColor="#FFFF66"></asp:Label>
        <br />
        住所：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="L_住所" runat="server" Text="Label" BackColor="#FFFF66"></asp:Label>
        <br />
        電話番号：<asp:Label ID="L_電話番号" runat="server" Text="Label" BackColor="#FFFF66"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        FAX番号：<asp:Label ID="L_FAX番号" runat="server" Text="Label" BackColor="#FFFF66"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="訪問履歴"></asp:Label>
        　<asp:Label ID="L_訪問結果" runat="server"></asp:Label>
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="SqlDataSource2" Font-Size="Small" 
            ForeColor="#333333" GridLines="Vertical">
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="fTrnFrDate" HeaderText="訪問日" />
                <asp:BoundField DataField="fUsrName" HeaderText="訪問者" />
                <asp:BoundField DataField="fTrnResult" HeaderText="結果" />
                <asp:TemplateField HeaderText="メモ">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fTrnMemo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("fTrnMemo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
        <br />
        <asp:Label ID="Label3" runat="server" Text="メモ情報"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="L_検索結果" runat="server"></asp:Label>
        </b>
    
    </div>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="Small" 
        GridLines="Vertical">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:TemplateField HeaderText="登録日時/登録者">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fMemtype") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("fMemtype") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="メモ内容">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("fMemMemo") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("fMemMemo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </form>
</body>
</html>
