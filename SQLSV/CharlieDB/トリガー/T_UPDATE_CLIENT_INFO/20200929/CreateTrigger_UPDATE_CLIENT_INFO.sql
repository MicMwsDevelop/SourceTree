USE [charlieDB]
GO

/****** Object:  Trigger [dbo].[trgInsertT_UPDATE_CLIENT_INFO]    Script Date: 2020/09/29 15:59:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE  TRIGGER [dbo].[trgInsertT_UPDATE_CLIENT_INFO] ON [dbo].[T_UPDATE_CLIENT_INFO] 
FOR INSERT
AS
/*
    INSERT�g���K�[
      trgInsertT_UPDATE_CLIENT_INFO

      �l�v�r�̐\���T�C�g�A����сA�ڋq���ύX�T�C�g����
      �ڋq���̍X�V�E�ύX�����������ɁACOUPLER����
        CharlieDB��[T_UPDATE_CLIENT_INFO]
      �ɍX�V��񂪒ǋL�������݂����B

      �{INSERT�g���K�[�ɂ�WonderWeb�f�[�^���X�V����B

    Ver.1.0 2015/06/22 ���Łiyamakawa�j
    Ver.1.1 2015/07/02 �ӂ肪�ȍ��ڂȂǑΏۊO
                       �����o�^���e��
    Ver.1.0 2015/08/06 �J�ݎҖ��̏����߂����T�|�[�g�i8/1�̍�ƘR��j
    Ver.1.1 2015/08/18 �d�b�ԍ��AFAX�ԍ��ύX���A���ł͂Ȃ��V������������o�^������ɍ쐬���Ă����̂��C��
    Ver.1.2 2015/08/19 �����ǉ����A
                          [fMemType]�̓o�^�`���� "yyyy/mm/dd hh:mm �l�v�r" �ɕύX
                          [fMemKubun]��'002'(�ڋq���)���Z�b�g
    Ver.1.3 2015/10/02 �ڋq���ӂ肪��  �S�p�Ђ炪�ȁ����p�� �ɕϊ����ď����߂�
    Ver.1.4 2015/11/06 JunpDB.dbo.tMik���[�U.fus���[�U�[ �� '1'(MIC���[�U�[)�ɐݒ��ǉ�
    Ver.1.5 2020/08/27 Estore���[���A�h���X�A�����}�K�w�ǃt���O�ɕύX�����������̏�����ǉ� by ���C
	Ver.1.6 2020/09/29 �����}�K�w�ǕύX���ɕ�����ϊ��G���[��Insert Into�Ɏ��s by ���C

*/
    DECLARE @�X�V�o�^�ԍ�     int
    DECLARE @�X�V�o�^����     datetime
    DECLARE @�ڋq�h�c         int
    DECLARE @�ڋq���P         nvarchar(255)
    DECLARE @�ڋq���Q         nvarchar(255)
    DECLARE @�ڋq���ӂ肪��   nvarchar(255)
    DECLARE @���[���A�h���X   nvarchar(255)
    DECLARE @�X�֔ԍ�         nvarchar(10)
    DECLARE @�Z���P           nvarchar(255)
    DECLARE @�Z���Q           nvarchar(255)
--  DECLARE @�Z���ӂ肪��     nvarchar(255)
    DECLARE @�d�b�ԍ�         nvarchar(20)
    DECLARE @�e�`�w�ԍ�       nvarchar(20)
    DECLARE @�J�ݎҖ�         nvarchar(255)
--  DECLARE @��@����         nvarchar(255)
--  DECLARE @��@�����ӂ肪�� nvarchar(255)
    DECLARE @��Ë@�փR�[�h   nvarchar(20)
    DECLARE @�x�X�R�[�h       nvarchar(10)
    DECLARE @�c�ƒS���Ј�ID   nvarchar(20)
    DECLARE @�\�����         int
    
    DECLARE @Pre�ڋq�h�c         int
    DECLARE @Pre�ڋq���P         nvarchar(255)
    DECLARE @Pre�ڋq���Q         nvarchar(255)
    DECLARE @Pre�ڋq���ӂ肪��   nvarchar(255)
    DECLARE @Pre���[���A�h���X   nvarchar(255)
    DECLARE @Pre�X�֔ԍ�         nvarchar(10)
    DECLARE @Pre�Z���P           nvarchar(255)
    DECLARE @Pre�Z���Q           nvarchar(255)
    DECLARE @Pre�d�b�ԍ�         nvarchar(20)
    DECLARE @Pre�e�`�w�ԍ�       nvarchar(20)
    DECLARE @Pre�J�ݎҖ�         nvarchar(255)
    DECLARE @Pre��Ë@�փR�[�h   nvarchar(20)
    DECLARE @Pre�x�X�R�[�h       nvarchar(10)
    DECLARE @Pre�c�ƒS���Ј�ID   nvarchar(20)
    DECLARE @Pre�\�����         int
 
    DECLARE @�X�V�o�^��          nvarchar(30)
    DECLARE @�ڋq��񃁃�        nvarchar(1000)
    DECLARE @�\����ʃ���        nvarchar(1000)
    DECLARE @Pre�\����ʕ�����   nvarchar(20)
    DECLARE @Post�\����ʕ�����  nvarchar(20)
    DECLARE @����������          nvarchar(10)

    DECLARE @Estore���[���A�h���X nvarchar(255)
    DECLARE @PreEstore���[���A�h���X nvarchar(255)
    DECLARE @�����}�K�w�ǃt���O int
    DECLARE @Pre�����}�K�w�ǃt���O int
    DECLARE @estoreDB_UserClass CHAR(1)

    SELECT @�X�V�o�^��   = '�l�v�r'                 -- �v�v�f�[�^�̍X�V�҂�'�l�v�r'�Ƃ���

    -- �X�V�����擾
    SELECT @�X�V�o�^�ԍ�      = fUpdateNumber
         , @�X�V�o�^����      = fUpdateEntryDate
         , @�ڋq�h�c          = fClientID
         , @�ڋq���P          = fClientName1
         , @�ڋq���Q          = fClientName2
         , @�ڋq���ӂ肪��    = fClientNameKana
         , @���[���A�h���X    = fMailAddress
         , @�X�֔ԍ�          = fZipCode
         , @�Z���P            = fLocationAddress1
         , @�Z���Q            = fLocationAddress2
         , @�d�b�ԍ�          = fPhoneNumber
         , @�e�`�w�ԍ�        = fFaxNumber
         , @�J�ݎҖ�          = fEstablisherName
         , @��Ë@�փR�[�h    = fInstitutionCode
         , @�x�X�R�[�h        = tBranchCode           -- �v�����������v�����X�V�ΏۊO
         , @�c�ƒS���Ј�ID    = tEmployeeID           -- �v�����������v�����X�V�ΏۊO
         , @�\�����          = tApplyType            -- CharlieDB.T_CUSTOMER_FOUNDATIONS.APPLY_TYPE���X�V
         , @Estore���[���A�h���X = fEstoreMailAddress
         , @�����}�K�w�ǃt���O = fMailMagazineFlg
    FROM inserted

    -- �X�V�O�����擾
    SELECT @Pre�ڋq�h�c          = fClientID
         , @Pre�ڋq���P          = fClientName1
         , @Pre�ڋq���Q          = fClientName2
         , @Pre�ڋq���ӂ肪��    = fClientNameKana
         , @Pre���[���A�h���X    = fMailAddress
         , @Pre�X�֔ԍ�          = fZipCode
         , @Pre�Z���P            = fLocationAddress1
         , @Pre�Z���Q            = fLocationAddress2
         , @Pre�d�b�ԍ�          = fPhoneNumber
         , @Pre�e�`�w�ԍ�        = fFaxNumber
         , @Pre�J�ݎҖ�          = fEstablisherName
         , @Pre��Ë@�փR�[�h    = fInstitutionCode
         , @Pre�x�X�R�[�h        = tBranchCode           -- �v�����������v�����X�V�ΏۊO
         , @Pre�c�ƒS���Ј�ID    = tEmployeeID           -- �v�����������v�����X�V�ΏۊO
         , @Pre�\�����          = tApplyType            -- CharlieDB.T_CUSTOMER_FOUNDATIONS.APPLY_TYPE���X�V
         , @PreEstore���[���A�h���X = fEstoreMailAddress
         , @Pre�����}�K�w�ǃt���O = fMailMagazineFlg
    FROM dbo.V_CLIENT_INFO
    WHERE dbo.V_CLIENT_INFO.fClientID = @�ڋq�h�c

    -- �S�p�Ђ炪�ȁ����p��
    SELECT @�ڋq���ӂ肪�� = dbo.ToNarrow(dbo.ToKatakana(@�ڋq���ӂ肪��))

    -- �����ǋL����X�V�O���̕�������쐬
    SELECT @�ڋq��񃁃� = ''
    SELECT @�\����ʃ��� = ''

    SET @estoreDB_UserClass = (SELECT �ڋqNo FROM estoreDB.dbo.tMicCust_mst WHERE estoreDB.dbo.tMicCust_mst.�ڋqNo = @�ڋq�h�c)

    -- �ڋq���̕ύX
    IF (@Pre�ڋq���P<>@�ڋq���P) or (@Pre�ڋq���Q<>@�ڋq���Q) or (@Pre�ڋq���ӂ肪��<>@�ڋq���ӂ肪��)
      BEGIN
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�ڋq���P�F'       + @Pre�ڋq���P       + NCHAR(13) + NCHAR(10)
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�ڋq���Q�F'       + @Pre�ڋq���Q       + NCHAR(13) + NCHAR(10)
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�ڋq���ӂ肪�ȁF' + @Pre�ڋq���ӂ肪�� + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre���[���A�h���X<>@���[���A�h���X)
      BEGIN
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j���[���A�h���X�F' + @Pre���[���A�h���X + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre�X�֔ԍ�<>@�X�֔ԍ�) or (@Pre�Z���P<>@�Z���P) or (@Pre�Z���Q<>@�Z���Q)
      BEGIN
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�X�֔ԍ��F'       + @Pre�X�֔ԍ�       + NCHAR(13) + NCHAR(10)
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�Z���P�F'         + @Pre�Z���P         + NCHAR(13) + NCHAR(10)
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�Z���Q�F'         + @Pre�Z���Q         + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre�d�b�ԍ�<>@�d�b�ԍ�) or (@Pre�e�`�w�ԍ�<>@�e�`�w�ԍ�)
      BEGIN
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�d�b�ԍ��F'       + @Pre�d�b�ԍ�       + NCHAR(13) + NCHAR(10)
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�e�`�w�ԍ��F'     + @Pre�e�`�w�ԍ�     + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre�J�ݎҖ�<>@�J�ݎҖ�)
      BEGIN
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�J�ݎҖ��F'       + @Pre�J�ݎҖ�       + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre��Ë@�փR�[�h<>@��Ë@�փR�[�h)
      BEGIN
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j��Ë@�փR�[�h�F' + @Pre��Ë@�փR�[�h + NCHAR(13) + NCHAR(10)
      END
    IF (@PreEstore���[���A�h���X<>@Estore���[���A�h���X)
      BEGIN
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���jEstore���[���A�h���X�F' + @PreEstore���[���A�h���X + NCHAR(13) + NCHAR(10)
      END
    IF (@Pre�����}�K�w�ǃt���O<>@�����}�K�w�ǃt���O)
      BEGIN
        --Ver.1.6 2020/09/29 �����}�K�w�ǕύX���ɕ�����ϊ��G���[��Insert Into�Ɏ��s by ���C
        --SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�����}�K�w�ǁF' + @Pre�����}�K�w�ǃt���O + NCHAR(13) + NCHAR(10)
        SELECT @�ڋq��񃁃� = @�ڋq��񃁃� + '���j�����}�K�w�ǁF' + CONVERT(nvarchar, @Pre�����}�K�w�ǃt���O) + NCHAR(13) + NCHAR(10)
      END

    -- �\����ʂ̕ύX
    IF @Pre�\�����<>@�\�����
      BEGIN
        SELECT @Pre�\����ʕ�����  = (case @Pre�\����� when 1 then '�o�����[�p�b�N' when 2 then '�A�b�v�O���[�h' when 3 then '���z�ۋ�' else '' end)
        SELECT @Post�\����ʕ����� = (case @�\�����    when 1 then '�o�����[�p�b�N' when 2 then '�A�b�v�O���[�h' when 3 then '���z�ۋ�' else '' end)
        -- ���������� �� 'yyyy/MM' �`���ō쐬
        SELECT @���������� = left(Convert(nvarchar, dateadd(month, 1, GetDate()) , 111), 7) 

        SELECT @�\����ʃ���  = '�l�v�r' +@Pre�\����ʕ����� + '���ԏI��' + NCHAR(13) + NCHAR(10)
        SELECT @�\����ʃ��� = @�\����ʃ��� + @���������� + '�`' + @Post�\����ʕ����� + '�J�n' + NCHAR(13) + NCHAR(10) + NCHAR(13) + NCHAR(10)
        SELECT @�\����ʃ��� = @�\����ʃ��� + '�\����ʁF' + @Pre�\����ʕ����� + '��' + @Post�\����ʕ����� + NCHAR(13) + NCHAR(10)
        SELECT @�\����ʃ��� = @�\����ʃ��� + '�ɕύX' + NCHAR(13) + NCHAR(10)
      END

    BEGIN TRY
      
      BEGIN TRANSACTION
        
        BEGIN
          -- ���ڐݒ�f�[�^����蒷��������̓G���[�Ƃ����؂�̂Ăēo�^���邽�� WARNINGS OFF�Ƃ���
          SET ANSI_WARNINGS OFF;

          -- �����ɒǋL
          IF @�ڋq��񃁃�<>'' 
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
                 (@�ڋq�h�c
                , 'tClient'
                , (convert(nvarchar,@�X�V�o�^����,111)+' '+left(convert(nvarchar,@�X�V�o�^����,8),5)+' �l�v�r')
                , ('�y�ڋq���ύX�z' + NCHAR(13) + NCHAR(10) + '�l�v�r���X�V' + NCHAR(13) + NCHAR(10) + @�ڋq��񃁃�)
                , @�X�V�o�^����
                , @�X�V�o�^��
                , NULL
                , NULL,NULL,NULL
                , NULL,NULL,NULL
                , 0,0,0
                ,'002')
            END

          IF @�\����ʃ���<>''
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
                 (@�ڋq�h�c
                , 'tClient'
                , (convert(nvarchar,@�X�V�o�^����,111)+' '+left(convert(nvarchar,@�X�V�o�^����,8),5)+' �l�v�r')
                , ('�y�ڋq���ύX�z' + NCHAR(13) + NCHAR(10) + '�l�v�r���X�V' + NCHAR(13) + NCHAR(10) + @�\����ʃ���)
                , @�X�V�o�^����
                , @�X�V�o�^��
                , NULL
                , NULL,NULL,NULL
                , NULL,NULL,NULL
                , 0,0,0
                ,'002')
            END

          -- tMik���[�U �X�V
          UPDATE   JunpDB.dbo.tMik���[�U
            SET    fusҰٱ��ڽ        = @���[���A�h���X 
                 , fus�J�ݎ�          = @�J�ݎҖ�
                 , fus��ۈ�ÃR�[�h  = @��Ë@�փR�[�h
                 , fus���[�U�[        = '1'
                 , fus�X�V��          = @�X�V�o�^����
                 , fus�X�V��          = @�X�V�o�^��
            WHERE (fusCliMicID = @�ڋq�h�c)
          
          -- tMik��{��� �X�V
          UPDATE   JunpDB.dbo.tMik��{���
            SET    fkj�ڋq���Q        = @�ڋq���Q
                 , fkj�X�֔ԍ�        = @�X�֔ԍ�
                 , fkj�Z���P          = @�Z���P
                 , fkj�Z���Q          = @�Z���Q
                 , fkj�d�b�ԍ�        = @�d�b�ԍ�
                 , fkj�t�@�b�N�X�ԍ�  = @�e�`�w�ԍ�
                 , fkj�X�V��          = @�X�V�o�^����
                 , fkj�X�V��          = @�X�V�o�^��
                 , fkj���1           = (case when isnull(fkj���Ӑ���,'')<>'' then 1 else 0 end)
                 , fkj���2           = (case when isnull(fkj�d������,'')<>'' then 1 else 0 end)
            WHERE (fkjCliMicID = @�ڋq�h�c)
          
          -- tClient �X�V
          UPDATE   JunpDB.dbo.tClient
            SET    fCliName           = @�ڋq���P
                 , fCliYomi           = @�ڋq���ӂ肪��
                 , fCliUpdate         = @�X�V�o�^����
                 , fCliUpdateMan      = @�X�V�o�^��
            WHERE (fCliID = @�ڋq�h�c)

          -- T_CUSTOMER_FOUNDATIONS�ɐ\����ʂ��X�V
          UPDATE   dbo.T_CUSTOMER_FOUNDATIONS
            SET    APPLY_TYPE         = @�\�����
                 , UPDATE_DATE        = @�X�V�o�^����
                 , UPDATE_PERSON      = @�X�V�o�^��
            WHERE (CUSTOMER_ID = @�ڋq�h�c)

          -- WARNINGS ON �ɖ߂�
          SET ANSI_WARNINGS ON;

        END
      
      COMMIT TRANSACTION

    END TRY

    BEGIN CATCH
      -- ���炩�̃G���[���������̂Ń��[���o�b�N�i���G���[�n���h�����O�j
      ROLLBACK TRANSACTION
    END CATCH

    IF @estoreDB_UserClass is not null
       BEGIN
           -- tMicCust_mst �X�V
           UPDATE estoreDB.dbo.tMicCust_mst
           SET  ���[���A�h���X = @Estore���[���A�h���X
                  ,�����}�K�w�� = @�����}�K�w�ǃt���O
           WHERE (estoreDB.dbo.tMicCust_mst.�ڋqNo = @�ڋq�h�c)
       END

  RETURN

GO

ALTER TABLE [dbo].[T_UPDATE_CLIENT_INFO] ENABLE TRIGGER [trgInsertT_UPDATE_CLIENT_INFO]
GO

