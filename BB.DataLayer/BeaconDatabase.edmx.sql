
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/11/2015 15:05:17
-- Generated from EDMX file: C:\Users\Jonathan\Desktop\BeaconBoard\BB.DataLayer\BeaconDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MscStePrescott];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Beacons'
CREATE TABLE [dbo].[Beacons] (
    [BeaconID] uniqueidentifier  NOT NULL,
    [Major] int  NOT NULL,
    [Minor] int  NOT NULL,
    [RoomID] uniqueidentifier  NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [RoomID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [CourseID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Lessons'
CREATE TABLE [dbo].[Lessons] (
    [LessonID] uniqueidentifier  NOT NULL,
    [RoomID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Sessions'
CREATE TABLE [dbo].[Sessions] (
    [SessionID] uniqueidentifier  NOT NULL,
    [LessonID] uniqueidentifier  NOT NULL,
    [ScheduledDate] datetime  NOT NULL,
    [RoomID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Resources'
CREATE TABLE [dbo].[Resources] (
    [ResourceID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [URLString] nvarchar(max)  NOT NULL,
    [ResourceTypeID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ResourceTypes'
CREATE TABLE [dbo].[ResourceTypes] (
    [ResourceTypeID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [OtherNames] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [EmailAddress] nvarchar(max)  NULL
);
GO

-- Creating table 'Users_Student'
CREATE TABLE [dbo].[Users_Student] (
    [UserID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Users_Lecturer'
CREATE TABLE [dbo].[Users_Lecturer] (
    [UserID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'LessonResource'
CREATE TABLE [dbo].[LessonResource] (
    [Lessons_LessonID] uniqueidentifier  NOT NULL,
    [Resources_ResourceID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'CourseLesson'
CREATE TABLE [dbo].[CourseLesson] (
    [Courses_CourseID] uniqueidentifier  NOT NULL,
    [Lessons_LessonID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'StudentCourse'
CREATE TABLE [dbo].[StudentCourse] (
    [Students_UserID] uniqueidentifier  NOT NULL,
    [Courses_CourseID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'LecturerCourse'
CREATE TABLE [dbo].[LecturerCourse] (
    [Lecturers_UserID] uniqueidentifier  NOT NULL,
    [Courses_CourseID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'LecturerSession'
CREATE TABLE [dbo].[LecturerSession] (
    [Lecturers_UserID] uniqueidentifier  NOT NULL,
    [Sessions_SessionID] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BeaconID] in table 'Beacons'
ALTER TABLE [dbo].[Beacons]
ADD CONSTRAINT [PK_Beacons]
    PRIMARY KEY CLUSTERED ([BeaconID] ASC);
GO

-- Creating primary key on [RoomID] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([RoomID] ASC);
GO

-- Creating primary key on [CourseID] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([CourseID] ASC);
GO

-- Creating primary key on [LessonID] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [PK_Lessons]
    PRIMARY KEY CLUSTERED ([LessonID] ASC);
GO

-- Creating primary key on [SessionID] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [PK_Sessions]
    PRIMARY KEY CLUSTERED ([SessionID] ASC);
GO

-- Creating primary key on [ResourceID] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [PK_Resources]
    PRIMARY KEY CLUSTERED ([ResourceID] ASC);
GO

-- Creating primary key on [ResourceTypeID] in table 'ResourceTypes'
ALTER TABLE [dbo].[ResourceTypes]
ADD CONSTRAINT [PK_ResourceTypes]
    PRIMARY KEY CLUSTERED ([ResourceTypeID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users_Student'
ALTER TABLE [dbo].[Users_Student]
ADD CONSTRAINT [PK_Users_Student]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users_Lecturer'
ALTER TABLE [dbo].[Users_Lecturer]
ADD CONSTRAINT [PK_Users_Lecturer]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [Lessons_LessonID], [Resources_ResourceID] in table 'LessonResource'
ALTER TABLE [dbo].[LessonResource]
ADD CONSTRAINT [PK_LessonResource]
    PRIMARY KEY CLUSTERED ([Lessons_LessonID], [Resources_ResourceID] ASC);
GO

-- Creating primary key on [Courses_CourseID], [Lessons_LessonID] in table 'CourseLesson'
ALTER TABLE [dbo].[CourseLesson]
ADD CONSTRAINT [PK_CourseLesson]
    PRIMARY KEY CLUSTERED ([Courses_CourseID], [Lessons_LessonID] ASC);
GO

-- Creating primary key on [Students_UserID], [Courses_CourseID] in table 'StudentCourse'
ALTER TABLE [dbo].[StudentCourse]
ADD CONSTRAINT [PK_StudentCourse]
    PRIMARY KEY CLUSTERED ([Students_UserID], [Courses_CourseID] ASC);
GO

-- Creating primary key on [Lecturers_UserID], [Courses_CourseID] in table 'LecturerCourse'
ALTER TABLE [dbo].[LecturerCourse]
ADD CONSTRAINT [PK_LecturerCourse]
    PRIMARY KEY CLUSTERED ([Lecturers_UserID], [Courses_CourseID] ASC);
GO

-- Creating primary key on [Lecturers_UserID], [Sessions_SessionID] in table 'LecturerSession'
ALTER TABLE [dbo].[LecturerSession]
ADD CONSTRAINT [PK_LecturerSession]
    PRIMARY KEY CLUSTERED ([Lecturers_UserID], [Sessions_SessionID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoomID] in table 'Beacons'
ALTER TABLE [dbo].[Beacons]
ADD CONSTRAINT [FK_RoomBeacon]
    FOREIGN KEY ([RoomID])
    REFERENCES [dbo].[Rooms]
        ([RoomID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomBeacon'
CREATE INDEX [IX_FK_RoomBeacon]
ON [dbo].[Beacons]
    ([RoomID]);
GO

-- Creating foreign key on [LessonID] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_LessonSession]
    FOREIGN KEY ([LessonID])
    REFERENCES [dbo].[Lessons]
        ([LessonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LessonSession'
CREATE INDEX [IX_FK_LessonSession]
ON [dbo].[Sessions]
    ([LessonID]);
GO

-- Creating foreign key on [ResourceTypeID] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [FK_ResourceResourceType]
    FOREIGN KEY ([ResourceTypeID])
    REFERENCES [dbo].[ResourceTypes]
        ([ResourceTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResourceResourceType'
CREATE INDEX [IX_FK_ResourceResourceType]
ON [dbo].[Resources]
    ([ResourceTypeID]);
GO

-- Creating foreign key on [Lessons_LessonID] in table 'LessonResource'
ALTER TABLE [dbo].[LessonResource]
ADD CONSTRAINT [FK_LessonResource_Lesson]
    FOREIGN KEY ([Lessons_LessonID])
    REFERENCES [dbo].[Lessons]
        ([LessonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Resources_ResourceID] in table 'LessonResource'
ALTER TABLE [dbo].[LessonResource]
ADD CONSTRAINT [FK_LessonResource_Resource]
    FOREIGN KEY ([Resources_ResourceID])
    REFERENCES [dbo].[Resources]
        ([ResourceID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LessonResource_Resource'
CREATE INDEX [IX_FK_LessonResource_Resource]
ON [dbo].[LessonResource]
    ([Resources_ResourceID]);
GO

-- Creating foreign key on [Courses_CourseID] in table 'CourseLesson'
ALTER TABLE [dbo].[CourseLesson]
ADD CONSTRAINT [FK_CourseLesson_Course]
    FOREIGN KEY ([Courses_CourseID])
    REFERENCES [dbo].[Courses]
        ([CourseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Lessons_LessonID] in table 'CourseLesson'
ALTER TABLE [dbo].[CourseLesson]
ADD CONSTRAINT [FK_CourseLesson_Lesson]
    FOREIGN KEY ([Lessons_LessonID])
    REFERENCES [dbo].[Lessons]
        ([LessonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseLesson_Lesson'
CREATE INDEX [IX_FK_CourseLesson_Lesson]
ON [dbo].[CourseLesson]
    ([Lessons_LessonID]);
GO

-- Creating foreign key on [Students_UserID] in table 'StudentCourse'
ALTER TABLE [dbo].[StudentCourse]
ADD CONSTRAINT [FK_StudentCourse_Student]
    FOREIGN KEY ([Students_UserID])
    REFERENCES [dbo].[Users_Student]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Courses_CourseID] in table 'StudentCourse'
ALTER TABLE [dbo].[StudentCourse]
ADD CONSTRAINT [FK_StudentCourse_Course]
    FOREIGN KEY ([Courses_CourseID])
    REFERENCES [dbo].[Courses]
        ([CourseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentCourse_Course'
CREATE INDEX [IX_FK_StudentCourse_Course]
ON [dbo].[StudentCourse]
    ([Courses_CourseID]);
GO

-- Creating foreign key on [Lecturers_UserID] in table 'LecturerCourse'
ALTER TABLE [dbo].[LecturerCourse]
ADD CONSTRAINT [FK_LecturerCourse_Lecturer]
    FOREIGN KEY ([Lecturers_UserID])
    REFERENCES [dbo].[Users_Lecturer]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Courses_CourseID] in table 'LecturerCourse'
ALTER TABLE [dbo].[LecturerCourse]
ADD CONSTRAINT [FK_LecturerCourse_Course]
    FOREIGN KEY ([Courses_CourseID])
    REFERENCES [dbo].[Courses]
        ([CourseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LecturerCourse_Course'
CREATE INDEX [IX_FK_LecturerCourse_Course]
ON [dbo].[LecturerCourse]
    ([Courses_CourseID]);
GO

-- Creating foreign key on [Lecturers_UserID] in table 'LecturerSession'
ALTER TABLE [dbo].[LecturerSession]
ADD CONSTRAINT [FK_LecturerSession_Lecturer]
    FOREIGN KEY ([Lecturers_UserID])
    REFERENCES [dbo].[Users_Lecturer]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Sessions_SessionID] in table 'LecturerSession'
ALTER TABLE [dbo].[LecturerSession]
ADD CONSTRAINT [FK_LecturerSession_Session]
    FOREIGN KEY ([Sessions_SessionID])
    REFERENCES [dbo].[Sessions]
        ([SessionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LecturerSession_Session'
CREATE INDEX [IX_FK_LecturerSession_Session]
ON [dbo].[LecturerSession]
    ([Sessions_SessionID]);
GO

-- Creating foreign key on [RoomID] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionRoom]
    FOREIGN KEY ([RoomID])
    REFERENCES [dbo].[Rooms]
        ([RoomID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionRoom'
CREATE INDEX [IX_FK_SessionRoom]
ON [dbo].[Sessions]
    ([RoomID]);
GO

-- Creating foreign key on [UserID] in table 'Users_Student'
ALTER TABLE [dbo].[Users_Student]
ADD CONSTRAINT [FK_Student_inherits_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserID] in table 'Users_Lecturer'
ALTER TABLE [dbo].[Users_Lecturer]
ADD CONSTRAINT [FK_Lecturer_inherits_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------