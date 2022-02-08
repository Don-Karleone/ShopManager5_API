namespace ShopManager5.Api.Data
{
    public static class ApplicationDbContextExtensions
    {
        public static void CopyEntityMembers<TEntity>(this ApplicationDbContext dbContext, TEntity source, TEntity destination)
        {
            dbContext.Entry(destination).CurrentValues.SetValues(source);

            var sourceEntry = dbContext.Entry(source);

            foreach (var collection in dbContext.Entry(destination).Collections)
            {
                collection.CurrentValue = sourceEntry.Collections
                    .Single(x => x.Metadata.Name == collection.Metadata.Name).CurrentValue;
            }

            foreach (var reference in dbContext.Entry(destination).References)
            {
                reference.CurrentValue = sourceEntry.References
                    .Single(x => x.Metadata.Name == reference.Metadata.Name).CurrentValue;
            }
        }
    }
}
