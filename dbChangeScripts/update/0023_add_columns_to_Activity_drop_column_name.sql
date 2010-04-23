/*
Run this script on:

        localhost.KokugenDataVersioned    -  This database will be modified

to synchronize it with:

        localhost.KokugenData

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.1.0 from Red Gate Software Ltd at 4/22/2010 11:09:53 PM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Altering [dbo].[CardActivities]'
GO
ALTER TABLE [dbo].[CardActivities] ADD
[Leaving_id] [uniqueidentifier] NULL,
[Entering_id] [uniqueidentifier] NULL

ALTER TABLE [dbo].[CardActivities] DROP COLUMN
[ColumnName] 

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[CardActivities]'
GO
ALTER TABLE [dbo].[CardActivities] ADD
CONSTRAINT [fk_activity_to_columnin] FOREIGN KEY ([Leaving_id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [fk_activity_to_columnout] FOREIGN KEY ([Entering_id]) REFERENCES [dbo].[BoardColumns] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
