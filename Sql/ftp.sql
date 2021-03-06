SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Group_access]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Group_access](
	[IndexNo] [int] NOT NULL CONSTRAINT [DF_Group_access_IndexNo]  DEFAULT ((0)),
	[User] [nvarchar](50) NOT NULL,
	[Access] [nvarchar](200) NULL,
 CONSTRAINT [PK_Group_access] PRIMARY KEY CLUSTERED 
(
	[IndexNo] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Group_accounts]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Group_accounts](
	[User] [nvarchar](50) NOT NULL,
	[Access] [nvarchar](200) NULL,
	[Notes] [nvarchar](200) NULL,
 CONSTRAINT [PK_Group_accounts] PRIMARY KEY CLUSTERED 
(
	[User] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Group_IP_access]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Group_IP_access](
	[IndexNo] [int] NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[Access] [nvarchar](200) NULL,
 CONSTRAINT [PK_Group_IP_access] PRIMARY KEY CLUSTERED 
(
	[IndexNo] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[User_access]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[User_access](
	[IndexNo] [int] NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[Access] [nvarchar](200) NULL,
 CONSTRAINT [PK_User_access] PRIMARY KEY CLUSTERED 
(
	[IndexNo] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[User_IP_access]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[User_IP_access](
	[IndexNo] [smallint] NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[Access] [nvarchar](200) NULL,
 CONSTRAINT [PK_User_IP_access] PRIMARY KEY CLUSTERED 
(
	[IndexNo] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[User_accounts]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[User_accounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[Access] [nvarchar](200) NULL,
	[Disable] [bit] NULL,
	[Password] [nvarchar](50) NULL,
	[ChangePass] [bit] NULL CONSTRAINT [DF_User_accounts_ChangePass]  DEFAULT ((1)),
	[SKey] [nvarchar](50) NULL,
	[HomeDir] [nvarchar](255) NULL,
	[RelPaths] [bit] NULL CONSTRAINT [DF_User_accounts_RelPaths]  DEFAULT ((1)),
	[MaxUsers] [int] NULL CONSTRAINT [DF_User_accounts_MaxUsers]  DEFAULT ((10)),
	[Expiration] [datetime] NULL,
	[RatioUp] [int] NULL CONSTRAINT [DF_User_accounts_RatioUp]  DEFAULT ((0)),
	[RatioDown] [int] NULL CONSTRAINT [DF_User_accounts_RatioDown]  DEFAULT ((0)),
	[RatioCredit] [real] NULL CONSTRAINT [DF_User_accounts_RatioCredit]  DEFAULT ((0)),
	[RatioType] [tinyint] NULL CONSTRAINT [DF_User_accounts_RatioType]  DEFAULT ((0)),
	[QuotaEnable] [bit] NULL,
	[QuotaMax] [int] NULL,
	[QuotaCurrent] [int] NULL CONSTRAINT [DF_User_accounts_QuotaCurrent]  DEFAULT ((0)),
	[Groups] [nvarchar](255) NULL,
	[Privilege] [tinyint] NULL,
	[PassType] [tinyint] NULL CONSTRAINT [DF_User_accounts_PassType]  DEFAULT ((0)),
	[RegIp] [nvarchar](50) NULL,
	[RegTime] [datetime] NULL CONSTRAINT [DF_User_accounts_RegTime]  DEFAULT (getdate()),
	[Email] [nvarchar](50) NULL,
	[Expirationtype] [int] NULL CONSTRAINT [DF_User_accounts_Expirationtype]  DEFAULT ((0)),
 CONSTRAINT [PK_User_accounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END