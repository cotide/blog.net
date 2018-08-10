# cotide.blog
>  2011年初用的DDD(领域驱动设计)的BLOG网站  

## 特点
* SQL Server 数据库
* Nhibernate + Fluent Nhibernate (ORM)  
* MVC4 + jQuery(前端)

## 实体模型  

![实体模型](http://ww1.sinaimg.cn/large/7c2c6ab7gy1fj5dc9o77tj20dr0f7t9p.jpg)
 
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
