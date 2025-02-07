USE [master]
GO
/****** Object:  Database [ProjektDB]    Script Date: 02.03.2021 20:34:21 ******/
CREATE DATABASE [ProjektDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjektDB', FILENAME = N'E:\SQL\SQL Server Installation\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProjektDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjektDB_log', FILENAME = N'E:\SQL\SQL Server Installation\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProjektDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ProjektDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjektDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjektDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjektDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjektDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjektDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjektDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjektDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjektDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjektDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjektDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjektDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjektDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjektDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjektDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjektDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjektDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjektDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjektDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjektDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjektDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjektDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjektDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjektDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjektDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProjektDB] SET  MULTI_USER 
GO
ALTER DATABASE [ProjektDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjektDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjektDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjektDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjektDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjektDB] SET QUERY_STORE = OFF
GO
USE [ProjektDB]
GO
/****** Object:  Table [dbo].[ExterneResource]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExterneResource](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[KostenG] [numeric](18, 0) NOT NULL,
	[Art] [nchar](50) NOT NULL,
 CONSTRAINT [PK_ExterneResource] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Aktivitaet]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aktivitaet](
	[Pkey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[StartDatumG] [date] NOT NULL,
	[EndDatumG] [date] NOT NULL,
	[StartDatum] [date] NULL,
	[EndDatum] [date] NULL,
	[BudgetExterneKostenG] [numeric](18, 0) NOT NULL,
	[BudgetPersonenKostenG] [numeric](18, 0) NOT NULL,
	[BudgetExterneKosten] [numeric](18, 0) NULL,
	[BudgetPersonenKosten] [numeric](18, 0) NULL,
	[Fortschritt] [int] NOT NULL,
	[FKey_VerantwortlichePersonID] [int] NOT NULL,
	[FKey_PhaseID] [int] NOT NULL,
	[Dokumente] [nvarchar](max) NULL,
 CONSTRAINT [PK_Aktivitaet] PRIMARY KEY CLUSTERED 
(
	[Pkey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Z_ExterneResource]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Z_ExterneResource](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[FKey_Aktiviteat] [int] NOT NULL,
	[FKey_ExterneResource] [int] NOT NULL,
	[StartDatum] [date] NOT NULL,
	[EndDatum] [date] NULL,
	[Kosten] [decimal](18, 0) NULL,
	[Abweichung] [decimal](18, 0) NULL,
	[Kommentar] [nvarchar](max) NULL,
 CONSTRAINT [PK_Z_ExterneResource] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[VKostenAbweichungExterne]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VKostenAbweichungExterne]
AS
SELECT        dbo.ExterneResource.Name, dbo.ExterneResource.KostenG, dbo.ExterneResource.Art, dbo.Z_ExterneResource.StartDatum, dbo.Z_ExterneResource.EndDatum, dbo.Z_ExterneResource.Kosten, 
                         dbo.Z_ExterneResource.Abweichung, dbo.Z_ExterneResource.Kommentar, dbo.Z_ExterneResource.PKey, dbo.Aktivitaet.Pkey AS Fkey_Aktivitaet
FROM            dbo.ExterneResource INNER JOIN
                         dbo.Z_ExterneResource ON dbo.ExterneResource.PKey = dbo.Z_ExterneResource.FKey_ExterneResource INNER JOIN
                         dbo.Aktivitaet ON dbo.Z_ExterneResource.FKey_Aktiviteat = dbo.Aktivitaet.Pkey
GO
/****** Object:  Table [dbo].[PerseonenResource]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PerseonenResource](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[KostenG] [numeric](18, 0) NOT NULL,
	[Funktion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PerseonenResource] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Z_PerseonenResource]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Z_PerseonenResource](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[FKey_Aktiviteat] [int] NOT NULL,
	[FKey_PerseonenResource] [int] NOT NULL,
	[StartDatum] [date] NOT NULL,
	[EndDatum] [date] NULL,
	[Kosten] [decimal](18, 0) NULL,
	[Abweichung] [decimal](18, 0) NULL,
	[Kommentar] [nvarchar](max) NULL,
 CONSTRAINT [PK_Z_PerseonenResource] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[VKostenAbweichungPersonen]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VKostenAbweichungPersonen]
AS
SELECT        dbo.Z_PerseonenResource.PKey, dbo.Z_PerseonenResource.FKey_Aktiviteat, dbo.Z_PerseonenResource.StartDatum, dbo.Z_PerseonenResource.EndDatum, dbo.Z_PerseonenResource.Kosten, 
                         dbo.Z_PerseonenResource.Abweichung, dbo.Z_PerseonenResource.Kommentar, dbo.PerseonenResource.Name, dbo.PerseonenResource.KostenG, dbo.PerseonenResource.Funktion
FROM            dbo.Z_PerseonenResource INNER JOIN
                         dbo.PerseonenResource ON dbo.Z_PerseonenResource.FKey_PerseonenResource = dbo.PerseonenResource.PKey
GO
/****** Object:  Table [dbo].[Meilenstein]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meilenstein](
	[Pkey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DatumG] [date] NOT NULL,
	[Datum] [date] NULL,
	[FKey_PhaseID] [int] NOT NULL,
 CONSTRAINT [PK_Meilenstein] PRIMARY KEY CLUSTERED 
(
	[Pkey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mitarbeiter]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mitarbeiter](
	[Pkey] [int] IDENTITY(1,1) NOT NULL,
	[Vorname] [nvarchar](50) NOT NULL,
	[Nachname] [nvarchar](50) NOT NULL,
	[Funktion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Mitarbeiter] PRIMARY KEY CLUSTERED 
(
	[Pkey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phase]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phase](
	[Pkey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Fortschritt] [int] NOT NULL,
	[StartDatumG] [date] NOT NULL,
	[EndDatumG] [date] NOT NULL,
	[StartDatum] [date] NULL,
	[EndDatum] [date] NULL,
	[FKey_PhaseTemplateID] [int] NOT NULL,
	[FKey_ProjektID] [int] NOT NULL,
 CONSTRAINT [PK_Phase] PRIMARY KEY CLUSTERED 
(
	[Pkey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhaseTemplate]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhaseTemplate](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FKey_VorgehensmodellID] [int] NOT NULL,
 CONSTRAINT [PK_PhaseTemplate] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projekt]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projekt](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](150) NOT NULL,
	[Beschreibung] [nvarchar](max) NOT NULL,
	[FreigabeDatum] [date] NULL,
	[StartDatumG] [date] NOT NULL,
	[EndDatumG] [date] NOT NULL,
	[StartDatum] [date] NULL,
	[EndDatum] [date] NULL,
	[FKey_ProjektleiterID] [int] NOT NULL,
	[KostenG] [numeric](18, 0) NOT NULL,
	[Kosten] [numeric](18, 0) NULL,
	[FKey_VorgehensmodellID] [int] NOT NULL,
	[Dokumente] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Fortschritt] [int] NULL,
 CONSTRAINT [PK_Projekt] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vorgehensmodell]    Script Date: 02.03.2021 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vorgehensmodell](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Beschreibung] [nvarchar](max) NULL,
 CONSTRAINT [PK_Vorgehensmodell] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Aktivitaet]  WITH CHECK ADD  CONSTRAINT [FK_Aktivitaet_Mitarbeiter] FOREIGN KEY([FKey_VerantwortlichePersonID])
REFERENCES [dbo].[Mitarbeiter] ([Pkey])
GO
ALTER TABLE [dbo].[Aktivitaet] CHECK CONSTRAINT [FK_Aktivitaet_Mitarbeiter]
GO
ALTER TABLE [dbo].[Aktivitaet]  WITH CHECK ADD  CONSTRAINT [FK_Aktivitaet_Phase] FOREIGN KEY([FKey_PhaseID])
REFERENCES [dbo].[Phase] ([Pkey])
GO
ALTER TABLE [dbo].[Aktivitaet] CHECK CONSTRAINT [FK_Aktivitaet_Phase]
GO
ALTER TABLE [dbo].[Phase]  WITH CHECK ADD  CONSTRAINT [FK_Phase_PhaseTemplate] FOREIGN KEY([FKey_PhaseTemplateID])
REFERENCES [dbo].[PhaseTemplate] ([PKey])
GO
ALTER TABLE [dbo].[Phase] CHECK CONSTRAINT [FK_Phase_PhaseTemplate]
GO
ALTER TABLE [dbo].[PhaseTemplate]  WITH CHECK ADD  CONSTRAINT [FK_PhaseTemplate_Vorgehensmodell] FOREIGN KEY([FKey_VorgehensmodellID])
REFERENCES [dbo].[Vorgehensmodell] ([PKey])
GO
ALTER TABLE [dbo].[PhaseTemplate] CHECK CONSTRAINT [FK_PhaseTemplate_Vorgehensmodell]
GO
ALTER TABLE [dbo].[Projekt]  WITH CHECK ADD  CONSTRAINT [FK_Projekt_Mitarbeiter] FOREIGN KEY([FKey_ProjektleiterID])
REFERENCES [dbo].[Mitarbeiter] ([Pkey])
GO
ALTER TABLE [dbo].[Projekt] CHECK CONSTRAINT [FK_Projekt_Mitarbeiter]
GO
ALTER TABLE [dbo].[Projekt]  WITH CHECK ADD  CONSTRAINT [FK_Projekt_Vorgehensmodell] FOREIGN KEY([FKey_VorgehensmodellID])
REFERENCES [dbo].[Vorgehensmodell] ([PKey])
GO
ALTER TABLE [dbo].[Projekt] CHECK CONSTRAINT [FK_Projekt_Vorgehensmodell]
GO
ALTER TABLE [dbo].[Z_ExterneResource]  WITH CHECK ADD  CONSTRAINT [FK_Z_ExterneResource_Aktivitaet] FOREIGN KEY([FKey_Aktiviteat])
REFERENCES [dbo].[Aktivitaet] ([Pkey])
GO
ALTER TABLE [dbo].[Z_ExterneResource] CHECK CONSTRAINT [FK_Z_ExterneResource_Aktivitaet]
GO
ALTER TABLE [dbo].[Z_ExterneResource]  WITH CHECK ADD  CONSTRAINT [FK_Z_ExterneResource_ExterneResource] FOREIGN KEY([FKey_ExterneResource])
REFERENCES [dbo].[ExterneResource] ([PKey])
GO
ALTER TABLE [dbo].[Z_ExterneResource] CHECK CONSTRAINT [FK_Z_ExterneResource_ExterneResource]
GO
ALTER TABLE [dbo].[Z_PerseonenResource]  WITH CHECK ADD  CONSTRAINT [FK_Z_PerseonenResource_Aktivitaet] FOREIGN KEY([FKey_Aktiviteat])
REFERENCES [dbo].[Aktivitaet] ([Pkey])
GO
ALTER TABLE [dbo].[Z_PerseonenResource] CHECK CONSTRAINT [FK_Z_PerseonenResource_Aktivitaet]
GO
ALTER TABLE [dbo].[Z_PerseonenResource]  WITH CHECK ADD  CONSTRAINT [FK_Z_PerseonenResource_PerseonenResource] FOREIGN KEY([FKey_PerseonenResource])
REFERENCES [dbo].[PerseonenResource] ([PKey])
GO
ALTER TABLE [dbo].[Z_PerseonenResource] CHECK CONSTRAINT [FK_Z_PerseonenResource_PerseonenResource]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ExterneResource"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Z_ExterneResource"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 452
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Aktivitaet"
            Begin Extent = 
               Top = 6
               Left = 490
               Bottom = 136
               Right = 739
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 3015
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VKostenAbweichungExterne'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VKostenAbweichungExterne'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Z_PerseonenResource"
            Begin Extent = 
               Top = 6
               Left = 325
               Bottom = 136
               Right = 547
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "PerseonenResource"
            Begin Extent = 
               Top = 6
               Left = 585
               Bottom = 136
               Right = 755
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VKostenAbweichungPersonen'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VKostenAbweichungPersonen'
GO
USE [master]
GO
ALTER DATABASE [ProjektDB] SET  READ_WRITE 
GO
