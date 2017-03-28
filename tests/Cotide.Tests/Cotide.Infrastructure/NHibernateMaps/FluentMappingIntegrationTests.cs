#region using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cotide.Framework.Attr.Desc;
using Cotide.Infrastructure.NHibernateMaps;
using Cotide.Infrastructure.NHibernateMaps.Conventions;
using NHibernate.Cfg;
using NHibernate.Mapping;
using NHibernate.Persister.Entity;
using NHibernate.SqlTypes;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SharpArch.NHibernate;
using Tests.SharpArch.NHibernate;

#endregion

namespace Tests.Cotide.Infrastructure.NHibernateMaps
{
    /// <summary>
    ///     Provides a means to verify that the target database is in compliance with all mappings.
    ///     Taken from http://ayende.com/Blog/archive/2006/08/09/NHibernateMappingCreatingSanityChecks.aspx.
    /// 
    ///     If this is failing, the error will likely inform you that there is a missing table or column
    ///     which needs to be added to your database.
    /// </summary>
    [TestFixture]
    [Category("DB Schema Tests")]
    public class FluentMappingIntegrationTests
    {
        #region Setup/Teardown

        [SetUp]
        public virtual void SetUp()
        {
            var mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            _nhConfig = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                                               new AutoPersistenceModelGenerator().Generate(),
                                               TEST_DB_CONFIG_PATH);
        }

        [TearDown]
        public virtual void TearDown()
        {
            NHibernateSession.CloseAllSessions();
            NHibernateSession.Reset();
        }

        #endregion

        private static SchemaUpdate PrepareSchemaUpdate()
        {
            return new SchemaUpdate(_nhConfig);
        }

        private static SchemaExport PrepareSchemaExport()
        {
            return new SchemaExport(_nhConfig);
        }

        private const string TEST_DB_CONFIG_PATH = "../../../../tests/Cotide.Tests/Hibernate.cfg.xml";
         

        [Test]
        public void ExportSchemaToFile()
        {
            PrepareSchemaExport().Execute(true, false, false, null, new StreamWriter(DB_FOLDER_PATH + "schema/DbSchema.sql"));
        }

        [Test]
        public void UpdateLiveDbSchema()
        {
            PrepareSchemaUpdate().Execute(true, false);
        }

        [Test]
        public void WriteMappingsToFile()
        {
            var mappings = new AutoPersistenceModelGenerator().Generate();
            mappings.BuildMappings();
            mappings.WriteMappingsTo(DB_FOLDER_PATH + "maps/");
        }

        /// <summary>
        /// 文件输出目录
        /// </summary>
        private const string DB_FOLDER_PATH = "../../../../db/";

        /// <summary>
        /// 领域对象DLL名
        /// </summary>
        private const string DomainDll = "Cotide.Domain";

        /// <summary>
        /// NHibernate Configuration
        /// </summary>
        private static NHibernate.Cfg.Configuration _nhConfig;

        [Test]
        public void WriteDomainDbInfo()
        {
            // 获取NHibernate 获取领域实体元数据集合 
            // 通过NHibernateSession静态类 -> 获取默认工厂 -> 获取所有领域实体元数据
            var allClassMetadata = NHibernateSession.
                GetDefaultSessionFactory().GetAllClassMetadata();

            // 输出内容变量
            var str = new StringBuilder();
            // 遍历元数据
            foreach (var entry in allClassMetadata)
            {
                // 获取当前元数据
                var entity = ((SingleTableEntityPersister)entry.Value);
                // 获取实体映射的表名
                var tableName = entity.TableName;
                // 领域实体名称
                var entityName = entity.EntityName; 
                // 领域实体Mapping对象
                var userClassMap = _nhConfig.GetClassMapping(entityName); 
                // 领域实体主键名
                var primaryKey = userClassMap.Table.PrimaryKey.Columns.FirstOrDefault().Text;
                // 领域实体下的属性列表
                var column = userClassMap.Table.ColumnIterator;

                // 输出表头
                str.Append(string.Format("<H1>{0}({1})</H1>", tableName, entityName));

                // 获取实体描述
                // 使用反射dll -> 获取EntityDescAttribute特性描述内容
                var type = Type.GetType(entityName + "," + DomainDll);
                if (type != null)
                {
                    var attr = type.GetCustomAttributes(typeof(EntityDescAttribute), true);
                    if (attr.Length > 0)
                    {
                        str.Append(string.Format("<P>表描述:{0}</P>",
                            ((EntityDescAttribute)attr[0]).Description));
                    }
                }
                 
                // 输出表格表头
                str.Append("<table width=\"100%\" " +
                           "border=\"1\" " + 
                           "cellspacing=\"0\" " +
                           "bordercolor=\"#333\" >");
                str.Append("<tr style='background-color:#3AF;'>" +
                           "<td>序号</td>" +
                           "<td>数据库字段</td>" +
                           "<td>类型</td>" +
                           "<td>描述</td>" +
                           "<td>唯一约束</td>" +
                           "<td>允许为NULL</td>" +
                           "<td>是否主键</td>" +
                           "</tr>");
                // 遍历领域实体下的属性
                // 顺序索引
                var index = 0;
                foreach (var obj in column)
                {
                    // 属性描述
                    var attrDesc = "";

                    // 获取NHibernate Column对象
                    var item = (Column)obj.Value.ColumnIterator.FirstOrDefault();
                    if (item == null)
                        continue;

                    // 判断如果为领域实体唯一标识则不获取属性描述
                    if (primaryKey == item.Text)
                    {
                        //唯一标识
                        attrDesc = "唯一标识";
                    } 
                    else
                    {
                        // 如果超出索引的 -> 判断为非数据库字段,为领域实体属性
                        // -> 跳出本次处理
                        if(index >= entity.PropertyNames.Count())
                        {
                            continue;
                        }

                        // 获取属性上的EntityPropertyDescAttribute特性描述内容 
                        var memberItem = entity.PropertyNames[index];
                        if(type!=null)
                        {
                            var property = type.GetProperty(memberItem);
                            var memberAttr = property.GetCustomAttributes(
                                typeof(EntityPropertyDescAttribute), true);

                            if (memberAttr.Length > 0)
                            {
                                attrDesc = ((EntityPropertyDescAttribute)
                                    memberAttr[0]).Description;
                            } 
                        }
                        ++index;
                    }

                    // 输出内容
                    str.Append(string.Format("<tr>" +
                                             "<td>{0}</td>" +
                                             "<td>{1}</td>" +
                                             "<td>{2}</td>" +
                                             "<td>{3}</td>" +
                                             "<td>{4}</td>" +
                                             "<td>{5}</td>" +
                                             "<td>{6}</td>" +
                                             "</tr>",
                          index + 1,
                          item.Text, GetType(item.Value.Type),
                          attrDesc,
                          item.IsUnique ? "Yes" : "No",
                          item.IsNullable ? "Yes" : "No",
                          (primaryKey == obj.Name) ? "Yes" : "No")); 
                }
                str.Append("</table>");
            }
            // 输出数据库字典文件
            // 文件名
            const string outputFileName = "数据库字典.doc";
            // 输出文件夹路径
            string outputFilePath = Path.Combine(DB_FOLDER_PATH, outputFileName);
            // 输出操作
            File.WriteAllText(outputFilePath, str.ToString(), Encoding.GetEncoding("utf-8"));
        }

        #region Helper 

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetType(NHibernate.Type.IType type)
        {
            if (type is NHibernate.Type.Int32Type)
            {
                return "int";
            }
            if (type is NHibernate.Type.StringType)
            {
                var sqlType = ((NHibernate.Type.StringType)type).SqlType;
                return string.Format("nvarchar({0})", GetLength(sqlType));
            }
            if (type is NHibernate.Type.BooleanType)
            {
                return "bit";
            }
            if (type is NHibernate.Type.DateTimeType)
            {
                return "datetime";
            }
            if (type is NHibernate.Type.DecimalType)
            {
                return "decimal";
            }
            if (type is NHibernate.Type.DoubleType)
            {
                return "double";
            }
            if (type is NHibernate.Type.Int64Type)
            {
                return "bitint";
            }
            if (type is NHibernate.Type.CharType)
            {
                return "char";
            }
            if (type is NHibernate.Type.TimestampType)
            {
                return "timestamp";
            }
            if (type is NHibernate.Type.Int16Type)
            {
                return "tinyint";
            }
            if (type is NHibernate.Type.XmlDocType)
            {
                return "xml";
            }
            if (type is NHibernate.Type.BinaryType)
            {
                var sqlType = ((NHibernate.Type.StringType)type).SqlType;
                return string.Format("varbinary({0})", sqlType.Length > 4000 ? "max" : sqlType.Length.ToString());
            }

            if (type is NHibernate.Type.PersistentEnumType)
            {
                var enumType = (NHibernate.Type.PersistentEnumType)type;
                return enumType.SqlType.DbType.ToString() == "Int32" ? "int" : "nvarchar(255)";
            }
            if (type is NHibernate.Type.EntityType)
            {
                // 暂时使用这种方式,以后重构
                return "int";
            }
            return "未知";
        }

        /// <summary>
        /// 获取SQL长度
        /// </summary>
        /// <param name="sqlType">SQL类型</param>
        /// <returns></returns>
        private static string GetLength(SqlType sqlType)
        {
            return sqlType.Length > 4000 ? "max" : sqlType.Length <= 0 ? "255" : sqlType.Length.ToString();
        }

        #endregion

    }
}