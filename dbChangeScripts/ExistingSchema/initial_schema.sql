/*
Run this script on:

        (local).KokugenDataVersioned    -  This database will be modified

to synchronize it with:

        (local).KokugenData

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.0.0 from Red Gate Software Ltd at 4/13/2010 6:50:14 PM

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
PRINT N'Creating [dbo].[BoardColumns]'
GO
CREATE TABLE [dbo].[BoardColumns]
(
[Id] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CardLimit] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__BoardCol__3214EC077F60ED59] on [dbo].[BoardColumns]'
GO
ALTER TABLE [dbo].[BoardColumns] ADD CONSTRAINT [PK__BoardCol__3214EC077F60ED59] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Cards]'
GO
CREATE TABLE [dbo].[Cards]
(
[Id] [uniqueidentifier] NOT NULL,
[CardNumber] [int] NOT NULL IDENTITY(1, 1),
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
PRINT N'Creating primary key [PK__Cards__3214EC0707020F21] on [dbo].[Cards]'
GO
ALTER TABLE [dbo].[Cards] ADD CONSTRAINT [PK__Cards__3214EC0707020F21] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Projects]'
GO
CREATE TABLE [dbo].[Projects]
(
[Id] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[NumberOfSessions] [int] NULL,
[AverageTimeSpentPerSession] [float] NULL,
[TotalTime] [float] NULL,
[Description] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Company_id] [uniqueidentifier] NULL,
[Backlog_id] [uniqueidentifier] NULL,
[Archive_id] [uniqueidentifier] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Projects__3214EC071ED998B2] on [dbo].[Projects]'
GO
ALTER TABLE [dbo].[Projects] ADD CONSTRAINT [PK__Projects__3214EC071ED998B2] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[CustomBoardColumn]'
GO
CREATE TABLE [dbo].[CustomBoardColumn]
(
[Id] [uniqueidentifier] NOT NULL,
[ColumnOrder] [int] NULL,
[Project_id] [uniqueidentifier] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__CustomBo__3214EC0703317E3D] on [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] ADD CONSTRAINT [PK__CustomBo__3214EC0703317E3D] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Permissions]'
GO
CREATE TABLE [dbo].[Permissions]
(
[Id] [uniqueidentifier] NOT NULL,
[PermissionNameId] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Permissi__3214EC070EA330E9] on [dbo].[Permissions]'
GO
ALTER TABLE [dbo].[Permissions] ADD CONSTRAINT [PK__Permissi__3214EC070EA330E9] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[RoleToPermission]'
GO
CREATE TABLE [dbo].[RoleToPermission]
(
[Role_id] [uniqueidentifier] NOT NULL,
[Permission_id] [uniqueidentifier] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Companies]'
GO
CREATE TABLE [dbo].[Companies]
(
[Id] [uniqueidentifier] NOT NULL,
[StreetLine1] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StreetLine2] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[State] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ZipCode] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Companie__3214EC070AD2A005] on [dbo].[Companies]'
GO
ALTER TABLE [dbo].[Companies] ADD CONSTRAINT [PK__Companie__3214EC070AD2A005] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[TimeRecords]'
GO
CREATE TABLE [dbo].[TimeRecords]
(
[Id] [uniqueidentifier] NOT NULL,
[Duration] [float] NULL,
[Billable] [float] NULL,
[StartTime] [datetime] NULL,
[EndTime] [datetime] NULL,
[Description] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserId] [uniqueidentifier] NULL,
[Task_id] [uniqueidentifier] NULL,
[Project_id] [uniqueidentifier] NULL,
[Card_id] [uniqueidentifier] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__TimeReco__3214EC0722AA2996] on [dbo].[TimeRecords]'
GO
ALTER TABLE [dbo].[TimeRecords] ADD CONSTRAINT [PK__TimeReco__3214EC0722AA2996] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Roles]'
GO
CREATE TABLE [dbo].[Roles]
(
[Id] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Roles__3214EC071273C1CD] on [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] ADD CONSTRAINT [PK__Roles__3214EC071273C1CD] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[RoleToUser]'
GO
CREATE TABLE [dbo].[RoleToUser]
(
[Role_id] [uniqueidentifier] NOT NULL,
[User_id] [uniqueidentifier] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[TaskCategories]'
GO
CREATE TABLE [dbo].[TaskCategories]
(
[Id] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__TaskCate__3214EC071B0907CE] on [dbo].[TaskCategories]'
GO
ALTER TABLE [dbo].[TaskCategories] ADD CONSTRAINT [PK__TaskCate__3214EC071B0907CE] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Users]'
GO
CREATE TABLE [dbo].[Users]
(
[Id] [uniqueidentifier] NOT NULL,
[UserName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsLocked] [bit] NULL,
[IsActivated] [bit] NULL,
[FirstName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Password] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Users__3214EC07267ABA7A] on [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK__Users__3214EC07267ABA7A] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] ADD CONSTRAINT [UQ__Roles__737584F615502E78] UNIQUE NONCLUSTERED  ([Name])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ__Users__C9F284562C3393D0] UNIQUE NONCLUSTERED  ([UserName])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ__Users__A9D1053429572725] UNIQUE NONCLUSTERED  ([Email])
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
PRINT N'Adding foreign keys to [dbo].[CustomBoardColumn]'
GO
ALTER TABLE [dbo].[CustomBoardColumn] ADD
CONSTRAINT [fk_CustomBoardColumn] FOREIGN KEY ([Id]) REFERENCES [dbo].[BoardColumns] ([Id]),
CONSTRAINT [FK_Project_To_Board_Columns] FOREIGN KEY ([Project_id]) REFERENCES [dbo].[Projects] ([Id])
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
PRINT N'Adding foreign keys to [dbo].[RoleToPermission]'
GO
ALTER TABLE [dbo].[RoleToPermission] ADD
CONSTRAINT [fk_permission_to_role] FOREIGN KEY ([Permission_id]) REFERENCES [dbo].[Permissions] ([Id]),
CONSTRAINT [fk_role_to_permission] FOREIGN KEY ([Role_id]) REFERENCES [dbo].[Roles] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[RoleToUser]'
GO
ALTER TABLE [dbo].[RoleToUser] ADD
CONSTRAINT [fk_role_to_user] FOREIGN KEY ([Role_id]) REFERENCES [dbo].[Roles] ([Id]),
CONSTRAINT [fk_user_to_role] FOREIGN KEY ([User_id]) REFERENCES [dbo].[Users] ([Id])
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
