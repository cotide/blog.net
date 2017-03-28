//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：BlogController.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/1/8 17:04:54 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Dtos;
using Cotide.Framework.ActionResult;
using Cotide.Framework.Collections;
using Cotide.Framework.Commands;
using Cotide.Framework.Exceptions;
using Cotide.Framework.UnitOfWork;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ArticleCommands;
using Cotide.Tasks.Commands.UserTourLogCommands;
using Cotide.Web.Controllers.Blog.ViewModel;
using Cotide.Web.Controllers.Blog.ViewModel.Article;
using Cotide.Web.Controllers.Blog.ViewModel.UserTourLog;
using Cotide.Web.Controllers.Controllers;
using Cotide.Web.Controllers.ViewModels;
using SharpArch.NHibernate.Web.Mvc;
using Cotide.Domain.Dtos.ArticleMessage;

namespace Cotide.Web.Controllers.Blog
{
    public class BlogController : BaseController
    {
        private readonly IArticleTypeQueryService _articleTypeQueryService;
        private readonly IArticleTagQueryService _articleTagQueryService;
        private readonly IArticleQueryService _articleQueryService;
        private readonly IArticleMessageQueryService _articleMessageQueryService;
        private readonly IUserTourLogQueryService _userTourLogQueryService;
        private readonly ILinkQueryService _linkQueryService;
        private readonly IProjectTypeQueryService _projectTypeQueryService;
        private readonly IProjectQueryService _projectQueryService;
        private readonly ILinkTypeQueryService _linkTypeQueryService;
        private readonly ICommandProcessor _processor;


        const int PageSize = 10;

        public BlogController(
            IArticleTypeQueryService articleTypeQueryService,
            IArticleQueryService articleQueryService,
            IArticleTagQueryService articleTagQueryService,
            ICommandProcessor processor,
            IUserTourLogQueryService userTourLogQueryService,
            IArticleMessageQueryService articleMessageQueryService,
            ILinkQueryService linkQueryService,
            IProjectTypeQueryService projectTypeQueryService,
            IProjectQueryService projectQueryService, ILinkTypeQueryService linkTypeQueryService)
        {
            _articleTypeQueryService = articleTypeQueryService;
            _articleQueryService = articleQueryService;
            _articleTagQueryService = articleTagQueryService;
            _processor = processor;
            _userTourLogQueryService = userTourLogQueryService;
            _articleMessageQueryService = articleMessageQueryService;
            _linkQueryService = linkQueryService;
            _projectTypeQueryService = projectTypeQueryService;
            _projectQueryService = projectQueryService;
            _linkTypeQueryService = linkTypeQueryService;
        }



        public ActionResult Index(string domain, int? year, int? mouth, int? articleTypeId, int? pageIndex)
        {

            return Articles(domain, year, mouth, null, null, pageIndex);
        }

        //[OutputCache(Duration = 5, VaryByParam = "pageIndex")]
        public ActionResult Articles(string domain, int? year, int? mouth, int? tag, int? articleTypeId, int? pageIndex)
        {
            var user = UserQueryService.GetUserByDomain(domain);
            var userId = user.ID;
            var result = _articleQueryService.FindAllPager(userId, articleTypeId, tag, year, mouth, true, pageIndex ?? 1, PageSize);
            var viewModel = result.Select(CreateArticleViewModel).ToPagedList(pageIndex ?? 1, PageSize, result.TotalCount);
            ViewData["domain"] = HistoryUser.Domain;


            #region 如果未登录或者当前用户是当前访问用户 不进行访问信息记录
            if (!IsLogin)
                return View("Index", viewModel);

            if (CurrentUser.UserId == HistoryUser.UserId)
                return View("Index", viewModel);
            #endregion

            // 更新访问信息
            try
            {
                const int maxCount = 50;
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<CreateUserTourLogCommand>(
                        new CreateUserTourLogCommand(CurrentUser.UserId, HistoryUser.UserId)
                            {
                                MaxTourUserCount = maxCount
                            });
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                // 记录日志
                LogServer.Error("更新阅读次数失败", ex.Message, HttpContext.Request.Path);
            }
            return View("Index", viewModel);
        }



        [HttpGet]
        public ViewResult Article(int? id)
        {
            var article = _articleQueryService.FindOne((int)id);
            var viewModel = new ArticleViewModel()
            {
                ArticleId = article.Id,
                ArticleTags = article.ArticleTags,
                ArticleTitle = article.Title,
                Content = article.Content,
                LastUpdate = article.LastUpdate,
                ReadCount = article.ReadCount,
                RealName = HistoryUser.RealName,
                FullArticleTitle = article.Title,
                UrlQuoteUrl = article.UrlQuoteUrl,
                CreateTime = article.CreateDate,
                CommentCount = article.CommentCount,
                Domain = article.Domain,
                UserHeadImg = article.UserHeadImg,
                ArticleTypeId = article.ArticleTypeId,
                ArticleTypeName = article.ArticleTypeName
            };
            // 更新阅读次数
            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<UpdateArticleCommand>(new UpdateArticleCommand(article.Id)
                                                                 {
                                                                     ReadCount = ++article.ReadCount,
                                                                     ArticleTagIds = article.ArticleTags.Select(x => x.Key).ToArray(),
                                                                     ArticleTypeId = article.ArticleTypeId,
                                                                     Content = article.Content,
                                                                     IsShow = article.IsShow,
                                                                     Title = article.Title,
                                                                     ContentDesc = article.ContentDesc,
                                                                     UrlQuoteUrl = article.UrlQuoteUrl
                                                                 });
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                // 记录日志
                LogServer.Error("更新阅读次数失败", ex.Message, HttpContext.Request.Path);
            }
            ViewBag.IsLogin = IsLogin;
            ViewData["domain"] = HistoryUser.Domain;
            return View("Article", viewModel);
        }


        public ActionResult ArticleType(string domain, int id, int? pageIndex)
        {
            return Articles(domain, null, null, null, id, pageIndex);
        }


        public ActionResult Tag(string domain, int id, int? pageIndex)
        {
            return Articles(domain, null, null, id, null, pageIndex);
        }


        public PartialViewResult HistoryUserHead()
        {
            return PartialView("_HistoryUserHead", HistoryUser);
        }


        public PartialViewResult FavoritesUserBlog()
        {
            const int maxCount = 50;
            var dto = _userTourLogQueryService.FindForLookMeTop(HistoryUser.UserId, maxCount);
            var viewModel = dto.Select(CreateUserTourLogViewModel).ToList();
            return PartialView("_FavoritesUserBlog", viewModel);
        }


        public PartialViewResult UserNav()
        {
            ViewBag.IsLogin = IsLogin;
            return PartialView("_UserNav", HistoryUser);
        }


        public PartialViewResult FavoritesUserLink()
        {
            const int topIndex = 10;
            var result = _linkQueryService.GetList(HistoryUser.UserId, true, topIndex);
            var viewModel = result.Select(CreateLinkViewModel).ToList();
            return PartialView("_FavoritesUserLink", viewModel);
        }

        public PartialViewResult _ArticleType()
        {
            var result = _articleTypeQueryService.FindAllForShow(HistoryUser.UserId);
            var viewModel = result.Select(x => new ArticleTypeViewModel()
            {
                TypeId = x.Id,
                TypeName = x.TypeName
            }).ToList();
            return PartialView("_ArticleType", viewModel);
        }


        public PartialViewResult ArticleDate()
        {
            var result = _articleQueryService.GetArticleCount(HistoryUser.UserId);
            var viewModel = result.Select(x => new ArticleDateViewModel()
            {
                Count = x.Count,
                Mouth = x.Month,
                Year = x.Year
            }).ToList();
            return PartialView("_ArticleDate", viewModel);
        }


        public PartialViewResult ArticleTag()
        {
            var keyNum = new int[] { 8, 11, 8, 8, 8, 14, 13, 8, 13, 11, 13, 11, 11, 14, 14, 13, 11, 11 };

            var viewModel =
                _articleTagQueryService.FindAll(HistoryUser.UserId).Select(x => new IndexArticleTagViewModel()
                {
                    Id = x.Id,
                    TagName = x.TagName
                }).ToList();
            // 生成不重复随机数
            var randomNum = keyNum;
            for (int i = 0; i < viewModel.Count(); i++)
            {
                var index = i;
                if (index >= randomNum.Length)
                {
                    index = 0;
                }
                viewModel[i].Random = randomNum[index];
            }
            return PartialView("_ArticleTag", viewModel);
        }

        public PartialViewResult HistoryArticle()
        {
            const int topIndex = 10;
            var result = _articleQueryService.GetTopList(HistoryUser.UserId, topIndex);
            var viewModel = result.Select(x => new HistoryArticleViewModel()
            {
                ArticleId = x.Id,
                ArticleTitle =x.Title,
                FullArticleTitle = x.Title
            }).ToList();
            return PartialView("_HistoryArticle", viewModel);
        }

        public PartialViewResult HomeArticleComment()
        {
            var dto = _articleMessageQueryService.FindTop(HistoryUser.UserId, 8);
            var viewModel = dto.Select(CreateHomeArticleCommentViewModel).ToList();
            return PartialView("_HomeArticleComment", viewModel);

        }

        /// <summary>
        /// 作品
        /// </summary>
        /// <returns></returns>
        public ActionResult ZuoPin(string domain, int? id, int? pageIndex)
        {
            var viewModel = new ZupPinViewModel
            {
                ProjectTypes = _projectTypeQueryService.FindAll(HistoryUser.UserId, true)
            };
            viewModel.Domain = HistoryUser.Domain;
            var result = _projectQueryService.FindAllPager(HistoryUser.UserId, true, id, pageIndex ?? 1, PageSize);
            ViewData["index"] = id;
            viewModel.Projects = result.Select(CreateProjectViewModel).ToPagedList(result.PageIndex, result.PageSize, result.TotalCount);
            return View("ZuoPin", viewModel);
        }

        [HttpGet]
        public ActionResult Project(int id)
        {
            var dto = _projectQueryService.FindOne(id);
            var viewModel = CreateProjectViewModel(dto);
            ViewData["domain"] = HistoryUser.Domain;
            return View(viewModel);
        }


        #region 连接管理

        public ActionResult Link(int? id)
        {

            var linkType = _linkTypeQueryService.FindAll(HistoryUser.UserId, true);
            ViewData["linkType"] = linkType;
            if (linkType == null || linkType.Count <= 0)
            {
                return View(new List<Cotide.Web.Controllers.Blog.ViewModel.LinkViewModel>());
            }

            if (id != null)
            {
                ViewData["linkTypeId"] = id;
                var link = _linkQueryService.FindAllPager(HistoryUser.UserId, id, true, 1, int.MaxValue);
                var viewModel = link.Select(CreateLinkViewModel).ToList();
                return View(viewModel);

            }
            else
            {
                id = linkType.FirstOrDefault().Id;
                ViewData["linkTypeId"] = id;
                var link = _linkQueryService.FindAllPager(HistoryUser.UserId, id, true, 1, int.MaxValue);
                var viewModel = link.Select(CreateLinkViewModel).ToList();
                return View(viewModel);
            }
        }

        #endregion

        #region 文章回复

        /// <summary>
        /// 删除文章留言
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteArticleComment(int msgId)
        {
            if (msgId > 0)
            {
                try
                {
                    _processor.Process<DeleteArticleMessageCommand>(new DeleteArticleMessageCommand(CurrentUser.UserId,
                        msgId));
                    return new JsonResultObjec<string>(true, "删除成功");
                }
                catch (BusinessException ex)
                {
                    return new JsonResultObjec<string>(false, ex.Message);
                }
                catch (Exception ex)
                {
                    base.LogServer.Error("删除文章回复出错", ex.Message);
                    return new JsonResultObjec<string>(false, "非法操作");
                }
            }
            return new JsonResultObjec<string>(false, "删除失败");
        }

        public PartialViewResult ArticleComment(int id, int? pageIndex)
        {
            const int pageSize = int.MaxValue;
            var viewModel = new ArticleCommentReplyViewModel();

            var doto = _articleMessageQueryService.FindAllByArticleIdPager(id, pageIndex ?? 1, pageSize);
            var msgData = doto.Where(x => x.BaseArticleMessageId == null).Select(x => new ArticleCommentViewModel()
            {
                Content = x.Content,
                CreateDate = x.CreateDate,
                Id = x.Id,
                TagerUserArticleMessageViewModel =
                CreateTagerUserArticleMessageViewModel(x.TagerUserArticleMessageDto),
                UserArticleMessageViewModel = CreateUserArticleMessageViewModel(x.UserArticleMessageDto),
                IsCanDelete = CurrentUser != null && (CurrentUser.UserId == HistoryUser.UserId ? true : false)
            }).ToList();

            foreach (var x in msgData)
            {
                var replyMsg = doto.Where(y => y.BaseArticleMessageId == x.Id).ToList();
                foreach (var y in replyMsg)
                {
                    x.ArticleReplyCommentViewModel.Add(new ArticleCommentViewModel()
                    {
                        Content = y.Content,
                        CreateDate = y.CreateDate,
                        Id = y.Id,
                        TagerUserArticleMessageViewModel = CreateTagerUserArticleMessageViewModel(y.TagerUserArticleMessageDto),
                        UserArticleMessageViewModel = CreateUserArticleMessageViewModel(y.UserArticleMessageDto),
                        IsCanDelete = CurrentUser != null && (CurrentUser.UserId == HistoryUser.UserId ? true : false)
                    });
                }
            }

            viewModel.ArticleCommentViewModels = msgData;
            //new PagedList<ArticleCommentViewModel>(msgData, doto.TotalCount, doto.PageIndex, doto.PageSize);

            //viewModel.ArticleCommentViewModels = doto.Select(x => new ArticleCommentViewModel()
            //{
            //    Content = x.Content,
            //    CreateDate = x.CreateDate,
            //    Id = x.Id,
            //    TagerUserArticleMessageViewModel = CreateTagerUserArticleMessageViewModel(x.TagerUserArticleMessageDto),
            //    UserArticleMessageViewModel = CreateUserArticleMessageViewModel(x.UserArticleMessageDto) ,
            //    IsCanDelete  = CurrentUser != null && (CurrentUser.UserId==HistoryUser.UserId?true:false)
            //}).ToPagedList(doto.PageIndex, doto.PageSize,doto.TotalCount);

            viewModel.ArticleId = id;
            viewModel.IsLogin = IsLogin;
            if (CurrentUser != null)
            {
                viewModel.CurrentUserId = CurrentUser.UserId;
            }
            return PartialView("_ArticleComment", viewModel);
        }




        public PartialViewResult UserReply(int id)
        {
            var viewModel = new SaveMessageViewModel()
            {
                ArticleId = id
            };
            return PartialView("_UserReply", viewModel);
        }

        /// <summary>
        /// 保存已登录用户留言
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns> 
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult SaveMessage(SaveMessageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var transaction = UnitOfWork.Begin())
                    {

                        _processor.Process<CreateArticleMessageCommand>(new CreateArticleMessageCommand(
                                                                            viewModel.ArticleId,
                                                                            CurrentUser.UserId,
                                                                            viewModel.Comment));
                        transaction.Commit();

                        return new JsonResultObjec<string>(true, "评论成功");
                    }
                }
                catch (Exception ex)
                {
                    // 记录日志
                    LogServer.Error("保存已登录用户评论失败", ex.Message, HttpContext.Request.Path);

                    if ((ex is PowerException) || (ex is BusinessException))
                    {
                        return new JsonResultObjec<string>(false, ex.Message);
                    }
                    else
                    {
                        return new JsonResultObjec<string>(false, "评论失败，请联系管理员!");
                    }
                }
            }
            return new JsonResultObjec<string>(false, "评论失败，请正确填写评论内容!");
        }

        public ActionResult CommentUserReply(int articleMessageId, int articleId)
        {
            var viewModel = new SaveMessageReplyViewModel()
            {
                ReplyMessageId = articleMessageId
            };
            viewModel.ArticleId = articleId;
            viewModel.FormGuId = Guid.NewGuid().ToString();
            return View("_CommentUserReply", viewModel);
        }


        /// <summary>
        /// 保存用户评论
        /// </summary>
        /// <returns></returns> 
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveMessageReply(SaveMessageReplyViewModel viewModel)
        {
            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<CreateArticleMessageReplyCommand>(new CreateArticleMessageReplyCommand(
                                                                             viewModel.ReplyMessageId,
                                                                             CurrentUser.UserId,
                                                                             viewModel.ReplyComment));
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                // 记录日志
                LogServer.Error("保存已登录用户评论失败", ex.Message, HttpContext.Request.Path);

                if ((ex is PowerException) || (ex is BusinessException))
                {
                    return new JsonResultObjec<string>(false, ex.Message);
                }
                else
                {
                    return new JsonResultObjec<string>(false, "评论失败，请联系管理员!");
                }
            }
            return new JsonResultObjec<string>(true, "评论成功");
        }


        [HttpGet]
        public ActionResult CommentAnonymousUserReply(int replyId, int articleId)
        {
            var viewModel = new SaveMessageReplyViewModel()
            {
                ReplyMessageId = replyId

            };
            viewModel.ArticleId = articleId;
            viewModel.FormGuId = Guid.NewGuid().ToString();
            return View("_CommentAnonymousUserReply", viewModel);
        }

        /// <summary>
        /// 保存匿名用户留言
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveAnonymousUserMessageReply(SaveMessageReplyViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (var transaction = UnitOfWork.Begin())
                    {

                        _processor.Process<CreateArticleMessageReplyCommand>(new CreateArticleMessageReplyCommand(
                                                                                 viewModel.ReplyMessageId,
                                                                                 viewModel.ReplyNickName,
                                                                                 viewModel.ReplyComment)
                                                                                 {
                                                                                     Email = viewModel.ReplyEmail,
                                                                                     WebSiteUrl =
                                                                                         viewModel.ReplyWebSiteUrl
                                                                                 });

                        transaction.Commit();
                    }
                    return new JsonResultObjec<string>(true, "评论成功");
                }
                catch (Exception ex)
                {
                    // 记录日志
                    LogServer.Error("保存已登录用户评论失败", ex.Message, HttpContext.Request.Path);

                    if ((ex is PowerException) || (ex is BusinessException))
                    {
                        return new JsonResultObjec<string>(false, ex.Message);
                    }
                    else
                    {
                        return new JsonResultObjec<string>(false, "评论失败，请联系管理员!");
                    }
                }

            }

            var errorStr = new StringBuilder();
            foreach (var item in ModelState)
            {
                foreach (var value in item.Value.Errors)
                {
                    errorStr.AppendLine(value.ErrorMessage);
                }
            }

            return new JsonResultObjec<string>(false, string.Format("{0}", errorStr));
        }


        public ActionResult AnonymousUserReply(int id)
        {
            var viewModel = new SaveMessageViewModel()
            {
                ArticleId = id
            };
            return View("_AnonymousUserReply", viewModel);
        }




        /// <summary>
        /// 保存匿名用户留言
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveAnonymousUserMessage(SaveMessageViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (var transaction = UnitOfWork.Begin())
                    {

                        _processor.Process<CreateArticleMessageCommand>(new CreateArticleMessageCommand(
                        viewModel.ArticleId,
                        viewModel.NickName,
                        viewModel.Comment)
                        {
                            WebSiteUrl = viewModel.WebSiteUrl
                        });

                        transaction.Commit();
                    }
                    return new JsonResultObjec<string>(true, "评论成功");
                }
                catch (Exception ex)
                {
                    // 记录日志
                    LogServer.Error("保存未登录用户评论失败", ex.Message, HttpContext.Request.Path);

                    if ((ex is PowerException) || (ex is BusinessException))
                    {
                        return new JsonResultObjec<string>(false, ex.Message);
                    }
                    else
                    {
                        return new JsonResultObjec<string>(false, "评论失败，请联系管理员!");
                    }
                }
            }
            var errorStr = new StringBuilder();
            foreach (var item in ModelState)
            {
                foreach (var value in item.Value.Errors)
                {
                    errorStr.AppendLine(value.ErrorMessage);
                }
            }

            return new JsonResultObjec<string>(false, string.Format("{0}", errorStr));
        }



        #endregion

        #region Helper

        private ProjectViewModel CreateProjectViewModel(ProjectDto dto)
        {
            return new ProjectViewModel()
            {
                Id = dto.Id,
                ProjectName = dto.ProjectName,
                ProjectImg = !string.IsNullOrEmpty(dto.ProjectImg)
                                    ? dto.ProjectImg
                                    : Url.Content(@"~/UploadFile/System/Files/NoFile.jpg"),
                SmallProjectImg = !string.IsNullOrEmpty(dto.SmallProjectImg)
                                    ? dto.SmallProjectImg
                                    : Url.Content(@"~/UploadFile/System/Files/MinNoFile.jpg"),
                StandardProjectImg = !string.IsNullOrEmpty(dto.StandardProjectImg)
                                    ? dto.StandardProjectImg
                                    : Url.Content(@"~/UploadFile/System/Files/NoFile.jpg"),
                WebSite = dto.WebSite,
                Introduction = dto.Introduction,
                Content = dto.Content,
                UserId = dto.UserId,
                ProductTypeId = dto.ProductTypeId,
                ProductTypeName = dto.ProductTypeName,
                IsShow = dto.IsShow,
                CreateDate = dto.CreateDate,
                LastDateTime = dto.LastDateTime
            };
        }


        private HomeArticleCommentViewModel CreateHomeArticleCommentViewModel(ArticleMessageDto dto)
        {
            return new HomeArticleCommentViewModel()
                       {
                           Content = Utils.CutStringBySuffix(dto.Content, 0, 10, "..."),
                           FullContent = dto.Content,
                           NickName = dto.UserArticleMessageDto.NickName,
                           ArticleId = dto.ArticleId
                       };
        }

        private LinkViewModel CreateLinkViewModel(LinkDto l)
        {
            return new LinkViewModel()
            {
                LinkTxt = l.LinkTxt,
                LinkUrl = !string.IsNullOrEmpty(l.LinkUrl) ? l.LinkUrl : "javascript:void"
            };
        }

        private TagerUserArticleMessageViewModel CreateTagerUserArticleMessageViewModel(TagerUserArticleMessageDto dto)
        {
            if (dto == null)
                return null;
            return new TagerUserArticleMessageViewModel()
                       {
                           NickName = dto.NickName,
                           UserDomain = dto.UserDomain,
                           UserId = dto.UserId,
                           UserImg = string.IsNullOrEmpty(dto.UserImg)
                              ? Url.Content(@"~/UploadFile/System/Files/DefaultHead.jpg")
                              : dto.UserImg,
                       };
        }

        private UserArticleMessageViewModel CreateUserArticleMessageViewModel(UserArticleMessageDto dto)
        {

            return new UserArticleMessageViewModel()
                       {
                           NickName = dto.NickName,
                           UserDomain = dto.UserDomain,
                           UserId = dto.UserId,
                           UserImg = string.IsNullOrEmpty(dto.UserImg)
                              ? Url.Content(@"~/UploadFile/System/Files/DefaultHead.jpg")
                              : dto.UserImg,
                       };
        }

        private ArticleViewModel CreateArticleViewModel(ArticleDto x)
        {
            return new ArticleViewModel()
            {
                ArticleId = x.Id,
                ArticleTags = x.ArticleTags,
                ArticleTitle =
                    Utils.CutStringBySuffix(x.Title, 0, 35,
                                            "..."),
                Content = HttpUtility.HtmlDecode("  " + x.ContentDesc),
                FullArticleTitle = x.Title,
                CreateTime = x.CreateDate,
                LastUpdate = x.LastUpdate,
                ReadCount = x.ReadCount,
                UrlQuoteUrl = x.UrlQuoteUrl,
                UserHeadImg = string.IsNullOrEmpty(x.UserHeadImg)
                              ? Url.Content(@"~/UploadFile/System/Files/DefaultHead.jpg")
                              : x.UserHeadImg,
                Domain = x.Domain,
                RealName = x.RealName,
                CommentCount = x.CommentCount,
                 ArticleTypeName =x.ArticleTypeName,
                  ArticleTypeId =x.ArticleTypeId
            };
        }

        private UserTourLogViewModel CreateUserTourLogViewModel(UserTourLogDto dto)
        {
            return new UserTourLogViewModel()
                       {
                           BlogName = dto.BlogName,
                           Id = dto.Id,
                           ImgHead = string.IsNullOrEmpty(dto.ImgHead)
                              ? Url.Content(@"~/UploadFile/System/Files/DefaultHead.jpg")
                              : dto.ImgHead,
                           SmallImgHead = string.IsNullOrEmpty(dto.SmallImgHead)
                           ? Url.Content(@"~/UploadFile/System/Files/DefaultHead.jpg")
                           : dto.SmallImgHead,
                           StandardImgHead = string.IsNullOrEmpty(dto.StandardImgHead)
                            ? Url.Content(@"~/UploadFile/System/Files/DefaultHead.jpg")
                            : dto.StandardImgHead,
                           UpdateDate = dto.UpdateDate,
                           UserId = dto.UserId,
                           UserName = dto.UserName,
                           Domain = dto.Domain
                       };
        }

        #endregion
    }
}
