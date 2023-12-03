using Moq;
using NUnit.Framework;
using System;
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

        [SetUp]
        public void Init()
        {
            artistRepoMock = new Mock<IRepository<Artist>>();





        }

    }
}
