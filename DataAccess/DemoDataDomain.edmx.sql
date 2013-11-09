
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/08/2013 15:11:48
-- Generated from EDMX file: D:\work\workspace\KonyES\KonyES_mock_server\src\demo\DataAccess\DemoDataDomain.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [demo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RoleUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_RoleUser];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_GroupEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTrackLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackLogs] DROP CONSTRAINT [FK_UserTrackLog];
GO
IF OBJECT_ID(N'[dbo].[FK_EventTrackLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackLogs] DROP CONSTRAINT [FK_EventTrackLog];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_UserGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUsersInGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersInGroups] DROP CONSTRAINT [FK_UserUsersInGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupUsersInGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersInGroups] DROP CONSTRAINT [FK_GroupUsersInGroups];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[TrackLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrackLogs];
GO
IF OBJECT_ID(N'[dbo].[UsersInGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersInGroups];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Role_Id] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Owner_Id] int  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [BaseLat] float  NOT NULL,
    [BaseLon] float  NOT NULL,
    [BaseRadius] float  NOT NULL,
    [Group_Id] int  NOT NULL
);
GO

-- Creating table 'TrackLogs'
CREATE TABLE [dbo].[TrackLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Lat] float  NOT NULL,
    [Lon] float  NOT NULL,
    [LogDateTime] datetime  NOT NULL,
    [User_Id] int  NOT NULL,
    [Event_Id] int  NOT NULL
);
GO

-- Creating table 'UsersInGroups'
CREATE TABLE [dbo].[UsersInGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsActive] bit  NOT NULL,
    [User_Id] int  NOT NULL,
    [Group_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TrackLogs'
ALTER TABLE [dbo].[TrackLogs]
ADD CONSTRAINT [PK_TrackLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersInGroups'
ALTER TABLE [dbo].[UsersInGroups]
ADD CONSTRAINT [PK_UsersInGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Role_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([Role_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[Users]
    ([Role_Id]);
GO

-- Creating foreign key on [Group_Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_GroupEvent]
    FOREIGN KEY ([Group_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupEvent'
CREATE INDEX [IX_FK_GroupEvent]
ON [dbo].[Events]
    ([Group_Id]);
GO

-- Creating foreign key on [User_Id] in table 'TrackLogs'
ALTER TABLE [dbo].[TrackLogs]
ADD CONSTRAINT [FK_UserTrackLog]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTrackLog'
CREATE INDEX [IX_FK_UserTrackLog]
ON [dbo].[TrackLogs]
    ([User_Id]);
GO

-- Creating foreign key on [Event_Id] in table 'TrackLogs'
ALTER TABLE [dbo].[TrackLogs]
ADD CONSTRAINT [FK_EventTrackLog]
    FOREIGN KEY ([Event_Id])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventTrackLog'
CREATE INDEX [IX_FK_EventTrackLog]
ON [dbo].[TrackLogs]
    ([Event_Id]);
GO

-- Creating foreign key on [Owner_Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_UserGroup]
    FOREIGN KEY ([Owner_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroup'
CREATE INDEX [IX_FK_UserGroup]
ON [dbo].[Groups]
    ([Owner_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UsersInGroups'
ALTER TABLE [dbo].[UsersInGroups]
ADD CONSTRAINT [FK_UserUsersInGroups]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUsersInGroups'
CREATE INDEX [IX_FK_UserUsersInGroups]
ON [dbo].[UsersInGroups]
    ([User_Id]);
GO

-- Creating foreign key on [Group_Id] in table 'UsersInGroups'
ALTER TABLE [dbo].[UsersInGroups]
ADD CONSTRAINT [FK_GroupUsersInGroups]
    FOREIGN KEY ([Group_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupUsersInGroups'
CREATE INDEX [IX_FK_GroupUsersInGroups]
ON [dbo].[UsersInGroups]
    ([Group_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------