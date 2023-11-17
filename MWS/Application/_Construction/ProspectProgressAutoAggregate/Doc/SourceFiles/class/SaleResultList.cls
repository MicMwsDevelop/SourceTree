VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "SaleResultList"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'売上実績リストクラス

Public Items As Collection

'実績日と営業部コードに対応する実績情報の取得
Public Function GetIndex(dd As Long, code As String) As Integer
    Dim i As Integer
    For i = 1 To Items.Count
        If Items(i).実績日 = dd And Items(i).営業部コード = code Then
            GetIndex = i
            Exit Function
        End If
    Next i
    GetIndex = -1
End Function

