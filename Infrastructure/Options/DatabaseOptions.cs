namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Options
{
    public class DatabaseOptions
    {
        public string MemoryRO { get; init; }
        public string MemoryRW { get; init; }

        internal const string SectionName = "Database";

    }
}
