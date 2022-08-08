# ExcelPatternTool.Core

开箱即用的应用程序包，不需要编写pattern.json，指定一个C#泛型类即可立即开始



## 快速开始

在项目中引用[ExcelPatternTool.Core]( https://www.nuget.org/packages/ExcelPatternTool.Core)


```
dotnet add package ExcelPatternTool.Core
```

## 使用说明

编辑你的C#类，此类将作为ExcelPatternTool导入导出功能的承载实体类型，继承自`IExcelEntity`

### 常规类型 

可定义 `string`， `DateTime`，`int`，`double`，`bool`

### EF标签
1. 若涉及数据库导入导出，请使用`Table`标签指定表名称， 使用`Key`标签指定主键类型，无键实体类型请使用`Keyless`
详情请参考EFCore官方文档https://docs.microsoft.com/zh-cn/ef/core/modeling/

### ImportableAttribute
1. Order 列序号为此列在Excel中的编号，从0开始，即A列对应0，B列对应1 ...

2. Ignore 为True时将忽略这一列，等效于ExcelEntity无此属性

### ExportableAttribute
1. Order 列序号为此列在Excel中的编号，从0开始，即A列对应0，B列对应1 ...

2. Name 列名称，将指定导出时的该列第一行名称

3. Ignore 为True时将忽略这一列，等效于ExcelEntity无此属性

4. Format 指定单元格格式，对应的值有

    ```
    "General",
    "0",
    "0.00",
    "#,##0",
    "#,##0.00",
    "\"$\"#,##0_);(\"$\"#,##0)",
    "\"$\"#,##0_);[Red](\"$\"#,##0)",
    "\"$\"#,##0.00_);(\"$\"#,##0.00)",
    "\"$\"#,##0.00_);[Red](\"$\"#,##0.00)",
    "0%",
    "0.00%",
    "0.00E+00",
    "# ?/?",
    "# ??/??",
    "m/d/yy",
    "d-mmm-yy",
    "d-mmm",
    "mmm-yy",
    "h:mm AM/PM",
    "h:mm:ss AM/PM",
    "h:mm",
    "h:mm:ss",
    "m/d/yy h:mm",
    "#,##0_);(#,##0)",
    "#,##0_);[Red](#,##0)",
    "#,##0.00_);(#,##0.00)",
    "#,##0.00_);[Red](#,##0.00)",
    "_(\"$\"* #,##0_);_(\"$\"* (#,##0);_(\"$\"* \"-\"_);_(@_)",
    "_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)",
    "_(* #,##0.00_);_(* (#,##0.00);_(* \"-\"??_);_(@_)",
    "_(\"$\"* #,##0.00_);_(\"$\"* (#,##0.00);_(\"$\"* \"-\"??_);_(@_)",
    "mm:ss",
    "[h]:mm:ss",
    "mm:ss.0",
    "##0.0E+0",
    "@"
    ```
   

5. Type: 单元格类型， Exportable中可指定Type类型的为

    值 | 含义 
    :----------: | :-----------:
    `Any`        | 自定义     
    `Text`       | 文本     
    `Numeric`    | 数值     
    `Date`       | 时间     
    `Bool`       | 布尔值     
  
### 高级类型

ExcelEntity中的属性为常规类型时，直接输出的为单元格值；
属性为AdvancedType类型时，导入时将读取单元格细节，导出至Excel时，将保留这些细节导出。
高级类型有："包含注解"，"包含样式"，"包含公式"，"全包含"类型

1. CommentedType<>: 包含注解的对象，泛型中的类型同"常规类型"
2. StyledType<>: 包含样式的对象，泛型中的类型同"常规类型"
3. FormulatedType<>: 包含公式的对象，泛型中的类型同"常规类型"
4. FullAdvancedType<>: 全包含对象，泛型中的类型同"常规类型"

Sample：
```
    [Keyless]
    [Table("EmployeeEntity")]
    public class EmployeeEntity : IExcelEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Exportable(ignore: true)]
        public Guid Id { get; set; }

        [Exportable(ignore: true)]
        [Importable(ignore: true)]
        public long RowNumber { get; set; }
        [DisplayName("常规")]
        [Exportable("常规")]
        [Importable(0)]
        public string StringValue { get; set; }
        [DisplayName("日期")]
        [Exportable("日期")]
        [Importable(1)]
        public DateTime DateTimeValue { get; set; }

        [DisplayName("整数")]
        [Exportable("整数")]
        [Importable(2)]
        public int IntValue { get; set; }

        [DisplayName("小数")]
        [Exportable("小数")]
        [Importable(3)]
        public double DoubleValue { get; set; }


        [DisplayName("布尔值")]
        [Exportable("布尔值")]
        [Importable(4)]
        public bool BoolValue { get; set; }

        [DisplayName("常规(注释)")]
        [Exportable("常规(注释)")]
        [Importable(5)]
        public CommentedType<string> StringWithNoteValue { get; set; }

        [DisplayName("常规(样式)")]
        [Exportable("常规(样式)")]
        [Importable(6)]
        public StyledType<string> StringWithStyleValue { get; set; }


        [DisplayName("公式")]
        [Exportable("公式")]
        [Importable(10)]
        public FormulatedType<int> IntWithFormula { get; set; }
    }

```


## 工具

[Roslyn Syntax Tool](https://github.com/MatoApps/RoslynSyntaxTool)

* 此工具能将C#代码，转换成使用语法工厂构造器（SyntaxFactory）生成等效语法树代码


## 已知问题



## 作者信息

作者：林小

邮箱：jevonsflash@qq.com



## License

The MIT License (MIT)
