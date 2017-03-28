using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cotide.Framework.Attr.Desc;
using Cotide.Infrastructure.NHibernateMaps;
using NHibernate.Cfg;
using NHibernate.Mapping;
using NHibernate.Persister.Entity;
using NHibernate.Tool.hbm2ddl;
using SharpArch.NHibernate;
using Tests.SharpArch.NHibernate;

namespace DBGenerate
{
    public partial class Form1 : Form
    {
        private static Configuration _nhConfig;
        private string dll = "Cotide.Domain";
        private string _fileName;
        private string _dllFileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = @"文本文件(*.config)|*.config" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _fileName = openFileDialog.FileName;
                textBox1.Text = _fileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = @"文本文件(*.dll)|*.dll" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _dllFileName = openFileDialog.FileName;
                textBox2.Text = _dllFileName;
            }
        }

        private static SchemaExport PrepareSchemaExport()
        {
            return new SchemaExport(_nhConfig);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            _nhConfig = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                                               new AutoPersistenceModelGenerator().Generate(),
                                               _fileName);

            var allClassMetadata = NHibernateSession.GetDefaultSessionFactory().GetAllClassMetadata();
            var str = new StringBuilder(""); 
            foreach (var entry in allClassMetadata)
            {

                str.Append("<H1>");
                // 输出表名
                var entity = ((SingleTableEntityPersister)entry.Value);
                var tableName = entity.TableName; ;
                str.Append(tableName+"("+entry.Value.EntityName+")");
                str.Append("</H1>"); 
                var type = Type.GetType(entry.Value.EntityName + "," + dll);
                var attr = type.GetCustomAttributes(typeof(EntityDescAttribute), true);
                if (attr.Length > 0)
                {
                    str.Append("<P>表描述:");
                    Console.WriteLine("表描述:" + ((EntityDescAttribute)attr[0]).Description);
                    str.Append("</P>");
                }

                PersistentClass userClassMap = _nhConfig.GetClassMapping(entry.Key);
                IEnumerable<Column> column = userClassMap.Table.ColumnIterator;  
                var index = 0;
                str.Append("<table>");
                str.Append("<tr><td>序号</td><td>字段</td><td>类型</td><td>描述</td><td>是否唯一</td><td>是否允许为NULL</td><td>是否主键</td></tr>");
                foreach (var column1 in column)
                {  
                    var item = (NHibernate.Mapping.Column)column1.Value.ColumnIterator.FirstOrDefault();
                    //Console.WriteLine(string.Format("属性值:{0}|SQL类型:{1}|是否唯一:{2}|是否允许为NULL:{3}|是否主键：{4}",
                    //    item.Text,
                    //    GetType(item.Value.Type),
                    //    item.IsUnique
                    //    , item.IsNullable,
                    //    column1.Value.Table.PrimaryKey.Columns.Where(x => x.Name == column1.Name).Count() > 0 ? "是" : "否")); 
                    if (index == 0)
                    {
                        str.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>",
                            index, 
                            item.Text,GetType(item.Value.Type),
                            "",
                            item.IsUnique,
                            item.IsNullable,
                            column1.Value.Table.PrimaryKey.Columns.Where(x => x.Name == column1.Name).Count() > 0 ? "是" : "否"));
                        ++index;
                        continue;
                    }
                        
                    str.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>",
                          index,
                          item.Text, GetType(item.Value.Type),
                          GetPropertyDesc(entity, index, type),
                          item.IsUnique,
                          item.IsNullable,
                          column1.Value.Table.PrimaryKey.Columns.Where(x => x.Name == column1.Name).Count() > 0 ? "是" : "否"));
                }

                str.Append("</table>"); 
            }

            textBox3.Text = str.ToString();
        }

    

        #region Helper

        private static string GetPropertyDesc(SingleTableEntityPersister entity, int index, Type type)
        {
            var memberItem = entity.PropertyNames[index - 1];
            var property = type.GetProperty(memberItem);
            var memberAttr = property.GetCustomAttributes(typeof(EntityPropertyDescAttribute), true);
            if (memberAttr.Length > 0)
            {
                return  ((EntityPropertyDescAttribute)memberAttr[0]).Description;
            }
            return "";
        }

        private string GetType(NHibernate.Type.IType type)
        {
            if (type is NHibernate.Type.Int32Type)
            {
                return "int";
            }
            if (type is NHibernate.Type.StringType)
            {
                var sqlType = ((NHibernate.Type.StringType)type).SqlType;
                return string.Format("nvarchar({0})", sqlType.Length > 4000 ? "max" : sqlType.Length.ToString());
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

            return "未知";
        }

        #endregion
    }
}
