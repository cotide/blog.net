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
    public class UpdateLinkTypeHandle : ICommandHandler<UpdateLinkTypeCommand>
    {
        private readonly ILinkTypeRepository _linkTypeRepository;
        private readonly IUserRepository _userRepository;

        public UpdateLinkTypeHandle(
            ILinkTypeRepository linkTypeRepository,
            IUserRepository userRepository)
        {
            _linkTypeRepository = linkTypeRepository;
            _userRepository = userRepository;
        }

        public void Handle(CreateLinkTypeCommand command)
        {
            var user = _userRepository.Get(command.UserId);
            Guard.IsNotNull(user, "user");
            _linkTypeRepository.Save(new Domain.LinkType()
            {
                TypeName = command.TypeName,
                User = user,
                CreateDate = DateTime.Now,
                LastDateTime = DateTime.Now
            });
        }

        public void Handle(UpdateLinkTypeCommand command)
        {
            var linkType = _linkTypeRepository.FindAll().FirstOrDefault(x => x.Id == command.LinkTypeId);
            Guard.IsNotNull(linkType, "linkType");
            if (linkType.User.Id != command.UserId)
            {
                throw new PowerException("非法操作");
            }
            if (command.IsShow != null)
            {
                linkType.IsShow = (bool)command.IsShow;
            }
            if (command.Sort != null)
            {
                linkType.Sort = (int)command.Sort;
            }

            linkType.TypeName = command.TypeName;
            linkType.LastDateTime = DateTime.Now;  
            _linkTypeRepository.Save(linkType);
        }
    }
}
