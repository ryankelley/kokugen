/****** Object:  Table [dbo].[DimTime]    Script Date: 04/23/2010 09:20:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DimTime]') AND type in (N'U'))
DROP TABLE [dbo].[DimTime]
GO

/****** Object:  Table [dbo].[DimTime]    Script Date: 04/23/2010 09:20:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DimTime](
	[DateValue] [datetime] NULL
) ON [PRIMARY]

GO

with mycte AS
(
select cast('2009-01-01' as datetime) DateValue
union ALL
select DateValue + 1
from mycte
where DateValue + 1 <= '2012-12-31'
)

insert into DimTime (DateValue)
select DateValue from mycte
OPTION (MAXRECURSION 0)
