IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Enterprise] (
    [Id] uniqueidentifier NOT NULL,
    [Name] NVARCHAR(80) NOT NULL,
    [CreateAt] DATETIME NOT NULL,
    [IdOwner] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Enterprise] PRIMARY KEY ([Id])
);

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [Email] NVARCHAR(180) NOT NULL,
    [Password] NVARCHAR(100) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);

CREATE TABLE [Owner] (
    [Id] uniqueidentifier NOT NULL,
    [Name] VARCHAR(170) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [EnterpriseId] uniqueidentifier NULL,
    CONSTRAINT [PK_Owner] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Owner_Enterprise] FOREIGN KEY ([EnterpriseId]) REFERENCES [Enterprise] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Owner_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Comments] (
    [Id] uniqueidentifier NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    [MemberId] uniqueidentifier NOT NULL,
    [Message] NVARCHAR(250) NOT NULL,
    [CreateAt] SMALLDATETIME NOT NULL,
    [CommentParentId] uniqueidentifier NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Comments_CommentParentId] FOREIGN KEY ([CommentParentId]) REFERENCES [Comments] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Mark] (
    [Id] uniqueidentifier NOT NULL,
    [TeamId] uniqueidentifier NOT NULL,
    [Title] VARCHAR(120) NOT NULL,
    [Descriptor] VARCHAR(180) NOT NULL,
    [TaskCount] SMALLINT NOT NULL DEFAULT CAST(0 AS SMALLINT),
    [TaskCountDone] SMALLINT NOT NULL DEFAULT CAST(0 AS SMALLINT),
    [Percentage] TINYINT NOT NULL DEFAULT CAST(0 AS TINYINT),
    [Done] bit NOT NULL,
    CONSTRAINT [PK_Mark] PRIMARY KEY ([Id])
);

CREATE TABLE [Member] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Name] VARCHAR(180) NOT NULL,
    [TeamId] uniqueidentifier NULL,
    [Role] VARCHAR(100) NOT NULL,
    [ManagedTeamId] uniqueidentifier NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Member_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Team] (
    [Id] uniqueidentifier NOT NULL,
    [Name] VARCHAR(120) NOT NULL,
    [ManagerId] uniqueidentifier NOT NULL,
    [EnterpriseId] uniqueidentifier NULL,
    [PercentageByMonthCurrent] TINYINT NOT NULL DEFAULT CAST(0 AS TINYINT),
    CONSTRAINT [PK_Team] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Team_Enterprise] FOREIGN KEY ([EnterpriseId]) REFERENCES [Enterprise] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Team_Manager] FOREIGN KEY ([ManagerId]) REFERENCES [Member] ([Id])
);

CREATE TABLE [RecurringTask] (
    [Id] uniqueidentifier NOT NULL,
    [TeamId] uniqueidentifier NOT NULL,
    [DayMonth] TINYINT NOT NULL,
    [DaysActiveTask] int NOT NULL,
    [Title] NVARCHAR(120) NOT NULL,
    [Description] NVARCHAR(180) NOT NULL,
    [Priority] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_RecurringTask] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RecurringTask_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Task] (
    [Id] uniqueidentifier NOT NULL,
    [MemberId] uniqueidentifier NULL,
    [CreateAt] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Percentage] TINYINT NOT NULL DEFAULT CAST(0 AS TINYINT),
    [Title] NVARCHAR(150) NOT NULL,
    [Description] NVARCHAR(200) NOT NULL,
    [Active] BIT NOT NULL DEFAULT CAST(1 AS BIT),
    [TeamId] uniqueidentifier NOT NULL,
    [Priority] VARCHAR(15) NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Task_Member] FOREIGN KEY ([MemberId]) REFERENCES [Member] ([Id]),
    CONSTRAINT [FK_Task_Team_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Comments_CommentParentId] ON [Comments] ([CommentParentId]);

CREATE INDEX [IX_Comments_MemberId] ON [Comments] ([MemberId]);

CREATE INDEX [IX_Comments_Task] ON [Comments] ([TaskId]);

CREATE INDEX [IX_Mark_TeamId] ON [Mark] ([TeamId]);

CREATE INDEX [IX_Member_ManagedTeamId] ON [Member] ([ManagedTeamId]);

CREATE INDEX [IX_Member_TeamId] ON [Member] ([TeamId]);

CREATE INDEX [IX_Member_UserId] ON [Member] ([UserId]);

CREATE UNIQUE INDEX [IX_Owner_EnterpriseId] ON [Owner] ([EnterpriseId]) WHERE [EnterpriseId] IS NOT NULL;

CREATE UNIQUE INDEX [IX_Owner_UserId] ON [Owner] ([UserId]);

CREATE INDEX [IX_RecurringTask_TeamId] ON [RecurringTask] ([TeamId]);

CREATE INDEX [IX_Task_MemberId] ON [Task] ([MemberId]);

CREATE INDEX [IX_Task_TeamId] ON [Task] ([TeamId]);

CREATE INDEX [IX_Team_EnterpriseId] ON [Team] ([EnterpriseId]);

CREATE UNIQUE INDEX [IX_Team_ManagerId] ON [Team] ([ManagerId]);

CREATE UNIQUE INDEX [IX_User_Email] ON [User] ([Email]);

ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Member] FOREIGN KEY ([MemberId]) REFERENCES [Member] ([Id]) ON DELETE CASCADE;

ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Task] FOREIGN KEY ([TaskId]) REFERENCES [Task] ([Id]) ON DELETE CASCADE;

ALTER TABLE [Mark] ADD CONSTRAINT [FK_Mark_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE;

ALTER TABLE [Member] ADD CONSTRAINT [FK_Member_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]);

ALTER TABLE [Member] ADD CONSTRAINT [FK_Member_Team_ManagedTeamId] FOREIGN KEY ([ManagedTeamId]) REFERENCES [Team] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260121121823_InitialCreate', N'10.0.1');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Task]') AND [c].[name] = N'EndDate');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Task] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Task] ALTER COLUMN [EndDate] SMALLDATETIME NOT NULL;

DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Task]') AND [c].[name] = N'CreateAt');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Task] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [Task] ALTER COLUMN [CreateAt] SMALLDATETIME NOT NULL;

ALTER TABLE [Task] ADD [KanbanCurrent] TINYINT NULL;

CREATE TABLE [Kanban] (
    [Id] uniqueidentifier NOT NULL,
    [KanbanTitle] VARCHAR(120) NOT NULL,
    [KanbanOrder] TINYINT NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Kanban] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Kanban_Task_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Task] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Member_Name] ON [Member] ([Name]);

CREATE INDEX [IX_Kanban_TaskId] ON [Kanban] ([TaskId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260124133531_v2', N'10.0.1');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Kanban] DROP CONSTRAINT [FK_Kanban_Task_TaskId];

EXEC sp_rename N'[Kanban].[TaskId]', N'TeamId', 'COLUMN';

EXEC sp_rename N'[Kanban].[IX_Kanban_TaskId]', N'IX_Kanban_TeamId', 'INDEX';

ALTER TABLE [Kanban] ADD CONSTRAINT [FK_Kanban_Team_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260125131841_v3', N'10.0.1');

COMMIT;
GO

