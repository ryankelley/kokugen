
IF NOT EXISTS (SELECT Feature FROM aspnet_SchemaVersions sv WHERE sv.Feature = 'common' )
	INSERT INTO [dbo].[aspnet_SchemaVersions] VALUES ('common', 1, 1)

IF NOT EXISTS (SELECT Feature FROM aspnet_SchemaVersions sv WHERE sv.Feature = 'health monitoring' )
	INSERT INTO [dbo].[aspnet_SchemaVersions] VALUES ('health monitoring', 1, 1)
	
	IF NOT EXISTS (SELECT Feature FROM aspnet_SchemaVersions sv WHERE sv.Feature = 'membership' )
	INSERT INTO [dbo].[aspnet_SchemaVersions] VALUES ('membership', 1, 1)
	
	IF NOT EXISTS (SELECT Feature FROM aspnet_SchemaVersions sv WHERE sv.Feature = 'personalization' )
	INSERT INTO [dbo].[aspnet_SchemaVersions] VALUES ('personalization', 1, 1)
	
	IF NOT EXISTS (SELECT Feature FROM aspnet_SchemaVersions sv WHERE sv.Feature = 'profile' )
	INSERT INTO [dbo].[aspnet_SchemaVersions] VALUES ('profile', 1, 1)
	
	IF NOT EXISTS (SELECT Feature FROM aspnet_SchemaVersions sv WHERE sv.Feature = 'role manager' )
	INSERT INTO [dbo].[aspnet_SchemaVersions] VALUES ('role manager', 1, 1)

