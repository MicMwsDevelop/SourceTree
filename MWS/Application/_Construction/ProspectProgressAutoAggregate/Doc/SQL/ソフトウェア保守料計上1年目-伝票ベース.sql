/****** SSMS の SelectTopNRows コマンドのスクリプト  ******/

--ソフトウェア保守料計上１年目 売上：伝票ベース

SELECT 
        iif(H.[f売上承認日] >= '2022-02-01', '0' + convert(nvarchar, BR.[fPca部門コード]), CASE H.[fBshCode2]
			WHEN '50' THEN '081'
			WHEN '60' THEN '083'
			WHEN '70' THEN '082'
			WHEN '75' THEN '086'
			WHEN '76' THEN '087'
			WHEN '80' THEN '085'
		END) AS 部門コード
		,H.[fBshCode2] AS 営業部コード
		,iif(H.[f売上承認日] >= '2022-02-01', BU.[fBshName2], CASE H.[fBshCode2]
			WHEN '50' THEN '東日本営業部'
			WHEN '60' THEN '関東営業部'
			WHEN '70' THEN '首都圏営業部'
			WHEN '75' THEN '中部営業部'
			WHEN '76' THEN '関西営業部'
			WHEN '80' THEN '西日本営業部'
		END) AS 営業部名
       ,H.[fBshCode3] AS 拠点コード
	   ,iif(H.[f売上承認日] >= '2022-02-01', BU.[fBshName3], CASE H.[fBshCode3]
			WHEN '11' THEN '札幌'
			WHEN '21' THEN '仙台'
			WHEN '31' THEN 'さいたま'
			WHEN '33' THEN '横浜'
			WHEN '41' THEN '首都圏'
			WHEN '51' THEN '名古屋'
			WHEN '52' THEN '金沢'
			WHEN '61' THEN '大阪'
			WHEN '71' THEN '広島'
			WHEN '81' THEN '福岡'
		END) AS 拠点名
      --,H.[f年度]
      ,H.[fユーザーコード] AS 顧客No
      ,H.[fユーザー] AS 顧客名
      ,H.[f受注番号] AS 受注番号
      ,H.[f受注承認日] AS 受注承認日
      ,H.[f売上承認日] AS 売上承認日
	  ,H.[f納期] AS 納期
	  ,H.[f販売種別] AS 販売種別
      ,[f商品コード] AS 商品コード
      --,[f表示順]
      --,[f区分]
      --,[f区分名]
      ,[f商品名] AS 商品名
      --,[f数量]
      --,[f標準価格]
      ,60000 AS 売上金額
      --,[f提供価格]
      --,[f税区分]
      --,[f税率]
      --,[f税込区分]
      --,[f売上原価]
      --,[f掛率]
      --,[f商品区分1]
      --,[f商品区分2]
      ,FORMAT(H.[f売上承認日], 'yyyy/MM') AS 計上1年目
      --,[fSV利用開始年月]
      --,[fSV利用終了年月]
  FROM [JunpDB].[dbo].[tMih受注詳細] as D
  LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H ON D.[f年度] = H.[f年度] AND D.[f受注番号] = H.[f受注番号]
  LEFT JOIN [JunpDB].[dbo].[tBusho] AS BU ON BU.[fBshCode3] = H.[fBshCode3]
  LEFT JOIN [JunpDB].[dbo].[tMih支店情報] AS BR ON BR.[fBshCode2] = H.[fBshCode2] AND BR.[fBshCode3] = H.[fBshCode3]
  WHERE H.[f販売種別] = 1 AND H.[f売上承認日] is Not Null AND D.[f商品コード] in ('800161', '800162')
  ORDER BY H.[f売上承認日]  
  


  
  
  
  
  
