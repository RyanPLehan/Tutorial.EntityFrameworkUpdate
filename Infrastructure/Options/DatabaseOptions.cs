namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Options
{
    public class DatabaseOptions
    {
        public string InventoryRO { get; init; }
        public string InventoryRW { get; init; }

        internal const string SectionName = "Database";

    }
}
