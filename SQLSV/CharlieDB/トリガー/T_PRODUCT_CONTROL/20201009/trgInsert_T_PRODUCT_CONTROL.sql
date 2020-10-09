USE [charlieDB]
GO

/****** Object:  Trigger [dbo].[trgInsert_T_PRODUCT_CONTROL]    Script Date: 2020/10/05 16:57:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  TRIGGER [dbo].[trgInsert_T_PRODUCT_CONTROL] ON [dbo].[T_PRODUCT_CONTROL] 
FOR INSERT
AS
/*
    INSERTトリガー
      trgInsert_T_PRODUCT_CONTROL

      MWSID発行時、T_PRODUCT_CONTROLの新規レコード追加時にT_USE_CLOUDDATA_LICENSEのライセンス情報に
	  新MWSユーザーの顧客Noを紐づける

    Ver.1.0 2020/10/09 初版 by 勝呂
*/
    DECLARE @顧客ID         int
	DECLARE @作成日時		datetime
    DECLARE @ライセンス     nvarchar(50)
    DECLARE @登録者         nvarchar(20)

    -- 追加情報を取得
    SELECT @顧客ID   = CUSTOMER_ID
         , @作成日時 = CREATE_DATE
		 , @登録者   = CREATE_PERSON
    FROM inserted
	WHERE PRODUCT_ID LIKE 'MWS%' AND (CUSTOMER_ID > 0 AND CUSTOMER_ID < 30000000)

	IF @顧客ID > 0
	BEGIN
		-- クラウドバックアップライセンス管理情報から顧客Noが設定されていない一番小さいライセンス番号を取得
		SELECT @ライセンス = Min(fLisenceId)
		FROM dbo.T_USE_CLOUDDATA_LICENSE
		WHERE dbo.T_USE_CLOUDDATA_LICENSE.fCustomerID is null

		BEGIN TRY
      
		  BEGIN TRANSACTION
        
			BEGIN
			  -- 項目設定データ長より長い文字列はエラーとせず切り捨てて登録するため WARNINGS OFFとする
			  SET ANSI_WARNINGS OFF;

				IF @ライセンス <> ''
				BEGIN
				  -- クラウドバックアップライセンス管理情報に顧客Noを設定
				  UPDATE dbo.T_USE_CLOUDDATA_LICENSE
				  SET   fCustomerID   = @顧客ID 
					  , fUpdateDate   = @作成日時
					  , fUpdatePerson = @登録者
				  WHERE (fLisenceId = @ライセンス)
				END
				ELSE
				BEGIN
				  --ライセンス不足時にはT_USE_CLOUDDATA_LICENSE_ERROR_USERテーブルに追加
				  INSERT INTO dbo.T_USE_CLOUDDATA_LICENSE_ERROR_USER
				  (
					 [fCustomerID]
					,[fUpdateDate]
					,[fUpdatePerson]			  
				  )
				  VALUES
				  (
					 @顧客ID
					,@作成日時
					,@登録者
				  )
				END

			  -- WARNINGS ON に戻す
			  SET ANSI_WARNINGS ON;

			END
      
		  COMMIT TRANSACTION

		END TRY

		BEGIN CATCH
		  -- 何らかのエラーがあったのでロールバック（未エラーハンドリング）
		  ROLLBACK TRANSACTION
		END CATCH
	END

  RETURN

GO

ALTER TABLE [dbo].[T_PRODUCT_CONTROL] ENABLE TRIGGER [trgInsert_T_PRODUCT_CONTROL]
GO

