<%@ Page Language="VB" MasterPageFile="~/BusinessPlanningWeb.master" AutoEventWireup="false" CodeFile="keiyaku.aspx.vb" Inherits="schedule_Scheduleselect" title="案件新規登録" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" Runat="Server">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="案件新規登録" 
        Font-Size="Large"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="伝票No"></asp:Label>
    <br />
    <asp:TextBox ID="T_伝票No" runat="server" Width="80px"></asp:TextBox>
    <asp:Button ID="B_検索" runat="server" Text="検索" />
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="顧客No"></asp:Label>
    <br />
    <asp:TextBox ID="T_顧客No" runat="server" BackColor="#CCCCCC" ReadOnly="True" 
        Width="87px"></asp:TextBox>
    <br />
    <asp:Label ID="Label4" runat="server" Text="顧客名"></asp:Label>
    <br />
    <asp:TextBox ID="T_顧客名" runat="server" BackColor="#CCCCCC" ReadOnly="True" 
        Width="362px"></asp:TextBox>
    <br />
    <asp:Label ID="Label9" runat="server" Text="担当支店"></asp:Label>
    <br />
    <asp:TextBox ID="T_担当部署名" runat="server" BackColor="#CCCCCC"></asp:TextBox>
    <br />
    <asp:TextBox ID="T_担当部署コード" runat="server" BackColor="#CCCCCC" Visible="False"></asp:TextBox>
    <br />
    <br />
    <asp:TextBox ID="T_担当者" runat="server" BackColor="#CCCCCC"></asp:TextBox>
    <br />
    <br />
    受付日<asp:Label ID="Label7" runat="server" ForeColor="Red" 
        Text="※ 入力形式はYYYY/MM/DD (カレンダーの日付をクリックしてください)"></asp:Label>
    <br />
    <asp:TextBox ID="T_作業予定日" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;<asp:RegularExpressionValidator 
        ID="RegularExpressionValidator1" runat="server" ControlToValidate="T_作業予定日" 
        ValidationExpression="\d{4}/\d{2}/\d{2}">※入力形式が違います</asp:RegularExpressionValidator>
    &nbsp;
    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
    <br />
    <br />
    <br />
    <asp:Label ID="Label8" runat="server" Text="備考"></asp:Label>
    <br />
    <asp:TextBox ID="T_備考" runat="server" Height="102px" TextMode="MultiLine" 
        Width="311px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="True" 
        Font-Size="Large"></asp:Label>
    <br />
    <br />
    <asp:Button ID="B_新規登録" runat="server" Text="新規登録" />
    &nbsp;<asp:Button ID="B_クリア" runat="server" Text="クリア" />
    &nbsp;<asp:Button ID="B_キャンセル" runat="server" Text="戻る" style="height: 21px" />
    <br />
    <br />
    <br />
</asp:Content>

