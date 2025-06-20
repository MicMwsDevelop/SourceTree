
Partial Class Distributor_Distributor
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '       If Me.IsPostBack Then
        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

        'Exit Sub
        'End If


        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

        Dim sqlstr As String
        Dim sqlstr2 As String

        '抽出文字列作成
        sqlstr = "SELECT * FROM dbo.vキャンペーン0 ORDER BY 本数 DESC"
        sqlstr2 = "SELECT 支店名,[41目標数],本数,ポイント,順序 FROM dbo.vキャンペーン2 ORDER BY 順序"

        'データソースセット
        '実環境
        SqlDataSource1.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='SalesDB'"
        SqlDataSource1.SelectCommand = sqlstr

        '実環境
        SqlDataSource2.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='SalesDB'"
        SqlDataSource2.SelectCommand = sqlstr2

        'ページは初期値
        GridView1.PageIndex = 0
        GridView2.PageIndex = 0

    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim sqlstr As String
        Dim sqlstr2 As String

        '抽出文字列作成

        '【代理店/特約店】
        sqlstr = "SELECT * FROM dbo.vキャンペーン0 ORDER BY 本数 DESC"
        'sqlstr = "SELECT CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = ''"
        'sqlstr = sqlstr & " THEN [顧客No] ELSE [fdl販売店グループ] END AS '顧客NoまたはグループNo',"
        'sqlstr = sqlstr & " CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = ''"
        'sqlstr = sqlstr & " THEN [f販売先] ELSE [販売店グループ] END AS '販売店名またはグループ名',"
        'sqlstr = sqlstr & " Sum(JunpDB.dbo.tMih受注詳細.f数量) AS 数量の合計,"
        'sqlstr = sqlstr & " Sum(JunpDB.dbo.tMih受注詳細.f提供価格) AS 提供価格の合計"

        'sqlstr = sqlstr & " FROM dbo.Distributor_特約代理店 INNER JOIN"
        'sqlstr = sqlstr & " (JunpDB.dbo.tMih受注ヘッダ INNER JOIN JunpDB.dbo.tMih受注詳細 ON"
        'sqlstr = sqlstr & " JunpDB.dbo.tMih受注ヘッダ.f受注番号 = JunpDB.dbo.tMih受注詳細.f受注番号) ON"
        'sqlstr = sqlstr & " dbo.Distributor_特約代理店.顧客No = JunpDB.dbo.tMih受注ヘッダ.[f販売先コード]"

        'sqlstr = sqlstr & " WHERE ((((JunpDB.dbo.tMih受注詳細.f区分)=1) OR ((JunpDB.dbo.tMih受注詳細.f区分)=202)) AND ((JunpDB.dbo.tMih受注ヘッダ.f売上承認日) Between '" & T_開始.Text & "' And '" & T_終了.Text & "'))"

        'sqlstr = sqlstr & " GROUP BY CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = '' THEN"
        'sqlstr = sqlstr & " [顧客No] ELSE [fdl販売店グループ] END,"
        'sqlstr = sqlstr & " CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = '' THEN"
        'sqlstr = sqlstr & " [f販売先] ELSE [販売店グループ] END"

        'sqlstr = sqlstr & " ORDER BY CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = '' THEN  [顧客No] ELSE [fdl販売店グループ] END"



        '【販売促進店】


        sqlstr2 = "SELECT 支店名,[41目標数],本数,ポイント,順序 FROM dbo.vキャンペーン2 ORDER BY 順序"
        'sqlstr2 = "SELECT CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = ''"
        'sqlstr2 = sqlstr2 & " THEN [顧客No] ELSE [fdl販売店グループ] END AS '顧客NoまたはグループNo',"
        'sqlstr2 = sqlstr2 & " CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = ''"
        'sqlstr2 = sqlstr2 & " THEN [契約先] ELSE [グループ名] END AS '販売店名またはグループ名',"
        'sqlstr2 = sqlstr2 & " Sum(dbo.Distributor_販売促進店伝票.f提供価格) AS 提供価格の合計"

        'sqlstr2 = sqlstr2 & " FROM dbo.Distributor_販売促進店 INNER JOIN"
        'sqlstr2 = sqlstr2 & " dbo.Distributor_販売促進店伝票 ON"
        'sqlstr2 = sqlstr2 & " dbo.Distributor_販売促進店.fps仕入先No = dbo.Distributor_販売促進店伝票.[手数料支払先コード]"

        'sqlstr2 = sqlstr2 & " WHERE (dbo.Distributor_販売促進店伝票.f売上承認日 Between '" & T_開始.Text & "' And '" & T_終了.Text & "')"

        'sqlstr2 = sqlstr2 & " GROUP BY CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = '' THEN"
        'sqlstr2 = sqlstr2 & " [顧客No] ELSE [fdl販売店グループ] END,"
        'sqlstr2 = sqlstr2 & " CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = '' THEN"
        'sqlstr2 = sqlstr2 & " [契約先] ELSE [グループ名] END"

        'sqlstr2 = sqlstr2 & " ORDER BY CASE WHEN [fdl販売店グループ] IS NULL OR [fdl販売店グループ] = '' THEN  [顧客No] ELSE [fdl販売店グループ] END"

        'データソースセット
        '実環境
        SqlDataSource1.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='SalesDB'"
        SqlDataSource1.SelectCommand = sqlstr

        '実環境
        SqlDataSource2.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='SalesDB'"
        SqlDataSource2.SelectCommand = sqlstr2

        'ページは初期値
        GridView1.PageIndex = 0
        GridView2.PageIndex = 0

    End Sub

    
End Class
