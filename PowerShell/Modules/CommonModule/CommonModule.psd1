@{
	Author = 'Common Module'
	ModuleVersion = '1.00'
	Description = 'PowerShell common module'
	FunctionsToExport = '*'
	CmdletsToExport = '*'
	AliasesToExport = '*'
	VariablesToExport = '*'
	NestedModules = '*'

	#FunctionsToExport = @('SendMailTextBody', 'SendMailHtmlBody', 'GetTaxRate', 'GetTax')
	#CmdletsToExport = @('Get-MyOtherData', 'Set-MyOtherData')
	#AliasesToExport = @('Get-MyAlias')
	#VariablesToExport = @('MyGlobalVariable')
	#NestedModules = @('MyNestedModule.psm1') # 別のモジュールをネストする場合
	RootModule = 'CommonModule.psm1' # モジュール本体のスクリプトファイル
}
