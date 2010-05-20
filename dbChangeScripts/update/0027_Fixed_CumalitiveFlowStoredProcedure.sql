USE [KokugenDataDev]
GO

/****** Object:  StoredProcedure [dbo].[GetCumalitiveFlowForProject]    Script Date: 05/03/2010 12:46:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCumalitiveFlowForProject]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCumalitiveFlowForProject]
GO

USE [KokugenDataDev]
GO

/****** Object:  StoredProcedure [dbo].[GetCumalitiveFlowForProject]    Script Date: 05/03/2010 12:46:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetCumalitiveFlowForProject]

 @projectId uniqueidentifier
 
 AS

SELECT dimtime.DateValue, lanein.Entering_id, col.Name, COUNT(DISTINCT lanein.Card_Id) AS CardCount
FROM dbo.DimTime dimtime
JOIN CardActivities lanein ON lanein.StartTime <= dimtime.DateValue and lanein.ActivityId = 3
inner join Cards on Cards.Id = lanein.Card_id and Cards.Project_id = @projectId
JOIN BoardColumns col on col.Id = lanein.Entering_id

LEFT JOIN CardActivities laneout ON lanein.Entering_id = laneout.Leaving_id 
and laneout.ActivityId = 3
and lanein.Card_Id = laneout.Card_Id
AND (laneout.EndTime <= dimtime.DateValue)

WHERE lanein.EndTime is null AND
 DimTime.DateValue BETWEEN '01-01-2010' AND GETDATE()

GROUP BY dimtime.DateValue,lanein.Entering_id, col.Name
order by lanein.Entering_id, DateValue




GO


