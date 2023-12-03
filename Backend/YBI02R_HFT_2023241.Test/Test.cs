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





        }

    }
}
