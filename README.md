# ExcelPatternTool

Excel表格-数据库互导工具

## 介绍
指定Pattern文件-一个规则描述的json文档，基于此规则实现Excel表格与数据库之间的导入导出，校验等功能。使用场景有：Excel导入至数据库、Excel转Excel（合并，校验，规范化）、数据库导出至Excel（报表生成）等。

本工具提供其他版本：

[ExcelPatternTool with UI]( https://github.com/jevonsflash/ExcelPatternTool-UI)

[ExcelPatternTool.Core]( https://github.com/jevonsflash/ExcelPatternTool/tree/master/ExcelPatternTool.Core)

## 特点
1. 小巧，轻量化的命令行工具
2. 基于json文档的配置
3. 支持Excel97-2003(xls)与Excel2007及以上(xlsx)格式
4. 数据库支持SQL server、Sqlite、MySql
5. 支持单元格注解，样式，公式的导出(导出至Excel)
6. 内置lambda表达式和正则表达式两种校验器

## 更新内容

Date | Version | Content
:----------: | :-----------: | :-----------
V0.1.0         | 2022-7-29     | 初始版本
V0.1.1         | 2022-8-3     | 1. 新增数据库导入 2. 减小程序包体积
V0.2.0         | 2023-10-19     | 1. 更新引用库，使用SixLabors.ImageSharp替换System.Drawing以兼容Linux 2. 升级项目框架到 .Net 7.0

## 快速开始

### 编写Pattern文档

1. 导入规则编写

* 指定表格的工作表名称SheetName或者工作表序号SheetNumber，二者选一配置即可，SheetName优先，SheetNumber从0开始
* 指定开始行数SkipRow，这个是实际数据的开始行数，不包含表头行。在Sample中，这个行数为3

Sample：
```
"ExcelImport": {			// excel导入规则
    "SheetName": "",		// 工作表名称
    "SheetNumber": 0,       // 工作表序号
    "SkipRow": 3           // 开始行数
  }
```
2. 导出规则编写
* 指定数据库表的名称，主键类型。数据库类型将在Cli参数中指定

Sample：
```
"DbExport": {               // Db导出规则
    "TableKeyType": "Guid", // 表主键类型 可选 "无"，"int"，"long"，"Guid"，
    "TableName": "Employee" // 表名称
  }
```
3. Pattern配置

 对列进行配置

 * 列指定列标题名称，属性名称，类型和排序
 * 单元格类型为普通类型是"常规"时，直接输出的为单元格值，"包含注解"，"包含样式"，"包含公式"，"全包含"仅对导出至Excel有效
* Ignore 为True时将忽略这一列，等效于无此列的Pattern设置
* 列序号为此列在Excel中的编号，从0开始，即A列对应0，B列对应1 ...
* 列属性类型PropType为bool时，可支持0，1，True，False

Sample：
```

"Patterns": [                       // Pattern配置
    {
      "PropName": "EmployeeName",   // 属性名称
      "HeaderName": "姓名",         // 列标题名称
      "PropType": "string",         // 属性类型，可选 "string"， "DateTime"，"int"，"double"，"bool"，
      "CellType": "常规",           // 单元格类型 可选 "常规"，"包含注解"，"包含样式"，"包含公式"，"全包含"
      "Ignore": false,              // 是否忽略
      "Order": 0,                   // 列序号
      "Validation": {               // 校验配置
       ...
      }
    },
```
配置校验

* 配置Target，可对单元格值或单元格公式进行校验
* 普通校验器时，{value}占位符代表当前单元格值或公式的内容
* Sample1为普通校验器，校验单元格数值，Sample2为正则校验器，校验单元格公式

Sample1：
```  
    
      "Validation": {
        "Target": "单元格数值",
        "Description": "整数值需要大于2",
        "Convention": "普通校验器",
        "Expression": "{value}>=2"
      }
    
```
Sample2：
```  
    
      "Validation": {
        "Target": "单元格公式",
        "Description": "需要满足正则表达式",
        "Convention": "正则表达式校验器",
        "Expression": "^ROUND\\(AN\\d+\\+BC\\d+\\+BD\\d+\\+BE\\d+\\+BF\\d+\\+BG\\d+\\+BH\\d+,2\\)$"
      }
    
```

完整示例请参考 [Sample](https://github.com/jevonsflash/ExcelPatternTool/raw/master/EPT/sample/pattern.json)

### 安装

不需要特别的安装，直接运行可执行文件即可
* 直接下载
，在此获取[ept.exe](https://github.com/jevonsflash/ExcelPatternTool/releases/download/v0.1.2/ExcelPatternTool.exe)，

或
```
git clone https://github.com/jevonsflash/ExcelPatternTool.git
```

```
cd .\ExcelPatternTool
```
```
dotnet publish -p:PublishSingleFile=true -r win-x64 -c Release --self-contained true -p:EnableCompressionInSingleFile=true
```


代码后`生成`可执行文件
```
ExcelPatternTool\bin\Release\net6.0\win-x64\publish\ExcelPatternTool.exe
```

### 运行
1. 进入可执行文件所在目录，并运行
* 若要导出至Sqlite，请确保相同目录下包含`e_sqlite3.dll`
* 若要导出至SQL server，请确保相同录下包含`Microsoft.Data.SqlClient.SNI.dll`

参数列表:

参数 | 含义 | 用法
:----------: | :-----------: | :-----------
 -p | PatternFile | 指定一个Pattern文件(Json), 作为转换的模型文件
 -i | Input | 指定一个路径，或Sql连接字符串作为导入目标<br>当指定 -s 参数为`sqlserver`, `sqlite`, `mysql`时，需指定为连接字符串;<br>当指定 -s 参数为`excel`时，需指定为将要读取的Excel文件路径，支持Xls或者Xlsx文件
 -o | Output | 指定一个路径，或Sql连接字符串作为导出目标<br>当指定 -d 参数为`sqlserver`, `sqlite`, `mysql`时，需指定为连接字符串;<br>当指定 -d 参数为`excel`时，需指定为将要另存的Excel文件路径，支持Xls或者Xlsx文件
 -s | Source | 值为`excel`, `sqlserver`, `sqlite`或者`mysql`
 -d | Destination  | 值为`excel`, `sqlserver`, `sqlite`或者`mysql`
 -w | WaitAtEnd  | 指定时，程序执行完成后，将等待用户输入退出
 -h | Help | 查看帮助


导出至Sqlite的Sample
```
.\ept.exe -p .\sample\pattern.json -i .\sample\test.xlsx -o "Data Source=mato.db" -s excel -d sqlite
```
导出至Excel的Sample
```
.\ept.exe -p .\sample\pattern.json -i .\sample\test.xlsx -o .\sample\output.xlsx -s excel -d excel
```

2. 等待程序执行完毕
![ss1](https://github.com/jevonsflash/ExcelPatternTool/blob/master/EPT/screenshots/1.png)

### 结果

将在-o 参数指定的地址生成数据
生成至Excel

![ss1](https://github.com/jevonsflash/ExcelPatternTool/blob/master/EPT/screenshots/2.png)


生成至Sqlite

![ss1](https://github.com/jevonsflash/ExcelPatternTool/blob/master/EPT/screenshots/3.png)


## 其他
### 配置

ept.exe 相同目录下新建`appsettings.json`可自定义配置，若无此文件将采用自定义样式配置，如下：

```
{
  "HeaderDefaultStyle": {
    "DefaultFontName": "宋体",
    "DefaultFontColor": "#FFFFFF",
    "DefaultFontSize": 10,
    "DefaultBorderColor": "#000000",
    "DefaultBackColor": "#888888"
  },
  "BodyDefaultStyle": {
    "DefaultFontName": "宋体",
    "DefaultFontColor": "#000000",
    "DefaultFontSize": 10,
    "DefaultBorderColor": "#000000",
    "DefaultBackColor": "#FFFFFF"
  },
  "CellComment": {
    "DefaultAuthor": "Linxiao"

  }
}
```
### 可扩展性

检验提供类ValidatorProvider类具有一定的扩展功能，
InitConventions方法对校验行为进行初始化，默认提供RegularExpression，LambdaExpression对应的委托函数分别实现了正则表达式校验和普通表达式校验，重写InitConventions可实现一个自定义方式校验

Sample：

```
public override Dictionary<string, ValidateConvention> InitConventions()
{

    var defaultConventions = base.InitConventions();
    //x 为当前列轮询的字段规则PatternItem对象，
    //e 为当前行轮询的Entity对象
    //返回ProcessResult作为校验结果
    defaultConventions.Add("MyExpression", new ValidateConvention((x, e) =>
    {
        //再此编写自定义校验功能
        //可用 x.PropName（或PropertyTypeMaper(x.PropName)） 获取当前列轮询的字段（Excel表头）名称
        //返回ProcessResult作为校验结果,IsValidated设置为true表示校验通过
        x.Validation.ProcessResult.IsValidated = true;
        return x.Validation.ProcessResult;
    }));

    return defaultConventions;
}
```
## Todo

- [x] 从数据库导入
- [x] ept带UI版本 [前往WPF版]( https://github.com/jevonsflash/ExcelPatternTool-UI)
- [ ] 校验过程的忽略与单独使用


## 工具

[Roslyn Syntax Tool](https://github.com/jevonsflash/RoslynSyntaxTool)

* 此工具能将C#代码，转换成使用语法工厂构造器（SyntaxFactory）生成等效语法树代码


## 已知问题



## 作者信息

作者：林小

邮箱：jevonsflash@qq.com



## License

The MIT License (MIT)
