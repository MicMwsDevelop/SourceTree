USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic��������\�z]    Script Date: 2021/05/10 12:20:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--ALTER VIEW [dbo].[�����p�쐬���r���[]
CREATE VIEW [dbo].[vMic��������\�z]
as
/*
    vMic��������\�z

        ����������z�̗\�z�l���Z�o����
        �Q�D�o�b�`�����̓�������i���[�X���揜���j
        �R�D�v�v�`�[�̃o�����[�p�b�N�`�[�Ŕ[�����������t�̔��㖢�v�㕪�i���[�X���揜���j
        �S�D�v�v�`�[�̃o�����[�p�b�N�ȊO�̓`�[�Ŕ[�����������t�̔��㖢�v�㕪�i���[�X���揜���j
        �̋��z�i�Ŕ����j�𕔖傲�ƂɏW�v���܂�


        Ver.1.00  2016/08/31  ����
        Ver.1.10  2016/11/11  �R�[�h����
        Ver.2.00  2017/08/25  �ێ�O�󌎊z�����p�~��������Ɉڍs
                              �u�ێ�E���[�X����E�i���R�[���E�g�l�d�������v���u���[�X����̂ݏ����v�ɕύX
        Ver.2.02  2018/02/01  ���i�敪�Q��ǉ� 

/*
���Ŏd�l
        ����������z�̗\�z�l���Z�o����
        �P�D�ێ�O����v���瓖��������i�T�Z�l�j
        �Q�D�o�b�`�����̓�������i�ێ瓙�����j
        �R�D�v�v�`�[�̃o�����[�p�b�N�`�[�Ŕ[�����������t�̔��㖢�v�㕪�i�ێ瓙�����j
        �S�D�v�v�`�[�̃o�����[�p�b�N�ȊO�̓`�[�Ŕ[�����������t�̔��㖢�v�㕪�i�ێ瓙�����j
        �̋��z�i�Ŕ����j�𕔖傲�ƂɏW�v���܂�

        ���i�ێ瓙�����j�͏��i�敪�Q��
            3: �\�t�g�ێ�
            5: ���[�X����
            102: �ٺ�ъ֘A
            103: HME�֘A
        �����O����
*/

*/
select choose(SL.�敪No,
              '�ێ�O�󌎊z����', 
              '�o�b�`�|����v��Ώ�',
              '�v�v�󒍎c�|�u�o',
              '�v�v�󒍎c�|�u�o�ȊO') as ����敪
      ,SL.����R�[�h                  as ����R�[�h
      ,rtrim(BM.emsb_str)             as ���喼
      ,SL.���i�敪�Q                  as ���i�敪�R�[�h
      ,KM.ems_str                     as ���i�敪��
      ,cast(SL.���z as integer)       as ���z
from (
    /*  �P�D�ێ�O����v���瓖��������i�T�Z�l�j  */
    /*      ���[�U�[���̕ێ�f�[�^���g�p�������v�㕪�i�T�Z�l�j���W�v����  */
    /*      �ێ�����ŗL�����[�U�[�i�I���ł͂Ȃ��j                          */
    /*        ���L�����[�U�[�̏������O����                                  */
    /*      �ێ痿��QURIA��55000�~�AU-BOX��90000�~�Ƃ��i�̔��X���͂��̂W�|�j��1/12�̋��z���W�v  */
/*
    select 1                                                                                    as �敪No
          ,right('00'+cast(B.fPca����R�[�h as varchar),3)                                      as ����R�[�h
          ,sum(((iif(U.�V�X�e������='QURIA',55000,90000))*(iif(U.S�����e�敪=2,(0.8),(1))))/12) as ���z
    from        vMic�S���[�U�[ U
      left join tMih�x�X���   B on B.f�x�X�R�[�h=U.�x�X�R�[�h
    where U.S�ێ�=1 -- and U.�L�����[�U�[�t���O=1
    group by B.fPca����R�[�h

  union
*/
    /*  �Q�D�o�b�`�����̓�������i�ێ瓙�����j          */
    /*      �o�b�`�̓�������v����W�v����              */
    /*      ���[�X���������  */
    select
           2                      as �敪No
     --    iif(H.f�̔����=1,1,2) as �敪No
          ,rtrim(D.sykd_jbmn)     as ����R�[�h
          ,sum(D.sykd_kingaku)    as ���z
          ,M.sms_skbn2            as ���i�敪�Q
    from         vMicPCA���㖾��   D
      inner join vMicPCA���i�}�X�^ M on M.sms_scd  =D.sykd_scd
--    left  join tMih�󒍃w�b�_    H on H.f�󒍔ԍ�=D.sykd_denno
    where D.sykd_kingaku<>0
      and (D.sykd_uribi> cast(convert(nvarchar,eomonth(getdate(), 0),112) as integer))
      and (D.sykd_uribi<=cast(convert(nvarchar,eomonth(getdate(), 1),112) as integer))
 --   Ver.2.00  2017/08/25  �u�ێ�E���[�X����E�i���R�[���E�g�l�d�������v���u���[�X����̂ݏ����v�ɕύX
 --     and not(M.sms_skbn2=3 or M.sms_skbn2=5 or M.sms_skbn2=102 or M.sms_skbn2=103)
        and not(M.sms_skbn2=5)
    group by D.sykd_jbmn,M.sms_skbn2
 -- group by H.f�̔����,D.sykd_jbmn,M.sms_skbn2

  union
    /*  �R�D�v�v�`�[�̃o�����[�p�b�N�`�[�Ŕ[�����������t�̔��㖢�v�㕪�i�ێ瓙�����j      */
    /*  �S�D�v�v�`�[�̃o�����[�p�b�N�ȊO�̓`�[�Ŕ[�����������t�̔��㖢�v�㕪�i�ێ瓙�����j*/
    /*      �v�v�`�[�̎󒍎c���W�v����                 */
    /*      ����v�����NULL�̂��́����v��             */
    /*      �[�����t�������̓��t                       */
    /*      ���[�X���������                           */
    /*      �o�����[�p�b�N�`�[�Ƃ���ȊO���敪         */
    select iif(H.f�̔����=1,3,4)                          -- as �敪No
          ,right('00'+cast(B.fPca����R�[�h as varchar),3) -- as ����R�[�h
          ,sum(D.f�񋟉��i)                                -- as ���z
          ,D.f���i�敪2                                    -- as ���i�敪�Q
    from         tMih�󒍃w�b�_ H
      inner join tMih�󒍏ڍ�   D on D.f�󒍔ԍ�  =H.f�󒍔ԍ�
      left  join tMih�x�X���   B on B.f�x�X�R�[�h=H.fBshCode3
    where (H.f����v��� IS NULL)
      and (H.f�[��> eomonth(getdate(), 0))
      and (H.f�[��<=eomonth(getdate(), 1))
 --   Ver.2.00  2017/08/25  �u�ێ�E���[�X����E�i���R�[���E�g�l�d�������v���u���[�X����̂ݏ����v�ɕύX
 --   and not(D.f���i�敪2=3 or D.f���i�敪2=5 or D.f���i�敪2=102 or D.f���i�敪2=103)
      and not(D.f���i�敪2=5)
    group by H.f�̔����,B.fPca����R�[�h,D.f���i�敪2

) SL left join vMicPCA����}�X�^ BM on BM.emsb_kbn=SL.����R�[�h
     left join vMicPCA�敪�}�X�^ KM on KM.ems_kbn =SL.���i�敪�Q and KM.ems_id=22

GO

