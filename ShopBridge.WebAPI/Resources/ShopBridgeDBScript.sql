CREATE Database [ShopBridgeDB]
GO
USE [ShopBridgeDB]
GO
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

CREATE TABLE [dbo].[mstProduct] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [UnitsInStock] smallint NOT NULL,
    [Discontinued] bit NOT NULL,
    [UnitId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_mstProduct] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[mstUnit] (
    [Id] smallint NOT NULL IDENTITY,
    [UnitName] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK_mstUnit] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210531175622_initial', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[mstProduct]') AND [c].[name] = N'UnitId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[mstProduct] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [dbo].[mstProduct] ALTER COLUMN [UnitId] smallint NOT NULL;
GO

CREATE INDEX [IX_mstProduct_UnitId] ON [dbo].[mstProduct] ([UnitId]);
GO

ALTER TABLE [dbo].[mstProduct] ADD CONSTRAINT [FK_mstProduct_mstUnit_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[mstUnit] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210531185914_addProductUnitFK', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[mstUnit]') AND [c].[name] = N'UpdatedBy');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[mstUnit] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [dbo].[mstUnit] ALTER COLUMN [UpdatedBy] int NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[mstProduct]') AND [c].[name] = N'UpdatedBy');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[mstProduct] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [dbo].[mstProduct] ALTER COLUMN [UpdatedBy] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210602173039_make_updatedby_nullable', N'5.0.6');
GO

COMMIT;
GO

