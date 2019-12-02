# 特性
1. 访问SQL Server
1. 多种方式连接数据库服务器
1. 一个方法完成一个事务
1. 直接执行存储过程
1. 按参数及占位符方便构造SQL命令
# 操作
# 语法
+ 构造方法
	+ SqlServerClient(string server="127.0.0.1",string database="master")
		1. SQL Server访问客户端类 构造方法 使用Windows身份登录
		1. server 服务器名 包括实例名 默认连接127.0.0.1
        1. database 数据库名 默认连接master
	+ SqlServerClient(string server,string user,string password)
		1. server 服务器名 包括实例名
		1. user 用户名
		1. password 密码
	+ SqlServerClient(string server,string database,string user,string password)
		1. SQL Server访问客户端类 构造方法 使用SQL Server身份登录
        1. server 服务器名 包括实例名
        1. database 数据库名
		1. user 用户名
        1. password 密码
+ 执行SQL命令
	+ DataTable GetDataTable(string sql,params object[] args)
        1. 使用SQL命令查询一个数据表
        1. sql SQL命令文本，{0}、{1}为参数占位符
        1. args 数量可变的参数数组
        1. 返回查询结果 数据表
    + GetDataSet(string sql,params object[] args)
        1. 使用SQL命令查询查询多个数据表
        1. sql SQL命令文本，{0}、{1}为参数占位符
        1. args 数量可变的参数数组
        1. 返回查询结果 数据集
	+ GetValue(string sql,params object[] args)
        1. 使用SQL命令查询一个标量值
        1. sql SQL命令文本，{0}、{1}为参数占位符
        1. args 数量可变的参数数组
        1. 返回查询结果 标量值
	+ Execute(string sql,params object[] args)
		1. 执行SQL命令
        1. sql SQL命令文本，{0}、{1}为参数占位符
        1. args 数量可变的参数数组
        1. 返回受影响条数
+ 执行存储过程
	+ GetDataTable(string name, Dictionary<string,object> args)
        1. 使用SQL命令查询一个数据表
        1. name 存储过程名称
        1. args 存储过程参数词典 参数名,参数值
        1. 返回查询结果 数据表
	+ GetDataSet(string name, Dictionary<string, object> args)
		1. 使用SQL命令查询查询多个数据表
        1. name 存储过程名称
        1. args 存储过程参数词典 参数名,参数值
        1. 返回查询结果 数据集
	+ GetValue(string name, Dictionary<string, object> args)
		1. 使用SQL命令查询一个标量值
        1. name 存储过程名称
        1. args 存储过程参数词典
        1. 返回查询结果 标量值
	+ Execute(string name, Dictionary<string, object> args)
		1. 执行SQL命令
        1. name 存储过程名称
        1. args 存储过程参数词典 参数名,参数值
        1. 返回受影响条数
# 备注
[目录](../README.md)  
如发现错误或提建议，请[提交 issue](https://github.com/yxl-net/Yxl.SqlClient/issues)或发送邮件到  
## 996986842@qq.com
请关注公众号了解更多信息  
![NET全栈程序员](https://raw.githubusercontent.com/yxl-net/javascript-packages/master/imgs/%E5%85%AC%E4%BC%97%E5%8F%B7%E5%9B%BE%E7%89%87.jpg)



