VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "PredictionList"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'予測連絡用リストクラス

Public Items As Collection

'指定された販売情報に該当するデータが存在するか？
Public Function IsExists(target As Prediction) As Boolean
    For Each Item In Items
        If Item.顧客No = target.顧客No And Item.課金開始日 = target.課金開始日 And Item.課金終了日 = target.課金終了日 Then
            IsExists = True
            Exit Function
        End If
    Next Item
    IsExists = False
End Function

'件数の取得
Public Function GetCount(YM As String, 営業部コード As String) As Integer
    Dim ret As Integer: ret = 0
    For Each Item In Items
        If Item.営業部コード = 営業部コード And Item.売上月 = YM Then
            ret = ret + 1
        End If
    Next Item
    GetCount = ret
End Function

'palette売上の取得
Public Function GetPalettePrice(YM As String, 営業部コード As String) As Long
    Dim ret As Long: ret = 0
    For Each Item In Items
        If Item.営業部コード = 営業部コード And Item.売上月 = YM Then
            ret = ret + Item.金額
        End If
    Next Item
    GetPalettePrice = ret
End Function

'予測連絡用-まとめ集計結果のクリア
Public Sub ItemClear()
    Dim index As Long
    For index = 1 To Items.Count
        Items.Remove 1
    Next
End Sub
