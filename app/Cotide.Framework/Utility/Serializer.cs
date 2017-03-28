#region Using

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Cotide.Framework.Logging;

#endregion

namespace Cotide.Framework.Utility
{
    ///<summary>
    /// 序列化辅助类
    ///</summary>
    public class Serializer
    {
        /// <summary>
        /// 序列化对象为XML字符串
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <returns>对象序列化之后生成的XML字符串</returns>
        public static string WriteObject<T>(T instance)
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(ms, instance);
                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ReadObject<T>(string content) where T : class
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var serializer = new DataContractSerializer(typeof(T));
                var entity = serializer.ReadObject(ms);
                return entity as T;
            }
        } /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <returns>对象序列化之后生成的XML字符串</returns>
        public static string WriteObjectToJson<T>(T instance)
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(ms, instance);
                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ReadObjectForJson<T>(string content) where T : class
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                var entity = serializer.ReadObject(ms);
                return entity as T;
            }
        }
        /// <summary>
        /// 序列化对象为json字符串
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <returns>对象序列化之后生成的json字符串</returns>
        public static string JsonSerializerObject<T>(T instance)
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(ms, instance);
                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 序列化对象为XML字符串
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <returns>对象序列化之后生成的XML字符串</returns>
        public static string ObjectToXML<T>(T instance)
        {
            using (var ms = new MemoryStream())
            {
                var ns = new XmlSerializerNamespaces();

                ns.Add(string.Empty, string.Empty);
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(ms, instance, ns);
                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 序列化对象为XML字符串
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <returns>对象序列化之后生成的XML字符串</returns>
        public static string XmlElementToString(XmlElement instance)
        {
            using (var ms = new MemoryStream())
            {
                var ns = new XmlSerializerNamespaces();

                ns.Add(string.Empty, string.Empty);
                var serializer = new XmlSerializer(typeof(XmlElement));
                serializer.Serialize(ms, instance, ns);
                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 序列化对象为XML字符串
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <returns>对象序列化之后生成的XML字符串</returns>
        public static XmlDocument ObjectToXmlDocument<T>(T instance)
        {
            using (var ms = new MemoryStream())
            {
                var ns = new XmlSerializerNamespaces();

                ns.Add(string.Empty, string.Empty);
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(ms, instance, ns);
                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    var doc = new XmlDocument();
                    doc.Load(reader);
                    return doc;
                }
            }
        }

        /// <summary>
        /// 序列化对象保存到XML文件
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <param name="fileName">保存的文件名</param>
        public static void ObjectToFile(Object instance, string fileName)
        {
            var writer = new StreamWriter(fileName);

            try
            {
                var serializer = new XmlSerializer(instance.GetType());
                serializer.Serialize(writer, instance);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
            }
            finally
            {

                writer.Close();
            }
        }

        /// <summary>
        /// 从文件中将XML字符串反序列化为一个对象
        /// </summary>
        /// <param name="xmlFilePath">保存对象序列化后生成的XML字符串的文件的路径</param>
        /// <param name="t">对象的类型</param>
        /// <returns>对象的实例</returns>
        public static object FileToObject(string xmlFilePath, Type t)
        {
            FileStream readFs = null;
            Object o = null;
            try
            {
                var ser = new XmlSerializer(t);
                readFs = System.IO.File.OpenRead(xmlFilePath);
                var reader = new XmlTextReader(readFs);
                o = ser.Deserialize(reader);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
            }
            finally
            {
                if (readFs != null) readFs.Close();
            }
            return o;
        }

        /// <summary>
        /// 将XML字符串反序列化为一个对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="xml">需要反序列化的XML字符串</param>
        /// <returns>对象的实例</returns>
        public static T XMLToObject<T>(string xml) where T : class
        {
            using (var ms = new MemoryStream())
            {
                using (var sr = new StreamWriter(ms, Encoding.UTF8))
                {
                    sr.Write(xml);
                    sr.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    var serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(ms) as T;
                }
            }
        }

        /// <summary>
        /// 将XML字符串反序列化为一个对象
        /// </summary>
        /// <param name="xml">需要反序列化的XML字符串</param>
        /// <param name="t">对象的类型</param>
        /// <returns>对象的实例</returns>
        public static object XMLToObject(string xml, Type t)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            Object o = null;
            try
            {
                var serializer = new XmlSerializer(t);
                stream = new StringReader(xml);
                reader = new XmlTextReader(stream);
                o = serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return o;
        }

        /// <summary>
        /// 将字符串写入到文件
        /// </summary>
        /// <param name="strInfo">文件内容</param>
        /// <param name="fileName">文件路径</param>
        /// <returns>true-写入成功，false-写入失败</returns>
        public static bool StringToFile(string strInfo, string fileName)
        {
            var writer = new StreamWriter(fileName);
            try
            {
                writer.Write(strInfo);
                writer.Flush();
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
                return false;
            }
            finally
            {
                writer.Close();
            }
        }

        /// <summary>
        /// 将字符串以指定编码格式写入到文件
        /// </summary>
        /// <param name="strInfo">文件内容</param>
        /// <param name="fileName">文件路径</param>
        /// <param name="encodName">编码</param>
        /// <returns>true-写入成功，false-写入失败</returns>
        public static bool StringToFileByEncoding(string strInfo, string fileName, string encodName)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create))
                {
                    using (var writer = new StreamWriter(fs, Encoding.GetEncoding(encodName)))
                    {
                        writer.Write(strInfo);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}