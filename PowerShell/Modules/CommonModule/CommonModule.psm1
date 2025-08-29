################################################################
# CommonModule.psm1
# PowerShell���ʃ��W���[��
#
# Ver1.00(2025/08/07 ���C):�V�K�쐬
################################################################
#
#SQL Server�ڑ�������
Set-Variable -Name SQL_CONNECTION_STR -Value "Server=SQLSV;Database=charlieDB;User ID=ww_reader;Password=20150801;" -Scope Global
Set-Variable -Name SQL_CONNECTION_STR_TEST -Value "Server=TESTSV2;Database=charlieDB;User ID=ww_reader;Password=20150801;" -Scope Global

#�S������
Set-Variable -Name BASE_DEPARTMENT -Value "�V�X�e���Ǘ���" -Scope Global

#���[���A�h���X
Set-Variable -Name BASE_MAIL_ADDRESS -Value "�V�X�e���Ǘ���<sys_kanri@mic.jp>" -Scope Global
Set-Variable -Name MAINTE_MAIL_ADDRESS -Value "MIC�Г��V�X�e���������M<mainte_info_sys@mic.jp>" -Scope Global
Set-Variable -Name ESTORE_MAIL_ADDRESS -Value "������Ѓ~�b�N�@e store <mic-estore@mic.jp>" -Scope Global

#���[�����M�ݒ�l
Set-Variable -Name SEND_USING -Value 2 -Option Constant
Set-Variable -Name SMTP_SERVER_MAIL_ADDRESS -Value "dove.mic.jp" -Option Constant
Set-Variable -Name SMTP_SERVER_PORT -Value 25 -Option Constant

#�[������
enum RoundFraction
{
	Cut      #�؂�̂�
	Round  #�l�̌ܓ�
	Raise    #�؂�グ
}

#CDO���[�����M�i�e�L�X�g�p�j
function SendMailTextBody
{
	param
	(
		[string]$from,     #���M���A�h���X
		[string]$to,         #���M��A�h���X
		[string]$cc,         #Cc ���M��A�h���X
		[string]$bcc,       #����J���M��A�h���X
		[string]$subject, #����
		[string]$body      #�{��
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

#CDO���[�����M�iHTML�p�j
function SendMailHtmlBody
{
	param
	(
		[string]$from,     #���M���A�h���X
		[string]$to,         #���M��A�h���X
		[string]$cc,         #Cc ���M��A�h���X
		[string]$bcc,       #����J���M��A�h���X
		[string]$subject, #����
		[string]$body      #�{��
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

#[JunpDB].[dbo].[vMicPCA����ŗ�]����w����̏���ŗ��̎擾
function GetTaxRate
{
	param
	(
		[DateTime]$today, #����
		[ref][int]$result      #�߂�l�F����ŗ�
	)
	$result.Value = 0

	$conn = New-Object System.Data.SqlClient.SqlConnection $SQL_CONNECTION_STR
	$conn.Open()
	$cmd = $conn.CreateCommand()
	$cmd.Connection  = $conn
	$cmd.CommandText = "SELECT CONVERT(int, T.tax_rate2) as ����ŗ� FROM [JunpDB].[dbo].[vMicPCA����ŗ�] as T" `
										+ " INNER JOIN (" `
										+ "SELECT MAX(R.tax_ymd) as ymd FROM [JunpDB].[dbo].[vMicPCA����ŗ�] as R WHERE R.tax_ymd <= " + $today.ToString("yyyyMMdd") + ") as S	ON T.tax_ymd = S.ymd"
	$adapter = New-Object System.Data.SqlClient.SqlDataAdapter $cmd;
	$dataTable = New-Object System.Data.DataTable;

	$adapter.Fill($dataTable)
	if ($dataTable.Rows.Count -gt 0)
	{
		$result.Value = $dataTable.Rows[0].item("����ŗ�")
        Write-Host $result.Value
        return $true
	}
    return $false
}

#���i����ō����i���擾����
function GetTax
{
	param
	(
		[int]$rate,                       #����ŗ�
		[RoundFraction]$round, #�[������
		[int]$price,                     #���i
		[ref][int]$result              #�߂�l�F�ō����i
	)
	$result.Value = 0
	if ($rate -gt 0 -And $price -gt 0)
	{
		[int]$collect = 0
		if ($round -eq [RoundFraction]::Cut)
		{
			#�؂�̂�
			$collect = 0
		}
		elseif ($round -eq [RoundFraction]::Round)
		{
			#�l�̌ܓ�
			$correct = 50
		}
		else
		{
			#�؂�グ
			$correct = 99
		}
		$result.Value = (price * tax_rate + correct) / 100
		Write-Host $result.Value
		return $true
	}
	return $false
}
