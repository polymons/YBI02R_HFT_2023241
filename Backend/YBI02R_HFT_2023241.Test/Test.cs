using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using YBI02R_HFT_2023241.Logic.Classes;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Interfaces;

namespace YBI02R_HFT_2023241.Test
{
    [TestFixture]
    internal class Test
    {
        ArtistLogic artistLogic;
        Mock<IRepository<Artist>> artistRepoMock;
        PublisherLogic publisherLogic;
        Mock<IRepository<Publisher>> publisherRepoMock;
        SongLogic songLogic;
        Mock<IRepository<Song>> songRepoMock;
        StatLogic statLogic;

        [SetUp]
        public void Init()
        {
            artistRepoMock = new Mock<IRepository<Artist>>();
            artistRepoMock.Setup(a => a.ReadAll()).Returns(new List<Artist>()
            {
                new Artist(1, "Post Malone", 101, 26), //int id, string name, int studioID, int age
                new Artist(2, "Niko B", 102, 22),
                new Artist(3, "The Weeknd", 103, 32),
                new Artist(4, "Central Cee", 104, 23),
                new Artist(5, "Pink Floyd", 105, 75),

            }.AsQueryable());

            songRepoMock = new Mock<IRepository<Song>>();
            songRepoMock.Setup(a => a.ReadAll()).Returns(new List<Song>()
            {
                new Song("Circles", "Pop", 242, 1, 1), // Post Malone
                new Song("Who's That What's That", "Hip-Hop", 243, 2, 2), // Niko B
                new Song("Blinding Lights", "R&B", 244, 3, 3), // The Weeknd
                new Song("Diamond Choker", "Hip-Hop", 245, 4, 4), // Central Cee
                new Song("Comfortably Numb", "Rock", 246, 5, 5), // Pink Floyd
            }.AsQueryable());

            publisherRepoMock = new Mock<IRepository<Publisher>>();
            publisherRepoMock.Setup(a => a.ReadAll()).Returns(new List<Publisher>()
            {
                new Publisher("US","Universal Music Group", "Los Angeles", 101), // Corresponding to Post Malone
                new Publisher("UK","Atlantic Records", "London", 102), // Corresponding to Niko B
                new Publisher("CA","Republic Records", "Toronto", 103), // Corresponding to The Weeknd
                new Publisher("UK", "Central Cee Music", "London", 104), // Corresponding to Central Cee
                new Publisher("UK", "EMI", "London", 105), // Corresponding to Pink Floyd
            }.AsQueryable());

            artistLogic = new ArtistLogic(artistRepoMock.Object);
            publisherLogic = new PublisherLogic(publisherRepoMock.Object);
            songLogic = new SongLogic(songRepoMock.Object);
        }

        [Test]
        public void CreateArtist_ShouldCreateArtist()
        {
            // Arrange
            Artist testArtist = new Artist(2000, "Analog Balaton", 3000, 20);

            // Act
            artistLogic.Create(testArtist);

            // Assert
            artistRepoMock.Verify(son => son.Create(testArtist), Times.Once);
        }

        [Test]
        public void CreatePublisher_ShouldCreatePublisher()
        {
            // Arrange
            Publisher createdPublisher = new Publisher("HUN", "Analog Balaton studio", 3000);

            // Act
            publisherLogic.Create(createdPublisher);

            // Assert
            publisherRepoMock.Verify(pub => pub.Create(createdPublisher), Times.Once);
        }

        //CreateSong_ShouldCreateSong string title, string genre, int length,int plays, int songID, int artistID

        [Test]
        public void ArtistHomeCity_ReturnsCorrectCity()
        {
            // Arrange
            string artistName = "Mozart";
            string expectedCity = "Vienna";
            var artist = new Artist(2001, artistName, 3001, 20);
            artist.Studio = new Publisher { City = expectedCity, StudioID = 3001 };
            var artistRepoMock = new Mock<IRepository<Artist>>();
            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist }.AsQueryable());
            var statLogic = new StatLogic(null, artistRepoMock.Object, null);

            // Act
            string result = statLogic.ArtistHomeCity(artistName);

            // Assert
            Assert.AreEqual(expectedCity, result);
        }

        [Test]
        public void ArtistWithMostSongs_ReturnsArtistWithMostSongs()
        {
            // Arrange
            var artist1 = new Artist(1, "Artist1", 20, 3000);
            artist1.Songs = new List<Song> { new Song(), new Song() };
            var artist2 = new Artist(2, "Artist2", 25, 4000);
            artist2.Songs = new List<Song> { new Song(), new Song(), new Song() };
            var artistRepoMock = new Mock<IRepository<Artist>>();
            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1, artist2 }.AsQueryable());
            var statLogic = new StatLogic(null, artistRepoMock.Object, null);

            // Act
            Artist result = statLogic.ArtistWithMostSongs();

            // Assert
            Assert.AreEqual(artist2, result);
        }

        [Test]
        public void GetAllArtists_ReturnsAllArtists()
        {
            // Arrange
            var expectedArtists = new List<Artist>
            {
                new Artist(1, "Post Malone", 101, 26),
                new Artist(2, "Niko B", 102, 22),
                new Artist(3, "The Weeknd", 103, 32),
                new Artist(4, "Central Cee", 104, 23),
                new Artist(5, "Pink Floyd", 105, 75)
            }.AsQueryable();

            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(expectedArtists);

            // Act
            var result = artistLogic.ReadAll();

            // Assert
            Assert.AreEqual(expectedArtists, result);
        }

        [Test]
        public void OldestArtistAge_ReturnsOldestArtistAge()
        {
            // Arrange
            var artist1 = new Artist(1, "Artist1", 20, 22);
            var artist2 = new Artist(2, "Artist2", 25, 78);
            var artistRepoMock = new Mock<IRepository<Artist>>();
            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1, artist2 }.AsQueryable());
            var statLogic = new StatLogic(null, artistRepoMock.Object, null);

            // Act
            int? result = statLogic.OldestArtistAge();

            // Assert
            Assert.AreEqual(78, result);
        }

        //[Test]
        //public void LongestSong_ReturnsLongestSong()
        //{
        //    // Arrange
        //    var song1 = new Song("Song1", "R&B", 200,1, 1, 1);
        //    var song2 = new Song("Song2","Pop", 3000,2, 2, 1);
        //    var songRepoMock = new Mock<IRepository<Song>>();
        //    songRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Song> { song1, song2 }.AsQueryable());
        //    var statLogic = new StatLogic(null, null, songRepoMock.Object);

        //    // Act
        //    Song result = statLogic.LongestSong();

        //    // Assert
        //    Assert.AreEqual(song2, result);
        //}
        //[Test]
        //public void AvgSongLengthForArtist_ReturnsAvgSongLengthForArtist()
        //{
        //    // Arrange
        //    var artist1 = new Artist(1, "Artist1", 20, 3000);
        //    artist1.Songs = new List<Song> { new Song("Song1", 200), new Song("Song2", 300) };
        //    var artist2 = new Artist(2, "Artist2", 25, 4000);
        //    artist2.Songs = new List<Song> { new Song("Song3", 250), new Song("Song4", 350) };
        //    var artistRepoMock = new Mock<IRepository<Artist>>();
        //    artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1, artist2 }.AsQueryable());
        //    var statLogic = new StatLogic(null, artistRepoMock.Object, null);

        //    // Act
        //    double? result = statLogic.AvgSongLengthForArtist("Artist1");

        //    // Assert
        //    Assert.AreEqual(250, result);
        //}

        //[Test]
        //public void MostPopularArtist_ReturnsMostPopularArtist()
        //{
        //    // Arrange
        //    var artist1 = new Artist(1, "Artist1", 20, 3000);
        //    artist1.Plays = 100;
        //    var artist2 = new Artist(2, "Artist2", 25, 4000);
        //    artist2.Plays = 200;
        //    var artistRepoMock = new Mock<IRepository<Artist>>();
        //    artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1, artist2 }.AsQueryable());
        //    var statLogic = new StatLogic(null, artistRepoMock.Object, null);

        //    // Act
        //    Artist result = statLogic.MostPopularArtist();

        //    // Assert
        //    Assert.AreEqual(artist2, result);
        //}

        //[Test]
        //public void MostPopularSongOfArtist_ReturnsMostPopularSongOfArtist()
        //{
        //    // Arrange
        //    var artist1 = new Artist(1, "Artist1", 20, 3000);
        //    artist1.Songs = new List<Song> { new Song("Song1", 200), new Song("Song2", 300) };
        //    var artist2 = new Artist(2, "Artist2", 25, 4000);
        //    artist2.Songs = new List<Song> { new Song("Song3", 250), new Song("Song4", 350) };
        //    var artistRepoMock = new Mock<IRepository<Artist>>();
        //    artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1, artist2 }.AsQueryable());
        //    var statLogic = new StatLogic(null, artistRepoMock.Object, null);

        //    // Act
        //    Song result = statLogic.MostPopularSongOfArtist("Artist1");

        //    // Assert
        //    Assert.AreEqual("Song2", result.Name);
        //}

        //[Test]
        //public void MinutesListenedToPublisher_ReturnsMinutesListenedToPublisher()
        //{
        //    // Arrange
        //    var artist1 = new Artist(1, "Artist1", 20, 3000);
        //    artist1.Songs = new List<Song> { new Song("Song1", 200), new Song("Song2", 300) };
        //    var artist2 = new Artist(2, "Artist2", 25, 4000);
        //    artist2.Songs = new List<Song> { new Song("Song3", 250), new Song("Song4", 350) };
        //    var publisher = new Publisher("Publisher1");
        //    publisher.Artists = new List<Artist> { artist1, artist2 };
        //    var publisherRepoMock = new Mock<IRepository<Publisher>>();
        //    publisherRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Publisher> { publisher }.AsQueryable());
        //    var statLogic = new StatLogic(publisherRepoMock.Object, null, null);

        //    // Act
        //    double? result = statLogic.MinutesListenedToPublisher("Publisher1");

        //    // Assert
        //    Assert.AreEqual(25, result);
        //}

        [Test]
        public void ArtistHomeCity_ReturnsArtistHomeCity()
        {
            // Arrange
            var artist1 = new Artist(1, "Artist1", 20, 3000);
            var studio1 = new Publisher("UK", "artist1Studio", "City1", 20);
            artist1.Studio = studio1; // Assign the studio to the artist
            var artist2 = new Artist(2, "Artist2", 25, 4000);
            var studio2 = new Publisher("UK", "artist1Studio", "City2", 25);
            artist2.Studio = studio2; // Assign the studio to the artist
            var artistRepoMock = new Mock<IRepository<Artist>>();
            var studioRepoMock = new Mock<IRepository<Publisher>>();
            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1, artist2 }.AsQueryable());

            var statLogic = new StatLogic(null, artistRepoMock.Object, null);

            // Act
            string result = statLogic.ArtistHomeCity("Artist1");

            // Assert
            Assert.AreEqual("City1", result);
        }
    }
}
