using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;

namespace Tournament.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceprovider = scope.ServiceProvider;
                var db = serviceprovider.GetRequiredService<TournamentAPIContext>();

                await db.Database.MigrateAsync();
                if (await db.TournamentDetails.AnyAsync()) return;

                try
                {
                    var tournaments = SeedData.GenerateSeedData(4, 3, 5);
                    db.AddRange(tournaments);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
