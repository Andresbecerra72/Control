declare @Data1 xml, @Data2 xml

    select @Data1 = 
    (
        select * 
        from (select * from Passangers except select * from KiuPassangers) as a
        for xml raw('Data')
    )

    select @Data2 = 
    (
        select * 
        from (select * from KiuPassangers except select * from Passangers) as a
        for xml raw('Data')
    )

    ;with CTE1 as (
        select
            T.C.value('../@ID', 'bigint') as ID,
            T.C.value('local-name(.)', 'nvarchar(128)') as Name,
            T.C.value('.', 'nvarchar(max)') as Value
        from @Data1.nodes('Data/@*') as T(C)    
    ), CTE2 as (
        select
            T.C.value('../@ID', 'bigint') as ID,
            T.C.value('local-name(.)', 'nvarchar(128)') as Name,
            T.C.value('.', 'nvarchar(max)') as Value
        from @Data2.nodes('Data/@*') as T(C)     
    )
    select
        isnull(C1.ID, C2.ID) as ID, isnull(C1.Name, C2.Name) as Name, C1.Value as Value1, C2.Value as Value2
    from CTE1 as C1
        full outer join CTE2 as C2 on C2.ID = C1.ID and C2.Name = C1.Name
    where
    not
    (
        C1.Value is null and C2.Value is null or
        C1.Value is not null and C2.Value is not null and C1.Value = C2.Value
    )