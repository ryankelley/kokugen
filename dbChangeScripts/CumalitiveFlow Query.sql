
SELECT dimtime.DateValue, lanein.Entering_id, col.Name, COUNT(DISTINCT lanein.Card_Id) AS CardCount
FROM dbo.DimTime dimtime
JOIN CardActivities lanein ON lanein.StartTime <= dimtime.DateValue and lanein.ActivityId = 3
JOIN BoardColumns col on col.Id = lanein.Entering_id
LEFT JOIN CardActivities laneout ON lanein.Leaving_id = laneout.Entering_id and laneout.ActivityId = 3
--AND lanein.Card_Id = laneout.Card_Id
--AND laneout.EndTime <= dimtime.DateValue
WHERE --laneout.What IS NULL
 DimTime.DateValue BETWEEN '01-01-2010' AND GETDATE()+1
GROUP BY dimtime.DateValue,lanein.Entering_id, col.Name


