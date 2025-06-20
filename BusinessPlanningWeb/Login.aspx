<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
        .style2
        {
            font-size: small;
        }
    </style>
</head>

<body background="IMG1.jpg">
     
     <form id="login" method=post runat="server" >
     
			<font face="MS UI Gothic"></FONT>
	        <h1>BusinessPlanningWeb</h1>
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 72px; WIDTH: 552px; POSITION: absolute; TOP: 160px; HEIGHT: 111px"
				cellSpacing="1" cellPadding="1" width="552" border="0">
				<TR>
					<TD align="center">
						<asp:Label id="Label1" runat="server" Font-Size="Medium" Font-Bold="True">ログインページ</asp:Label></TD>
				</TR>
				<TR>
					<TD><FONT face="MS UI Gothic"></FONT></TD>
				</TR>
				<TR>
					<TD align="center"><FONT face="MS UI Gothic">
							<asp:Label id="lblError" runat="server" Font-Bold="True" ForeColor="Red" Height="16px"></asp:Label></FONT></TD>
				</TR>
				<TR>
					<TD><FONT face="MS UI Gothic"></FONT></TD>
				</TR>
				<TR>
					<TD align="center"><FONT face="MS UI Gothic">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="300" border="0">
								<TR>
									<TD align="center" bgColor="#666666" width="100">
										<asp:Label id="Label2" runat="server" Font-Bold="True" ForeColor="White">ID</asp:Label></TD>
									<TD width="20">&nbsp;</TD>
									<TD>
										<asp:TextBox id="txtID" runat="server" Width="180px" Height="25px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD width="100">&nbsp;</TD>
									<TD width="20">&nbsp;</TD>
									<TD>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="center" bgColor="#666666" width="100">
										<asp:Label id="Label3" runat="server" Font-Bold="True" ForeColor="White">PASS</asp:Label></TD>
									<TD width="20">&nbsp;</TD>
									<TD>
										<asp:TextBox id="txtPass" runat="server" TextMode="Password" Width="180px" Height="25px"></asp:TextBox></TD>
								</TR>
							</TABLE>
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>
				<tr>
					<TD align="center" style="text-align: center" class="style1">
						<asp:Button id="btnLogin0" runat="server" Text="ログイン" style="height: 21px"></asp:Button></TD>
                </tr>
				
				<tr>
					<TD align="center" style="text-align: left" class="style1">
						<span class="style2">※　電子レセプト対応進捗状況を終了しました。 
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; →ＷｏｎｄｅｒＷｅｂ-データページ＜レセ電進捗状況＞でご確認ください。 </span>
                        <br class="style2" />
                        <span class="style2">
                        <br />
                        ※　キャンペーン伝票抽出を終了しました。<br />
                        　　 →必要な場合には営業管理課までご連絡ください。<br />
                        <br />
                        ※　ＭＷＳユーザー数&nbsp; を終了しました。<br />
                        　　 →WonderWeb-データページ＜ユーザー数集計表＞でご確認ください。 
                        <br />
                        <br />
                        ※　ＭＷＳ口振未加入ユーザーを終了しました。<br />
                        　　 →WonderWeb-データページ＜口座振替未登録MWSユーザー＞でご確認ください。test<br />
                        <br />
                        </span></TD>
                </tr>
				
			</TABLE>
			&nbsp;<p>
                <asp:Label ID="Label4" runat="server" Font-Size="X-Large" ForeColor="#3333CC" 
                    Text=" "></asp:Label>
            </p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </p>
  </form>

</body>
</html>
