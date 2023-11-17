VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "Prediction"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'予測連絡用クラス

Public 売上月 As String
Public 営業部コード As String
Public 営業部名 As String
Public 拠点コード As String
Public 拠点名 As String
Public 担当者コード As String
Public 担当者 As String
Public 受注番号 As String
Public 顧客No As Long
Public 顧客名 As String
Public 商品コード As String
Public 数量 As Integer
Public 課金開始日 As String
Public 課金終了日 As String
Public 金額 As Long


'Public Sub InitializeMatome(ByVal rng As Range)
'    売上月 = rng(1).Value
'    営業部コード = rng(2).Value
'    営業部名 = rng(3).Value
'    拠点コード = rng(4).Value
'    拠点名 = rng(5).Value
'    担当者コード = rng(6).Value
'    担当者 = rng(7).Value
'    受注番号 = rng(8).Value
'    顧客No = rng(9).Value
'    顧客名 = rng(10).Value
'    商品コード = rng(11).Value
'    数量 = rng(12).Value
'    課金開始日 = rng(13).Value
'    課金終了日 = rng(14).Value
'    金額 = rng(15).Value
'End Sub

Public Sub InitializeES(ByVal rng As Range)
    売上月 = rng(1).Value
    営業部コード = rng(15).Value
    '営業部名 = rng(3).Value
    拠点コード = rng(16).Value
    拠点名 = rng(17).Value
    担当者コード = rng(13).Value
    担当者 = rng(14).Value
    受注番号 = rng(2).Value
    顧客No = rng(5).Value
    顧客名 = rng(7).Value
    商品コード = "800121"
    数量 = "1"
    課金開始日 = rng(24).Value
    課金終了日 = rng(25).Value
    金額 = rng(8).Value
End Sub

