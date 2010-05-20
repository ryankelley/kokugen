USE [KokugenDataDev]
GO

/****** Object:  StoredProcedure [dbo].[GetCumalitiveFlowForProject]    Script Date: 04/29/2010 11:24:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCumalitiveFlowForProject]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCumalitiveFlowForProject]
GO

