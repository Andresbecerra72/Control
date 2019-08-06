SELECT DISTINCT * FROM (SELECT * FROM (SELECT Flight, Adult, Infant, Child, Total FROM Passangers WHERE DATEDIFF(DAY, PublishOn, '31/julio/2019')=0  UNION ALL SELECT Flight, Adult, Infant, Child, Total FROM KiuPassangers WHERE PublishOn ='31/julio/2019') Tbls GROUP BY Flight, Adult, Infant, Child, Total HAVING COUNT(*) < 2) Diff 
          
		  