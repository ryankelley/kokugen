USE [KokugenDataDev]
GO

/****** Object:  StoredProcedure [dbo].[GetCumalitiveFlowForProject]    Script Date: 04/23/2010 09:27:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCumalitiveFlowForProject]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCumalitiveFlowForProject]
GO

USE [KokugenDataDev]
GO

/****** Object:  StoredProcedure [dbo].[GetCumalitiveFlowForProject]    Script Date: 04/23/2010 09:27:45 ******/
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
JOIN BoardColumns col on col.Id = lanein.Entering_id
LEFT JOIN CardActivities laneout ON lanein.Leaving_id = laneout.Entering_id and laneout.ActivityId = 3
inner join Cards on Cards.Project_id = @projectId
--AND lanein.Card_Id = laneout.Card_Id
--AND laneout.EndTime <= dimtime.DateValue
WHERE --laneout.What IS NULL
 DimTime.DateValue BETWEEN '01-01-2010' AND GETDATE()+1
GROUP BY dimtime.DateValue,lanein.Entering_id, col.Name



GO


