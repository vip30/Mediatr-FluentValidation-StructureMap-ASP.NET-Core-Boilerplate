
CREATE TABLE [dbo].[SystemLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [nvarchar](max) NOT NULL,
	[LogLevel] [nvarchar](max) NOT NULL,
	[LogLogger] [nvarchar](max) NOT NULL,
	[LogMessage] [nvarchar](max) NOT NULL,
	[LogMachineName] [nvarchar](max) NULL,
	[LogCallSite] [nvarchar](max) NULL,
	[LogThread] [nvarchar](max) NULL,
	[LogException] [nvarchar](max) NULL,
	[LogStacktrace] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.SystemLog] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


