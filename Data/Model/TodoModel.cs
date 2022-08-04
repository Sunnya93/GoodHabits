namespace GoodHabits.Data.Model
{
    public class TodoModel
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime TodoDate { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
