using Cotide.Domain;
using Cotide.Domain.Enum;
using Cotide.Infrastructure.NHibernateMaps.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public sealed class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table(MapConst.Mapname + "Users");
            Id(m => m.Id).Column("Id").GeneratedBy.HiLo(
                MapConst.Mapname + "hibernate_unique_key",
                "next_hi", 
                "0", 
                p =>p.AddParam("where", 
            string.Format(
            "table_name = '{0}'",
            MapConst.Mapname + "Users")));

            Map(m => m.UserName).Length(100).Column("UserName");
            Map(m => m.UserNo).Length(100);
            Map(m => m.ImgHead).Length(200);
            Map(m => m.UserRole).CustomType<UserRole>();
            Map(m => m.SmallImgHead).Length(200);
            Map(m => m.StandardImgHead).Length(200); 
            Map(m => m.Domain).Length(15);
            Map(m => m.Email).Length(30);
            Map(m => m.Card).Length(20); 
            Map(m => m.UserSex).CustomType<UserSex>(); 
            Map(m => m.UserState).CustomType<UserState>(); 
            Map(m => m.Phone).Length(15);  
            Map(m => m.Paw);
            Map(m => m.RealName).Length(25);
            Map(m => m.EnRealName).Length(25);
            Map(m => m.EmailValidate);
            Map(m => m.QQ).Length(30);
            Map(m => m.WeiBoUrl);
            Map(m => m.BlogName).Length(50);
            Map(m => m.BlogDesc);
            Map(m => m.CreateDate); 
            Map(m => m.LastDateTime);
            Map(m => m.LoginDate);
            Map(m => m.LastLoginDate);
            Map(m => m.LoginIp).Length(15);
            Map(m => m.LastLoginIp).Length(15);
            Map(m => m.DesPassword); 
            HasMany(x => x.UserTourLogs);
            HasMany(x => x.Articles); 
        } 
    }
}
