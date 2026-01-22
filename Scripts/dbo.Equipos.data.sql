USE [ObligatorioGus25]
GO

/****** Objeto: Table [dbo].[Equipos] Fecha del script: 17/11/2025 20:48:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Equipos];


GO
CREATE TABLE [dbo].[Equipos] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Nombre] NVARCHAR (450) NOT NULL
);


