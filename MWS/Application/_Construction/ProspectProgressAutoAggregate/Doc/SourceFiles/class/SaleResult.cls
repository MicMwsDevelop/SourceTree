VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "SaleResult"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'”„ãÀÑƒNƒ‰ƒX

Public ÀÑ“ú As Long
Public ‰c‹Æ•”ƒR[ƒh As String
Public —\ZVP As Integer
Public —\ZES As Integer
Public —\Z‚Ü‚Æ‚ß As Integer
Public —\Z”„ã As Long
Public —\Z‰c‹Æ‘¹‰v As Long
Public —\‘ªVP As Integer
Public —\‘ªES As Integer
Public —\‘ª‚Ü‚Æ‚ß As Integer
Public —\‘ª”„ã As Long
Public —\‘ª‰c‹Æ‘¹‰v As Long
Public ÀÑVP As Integer
Public ÀÑES As Integer
Public ÀÑ‚Ü‚Æ‚ß As Integer
Public ÀÑ”„ã As Long
Public ÀÑ‰c‹Æ‘¹‰v As Long

Public Sub Initialize(ByVal rng As Range)
    ÀÑ“ú = rng(1).Value
    ‰c‹Æ•”ƒR[ƒh = rng(2).Value
    —\ZVP = rng(3).Value
    —\ZES = rng(4).Value
    —\Z‚Ü‚Æ‚ß = rng(5).Value
    —\Z”„ã = rng(6).Value
    —\Z‰c‹Æ‘¹‰v = rng(7).Value
    —\‘ªVP = rng(8).Value
    —\‘ªES = rng(9).Value
    —\‘ª‚Ü‚Æ‚ß = rng(10).Value
    —\‘ª”„ã = rng(11).Value
    —\‘ª‰c‹Æ‘¹‰v = rng(12).Value
    ÀÑVP = rng(13).Value
    ÀÑES = rng(14).Value
    ÀÑ‚Ü‚Æ‚ß = rng(15).Value
    ÀÑ”„ã = rng(16).Value
    ÀÑ‰c‹Æ‘¹‰v = rng(17).Value
End Sub

