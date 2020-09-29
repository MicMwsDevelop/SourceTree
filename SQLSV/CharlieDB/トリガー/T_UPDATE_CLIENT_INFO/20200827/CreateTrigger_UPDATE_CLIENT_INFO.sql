USE [charlieDB]
GO

/****** Object:  Trigger [dbo].[trgInsertT_UPDATE_CLIENT_INFO]    Script Date: 2020/06/09 11:22:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE  TRIGGER [dbo].[trgInsertT_UPDATE_CLIENT_INFO] ON [dbo].[T_UPDATE_CLIENT_INFO] 
FOR INSERT
AS
/*
    INSERTトリガー
      trgInsertT_UPDATE_CLIENT_INFO

      ＭＷＳの申込サイト、および、顧客情報変更サイトから
      顧客情報の更新・変更があった時に、COUPLERから
        CharlieDBの[T_UPDATE_CLIENT_INFO]
      に更新情報が追記書き込みされる。

      本INSERTトリガーにてWonderWebデータを更新する。

    Ver.1.0 2015/06/22 初版（yamakawa）
    Ver.1.1 2015/07/02 ふりがな項目など対象外
                       メモ登録内容変
    Ver.1.0 2015/08/06 開設者名の書き戻しをサポート（8/1の作業漏れ）
    Ver.1.1 2015/08/18 電話番号、FAX番号変更時、旧ではなく新文字列をメモ登録文字列に作成していたのを修正
    Ver.1.2 2015/08/19 メモ追加時、
                          [fMemType]の登録形式を "yyyy/mm/dd hh:mm ＭＷＳ" に変更
                          [fMemKubun]に'002'(顧客情報)をセット
    Ver.1.3 2015/10/02 顧客名ふりがな  全角ひらがな→半角ｶﾅ に変換して書き戻す
    Ver.1.4 2015/11/06 JunpDB.dbo.tMikユーザ.fusユーザー を '1'(MICユーザー)に設定を追加
    Ver.1.5 2020/08/27 Estoreメールアドレス、メルマガ購読フラグに変更があった時の処理を追加 by 勝呂

*/
    DECLARE @更新登録番号     int
    DECLARE @更新登録日時     datetime
    DECLARE @顧客ＩＤ         int
    DECLARE @顧客名１         nvarchar(255)
    DECLARE @顧客名２         nvarchar(255)
    DECLARE @顧客名ふりがな   nvarchar(255)
    DECLARE @メールアドレス   nvarchar(255)
    DECLARE @郵便番号         nvarchar(10)
    DECLARE @住所１           nvarchar(255)
    DECLARE @住所２           nvarchar(255)
--  DECLARE @住所ふりがな     nvarchar(255)
    DECLARE @電話番号         nvarchar(20)
    DECLARE @ＦＡＸ番号       nvarchar(20)
    DECLARE @開設者名         nvarchar(255)
--  DECLARE @医院長名         nvarchar(255)
--  DECLARE @医院長名ふりがな nvarchar(255)
    DECLARE @医療機関コード   nvarchar(20)
    DECLARE @支店コード       nvarchar(10)
    DECLARE @営業担当社員ID   nvarchar(20)
    DECLARE @申込種別         int
    
    DECLARE @Pre顧客ＩＤ         int
    DECLARE @Pre顧客名１         nvarchar(255)
    DECLARE @Pre顧客名２         nvarchar(255)
    DECLARE @Pre顧客名ふりがな   nvarchar(255)
    DECLARE @Preメールアドレス   nvarchar(255)
    DECLARE @Pre郵便番号         nvarchar(10)
    DECLARE @Pre住所１           nvarchar(255)
    DECLARE @Pre住所２           nvarchar(255)
    DECLARE @Pre電話番号         nvarchar(20)
    DECLARE @PreＦＡＸ番号       nvarchar(20)
    DECLARE @Pre開設者名         nvarchar(255)
    DECLARE @Pre医療機関コード   nvarchar(20)
    DECLARE @Pre支店コード       nvarchar(10)
    DECLARE @Pre営業担当社員ID   nvarchar(20)
    DECLARE @Pre申込種別         int
 
    DECLARE @更新登録者          nvarchar(30)
    DECLARE @顧客情報メモ        nvarchar(1000)
    DECLARE @申込種別メモ        nvarchar(1000)
    DECLARE @Pre申込種別文字列   nvarchar(20)
    DECLARE @Post申込種別文字列  nvarchar(20)
    DECLARE @来月文字列          nvarchar(10)

    DECLARE @Estoreメールアドレス nvarchar(255)
    DECLARE @PreEstoreメールアドレス nvarchar(255)
    DECLARE @メルマガ購読フラグ int
    DECLARE @Preメルマガ購読フラグ int
    DECLARE @estoreDB_UserClass CHAR(1)

    SELECT @更新登録者   = 'ＭＷＳ'                 -- ＷＷデータの更新者を'ＭＷＳ'とする

    -- 更新情報を取得
    SELECT @更新登録番号      = fUpdateNumber
         , @更新登録日時      = fUpdateEntryDate
         , @顧客ＩＤ          = fClientID
         , @顧客名１          = fClientName1
         , @顧客名２          = fClientName2
         , @顧客名ふりがな    = fClientNameKana
         , @メールアドレス    = fMailAddress
         , @郵便番号          = fZipCode
         , @住所１            = fLocationAddress1
         , @住所２            = fLocationAddress2
         , @電話番号          = fPhoneNumber
         , @ＦＡＸ番号        = fFaxNumber
         , @開設者名          = fEstablisherName
         , @医療機関コード    = fInstitutionCode
         , @支店コード        = tBranchCode           -- ＷｏｎｄｅｒＷｅｂ更新対象外
         , @営業担当社員ID    = tEmployeeID           -- ＷｏｎｄｅｒＷｅｂ更新対象外
         , @申込種別          = tApplyType            -- CharlieDB.T_CUSTOMER_FOUNDATIONS.APPLY_TYPEを更新
         , @Estoreメールアドレス = fEstoreMailAddress
         , @メルマガ購読フラグ = fMailMagazineFlg
    FROM inserted

    -- 更新前情報を取得
    SELECT @Pre顧客ＩＤ          = fClientID
         , @Pre顧客名１          = fClientName1
         , @Pre顧客名２          = fClientName2
         , @Pre顧客名ふりがな    = fClientNameKana
         , @Preメールアドレス    = fMailAddress
         , @Pre郵便番号          = fZipCode
         , @Pre住所１            = fLocationAddress1
         , @Pre住所２            = fLocationAddress2
         , @Pre電話番号          = fPhoneNumber
         , @PreＦＡＸ番号        = fFaxNumber
         , @Pre開設者名          = fEstablisherName
         , @Pre医療機関コード    = fInstitutionCode
         , @Pre支店コード        = tBranchCode           -- ＷｏｎｄｅｒＷｅｂ更新対象外
         , @Pre営業担当社員ID    = tEmployeeID           -- ＷｏｎｄｅｒＷｅｂ更新対象外
         , @Pre申込種別          = tApplyType            -- CharlieDB.T_CUSTOMER_FOUNDATIONS.APPLY_TYPEを更新
         , @PreEstoreメールアドレス = fEstoreMailAddress
         , @Preメルマガ購読フラグ = fMailMagazineFlg
    FROM dbo.V_CLIENT_INFO
    WHERE dbo.V_CLIENT_INFO.fClientID = @顧客ＩＤ

    -- 全角ひらがな→半角ｶﾅ
    SELECT @顧客名ふりがな = dbo.ToNarrow(dbo.ToKatakana(@顧客名ふりがな))

    -- メモ追記する更新前情報の文字列を作成
    SELECT @顧客情報メモ = ''
    SELECT @申込種別メモ = ''

    SET @estoreDB_UserClass = (SELECT 顧客No FROM estoreDB.dbo.tMicCust_mst WHERE estoreDB.dbo.tMicCust_mst.顧客No = @顧客ＩＤ)

    -- 顧客情報の変更
    IF (@Pre顧客名１<>@顧客名１) or (@Pre顧客名２<>@顧客名２) or (@Pre顧客名ふりがな<>@顧客名ふりがな)
      BEGIN
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）顧客名１：'       + @Pre顧客名１       + NCHAR(13) + NCHAR(10)
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）顧客名２：'       + @Pre顧客名２       + NCHAR(13) + NCHAR(10)
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）顧客名ふりがな：' + @Pre顧客名ふりがな + NCHAR(13) + NCHAR(10)
      END
    IF (@Preメールアドレス<>@メールアドレス)
      BEGIN
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）メールアドレス：' + @Preメールアドレス + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre郵便番号<>@郵便番号) or (@Pre住所１<>@住所１) or (@Pre住所２<>@住所２)
      BEGIN
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）郵便番号：'       + @Pre郵便番号       + NCHAR(13) + NCHAR(10)
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）住所１：'         + @Pre住所１         + NCHAR(13) + NCHAR(10)
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）住所２：'         + @Pre住所２         + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre電話番号<>@電話番号) or (@PreＦＡＸ番号<>@ＦＡＸ番号)
      BEGIN
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）電話番号：'       + @Pre電話番号       + NCHAR(13) + NCHAR(10)
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）ＦＡＸ番号：'     + @PreＦＡＸ番号     + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre開設者名<>@開設者名)
      BEGIN
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）開設者名：'       + @Pre開設者名       + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre医療機関コード<>@医療機関コード)
      BEGIN
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）医療機関コード：' + @Pre医療機関コード + NCHAR(13) + NCHAR(10)
      END
    IF (@PreEstoreメールアドレス<>@Estoreメールアドレス)
      BEGIN
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）Estoreメールアドレス：' + @PreEstoreメールアドレス + NCHAR(13) + NCHAR(10)
      END
    IF (@Preメルマガ購読フラグ<>@メルマガ購読フラグ)
      BEGIN
        SELECT @顧客情報メモ = @顧客情報メモ + '旧）メルマガ購読：' + @Preメルマガ購読フラグ + NCHAR(13) + NCHAR(10)
      END

    -- 申込種別の変更
    IF @Pre申込種別<>@申込種別
      BEGIN
        SELECT @Pre申込種別文字列  = (case @Pre申込種別 when 1 then 'バリューパック' when 2 then 'アップグレード' when 3 then '月額課金' else '' end)
        SELECT @Post申込種別文字列 = (case @申込種別    when 1 then 'バリューパック' when 2 then 'アップグレード' when 3 then '月額課金' else '' end)
        -- 来月文字列 を 'yyyy/MM' 形式で作成
        SELECT @来月文字列 = left(Convert(nvarchar, dateadd(month, 1, GetDate()) , 111), 7) 

        SELECT @申込種別メモ  = 'ＭＷＳ' +@Pre申込種別文字列 + '期間終了' + NCHAR(13) + NCHAR(10)
        SELECT @申込種別メモ = @申込種別メモ + @来月文字列 + '～' + @Post申込種別文字列 + '開始' + NCHAR(13) + NCHAR(10) + NCHAR(13) + NCHAR(10)
        SELECT @申込種別メモ = @申込種別メモ + '申込種別：' + @Pre申込種別文字列 + '→' + @Post申込種別文字列 + NCHAR(13) + NCHAR(10)
        SELECT @申込種別メモ = @申込種別メモ + 'に変更' + NCHAR(13) + NCHAR(10)
      END

    BEGIN TRY
      
      BEGIN TRANSACTION
        
        BEGIN
          -- 項目設定データ長より長い文字列はエラーとせず切り捨てて登録するため WARNINGS OFFとする
          SET ANSI_WARNINGS OFF;

          -- メモに追記
          IF @顧客情報メモ<>'' 
            BEGIN
              INSERT INTO JunpDB.dbo.tMemo
                 (fMemKey
                , fMemTable
                , fMemType
                , fMemMemo
                , fMemUpdate
                , fMemUpdateMan
                , fMemUrl
                , fMemOriginalPath1,fMemOriginalPath2,fMemOriginalPath3
                , fMemWlfID1,fMemWlfID2,fMemWlfID3
                , fMemCatID1,fMemCatID2,fMemCatID3
                , fMemKubun)
              VALUES
                 (@顧客ＩＤ
                , 'tClient'
                , (convert(nvarchar,@更新登録日時,111)+' '+left(convert(nvarchar,@更新登録日時,8),5)+' ＭＷＳ')
                , ('【顧客情報変更】' + NCHAR(13) + NCHAR(10) + 'ＭＷＳより更新' + NCHAR(13) + NCHAR(10) + @顧客情報メモ)
                , @更新登録日時
                , @更新登録者
                , NULL
                , NULL,NULL,NULL
                , NULL,NULL,NULL
                , 0,0,0
                ,'002')
            END

          IF @申込種別メモ<>''
            BEGIN
              INSERT INTO JunpDB.dbo.tMemo
                 (fMemKey
                , fMemTable
                , fMemType
                , fMemMemo
                , fMemUpdate
                , fMemUpdateMan
                , fMemUrl
                , fMemOriginalPath1,fMemOriginalPath2,fMemOriginalPath3
                , fMemWlfID1,fMemWlfID2,fMemWlfID3
                , fMemCatID1,fMemCatID2,fMemCatID3
                , fMemKubun)
              VALUES
                 (@顧客ＩＤ
                , 'tClient'
                , (convert(nvarchar,@更新登録日時,111)+' '+left(convert(nvarchar,@更新登録日時,8),5)+' ＭＷＳ')
                , ('【顧客情報変更】' + NCHAR(13) + NCHAR(10) + 'ＭＷＳより更新' + NCHAR(13) + NCHAR(10) + @申込種別メモ)
                , @更新登録日時
                , @更新登録者
                , NULL
                , NULL,NULL,NULL
                , NULL,NULL,NULL
                , 0,0,0
                ,'002')
            END

          -- tMikユーザ 更新
          UPDATE   JunpDB.dbo.tMikユーザ
            SET    fusﾒｰﾙｱﾄﾞﾚｽ        = @メールアドレス 
                 , fus開設者          = @開設者名
                 , fus医保医療コード  = @医療機関コード
                 , fusユーザー        = '1'
                 , fus更新日          = @更新登録日時
                 , fus更新者          = @更新登録者
            WHERE (fusCliMicID = @顧客ＩＤ)
          
          -- tMik基本情報 更新
          UPDATE   JunpDB.dbo.tMik基本情報
            SET    fkj顧客名２        = @顧客名２
                 , fkj郵便番号        = @郵便番号
                 , fkj住所１          = @住所１
                 , fkj住所２          = @住所２
                 , fkj電話番号        = @電話番号
                 , fkjファックス番号  = @ＦＡＸ番号
                 , fkj更新日          = @更新登録日時
                 , fkj更新者          = @更新登録者
                 , fkj状態1           = (case when isnull(fkj得意先情報,'')<>'' then 1 else 0 end)
                 , fkj状態2           = (case when isnull(fkj仕入先情報,'')<>'' then 1 else 0 end)
            WHERE (fkjCliMicID = @顧客ＩＤ)
          
          -- tClient 更新
          UPDATE   JunpDB.dbo.tClient
            SET    fCliName           = @顧客名１
                 , fCliYomi           = @顧客名ふりがな
                 , fCliUpdate         = @更新登録日時
                 , fCliUpdateMan      = @更新登録者
            WHERE (fCliID = @顧客ＩＤ)

          -- T_CUSTOMER_FOUNDATIONSに申込種別を更新
          UPDATE   dbo.T_CUSTOMER_FOUNDATIONS
            SET    APPLY_TYPE         = @申込種別
                 , UPDATE_DATE        = @更新登録日時
                 , UPDATE_PERSON      = @更新登録者
            WHERE (CUSTOMER_ID = @顧客ＩＤ)

          -- WARNINGS ON に戻す
          SET ANSI_WARNINGS ON;

        END
      
      COMMIT TRANSACTION

    END TRY

    BEGIN CATCH
      -- 何らかのエラーがあったのでロールバック（未エラーハンドリング）
      ROLLBACK TRANSACTION
    END CATCH

    IF @estoreDB_UserClass is not null
       BEGIN
           -- tMicCust_mst 更新
           UPDATE estoreDB.dbo.tMicCust_mst
           SET  メールアドレス = @Estoreメールアドレス
                  ,メルマガ購読 = @メルマガ購読フラグ
           WHERE (estoreDB.dbo.tMicCust_mst.顧客No = @顧客ＩＤ)
       END

  RETURN

GO

ALTER TABLE [dbo].[T_UPDATE_CLIENT_INFO] ENABLE TRIGGER [trgInsertT_UPDATE_CLIENT_INFO]
GO

