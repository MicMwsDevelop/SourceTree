USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic�S���[�U�[2]    Script Date: 2019/10/29 16:34:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--CREATE VIEW [dbo].[vMic�S���[�U�[2]
ALTER VIEW [dbo].[vMic�S���[�U�[2]
AS
/*
    vMic�S���[�U�[2

     2011/03/02  [����z��]�t�B�[���h��ǉ�
                   ������ݒ肪����Ƃ��͐����悪
                   ����ȊO�̓��[�U�[�� ���Ӑ�敪�R���V�O�̂Ƃ�  ����z��=1 �Ƃ���
                 [����z��]�t�B�[���h�ǉ��ɕ����Ċ�����[��������]�t�B�[���h��
                 ������ݒ肪����Ƃ��͐�����̒������Q�Ƃ���悤�ύX����
                 ����ɁA[���������]�t�B�[���h���ǉ�
                 ������ݒ肪����Ƃ��͐�����́A����ȊO�̓��[�U�[�̉�������Q�Ɖ\�ɂ���
     2011/03/10  [�����敪No]��ǉ�
                   ������ݒ肪����Ƃ��͐������
                   ����ȊO�̓��[�U�[�� ���Ӑ�敪�R ��[�����敪No]�ŎQ�Ƃ��邱�Ƃɂ���
                   PCADB�̋敪�}�X�^���琿���敪��������Q�Ƃ��悤�Ƃ��������s�Ɏ��Ԃ�������f�O
     2011/04/27  [�����敪]���R�����g�A�E�g
     2011/08/08  ���Z�d�Z�֘A�t�B�[���h��ǉ�
                 �R�����g�A�E�g���Ă���[�����敪]�� HASH �g�p�ō������ɂ�蕜��
     2011/11/21  �c�ƒS���҃R�[�h�A�c�ƒS���Җ��t�B�[���h��ǉ�
     2011/12/06  eStore�o�^�t���O�AeStore�o�^���[���A�h���X �t�B�[���h��ǉ�
     2012/09/10  eStore_�p�X���[�h�AeStore_�p�X���[�h�ǂ݁AMWS_ID�AMWS_�p�X���[�h�AMWS_�p�X���[�h�ǂ� �t�B�[���h��ǉ�
     2012/10/18  �Q�Ɛ�r���[�̖��̕ύX vMicCHARLIE�ڋq�� �� vMic_MWS�ڋq��
     2013/01/16  [MWS_�\�����],[MWS_�\���������],[MWS_�̔����]���ڒǉ�
     2013/04/05  [MWS_�g�p��������]���ڒǉ�
     2013/05/28  [UPG�����p����][�c����]���ڒǉ�
     2013/08/07  [�̔��XID][�̔��X����][�̔��X�O���[�v�R�[�h][�̔��X�O���[�v����][�̔��X�敪�R�[�h][�̔��X�敪����]���ڒǉ�
                   �l�v�r���[�U�[�͂b�������������ɓo�^���ꂽ�̔��X��񂩂�A
                   ���V�X�e�����[�U�[�͂v�v���[�U�[���̔̔��X��(WWNo:xxxxxxxx/�E�E�E)�̏�񂩂�
                   �̔��XID���Ƃ肾���A���́A�O���[�v�A�敪�͂v�v�ڋq�̔̔��X�f�[�^���Q��
     2013/12/04  [���Z�d�Z�\��N��]���ڒǉ�
                 �������i�قƂ�ǌ��ʂȂ��j
     2014/01/10  [���Z�d�Z�}��],[���Z�d�Z�I�����C��],[���Z�d�Z������]���ڒǉ�
     2014/03/12  [�̔��X�S���Җ�]���ڒǉ�
     2014/04/22  [�C���X�g�S���҃R�[�h],[�C���X�g�S���Җ�]���ڒǉ� (�˓c)
     2014/04/23  ��������������
     2014/05/29  [�����}�K�w�ǃt���O]�ǉ��A[�C���X�g�S���҃R�[�h],[�C���X�g�S���Җ�]���ڗ�ʒu�ύX
     2014/06/27  [�X�V����]���ڒǉ�
                 dbo.tClient�Adbo.tMik��{���Adbo.tMik���[�U �̍ŏI�X�V����
     2014/08/04  [�L�����[�U�[�t���O]�ǉ��i[�V�X�e����]���o�^����Ă��āA�I�����[�U�[�ł͂Ȃ������Ή����[�U�[�ł��Ȃ���=1�j
     2014/09/03  �������������� 
     2014/09/10  PCA�ŐV���������O���[�v�쐬������USER����.MDB�̕\���Ō���̂Ȃ��悤�ɑΉ�
     2015/07/30  [�����ڑ��N���C�A���g��]���ڒǉ�
     2015/09/03  �̔��X����VWonderWeb�ŐV�K�ǉ�����[�̔��XNo](dbo.tMik���[�U.fus�̔��XNo)���瓱�o�ɕύX
     2015/09/09  H�ێ� �֘A���� NULL �o��
     2015/09/15  ODBC�ڑ��G���[�̂���H�ێ�֘A���ڂ����ɖ߂�
     2015/10/19  �ēxH�ێ� �֘A���� NULL �o��
     2016/03/24  ���Z�d    �֘A���� NULL �o��,case �� iif
     2016/04/13  [���Z�d�Z�������],[���Z�d�Z�����J�n]���ڂ�VWonderWeb�ŐV�K�ǉ�����dbo.tMik���[�U���炩�瓱�o�ɕύX
	 2019/07/22	�\�t�g�ێ�_��̏���PC���S�T�|�[�g�_��e�[�u���ɕύX by ���C
	 2019/10/29	S�����e�_����Ԃ�[fBillingStartDate]����[fContractStartDate]�A[fBillingEndDate]����[fContractEndDate]�ɂ��ꂼ��ύX�A by ���C
*/
SELECT vUSER��{.�S���҃R�[�h               as �S���҃R�[�h
     , vUSER��{.�S���Җ�                   as �S���Җ�
     , vUSER��{.�x�X�R�[�h                 as �x�X�R�[�h
     , vUSER��{.�x�X��                     as �x�X��
     , vUSER��{.�V�X�e������               as �V�X�e������
     , vUSER��{.�I���t���O                 as �I���t���O
     , vUSER��{.�ڋqNo                     as �ڋqNo
     , vUSER��{.���Ӑ�No                   as ���Ӑ�No
     , vUSER��{.�ڋq���P                   as �ڋq���P
     , vUSER��{.�ڋq���Q                   as �ڋq���Q
     , vUSER��{.�t���K�i                   as �t���K�i
     , vUSER��{.�X�֔ԍ�                   as �X�֔ԍ�
     , vUSER��{.�Z���P                     as �Z���P
     , vUSER��{.�Z���Q                     as �Z���Q
     , vUSER��{.�Z���t���K�i               as �Z���t���K�i
     , vUSER��{.�d�b�ԍ�                   as �d�b�ԍ�
     , vUSER��{.FAX�ԍ�                    as FAX�ԍ�
     , vUSER��{.���`No                     as ���`No
     , vUSER��{.��ۈ�ÃR�[�h             as ��ۈ�ÃR�[�h
     , vUSER��{.���ۈ�ÃR�[�h             as ���ۈ�ÃR�[�h
     , vUSER��{.�@����                     as �@����
     , vUSER��{.�@�����t���K�i             as �@�����t���K�i
     , vUSER��{.�����於                   as �����於
     , vUSER��{.������X�֔ԍ�             as ������X�֔ԍ�
     , vUSER��{.������Z��                 as ������Z��
     , vUSER��{.������d�b�ԍ�             as ������d�b�ԍ�
     , vUSER��{.��������l                 as ��������l
     , vUSER��{.������R�[�h               as ������R�[�h
     , vUSER��{.�����於                   as �����於
     , vUSER��{.������X�֔ԍ�             as ������X�֔ԍ�
     , vUSER��{.������Z��                 as ������Z��
     , vUSER��{.������d�b�ԍ�             as ������d�b�ԍ�
     , vUSER��{.��������l                 as ��������l
     , vUSER��{.�V�X�e����                 as �V�X�e����
     , vUSER��{.�I�v�V����1                as �I�v�V����1
     , vUSER��{.�I�v�V����2                as �I�v�V����2
     , vUSER��{.�I�v�V����3                as �I�v�V����3
     , vUSER��{.�I�v�V����4                as �I�v�V����4
     , vUSER��{.�I�v�V����5                as �I�v�V����5
     , vUSER��{.�I�v�V����6                as �I�v�V����6
     , vUSER��{.���Z�v�g�p��               as ���Z�v�g�p��
     , vUSER��{.�A�P                       as �A�P
     , vUSER��{.�J���e�p��                 as �J���e�p��
     , vUSER��{.����ⳗp��                 as ����ⳗp��
     , vUSER��{.�̎����p��                 as �̎����p��
     , vUSER��{.�̎����p���Q               as �̎����p���Q
     , vUSER��{.���f�B�A                   as ���f�B�A
     , vUSER��{.�e�c��                     as �e�c��
     , vUSER��{.�[�i��                     as �[�i��
     , vUSER��{.���㌎                     as ���㌎
     , vUSER��{.�P��                       as �P��
     , vUSER��{.�T�[�o�[                   as �T�[�o�[
     , vUSER��{.�N���C�A���g               as �N���C�A���g
     , vUSER��{.�̔��X��                   as �̔��X��
     , vUSER��{.LicensedKey                as LicensedKey
     , vUSER��{.�o�[�W�������             as �o�[�W�������
     , vUSER��{.�̔��`��                   as �̔��`��
     , vUSER��{.��s���                   as ��s���
     , vUSER��{.S�ێ�_��                  as S�ێ�_��
     , vUSER��{.H�ێ�_��                  as H�ێ�_��
     , vUSER��{.�n�[�h�\��                 as �n�[�h�\��
     , vUSER��{.���[�X���                 as ���[�X���
     , vUSER��{.�o�^�J�[�h���             as �o�^�J�[�h���
     , vUSER��{.�ێ�_�񏑉��             as �ێ�_�񏑉��
     , vUSER��{.��s������               as ��s������
     , vUSER��{.���������                 as ���������
     , vUSER��{.�x�f��                     as �x�f��
     , vUSER��{.�f�Î���                   as �f�Î���
     , vUSER��{.���[���A�h���X             as ���[���A�h���X
     , vUSER��{.ClientLicense1             as ClientLicense1
     , vUSER��{.ClientLicense2             as ClientLicense2
     , vUSER��{.ClientLicense3             as ClientLicense3
     , vUSER��{.ClientLicense4             as ClientLicense4
     , vUSER��{.ClientLicense6             as ClientLicense6
     , vUSER��{.ClientLicense5             as ClientLicense5
     , vUSER��{.ClientLicense7             as ClientLicense7
     , vUSER��{.ClientLicense8             as ClientLicense8
     , vUSER��{.ClientLicense9             as ClientLicense9
     , vUSER��{.ClientLicense10            as ClientLicense10
     , vUSER��{.ClientLicense11            as ClientLicense11
     , vUSER��{.ClientLicense12            as ClientLicense12
     , vUSER��{.�n�r                       as �n�r
     , vUSER��{.�o�^�J�[�h�����           as �o�^�J�[�h�����
     , vUSER��{.ODeS����                   as ODeS����
     , vUSER��{.�O�V�X�e����               as �O�V�X�e����
     , vUSER��{.�O�V�X�e���I��             as �O�V�X�e���I��
     , vUSER��{.���l                       as ���l
     , vUSER��{.�O�V�X�e������             as �O�V�X�e������
     , t���[�X���.fle���[�X�X��            as ���[�X�X��
     , t���[�X���.fle�_��No                as �_��No
     , t���[�X���.fle����                  as ����
     , t���[�X���.fle���[�X�J�n            as ���[�X�J�n
     , t���[�X���.fle���[�X�I��            as ���[�X�I��
     , t���[�X���.fle���[�X��              as ���[�X��
     , t���[�X���.fle�c��                as �c��
     , t���[�X���.fle�c���z                as �c���z
     , t���[�X���.fle���[�X�_����l        as ���[�X�_����l
      ,iif(PCS.fApplyNo is null, '0', '1') as S�ێ�		-- 2019/07/22 �ύX
      ,CONVERT(NVARCHAR, PCS.fApplyDate, 111) as S�_�񏑉���N��
      ,NULL AS S���v��
      ,PCS.fYears AS S�_��N��
      ,CAST(�T�[�r�X�ꗗ.���i�P�� AS int) AS S�����e����
      ,FORMAT(PCS.fContractStartDate, 'yyyy/MM') as S�����e�_��J�n	-- 2019/10/29 �ύX
      ,FORMAT(PCS.fContractEndDate, 'yyyy/MM') as S�����e�_��I��	-- 2019/10/29 �ύX
      ,NULL AS S�����e�_����l1
      ,NULL AS S�����e�_����l2
      ,NULL AS S�_�񖼋`
      ,NULL AS S�����e������R�[�h
      ,NULL AS S�����e�����於
      ,NULL AS S�����e�敪
      ,NULL AS S��BM�於
      ,NULL AS S���z
     , NULL                                 as H�ێ�                  -- 2015/10/19 �ύX
     , NULL                                 as H�_�񏑉���N��
     , NULL                                 as H���v��
     , NULL                                 as H�_��N��
     , NULL                                 as H�����e����
     , NULL                                 as H�����e�_��J�n
     , NULL                                 as H�����e�_��I��
     , NULL                                 as H�����e�_����l1
     , NULL                                 as H�����e�_����l2
     , NULL                                 as H�_�񖼋`
     , NULL                                 as H�����e������R�[�h
     , NULL                                 as H�����e�����於
     , NULL                                 as H�����e�敪
     , NULL                                 as H��BM�於
     , NULL                                 as H���z
     , t��s���.fdaAPLUS�R�[�h             as ��s���APLUS�R�[�h
     , t��s���.fda��s���J�i              as ��s�����s���J�i
     , t��s���.fda��s�R�[�h              as ��s�����s�R�[�h
     , t��s���.fda�x�X���J�i              as ��s����x�X���J�i
     , t��s���.fda�x�X�R�[�h              as ��s����x�X�R�[�h
     , t��s���.fda�a�����                as ��s����a�����
     , t��s���.fda�����ԍ�                as ��s��������ԍ�
     , t��s���.fda�a���Җ�                as ��s����a���Җ�
     , t��s���.fda������z                as ��s���������z
     , t��s���.fda�ŏI������              as ��s����ŏI������
     , t��s���.fda���                    as ��s������
     , t��s���.fda���l                    as ��s������l
     , t���Ӑ�.fpt�K�p����No                as �K�p����No
     , q�������@.��������                   as ��������
     , q�������@.���������                 as ���������
     , q�������@.�������Ӑ�敪             as �����敪No
     , q�������@.�����敪                   as �����敪
     , q�������@.����z��                   as ����z��
     , t���ԍ�.���ԍ�                       as ���ԍ�
     , t���ԍ�.�s���{����                   as �s���{����
     , t�����}�X�^.fcm����                  as �V�X�e������
     , NULL                                 as �I�����C��             -- 2016/03/24 �ύX
     , vUSER��{.���Z�d�Z�������           as ���Z�d�Z�������
     , NULL                                 as ���Z�d�Z�m�F����
     , vUSER��{.���Z�d�Z�����J�n           as ���Z�d�Z�����J�n
     , NULL                                 as ���Z�d�Z�I�����C����o�\��t���O
     , NULL                                 as ���Z�d�Z�C���^�[�l�b�g���p���
     , NULL                                 as ���Z�d�Z���p�v���o�C�_
     , NULL                                 as ���Z�d�Z���p���
     , NULL                                 as ���Z�d�Z������ݏꏊ
     , NULL                                 as ���Z�d�Z�I�����C������PC
     , NULL                                 as ���Z�d�Z�I�����C���m�F����
     , NULL                                 as ���Z�d�Z�I�����C�������J�n
     , NULL                                 as ���Z�d�Z�����N��Ə�
     , NULL                                 as ���Z�d�Z�\��N��
     , NULL                                 as ���Z�d�Z�G���g���[�`�F�b�N
     , NULL                                 as ���Z�d�Z��t�S����
     , NULL                                 as ���Z�d�Z��t��
     , NULL                                 as ���Z�d�Z��Ƌ敪
     , NULL                                 as ���Z�d�Z�����˗���
     , NULL                                 as ���Z�d�Z������
     , NULL                                 as ���Z�d�Z��ƒS����
     , NULL                                 as ���Z�d�Z��Ɗ�����
     , NULL                                 as ���Z�d�Z��Ɗ����t���O
     , NULL                                 as ���Z�d�Z���l
     , NULL                                 as ���Z�d�Z�}��
     , NULL                                 as ���Z�d�Z�I�����C��
     , NULL                                 as ���Z�d�Z������
     , v�c�ƒS��.�c�ƒS���҃R�[�h           as �c�ƒS���҃R�[�h
     , v�c�ƒS��.�c�ƒS���Җ�               as �c�ƒS���Җ�
     , v�C���X�g�S��.�C���X�g�S���҃R�[�h   as �C���X�g�S���҃R�[�h
     , v�C���X�g�S��.�C���X�g�S���Җ�       as �C���X�g�S���Җ�
     , iif(veStoreCust.���[���A�h���X<>'', 1, 0)
                                            as eStore�o�^�t���O
     , iif(veStoreCust.���[���A�h���X<>'' and veStoreCust.�����}�K�w��='1', 1, 0)
                                            as �����}�K�w�ǃt���O
     , veStoreCust.���[���A�h���X           as eStore�o�^���[���A�h���X
     , veStoreCust.�p�X���[�h               as eStore_�p�X���[�h
     , veStoreCust.�p�X���[�h�ǂ�           as eStore_�p�X���[�h�ǂ�
     , vCharlie�ڋq.MWS_ID                  as MWS_ID
     , vCharlie�ڋq.MWS_�p�X���[�h          as MWS_�p�X���[�h
     , vCharlie�ڋq.MWS_�p�X���[�h�ǂ�      as MWS_�p�X���[�h�ǂ�
     , vCharlie�ڋq.MWS_�\�����            as MWS_�\�����
     , vCharlie�ڋq.MWS_�\���������        as MWS_�\���������
     , vCharlie�ڋq.MWS_�̔����            as MWS_�̔����
     , vCharlie�ڋq.MWS_�g�p��������        as MWS_�g�p��������
     , vUPG_USER.���p����                   as UPG�����p����
     , vUPG_USER.�c����                     as �c����
     , vRESALER.�ڋqNo                      as �̔��XID
     , vRESALER.�ڋq���P+vRESALER.�ڋq���Q  as �̔��X����
     , vRESALER.�̔��X�O���[�v              as �̔��X�O���[�v�R�[�h
     , vRESALER.�̔��X�O���[�v��            as �̔��X�O���[�v����
     , vRESALER.�̔��X�敪�R�[�h            as �̔��X�敪�R�[�h
     , vRESALER.�̔��X�敪����              as �̔��X�敪����
     , vUSER��{.�̔��X�S���Җ�             as �̔��X�S���Җ�
     , vUSER��{.�X�V����                   as �X�V����
     , iif((vUSER��{.�I���t���O=0) and
           (vUSER��{.�V�X�e����>'000' and vUSER��{.�V�X�e����<'999') and
           (vUSER��{.��������� not Like '%�����Ή��Ȃ�%'), 1 , 0)
                                            as �L�����[�U�[�t���O
     , vUSER��{.�����ڑ��N���C�A���g��     as �����ڑ��N���C�A���g��
from dbo.vMic���[�U�[��{2 vUSER��{
  inner join dbo.tMikPca���Ӑ�        t���Ӑ�       on (vUSER��{.�ڋqNo=t���Ӑ�.fptCliMicID)
  left  join
     (select TMS.fpt���Ӑ�No    as �������Ӑ�No
            ,TMS.fpt���Ӑ�敪3 as �������Ӑ�敪
        --  ,TMS.fpt��������1   as ��������   2014/09/10  PCA�ŐV���������O���[�v�쐬������USER����.MDB�̕\���Ō���̂Ȃ��悤�ɑΉ�
            ,(case TMS.fpt��������1
                when '11' then '10'
                when '19' then '20'
                when '29' then '31'
                when '32' then '31'
                else TMS.fpt��������1
              end)              as ��������
            ,TMS.fpt�����1     as ���������
            ,RTRIM(ems.ems_str) as �����敪
            ,��{.fkj�ڋq�敪   as �����ڋq�敪
            ,iif((��{.fkj�ڋq�敪=2 or ��{.fkj�ڋq�敪=18) and (TMS.fpt���Ӑ�敪3=70), 1 ,0)
                                as ����z��
      from dbo.tMikPca���Ӑ� TMS
        left  join dbo.tMik��{���      ��{ on (��{.fkjCliMicID=TMS.fptCliMicID)
        left  join dbo.vMicPCA�敪�}�X�^ ems  on ((ems.ems_id=13) and (ems.ems_kbn=TMS.fpt���Ӑ�敪3))
     )                                q�������@     on
        (q�������@.�������Ӑ�No=
         iif(isnull(vUSER��{.������R�[�h,'')='',(vUSER��{.���Ӑ�No) ,(vUSER��{.������R�[�h)))
  left  join dbo.tMik���[�X���       t���[�X���   on (vUSER��{.�ڋqNo=t���[�X���.fleCliMicID)
--  left  join dbo.tMik�ێ�_��         t�ێ�_��     on (vUSER��{.�ڋqNo=t�ێ�_��.fhsCliMicID)
  left  join dbo.tMik��s���         t��s���     on (vUSER��{.�ڋqNo=t��s���.fdaCliMicID)
  left  join dbo.tMik���ԍ�           t���ԍ�       on (LEFT(vUSER��{.�Z���P,3)=LEFT(t���ԍ�.�s���{����,3))
  left  join dbo.tMik�R�[�h�}�X�^     t�����}�X�^   on ((vUSER��{.�V�X�e����=t�����}�X�^.fcm�R�[�h) and (t�����}�X�^.fcm�R�[�h���='91'))
  left  join dbo.vMic�c�ƒS��         v�c�ƒS��     on (vUSER��{.�ڋqNo=v�c�ƒS��.�ڋqNo)
  left  join dbo.vMic�C���X�g�S��     v�C���X�g�S�� on (vUSER��{.�ڋqNo=v�C���X�g�S��.�ڋqNo)
  left  join dbo.vMic_estore_Cust_mst veStoreCust   on (vUSER��{.�ڋqNo=veStoreCust.�ڋqNo)
  left  join dbo.vMic_MWS�ڋq��     vCharlie�ڋq  on (vUSER��{.�ڋqNo=vCharlie�ڋq.�ڋqID)
  left join dbo.T_USE_PCCSUPPORT as PCS on vUSER��{.�ڋqNo = PCS.fCustomerID
  left join dbo.vMic_MWS�T�[�r�X�ꗗ as �T�[�r�X�ꗗ on �T�[�r�X�ꗗ.�T�[�r�X�R�[�h = PCS.fServiceId
  left  join
     (select U.fusCliMicID as �ڋqNo
            ,left(CONVERT(VARCHAR(10),dateadd(month,6*12-1,CONVERT(DATETIME,(U.fus�[�i��+'/01'))),111),7) as ���p����
            ,datediff(month,Getdate(),dateadd(month,6*12  ,CONVERT(DATETIME,(U.fus�[�i��+'/01'))))        as �c����
      from dbo.tMik���[�U U
      where ((U.fus�V�X�e���� = '047' or U.fus�V�X�e���� = '048') and (U.fus�[�i��>='2008/06'))
     )                                vUPG_USER     on (vUSER��{.�ڋqNo=vUPG_USER.�ڋqNo)
  left  join dbo.vMic�S�̔��X         vRESALER      on (vUSER��{.�̔��XNo=vRESALER.�ڋqNo)





GO

