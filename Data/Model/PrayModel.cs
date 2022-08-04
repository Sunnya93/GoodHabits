namespace GoodHabits.Data.Model
{
    public enum PrayType { Praise, Thanksgiving, Supplication, Petition }

    public class PrayModel
    {
        public long? Id { get; set; }
        public PrayType Type { get; set; }
        public string? Content { get; set; }
        public DateTime PrayDate { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
