################################################################
# CustMstConfirmCheck.ps1
# APPID150 CustMst動作チェック
#
# 処理内容
# Cust_mst.mdb実行後に出力されるcheck2.txtの確認。ファイルスタンプの日付が本日かどうかチェック。
# ファイルが存在しない。もしくはファイルスタンプの日付が本日でないない場合、mainte_info_sys@mic.jpに対し警告メールを送信
#
# Ver1.01(2021/05/17 勝呂):64bit対応の為、メール送信方式をbasp21からCDO方式に変更
# Ver1.02(2024/01/30 越田):2023/08組織変更対応 メール[Cust_mst Error]の宛先など整理
# Ver1.03(2025/08/07 勝呂):VBScriptからPowerShellに移植
################################################################
#
#Import-Module -Name "D:\SourceTree\PowerShell\Modules\CommonModule\CommonModule.psm1"
Import-Module -Name "C:\_PowerShell\Modules\CommonModule\CommonModule.psm1"

#検索対象ファイルパス名
Set-Variable -Name TargetPathname "c:\estore_common\check2.txt"

#Add-Type -Assembly System.Windows.Forms

if ($Args.Count -ne 1 -Or $Args[0].ToUpper() -ne "AUTO")
{
	#第一引数に AUTO が必要 ※誤動作の防止策
	Write-Output "Usage: CustMstConfirmCheck.ps1 AUTO"
	return
}
try
{
	$SendMailFlag = $false
	if (Test-Path $TargetPathname)
	{
		#check2.txtが存在する
		$file = Get-ChildItem $TargetPathname
		$lastdayStr = $file.LastWriteTime.ToString("yyyyMMdd")
		$todayStr = [datetime]::Now.ToString("yyyyMMdd")

		#debugコード
		#[System.Windows.Forms.MessageBox]::Show("check2.txt:" + [string]$lastdayStr + " 本日:" + [string]$todayStr)

		if ($lastdayStr -ne $todayStr)
		{
			#check2.txtのタイムスタンプが本日でない
			$SendMailFlag = $true
		}
		else
		{
			#check2.txtの削除
			Remove-Item -Path $TargetPathname
		}
	}
	else
	{
		#check2.txtが存在しない
		$SendMailFlag = $true
	}
	if ($SendMailFlag -eq $true)
	{
		#警告メールを送信
		$subject = "Cust_mst Error"
		$textBody = "Cust_mst.mdbが動作していない可能性があります。`n`n" + $BASE_DEPARTMENT
		#SendMailTextBody $MAINTE_MAIL_ADDRESS $BASE_MAIL_ADDRESS "" $MAINTE_MAIL_ADDRESS $subject $textBody
		SendMailTextBody $MAINTE_MAIL_ADDRESS "suguro@mic.jp" "" "suguro@mic.jp" $subject $textBody
	}
}
catch
{
	Write-Output $Error[0].Exception.Message
}
