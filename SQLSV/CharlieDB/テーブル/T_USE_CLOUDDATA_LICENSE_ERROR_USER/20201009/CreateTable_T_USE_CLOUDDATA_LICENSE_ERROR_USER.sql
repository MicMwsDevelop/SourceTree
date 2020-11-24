USE [charlieDB]
GO

/****** Object:  Table [dbo].[T_USE_CLOUDDATA_LICENSE_ERROR_USER]    Script Date: 2020/10/05 16:37:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
    T_USE_CLOUDDATA_LICENSE_ERROR_USER

    AOS�N���E�h�o�b�N�A�b�v���C�Z���X�o�^�G���[���[�U�[�Ǘ��e�[�u��

    T_PRODUCT_CONTROL�V�K�ǉ��g���K�[�ɍs���Ă���VMWS���[�U�[�ɑ΂���AOS�N���E�h�o�b�N�A�b�v���C�Z���X�̊��蓖�ĂɎ��s�������[�U�[��
    �{�e�[�u���ɓo�^����

    Ver.1.0 2020/10/09 ���� by ���C
*/
CREATE TABLE [dbo].[T_USE_CLOUDDATA_LICENSE_ERROR_USER](
	[fCustomerID] [int] NOT NULL,
	[fUpdateDate] [datetime] NULL,
	[fUpdatePerson] [nvarchar](30) NULL,
 CONSTRAINT [PK_T_USE_CLOUDDATA_LICENSE_ERROR_USER] PRIMARY KEY CLUSTERED 
(
	[fCustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

