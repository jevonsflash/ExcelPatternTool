namespace Workshop.Infrastructure.Models
{
    public class ProcessResult
    {
        public int Id { get; set; }
        public string Position => $"第{Row}行, 列名称:{Column}";
        public int Level { get; set; }
        public string Content { get; set; }

        public string Column { get; set; }
        public int Row { get; set; }

        public bool IsValidated { get; set; }

    }
}