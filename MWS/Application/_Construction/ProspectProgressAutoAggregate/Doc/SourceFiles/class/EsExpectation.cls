VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "EsExpectation"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'ES売上予想クラス

Public 部門コード As String
Public 営業部名 As String
Public 拠点コード As String
Public 拠点名 As String
Public 顧客No As String
Public 顧客名 As String
Public 受注番号 As String
Public 受注承認日 As String
Public 売上承認日 As String
Public 納期 As String
Public 売上金額 As Long
Public 計上月 As String

Public Sub Initialize(ByVal rng As Range)
    部門コード = rng(1).Value
    営業部名 = rng(2).Value
    拠点コード = rng(3).Value
    拠点名 = rng(4).Value
    顧客No = rng(5).Value
    顧客名 = rng(6).Value
    受注番号 = rng(7).Value
    受注承認日 = rng(8).Value
    売上承認日 = rng(9).Value
    納期 = rng(10).Value
    売上金額 = rng(11).Value
    計上月 = rng(12).Value
End Sub


