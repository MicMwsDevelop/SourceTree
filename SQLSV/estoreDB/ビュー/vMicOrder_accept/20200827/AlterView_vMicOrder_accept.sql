USE [estoreDB]
GO

/****** Object:  View [dbo].[vMicOrder_accept]    Script Date: 2020/08/28 11:40:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
    2009/01/16 vMicOrder_accept
        �v�����󒍃T�C�g�p�~�b�N�Г��p�󒍃f�[�^��M�T�[�o E-STORESV �̎󒍃f�[�^���Q�Ƃ���r���[
	2020/08/27 estore���֍�Ƃɔ����A�{�r���[�̎Q�Ɛ��E-STORESV����estoredb.dbo.order_accept�ɕύX���� by ���C
*/
ALTER VIEW [dbo].[vMicOrder_accept]
AS
SELECT          order_accept_id, order_no, customer_no, pref_arrival_date, goods_code, 
                      web_price, order_num, order_dt, send_dt
FROM            dbo.order_accept
GO

