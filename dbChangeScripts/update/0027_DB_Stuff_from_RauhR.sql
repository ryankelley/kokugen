/*
Run this script on:

        localhost.KokugenDataVersioned    -  This database will be modified

to synchronize it with:

        localhost.KokugenData

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.0.0 from Red Gate Software Ltd at 4/12/2010 11:40:41 AM

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
PRINT N'Dropping foreign keys from [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] DROP
CONSTRAINT [fk_TimeRecord_to_Card],
CONSTRAINT [fk_TimeRecord_to_User],
CONSTRAINT [FK_Project_To_Time_Record],
CONSTRAINT [fk_TimeRecord_to_Task]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] DROP
CONSTRAINT [fk_Card_to_AssignedTo],
CONSTRAINT [fk_column_to_card],
CONSTRAINT [fk_card_to_column]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] DROP
CONSTRAINT [fk_Project_to_Archive],
CONSTRAINT [fk_Project_to_Backlog],
CONSTRAINT [fk_Project_to_Company]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] DROP
CONSTRAINT [FKD562296A86C5D437],
CONSTRAINT [FK_Project_To_Board_Columns]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[BoardColumns]'
GO
ALTER TABLE [dbo].[BoardColumns] DROP CONSTRAINT [PK__BoardCol__3214EC071273C1CD]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] DROP CONSTRAINT [PK__Cards__3214EC071DE57479]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Companies]'
GO
ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [PK__Companie__3214EC070AD2A005]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] DROP CONSTRAINT [PK__CustomBo__3214EC07164452B1]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [PK__Projects__3214EC070EA330E9]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [PK__Roles__3214EC0707020F21]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[TaskCategories]'
GO
ALTER TABLE [dbo].[TaskCategories] DROP CONSTRAINT [PK__TaskCate__3214EC077F60ED59]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] DROP CONSTRAINT [PK__TimeReco__3214EC071A14E395]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [PK__Users__3214EC0703317E3D]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Rebuilding [dbo].[Cards]'
GO
CREATE TABLE [dbo].[tmp_rg_xx_Cards]
(
[Id] [uniqueidentifier] NOT NULL,
[CardNumber] [int] NULL,
[Title] [nvarchar] (2047) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Details] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TimeEstimate] [int] NULL,
[Size] [int] NULL,
[Priority] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Deadline] [datetime] NULL,
[Color] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AssignedTo] [uniqueidentifier] NULL,
[Started] [datetime] NULL,
[DateCompleted] [datetime] NULL,
[StatusId] [int] NULL,
[CardOrder] [int] NULL,
[BlockReason] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Column_id] [uniqueidentifier] NULL,
[Project_id] [uniqueidentifier] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
INSERT INTO [dbo].[tmp_rg_xx_Cards]([Id], [CardNumber], [Title], [Details], [TimeEstimate], [Size], [Priority], [Deadline], [Color], [AssignedTo], [Started], [DateCompleted], [StatusId], [CardOrder], [BlockReason], [Column_id], [Project_id]) SELECT [Id], [CardNumber], [Title], [Details], [TimeEstimate], [Size], [Priority], [Deadline], [Color], [AssignedTo], [Started], [DateCompleted], [StatusId], [CardOrder], [BlockReason], [Column_id], [Project_id] FROM [dbo].[Cards]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
DROP TABLE [dbo].[Cards]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_rename N'[dbo].[tmp_rg_xx_Cards]', N'Cards'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Cards__3214EC07236943A5] on [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] ADD CONSTRAINT [PK__Cards__3214EC07236943A5] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] DROP
COLUMN [User_id]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__TimeReco__3214EC07367C1819] on [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] ADD CONSTRAINT [PK__TimeReco__3214EC07367C1819] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Rebuilding [dbo].[Users]'
GO
CREATE TABLE [dbo].[tmp_rg_xx_Users]
(
[Id] [uniqueidentifier] NOT NULL,
[UserName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[HashedPassword] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
INSERT INTO [dbo].[tmp_rg_xx_Users]([Id], [FirstName], [LastName], [HashedPassword]) SELECT [Id], [FirstName], [LastName], [HashedPassword] FROM [dbo].[Users]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
DROP TABLE [dbo].[Users]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
EXEC sp_rename N'[dbo].[tmp_rg_xx_Users]', N'Users'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Users__3214EC073A4CA8FD] on [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK__Users__3214EC073A4CA8FD] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__BoardCol__3214EC071BC821DD] on [dbo].[BoardColumns]'
GO
ALTER TABLE [dbo].[BoardColumns] ADD CONSTRAINT [PK__BoardCol__3214EC071BC821DD] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Companie__3214EC072739D489] on [dbo].[Companies]'
GO
ALTER TABLE [dbo].[Companies] ADD CONSTRAINT [PK__Companie__3214EC072739D489] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__CustomBo__3214EC071F98B2C1] on [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] ADD CONSTRAINT [PK__CustomBo__3214EC071F98B2C1] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Projects__3214EC0732AB8735] on [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] ADD CONSTRAINT [PK__Projects__3214EC0732AB8735] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Roles__3214EC072B0A656D] on [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] ADD CONSTRAINT [PK__Roles__3214EC072B0A656D] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__TaskCate__3214EC072EDAF651] on [dbo].[TaskCategories]'
GO
ALTER TABLE [dbo].[TaskCategories] ADD CONSTRAINT [PK__TaskCate__3214EC072EDAF651] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] ADD
CONSTRAINT [fk_TimeRecord_to_Card] FOREIGN KEY ([Card_id]) REFERENCES [dbo].[Cards] ([Id]),
CONSTRAINT [FK_Project_To_Time_Record] FOREIGN KEY ([Project_id]) REFERENCES [dbo].[Projects] ([Id]),
CONSTRAINT [fk_TimeRecord_to_Task] FOREIGN KEY ([Task_id]) REFERENCES [dbo].[TaskCategories] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] ADD
CONSTRAINT [fk_card_to_column] FOREIGN KEY ([Column_id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [fk_column_to_card] FOREIGN KEY ([Project_id]) REFERENCES [dbo].[Projects] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] ADD
CONSTRAINT [fk_Project_to_Archive] FOREIGN KEY ([Archive_id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [fk_Project_to_Backlog] FOREIGN KEY ([Backlog_id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [fk_Project_to_Company] FOREIGN KEY ([Company_id]) REFERENCES [dbo].[Companies] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] ADD
CONSTRAINT [FKD562296A86C5D437] FOREIGN KEY ([Id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [FK_Project_To_Board_Columns] FOREIGN KEY ([Project_id]) REFERENCES [dbo].[Projects] ([Id])
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
