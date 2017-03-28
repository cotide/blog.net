//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateUserTourLogHandlers.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/9 0:04:03 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System;
using System.Linq;
using Cotide.Domain;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.UserTourLogCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.UserTourLogHandlers
{
    public class CreateUserTourLogHandle : ICommandHandler<CreateUserTourLogCommand>
    {
        protected IUserTourLogRepository UserTourLogRepository;

        protected IUserRepository UserRepository;
         

        public CreateUserTourLogHandle(IUserTourLogRepository userTourLogRepository,
            IUserRepository userRepository)
        {
            UserTourLogRepository = userTourLogRepository;
            UserRepository = userRepository;
        }

        public void Handle(CreateUserTourLogCommand command)
        { 

            //当前用户是当前访问用户 不进行访问信息记录
            if(command.UserId==command.TourUserId)
                return;  

            var query = (from utr in UserTourLogRepository.FindAll()
                         let u = utr.User
                         let ut = utr.TourUser
                         where u.Id == command.UserId
                               && ut.Id == command.TourUserId
                         select utr);

            var obj = query.FirstOrDefault();
            if (obj != null)
            {
                // 进行更新&排序
                obj.LastDateTime = DateTime.Now;
                UserTourLogRepository.Update(obj);
            }
            else
            {

                
                var user = UserRepository.Get(command.UserId);
                Guard.IsNotNull(user,"user");

                var insertTourUser = UserRepository.Get(command.TourUserId);
                Guard.IsNotNull(insertTourUser, "insertTourUser");


                UserTourLogRepository.Save(new UserTourLog()
                {
                    User = user,
                    CreateDate = DateTime.Now,
                    TourUser = insertTourUser,
                     LastDateTime = DateTime.Now
                });

                 
                // 进行删除&插入
                var maxIndex = GetMaxSort(command.UserId);
                if (command.MaxTourUserCount != null && command.MaxTourUserCount > 0)
                {
                    if (maxIndex > command.MaxTourUserCount)
                    { 
                        var endObj = (from ut in UserTourLogRepository.FindAll()
                                     let u = ut.User
                                     where u.Id == command.UserId 
                                     orderby ut.LastDateTime descending 
                                     select ut).FirstOrDefault();

                        UserTourLogRepository.Delete(endObj);
                    }

                }
            }
        }


        /// <summary>
        /// 获取当前游览用户的最大Sort值
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int GetMaxSort(int userId)
        {
            return (from u in UserRepository.FindAll()
                    let ur = u.UserTourLogs
                    where u.Id == userId
                    select ur).Count(); 
        }

    }
}
