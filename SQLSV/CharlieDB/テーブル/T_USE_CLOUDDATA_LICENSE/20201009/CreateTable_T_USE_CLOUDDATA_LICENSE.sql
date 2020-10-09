USE [charlieDB]
GO

/****** Object:  Table [dbo].[T_USE_CLOUDDATA_LICENSE]    Script Date: 2020/09/29 11:38:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
    T_USE_CLOUDDATA_LICENSE

    AOS�N���E�h�o�b�N�A�b�v���C�Z���X�Ǘ��e�[�u��

    Ver.1.0 2020/10/09 ���� by ���C
*/
CREATE TABLE [dbo].[T_USE_CLOUDDATA_LICENSE](
	[fLisenceId] [varchar](50) NOT NULL,
	[fCustomerID] [int] NULL,
	[fUpdateDate] [datetime] NULL,
	[fUpdatePerson] [nvarchar](30) NULL,
 CONSTRAINT [PK_T_USE_CLOUDDATA_LICENSE] PRIMARY KEY CLUSTERED 
(
	[fLisenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

