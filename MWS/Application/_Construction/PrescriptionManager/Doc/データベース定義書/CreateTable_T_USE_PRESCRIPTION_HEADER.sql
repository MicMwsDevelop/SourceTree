USE [charlieDB]
GO

/****** Object:  Table [dbo].[T_USE_PRESCRIPTION_HEADER]    Script Date: 2023/02/14 15:35:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_USE_PRESCRIPTION_HEADER](
	[fContractID] [int] IDENTITY(1,1) NOT NULL,
	[fCustomerID] [int] NULL,
	[fGoodsID] [nvarchar](13) NULL,
	[fOrderReserveID] [int] NULL,
	[fApplyDate] [datetime] NULL,
	[fOperationDate] [datetime] NULL,
	[fContractStartDate] [datetime] NULL,
	[fContractEndDate] [datetime] NULL,
	[fBillingStartDate] [datetime] NULL,
	[fBillingEndDate] [datetime] NULL,
	[fEndFlag] [char](1) NULL,
	[fDeleteFlag] [char](1) NULL,
	[fCreateDate] [datetime] NULL,
	[fCreatePerson] [nvarchar](30) NULL,
	[fUpdateDate] [datetime] NULL,
	[fUpdatePerson] [nvarchar](30) NULL,
 CONSTRAINT [PK_T_USE_PRESCRIPTION_HEADER] PRIMARY KEY CLUSTERED 
(
	[fContractID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_USE_PRESCRIPTION_HEADER] ADD  CONSTRAINT [DF_T_USE_PRESCRIPTION_HEADER_fEndFlag]  DEFAULT ((0)) FOR [fEndFlag]
GO

ALTER TABLE [dbo].[T_USE_PRESCRIPTION_HEADER] ADD  CONSTRAINT [DF_T_USE_PRESCRIPTION_HEADER_fDeleteFlag]  DEFAULT ((0)) FOR [fDeleteFlag]
GO

ALTER TABLE [dbo].[T_USE_PRESCRIPTION_HEADER] ADD  CONSTRAINT [DF_T_USE_PRESCRIPTION_HEADER_fCreateDate]  DEFAULT (getdate()) FOR [fCreateDate]
GO


