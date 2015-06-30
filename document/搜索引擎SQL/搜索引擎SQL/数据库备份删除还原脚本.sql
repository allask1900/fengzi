 
/**
declare @backup_fullpath  nvarchar(255),
        @backup_path      nvarchar(50),
        @str_date         nvarchar(50)

   set @backup_path = N'D:\digview\'
   set @str_date = convert(varchar(13), getdate(), 121)
   set @str_date = replace(@str_date, N'-', N'')
   set @str_date = replace(@str_date, N':', N'')
   set @str_date = replace(@str_date, N'.', N'')
   set @str_date = replace(@str_date, N' ', N'')

   -- backup AllAsk database
   set @backup_fullpath = @backup_path + N'SearchSystem_' +  @str_date + N'.bak'
   backup database SearchSystem to disk = @backup_fullpath 
---------------------------------------------------------------------------------

/**
���챸��
**/
backup database SearchSystem to disk = @backup_fullpath  with differential
**/

/**
������־�ļ�
**/
USE [master]
GO
ALTER DATABASE SearchSystem SET RECOVERY SIMPLE WITH NO_WAIT
GO
ALTER DATABASE SearchSystem SET RECOVERY SIMPLE   --��ģʽ
GO
USE SearchSystem 
GO
DBCC SHRINKFILE (N'SearchSystem_log' , 11, TRUNCATEONLY)
GO
USE [master]
GO
ALTER DATABASE SearchSystem SET RECOVERY FULL WITH NO_WAIT
GO
ALTER DATABASE SearchSystem SET RECOVERY FULL  --��ԭΪ��ȫģʽ
