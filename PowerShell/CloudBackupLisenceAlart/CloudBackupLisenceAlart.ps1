################################################################
# CloudBackupLisenceAlart.ps1
# APPID604 クラウドバックアップライセンスアラート
#
# 処理内容
# クラウドバックアップライセンス管理テーブル（T_USE_CLOUDDATA_LICENSE）で顧客が割り当てられていないライセンスの残数が
# 100未満の時にはmainte_info_sys@mic.jpに対しメールを送信する
#
# Ver1.00(2020/10/16 勝呂):新規作成
# Ver1.01(2021/05/17 勝呂):64bit対応の為、メール送信方式をbasp21からCDO方式に変更
# Ver1.02(2021/06/02 勝呂):本社移転に伴い実行環境をxenappsvからtasksvに移動
# Ver1.03(2023/09/14 勝呂):2023/08/01の組織変更に伴い、営業管理部からシステム管理部に変更
# Ver1.04(2024/01/22 越田):2023/08/01組織変更対応 送信元・宛先・BCCを再整理
# Ver1.05(2025/08/07 勝呂):VBScriptからPowerShellに移植
################################################################
#
#Import-Module -Name "D:\SourceTree\PowerShell\Modules\CommonModule\CommonModule.psm1"
Import-Module -Name "C:\_PowerShell\Modules\CommonModule\CommonModule.psm1"

#警告下限数
Set-Variable -Name AlartCount 2000  #100

if ($Args.Count -ne 1 -Or $Args[0].ToUpper() -ne "AUTO")
{
	#第一引数に AUTO が必要 ※誤動作の防止策
	Write-Output "Usage: CloudBackupLisenceAlart.ps1 AUTO"
	return
}
try
{
	$conn = New-Object System.Data.SqlClient.SqlConnection $SQL_CONNECTION_STR
	$conn.Open()
	$cmd = $conn.CreateCommand()
	$cmd.Connection  = $conn
	$cmd.CommandText = "SELECT LC.LicCnt as recordCnt FROM (SELECT Count(*) as LicCnt FROM dbo.[T_USE_CLOUDDATA_LICENSE] WHERE fCustomerID is null) as LC WHERE LC.LicCnt < " + [string]$AlartCount
	$adapter = New-Object System.Data.SqlClient.SqlDataAdapter $cmd;
	$dataTable = New-Object System.Data.DataTable;

	$adapter.Fill($dataTable)
	[int]$recCount = 0;
	foreach ($row in $dataTable.Rows)
	{
		$recCount = $row.item("recordCnt")
		break;
	}
	if ($recCount -gt 0)
	{
		$subject = "クラウドバックアップ ライセンスアラート"
		$textBody = "クラウドバックアップのライセンス残数が" + [string]$recCount + " 個になりました。`nライセンスを追加してください。`n`n" + $BaseDepartment
		#SendMailTextBody $MAINTE_MAIL_ADDRESS $MAINTE_MAIL_ADDRESS "" "" $subject $textBody
		SendMailTextBody $MAINTE_MAIL_ADDRESS "suguro@mic.jp" "" "" $subject $textBody
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
