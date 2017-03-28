# Cotide.Blog
>  2011年初用的领域驱动设计驱动的设计的BLOG网站  

## 使用技术
设计思路：领域驱动设计
数据库存储：Nhibernate(ORM) + SQL Server
前台处理：MVC4 + jQuery

## 实体模型
![实体模型](http://ww1.sinaimg.cn/large/7c2c6ab7gy1fe2om4yc96j20j70l2t9n.jpg)

## 文件结构说明
* /app/ 程序文件
* /app/Cotide.Framework/ 公共类库层
* /app/Cotide.Core/ 实体层
* /app/Cotide.Portal/ 表示层
* /app/Cotide.Web.Controllers/ 表示层-控制器
* /app/Cotide.ApplicationServices/ 业务逻辑层 - 持久化
* /app/Cotide.QueryServices/  业务逻辑层 - 查询
* /app/Cotide.Data/ 基础设施层
* /app/Design/ 设计图
* /app/T4/ T4模板
* /db/ 数据库初始化脚本
* /lib/ 引用的第三方的dll (以前没用Nuget的原因)
* /tests/ 单元测试

## 参考资料
* [Nhibernate](https://github.com/nhibernate/nhibernate-core)
* [Fluent Nhibernate](https://github.com/jagregory/fluent-nhibernate)
* [Sharp-Architecture](https://github.com/sharparchitecture/Sharp-Architecture)
