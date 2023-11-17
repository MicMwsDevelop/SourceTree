VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "EsExpectationList"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'ES売上予想リストクラス

Public Items As Collection

'部門コードに対するデータの取得
Public Function GetPrice(部門コード As String, 計上月 As String) As Long
    Dim ret As Long: ret = 0
    For Each Item In Items
        If Item.部門コード = 部門コード And Item.計上月 = 計上月 Then
            ret = ret + Item.売上金額
        End If
    Next Item
    GetPrice = ret
End Function

