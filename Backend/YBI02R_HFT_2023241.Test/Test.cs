using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using YBI02R_HFT_2023241.Logic.Classes;
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
                new Artist { ArtistID = 1, Name = "Post Malone", StudioID = 101, Age = 26 },
                new Artist { ArtistID = 2, Name = "Niko B", StudioID = 102, Age = 22 },
                new Artist { ArtistID = 3, Name = "The Weeknd", StudioID = 103, Age = 32 },
                new Artist { ArtistID = 4, Name = "Central Cee", StudioID = 104, Age = 23 },
                new Artist { ArtistID = 5, Name = "Pink Floyd", StudioID = 105, Age = 75 }
            }.AsQueryable());

            songRepoMock = new Mock<IRepository<Song>>();
            songRepoMock.Setup(a => a.ReadAll()).Returns(new List<Song>()
            {
                new Song { Title = "Circles", Genre = "Pop", Length = 242, Plays = 1, SongID = 1 },
                new Song { Title = "Who's That What's That", Genre = "Hip-Hop", Length = 243, Plays = 2, SongID = 2 },
                new Song { Title = "Blinding Lights", Genre = "R&B", Length = 244, Plays = 3, SongID = 3 },
                new Song { Title = "Diamond Choker", Genre = "Hip-Hop", Length = 245, Plays = 4, SongID = 4 },
                new Song { Title = "Comfortably Numb", Genre = "Rock", Length = 246, Plays = 5, SongID = 5 },
                new Song { Title = "Another Brick in the Wall", Genre = "Rock", Length = 247, Plays = 6, SongID = 6 },
                new Song { Title = "Wish You Were Here", Genre = "Rock", Length = 248, Plays = 7, SongID = 7 },
                new Song { Title = "Money", Genre = "Rock", Length = 249, Plays = 8, SongID = 8 }
            }.AsQueryable());

            publisherRepoMock = new Mock<IRepository<Publisher>>();
            publisherRepoMock.Setup(a => a.ReadAll()).Returns(new List<Publisher>()
            {
                new Publisher { Country = "US", StudioName = "Universal Music Group", City = "Los Angeles", StudioID = 101 },
                new Publisher { Country = "UK", StudioName = "Atlantic Records", City = "London", StudioID = 102 },
                new Publisher { Country = "CA", StudioName = "Republic Records", City = "Toronto", StudioID = 103 },
                new Publisher { Country = "UK", StudioName = "Central Cee Music", City = "London", StudioID = 104 },
                new Publisher { Country = "UK", StudioName = "EMI", City = "London", StudioID = 105 }
            }.AsQueryable());

            statLogic = new StatLogic(songRepoMock.Object, artistRepoMock.Object, publisherRepoMock.Object);
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

        [Test]
        public void CreateSong_ShouldCreateSong()
        {
            // Arrange
            string title = "Song Title";
            string genre = "Pop";
            int length = 180;
            int plays = 1000;
            int songID = 1;
            int artistID = 1;
            var song = new Song(title, genre, length, plays, songID, artistID);
            var songRepoMock = new Mock<IRepository<Song>>();
            var songLogic = new SongLogic(songRepoMock.Object);

            // Act
            songLogic.Create(song);

            // Assert
            songRepoMock.Verify(repo => repo.Create(song), Times.Once);
        }

        [Test]
        public void DeleteArtist_ShouldDeleteArtist()
        {
            // Arrange
            int artistId = 1;
            var artistToDelete = new Artist(artistId, "Artist to Delete", 3000, 20);
            artistRepoMock.Setup(repo => repo.Read(artistId)).Returns(artistToDelete);

            // Act
            artistLogic.Delete(artistId);

            // Assert
            artistRepoMock.Verify(repo => repo.Delete(artistToDelete.ArtistID), Times.Once);
        }

        [Test]
        public void DeletePublisher_ShouldDeletePublisher()
        {
            // Arrange
            int publisherId = 1;
            var publisherToDelete = new Publisher("US", "Publisher to Delete", "City", publisherId);
            publisherRepoMock.Setup(repo => repo.Read(publisherId)).Returns(publisherToDelete);

            // Act
            publisherLogic.Delete(publisherId);

            // Assert
            publisherRepoMock.Verify(repo => repo.Delete(publisherToDelete.StudioID), Times.Once);
        }

        [Test]
        public void DeleteSong_ShouldDeleteSong()
        {
            // Arrange
            int songId = 1;
            var songToDelete = new Song("Song Title", "Pop", 180, 1000, songId, 1);
            songRepoMock.Setup(repo => repo.Read(songId)).Returns(songToDelete);

            // Act
            songLogic.Delete(songId);

            // Assert
            songRepoMock.Verify(repo => repo.Delete(songToDelete.SongID), Times.Once);
        }

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
            var artist1 = new Artist(1, "Artist1", 20, 30);
            artist1.Songs = new List<Song> { new Song(), new Song() };
            var artist2 = new Artist(2, "Artist2", 25, 40);
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
        public void LongestSong_ReturnsLongestSong()
        {
            // Arrange
            var expected = new Song("Bohemian Rhapsody", "Rock", 354, 100000, 1, 1);
            var song1 = new Song("Song 1", "Pop", 180, 1000, 2, 1);
            var song2 = new Song("Song 2", "Hip Hop", 240, 2000, 3, 2);
            var song3 = new Song("Song 3", "R&B", 210, 1500, 4, 2);

            var artist1 = new Artist(1, "Artist1", 20, 30);
            artist1.Songs = new List<Song> { song1, expected };
            var artist2 = new Artist(2, "Artist2", 25, 40);
            artist2.Songs = new List<Song> { song2, song3 };
            var artists = new List<Artist> { artist1, artist2 };
            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(artists.AsQueryable()); 
            var statLogic = new StatLogic(songRepoMock.Object, artistRepoMock.Object, null);

            // Act
            var longestSong = statLogic.LongestSong();

            // Assert
            Assert.AreEqual(expected, longestSong);
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
        public void GetAllPublishers_ReturnsAllPublishers()
        {
            // Arrange
            var expectedPublishers = new List<Publisher>
            {
                new Publisher { Country = "US", StudioName = "Universal Music Group", City = "Los Angeles", StudioID = 101 },
                new Publisher { Country = "UK", StudioName = "Atlantic Records", City = "London", StudioID = 102 },
                new Publisher { Country = "CA", StudioName = "Republic Records", City = "Toronto", StudioID = 103 },
                new Publisher { Country = "UK", StudioName = "Central Cee Music", City = "London", StudioID = 104 },
                new Publisher { Country = "UK", StudioName = "EMI", City = "London", StudioID = 105 }
            }.AsQueryable();

            publisherRepoMock.Setup(repo => repo.ReadAll()).Returns(expectedPublishers);

            // Act
            var result = publisherLogic.ReadAll();

            // Assert
            Assert.AreEqual(expectedPublishers, result);
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

        [Test]
        public void AvgSongLengthForArtist_ReturnsAvgSongLengthForArtist()
        {
            // Arrange
            var artist1 = new Artist(1, "Artist1", 20, 3000);
            var song1 = new Song { Title = "Song1", Genre = "Rock", Length = 246, Plays = 5, SongID = 5 };
            var song2 = new Song { Title = "Song2", Genre = "Rock", Length = 247, Plays = 6, SongID = 6 };
            artist1.Songs = new List<Song> { song1, song2 };

            var artist2 = new Artist(2, "Artist2", 25, 5000);
            var song3 = new Song { Title = "Song3", Genre = "Pop", Length = 200, Plays = 10, SongID = 7 };
            var song4 = new Song { Title = "Song4", Genre = "Pop", Length = 180, Plays = 8, SongID = 8 };
            artist2.Songs = new List<Song> { song3, song4 };

            var artistRepoMock = new Mock<IRepository<Artist>>();
            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1, artist2 }.AsQueryable());

            var statLogic = new StatLogic(null, artistRepoMock.Object, null);

            // Act
            var result = statLogic.AvgSongLengthForArtist("Artist1");

            // Assert
            Assert.AreEqual(246.5, result);
        }

        [Test]
        public void MostPopularArtist_ReturnsMostPopularArtist()
        {
            // Arrange
            var artist1 = new Artist(1, "Artist1", 20, 3000);
            var song1 = new Song { Title = "Song1", Genre = "Rock", Length = 246, Plays = 5, SongID = 5 };
            var song2 = new Song { Title = "Song2", Genre = "Rock", Length = 247, Plays = 6, SongID = 6 };
            artist1.Songs = new List<Song> { song1, song2 };

            var artist2 = new Artist(2, "Artist2", 25, 5000);
            var song3 = new Song { Title = "Song3", Genre = "Pop", Length = 200, Plays = 10, SongID = 7 };
            var song4 = new Song { Title = "Song4", Genre = "Pop", Length = 180, Plays = 8, SongID = 8 };
            artist2.Songs = new List<Song> { song3, song4 };

            var artistRepoMock = new Mock<IRepository<Artist>>();
            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1, artist2 }.AsQueryable());

            var statLogic = new StatLogic(null, artistRepoMock.Object, null);

            // Act
            var result = statLogic.MostPopularArtist();

            // Assert
            Assert.AreEqual(artist2, result);
        }

        [Test]
        public void MostPopularSongOfArtist_ReturnsMostPopularSongOfArtist()
        {
            // Arrange
            var artist1 = new Artist(1, "Artist1", 20, 3000);
            var song1 = new Song { Title = "Comfortably Numb", Genre = "Rock", Length = 246, Plays = 5, SongID = 5 };
            var song2 = new Song { Title = "Another Brick in the Wall", Genre = "Rock", Length = 247, Plays = 6, SongID = 6 };
            artist1.Songs = new List<Song> { song1, song2 };
            var artistRepoMock = new Mock<IRepository<Artist>>();
            artistRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Artist> { artist1 }.AsQueryable());
            var statLogic = new StatLogic(null, artistRepoMock.Object, null);

            // Act
            var result = statLogic.MostPopularSongOfArtist("Artist1");

            // Assert
            Assert.AreEqual(song2, result);
        }

        [Test]
        public void MinutesListenedToPublisher_ReturnsMinutesListenedToPublisher()
        {
            // Arrange
            var publisherName = "Publisher1";
            var publisher = new Publisher("UK", "Publisher1", "City1", 20);
            var artist1 = new Artist(1, "Artist1", 20, 3000);
            var song1 = new Song { Title = "Song1", Genre = "Rock", Length = 246, Plays = 5, SongID = 5 };
            var song2 = new Song { Title = "Song2", Genre = "Rock", Length = 247, Plays = 6, SongID = 6 };
            artist1.Songs = new List<Song> { song1, song2 };

            publisher.Artists = new List<Artist> { artist1 };

            var publisherRepoMock = new Mock<IRepository<Publisher>>();
            publisherRepoMock.Setup(repo => repo.ReadAll()).Returns(new List<Publisher> { publisher }.AsQueryable());

            var statLogic = new StatLogic(null, null, publisherRepoMock.Object);

            // Act
            var result = Math.Round((double)statLogic.MinutesListenedToPublisher(publisherName), 2);

            // Assert
            Assert.AreEqual(44.0d, result); 
        }

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
