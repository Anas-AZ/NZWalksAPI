using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty>    Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for difficulties
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("17dc1406-2245-4270-8611-2587e9c44e74") ,
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("eed37763-851d-452d-b429-e4964a78d5b7"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("d5af210e-03b1-4066-b9b0-84e14967c3ea"),
                    Name = "Hard"
                },
            };

            //Seed difficulties into the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        


            //seed data for regions
            var regions = new List<Region>()
            {
                    new Region
                    {
                        Id = Guid.Parse("461f524a-609f-426b-b9f7-15d0e96e245a"),
                        Name = "Auckland",
                        Code = "AKL",
                        RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                    },
                    new Region
                    {
                        Id = Guid.Parse("d7b9dc43-bc39-41ea-94c8-9b8944d2d362"),
                        Name = "Northland",
                        Code = "NTL",
                        RegionImageUrl = null
                    },
                    new Region
                    {
                        Id = Guid.Parse("65d76290-9091-4646-a786-72bf53255779"),
                        Name = "Bay Of Plenty",
                        Code = "BOP",
                        RegionImageUrl = null
                    },
                    new Region
                    {
                        Id = Guid.Parse("d96e07cf-ff1f-4dfb-892c-86a8b8e8c287"),
                        Name = "Wellington",
                        Code = "WGN",
                        RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                    },
                    new Region
                    {
                        Id = Guid.Parse("166b7240-6e03-49af-b2c5-98de777adb41"),
                        Name = "Nelson",
                        Code = "NSN",
                        RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                    },
                    new Region
                    {
                        Id = Guid.Parse("b0026d9e-b538-49c5-a0ae-3102974d53ca"),
                        Name = "Southland",
                        Code = "STL",
                        RegionImageUrl = null
                    },
            };

            //Seed regions into the database
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
