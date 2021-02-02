EXEC sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXEC sp_configure 'clr enabled', 1;
GO

RECONFIGURE;
GO

ALTER DATABASE master SET TRUSTWORTHY ON
GO

CREATE ASSEMBLY RDotNetNativeLibrary
FROM 'c:\bin\RDotNet.NativeLibrary.dll' WITH PERMISSION_SET = UNSAFE;
GO

CREATE ASSEMBLY RDotNet
FROM 'c:\bin\RDotNet.dll' WITH PERMISSION_SET = UNSAFE;
GO

CREATE ASSEMBLY RStoredProc
FROM 'c:\bin\RStoredProc.dll' WITH PERMISSION_SET = UNSAFE;
GO

CREATE PROCEDURE dbo.RunRScript (@Script nvarchar(MAX))
AS EXTERNAL NAME
RStoredProc.[RStoredProc.StoredProcedures].RunRScript;
GO
