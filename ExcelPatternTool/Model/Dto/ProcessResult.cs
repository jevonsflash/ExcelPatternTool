namespace ExcelPatternTool.Model.Dto
{
    public class ProcessResultDto
    {
        public int Id { get; set; }
        public string Position => $"第{Row}行,第{Column}列， 列名称:{KeyName}";
        public int Level { get; set; }
        public string Content { get; set; }

        public string KeyName { get; set; }

        public long Row { get; set; }
        public int Column { get; set; }


    }
}