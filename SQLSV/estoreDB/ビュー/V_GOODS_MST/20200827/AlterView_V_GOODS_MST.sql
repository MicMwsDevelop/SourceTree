USE [estoreDB]
GO

/****** Object:  View [dbo].[V_GOODS_MST]    Script Date: 2020/07/27 11:33:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[V_GOODS_MST]
/*
      [estoreDB].[dbo].[V_GOODS_MST]

      2020/08/27 estore���֍�Ƃɔ����ABO_ORDER_UNIT�t�B�[���h�̒ǉ� by ���C
*/
AS
SELECT WebSMS.ID AS GOODS_MST_ID, WebSMS.���i�R�[�h AS GOODS_CODE, RTRIM(PCASMS.sms_mei) AS GOODS_NAME, PCASMS.sms_bai1 AS GENERAL_PRICE, 
       WebSMS.web�񋟉��i AS WEB_PRICE, WebSMS.Q�ʏ�񋟉��i AS Q_GENERAL_PRICE, WebSMS.Qweb�񋟉��i AS Q_WEB_PRICE, 
       WebSMS.�J�e�S��No AS CATEGORY_NO, WebSMS.���i�J�e�S�� AS GOODS_CATEGORY, WebSMS.���i���� AS GOODS_COMMENT, WebSMS.�폜 AS DEL_FLG, 
       WebSMS.�\���J�n���� AS GOODS_VISIBLE_FROM_DT, WebSMS.�\���I������ AS GOODS_VISIBLE_UNTIL_DT, WebSMS.�t����� AS GOODS_ADD_INFO, 
       WebSMS.���i�ڍׂ̃����N�ݒ� AS GOODS_DETAIL_URL
       ,OS���׈�.fms�����P�� AS BO_ORDER_UNIT
FROM JunpDB.dbo.tMicWebSMS AS WebSMS
INNER JOIN JunpDB.dbo.vMicPCA���i�}�X�^ AS PCASMS ON WebSMS.���i�R�[�h = PCASMS.sms_scd
LEFT JOIN JunpDB.dbo.tMikOS���׈� AS OS���׈� ON OS���׈�.fms�󎚕K�v = '1' AND OS���׈�.fms�R�[�h��� <> '11'
          AND (WebSMS.���i�R�[�h = OS���׈�.fms���i�R�[�h�P OR WebSMS.���i�R�[�h = OS���׈�.fms���i�R�[�h2 OR WebSMS.���i�R�[�h = OS���׈�.fms���i�R�[�h3 OR WebSMS.���i�R�[�h = OS���׈�.fms���i�R�[�h4
				OR WebSMS.���i�R�[�h = OS���׈�.fms���i�R�[�h5 OR WebSMS.���i�R�[�h = OS���׈�.fms���i�R�[�h6 OR WebSMS.���i�R�[�h = OS���׈�.fms���i�R�[�h7 OR WebSMS.���i�R�[�h = OS���׈�.fms���i�R�[�h8)
WHERE NOT ((WebSMS.���i�R�[�h = '003501' AND OS���׈�.fms�R�[�h = '003') OR (WebSMS.���i�R�[�h = '003502' AND OS���׈�.fms�R�[�h = '003') OR (WebSMS.���i�R�[�h = '003503' AND OS���׈�.fms�R�[�h = '003') OR (WebSMS.���i�R�[�h = '003504' AND OS���׈�.fms�R�[�h = '003'))
GO

