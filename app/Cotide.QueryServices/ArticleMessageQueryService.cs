using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Domain.Dtos.ArticleMessage;
using Cotide.Framework.Collections;
using Cotide.Framework.Extensions;

namespace Cotide.QueryServices
{
    /// <summary>
    /// 文章留言查询服务
    /// </summary>
    public class ArticleMessageQueryService : IArticleMessageQueryService
    {
        ///// <summary>
        ///// 默认用户头像
        ///// </summary>
        //private string DefaultUserHeadImg
        //{
        //    get
        //    {
        //        var defaultUserHeadImg = ConfigurationManager.AppSettings["DefaultUserHeadImg"];
        //        return defaultUserHeadImg ?? "";
        //    }
        //}


        protected readonly IDbProxyRepository<User> UserDbProxyRepository; 
        protected readonly IDbProxyRepository<Article> ArticleDbProxyRepository; 
        protected readonly IDbProxyRepository<ArticleMessage> ArticleMessageDbProxyRepository;

        #region IQC注入
        public ArticleMessageQueryService(
            IDbProxyRepository<Article> articleDbProxyRepository
           , IDbProxyRepository<ArticleMessage> articleMessageDbProxyRepository,
           IDbProxyRepository<User> userDbProxyRepository)
        {
            ArticleDbProxyRepository = articleDbProxyRepository;
            ArticleMessageDbProxyRepository = articleMessageDbProxyRepository;
            UserDbProxyRepository = userDbProxyRepository;
        }

        #endregion

        /// <summary>
        /// 获取文章留言
        /// </summary>
        /// <param name="articleId">文章ID</param> 
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagedList<ArticleMessageDto> FindAllByArticleIdPager(
            int articleId, 
            int pageIndex,
            int pageSize)
        {
            var query = (from a in ArticleDbProxyRepository.FindAll()
                         from m in a.ArticleMessages
                         let u = m.User
                         where a.Id == articleId
                         && a.IsShow 
                         orderby m.CreateDate ascending
                         select CreateArticleMessageDto(a,m, u));
            return query.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取文章留言（状态为显示）
        /// </summary>
        /// <param name="articleId">文章ID</param> 
        /// <returns></returns>
        public IList<ArticleMessageDto> FindAllByArticleId(int articleId)
        {
            var query = (from a in ArticleDbProxyRepository.FindAll()
                         from m in a.ArticleMessages
                         let u = m.User
                         where a.Id == articleId
                         && a.IsShow 
                         orderby m.CreateDate descending
                         select CreateArticleMessageDto(a,m, u));
            return query.ToList(); 
        }

        /// <summary>
        /// 获取文章留言（状态为显示）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="topIndex">前N条</param>
        /// <returns></returns>
        public IList<ArticleMessageDto> FindTop(int userId,int topIndex)
        {

            var articles = ArticleDbProxyRepository.FindAll().Where(x=>x.User.Id == userId);
            var query = (
                from a in articles
                
                         from m in a.ArticleMessages 
                         where  a.IsShow 
                         orderby m.CreateDate descending
                         select CreateArticleMessageDto(a,m, m.User)).Take(topIndex); 
            return query.ToList(); 
        }

        #region Helper
        private ArticleMessageDto CreateArticleMessageDto( 
            Article a,
            ArticleMessage am,
            User u)
        {
            //var doto = new ArticleMessageDto()
            //          {
            //              Id = am.Id,
            //              Content = HttpUtility.HtmlDecode(am.Content),
            //              CreateDate = am.CreateDate,
            //              UserImg = DefaultUserHeadImg
            //          };

            //if (u != null)
            //{
            //    doto.UserId = u.Id;
            //    doto.NickName = u.RealName;
            //    doto.UserImg = u.SmallImgHead;
            //    doto.UserDomain = u.Domain;
            //}
            //else
            //{
            //    doto.NickName = am.NickName;
            //}



            //var articleReplyMessagesList = (from ar in ArticleMessageDbProxyRepository.FindAll()
            //                                from  arm in ar.ArticleMessages
            //                                let ra =ar.RootArticleMessage
            //                                let ba = ar.BaseArticleMessage
            //                                let aru = ar.User
            //                                let bau = ba.User
            //                                where ra.Id == am.Id 
            //                                select CreateArticleReplyMessageDto(ar,arm,aru,bau)).ToList();


            ////var articleReplyMessageList = (from qam in am.ArticleMessages
            ////                               let qu = qam.User
            ////                               let bm = qam.BaseArticleMessage
            ////                               let tu = bm.User
            ////                               select CreateArticleReplyMessageDto(am,bm, qu, tu)).ToList();



            //doto.ArticleReplyMessages = articleReplyMessagesList;

            //// 加载留言的所有回复
            //var articleReplyMessageList = (from ar in ArticleReplyMessageDbProxyRepository.FindAll()
            //                               let aru = ar.User
            //                               let bam = ar.BaseArticleMessage
            //                               where bam.Id == am.Id
            //                               select ar).ToList();

            //// 加载留言一级回复
            //var articleReplyMessageOneList = articleReplyMessageList.Where(x => x.BaseArticleReplyMessage == null).ToList(); 
            //// 加载留言第二级回复
            //var articleReplyMessageTwoList = articleReplyMessageList.Where(x => x.BaseArticleReplyMessage != null).ToList(); 
        
            //// 加载留言回复数据
            //articleReplyMessageOneList.ForEach(x => doto.ArticleReplyMessages.Add(
            //    CreateArticleReplyMessageDto(x, x.User, articleReplyMessageTwoList)));


            //articleReplyMessageList.ForEach(x=>
            //                                    {
                                                    
            //                                        doto.ArticleReplyMessages=
            //                                    });

            //if (amr != null)
            //{

            //    amr.ForEach(x =>
            //                    {
            //                       x.ArticleReplyMessage.Add(new ArticleReplyMessageDto()
            //                                                     {
            //                                                         Content = HttpUtility.HtmlDecode(x.Content),
            //                                                     });

            //                    });






            //    doto.ArticleReplyMessages
                    
            //        .. = new BaseArticleMessageDto();
            //    if (bam.User != null)
            //    {
            //        doto.BaseArticleMessage.UserId = bam.User.Id;
            //        doto.BaseArticleMessage.NickName = bam.User.RealName;
            //        doto.BaseArticleMessage.Domain = bam.User.Domain;
            //    }
            //    else
            //    {
            //        doto.BaseArticleMessage.NickName = bam.NickName;
            //    }
            //}
            var dto =  new ArticleMessageDto()
                       {
                           Content = HttpUtility.HtmlDecode(am.Content),
                           CreateDate = am.CreateDate,
                           Id = am.Id,
                           ArticleId = a.Id
                       };
            // 评论用户信息
            if (u != null)
            {
                dto.UserArticleMessageDto = new UserArticleMessageDto()
                                                {
                                                    NickName = u.RealName,
                                                    UserId = u.Id,
                                                    UserImg =  u.SmallImgHead,
                                                    UserDomain = u.Domain
                                                }; 
            }
            else
            {
                dto.UserArticleMessageDto = new UserArticleMessageDto()
                                                {
                                                    NickName = am.NickName
                                                };
            }

            // 评论对象用户信息
            if(am.BaseArticleMessage!=null)
            {
                dto.BaseArticleMessageId = am.BaseArticleMessage.Id;
                var user = am.BaseArticleMessage.User;
                if (user != null)
                {
                    dto.TagerUserArticleMessageDto = new TagerUserArticleMessageDto()
                    {
                        NickName = user.RealName,
                        UserId = user.Id,
                        UserImg = user.SmallImgHead,
                        UserDomain = user.Domain
                    };
                }
                else
                {
                    dto.TagerUserArticleMessageDto  = new TagerUserArticleMessageDto()
                    {
                        NickName = am.BaseArticleMessage.NickName
                    };
                }
            }
            return dto;
        }


        //private ArticleReplyMessageDto CreateArticleReplyMessageDto(
        //    ArticleMessage am,
        //    ArticleMessage bm,
        //    User u,
        //    User tu)
        //{
        //    var result = new ArticleReplyMessageDto()
        //               {
        //                   Content = HttpUtility.HtmlDecode(am.Content),
        //                   CreateDate = am.CreateDate,
        //                   Id = am.Id,
        //                   NickName = am.NickName,
        //                   TagerNickName = bm.NickName
        //               };
        //    if (u != null)
        //    {
        //        result.UserDomain = u.Domain;
        //        result.UserId = u.Id;
        //        result.UserImg = u.SmallImgHead;
                
        //    }
        //    if(tu!=null)
        //    {
        //        result.TagerUserDomain = u.Domain;
        //        result.TagerUserId = u.Id;
        //        result.TagerUserImg = u.SmallImgHead;
        //    }
        //    return result;
        //}
        #endregion
    }
}
