################################################################
# OnlineDemandDuplicateAlart.ps1
# APPID646 各種作業料同月内重複申請アラート
#
# 処理内容
# 各種作業料作業済申請情報管理テーブル（T_USE_ONLINE_DEMAND）で同月内に２回以上作業料を申請している顧客が
# 存在する場合にmainte_info_sys@mic.jpに対しアラートメールを送信する
#
# Ver1.00(2024/08/21 勝呂):新規作成
# Ver1.01(2025/08/05 勝呂):VBScriptからPowerShellに移植
################################################################
#
#Import-Module -Name "D:\SourceTree\PowerShell\Modules\CommonModule\CommonModule.psm1"
Import-Module -Name "C:\_PowerShell\Modules\CommonModule\CommonModule.psm1"


if ($Args.Count -ne 1 -Or $Args[0].ToUpper() -ne "AUTO")
{
	#第一引数に AUTO が必要 ※誤動作の防止策
	Write-Output "Usage: OnlineDemandDuplicateAlart.ps1 AUTO"
	return
}
try
{
	#$conn = New-Object System.Data.SqlClient.SqlConnection $SQL_CONNECTION_STR
	$conn = New-Object System.Data.SqlClient.SqlConnection $SQL_CONNECTION_STR_TEST
	$conn.Open()
	$cmd = $conn.CreateCommand()
	$cmd.Connection  = $conn
	$cmd.CommandText = "SELECT [ApplyNo], D.[CustomerID] as 顧客No, [fCliName], [GoodsID], [sms_mei], CONVERT(date, [ApplyDate]) as 受付日, CONVERT(date, [SalesDate]) as 売上日" `
				+ " FROM [charlieDB].[dbo].[T_USE_ONLINE_DEMAND] as D" `
				+ " INNER JOIN [JunpDB].[dbo].[tClient] as CL on CL.[fCliID] = D.[CustomerID]" `
				+ " LEFT JOIN [JunpDB].[dbo].[vMicPCA商品マスタ] as M on M.[sms_scd] = D.[GoodsID]" `
				+ " INNER JOIN" `
				+ " (" `
					+ " SELECT [CustomerID], DATEADD(dd, 1, EOMONTH([ApplyDate] , -1)) as 受付月" `
					+ " FROM [charlieDB].[dbo].[T_USE_ONLINE_DEMAND]" `
					+ " WHERE [CustomerID] < 30000000" `
					+ " GROUP BY [CustomerID], DATEADD(dd, 1, EOMONTH([ApplyDate] , -1))" `
					+ " HAVING count([CustomerID]) > 1" `
				+ ") as DU on DU.CustomerID = D.CustomerID" `
				+ " ORDER BY D.[CustomerID], [ApplyNo]"
	$adapter = New-Object System.Data.SqlClient.SqlDataAdapter $cmd;
	$dataTable = New-Object System.Data.DataTable;

	$adapter.Fill($dataTable)
	if ($dataTable.Rows.Count -gt 0)
	{
		$subject = "各種作業料同月内重複申請アラート"
		$htmlBody = "<html>" `
						+ "<body>" `
						+ "<font face=""MS UI Gothic"" size=""2"">" `
						+ "<p>システム管理部各位<br><br>同商品の各種作業料が同月内に２回以上申請されています。拠点へのご確認をお願います。</p>" `
						+ "<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>" `
						+ "<tr>" `
						+ "<th style=""BACKGROUND-COLOR: silver""><font size=2>受付番号</font></th>" `
						+ "<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>" `
						+ "<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客名</font></th>" `
						+ "<th style=""BACKGROUND-COLOR: silver""><font size=2>商品コード</font></th>" `
						+ "<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>" `
						+ "<th style=""BACKGROUND-COLOR: silver""><font size=2>受付日</font></th>" `
						+ "<th style=""BACKGROUND-COLOR: silver""><font size=2>売上日</font></th>" `
						+ "</tr>"
		foreach ($row in $dataTable.Rows)
		{
			[string]$acceptNo = $row.item("ApplyNo")
			[int]$customerID = $row.item("顧客No")
			[string]$customerName = $row.item("fCliName")
			[string]$goodsID = $row.item("GoodsID")
			[string]$goodsName = $row.item("sms_mei")
			[string]$acceptDare = ""
			[string]$saleDate = ""
			if ($row.item("受付日").GetType().FullName -eq "System.DateTime")
			{
				$acceptDare = $row.item("受付日").ToString("yyyy/MM/dd")
			}
			if ($row.item("売上日").GetType().FullName -eq "System.DateTime")
			{
				$saleDate = $row.item("売上日").ToString("yyyy/MM/dd")
			}
			$htmlBody += "<tr>" `
								+ "<td><font size=2>" + $acceptNo + "</font></td>" `
								+ "<td><font size=2>" + [string]$customerID + "</font></td>" `
								+ "<td><font size=2>" + $customerName + "</font></td>" `
								+ "<td><font size=2>" + $goodsID + "</font></td>" `
								+ "<td><font size=2>" + $goodsName + "</font></td>" `
								+ "<td><font size=2>" + $acceptDare + "</font></td>" `
								+ "<td><font size=2>" + $saleDate + "</font></td>"
			$htmlBody += "</tr>"
		}
		$htmlBody += "</table>" `
							+ "<p>以上、よろしくお願いいたします。</p>" `
							+ "</font>" `
							+ "</body>" `
							+ "</html>"
		#SendMailHtmlBody $MAINTE_MAIL_ADDRESS $MAINTE_MAIL_ADDRESS "" "" $subject $htmlBody
		SendMailHtmlBody $MAINTE_MAIL_ADDRESS "suguro@mic.jp" "" "" $subject $htmlBody
	}
}
catch
{
	Write-Output $Error[0].Exception.Message
}
finally
{
	$conn.Close()
	$conn.Dispose()
}
