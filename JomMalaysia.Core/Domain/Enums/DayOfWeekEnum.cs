namespace JomMalaysia.Core.Domain.Enums
{
    public class DayOfWeekEnum : EnumerationBase
    {
        public static DayOfWeekEnum Sunday = new DayOfWeekEnum(0, nameof(Sunday));
        public static DayOfWeekEnum Monday = new DayOfWeekEnum(1, nameof(Monday));
        public static DayOfWeekEnum Tuesday = new DayOfWeekEnum(2, nameof(Tuesday));
        public static DayOfWeekEnum Wednesday = new DayOfWeekEnum(3, nameof(Wednesday));
        public static DayOfWeekEnum Thursday = new DayOfWeekEnum(4, nameof(Thursday));
        public static DayOfWeekEnum Friday = new DayOfWeekEnum(5, nameof(Friday));
        public static DayOfWeekEnum Saturday = new DayOfWeekEnum(6, nameof(Saturday));
        public DayOfWeekEnum(int id, string name) : base(id, name)
        {

        }

        public static DayOfWeekEnum For(string enumstring)
        {

            return Parse<DayOfWeekEnum>(enumstring);
        }

        public int ToInt()
        {
            return Id;
        }
    }
}