using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleTypeCommands
{
    public class DeleteArticleTypeCommand : CommandBase
    {
        public readonly int ArticleTypeId;

        public readonly int CurrentUserId;

        public DeleteArticleTypeCommand(int articleTypeId, int currentUserId)
        {
            Guard.IsNotZeroOrNegative(articleTypeId, "articleTypeId");
            Guard.IsNotZeroOrNegative(currentUserId, "currentUserId");
            ArticleTypeId = articleTypeId;
            CurrentUserId = currentUserId;
        }
    }
}
