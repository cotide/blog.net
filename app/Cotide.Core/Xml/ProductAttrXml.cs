//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductAttrXml.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 17:07:16 
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
using System.Xml.Serialization;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Xml
{
    /// <summary>
    /// 商品业务属性XML
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "")]
    [XmlRoot("ProductAttrXml", IsNullable = false, Namespace = "")]
    public class ProductAttrXml
    {
        [XmlElement("ProductAttrItems")]
        public List<ProductAttrItmXml> ProductAttrItems { get; set; }
    }

    /// <summary>
    /// 商品业务属性项
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "")]
    [XmlRoot("ProductAttrItem", IsNullable = false, Namespace = "")]
    public class ProductAttrItmXml
    {
        /// <summary>
        /// 属性唯一标识
        /// </summary>
        [XmlElement("Key")]
        public string Key { get; set; } 

        /// <summary>
        /// 属性显示名称
        /// </summary>
        [XmlElement("DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [XmlElement("FieldName")]
        public string FieldName { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>

        [XmlElement("AttrType")]
        public ProductAttrType AttrType { get; set; }

        /// <summary>
        /// 排序
        /// </summary> 
        [XmlElement("Sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 商品业务属性值
        /// </summary>
        public List<ProductAttrItmValueXml> ProductAttrItmValue { get; set; }
    }

    /// <summary>
    /// 商品业务属性项值
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "")]
    [XmlRoot("ProductAttrItmValueXml", IsNullable = false, Namespace = "")]
    public class ProductAttrItmValueXml
    {
        /// <summary>
        /// 属性唯一标识
        /// </summary>
        [XmlElement("Key")]
        public string Key { get; set; } 

        /// <summary>
        /// 商品业务属性值
        /// </summary>
        [XmlElement("Value")]
        public string Value { get; set; }

        /// <summary>
        /// 商品业务属性值图片
        /// </summary> 
        [XmlElement("Img")]
        public string Img { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [XmlElement("Sort")]
        public int Sort { get; set; }
    }

}
