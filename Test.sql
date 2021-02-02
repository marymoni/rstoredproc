
EXEC dbo.RunRScript @Script = N'
 cat(1 + 2)
 cat("Hello There!")
'
GO

EXEC dbo.RunRScript @Script = N'
 m1 = matrix(rnorm(10), 2, 5)
 m2 = matrix(rnorm(10), 2, 5)
 as.data.frame(m1 + m2)
'
GO

EXEC dbo.RunRScript @Script = N'
 x = rnorm(1e5, 1, 0.01)
 x = rnorm(1e5)
 y = 5 * x + rnorm(1e5, 0, 0.00001)
 r = lm(y ~ x)
 print(r)
'
GO

EXEC dbo.RunRScript @Script = N'
 library(RODBC);
 
 conn = odbcDriverConnect(''driver={SQL Server};server=..;database=master;trusted_connection=true'')

 sqlQuery(conn, "
  select top 3
   object_id,
   name
  from
  sys.tables
 ")
'
GO
