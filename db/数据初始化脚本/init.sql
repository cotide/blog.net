 
use [Cotide.Shizong_DB]    
/******** 清除所有表 ********/
   if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Users') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Users

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Activities') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Activities

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ActivityOrders') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ActivityOrders

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ActivityTypes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ActivityTypes

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Admins') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Admins

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Admin_AdminRole') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Admin_AdminRole

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_AdminMenus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_AdminMenus

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_AdminRole_AdminMenu') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_AdminRole_AdminMenu

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_AdminPowers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_AdminPowers

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_AdminRole_AdminPower') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_AdminRole_AdminPower

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_AdminRoles') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_AdminRoles

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Articles') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Articles

    if exists (select * from dbo.sysobjects where id = object_id(N'ArticleTags_Article') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ArticleTags_Article

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ArticleMessages') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ArticleMessages

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ArticleTags') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ArticleTags

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ArticleTypes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ArticleTypes

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Brands') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Brands

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Products') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Products

    if exists (select * from dbo.sysobjects where id = object_id(N'ProductTags_Product') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ProductTags_Product

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ProductAttrs') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ProductAttrs

    if exists (select * from dbo.sysobjects where id = object_id(N'ProductAttrs_ProductTypes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ProductAttrs_ProductTypes

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ProductAttrValues') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ProductAttrValues

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ProductImgs') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ProductImgs

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ProductOrders') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ProductOrders

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ProductOrderItems') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ProductOrderItems

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ProductProerties') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ProductProerties

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ProductTags') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ProductTags

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ProductTypes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ProductTypes

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Suppliers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Suppliers

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_UserAddresses') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_UserAddresses

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_hibernate_unique_key') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_hibernate_unique_key

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_Ads') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_Ads

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ShizongArticles') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ShizongArticles

    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_ShizongArticleTypes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_ShizongArticleTypes
    
    if exists (select * from dbo.sysobjects where id = object_id(N'Cotide_UserTourLogs') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cotide_UserTourLogs
go
/******** 创建表 ********/
create table Cotide_Users (
        UserId INT not null,
       UserName NVARCHAR(100) null,
       UserNo NVARCHAR(100) null,
       ImgHead NVARCHAR(200) null,
       UserRole NVARCHAR(255) null,
       SmallImgHead NVARCHAR(200) null,
       StandardImgHead NVARCHAR(200) null,
       Domain NVARCHAR(15) null,
       Email NVARCHAR(30) null,
       [Card] NVARCHAR(20) null,
       UserSex INT null,
       UserState INT null,
       Phone NVARCHAR(15) null,
       Paw NVARCHAR(255) null,
       RealName NVARCHAR(25) null,
       EnRealName NVARCHAR(25) null,
       EmailValidate BIT null,
       QQ NVARCHAR(30) null,
       WeiBoUrl NVARCHAR(255) null,
       BlogName NVARCHAR(50) null,
       BlogDesc NVARCHAR(255) null,
       CreateDate DATETIME null,
       LastUpdate DATETIME null,
       LoginDate DATETIME null,
       LastLoginDate DATETIME null,
       LoginIp NVARCHAR(15) null,
       LastLoginIp NVARCHAR(15) null,
       DesPassword NVARCHAR(255) null,
       primary key (UserId)
    )



    create table Cotide_Activities (
       ActivityId INT not null,
       Title NVARCHAR(150) null,
       Content NVARCHAR(MAX) null,
       Organizers NVARCHAR(20) null,
       GatherPlace NVARCHAR(30) null,
       Destination NVARCHAR(30) null,
       ActivityState INT null,
       ActivityImg NVARCHAR(255) null,
       StandardActivityImg NVARCHAR(255) null,
       [ContentDesc] NVARCHAR(max) null,
       ActivityMoney DECIMAL(19,2) null,
       IsShow BIT null,
       BeginTime DATETIME null,
       EndTime DATETIME null,
       CreateDate DATETIME null,
       LastUpdate DATETIME null,
       ActivityTypeId INT null,
       UserId INT null,
       primary key (ActivityId)
    )

    create table Cotide_ActivityOrders (
        ActivityOrderId INT not null,
       ActivityOrderMoneyState INT null,
       ActivityOrderState INT null,
       MsgTip NVARCHAR(255) null,
       CreateDate DATETIME null,
       LastUpdate DATETIME null,
       ActivityId INT null,
       UserId INT null,
       primary key (ActivityOrderId)
    )

    create table Cotide_ActivityTypes (
       ActivityTypeId INT not null,
       TypeName NVARCHAR(50) null,
       IsShow BIT null,
       Sort INT null,
       UserId INT null,
       primary key (ActivityTypeId)
    )

    create table Cotide_Admins (
        AdminId INT not null,
       UserName NVARCHAR(30) null,
       RealName NVARCHAR(10) null,
       Email NVARCHAR(30) null,
       Mobile NVARCHAR(15) null,
       Password NVARCHAR(255) null,
       CreateTime DATETIME null,
       IsLocked BIT null,
       IsSuperAdmin BIT null,
       primary key (AdminId)
    )

    create table Cotide_Admin_AdminRole (
        AdminId INT not null,
       AdminRoleId INT not null
    )

    create table Cotide_AdminMenus (
        AdminMenuId INT not null,
       MenuName NVARCHAR(20) null,
       LinkUrl NVARCHAR(100) null,
       SortOrder INT null,
       BaseAdminMenuId INT null,
       primary key (AdminMenuId)
    )

    create table Cotide_AdminRole_AdminMenu (
        AdminMenuId INT not null,
       AdminRoleId INT not null
    )

    create table Cotide_AdminPowers (
        AdminPowerId INT not null,
       PowerName NVARCHAR(30) null,
       ActionName NVARCHAR(30) null,
       ControllerName NVARCHAR(30) null,
       primary key (AdminPowerId)
    )

    create table Cotide_AdminRole_AdminPower (
        AdminPowerId INT not null,
       AdminRoleId INT not null
    )

    create table Cotide_AdminRoles (
        AdminRoleId INT not null,
       RoleName NVARCHAR(30) null,
       CreateTime DATETIME null,
       primary key (AdminRoleId)
    )

    create table Cotide_Articles (
       ArticleId INT not null,
       Title NVARCHAR(200) null,
       Content NVARCHAR(MAX) null,
       UrlQuoteUrl NVARCHAR(200) null,
       IsShow BIT null,
       ReadCount INT null,
       CreateDate DATETIME null,
       LastUpdate DATETIME null,
       UserId INT null,
       ArticleTypeId INT null,
       primary key (ArticleId)
    )

    create table ArticleTags_Article (
        ArticleId INT not null,
       ArticleTagId INT not null
    )

        create table Cotide_ArticleMessages (
        ArticleMessageId INT not null,
       Content NVARCHAR(MAX) null,
       CreateDate DATETIME null,
       IsShow BIT null,
       NickName NVARCHAR(100) null,
       Email NVARCHAR(50) null,
       WebSiteUrl NVARCHAR(200) null,
       UserId INT null,
       BaseArticleMessageId INT null,
       RootArticleMessageId INT null,
       ArticleId INT null,
       primary key (ArticleMessageId)
    ) 

    create table Cotide_ArticleTags (
        ArticleTagId INT not null,
       TagName NVARCHAR(50) null,
       IsShow BIT null,
       UserId INT null,
       primary key (ArticleTagId)
    )

    create table Cotide_ArticleTypes (
        ArticleTypeId INT not null,
       TypeName NVARCHAR(30) null,
       IsShow BIT null,
       UserId INT null,
       primary key (ArticleTypeId)
    )

    create table Cotide_Brands (
        BrandId INT not null,
       BrandName NVARCHAR(255) null,
       BrandLogo NVARCHAR(255) null,
       BrandWebSite NVARCHAR(255) null,
       Description NVARCHAR(255) null,
       Sort INT null,
       ProductId INT null,
       primary key (BrandId)
    )

    create table Cotide_Products (
        ProductId INT not null,
       ProductName NVARCHAR(200) null,
       ProductNo NVARCHAR(50) null,
       Description NVARCHAR(MAX) null,
       SimpleDescription NVARCHAR(MAX) null,
       Remark NVARCHAR(255) null,
       Title NVARCHAR(100) null,
       MetaKeywords NVARCHAR(150) null,
       Price DECIMAL(19,2) null,
       CostPrice DECIMAL(19,2) null,
       DiscountCount DOUBLE PRECISION null,
       ShopTotal INT null,
       MetaDescription NVARCHAR(255) null,
       ProductTypeId INT null,
       primary key (ProductId)
    )

    create table ProductTags_Product (
        ProductId INT not null,
       ProductTagsId INT not null
    )

    create table Cotide_ProductAttrs (
        ProductAttrId INT not null,
       [Key] NVARCHAR(255) null,
       DisplayName NVARCHAR(255) null,
       FieldName NVARCHAR(255) null,
       AttrType NVARCHAR(255) null,
       Sort INT null,
       IsUse BIT null,
       primary key (ProductAttrId)
    )

    create table ProductAttrs_ProductTypes (
        ProductAttrId INT not null,
       ProductTypeId INT not null
    )

    create table Cotide_ProductAttrValues (
        ProductAttrValueId INT not null,
       [Key] NVARCHAR(255) null,
       Value NVARCHAR(255) null,
       IsUse BIT null,
       Sort INT null,
       ProductAttrId INT null,
       ProductProertyId INT null,
       primary key (ProductAttrValueId)
    )

    create table Cotide_ProductImgs (
        ProductImgId INT not null,
       Name NVARCHAR(25) null,
       ImgTip NVARCHAR(25) null,
       FileName NVARCHAR(30) null,
       AccessLocation NVARCHAR(200) null,
       Width INT null,
       Height INT null,
       ProductId INT null,
       primary key (ProductImgId)
    )

    create table Cotide_ProductOrders (
        ProductOrderId INT not null,
       OrderNo NVARCHAR(50) null,
       OrderPayStatus INT null,
       Phone NVARCHAR(15) null,
       Address NVARCHAR(500) null,
       Ip NVARCHAR(15) null,
       SumCount INT null,
       SumPrice DECIMAL(19,2) null,
       CreateTime DATETIME null,
       LastUpdateTime DATETIME null,
       UserId INT null,
       primary key (ProductOrderId)
    )

    create table Cotide_ProductOrderItems (
        ProductOrderItemId INT not null,
       ProductName NVARCHAR(200) null,
       OrderItemNo NVARCHAR(50) null,
       OrderStatus INT null,
       Remark NVARCHAR(500) null,
       ProductId INT null,
       Price DECIMAL(19,2) null,
       [Count] INT null,
       SumPrice DECIMAL(19,2) null,
       CreateTime DATETIME null,
       LastUpdateTime DATETIME null,
       ProductOrderId INT null,
       primary key (ProductOrderItemId)
    )

    create table Cotide_ProductProerties (
        ProductProertyId INT not null,
       ProductAttrId INT null,
       ProductId INT null,
       primary key (ProductProertyId)
    )

    create table Cotide_ProductTags (
        ProductTagId INT not null,
       TagName NVARCHAR(10) null,
       Sort INT null,
       IsShow BIT null,
       primary key (ProductTagId)
    )

    create table Cotide_ProductTypes (
        ProductTypeId INT not null,
       TypeName NVARCHAR(25) null,
       UrlRouting NVARCHAR(255) null,
       Logo NVARCHAR(255) null,
       Level INT null,
       IsShow BIT null,
       Sort INT null,
       Title NVARCHAR(255) null,
       MetaDescription NVARCHAR(255) null,
       MetaKeywords NVARCHAR(255) null,
       BaseProductTypeId INT null,
       primary key (ProductTypeId)
    )

    create table Cotide_Suppliers (
        SuppliersId INT not null,
       Name NVARCHAR(255) null,
       [User] NVARCHAR(255) null,
       Tel NVARCHAR(255) null,
       Phone NVARCHAR(255) null,
       Address NVARCHAR(255) null,
       Remark NVARCHAR(255) null,
       primary key (SuppliersId)
    )

    create table Cotide_UserAddresses (
        UserAddressId INT not null,
       Country NVARCHAR(15) null,
       Provice NVARCHAR(15) null,
       City NVARCHAR(15) null,
       ZipCode NVARCHAR(10) null,
       Address NVARCHAR(500) null,
       IsDefault BIT null,
       UserId INT null,
       primary key (UserAddressId)
    )

  create table Cotide_Ads (
       AdId INT not null,
       AdName NVARCHAR(50) null,
       AdDesc NVARCHAR(100) null,
       AdImg NVARCHAR(200) null,
       StandardAdImg NVARCHAR(200) null,
       SmallAdImg NVARCHAR(200) null,
       AdType INT null,
       IsShow BIT null,
       Sort INT null
       primary key (AdId)
    )
    
     create table Cotide_ShizongArticles (
        ShizongArticleId INT not null,
       Content NVARCHAR(MAX) null,
       IsShow BIT null,
       ShizongArticleImg NVARCHAR(255) null,
       StandardShizongArticleImg NVARCHAR(255) null,
       Title NVARCHAR(255) null,
       ReadCount INT null,
       ContentDesc NVARCHAR(max) null,
       CreateDate DATETIME null,
       LastUpdate DATETIME null,
       UserId INT null,
       ShizongArticleTypeId INT null,
       primary key (ShizongArticleId)
    )

    create table Cotide_ShizongArticleTypes (
        ShizongArticleTypeId INT not null,
       TypeName NVARCHAR(10) null,
       IsShow BIT null,
       Sort INT null,
       primary key (ShizongArticleTypeId)
    )
    
     create table Cotide_UserTourLogs (
        UserTourLogId INT not null, 
       CreateDate DATETIME null,
       UpdateDate DATETIME null,
       TourUserId INT null,
       UserId INT null,
       primary key (UserTourLogId)
    )
go
-- 高低位表
CREATE TABLE Cotide_hibernate_unique_key(
	[next_hi] [int] NULL,
	[table_name] [nvarchar](255) NULL
) ON [PRIMARY]
go

exec Cotide_HibernateUniqueKeyInit
go
-- 初始化数据
INSERT [dbo].[Cotide_Users] ([UserId], [UserName], [UserNo], [ImgHead], [SmallImgHead], [StandardImgHead],[UserRole], [Domain], [Email], [Card], [UserSex], [UserState], [Phone], [Paw], [RealName], [EnRealName], [EmailValidate], [QQ], [WeiBoUrl], [BlogName], [BlogDesc], [CreateDate], [LastUpdate], [LoginDate], [LastLoginDate], [LoginIp], [LastLoginIp], [DesPassword]) VALUES (1005, N'xcli', NULL, N'/UploadFile/User/1005/Images/beb2d264-f85f-45f6-a84f-92ccdce48332.jpg', N'/UploadFile/User/1005/Images/s_beb2d264-f85f-45f6-a84f-92ccdce48332.jpg', N'/UploadFile/User/1005/Images/b_beb2d264-f85f-45f6-a84f-92ccdce48332.jpg',1, N'xcli', NULL, NULL, 0, 0, N'15013159108', N'e10adc3949ba59abbe56e057f20f883e', N'xcli', N'Toby', NULL, NULL, N'http://weibo.com/silencenet', N'xcli''Blog', NULL, CAST(0x0000A1480176F7CC AS DateTime), CAST(0x0000A1660005ED94 AS DateTime), NULL, NULL, N'', N'', N'uQFZTBF2M1FxMS0wGDkv4w==')
go
INSERT [dbo].[Cotide_ActivityTypes] ([ActivityTypeId], [TypeName], [IsShow], [Sort], [UserId]) VALUES (1001, N'户外旅游', 1, 1, 1005)
INSERT [dbo].[Cotide_ActivityTypes] ([ActivityTypeId], [TypeName], [IsShow], [Sort], [UserId]) VALUES (1002, N'健身活动', 1, 1, 1005)
INSERT [dbo].[Cotide_ActivityTypes] ([ActivityTypeId], [TypeName], [IsShow], [Sort], [UserId]) VALUES (1003, N'摄影爱好', 1, 0, 1005)
INSERT [dbo].[Cotide_ActivityTypes] ([ActivityTypeId], [TypeName], [IsShow], [Sort], [UserId]) VALUES (1004, N'DIY活动', 1, 0, 1005)
go
INSERT [dbo].[Cotide_Ads] ([AdId], [AdName], [AdDesc], [AdImg], [StandardAdImg], [SmallAdImg], [AdType], [IsShow], [Sort]) VALUES (1001, N'徒步南昆山', N'徒步南昆山', N'/UploadFile/User/System/Images/a502fd70-3b73-4061-9782-24abd68966f3.jpg', N'/UploadFile/User/System/Images/b_a502fd70-3b73-4061-9782-24abd68966f3.jpg', N'/UploadFile/User/System/Images/s_a502fd70-3b73-4061-9782-24abd68966f3.jpg', 0, 1, 1)
INSERT [dbo].[Cotide_Ads] ([AdId], [AdName], [AdDesc], [AdImg], [StandardAdImg], [SmallAdImg], [AdType], [IsShow], [Sort]) VALUES (1002, N'户外厦门旅游', N'户外厦门旅游', N'/UploadFile/User/System/Images/9bede272-e541-4088-bda8-c41941dc1c51.jpg', N'/UploadFile/User/System/Images/b_9bede272-e541-4088-bda8-c41941dc1c51.jpg', N'/UploadFile/User/System/Images/s_9bede272-e541-4088-bda8-c41941dc1c51.jpg', 0, 1, 22)
