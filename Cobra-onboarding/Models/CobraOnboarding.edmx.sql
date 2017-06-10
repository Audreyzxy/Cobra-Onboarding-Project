
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/02/2017 20:41:25
-- Generated from EDMX file: C:\Users\apple\Documents\Visual Studio 2015\Projects\slnCobraOnboarding\Cobra-onboarding\Models\CobraOnboarding.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Cobra-onboarding-db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderDetail_OrderHeader]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_OrderHeader];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderHeader_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderHeader] DROP CONSTRAINT [FK_OrderHeader_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[OrderDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetail];
GO
IF OBJECT_ID(N'[dbo].[OrderHeader]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderHeader];
GO
IF OBJECT_ID(N'[dbo].[Person]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Person];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NULL,
    [ProductId] int  NULL
);
GO

-- Creating table 'OrderHeaders'
CREATE TABLE [dbo].[OrderHeaders] (
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [OrderDate] datetime  NULL,
    [PersonId] int  NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [Address1] varchar(50)  NULL,
    [Address2] varchar(50)  NULL,
    [City] varchar(50)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [Price] decimal(18,2)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [OrderId] in table 'OrderHeaders'
ALTER TABLE [dbo].[OrderHeaders]
ADD CONSTRAINT [PK_OrderHeaders]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OrderId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetail_OrderHeader]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderHeaders]
        ([OrderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_OrderHeader'
CREATE INDEX [IX_FK_OrderDetail_OrderHeader]
ON [dbo].[OrderDetails]
    ([OrderId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetail_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_Product'
CREATE INDEX [IX_FK_OrderDetail_Product]
ON [dbo].[OrderDetails]
    ([ProductId]);
GO

-- Creating foreign key on [PersonId] in table 'OrderHeaders'
ALTER TABLE [dbo].[OrderHeaders]
ADD CONSTRAINT [FK_OrderHeader_Person]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderHeader_Person'
CREATE INDEX [IX_FK_OrderHeader_Person]
ON [dbo].[OrderHeaders]
    ([PersonId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------