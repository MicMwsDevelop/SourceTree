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
    INSERT�g���K�[
      trgInsert_T_PRODUCT_CONTROL

      MWSID���s���AT_PRODUCT_CONTROL�̐V�K���R�[�h�ǉ�����T_USE_CLOUDDATA_LICENSE�̃��C�Z���X����
	  �VMWS���[�U�[�̌ڋqNo��R�Â���

    Ver.1.0 2020/10/09 ���� by ���C
*/
    DECLARE @�ڋqID         int
	DECLARE @�쐬����		datetime
    DECLARE @���C�Z���X     nvarchar(50)
    DECLARE @�o�^��         nvarchar(20)

    -- �ǉ������擾
    SELECT @�ڋqID   = CUSTOMER_ID
         , @�쐬���� = CREATE_DATE
		 , @�o�^��   = CREATE_PERSON
    FROM inserted
	WHERE PRODUCT_ID LIKE 'MWS%' AND (CUSTOMER_ID > 0 AND CUSTOMER_ID < 30000000)

	IF @�ڋqID > 0
	BEGIN
		-- �N���E�h�o�b�N�A�b�v���C�Z���X�Ǘ���񂩂�ڋqNo���ݒ肳��Ă��Ȃ���ԏ��������C�Z���X�ԍ����擾
		SELECT @���C�Z���X = Min(fLisenceId)
		FROM dbo.T_USE_CLOUDDATA_LICENSE
		WHERE dbo.T_USE_CLOUDDATA_LICENSE.fCustomerID is null

		BEGIN TRY
      
		  BEGIN TRANSACTION
        
			BEGIN
			  -- ���ڐݒ�f�[�^����蒷��������̓G���[�Ƃ����؂�̂Ăēo�^���邽�� WARNINGS OFF�Ƃ���
			  SET ANSI_WARNINGS OFF;

				IF @���C�Z���X <> ''
				BEGIN
				  -- �N���E�h�o�b�N�A�b�v���C�Z���X�Ǘ����ɌڋqNo��ݒ�
				  UPDATE dbo.T_USE_CLOUDDATA_LICENSE
				  SET   fCustomerID   = @�ڋqID 
					  , fUpdateDate   = @�쐬����
					  , fUpdatePerson = @�o�^��
				  WHERE (fLisenceId = @���C�Z���X)
				END
				ELSE
				BEGIN
				  --���C�Z���X�s�����ɂ�T_USE_CLOUDDATA_LICENSE_ERROR_USER�e�[�u���ɒǉ�
				  INSERT INTO dbo.T_USE_CLOUDDATA_LICENSE_ERROR_USER
				  (
					 [fCustomerID]
					,[fUpdateDate]
					,[fUpdatePerson]			  
				  )
				  VALUES
				  (
					 @�ڋqID
					,@�쐬����
					,@�o�^��
				  )
				END

			  -- WARNINGS ON �ɖ߂�
			  SET ANSI_WARNINGS ON;

			END
      
		  COMMIT TRANSACTION

		END TRY

		BEGIN CATCH
		  -- ���炩�̃G���[���������̂Ń��[���o�b�N�i���G���[�n���h�����O�j
		  ROLLBACK TRANSACTION
		END CATCH
	END

  RETURN

GO

ALTER TABLE [dbo].[T_PRODUCT_CONTROL] ENABLE TRIGGER [trgInsert_T_PRODUCT_CONTROL]
GO

