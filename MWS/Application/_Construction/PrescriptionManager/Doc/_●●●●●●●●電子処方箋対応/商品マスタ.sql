/****** SSMS の SelectTopNRows コマンドのスクリプト  ******/
SELECT [sms_scd]
      ,[sms_mei]
      ,[sms_syskbn]
      ,[sms_mkbn]
      ,[sms_zkbn]
      ,[sms_jsk]
      ,[sms_tani]
      ,[sms_iri]
      ,[sms_skbn1]
      ,[sms_skbn2]
      ,[sms_skbn3]
      ,[sms_skbn4]
      ,[sms_skbn5]
      ,[sms_tax]
      ,[sms_komi]
      ,[sms_tketa]
      ,[sms_sketa]
      ,[sms_hyo]
      ,[sms_gen]
      ,[sms_bai1]
      ,[sms_bai2]
      ,[sms_bai3]
      ,[sms_bai4]
      ,[sms_bai5]
      ,[sms_upddate]
      ,[sms_rcd]
      ,[sms_ztan]
      ,[sms_state]
  FROM [JunpDB].[dbo].[vMicPCA商品マスタ]
		where [sms_scd] in
		(
		'800705'
		,'800706'
		,'800703'
		,'800704'
		,'800701'
		,'800702'
		,'800093'
		,'800094'
		)

