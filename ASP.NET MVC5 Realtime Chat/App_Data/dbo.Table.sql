﻿CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [username] NVARCHAR(50) NOT NULL, 
    [text] NCHAR(200) NOT NULL, 
    [created] DATETIME NOT NULL DEFAULT GETDATE()
)
