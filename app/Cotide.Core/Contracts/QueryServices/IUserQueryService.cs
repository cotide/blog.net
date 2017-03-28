//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminQueryService.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/6 12:43:05 
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
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 查询管理员服务接口
    /// </summary>
    public interface IUserQueryService
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        UserDto FindOne(int id);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户ID</param>
        /// <returns></returns>
        UserDto FindOne(string userName);

        /// <summary>
        /// 根据域名获取用户实体
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns></returns>
        UserDto GetUserByDomain(string domain);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="paw">密码</param>
        /// <returns></returns>
        UserDto FindOne(string userName, string paw);

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<UserDto> FindAllPager(string userName, string realName, int pageIndex, int pageSize);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="niceName">用户昵称</param>
        /// <returns></returns>
        UserDto FindOneByNiceName(string niceName);
    }
}
