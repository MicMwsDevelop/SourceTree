$connStr = "Server=TESTSV2;Database=charlieDB;User ID=ww_reader;Password=20150801;"
$conn = New-Object System.Data.SqlClient.SqlConnection $connStr

try
{
    $conn.Open()
    $cmd = $conn.CreateCommand()
    $cmd.Connection  = $conn
    $cmd.CommandText = "SELECT LC.LicCnt as recordCnt FROM (SELECT Count(*) as LicCnt FROM dbo.[T_USE_CLOUDDATA_LICENSE] WHERE fCustomerID is null) as LC WHERE LC.LicCnt < 2000"
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
        $sendusing = "http://schemas.microsoft.com/cdo/configuration/sendusing"
        $smtpserver = "http://schemas.microsoft.com/cdo/configuration/smtpserver"
        $smtpserverport = "http://schemas.microsoft.com/cdo/configuration/smtpserverport"
        $oMsg = New-Object -com "CDO.Message"
        $oMsg.From = "tasksv@mic.jp"
        $oMsg.To = "suguro@mic.jp"  #densan@mic.jp"
        $oMsg.Subject = "クラウドバックアップ ライセンスアラート"
        $oMsg.TextBody = "クラウドバックアップのライセンス残数が" + [string]$recCount + " 個になりました。`nライセンスを追加してください。`n`nシステム管理部"
        $oMsg.Configuration.Fields.Item($sendusing) = 2
        $oMsg.Configuration.Fields.Item($smtpserver) = "dove.mic.jp"
        $oMsg.Configuration.Fields.Item($smtpserverport) = 25
        $oMsg.Configuration.Fields.Update()
        $oMsg.Send()
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
