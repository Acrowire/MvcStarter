USE [master]
GO

/****** Object:  Database [Acrowire]    Script Date: 12/23/2014 7:14:45 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Acrowire')
BEGIN
CREATE DATABASE [Acrowire]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Acrowire', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Acrowire.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Acrowire_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Acrowire_log.ldf' , SIZE = 4096KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

GO

ALTER DATABASE [Acrowire] ADD FILEGROUP [INDEX]
GO

ALTER DATABASE [Acrowire] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Acrowire].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Acrowire] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Acrowire] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Acrowire] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Acrowire] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Acrowire] SET ARITHABORT OFF 
GO

ALTER DATABASE [Acrowire] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Acrowire] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [Acrowire] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Acrowire] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Acrowire] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Acrowire] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Acrowire] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Acrowire] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Acrowire] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Acrowire] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Acrowire] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Acrowire] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Acrowire] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Acrowire] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Acrowire] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Acrowire] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Acrowire] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Acrowire] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Acrowire] SET RECOVERY FULL 
GO

ALTER DATABASE [Acrowire] SET  MULTI_USER 
GO

ALTER DATABASE [Acrowire] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Acrowire] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Acrowire] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Acrowire] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [Acrowire] SET  READ_WRITE 
GO


USE [Acrowire]
GO
/****** Object:  StoredProcedure [dbo].[sp_Reports_Delete]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Reports_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Reports_Delete]
(
@Id int
)
AS
BEGIN
    DELETE FROM [dbo].[Reports]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Reports_Insert]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Reports_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Reports_Insert]
(
@OrganizationId int,
@PublicId uniqueidentifier,
@Name varchar(50),
@Controller varchar(50),
@Action varchar(50)
)
AS
BEGIN
    INSERT INTO [dbo].[Reports]
    (
        [OrganizationId],
[PublicId],
[Name],
[Controller],
[Action]
    )
    VALUES
    (
        @OrganizationId,
@PublicId,
@Name,
@Controller,
@Action

    )
    Select @@identity;
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Reports_Select_All]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Reports_Select_All]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Reports_Select_All]
AS
BEGIN
    SELECT [Id],[OrganizationId],[PublicId],[Name],[Controller],[Action]
    FROM [dbo].[Reports]
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Reports_Select_One]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Reports_Select_One]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Reports_Select_One]
(
@Id int
)
AS
BEGIN
    SELECT [Id],[OrganizationId],[PublicId],[Name],[Controller],[Action]
    FROM [dbo].[Reports]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Reports_Update]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Reports_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Reports_Update]
(
@Id int,
@OrganizationId int,
@PublicId uniqueidentifier,
@Name varchar(50),
@Controller varchar(50),
@Action varchar(50)
)
AS
BEGIN
    UPDATE [dbo].[Reports]
    SET
        [OrganizationId]=@OrganizationId,
        [PublicId]=@PublicId,
        [Name]=@Name,
        [Controller]=@Controller,
        [Action]=@Action
WHERE        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Roles_Delete]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Roles_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Roles_Delete]
(
@Id int
)
AS
BEGIN
    DELETE FROM [dbo].[Roles]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Roles_Insert]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Roles_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Roles_Insert]
(
@PublicId uniqueidentifier,
@Name varchar(50),
@Active bit
)
AS
BEGIN
    INSERT INTO [dbo].[Roles]
    (
        [PublicId],
[Name],
[Active]
    )
    VALUES
    (
        @PublicId,
@Name,
@Active

    )
    Select @@identity;
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Roles_Select_All]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Roles_Select_All]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Roles_Select_All]
AS
BEGIN
    SELECT [Id],[PublicId],[Name],[Active]
    FROM [dbo].[Roles]
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Roles_Select_One]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Roles_Select_One]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Roles_Select_One]
(
@Id int
)
AS
BEGIN
    SELECT [Id],[PublicId],[Name],[Active]
    FROM [dbo].[Roles]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Roles_Update]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Roles_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Roles_Update]
(
@Id int,
@PublicId uniqueidentifier,
@Name varchar(50),
@Active bit
)
AS
BEGIN
    UPDATE [dbo].[Roles]
    SET
        [PublicId]=@PublicId,
        [Name]=@Name,
        [Active]=@Active
WHERE        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Select_UserReportss_By_ReportId]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Select_UserReportss_By_ReportId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Select_UserReportss_By_ReportId]
(
@ReportId int
)
AS
BEGIN
    SELECT [Id],[UserId],[ReportId]
    FROM [dbo].[UserReports]
    WHERE
        [ReportId]=@ReportId
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Select_UserReportss_By_UserId]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Select_UserReportss_By_UserId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Select_UserReportss_By_UserId]
(
@UserId int
)
AS
BEGIN
    SELECT [Id],[UserId],[ReportId]
    FROM [dbo].[UserReports]
    WHERE
        [UserId]=@UserId
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Select_UserRoless_By_RoleId]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Select_UserRoless_By_RoleId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Select_UserRoless_By_RoleId]
(
@RoleId int
)
AS
BEGIN
    SELECT [Id],[RoleId],[UserId],[Active]
    FROM [dbo].[UserRoles]
    WHERE
        [RoleId]=@RoleId
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Select_UserRoless_By_UserId]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Select_UserRoless_By_UserId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Select_UserRoless_By_UserId]
(
@UserId int
)
AS
BEGIN
    SELECT [Id],[RoleId],[UserId],[Active]
    FROM [dbo].[UserRoles]
    WHERE
        [UserId]=@UserId
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserReports_Delete]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserReports_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserReports_Delete]
(
@Id int
)
AS
BEGIN
    DELETE FROM [dbo].[UserReports]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserReports_Insert]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserReports_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserReports_Insert]
(
@UserId int,
@ReportId int
)
AS
BEGIN
    INSERT INTO [dbo].[UserReports]
    (
        [UserId],
[ReportId]
    )
    VALUES
    (
        @UserId,
@ReportId

    )
    Select @@identity;
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserReports_Select_All]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserReports_Select_All]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserReports_Select_All]
AS
BEGIN
    SELECT [Id],[UserId],[ReportId]
    FROM [dbo].[UserReports]
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserReports_Select_One]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserReports_Select_One]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserReports_Select_One]
(
@Id int
)
AS
BEGIN
    SELECT [Id],[UserId],[ReportId]
    FROM [dbo].[UserReports]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserReports_Update]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserReports_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserReports_Update]
(
@Id int,
@UserId int,
@ReportId int
)
AS
BEGIN
    UPDATE [dbo].[UserReports]
    SET
        [UserId]=@UserId,
        [ReportId]=@ReportId
WHERE        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserRoles_Delete]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserRoles_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserRoles_Delete]
(
@Id int
)
AS
BEGIN
    DELETE FROM [dbo].[UserRoles]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserRoles_Insert]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserRoles_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserRoles_Insert]
(
@RoleId int,
@UserId int,
@Active bit
)
AS
BEGIN
    INSERT INTO [dbo].[UserRoles]
    (
        [RoleId],
[UserId],
[Active]
    )
    VALUES
    (
        @RoleId,
@UserId,
@Active

    )
    Select @@identity;
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserRoles_Select_All]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserRoles_Select_All]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserRoles_Select_All]
AS
BEGIN
    SELECT [Id],[RoleId],[UserId],[Active]
    FROM [dbo].[UserRoles]
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserRoles_Select_One]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserRoles_Select_One]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserRoles_Select_One]
(
@Id int
)
AS
BEGIN
    SELECT [Id],[RoleId],[UserId],[Active]
    FROM [dbo].[UserRoles]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserRoles_Update]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserRoles_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_UserRoles_Update]
(
@Id int,
@RoleId int,
@UserId int,
@Active bit
)
AS
BEGIN
    UPDATE [dbo].[UserRoles]
    SET
        [RoleId]=@RoleId,
        [UserId]=@UserId,
        [Active]=@Active
WHERE        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Users_Delete]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Users_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Users_Delete]
(
@Id int
)
AS
BEGIN
    DELETE FROM [dbo].[Users]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Users_Insert]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Users_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Users_Insert]
(
@PublicId uniqueidentifier,
@UserName varchar(50),
@Email varchar(150),
@Active bit,
@PasswordHash varchar(256)
)
AS
BEGIN
    INSERT INTO [dbo].[Users]
    (
        [PublicId],
[UserName],
[Email],
[Active],
[PasswordHash]
    )
    VALUES
    (
        @PublicId,
@UserName,
@Email,
@Active,
@PasswordHash

    )
    Select @@identity;
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Users_Select_All]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Users_Select_All]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Users_Select_All]
AS
BEGIN
    SELECT [Id],[PublicId],[UserName],[Email],[Active],[PasswordHash]
    FROM [dbo].[Users]
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Users_Select_One]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Users_Select_One]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Users_Select_One]
(
@Id int
)
AS
BEGIN
    SELECT [Id],[PublicId],[UserName],[Email],[Active],[PasswordHash]
    FROM [dbo].[Users]
    WHERE
        [Id]=@Id
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Users_Update]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Users_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [dbo].[sp_Users_Update]
(
@Id int,
@PublicId uniqueidentifier,
@UserName varchar(50),
@Email varchar(150),
@Active bit,
@PasswordHash varchar(256)
)
AS
BEGIN
    UPDATE [dbo].[Users]
    SET
        [PublicId]=@PublicId,
        [UserName]=@UserName,
        [Email]=@Email,
        [Active]=@Active,
        [PasswordHash]=@PasswordHash
WHERE        [Id]=@Id
END

' 
END
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PublicId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/23/2014 7:15:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PublicId] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Active] [bit] NOT NULL,
	[PasswordHash] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoles]'))
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoles]'))
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoles_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoles]'))
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoles_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoles]'))
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO


