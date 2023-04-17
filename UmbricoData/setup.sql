USE [master]
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'UmbricoDb')
BEGIN

    CREATE DATABASE [UmbricoDb];

END;
GO

USE UmbricoDb;