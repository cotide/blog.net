using System.Web;
using Cotide.Framework.Utility;

namespace Cotide.Domain.Contracts.Commands
{
    public class UserFileCommand
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public readonly int UserId;
 

        /// <summary>
        /// 文件
        /// </summary>
        public readonly HttpPostedFile Data;


        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="data"></param> 
        public UserFileCommand(int userId, HttpPostedFile data)
        {
            Guard.IsNotNull(data, "data");
            Guard.IsNotZeroOrNegative(userId, "userId");
            UserId = userId;
            Data = data; 
        }
    }
}
