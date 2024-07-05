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
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [Courses] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Duration] int NULL,
        [Level] int NULL,
        CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [Instructors] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        CONSTRAINT [PK_Instructors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [Students] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [DateEnrolled] datetime2 NULL,
        CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [CourseInstructor] (
        [CourseInstructorsId] uniqueidentifier NOT NULL,
        [CourseInstructorsId1] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_CourseInstructor] PRIMARY KEY ([CourseInstructorsId], [CourseInstructorsId1]),
        CONSTRAINT [FK_CourseInstructor_Courses_CourseInstructorsId] FOREIGN KEY ([CourseInstructorsId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CourseInstructor_Instructors_CourseInstructorsId1] FOREIGN KEY ([CourseInstructorsId1]) REFERENCES [Instructors] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE TABLE [Enrollments] (
        [Id] uniqueidentifier NOT NULL,
        [CourseId] uniqueidentifier NULL,
        [StudentId] uniqueidentifier NULL,
        [DateOfCourseEnrollment] datetime2 NULL,
        CONSTRAINT [PK_Enrollments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Enrollments_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]),
        CONSTRAINT [FK_Enrollments_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE INDEX [IX_CourseInstructor_CourseInstructorsId1] ON [CourseInstructor] ([CourseInstructorsId1]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE INDEX [IX_Enrollments_CourseId] ON [Enrollments] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    CREATE INDEX [IX_Enrollments_StudentId] ON [Enrollments] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627124940_Initial_Migration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240627124940_Initial_Migration', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    DROP TABLE [CourseInstructor];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    DROP TABLE [Instructors];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    ALTER TABLE [Students] ADD [Index] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    ALTER TABLE [Students] ADD [ProfilePicture] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    ALTER TABLE [Courses] ADD [CourseImage] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    CREATE TABLE [CourseInstructors] (
        [Id] uniqueidentifier NOT NULL,
        [CourseId] uniqueidentifier NULL,
        [InstructorId] nvarchar(450) NULL,
        CONSTRAINT [PK_CourseInstructors] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CourseInstructors_AspNetUsers_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_CourseInstructors_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    CREATE INDEX [IX_CourseInstructors_CourseId] ON [CourseInstructors] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    CREATE INDEX [IX_CourseInstructors_InstructorId] ON [CourseInstructors] ([InstructorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704100439_FirstUpdate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240704100439_FirstUpdate', N'8.0.6');
END;
GO

COMMIT;
GO

