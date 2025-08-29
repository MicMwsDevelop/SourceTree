################################################################
# EstoreShippingMailCheck.ps1
# APPID148 estore出荷メールチェック
#
# 処理内容
# 平日の17:40にestore出荷済メール ログファイルを取り込んでmic-estore@mic.jpに対しメールを送信する
# 佐川急便用ファイル：maillogyyyyMMdd.txt、ヤマト運輸用ファイル：maillogYyyyyMMdd.txt
#
# Ver1.01(2021/05/18 勝呂):64bit対応の為、メール送信方式をbasp21からCDO方式に変更
# Ver1.02(2024/01/30 越田):2023/08組織変更対応 メール[yyyymmddestore出荷メール送信][yyyymmdd確認estore出荷メールError]の宛先など整理
# Ver1.03(2024/02/08 越田):メール送信ログファイルが0byteの場合にエラー(アプリ強制終了)になる障害を修正
# Ver1.04(2024/02/24 越田):メール[yyyymmdd確認estore出荷メールError]が送信されない障害修正(Ver1.02対応の障害)
# Ver1.05(2025/08/06 勝呂):VBScriptからPowerShellに移植
################################################################
#
#Import-Module -Name "D:\SourceTree\PowerShell\Modules\CommonModule\CommonModule.psm1"
Import-Module -Name "C:\_PowerShell\Modules\CommonModule\CommonModule.psm1"

#出荷メール格納フォルダ\maillog
Set-Variable -Name TargetFolder "c:\estore_common\maillog\maillog"

if ($Args.Count -ne 1 -Or $Args[0].ToUpper() -ne "AUTO")
{
	#第一引数に AUTO が必要 ※誤動作の防止策
	Write-Output "Usage: EstoreShippingMailCheck.ps1 AUTO"
	return
}
try
{
	#本日（yyyyMMdd）
	$dateStr = [datetime]::Now.ToString("yyyyMMdd")

	#佐川用ファイル名生成：maillogyyyyMMdd.txt
	$SagawaPathname = $TargetFolder + $dateStr + ".txt"

	#ヤマト用ファイル名生成：maillogYyyyyMMdd.txt
	$YamatoPathname = $TargetFolder + "Y" + $dateStr + ".txt"

	$sagawaArray
	if (Test-Path $SagawaPathname)
	{
		#佐川用ファイルが存在する
		$sagawaArray = Get-Content -Path $SagawaPathname  #配列読込
	}
	$yamatoArray
	if (Test-Path $YamatoPathname)
	{
		#ヤマト用ファイルが存在する
		$yamatoArray = Get-Content -Path $YamatoPathname  #配列読込
	}
	$weekDay = (Get-Date).DayOfWeek -as [int]

	#debugコード
	#[System.Windows.Forms.MessageBox]::Show("曜日:" + [string]$weekDay)

	if ($sagawaArray -ne $null -Or $yamatoArray -ne $null)
	{
		#出荷リストが存在する
		#estore出荷メール送信
		$subject = $dateStr + "estore出荷メール送信"
		$textBody
		if ($sagawaArray.Count -gt 0)
		{
			#佐川分
			$textBody = "【佐川】`n"
			$textBody += $sagawaArray | Out-String  #配列を文字列に変換
		}
		if ($yamatoArray.Count -gt 0)
		{
			#ヤマト分
			$textBody += "【ヤマト】`n"
			$textBody += $yamatoArray | Out-String  #配列を文字列に変換
		}
		#SendMailTextBody $MAINTE_MAIL_ADDRESS $ESTORE_MAIL_ADDRESS "" $MAINTE_MAIL_ADDRESS $subject $textBody
		SendMailTextBody $MAINTE_MAIL_ADDRESS "suguro@mic.jp" "" "suguro@mic.jp" $subject $textBody
	}
	elseif ($weekDay -ne 0 -And $weekDay -ne 6)
	{
		#出荷リストが存在しない。日曜日、土曜日以外
		#確認estore出荷メールErrorメール送信
		$subject = $dateStr + "確認estore出荷メールError"
		$textBody = "出荷メールが送信されていない可能性があります。`n`n※MIC休日(祝日、長期休暇)の本メールは無視して問題ありません。"
		#SendMailTextBody $MAINTE_MAIL_ADDRESS $ESTORE_MAIL_ADDRESS "" $MAINTE_MAIL_ADDRESS $subject $textBody
		SendMailTextBody $MAINTE_MAIL_ADDRESS "suguro@mic.jp" "" "suguro@mic.jp" $subject $textBody
	}
}
catch
{
	Write-Output $Error[0].Exception.Message
}
