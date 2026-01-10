CREATE SEQUENCE [QCBatchId] AS int START WITH 100 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE TABLE [ControlMasters] (
    [Identifier] bigint NOT NULL IDENTITY,
    [ControlName] nvarchar(max) NOT NULL,
    [ControlType] nvarchar(max) NOT NULL,
    [LotNumber] nvarchar(max) NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [ManufacturerID] bigint NOT NULL,
    [DistributorID] bigint NOT NULL,
    [Notes] nvarchar(max) NULL,
    [Form] nvarchar(max) NOT NULL,
    [Level] bigint NOT NULL,
    [ReagentID] bigint NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [PreparationDateTime] datetime2 NULL,
    [PreparedBy] bigint NULL,
    [PreparedByUserName] nvarchar(max) NULL,
    [StorageTemperature] bigint NULL,
    [AliquoteCount] bigint NULL,
    [IsActive] bit NOT NULL,
<<<<<<< HEAD
<<<<<<< HEAD
    [IsQualitative] bit NOT NULL,
=======
>>>>>>> QC: issue fixes
=======
    [IsQualitative] bit NOT NULL,
>>>>>>> QC: demo feedback fixes
    CONSTRAINT [PK_ControlMasters] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [MediaInventories] (
    [Identifier] bigint NOT NULL IDENTITY,
    [BatchId] bigint NOT NULL,
    [ReceivedDateAndTime] datetime2 NOT NULL,
    [MediaId] bigint NOT NULL,
    [MediaLotNumber] nvarchar(max) NOT NULL,
    [ExpirationDateAndTime] datetime2 NOT NULL,
    [Colour] nvarchar(max) NOT NULL,
    [Crack] nvarchar(max) NOT NULL,
    [Contaminate] nvarchar(max) NOT NULL,
    [Leakage] nvarchar(max) NOT NULL,
    [Turbid] nvarchar(max) NOT NULL,
    [VolumeOrAgarThickness] nvarchar(max) NOT NULL,
    [Sterlity] nvarchar(max) NOT NULL,
    [Vividity] nvarchar(max) NOT NULL,
    [Remarks] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_MediaInventories] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [MicroQCMasters] (
    [Identifier] bigint NOT NULL IDENTITY,
    [CultureReagentTestId] bigint NOT NULL,
    [StrainId] bigint NOT NULL,
    [Frequency] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_MicroQCMasters] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [QCResultEntries] (
    [Identifier] bigint NOT NULL IDENTITY,
    [TestID] bigint NOT NULL,
    [ParameterName] nvarchar(max) NOT NULL,
    [ControlID] bigint NOT NULL,
    [AnalyzerID] bigint NOT NULL,
    [QcMonitoringMethod] nvarchar(max) NOT NULL,
    [TestDate] datetime2 NOT NULL,
    [ObservedValue] decimal(18,3) NOT NULL,
    [Comments] nvarchar(max) NULL,
    [Status] nvarchar(max) NOT NULL,
    [BatchId] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    [TestControlSamplesID] bigint NOT NULL,
    CONSTRAINT [PK_QCResultEntries] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [Reagents] (
    [Identifier] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [LotNo] nvarchar(max) NOT NULL,
    [SequenceNo] nvarchar(max) NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [LotSetupOrInstallationDate] datetime2 NOT NULL,
    [ManufacturerID] bigint NOT NULL,
    [DistributorID] bigint NOT NULL,
    [Status] bit NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    CONSTRAINT [PK_Reagents] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [Schedulers] (
    [Identifier] bigint NOT NULL IDENTITY,
    [AnalyzerId] bigint NOT NULL,
    [TestId] bigint NOT NULL,
    [ScheduleTitle] nvarchar(max) NOT NULL,
    [ScheduleStartDate] datetime2 NOT NULL,
    [ScheduleEndDate] datetime2 NOT NULL,
    [BatchId] bigint NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    CONSTRAINT [PK_Schedulers] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [StrainInventories] (
    [Identifier] bigint NOT NULL IDENTITY,
    [BatchId] bigint NOT NULL,
    [ReceivedDateAndTime] datetime2 NOT NULL,
    [StrainId] bigint NOT NULL,
    [StrainLotNumber] nvarchar(max) NOT NULL,
    [ExpirationDateAndTime] datetime2 NOT NULL,
    [Remarks] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_StrainInventories] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [StrainMasters] (
    [Identifier] bigint NOT NULL IDENTITY,
    [StrainCategoryId] bigint NOT NULL,
    [StrainName] nvarchar(max) NOT NULL,
    [ExpiryAlertPeriod] bigint NOT NULL,
    [LinkedMedias] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_StrainMasters] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [StrainMediaMappings] (
    [Identifier] bigint NOT NULL IDENTITY,
    [ReceivedDateAndTime] datetime2 NOT NULL,
    [StrainInventoryId] bigint NOT NULL,
    [MediaInventoryId] bigint NOT NULL,
    [Remarks] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_StrainMediaMappings] PRIMARY KEY ([Identifier])
);
GO


CREATE TABLE [TestControlSamples] (
    [Identifier] bigint NOT NULL IDENTITY,
    [TestID] bigint NULL,
    [SubTestID] bigint NULL,
    [QcMonitoringMethod] nvarchar(max) NOT NULL,
    [ControlLimit] nvarchar(max) NOT NULL,
    [ParameterName] nvarchar(max) NULL,
    [ControlID] bigint NOT NULL,
    [TargetRangeMin] decimal(18,3) NOT NULL,
    [TargetRangeMax] decimal(18,3) NOT NULL,
    [SampleTypeId] bigint NOT NULL,
    [UoMId] bigint NOT NULL,
    [UomText] nvarchar(max) NOT NULL,
    [Mean] decimal(18,3) NOT NULL,
    [SD] decimal(18,3) NOT NULL,
    [Cv] decimal(18,3) NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [ModifiedByUserName] nvarchar(max) NOT NULL,
    [LastModifiedDateTime] datetime2 NOT NULL,
    [StartTime] datetime2 NULL,
    [EndTime] datetime2 NULL,
    [IsSelected] bit NOT NULL,
    CONSTRAINT [PK_TestControlSamples] PRIMARY KEY ([Identifier]),
    CONSTRAINT [FK_TestControlSamples_ControlMasters_ControlID] FOREIGN KEY ([ControlID]) REFERENCES [ControlMasters] ([Identifier]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_TestControlSamples_ControlID] ON [TestControlSamples] ([ControlID]);
GO


