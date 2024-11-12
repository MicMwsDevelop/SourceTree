$connStr = "Server=TESTSV2;Database=charlieDB;User ID=ww_reader;Password=20150801;"
$conn = New-Object System.Data.SqlClient.SqlConnection $connStr

try
{
    $conn.Open()
    $cmd = $conn.CreateCommand()
    $cmd.Connection  = $conn
    $cmd.CommandText = "SELECT TOP 10 [fLisenceId], [fCustomerID] FROM [dbo].[T_USE_CLOUDDATA_LICENSE]"
    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter $cmd;
    $dataTable = New-Object System.Data.DataTable;

    $adapter.Fill($dataTable)
    foreach ($row in $dataTable.Rows)
    {
        Write-Output("LisenceId:" + $row.item("fLisenceId") + " CustomerID:" + $row.item("fCustomerID").tostring())
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
