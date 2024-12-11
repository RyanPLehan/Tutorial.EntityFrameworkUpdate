namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Options
{
    public class DatabaseOptions
    {
        public string ReadOnly { get; init; }
        public string ReadWrite { get; init; }

        internal const string SectionName = "Database";

    }
}
