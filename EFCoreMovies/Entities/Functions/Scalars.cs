using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Entities.Functions
{
    public static class Scalars
    {
        public static void RegisterFunctions(ModelBuilder modelBuilder) //invoke this method in application db context and pass modelbuilder instance
        {
            modelBuilder.HasDbFunction(() => InvoiceDetailAverage(0));
        }
        public static decimal InvoiceDetailAverage(int invoiceId) //used to invoice db function
        {
            return 0;
        }
    }
}
