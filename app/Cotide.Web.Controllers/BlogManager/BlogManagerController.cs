using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cotide.Domain.Contracts.Commands;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Services;
using Cotide.Domain.Contracts.Task;
using Cotide.Domain.Dtos;
using Cotide.Domain.Enum;
using Cotide.Framework.Attr;
using Cotide.Framework.Commands;
using Cotide.Framework.Exceptions;
using Cotide.Framework.File;
using Cotide.Framework.File.Config;
using Cotide.Framework.Setting;
using Cotide.Framework.UnitOfWork;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.AdCommands;
using Cotide.Tasks.Commands.ArticleCommands;
using Cotide.Tasks.Commands.ArticleTagCommands;
using Cotide.Tasks.Commands.ArticleTypeCommands;
using Cotide.Tasks.Commands.LinkCommands;
using Cotide.Tasks.Commands.LinkTypeCommands;
using Cotide.Tasks.Commands.ProjectCommands;
using Cotide.Tasks.Commands.ProjectTypeCommands;
using Cotide.Tasks.Commands.UserCommands;
using Cotide.Web.Controllers.BlogManager.ViewModel;
using Cotide.Web.Controllers.BlogManager.ViewModel.LinkType;
using Cotide.Web.Controllers.Controllers;
using Cotide.Web.Controllers.Utility.Attr;
using SharpArch.NHibernate.Web.Mvc;
using Cotide.Framework.Collections;
using Cotide.Web.Controllers.BlogManager.ViewModel.Ad;
using Cotide.Framework.Enumerable;
using Cotide.Web.Controllers.BlogManager.ViewModel.Link;

namespace Cotide.Web.Controllers.BlogManager
{
    [Authorize]
    //[HandleErrorWithELMAH]
    [UserPower(UserLoginRole.Admin)]
    public class BlogManagerController : BaseController
    {
        private const int PageSize = 10;
        private readonly ILinkQueryService _linkQueryService;
        private readonly IArticleTypeQueryService _articleTypeQueryService;
        private readonly IProjectQueryService _projectQueryService;
        private readonly IProjectTypeQueryService _projectTypeQueryService;
        private readonly IArticleTagQueryService _articleTagQueryService;
        private readonly IArticleQueryService _articleQueryService;
        private readonly IUserFileTask _userFileTask;
        private readonly IUserQueryService _userQueryService;
        private readonly IAdQueryService _adQueryService;
        private readonly ICommandProcessor _processor;
        private readonly ILinkTypeQueryService _linkTypeQueryService;

        #region IOC注入
        public BlogManagerController(
            IArticleTypeQueryService articleTypeQueryService,
            IArticleTagQueryService articleTagQueryService,
            IArticleQueryService articleQueryService,
            IUserFileTask userFileTask,
            IUserQueryService userQueryService,
            ICommandProcessor processor,
            IAdQueryService adQueryService,
            ILinkQueryService linkQueryService,
            IProjectQueryService projectQueryService,
            IProjectTypeQueryService projectTypeQueryService,
            ILinkTypeQueryService linkTypeQueryService)
        {
            _articleTypeQueryService = articleTypeQueryService;
            _articleTagQueryService = articleTagQueryService;
            _articleQueryService = articleQueryService;
            _userFileTask = userFileTask;
            _userQueryService = userQueryService;
            _processor = processor;
            _adQueryService = adQueryService;
            _linkQueryService = linkQueryService;
            _projectQueryService = projectQueryService;
            _projectTypeQueryService = projectTypeQueryService;
            _linkTypeQueryService = linkTypeQueryService;
        }
        #endregion


        public PartialViewResult UserMenu()
        {
            return PartialView("_UserMenu", CurrentUser);
        }

        #region 文章管理
        public ActionResult Index()
        {

            var articleTypes = _articleTypeQueryService.FindAll(CurrentUser.UserId);
            if (articleTypes != null)
            {
                var articleTypeList = articleTypes
                 .Select(x => new SelectListItem()
                 {
                     Text = x.TypeName,
                     Value = x.Id.ToString()
                 }).ToList();
                articleTypeList.Insert(0, new SelectListItem()
                {
                    Selected = true,
                    Text = @"--请选择所属文章分类--",
                    Value = ""
                });
                ViewData["ArticleTypes"] = articleTypeList;
            }

            var articleTags = _articleTagQueryService.FindAll(CurrentUser.UserId);
            if (articleTags != null)
            {
                var articleTagsList = articleTags
                .Select(x => new SelectListItem()
                {
                    Text = x.TagName,
                    Value = x.Id.ToString()
                }).ToList();


                ViewData["ArticleTags"] = articleTagsList;
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(AddArticleModel articleModel)
        {
            if (ModelState.IsValid)
            {
                int[] articleTags = null;
                if (Request.Form.GetValues("ArticleTag") != null)
                {
                    articleTags = Request.Form.GetValues("ArticleTag").Select(int.Parse).ToArray();
                }
                try
                {
                    using (var transaction = UnitOfWork.Begin())
                    {

                        var command = new CreateArticleCommand(articleModel.Title, CurrentUser.UserId, articleModel.Content, articleTags)
                                          {
                                              ArticleTypeId = articleModel.ArticleTypeId,
                                              UrlQuoteUrl = articleModel.UrlQuoteUrl,
                                              IsShow = articleModel.IsShow
                                          };
                        _processor.Process<CreateArticleCommand, int>(command);
                        transaction.Commit();
                    }
                    AlterForRightGoTo("保存文章成功!", Url.Action("Index"));
                    return Redirect(Url.Action("Index"));
                }
                catch (Exception ex)
                {
                    // 记录日志
                    LogServer.Error("保存文章失败", ex.Message, HttpContext.Request.Path);
                    if ((ex is PowerException) || (ex is BusinessException))
                    {
                        AlterForError(ex.Message);
                    }
                    else
                    {
                        AlterForError("保存文章失败，请联系管理员!");
                    }
                    return Redirect(Url.Action("Index"));
                }
            }
            return Redirect(Url.Action("Index"));
        }

        public ActionResult ArticleManger(int? pageIndex)
        {
            var viewModel = _articleQueryService.FindAllPager(CurrentUser.UserId, null, null, null, null, null, pageIndex ?? 1, 20);
            return View("ArticleManger", viewModel);
        }


        public ActionResult UpdateArticle(int articleId)
        {
            var articleDto = _articleQueryService.FindOne(articleId);
            // 进行数据验证
            if (articleDto == null)
            {
                return Redirect(Url.Action("Error500", "Error", new { @errorMsg = "无效的文章" }));
            }
            // 进行权限验证
            if (articleDto.UserId != CurrentUser.UserId)
            {
                return Redirect(Url.Action("Error500", "Error", new { @errorMsg = "非法操作" }));
            }

            var articleTypes = _articleTypeQueryService.FindAll(CurrentUser.UserId);
            if (articleTypes != null)
            {
                var articleTypeList = articleTypes
                 .Select(x => new SelectListItem()
                 {
                     Text = x.TypeName,
                     Value = x.Id.ToString()
                 }).ToList();
                articleTypeList.Insert(0, new SelectListItem()
                {
                    Selected = true,
                    Text = @"--请选择所属文章分类--",
                    Value = ""
                });
                ViewData["ArticleTypes"] = articleTypeList;
            }

            ViewData["ArticleTags"] = _articleTagQueryService.FindAll(CurrentUser.UserId)
                .Select(x => new SelectListItem()
                {
                    Text = x.TagName,
                    Value = x.Id.ToString()
                }).ToList();

            var viewModel = CreateUpdateArticleModel(articleDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateArticle(UpdateArticleModel viewModel)
        {
            int[] articleTags = null;
            if (Request.Form.GetValues("ArticleTag") != null)
            {
                articleTags = Request.Form.GetValues("ArticleTag").Select(x => int.Parse(x)).ToArray();
            }
            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<UpdateArticleCommand>(
                        new UpdateArticleCommand(viewModel.Id, CurrentUser.UserId)
                            {
                                ArticleTagIds = articleTags,
                                ArticleTypeId = viewModel.ArticleTypeId,
                                Content = viewModel.Content,
                                IsShow = viewModel.IsShow,
                                Title = viewModel.Title,
                                ContentDesc = viewModel.ContentDesc,
                                UrlQuoteUrl = viewModel.UrlQuoteUrl
                            });
                    transaction.Commit();
                }
                AlterForRightGoTo("更新文章成功!",
                    Url.Action("UpdateArticle", "BlogManager",
                    new
                    {
                        articleId = viewModel.Id
                    }));

                return Redirect(Url.Action("UpdateArticle", new {@articleId = viewModel.Id}));
            }
            catch (Exception ex)
            {
                LogServer.Error("更新文章失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                   AlterForError(ex.Message);
                }
                else
                {
                   AlterForError("更新文章失败，请联系管理员!");
                }
                return Redirect(Url.Action("UpdateArticle", new { @articleId = viewModel.Id }));
            }
        }


        [HttpGet]
        public ActionResult DeleteArticle(int articleId)
        {
            try
            { 
                _processor.Process<DeleteArticleCommand>(new DeleteArticleCommand(
                articleId,
                CurrentUser.UserId)); 
                AlterForRightGoTo("删除文章成功!", Url.Action("ArticleManger", "BlogManager"));
                return Redirect(Url.Action("ArticleManger")); 
            }
            catch (Exception ex)
            {
                LogServer.Error("删除文章失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                    AlterForError(ex.Message);
                }
                else
                {
                    AlterForError("删除文章失败，请联系管理员!");
                }
                return Redirect(Url.Action("ArticleManger"));
            }
        }


        #endregion

        #region 文章分类管理

        public ActionResult AddArticlesType()
        {
            return View("Index");
        }

        public ActionResult ArticleType(int? pageIndex)
        {
            var viewModel = _articleTypeQueryService.FindAllPager(CurrentUser.UserId, pageIndex ?? 1, PageSize);
            return View("ArticleType", viewModel);
        }

        [HttpPost]
        public ActionResult ArticleType(string articleTypeName, int? pageIndex)
        {
            if (string.IsNullOrEmpty(articleTypeName) || articleTypeName.Length > 30)
            {
               AlterForError("文章分类名称字符长度不正确");
                return ArticleType(null);
            }

            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<CreateArticleTypeCommand>(
                        new CreateArticleTypeCommand(articleTypeName, CurrentUser.UserId));
                    transaction.Commit();


                }
                AlterForRight("保存文章分类成功!",2000);
                return Redirect(Url.Action("ArticleType"));
            }
            catch (Exception ex)
            {
                LogServer.Error("保存文章分类失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                   AlterForError(ex.Message);
                }
                else
                {
                   AlterForError("保存文章分类失败，请联系管理员!");
                }
                return Redirect(Url.Action("ArticleType"));
            }
        }


        public ActionResult UpdateArticleType(int articleTypeId)
        {
            var model = _articleTypeQueryService.FindOne(articleTypeId);

            return View(CreateArticleTypeModel(model));
        }

        [HttpPost]
        public ActionResult UpdateArticleType(ArticleTypeModel articleTypeModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var transaction = UnitOfWork.Begin())
                    {
                        _processor.Process<UpdateArticleTypeCommand>(new UpdateArticleTypeCommand(articleTypeModel.Id, CurrentUser.UserId)
                                                                         {
                                                                             IsShow = articleTypeModel.IsShow,
                                                                             TypeName = articleTypeModel.TypeName
                                                                         });
                        AlterForRightGoTo("更新文章分类成功!",
                                                            Url.Action("UpdateArticleType",
                                                                       new { @articleTypeId = articleTypeModel.Id }));
                        transaction.Commit();
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    LogServer.Error("更新文章分类失败", ex.Message, HttpContext.Request.Path);
                    if ((ex is PowerException) || (ex is BusinessException))
                    {
                       AlterForError(ex.Message);
                    }
                    else
                    {
                       AlterForError("更新文章分类失败，请联系管理员!");
                    }
                    return View();
                }
            }
            AlterForError("非法操作!");
            return View();
        }


        [HttpGet]
        public ActionResult DeleteArticleType(int articleTypeId)
        {
            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<DeleteArticleTypeCommand>(new DeleteArticleTypeCommand(
                        articleTypeId,
                        CurrentUser.UserId));
                    transaction.Commit();
                    AlterForRight("删除文章分类成功!",2000);
                    return Redirect(Url.Action("ArticleType"));
                }
            }
            catch (Exception ex)
            {
                LogServer.Error("删除文章分类失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                   AlterForError(ex.Message);
                }
                else
                {
                   AlterForError("删除文章分类失败，请联系管理员!");
                }
                return Redirect(Url.Action("ArticleType"));
            }
        }


        public ActionResult SetArticleTypeIsShow(int articleTypeId)
        {
            Guard.IsNotZeroOrNegative(articleTypeId, "文章分类ID格式不正确");
            var articleType = _articleTypeQueryService.FindOne(articleTypeId);
            Guard.IsNotNull(articleType, "articleType");
            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<UpdateArticleTypeCommand>(
                        new UpdateArticleTypeCommand(articleTypeId, CurrentUser.UserId)
                            {
                                IsShow = !articleType.IsShow
                            });
                    AlterForRight("修改文章状态成功!", 2000);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                LogServer.Error("修改文章分类状态失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                   AlterForError(ex.Message);
                }
                else
                {
                   AlterForError("修改文章分类状态失败，请联系管理员!");
                }
            }
            return Redirect(Url.Action("ArticleType"));
        }

        public ActionResult ArticleTag(int? pageIndex)
        {
            var result = _articleTagQueryService.FindAllPager(CurrentUser.UserId, pageIndex ?? 1, PageSize);
            return View("ArticleTag", result);
        }


        [HttpPost]
        public ActionResult ArticleTag(string articleTagName)
        {
            if (string.IsNullOrEmpty(articleTagName) || articleTagName.Length > 30)
            {
               AlterForError("文章标签名称字符长度不正确");
                return View("ArticleTag");
            }
            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<CreateArticleTagCommand>(
                        new CreateArticleTagCommand(articleTagName, CurrentUser.UserId));
                    transaction.Commit();
                }
                AlterForRight("保存文章标签成功!",2000);
                return Redirect(Url.Action("ArticleTag"));
            }
            catch (Exception ex)
            {
                LogServer.Error("保存文章标签失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                   AlterForError(ex.Message);
                }
                else
                {
                   AlterForError("保存文章标签失败，请联系管理员!");
                }
                return Redirect(Url.Action("ArticleTag"));
            }
        }


        public ActionResult SetArticleTagIsShow(int articleTagId)
        {
            Guard.IsNotZeroOrNegative(articleTagId, "文章标签ID格式不正确");
            var articleTag = _articleTagQueryService.FindOne(articleTagId);
            Guard.IsNotNull(articleTag, "不存在该文章标签");
            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<UpdateArticleTagCommand>(new UpdateArticleTagCommand(articleTagId,
                                                                                            CurrentUser.UserId)
                                                                    {
                                                                        IsShow = !articleTag.IsShow
                                                                    });
                    transaction.Commit();
                    AlterForRight("修改文章标签状态成功!", 2000);
                }
            }
            catch (Exception ex)
            {
                LogServer.Error("修改文章标签状态失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                   AlterForError(ex.Message);
                }
                else
                {
                   AlterForError("修改文章标签状态失败，请联系管理员!");
                }
            }
            return Redirect(Url.Action("ArticleTag"));
        }

        public ActionResult UpdateArticleTag(int articleTagId)
        {
            var model = _articleTagQueryService.FindOne(articleTagId);
            return View("UpdateArticleTag", CreateArticleTagModel(model));
        }

        [HttpPost]
        public ActionResult UpdateArticleTag(ArticleTagModel articleTagModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (var transaction = UnitOfWork.Begin())
                    {
                        _processor.Process<UpdateArticleTagCommand>(new UpdateArticleTagCommand(articleTagModel.Id,
                                                                                      CurrentUser.UserId)
                        {
                            IsShow = articleTagModel.IsShow,
                            TagName = articleTagModel.TagName
                        });
                        AlterForRight("更新文章标签成功!");
                        transaction.Commit();
                    }
                    return Redirect(Url.Action("UpdateArticleTag",  new { articleTagId = articleTagModel.Id }));
                }
                catch (Exception ex)
                {
                    LogServer.Error("更新文章标签失败", ex.Message, HttpContext.Request.Path);
                    if ((ex is PowerException) || (ex is BusinessException))
                    {
                       AlterForError(ex.Message);
                    }
                    else
                    {
                       AlterForError("更新文章标签失败，请联系管理员!");
                    }
                    return View();
                }
            }
            AlterForError("非法操作!");
            return View(); 
        }



        public ActionResult DeleteArticleTag(int articleTagId)
        {
            try
            {
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<DeleteArticleTagCommand>(new DeleteArticleTagCommand(articleTagId));
                    transaction.Commit();
                    AlterForRight("删除文章标签成功!",2000 );
                    return Redirect(Url.Action("ArticleTag"));
                }
            }
            catch (Exception ex)
            {
                LogServer.Error("删除文章标签失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                   AlterForError(ex.Message);
                }
                else
                {
                   AlterForError("删除文章标签失败，请联系管理员!");
                }
                return Redirect(Url.Action("ArticleTag"));
            }

        }


        public ActionResult SetArticleIsShow(int articleId, int? pageIndex)
        {
            try
            {
                Guard.IsNotZeroOrNegative(articleId, "articleId");
                var article = _articleQueryService.FindOne(articleId);
                Guard.IsNotNull(article, "article");
                using (var transaction = UnitOfWork.Begin())
                {
                    _processor.Process<UpdateArticleCommand>(new UpdateArticleCommand(articleId, CurrentUser.UserId)
                                                                 {
                                                                     IsShow = !article.IsShow,
                                                                     ArticleTagIds = article.ArticleTags.Select(x => x.Key).ToArray(),
                                                                     ArticleTypeId = article.ArticleTypeId,
                                                                     Content = article.Content,
                                                                     ContentDesc = article.ContentDesc,
                                                                     ReadCount = article.ReadCount,
                                                                     Title = article.Title,
                                                                     UrlQuoteUrl = article.UrlQuoteUrl
                                                                 });
                    transaction.Commit();
                }
                AlterForRight("修改文章状态成功!", 2000);
            }
            catch (Exception ex)
            {
                LogServer.Error("修改文章状态失败", ex.Message, HttpContext.Request.Path);
                if ((ex is PowerException) || (ex is BusinessException))
                {
                   AlterForError(ex.Message);
                }
                else
                {
                   AlterForError("修改文章状态失败，请联系管理员!");
                }
            }
            return Redirect(Url.Action("ArticleManger", new {pageIndex }));
        }

        #endregion

        #region 个人信息管理

        [HttpGet]
        public ActionResult MyAccount()
        {
            var user = _userQueryService.FindOne(CurrentUser.UserId);
            var userModel = CreateUserAccountModel(user);
            return View("MyAccount", userModel);
        }


        [HttpPost]
        public ActionResult MyAccount(UserAccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var transaction = UnitOfWork.Begin())
                    {
                        var command = new UpdateUserCommand(CurrentUser.UserId)
                                          {
                                              BlogDesc = model.BlogDesc,
                                              BlogName = model.BlogName,
                                              Sex = ReturnUserSexForInt(model.Sex),
                                              RealName = model.RealName,
                                              WeiBoUrl = model.WeiBoUrl,
                                              Phone = model.Phone,
                                              Email = model.Emali,
                                              EnRealName = model.EnRealName
                                          };
                        var userHeadFile = System.Web.HttpContext.Current.Request.Files[0];
                        if (userHeadFile.ContentLength > 0)
                        {
                            string smallImgHead;
                            string standardImgHead;
                            command.ImgHead = _userFileTask.SaveUserImg(
                                new UserFileCommand(CurrentUser.UserId, userHeadFile), out smallImgHead, out standardImgHead);
                            command.SmallImgHead = smallImgHead;
                            command.StandardImgHead = standardImgHead;
                        }
                        _processor.Process<UpdateUserCommand>(command);
                        transaction.Commit();
                        AlterForRight("更新个人信息成功!", 2000);
                        // 清除缓存
                        ClearNowLoginUserCache();
                        return Redirect(Url.Action("MyAccount"));
                    }

                }
                catch (Exception ex)
                {
                    // 记录日志
                    LogServer.Error("更新个人信息失败", ex.Message, HttpContext.Request.Path);
                    if ((ex is PowerException) || (ex is BusinessException))
                    {
                       AlterForError(ex.Message);
                    }
                    else
                    {
                       AlterForError("更新个人信息失败，请联系管理员!");
                    }
                    return Redirect(Url.Action("MyAccount"));
                }
            }
            return Redirect(Url.Action("MyAccount"));
        }

        #endregion

        //#region 广告管理

        //[ActionPower(UserLoginRole.Admin)]
        //public ActionResult AdManager(int? pageIndex)
        //{
        //    var dto = _adQueryService.FindAll();
        //    var viewModel = dto.Select(CreateAdViewModel).ToList();
        //    return View("AdManager", viewModel);
        //}

        //[ActionPower(UserLoginRole.Admin)]
        //public ActionResult CreateAd(CreateAdViewModel viewModel)
        //{
        //    var command = new CreateAdCommand(
        //        viewModel.AdName,
        //        viewModel.AdDesc,
        //        viewModel.IsShow,
        //        CurrentUser.UserId)
        //                      {
        //                          Sort = viewModel.Sort
        //                      };

        //    // 上传图片 
        //    if (Request.Files != null
        //        && Request.Files.Count > 0
        //        && Request.Files[0] != null
        //        && Request.Files[0].ContentLength > 0)
        //    {
        //        var result = _userFileTask.SaveSystemImg(new SystemImgFileCommand(
        //                                                       System.Web.HttpContext.Current.Request.Files[0])
        //                                                       {
        //                                                           SmallImgFileSetting = new SmallImgFileSetting()
        //                                                                                     {
        //                                                                                         Height = 50,
        //                                                                                         Width = 50,
        //                                                                                         IsUser = true
        //                                                                                     },
        //                                                           StandardImgFileSetting = new StandardImgFileSetting()
        //                                                                                        {
        //                                                                                            Height = 450,
        //                                                                                            Width = 940,
        //                                                                                            IsUser = true
        //                                                                                        }
        //                                                       });


        //        command.AdImg = result.Img;
        //        command.SmallImg = result.SmallImg;
        //        command.StandardAdImg = result.StandardImg;
        //    }
        //    try
        //    {
        //        using (var transaction = UnitOfWork.Begin())
        //        {
        //            _processor.Process<CreateAdCommand>(command);
        //            transaction.Commit();
        //            ViewData["alert"] = AlertRightToStr("新增广告成功!",
        //            Url.Action("AdManager", "BlogManager"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogServer.Error("创建广告失败", ex.Message, HttpContext.Request.Path);
        //        if ((ex is PowerException) || (ex is BusinessException))
        //        {
        //           AlterForError(ex.Message);
        //        }
        //        else
        //        {
        //           AlterForError("创建广告失败，请联系管理员!");
        //        }
        //        return AdManager(null);
        //    }

        //    return AdManager(null);
        //}


        //[ActionPower(UserLoginRole.Admin)]
        //public ActionResult SetAdIsShow(int adId)
        //{
        //    Guard.IsNotZeroOrNegative(adId, "广告ID格式不正确");
        //    var ad = _adQueryService.FindOne(adId);
        //    Guard.IsNotNull(ad, "ad");
        //    try
        //    {
        //        using (var transaction = UnitOfWork.Begin())
        //        {
        //            _processor.Process<UpdateAdCommand>(
        //                new UpdateAdCommand(adId, CurrentUser.UserId)
        //                {
        //                    IsShow = !ad.IsShow,
        //                    AdDesc = ad.AdDesc,
        //                    AdImg = ad.AdImg,
        //                    AdName = ad.AdName,
        //                    Sort = ad.Sort
        //                });
        //            ViewData["alert"] = AlertRightToStr("修改广告状态成功!", 2000);
        //            transaction.Commit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogServer.Error("修改广告状态失败", ex.Message, HttpContext.Request.Path);
        //        if ((ex is PowerException) || (ex is BusinessException))
        //        {
        //           AlterForError(ex.Message);
        //        }
        //        else
        //        {
        //           AlterForError("修改广告状态失败，请联系管理员!");
        //        }
        //    }
        //    return AdManager(null);
        //}

        //[ActionPower(UserLoginRole.Admin)]
        //public ActionResult DeleteAd(int adId)
        //{
        //    try
        //    {
        //        var dto = _adQueryService.FindOne(adId);
        //        Guard.IsNotNull(dto, "dto");
        //        using (var transaction = UnitOfWork.Begin())
        //        {
        //            _processor.Process<DeleteAdCommand>(new DeleteAdCommand(
        //                adId,
        //                CurrentUser.UserId));
        //            transaction.Commit();
        //            ViewData["alert"] = AlertRightToStr("删除广告成功!", Url.Action("AdManager"));
        //            // 删除广告图片文件   
        //            return AdManager(null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogServer.Error("删除广告失败", ex.Message, HttpContext.Request.Path);
        //        if ((ex is PowerException) || (ex is BusinessException))
        //        {
        //           AlterForError(ex.Message);
        //        }
        //        else
        //        {
        //           AlterForError("删除广告失败，请联系管理员!");
        //        }
        //        return AdManager(null);
        //    }
        //}

        //[ActionPower(UserLoginRole.Admin)]
        //public ActionResult UpdateAd(int adId)
        //{
        //    var model = _adQueryService.FindOne(adId);
        //    return View(CreateUpdateAdViewModel(model));
        //}

        //[ActionPower(UserLoginRole.Admin)]
        //[HttpPost]
        //public ActionResult UpdateAd(UpdateAdViewModel updateAdViewModel)
        //{

        //    var dto = CreateUpdateAdViewModel(_adQueryService.FindOne(updateAdViewModel.Id));
        //    Guard.IsNotNull(dto, "dto");
        //    try
        //    {
        //        var command = new UpdateAdCommand(updateAdViewModel.Id, CurrentUser.UserId)
        //                          {
        //                              AdDesc = updateAdViewModel.AdDesc,
        //                              AdImg = dto.AdImg,
        //                              StandardAdImg = dto.StandardAdImg,
        //                              SmallImg = dto.SmallAdImg,
        //                              AdName = updateAdViewModel.AdName,
        //                              IsShow = updateAdViewModel.IsShow,
        //                              Sort = updateAdViewModel.Sort
        //                          };

        //        // 上传图片 
        //        if (Request.Files != null
        //            && Request.Files.Count > 0
        //            && Request.Files[0] != null
        //            && Request.Files[0].ContentLength > 0)
        //        {
        //            var result = _userFileTask.SaveSystemImg(new SystemImgFileCommand(
        //                                                           System.Web.HttpContext.Current.Request.Files[0])
        //            {
        //                SmallImgFileSetting = new SmallImgFileSetting()
        //                {
        //                    Height = 50,
        //                    Width = 50,
        //                    IsUser = true
        //                },
        //                StandardImgFileSetting = new StandardImgFileSetting()
        //                {
        //                    Height = 450,
        //                    Width = 940,
        //                    IsUser = true
        //                }
        //            });


        //            command.AdImg = result.Img;
        //            command.SmallImg = result.SmallImg;
        //            command.StandardAdImg = result.StandardImg;
        //        }

        //        using (var transaction = UnitOfWork.Begin())
        //        {
        //            _processor.Process<UpdateAdCommand>(command);
        //            ViewData["alert"] = AlertRightToStr("更新广告成功!");
        //            transaction.Commit();
        //        }

        //        return View(dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogServer.Error("更新广告失败", ex.Message, HttpContext.Request.Path);
        //        if ((ex is PowerException) || (ex is BusinessException))
        //        {
        //           AlterForError(ex.Message);
        //        }
        //        else
        //        {
        //           AlterForError("更新广告失败，请联系管理员!");
        //        }
        //        return View(dto);
        //    }
        //}
        //#endregion

        #region 链接管理

        public ActionResult LinkType(int? pageIndex)
        {

            const int pageSize = 10;
            var projectTypeDto = _linkTypeQueryService.FindAllPager(CurrentUser.UserId, null, pageIndex ?? 1, pageSize);
            var viewMode = projectTypeDto.Select(CreateLlinkTypeModel)
            .ToPagedList(projectTypeDto.PageIndex, projectTypeDto.PageSize, projectTypeDto.TotalCount);
            return View(viewMode);
        }


        [HttpPost]
        [Transaction]
        public ActionResult LinkType(string linkTypeName)
        {
            if (string.IsNullOrEmpty(linkTypeName))
            {
                AlterForError("请输入链接类型名称!"); 
                return View("LinkType");
            }
            _processor.Process<CreateLinkTypeCommand>(new CreateLinkTypeCommand(linkTypeName, CurrentUser.UserId));
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "LinkType");
        }


        [Transaction]
        public ActionResult SetLinkTypeIsShow(int linkTypeId)
        {
            var dto = _linkTypeQueryService.FindOne(linkTypeId);
            Guard.IsNotNull(dto, "dto");
            _processor.Process<UpdateLinkTypeCommand>(new UpdateLinkTypeCommand(linkTypeId, CurrentUser.UserId)
            {
                IsShow = !dto.IsShow,
                TypeName = dto.TypeName,
                Sort = dto.Sort
            });
            return Redirect("LinkType");
        }




        public ActionResult UpdateLinkType(int linkTypeId)
        {
            Guard.IsNotZeroOrNegative(linkTypeId, "linkTypeId");
            var linkType = _linkTypeQueryService.FindOne(linkTypeId);
            Guard.IsNotNull(linkType, "linkType");
            return View(CreateUpdateLinkTypeModel(linkType));
        }

        [HttpPost]
        [Transaction]
        public ActionResult UpdateLinkType(LinkTypeModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.TypeName))
            {
                AlterForError("请输入链接类型名称!");
                return View("LinkType");
            }
            _processor.Process<UpdateLinkTypeCommand>(new UpdateLinkTypeCommand(viewModel.Id, CurrentUser.UserId)
            {
                TypeName = viewModel.TypeName,
                Sort = viewModel.Sort
            });
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "LinkType");
        }



        [Transaction]
        public ActionResult DeleteLinkType(int linkTypeId)
        {
            Guard.IsNotZeroOrNegative(linkTypeId, "linkTypeId");
            try
            {
                _processor.Process<DeleteLinkTypeCommand>(new DeleteLinkTypeCommand(linkTypeId, CurrentUser.UserId));
                return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "LinkType");
            }
            catch (BusinessException ex)
            {
                AlterForError(ex.Message);
                return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "LinkType");
            }

        }


        public ActionResult Link(int? pageIndex)
        {
            // 获取链接分类
            ViewData["LinkTypes"] = _linkTypeQueryService.FindAll(CurrentUser.UserId).Select(x => new SelectListItem()
            {
                Text = x.TypeName,
                Value = x.Id.ToString()
            }).ToList(); ;

            var dtos = _linkQueryService.FindAllPager(CurrentUser.UserId, null, null, pageIndex ?? 1, 20);
            Guard.IsNotNull(dtos, "dtos");
            var viewModels = dtos.Select(CreateLinkModel).ToPagedList(dtos.PageIndex, dtos.PageSize, dtos.TotalCount);
            return View(viewModels);
        }



        [Transaction]
        public ActionResult SetLinkIsShow(int linkId)
        {
            var dto = _linkQueryService.FindOne(linkId);
            Guard.IsNotNull(dto, "dto");
            _processor.Process<UpdateLinkCommand>(new UpdateLinkCommand(dto.Id)
                                                      {
                                                          IsShow = !dto.IsShow
                                                      });
            return Redirect("Link");
        }

        [HttpPost]
        [Transaction]
        public ActionResult AddLink(string linkTxt, int linkTypeId, string linkHref)
        {

            _processor.Process<CreateLinkCommand>(new CreateLinkCommand(
                linkTxt,
                linkTypeId,
                linkHref,
                CurrentUser.UserId));

            return Redirect("Link");
        }

        [HttpPost]
        [Transaction]
        [ValidateInput(false)]
        public ActionResult UpdateLink(UpdateLinkModel viewModel)
        {
            _processor.Process<UpdateLinkCommand>(new UpdateLinkCommand(viewModel.Id)
            {
                IsShow = viewModel.IsShow,
                LinkTxt = viewModel.LinkTxt,
                LinkUrl = viewModel.LinkUrl,
                LinkTypeId = viewModel.LinkTypeId
            });
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "Link");
        }


        public ActionResult UpdateLink(int linkId)
        {

            Guard.IsNotZeroOrNegative(linkId, "linkId");
            var linkObj = _linkQueryService.FindOne(linkId);
            Guard.IsNotNull(linkObj, "linkObj");

            // 获取链接分类
            ViewData["LinkTypes"] = _linkTypeQueryService.FindAll(CurrentUser.UserId).Select(x => new SelectListItem()
            {
                Text = x.TypeName,
                Value = x.Id.ToString()
            }).ToList(); ;

            return View(CreateUpdateLinkModel(linkObj));
        }

        [Transaction]
        public ActionResult DeleteLink(int linkId)
        {
            Guard.IsNotZeroOrNegative(linkId, "linkId");
            _processor.Process<DeleteLinkCommand>(new DeleteLinkCommand(linkId, CurrentUser.UserId));
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "Link");
        }

        #endregion

        #region 作品类型管理

        [HttpGet]
        public ActionResult ProjectType(int? pageIndex)
        {
            const int pageSize = 10;
            var projectTypeDto = _projectTypeQueryService.FindAllPager(CurrentUser.UserId, null, pageIndex ?? 1, pageSize);
            var viewMode = projectTypeDto.Select(CreateProjectTypeModel)
            .ToPagedList(projectTypeDto.PageIndex, projectTypeDto.PageSize, projectTypeDto.TotalCount);
            return View(viewMode);
        }

        [HttpGet]
        public ActionResult UpdateProjectType(int projectTypeId)
        {
            var projectTypeDto = _projectTypeQueryService.FindOne(projectTypeId);
            var projectTypeViewModel = CreateUpdateProjectTypeModel(projectTypeDto);
            return View(projectTypeViewModel);
        }


        [HttpPost]
        [Transaction]
        public ActionResult UpdateProjectType(UpdateProjectTypeModel model)
        {
            _processor.Process<UpdateProjectTypeCommand>(new UpdateProjectTypeCommand(model.Id)
                                                             {
                                                                 IsShow = model.IsShow,
                                                                 TypeName = model.TypeName
                                                             });
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "ProjectType");
        }

        [Transaction]
        public ActionResult SetProjectTypeIsShow(int projectTypeId)
        {
            Guard.IsNotZeroOrNegative(projectTypeId, "projectTypeId");
            var projectType = _projectTypeQueryService.FindOne(projectTypeId);
            Guard.IsNotNull(projectType, "projectType");
            _processor.Process<UpdateProjectTypeCommand>(new UpdateProjectTypeCommand(projectTypeId)
            {
                IsShow = !projectType.IsShow
            });
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "ProjectType");
        }

        [HttpPost]
        [Transaction]
        public ActionResult ProjectType(string projectTypeName)
        {
            if (string.IsNullOrEmpty(projectTypeName))
            { 
                AlterForError("请输入作品类型名称!");
                return View("ProjectType");
            }
            _processor.Process<CreateProjectTypeCommand>(new CreateProjectTypeCommand(projectTypeName, true, CurrentUser.UserId));
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "ProjectType");
        }

        [Transaction]
        public ActionResult DeleteProjectType(int projectTypeId)
        {
            Guard.IsNotZeroOrNegative(projectTypeId, "projectTypeId");
            _processor.Process<DeleteProjectTypeCommand>(new DeleteProjectTypeCommand(projectTypeId));
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "ProjectType");
        }

        #endregion

        #region 作品管理

        public ActionResult AddProject()
        {

            ViewData["ProjectTypes"] = _projectTypeQueryService.FindAll(CurrentUser.UserId, true).Select(x => new SelectListItem()
            {
                Text = x.TypeName,
                Value = x.Id.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [Transaction]
        [ValidateInput(false)]
        public ActionResult AddProject(AddProjectModel model)
        {
            // 上传目录
            var targetCategory = string.Empty;

            var command = new CreateProjectCommand(model.ProjectName, model.Introduction, model.Content, CurrentUser.UserId,
                                                 false, model.ProjectTypeId)
            {
                WebSite = model.WebSite
            };

            // 上传图片 
            if (Request.Files != null && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
            {
                //targetCategory = ConfigurationManager.AppSettings["UploadFilePath"] + CurrentUser.UserId + @"\";
                //// 允许上传文件后缀
                //var imgSuffix = ConfigurationManager.AppSettings["ImgSuffix"];

                string smallImgHead;
                string standardImgHead;
                command.ProjectImg = _userFileTask.SaveUserImg(
                    new UserFileCommand(
                        CurrentUser.UserId,
                        System.Web.HttpContext.Current.Request.Files[0]),
                        out smallImgHead,
                        out standardImgHead);
                command.SmallProjectImg = smallImgHead;
                command.StandardProjectImg = standardImgHead;
                /*
                    , ) _fileAttachmentUtility.SaveLocalhostImgAttach(
                    ,
                    targetCategory,
                    new FileAttachOption()
                    {
                        BuildOriginalImg = true,
                        BuildStandardImg = true,
                        ImgSuffix = imgSuffix.Split('|')
                    });*/
            }

            _processor.Process<CreateProjectCommand>(command);
            return Redirect("Project");
        }

        public ActionResult Project(int? pageIndex)
        {
            const int pageSize = 10;
            var projectDto = _projectQueryService.FindAllPager(CurrentUser.UserId, null, null, pageIndex ?? 1, pageSize);
            var projectModel = projectDto.Select(CreateProjectModel).ToPagedList(projectDto.PageIndex, projectDto.PageSize, projectDto.TotalCount);
            return View(projectModel);
        }

        [Transaction]
        public ActionResult SetProjectIsShow(int projectId)
        {
            Guard.IsNotZeroOrNegative(projectId, "projectId");
            var project = _projectQueryService.FindOne(projectId);
            Guard.IsNotNull(project, "project");

            _processor.Process<UpdateProjectCommand>(new UpdateProjectCommand(projectId)
                                                         {
                                                             IsShow = !project.IsShow
                                                         });
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "Project");
        }


        [Transaction]
        public ActionResult DeleteProject(int projectId)
        {
            Guard.IsNotZeroOrNegative(projectId, "projectId");
            var project = _projectQueryService.FindOne(projectId);
            Guard.IsNotNull(project, "project");
            if (!string.IsNullOrEmpty(project.ProjectImg))
            {
                // 删除附件图片
                // 上传目录 
                var projectImgFilePath = Server.MapPath("/") + project.ProjectImg;
                var projectSmallImgFilePath = Server.MapPath("/") + project.SmallProjectImg;
                var projectStandardImgFilePath = Server.MapPath("/") + project.StandardProjectImg;

                if (System.IO.File.Exists(projectImgFilePath))
                {
                    System.IO.File.Delete(projectImgFilePath);
                }
                if (System.IO.File.Exists(projectSmallImgFilePath))
                {
                    System.IO.File.Delete(projectSmallImgFilePath);
                }
                if (System.IO.File.Exists(projectStandardImgFilePath))
                {
                    System.IO.File.Delete(projectStandardImgFilePath);
                }
            }
            _processor.Process<DeleteProjectCommand>(new DeleteProjectCommand(projectId));
            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "Project");
        }


        public ActionResult UpdateProject(int projectId)
        {
            Guard.IsNotZeroOrNegative(projectId, "projectId");
            var projectDto = _projectQueryService.FindOne(projectId);
            Guard.IsNotNull(projectDto, "projectDot");
            ViewData["ProjectTypes"] = _projectTypeQueryService.FindAll(CurrentUser.UserId, true).Select(x => new SelectListItem()
            {
                Text = x.TypeName,
                Value = x.Id.ToString()
            }).ToList();
            return View(CreateUpdateProjectModel(projectDto));
        }


        [HttpPost]
     
        [ValidateInput(false)]
        public ActionResult UpdateProject(UpdateProjectModel model)
        {
            var fileName = string.Empty;

            var command = new UpdateProjectCommand(model.Id)
            {
                Content = model.Content,
                Introduction = model.Introduction,
                ProjectImg = fileName,
                ProjectName = model.ProjectName,
                ProjectTypeId = model.ProjectTypeId,
                WebSite = model.WebSite,
                Sort = model.Sort

            };

            // 上传图片 
            if (Request.Files.Count > 0 && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
            {

                string smallImgHead;
                string standardImgHead;
                command.ProjectImg = _userFileTask.SaveUserImg(
                    new UserFileCommand(
                        CurrentUser.UserId,
                        System.Web.HttpContext.Current.Request.Files[0]),
                        out smallImgHead,
                        out standardImgHead);
                command.SmallProjectImg = smallImgHead;
                command.StandardProjectImg = standardImgHead;

            }
                try
                {
                    using (var transaction = UnitOfWork.Begin())
                    {
                        _processor.Process<UpdateProjectCommand>(command); 
                        transaction.Commit();
                        AlterForRight("更新项目成功!", 2000);
                        return Redirect(Url.Action("Project"));
                    }

                }
                catch (Exception ex)
                {
                    // 记录日志
                    LogServer.Error("更新项目失败", ex.Message, HttpContext.Request.Path);
                    if ((ex is PowerException) || (ex is BusinessException))
                    {
                        AlterForError(ex.Message);
                    }
                    else
                    {
                        AlterForError("更新项目失败，请联系管理员!");
                    }
                    return Redirect(Url.Action("Project"));
                }
        }



        #endregion


        #region Helper

        private UpdateLinkTypeModel CreateUpdateLinkTypeModel(LinkTypeDto dto)
        {
            return new UpdateLinkTypeModel()
            {
                Id = dto.Id,
                TypeName = dto.TypeName,
                Sort = dto.Sort,
                IsShow = dto.IsShow
            };
        }


        private LinkTypeModel CreateLlinkTypeModel(LinkTypeDto l)
        {
            return new LinkTypeModel()
            {
                Id = l.Id,
                TypeName = l.TypeName,
                UserId = l.UserId,
                UserName = l.UserName,
                CreateDate = l.CreateDate,
                LastUpdate = l.LastUpdate,
                IsShow = l.IsShow,
                Sort = l.Sort
            };
        }


        private UpdateProjectModel CreateUpdateProjectModel(ProjectDto p)
        {
            return new UpdateProjectModel()
            {
                Id = p.Id,
                Content = p.Content,
                Introduction = p.Introduction,
                IsShow = p.IsShow,
                ProjectImg = !string.IsNullOrEmpty(p.ProjectImg)
                             ? p.ProjectImg
                             : Url.Content(@"~/UploadFile/System/Files/MinNoFile.jpg"),
                SmallProjectImg = !string.IsNullOrEmpty(p.SmallProjectImg)
                             ? p.SmallProjectImg
                             : Url.Content(@"~/UploadFile/System/Files/MinNoFile.jpg"),
                StandardProjectImg = !string.IsNullOrEmpty(p.StandardProjectImg)
                             ? p.StandardProjectImg
                             : Url.Content(@"~/UploadFile/System/Files/MinNoFile.jpg"),
                ProjectName = p.ProjectName,
                ProjectTypeId = p.ProductTypeId,
                WebSite = p.WebSite,
                Sort = p.Sort
            };

        }


        private ProjectModel CreateProjectModel(ProjectDto p)
        {
            return new ProjectModel()
            {
                Id = p.Id,
                IsShow = p.IsShow,
                LastDateTime = p.LastDateTime,
                ProjectName = p.ProjectName,
                TypeName = p.ProductTypeName,
                Sort = p.Sort
            };
        }

        private UpdateProjectTypeModel CreateUpdateProjectTypeModel(ProjectTypeDto p)
        {
            return new UpdateProjectTypeModel()
            {
                Id = p.Id,
                IsShow = p.IsShow,
                TypeName = p.TypeName
            };
        }

        private ProjectTypeModel CreateProjectTypeModel(ProjectTypeDto p)
        {
            return new ProjectTypeModel()
            {
                Id = p.Id,
                TypeName = p.TypeName,
                IsShow = p.IsShow
            };
        }


        private UpdateLinkModel CreateUpdateLinkModel(LinkDto dto)
        {
            return new UpdateLinkModel()
            {
                Id = dto.Id,
                IsShow = dto.IsShow,
                LinkTxt = dto.LinkTxt,
                LinkUrl = dto.LinkUrl,
                LinkTypeId = dto.LinkTypeId
            };
        }

        private LinkModel CreateLinkModel(LinkDto dto)
        {
            return new LinkModel()
            {
                CreateDate = dto.CreateDate,
                Id = dto.Id,
                IsShow = dto.IsShow,
                LastUpdate = dto.LastUpdate,
                LinkTxt = dto.LinkTxt,
                LinkUrl = dto.LinkUrl,
                LinkTypeId = dto.LinkTypeId,
                LinkTypeName = dto.LinkTypeName
            };
        }

        private UpdateAdViewModel CreateUpdateAdViewModel(AdDto dto)
        {
            return new UpdateAdViewModel()
                       {
                           Id = dto.Id,
                           AdDesc = dto.AdDesc,
                           AdImg = string.IsNullOrEmpty(dto.AdImg)
                                   ? Url.Content(@"~/UploadFile/System/Files/Default.jpg")
                                   : dto.AdImg,
                           SmallAdImg = string.IsNullOrEmpty(dto.SmallAdImg)
                                  ? Url.Content(@"~/UploadFile/System/Files/Default.jpg")
                                  : dto.SmallAdImg,
                           StandardAdImg = string.IsNullOrEmpty(dto.StandardAdImg)
                                  ? Url.Content(@"~/UploadFile/System/Files/Default.jpg")
                                   : dto.StandardAdImg,
                           AdName = dto.AdName,
                           AdType = dto.AdType,
                           IsShow = dto.IsShow,
                           Sort = dto.Sort
                       };
        }

        private AdViewModel CreateAdViewModel(AdDto dto)
        {
            return new AdViewModel()
                       {
                           Id = dto.Id,
                           AdDesc = dto.AdDesc,
                           AdImg = string.IsNullOrEmpty(dto.AdImg)
                                   ? Url.Content(@"~/UploadFile/System/Files/Default.jpg")
                                   : dto.AdImg,
                           SmallAdImg = string.IsNullOrEmpty(dto.SmallAdImg)
                                  ? Url.Content(@"~/UploadFile/System/Files/Default.jpg")
                                  : dto.SmallAdImg,
                           StandardAdImg = string.IsNullOrEmpty(dto.StandardAdImg)
                                  ? Url.Content(@"~/UploadFile/System/Files/Default.jpg")
                                   : dto.StandardAdImg,
                           AdName = dto.AdName,
                           AdType = dto.AdType,
                           IsShow = dto.IsShow,
                           Sort = dto.Sort

                       };
        }




        private UserAccountModel CreateUserAccountModel(UserDto dto)
        {
            return new UserAccountModel()
                       {
                           UserName = dto.UserName,
                           BlogDesc = dto.BlogDesc,
                           BlogName = dto.BlogName,
                           ImgHead = GetUserHeadImg(dto.ImgHead),
                           RealName = dto.RealName,
                           Sex = ReturnUserSex(dto.UserSex),
                           WebSiteUrl = dto.WebSiteUrl,
                           WeiBoUrl = dto.WeiBoUrl,
                           Phone = dto.Phone,
                           Emali = dto.Email,
                           EnRealName = dto.EnRealName
                       };

        }

        private int ReturnUserSex(UserSex? userSex)
        {
            if (userSex == null)
            {
                return 2;
            }
            else if (userSex == UserSex.Male)
            {
                return 0;
            }
            return 1;

        }

        private UserSex? ReturnUserSexForInt(int? userSex)
        {
            if (userSex == null || userSex == 2)
            {
                return null;
            }
            else if (userSex == 0)
            {
                return UserSex.Male;
            }
            return UserSex.Female;

        }


        private ArticleTypeModel CreateArticleTypeModel(ArticleTypeDto articleType)
        {
            return new ArticleTypeModel()
                       {
                           Id = articleType.Id,
                           IsShow = articleType.IsShow,
                           TypeName = articleType.TypeName
                       };
        }

        private ArticleTagModel CreateArticleTagModel(ArticleTagDto articleTag)
        {
            return new ArticleTagModel()
                       {
                           Id = articleTag.Id,
                           IsShow = articleTag.IsShow,
                           TagName = articleTag.TagName
                       };
        }

        private UpdateArticleModel CreateUpdateArticleModel(ArticleDto article)
        {
            return new UpdateArticleModel()
                       {
                           ArticleTags = article.ArticleTags,
                           ArticleTypeId = article.ArticleTypeId,
                           ArticleTypeName = article.ArticleTypeName,
                           Content = article.Content,
                           Id = article.Id,
                           IsShow = article.IsShow,
                           LastUpdate = article.LastUpdate,
                           ReadCount = article.ReadCount,
                           Title = article.Title,
                           UrlQuoteUrl = article.UrlQuoteUrl,
                           ContentDesc = article.ContentDesc
                       };
        }


        #endregion
    }
}
