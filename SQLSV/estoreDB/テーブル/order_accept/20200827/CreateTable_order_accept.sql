USE [estoreDB]
GO

/****** Object:  Table [dbo].[order_accept]    Script Date: 2020/07/27 11:13:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
	2020/07/27 estore入替作業に伴い、[E-STORESV].mic_orderaccept.dbo.order_acceptのデータをestoredb.dbo.order_acceptに移行する by 勝呂

*/

CREATE TABLE [dbo].[order_accept](
	[order_accept_id] [int] IDENTITY(1,1) NOT NULL,
	[order_no] [int] NOT NULL,
	[customer_no] [int] NOT NULL,
	[pref_arrival_date] [datetime] NULL,
	[goods_code] [varchar](13) NOT NULL,
	[web_price] [int] NOT NULL,
	[order_num] [int] NOT NULL,
	[order_dt] [datetime] NOT NULL,
	[send_dt] [datetime] NOT NULL,
 CONSTRAINT [PK_order_accept] PRIMARY KEY CLUSTERED 
(
	[order_accept_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[order_accept] ADD  CONSTRAINT [DF_order_accept_order_no]  DEFAULT ((0)) FOR [order_no]
GO

ALTER TABLE [dbo].[order_accept] ADD  CONSTRAINT [DF_order_accept_customer_no]  DEFAULT ((0)) FOR [customer_no]
GO

ALTER TABLE [dbo].[order_accept] ADD  CONSTRAINT [DF_order_accept_goods_code]  DEFAULT ('') FOR [goods_code]
GO

ALTER TABLE [dbo].[order_accept] ADD  CONSTRAINT [DF_order_accept_web_price]  DEFAULT ((0)) FOR [web_price]
GO

ALTER TABLE [dbo].[order_accept] ADD  CONSTRAINT [DF_order_accept_order_num]  DEFAULT ((0)) FOR [order_num]
GO

