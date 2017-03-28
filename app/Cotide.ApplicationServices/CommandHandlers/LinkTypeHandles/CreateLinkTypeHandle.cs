using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Tasks.Commands.LinkCommands;
using Cotide.Tasks.Commands.LinkTypeCommands;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.CommandHandlers.LinkTypeHandles
{
    public class CreateLinkTypeHandle : ICommandHandler<CreateLinkTypeCommand>
    {
        private readonly ILinkTypeRepository _linkTypeRepository;
        private readonly IUserRepository _userRepository;

        public CreateLinkTypeHandle(
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
                IsShow = command.IsShow,
                Sort = command.Sort,
                CreateDate = DateTime.Now,
                LastDateTime = DateTime.Now
            });
        }
    }
}
