USE [charlieDB]
GO

/****** Object:  Table [dbo].[T_USE_ONLINE_DEMAND]    Script Date: 2023/11/09 15:17:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_USE_ONLINE_DEMAND](
	[ApplyNo] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[RemoteFlag] [char](1) NULL,
	[GoodsID] [nvarchar](13) NULL,
	[ApplyDate] [datetime] NULL,
	[DeleteFlag] [char](1) NULL,
	[SalesDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreatePerson] [nvarchar](30) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatePerson] [nvarchar](30) NULL,
 CONSTRAINT [PK_T_USE_ONLINE_DEMAND] PRIMARY KEY CLUSTERED 
(
	[ApplyNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_USE_ONLINE_DEMAND] ADD  CONSTRAINT [DF_T_USE_ONLINE_DEMAND_RemoteFlag]  DEFAULT ((0)) FOR [RemoteFlag]
GO

ALTER TABLE [dbo].[T_USE_ONLINE_DEMAND] ADD  CONSTRAINT [DF_T_USE_ONLINE_DEMAND_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO



