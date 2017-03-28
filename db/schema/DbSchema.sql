
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEC5C9E843FE2728C]') AND parent_object_id = OBJECT_ID('Blog_Articles'))
alter table Blog_Articles  drop constraint FKEC5C9E843FE2728C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEC5C9E844D59DE92]') AND parent_object_id = OBJECT_ID('Blog_Articles'))
alter table Blog_Articles  drop constraint FKEC5C9E844D59DE92


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB4586BE4AB53CDD4]') AND parent_object_id = OBJECT_ID('ArticleTags_Article'))
alter table ArticleTags_Article  drop constraint FKB4586BE4AB53CDD4


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB4586BE4A8B479FA]') AND parent_object_id = OBJECT_ID('ArticleTags_Article'))
alter table ArticleTags_Article  drop constraint FKB4586BE4A8B479FA


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1F52BA553FE2728C]') AND parent_object_id = OBJECT_ID('Blog_ArticleMessages'))
alter table Blog_ArticleMessages  drop constraint FK1F52BA553FE2728C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1F52BA55ED969811]') AND parent_object_id = OBJECT_ID('Blog_ArticleMessages'))
alter table Blog_ArticleMessages  drop constraint FK1F52BA55ED969811


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1F52BA55A8B479FA]') AND parent_object_id = OBJECT_ID('Blog_ArticleMessages'))
alter table Blog_ArticleMessages  drop constraint FK1F52BA55A8B479FA


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1F52BA5587C719FC]') AND parent_object_id = OBJECT_ID('Blog_ArticleMessages'))
alter table Blog_ArticleMessages  drop constraint FK1F52BA5587C719FC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF498195E3FE2728C]') AND parent_object_id = OBJECT_ID('Blog_ArticleTags'))
alter table Blog_ArticleTags  drop constraint FKF498195E3FE2728C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB223B6FC3FE2728C]') AND parent_object_id = OBJECT_ID('Blog_ArticleTypes'))
alter table Blog_ArticleTypes  drop constraint FKB223B6FC3FE2728C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8BA0441091325CC]') AND parent_object_id = OBJECT_ID('Blog_Links'))
alter table Blog_Links  drop constraint FK8BA0441091325CC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8BA044103FE2728C]') AND parent_object_id = OBJECT_ID('Blog_Links'))
alter table Blog_Links  drop constraint FK8BA044103FE2728C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK52ABEF303FE2728C]') AND parent_object_id = OBJECT_ID('Blog_LinkTypes'))
alter table Blog_LinkTypes  drop constraint FK52ABEF303FE2728C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF404B7DF3FE2728C]') AND parent_object_id = OBJECT_ID('Blog_Projects'))
alter table Blog_Projects  drop constraint FKF404B7DF3FE2728C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF404B7DF991984CA]') AND parent_object_id = OBJECT_ID('Blog_Projects'))
alter table Blog_Projects  drop constraint FKF404B7DF991984CA


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3A2505673FE2728C]') AND parent_object_id = OBJECT_ID('Blog_ProjectTypes'))
alter table Blog_ProjectTypes  drop constraint FK3A2505673FE2728C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB8CD86FB3E86CF7E]') AND parent_object_id = OBJECT_ID('Blog_UserTourLogs'))
alter table Blog_UserTourLogs  drop constraint FKB8CD86FB3E86CF7E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB8CD86FB3FE2728C]') AND parent_object_id = OBJECT_ID('Blog_UserTourLogs'))
alter table Blog_UserTourLogs  drop constraint FKB8CD86FB3FE2728C


    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_Users') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_Users

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_Ads') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_Ads

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_Articles') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_Articles

    if exists (select * from dbo.sysobjects where id = object_id(N'ArticleTags_Article') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ArticleTags_Article

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_ArticleMessages') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_ArticleMessages

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_ArticleTags') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_ArticleTags

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_ArticleTypes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_ArticleTypes

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_Links') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_Links

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_LinkTypes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_LinkTypes

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_Projects') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_Projects

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_ProjectTypes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_ProjectTypes

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_UserTourLogs') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_UserTourLogs

    if exists (select * from dbo.sysobjects where id = object_id(N'Blog_hibernate_unique_key') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Blog_hibernate_unique_key

    create table Blog_Users (
        Id INT not null,
       UserName NVARCHAR(100) null,
       UserNo NVARCHAR(100) null,
       ImgHead NVARCHAR(200) null,
       UserRole INT null,
       SmallImgHead NVARCHAR(200) null,
       StandardImgHead NVARCHAR(200) null,
       Domain NVARCHAR(15) null,
       Email NVARCHAR(30) null,
       Card NVARCHAR(20) null,
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
       LastDateTime DATETIME null,
       LoginDate DATETIME null,
       LastLoginDate DATETIME null,
       LoginIp NVARCHAR(15) null,
       LastLoginIp NVARCHAR(15) null,
       DesPassword NVARCHAR(255) null,
       primary key (Id)
    )

    create table Blog_Ads (
        AdId INT not null,
       AdName NVARCHAR(50) null,
       AdDesc NVARCHAR(100) null,
       AdImg NVARCHAR(200) null,
       StandardAdImg NVARCHAR(200) null,
       SmallAdImg NVARCHAR(200) null,
       AdType INT null,
       IsShow BIT null,
       Sort INT null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       primary key (AdId)
    )

    create table Blog_Articles (
        ArticleId INT not null,
       Title NVARCHAR(200) null,
       Content NVARCHAR(MAX) null,
       ContentDesc NVARCHAR(800) null,
       UrlQuoteUrl NVARCHAR(200) null,
       IsShow BIT null,
       ReadCount INT null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       UserId INT null,
       ArticleTypeId INT null,
       primary key (ArticleId)
    )

    create table ArticleTags_Article (
        ArticleId INT not null,
       ArticleTagId INT not null
    )

    create table Blog_ArticleMessages (
        ArticleMessageId INT not null,
       Content NVARCHAR(MAX) null,
       CreateDate DATETIME null,
       IsShow BIT null,
       NickName NVARCHAR(100) null,
       Email NVARCHAR(50) null,
       WebSiteUrl NVARCHAR(200) null,
       LastDateTime DATETIME null,
       UserId INT null,
       BaseArticleMessageId INT null,
       ArticleId INT null,
       primary key (ArticleMessageId)
    )

    create table Blog_ArticleTags (
        ArticleTagId INT not null,
       TagName NVARCHAR(50) null,
       IsShow BIT null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       UserId INT null,
       primary key (ArticleTagId)
    )

    create table Blog_ArticleTypes (
        ArticleTypeId INT not null,
       TypeName NVARCHAR(30) null,
       IsShow BIT null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       UserId INT null,
       primary key (ArticleTypeId)
    )

    create table Blog_Links (
        LinkId INT not null,
       LinkTxt NVARCHAR(200) null,
       LinkUrl NVARCHAR(200) null,
       Img NVARCHAR(200) null,
       Description NVARCHAR(500) null,
       IsShow BIT null,
       Sort INT null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       LinkTypeId INT null,
       UserId INT null,
       primary key (LinkId)
    )

    create table Blog_LinkTypes (
        LinkTypeId INT not null,
       TypeName NVARCHAR(30) null,
       IsShow BIT null,
       Sort INT null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       UserId INT null,
       primary key (LinkTypeId)
    )

    create table Blog_Projects (
        ProjectId INT not null,
       ProjectName NVARCHAR(200) null,
       ProjectImg NVARCHAR(200) null,
       SmallProjectImg NVARCHAR(200) null,
       StandardProjectImg NVARCHAR(200) null,
       WebSite NVARCHAR(200) null,
       Content NVARCHAR(MAX) null,
       Introduction NVARCHAR(MAX) null,
       IsShow BIT null,
       Sort INT null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       UserId INT null,
       ProjectTypeId INT null,
       primary key (ProjectId)
    )

    create table Blog_ProjectTypes (
        ProjectTypeId INT not null,
       TypeName NVARCHAR(30) null,
       IsShow BIT null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       UserId INT null,
       primary key (ProjectTypeId)
    )

    create table Blog_UserTourLogs (
        UserTourLogId INT not null,
       CreateDate DATETIME null,
       LastDateTime DATETIME null,
       TourUserId INT null,
       UserId INT null,
       primary key (UserTourLogId)
    )

    alter table Blog_Articles 
        add constraint FKEC5C9E843FE2728C 
        foreign key (UserId) 
        references Blog_Users

    alter table Blog_Articles 
        add constraint FKEC5C9E844D59DE92 
        foreign key (ArticleTypeId) 
        references Blog_ArticleTypes

    alter table ArticleTags_Article 
        add constraint FKB4586BE4AB53CDD4 
        foreign key (ArticleTagId) 
        references Blog_ArticleTags

    alter table ArticleTags_Article 
        add constraint FKB4586BE4A8B479FA 
        foreign key (ArticleId) 
        references Blog_Articles

    alter table Blog_ArticleMessages 
        add constraint FK1F52BA553FE2728C 
        foreign key (UserId) 
        references Blog_Users

    alter table Blog_ArticleMessages 
        add constraint FK1F52BA55ED969811 
        foreign key (BaseArticleMessageId) 
        references Blog_ArticleMessages

    alter table Blog_ArticleMessages 
        add constraint FK1F52BA55A8B479FA 
        foreign key (ArticleId) 
        references Blog_Articles

    alter table Blog_ArticleMessages 
        add constraint FK1F52BA5587C719FC 
        foreign key (ArticleMessageId) 
        references Blog_ArticleMessages

    alter table Blog_ArticleTags 
        add constraint FKF498195E3FE2728C 
        foreign key (UserId) 
        references Blog_Users

    alter table Blog_ArticleTypes 
        add constraint FKB223B6FC3FE2728C 
        foreign key (UserId) 
        references Blog_Users

    alter table Blog_Links 
        add constraint FK8BA0441091325CC 
        foreign key (LinkTypeId) 
        references Blog_LinkTypes

    alter table Blog_Links 
        add constraint FK8BA044103FE2728C 
        foreign key (UserId) 
        references Blog_Users

    alter table Blog_LinkTypes 
        add constraint FK52ABEF303FE2728C 
        foreign key (UserId) 
        references Blog_Users

    alter table Blog_Projects 
        add constraint FKF404B7DF3FE2728C 
        foreign key (UserId) 
        references Blog_Users

    alter table Blog_Projects 
        add constraint FKF404B7DF991984CA 
        foreign key (ProjectTypeId) 
        references Blog_ProjectTypes

    alter table Blog_ProjectTypes 
        add constraint FK3A2505673FE2728C 
        foreign key (UserId) 
        references Blog_Users

    alter table Blog_UserTourLogs 
        add constraint FKB8CD86FB3E86CF7E 
        foreign key (TourUserId) 
        references Blog_Users

    alter table Blog_UserTourLogs 
        add constraint FKB8CD86FB3FE2728C 
        foreign key (UserId) 
        references Blog_Users

    create table Blog_hibernate_unique_key (
         next_hi INT 
    )

    insert into Blog_hibernate_unique_key values ( 1 )
