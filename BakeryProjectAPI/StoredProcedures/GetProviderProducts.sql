USE [BakareyProjectAPI]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetProviderProducts]
    @ProviderID NVARCHAR(255)= Null,
	@UserID VARCHAR(255)= Null
AS
BEGIN
select 
tblUsers.Email,
tblUsers.CreatedAt,
tblUsers.EnglishUserName,
tblUsers.PhoneNumber,
tblUsers.ID as UserID,
tblUsers.Avatar,
tblProviders.ID as ProviderID,
tblProducts.EnglishProductName,
tblProducts.EnglishDescription,
tblProducts.ID as ProductID,
tblProducts.Price,
tblProducts.Quantity,
tblCategories.EnglishTitle,
tblCategories.ID as CategoryID
from tblUsers
join tblProviders on tblProviders.UserID = tblUsers.ID
join tblProductProviders on  tblProviders.ID = tblProductProviders.ProviderID
join tblProducts on tblProducts.ID = tblProductProviders.ProductID
join tblCategories on tblCategories.ID = tblProducts.CategoryID
where tblProducts.IsDeleted=0 
and (@ProviderID is null or tblProductProviders.ProviderID=@ProviderID) 
and (@UserID is null or tblProviders.UserID=@UserID)
END;