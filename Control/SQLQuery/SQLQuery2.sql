Select case when A.Adult = B.Adult  then '' else 'x' end as Adult, 
case when A.Child = B.Child then '' else 'x' end as Child,
case when A.Infant = B.Infant then '' else 'x' end as Infant,
case when A.Total = B.Total then '' else 'x' end as Total,
case when A.Flight = B.Flight then A.Flight else 'x' end as Flight,
case when A.PublishOn = B.PublishOn then A.PublishOn else 'x' end as Fecha
from KiuPassangers A 
Join Passangers B 
on (A.PublishOn = '23/julio/2019' AND a.Flight=b.Flight AND B.PublishOn = '23/julio/2019') ;
