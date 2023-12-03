using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System.Xml.Linq;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.Repository.Database
{
    public class MusicDbContext : DbContext
    {
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        public MusicDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies(true).UseInMemoryDatabase("Music");
            }
            else
            {
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                .HasOne(x => x.Artist) //A song has one artist in this context
                .WithMany(x => x.Songs) //And the artist can of course have many songs
                .HasForeignKey(x => x.ArtistID) //We can connect using the ArtistID
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Publisher>()
                .HasMany(x => x.Artists)
                .WithOne(x => x.Studio)
                .HasForeignKey(x => x.StudioID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Artist>()
                .HasOne(x => x.Studio)
                .WithMany(x => x.Artists)
                .HasForeignKey(x => x.StudioID)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Artist>().HasData(new Artist[]
            {
                //(int id, string name, int studioID, int age)

                new Artist(1, "Post Malone", 101, 26),
                new Artist(2, "Niko B", 102, 22),
                new Artist(3, "The Weeknd", 103, 32),
                new Artist(4, "Central Cee", 104, 23),
                new Artist(5, "Pink Floyd", 105, 75),
                new Artist(6, "Boris Brejcha", 106, 39),
                new Artist(7, "Ben Böhmer", 107, 34),
                new Artist(8, "Jan Blomqvist", 108, 40),
                new Artist(9, "Red Hot Chili Peppers", 109, 59),
                new Artist(10, "The Rolling Stones", 110, 79),
                new Artist(11, "Lewis Capaldi", 111, 25),
                new Artist(12, "Moritz Hofbauer", 112, 30),
                new Artist(13, "Sofi Tukker", 113, 35),
                new Artist(14, "Arctic Monkeys", 114, 36)
                //...
            });

            modelBuilder.Entity<Song>().HasData(new Song[]
            {
                //(string title, string genre, int length, int plays, int songID, int artistID)

                new Song("Circles", "Pop", 242, 11000, 1, 1), // Post Malone
                new Song("Sunflower", "Pop", 158, 8500, 2, 1),
                new Song("I Fall Apart", "R&B", 219, 6700, 3, 1),
                new Song("Who's That What's That", "Hip-Hop", 243, 4300, 4, 2), // Niko B
                new Song("Blinding Lights", "R&B", 244, 9800, 5, 3), // The Weeknd
                new Song("Starboy", "Pop", 229, 11200, 6, 3),
                new Song("Can't Feel My Face", "Pop", 213, 12300, 7, 3),
                new Song("Save Your Tears", "Pop", 215, 10400, 8, 3),
                new Song("In The Night", "Pop", 232, 9600, 9, 3),
                new Song("Diamond Choker", "Hip-Hop", 245, 7500, 10, 4), // Central Cee
                new Song("Comfortably Numb", "Rock", 246, 8200, 11, 5), // Pink Floyd
                new Song("Gravity", "Techno", 247, 6800, 12, 6), // Boris Brejcha
                new Song("After Earth", "Electronic", 248, 5400, 13, 7), // Ben Böhmer
                new Song("Maybe Not", "Electronic", 249, 4300, 14, 8), // Jan Blomqvist
                new Song("Under the Bridge", "Rock", 250, 9200, 15, 9), // Red Hot Chili Peppers
                new Song("Paint It Black", "Rock", 251, 11000, 16, 10), // The Rolling Stones
                new Song("Someone You Loved", "Pop", 252, 10500, 17, 11), // Lewis Capaldi
                new Song("Midnight in Berlin", "Electronic", 253, 7800, 18, 12), // Moritz Hofbauer
                new Song("Drinkee", "Electronic", 254, 6900, 19, 13), // Sofi Tukker
                new Song("Do I Wanna Know?", "Indie Rock", 255, 8800, 20, 14) // Arctic Monkeys

            });

            modelBuilder.Entity<Publisher>().HasData(new Publisher[]
            {
                //(string country, string studioName, string studioCity, int studioID)

                new Publisher("US","Universal Music Group", "Los Angeles", 101), // Corresponding to Post Malone
                new Publisher("UK","Atlantic Records", "London", 102), // Corresponding to Niko B
                new Publisher("CA","Republic Records", "Toronto", 103), // Corresponding to The Weeknd
                new Publisher("UK", "Central Cee Music", "London", 104), // Corresponding to Central Cee
                new Publisher("UK", "EMI", "London", 105), // Corresponding to Pink Floyd
                new Publisher("DE", "Ultra Music", "Stuttgart", 106), // Corresponding to Boris Brejcha
                new Publisher("DE", "Anjunadeep", "Berlin", 107), // Corresponding to Ben Böhmer
                new Publisher("NE", "Armada Music", "Berlin", 108), // Corresponding to Jan Blomqvist
                new Publisher("US", "Warner Records", "Los Angeles", 109), // Corresponding to Red Hot Chili Peppers
                new Publisher("UK", "Rolling Stones Records", "London", 110), // Corresponding to The Rolling Stones
                new Publisher("UK", "Virgin EMI", "London", 111), // Corresponding to Lewis Capaldi
                new Publisher("DE", "Sony Music", "Berlin", 112), // Corresponding to Moritz Hofbauer
                new Publisher("US", "Ultra Music", "New York", 113), // Corresponding to Sofi Tukker
                new Publisher("UK", "Domino Recording Company", "Sheffield", 114) // Corresponding to Arctic Monkeys                                                             // Add more publishers if needed
            });
        }
    }
}
