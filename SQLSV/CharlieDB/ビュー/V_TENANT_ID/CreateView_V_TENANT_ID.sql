USE [charlieDB]
GO

/****** Object:  View [dbo].[V_TENANT_ID]    Script Date: 2021/01/29 17:42:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--ALTER VIEW [dbo].[V_TENANT_ID]
CREATE VIEW [dbo].[V_TENANT_ID]
as
/*

    V_TENANT_ID

        Web�\���t�Ŏg�p����e�i���gID�̎Q�ƃr���[

        Ver.1.00 2021/01/29 ���� by ���C

*/
SELECT
    U.[�x�X�h�c] as ���_�R�[�h
   ,U.[�x�X��] as ���_��
   ,W.[fCustomerID] as �ڋqNo
   ,U.[�ڋq���P] + U.[�ڋq��2] as �ڋq��
   ,W.[fTenantID] as �e�i���gID
  FROM [charlieDB].[dbo].[T_USE_WEBAPPOINT_TENANTID] as W
  INNER JOIN [charlieDB].[dbo].[�ڋq�}�X�^�Q�ƃr���[] as U on W.[fCustomerID] = U.[�ڋq�h�c]
  WHERE W.[fCustomerID] is not null AND U.�I�� = '0'

GO

