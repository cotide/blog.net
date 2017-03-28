//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserTourLogViewModel.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/9 0:43:03 
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

namespace Cotide.Web.Controllers.Blog.ViewModel.UserTourLog
{
    public class UserTourLogViewModel
    {

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// 游览用户
        /// </summary> 
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 用户博客名称
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// 用户头像(原图)
        /// </summary>
        public string ImgHead { get; set; }

        /// <summary>
        /// 用户头像(小图) 50 X 50
        /// </summary>
        public string SmallImgHead { get; set; }

        /// <summary>
        /// 用户头像(标准图) 150 X 150
        /// </summary>
        public string StandardImgHead { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
