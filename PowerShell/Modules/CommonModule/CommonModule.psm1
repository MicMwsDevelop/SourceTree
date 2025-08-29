################################################################
# CommonModule.psm1
# PowerShell共通モジュール
#
# Ver1.00(2025/08/07 勝呂):新規作成
################################################################
#
#SQL Server接続文字列
Set-Variable -Name SQL_CONNECTION_STR -Value "Server=SQLSV;Database=charlieDB;User ID=ww_reader;Password=20150801;" -Scope Global
Set-Variable -Name SQL_CONNECTION_STR_TEST -Value "Server=TESTSV2;Database=charlieDB;User ID=ww_reader;Password=20150801;" -Scope Global

#担当部署
Set-Variable -Name BASE_DEPARTMENT -Value "システム管理部" -Scope Global

#メールアドレス
Set-Variable -Name BASE_MAIL_ADDRESS -Value "システム管理部<sys_kanri@mic.jp>" -Scope Global
Set-Variable -Name MAINTE_MAIL_ADDRESS -Value "MIC社内システム自動送信<mainte_info_sys@mic.jp>" -Scope Global
Set-Variable -Name ESTORE_MAIL_ADDRESS -Value "株式会社ミック　e store <mic-estore@mic.jp>" -Scope Global

#メール送信設定値
Set-Variable -Name SEND_USING -Value 2 -Option Constant
Set-Variable -Name SMTP_SERVER_MAIL_ADDRESS -Value "dove.mic.jp" -Option Constant
Set-Variable -Name SMTP_SERVER_PORT -Value 25 -Option Constant

#端数処理
enum RoundFraction
{
	Cut      #切り捨て
	Round  #四捨五入
	Raise    #切り上げ
}

#CDOメール送信（テキスト用）
function SendMailTextBody
{
	param
	(
		[string]$from,     #送信元アドレス
		[string]$to,         #送信先アドレス
		[string]$cc,         #Cc 送信先アドレス
		[string]$bcc,       #非公開送信先アドレス
		[string]$subject, #件名
		[string]$body      #本文
	)
	$sendusing = "http://schemas.microsoft.com/cdo/configuration/sendusing"
	$smtpserver = "http://schemas.microsoft.com/cdo/configuration/smtpserver"
	$smtpserverport = "http://schemas.microsoft.com/cdo/configuration/smtpserverport"
	$oMsg = New-Object -com "CDO.Message"
	$oMsg.From = $from
	$oMsg.To = $to
	$oMsg.Cc = $cc
	$oMsg.Bcc = $bcc
	$oMsg.Subject = $subject
	$oMsg.TextBody = $body
	$oMsg.BodyPart.Charset = "UTF-8"
	$oMsg.Configuration.Fields.Item($sendusing) = $SEND_USING
	$oMsg.Configuration.Fields.Item($smtpserver) = $SMTP_SERVER_MAIL_ADDRESS
	$oMsg.Configuration.Fields.Item($smtpserverport) = $SMTP_SERVER_PORT
	$oMsg.Configuration.Fields.Update()
	return $oMsg.Send()
}

#CDOメール送信（HTML用）
function SendMailHtmlBody
{
	param
	(
		[string]$from,     #送信元アドレス
		[string]$to,         #送信先アドレス
		[string]$cc,         #Cc 送信先アドレス
		[string]$bcc,       #非公開送信先アドレス
		[string]$subject, #件名
		[string]$body      #本文
	)
	$sendusing = "http://schemas.microsoft.com/cdo/configuration/sendusing"
	$smtpserver = "http://schemas.microsoft.com/cdo/configuration/smtpserver"
	$smtpserverport = "http://schemas.microsoft.com/cdo/configuration/smtpserverport"
	$oMsg = New-Object -com "CDO.Message"
	$oMsg.From = $from
	$oMsg.To = $to
	$oMsg.Cc = $cc
	$oMsg.Bcc = $bcc
	$oMsg.Subject = $subject
	$oMsg.HtmlBody = $body
	$oMsg.BodyPart.Charset = "UTF-8"
	$oMsg.Configuration.Fields.Item($sendusing) = $SEND_USING
	$oMsg.Configuration.Fields.Item($smtpserver) = $SMTP_SERVER_MAIL_ADDRESS
	$oMsg.Configuration.Fields.Item($smtpserverport) = $SMTP_SERVER_PORT
	$oMsg.Configuration.Fields.Update()
	return $oMsg.Send()
}

#[JunpDB].[dbo].[vMicPCA消費税率]から指定日の消費税率の取得
function GetTaxRate
{
	param
	(
		[DateTime]$today, #当日
		[ref][int]$result      #戻り値：消費税率
	)
	$result.Value = 0

	$conn = New-Object System.Data.SqlClient.SqlConnection $SQL_CONNECTION_STR
	$conn.Open()
	$cmd = $conn.CreateCommand()
	$cmd.Connection  = $conn
	$cmd.CommandText = "SELECT CONVERT(int, T.tax_rate2) as 消費税率 FROM [JunpDB].[dbo].[vMicPCA消費税率] as T" `
										+ " INNER JOIN (" `
										+ "SELECT MAX(R.tax_ymd) as ymd FROM [JunpDB].[dbo].[vMicPCA消費税率] as R WHERE R.tax_ymd <= " + $today.ToString("yyyyMMdd") + ") as S	ON T.tax_ymd = S.ymd"
	$adapter = New-Object System.Data.SqlClient.SqlDataAdapter $cmd;
	$dataTable = New-Object System.Data.DataTable;

	$adapter.Fill($dataTable)
	if ($dataTable.Rows.Count -gt 0)
	{
		$result.Value = $dataTable.Rows[0].item("消費税率")
        Write-Host $result.Value
        return $true
	}
    return $false
}

#価格から税込価格を取得する
function GetTax
{
	param
	(
		[int]$rate,                       #消費税率
		[RoundFraction]$round, #端数処理
		[int]$price,                     #価格
		[ref][int]$result              #戻り値：税込価格
	)
	$result.Value = 0
	if ($rate -gt 0 -And $price -gt 0)
	{
		[int]$collect = 0
		if ($round -eq [RoundFraction]::Cut)
		{
			#切り捨て
			$collect = 0
		}
		elseif ($round -eq [RoundFraction]::Round)
		{
			#四捨五入
			$correct = 50
		}
		else
		{
			#切り上げ
			$correct = 99
		}
		$result.Value = (price * tax_rate + correct) / 100
		Write-Host $result.Value
		return $true
	}
	return $false
}
