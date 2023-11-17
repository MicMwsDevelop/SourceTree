USE [JunpDB]
GO

DECLARE @InputDate as datetime
SET @InputDate = '2021-09-24'

SELECT choose(SL.�敪No,
              '�ێ�O�󌎊z����', 
              '�o�b�`�|����v��Ώ�',
              '�v�v�󒍎c�|�u�o',
              '�v�v�󒍎c�|�u�o�ȊO') as ����敪
      , SL.����R�[�h as ����R�[�h
      , RTRIM(BM.emsb_str) as ���喼
      , SL.���i�敪�Q as ���i�敪�R�[�h
      , KM.ems_str as ���i�敪��
      , CAST(SL.���z as integer) as ���z
FROM (
    SELECT
           2 as �敪No
          ,RTRIM(D.sykd_jbmn) as ����R�[�h
          ,SUM(D.sykd_kingaku) as ���z
          ,M.sms_skbn2 as ���i�敪�Q
    FROM vMicPCA���㖾�� as D
    INNER JOIN vMicPCA���i�}�X�^ as M on M.sms_scd = D.sykd_scd
    WHERE D.sykd_kingaku <> 0
      AND D.sykd_uribi > CAST(CONVERT(nvarchar, EOMONTH(@InputDate, -1), 112) as integer)
      AND D.sykd_uribi <= CAST(CONVERT(nvarchar, EOMONTH(@InputDate, 0), 112) as integer)
      AND M.sms_skbn2 <> 5 AND M.sms_skbn2 <> 3
    GROUP BY D.sykd_jbmn, M.sms_skbn2

  UNION
    SELECT IIF(H.f�̔���� = 1, 3, 4)                          -- as �敪No
          , RIGHT('00' + CAST(B.fPca����R�[�h as varchar), 3) -- as ����R�[�h
          , SUM(D.f�񋟉��i)                                   -- as ���z
          , D.f���i�敪2                                       -- as ���i�敪�Q
    FROM tMih�󒍃w�b�_ as H
      INNER JOIN tMih�󒍏ڍ� as D on D.f�󒍔ԍ� = H.f�󒍔ԍ�
      LEFT JOIN tMih�x�X��� as B on B.f�x�X�R�[�h = H.fBshCode3
    WHERE H.f����v��� IS NULL
      AND H.f�[�� > EOMONTH(@InputDate, -1)
      AND H.f�[�� <= EOMONTH(@InputDate, 0)
      AND D.f���i�敪2 <> 5 AND D.f���i�敪2 <> 3
    GROUP BY H.f�̔����, B.fPca����R�[�h, D.f���i�敪2
) SL
LEFT JOIN vMicPCA����}�X�^ as BM on BM.emsb_kbn = SL.����R�[�h
LEFT JOIN vMicPCA�敪�}�X�^ as KM on KM.ems_kbn = SL.���i�敪�Q AND KM.ems_id = 22

--where SL.����R�[�h = '081'

