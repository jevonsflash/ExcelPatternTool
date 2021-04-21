namespace Workshop.Model.Dto
{
    public class ProcessResultDto
    {
        public int Id { get; set; }
        public string Position => $"{Row},{Column}";
        public int Level { get; set; }
        public string Content { get; set; }

        public string Column { get; set; }
        public int Row { get; set; }

    }
}