//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateArticleTypeHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/10 23:01:03 
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
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Exceptions;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ArticleTypeCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleTypeHandlers
{
    public class UpdateArticleTypeHandle : ICommandHandler<UpdateArticleTypeCommand>
    {
        private readonly IArticleTypeRepository _iArticleTypeRepository;

        #region IQC注入
        public UpdateArticleTypeHandle(IArticleTypeRepository articleTypeRepository)
        {
            _iArticleTypeRepository = articleTypeRepository;
        }
        #endregion


        public void Handle(UpdateArticleTypeCommand command)
        {
            var articleType = _iArticleTypeRepository.Load(command.ArticleTypeId);
            Guard.IsNotNull(articleType, "articleType");
            if (articleType.User.Id != command.CurrentUserId)
            {
                throw new BusinessException("非法操作");
                return;
            }

            var isHaveArticleType = (from query in _iArticleTypeRepository.FindAll()
                                     where query.TypeName == command.TypeName
                                           && query.TypeName != null
                                           && query.Id != articleType.Id
                                     select query);
            if (isHaveArticleType.Count() > 0)
            {
                throw new BusinessException("已经存在相同的文章类别名称!");
                return;
            }

            if (command.IsShow != null)
            {
                articleType.IsShow = (bool)command.IsShow;
            }
            if (!string.IsNullOrEmpty(command.TypeName))
            {
                articleType.TypeName = command.TypeName.Trim();
            }
            articleType.LastDateTime = DateTime.Now;
            _iArticleTypeRepository.SaveOrUpdate(articleType);
        }

    }
}
