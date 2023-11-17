VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "SaleExpectation"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'売上予想クラス

Public 売上区分 As String
Public 部門コード As String
Public 部門名 As String
Public 商品区分コード As String
Public 商品区分名 As String
Public 金額 As Long

Public Sub Initialize(ByVal rng As Range)
    売上区分 = rng(1).Value
    部門コード = rng(2).Value
    部門名 = rng(3).Value
    商品区分コード = rng(4).Value
    商品区分名 = rng(5).Value
    金額 = rng(6).Value
End Sub

