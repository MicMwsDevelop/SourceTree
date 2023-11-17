  --ソフトウェア保守料計上２年目 売上：担当エリアベース

SELECT
        iif(dateadd(month, 11, H.[f納期]) >= '2022-02-01', '0' + convert(nvarchar, BR.[fPca部門コード]), CASE U.[営業部コード]
			WHEN '50' THEN '081'
			WHEN '60' THEN '083'
			WHEN '70' THEN '082'
			WHEN '75' THEN '086'
			WHEN '76' THEN '087'
			WHEN '80' THEN '085'
		END) AS 部門コード
	   ,iif(dateadd(month, 11, H.[f納期]) >= '2022-02-01', U.[営業部コード], CASE U.[拠点コード]
			WHEN '31' THEN '50'	--さいたま
			WHEN '33' THEN '75'	--横浜
			WHEN '52' THEN '75'	--金沢
			ELSE U.[営業部コード]
		END) AS 営業部コード
	   ,iif(dateadd(month, 11, H.[f納期]) >= '2022-02-01', U.[営業部名], CASE U.[拠点コード]
			WHEN '11' THEN '東日本営業部'
			WHEN '21' THEN '東日本営業部'
			WHEN '31' THEN '関東営業部'
			WHEN '33' THEN '関東営業部'
			WHEN '41' THEN '首都圏営業部'
			WHEN '51' THEN '中部営業部'
			WHEN '52' THEN '中部営業部'
			WHEN '61' THEN '関西営業部'
			WHEN '71' THEN '西日本営業部'
			WHEN '81' THEN '西日本営業部'
		END) AS 営業部名
	   ,U.[拠点コード] AS 拠点コード
	   ,iif(dateadd(month, 11, H.[f納期]) >= '2022-02-01', U.[拠点名], CASE U.[拠点コード]
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
		,H.[fユーザーコード] AS 顧客No
		,H.[fユーザー] AS 顧客名
		,D.[f受注番号] AS 受注番号
		,H.[f受注承認日] AS 受注承認日
		,H.[f売上承認日] AS 売上承認日
		,H.[f納期] AS 納期
		,H.[f販売種別] AS 販売種別
		,D.[f商品コード] AS 商品コード
		,D.[f商品名] AS 商品名
		,60000 as 売上金額
		,LEFT(convert(nvarchar, dateadd(month, 11, H.[f納期]), 111), 7) AS 計上2年目
		--,LEFT(convert(nvarchar, dateadd(month, 23, H.[f納期]), 111), 7) AS 計上3年目
		--,LEFT(convert(nvarchar, dateadd(month, 35, H.[f納期]), 111), 7) AS 計上4年目
		--,LEFT(convert(nvarchar, dateadd(month, 47, H.[f納期]), 111), 7) AS 計上5年目
		--,LEFT(convert(nvarchar, dateadd(month, 59, H.[f納期]), 111), 7) AS 計上6年目
		FROM [JunpDB].[dbo].[tMih受注詳細] AS D
		LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H ON D.[f年度] = H.[f年度] AND D.[f受注番号] = H.[f受注番号]
		LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー３] AS U ON U.[顧客No] = H.[fユーザーコード]
        LEFT JOIN [JunpDB].[dbo].[tMih支店情報] AS BR ON BR.[fBshCode2] = U.[営業部コード] AND BR.[fBshCode3] = U.[拠点コード]
		WHERE H.[f販売種別] = 1 AND H.[f売上承認日] is Not Null AND D.[f商品コード] in ('800161', '800162')
		ORDER BY H.[f売上承認日]  

