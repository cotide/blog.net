using System;
using System.Collections.Generic;
using Cotide.Domain;
using Cotide.Framework.Attr.Desc;
using Cotide.Infrastructure.NHibernateMaps;
using Cotide.Infrastructure.NHibernateMaps.Conventions;
using NHibernate;
using NHibernate.Mapping;
using NHibernate.Persister.Entity;
using NUnit.Framework;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using SharpArch.NHibernate;
using Tests.SharpArch.NHibernate;
using System.Linq;
using System.Data;

namespace Tests.Cotide.Infrastructure.NHibernateMaps
{
    /// <summary>
    /// Provides a means to verify that the target database is in compliance with all mappings.
    /// Taken from http://ayende.com/Blog/archive/2006/08/09/NHibernateMappingCreatingSanityChecks.aspx.
    /// 
    /// If this is failing, the error will likely inform you that there is a missing table or column
    /// which needs to be added to your database.
    /// </summary>
    [TestFixture]
    [Category("DB Tests")]
    public class MappingIntegrationTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            configuration = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                                   new AutoPersistenceModelGenerator().Generate(),
                                   "../../../../app/Cotide.Web/NHibernate.config");
        }

        [TearDown]
        public virtual void TearDown()
        {
            NHibernateSession.CloseAllSessions();
            NHibernateSession.Reset();
        }

        [Test]
        public void AllNHibernateMappingAreOkay()
        {
           
            PersistentClass userClassMap = configuration.GetClassMapping(typeof(User)); 
            IEnumerable<Column> column = userClassMap.Table.ColumnIterator;
            foreach (var column1 in column)
            {
                var item = column1.Value.ColumnIterator.FirstOrDefault().Text;
                Console.WriteLine(item);
            } 
        }

        [Test]
        public void CanConfirmDatabaseMatchesMappings()
        {
            string dll = "Cotide.Domain";

            var allClassMetadata = NHibernateSession.GetDefaultSessionFactory().GetAllClassMetadata();

            foreach (var entry in allClassMetadata)
            {

                 var entity = ((SingleTableEntityPersister) entry.Value);
                 var tableName = entity.TableName;;
                 Console.WriteLine("表名:"+tableName);
                 Console.WriteLine("实体:" + entry.Value.EntityName);
                 var type = Type.GetType(entry.Value.EntityName + "," + dll);
                var  attr = type.GetCustomAttributes(typeof (EntityDescAttribute), true);
                   if(attr.Length>0)
                   {
                       Console.WriteLine("表描述:" + ((EntityDescAttribute)attr[0]).Description);
                   }
                 
                 PersistentClass userClassMap = configuration.GetClassMapping(entry.Key);
                 IEnumerable<Column> column = userClassMap.Table.ColumnIterator;


                var index = 0;
                 foreach (var column1 in column)
                 {
                        if(index==0)
                        {
                            ++index;
                            continue;
                        }
                        var memberItem = entity.PropertyNames[index-1]; 
                        var property = type.GetProperty(memberItem);
                        var memberAttr = property.GetCustomAttributes(typeof(EntityPropertyDescAttribute), true);
                        if (memberAttr.Length > 0)
                        {
                            Console.WriteLine("属性描述:" + ((EntityPropertyDescAttribute)memberAttr[0]).Description);
                        }


                        var item = (NHibernate.Mapping.Column)column1.Value.ColumnIterator.FirstOrDefault();
                        Console.WriteLine(string.Format("属性值:{0}|SQL类型:{1}|是否唯一:{2}|是否允许为NULL:{3}|是否主键：{4}",
                            item.Text,
                            GetType(item.Value.Type),
                            item.IsUnique
                            , item.IsNullable,
                            column1.Value.Table.PrimaryKey.Columns.Where(x => x.Name == column1.Name).Any() ? "是" : "否"));  
                 }
                 Console.WriteLine("==============================");
                //PersistentClass userClassMap = configuration.GetClassMapping(typeof(entry));
                //IEnumerable<Column> column = userClassMap.Table.ColumnIterator;
                //foreach (var column1 in column)
                //{
                //    var item = column1.Value.ColumnIterator.FirstOrDefault().Text;
                //    Console.WriteLine(item);
                //} 

                //var entityName = entry.Value.EntityName.Split('.');
                //var className = entityName[entityName.Length-1];

                // NHibernateSession.Current.CreateCriteria(entry.Value.GetMappedClass(EntityMode.Poco))
                // .SetMaxResults(0).List();
            }
        }


        public string GetType(NHibernate.Type.IType  type)
        {
             if(type is NHibernate.Type.Int32Type)
             {
                 return "int";
             }
            if(type is NHibernate.Type.StringType)
            { 
                var sqlType = ((NHibernate.Type.StringType) type).SqlType;
                return string.Format("nvarchar({0})",sqlType.Length>4000?"max":sqlType.Length.ToString() );
            }
            if (type is NHibernate.Type.BooleanType)
            {
                return "bit";
            }
            if (type is NHibernate.Type.DateTimeType)
            {
                return "datetime";
            }
            if(type is NHibernate.Type.DecimalType)
            {
                return "decimal";
            }
            if(type is NHibernate.Type.DoubleType)
            {
                return "double";
            }
            if(type is NHibernate.Type.Int64Type)
            {
                return "bitint";
            }
            if(type is NHibernate.Type.CharType)
            {
                return "char";
            }
            if(type is NHibernate.Type.TimestampType)
            {
                return "timestamp";
            }
            if(type is NHibernate.Type.Int16Type)
            {
                return "tinyint";
            }
            if(type is NHibernate.Type.XmlDocType)
            {
                return "xml";
            }
            if(type is NHibernate.Type.BinaryType)
            {
                var sqlType = ((NHibernate.Type.StringType)type).SqlType;
                return string.Format("varbinary({0})", sqlType.Length > 4000 ? "max" : sqlType.Length.ToString());
            }  

            if (type is NHibernate.Type.PersistentEnumType)
            {
                var enumType = (NHibernate.Type.PersistentEnumType) type;
                return enumType.SqlType.DbType.ToString() == "Int32" ? "int" : "nvarchar(255)";
            }
             
            return "未知";
        }

        /// <summary>
        /// Generates and outputs the database schema SQL to the console
        /// </summary>
        [Test]
        public void CanGenerateDatabaseSchema()
        {
            var session = NHibernateSession.GetDefaultSessionFactory().OpenSession();

            using (TextWriter stringWriter = new StreamWriter("../../../../db/schema/UnitTestGeneratedSchema.sql"))
            {
                new SchemaExport(configuration).Execute(true, false, false, session.Connection, stringWriter);
            }
        }

        private Configuration configuration;
    }
}
