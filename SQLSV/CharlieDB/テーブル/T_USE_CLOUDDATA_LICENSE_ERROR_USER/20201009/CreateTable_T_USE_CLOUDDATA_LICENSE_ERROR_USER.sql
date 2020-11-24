USE [charlieDB]
GO

/****** Object:  Table [dbo].[T_USE_CLOUDDATA_LICENSE_ERROR_USER]    Script Date: 2020/10/05 16:37:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
    T_USE_CLOUDDATA_LICENSE_ERROR_USER

    AOSクラウドバックアップライセンス登録エラーユーザー管理テーブル

    T_PRODUCT_CONTROL新規追加トリガーに行っている新MWSユーザーに対してAOSクラウドバックアップライセンスの割り当てに失敗したユーザーを
    本テーブルに登録する

    Ver.1.0 2020/10/09 初版 by 勝呂
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

