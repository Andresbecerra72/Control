﻿SELECT Flight, Adult, Child, Infant, Total FROM Passangers WHERE DATEDIFF(DAY, PublishOn, 2019-08-26)=0 ORDER BY Flight