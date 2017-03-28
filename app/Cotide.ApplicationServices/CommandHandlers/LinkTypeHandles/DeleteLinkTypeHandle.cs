using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Tasks.Commands.LinkCommands;
using Cotide.Tasks.Commands.LinkTypeCommands;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;
using Cotide.Framework.Exceptions;

namespace Cotide.Tasks.CommandHandlers.LinkTypeHandles
{
    public class DeleteLinkTypeHandle : ICommandHandler<DeleteLinkTypeCommand>
    {
        private readonly ILinkTypeRepository _linkTypeRepository;
        private readonly ILinkRepository _linkRepository; 


        public DeleteLinkTypeHandle(
            ILinkTypeRepository linkTypeRepository, 
            ILinkRepository linkRepository)
        {
            _linkTypeRepository = linkTypeRepository;
            _linkRepository = linkRepository;
        }

        public void Handle(DeleteLinkTypeCommand command)
        {
            var linkType = _linkTypeRepository.FindAll().FirstOrDefault(x => x.Id == command.LinkTypeId);
            Guard.IsNotNull(linkType, "linkType");

            var link = (from f in _linkRepository.FindAll()
                let t = f.LinkType
                where t.Id == command.LinkTypeId
                select f);
            if (link.Any())
            {
                throw new BusinessException("链接类型下有相关链接，请清除后删除!");
            }

            if (linkType.User.Id != command.UserId)
            {
                throw new PowerException("非法操作");
            }
            _linkTypeRepository.Delete(linkType);
        }
    }
}
