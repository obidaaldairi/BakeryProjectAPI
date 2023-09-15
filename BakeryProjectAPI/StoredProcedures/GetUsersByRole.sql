USE [BakareyProjectAPI]
GO
/****** Object:  StoredProcedure [dbo].[GetUsersByRole]    Script Date: 9/14/2023 11:43:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetUsersByRole]
    @RoleName NVARCHAR(255) Null
AS
BEGIN
    SELECT tblUsers.*
    FROM tblUserRoles
    JOIN tblUsers ON tblUserRoles.UserId = tblUsers.ID
    JOIN tblRoles ON tblRoles.ID = tblUserRoles.RoleId
    WHERE tblRoles.EnglishRoleName = @RoleName;
END;
