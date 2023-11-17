Dim inFileName : inFileName = Wscript.Arguments(0)
Dim fso : Set fso = CreateObject("Scripting.FileSystemObject")
Dim inFile : Set inFile = fso.GetFile(inFileName)
Dim outPath : outPath = inFile.ParentFolder.Path & "\SourceFiles"

If fso.FileExists(inFileName) <> True Then
	MsgBox inFileName & vbCrLf & "は存在しません。", vbCritical
	WScript.Quit()
End If

If fso.FolderExists(outPath) <> True Then
	fso.CreateFolder(outPath)
End If

' タイプ別のフォルダ定義と作成
Dim moduleFolder : moduleFolder = outPath & "\module\"
Dim classFolder : classFolder = outPath & "\class\"
Dim formFolder : formFolder = outPath & "\form\"
Dim sheetFolder : sheetFolder = outPath & "\sheet\"
If fso.FolderExists(moduleFolder) <> True Then fso.CreateFolder(moduleFolder)
If fso.FolderExists(classFolder) <> True Then fso.CreateFolder(classFolder)
If fso.FolderExists(formFolder) <> True Then fso.CreateFolder(formFolder)
If fso.FolderExists(sheetFolder) <> True Then fso.CreateFolder(sheetFolder)

Dim objExcel: Set objExcel = CreateObject("Excel.Application")
objExcel.Visible = False
objExcel.DisplayAlerts = False
objExcel.EnableEvents = False
Dim objWorkBook : Set objWorkBook = objExcel.Workbooks.Open(inFileName)

'ソースをエクスポートする
    For Each TempComponent In objWorkBook.VBProject.VBComponents
        If TempComponent.CodeModule.CountOfDeclarationLines <> TempComponent.CodeModule.CountOfLines Then
            Select Case TempComponent.Type
                'STANDARD_MODULE
                Case 1
                    TempComponent.Export moduleFolder & "\" & TempComponent.Name & ".bas"
                'CLASS_MODULE
                Case 2
                    TempComponent.Export classFolder & "\" & TempComponent.Name & ".cls"
                'USER_FORM
                Case 3
                    TempComponent.Export formFolder & "\" & TempComponent.Name & ".frm"
                'SHEETとThisWorkBook
                Case 100
                    TempComponent.Export sheetFolder & "\" & TempComponent.Name & ".bas"
            End Select
            With TempComponent.CodeModule
                'Code = .Lines(1, .CountOfLines)
                'Code = .Lines(.CountOfDeclarationLines + 1, .CountOfLines - .CountOfDeclarationLines + 1)                    
            End With
        End If
    Next

'Excelをクローズ
Set FSO = nothing
Set objParams = nothing
objExcel.DisplayAlerts = True
objExcel.EnableEvents = True
objWorkBook.Close False
objExcel.Quit
Set objWorkBook = nothing
Set objExcel = nothing

MsgBox "エクスポートが完了しました。"
