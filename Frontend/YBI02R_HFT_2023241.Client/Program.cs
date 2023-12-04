using ConsoleTools;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Client
{
    internal class Program
    {
        static RestService _rest;

        static void Main(string[] args)
        {
            _rest = new RestService("http://localhost:53910/", "swagger");

            var mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Songs", () => EntityMenu(args,"Song").Show())
                .Add("Artists", () => EntityMenu(args, "Artist").Show())
                .Add("Publishers", () => EntityMenu(args, "Publisher").Show())
                .Add("Stats", () => subMenu(args).Show())
                .Add("Exit", ConsoleMenu.Close);
            mainMenu.Show();


        }
        #region MenuElements
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="entity">Entity is the name of the parameter to pass into the CRUD methods</param>
        /// <returns></returns>
        private static ConsoleMenu EntityMenu(string[] args, string entity)
        {
            return new ConsoleMenu(args, level: 1)
                .Add("List", () => Read(entity))
                .Add("Create", () => Create(entity))
                .Add("Update", () => Update(entity))
                .Add("Delete", () => Delete(entity))
                .Add("Stats", () => subMenu(args, entity).Show())
                .Add("Exit", ConsoleMenu.Close);
        }
        //this one is for the generic stats
        private static ConsoleMenu subMenu(string[] args)
        {

            //stats that affect all Models
            return new ConsoleMenu(args, level: 2)
                //.Add("stat1", () )
                //.Add("stat2", )
                //.Add("stat3", )
                //.Add("stat4", )
                .Add("Exit", ConsoleMenu.Close);
        }
        //this is for the stats of a given entity
        private static ConsoleMenu subMenu(string[] args, string entity)
        {
            switch (entity)
            {
                case ("Artist"):
                    return new ConsoleMenu(args, level: 2)
                    .Add("Oldest artist", () => GetOldestArtistAge() )
                    .Add("Artist home town", () => GetArtistHomeCity())
                    .Add("Artist with most songs", () => GetArtistWithMostSongs())
                    .Add("Average song length for artist", () => GetAvgSongLengthForArtist())
                    //.Add("stat3", )
                    //.Add("stat4", )
                    .Add("Exit", ConsoleMenu.Close);
                case ("Song"):
                    return new ConsoleMenu(args, level: 2)
                    .Add("Longest song", () => GetLongestSong())
                    //.Add("stat2", )
                    //.Add("stat3", )
                    //.Add("stat4", )
                    .Add("Exit", ConsoleMenu.Close);
                case ("Publisher"):
                    return new ConsoleMenu(args, level: 2)
                    .Add("Minutes listened to studio", () => GetMinutesListenedToPublisher())
                    //.Add("stat2", )
                    //.Add("stat3", )
                    //.Add("stat4", )
                    .Add("Exit", ConsoleMenu.Close);
                default:
                     return new ConsoleMenu(args, level: 2)
                    .Add("Exit", ConsoleMenu.Close);
            }
        }
        #endregion

        #region CRUD
        internal static void Create(string entity)
        {
            switch (entity)
            {
                case ("Song"):
                    try
                    {
                        Console.Write("Enter song title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter song genre: ");
                        string genre = Console.ReadLine();
                        Console.Write("Enter ArtistID: ");
                        int aid = int.Parse(Console.ReadLine());
                        _rest.Post<Song>(new Song(title, genre, aid), "Song");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"There was an error: {e.Message}");
                    }
                    break;
                case ("Artist"):
                    try
                    {
                        Console.Write("Enter artist name:");
                        string newArtistName = Console.ReadLine();
                        Console.Write("Enter artist StudioID:");
                        int newArtistStudio = int.Parse(Console.ReadLine());
                        Console.Write("Enter age:");
                        int newArtistAge = int.Parse(Console.ReadLine());
                        _rest.Post(new Artist(newArtistName, newArtistStudio, newArtistAge), "Artist");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"There was an error: {e.Message}");
                    }
                    break;
                case ("Publisher"):
                    try
                    {
                        Console.Write("Enter publisher name:");
                        string pubName = Console.ReadLine();
                        Console.Write("Enter publisher country:");
                        string pubCountry = Console.ReadLine();
                        Console.Write("Enter studio city:");
                        string pubCity = Console.ReadLine();
                        _rest.Post(new Publisher(pubCountry, pubName,pubCity), "Publisher");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"There was an error: {e.Message}");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid entity");
                    break;
            }
        }

        internal static void Read(string entity)
        {
            switch (entity)
            {
                case "Song":
                    List<Song> songs = _rest.Get<Song>("Song");
                    Console.WriteLine("\n" + "--------------------------------");
                    foreach (var s in songs)
                    {
                        Console.Write($"{s.SongID}\t{s.Title}\n\t{s?.Genre} | {s.Length} | {s.Plays} |");
                        if (s.Artist == null) Console.Write("Artist not found\n--------------------------------\n");
                        else Console.Write($"{s.Artist.Name}\n--------------------------------\n");
                    }
                    break;
                case "Artist":
                    List<Artist> artists = _rest.Get<Artist>("Artist");
                    Console.WriteLine("\n" + "--------------------------------");
                    foreach (var a in artists)
                    {
                        Console.WriteLine($"{a.ArtistID}\t{a.Name}\n\t{a?.Age}\t{a?.StudioID}-{a.Studio?.StudioName}\n--------------------------------");
                    }
                    break;
                case "Publisher":
                    List<Publisher> pubs = _rest.Get<Publisher>("Publisher");
                    Console.WriteLine("\n" + "--------------------------------");
                    foreach (Publisher p in pubs)
                    {
                        Console.WriteLine($"{p.StudioID}\t{p.StudioName}\t{p.Country}\n--------------------------------");
                    }
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }

        internal static void Update(string entity)
        {
            Console.WriteLine($"Enter the wanted {entity}'s ID");
            int updateid = int.Parse(Console.ReadLine());
            switch (entity)
            {
                case "Song":
                    try
                    {
                        Song songUpdate = _rest.GetId<Song>(updateid, "Song/");
                        Console.Write($"Enter new song title (old: {songUpdate?.Title}):");
                        string newSongTitle = Console.ReadLine();
                        Console.Write($"Enter new song artist id (old: {songUpdate?.ArtistID}):");
                        int newSongArtist = int.Parse(Console.ReadLine());
                        Console.Write($"Enter new song length (old: {songUpdate?.Length}):");
                        int newSongLength = int.Parse(Console.ReadLine());
                        Console.Write($"Enter new song genre (old: {songUpdate?.Genre}):");
                        string newSongGenre = Console.ReadLine();
                        songUpdate.Length = newSongLength;
                        songUpdate.Genre = newSongGenre;
                        songUpdate.Title = newSongTitle;
                        songUpdate.ArtistID = newSongArtist;
                        _rest.Put(songUpdate, "Song");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"There was an error: {e.Message}");
                    }
                    break;
                case "Artist":
                    try
                    {
                        Artist artistUpdate = _rest.GetId<Artist>(updateid, "Artist/");
                        Console.Write($"Enter new artist studio ID (old: {artistUpdate?.StudioID}):");
                        int newArtistStudioID = int.Parse(Console.ReadLine());
                        Console.Write($"Enter new artist name (old: {artistUpdate?.Name}):");
                        string newArtistName = Console.ReadLine();
                        Console.Write($"Enter new artist age(old: {artistUpdate?.Age}):");
                        int newArtistAge = int.Parse(Console.ReadLine());
                        artistUpdate.Age = newArtistAge;
                        artistUpdate.StudioID = newArtistStudioID;
                        artistUpdate.Name = newArtistName;
                        _rest.Put(artistUpdate, "Artist");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"There was an error: {e.Message}");
                    }
                    break;
                case "Publisher":
                    try
                    {
                        Publisher publisherUpdate = _rest.GetId<Publisher>(updateid, "Publisher/");
                        Console.Write($"Enter new publisher name (old: {publisherUpdate?.StudioName}):");
                        string newPublisherName = Console.ReadLine();
                        Console.Write($"Enter the new publisher's country (old: {publisherUpdate?.Country}):");
                        string newPublisherCountry = Console.ReadLine();
                        Console.Write($"Enter the new publisher's city (old: {publisherUpdate?.City}):");
                        string newPublisherCity= Console.ReadLine();

                        publisherUpdate.Country = newPublisherCountry;
                        publisherUpdate.StudioName = newPublisherName;
                        publisherUpdate.City = newPublisherCity;
                        _rest.Put(publisherUpdate, "Publisher");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"There was an error: {e.Message}");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid entity");
                    break;
            }
        }

        internal static void Delete(string entity)
        {
            Console.WriteLine($"Enter {entity} id to delete:");
            int del = int.Parse(Console.ReadLine());
            switch (entity)
            {
                case "Song":
                    _rest.Delete(del, "Song");
                    break;
                case "Artist":
                    _rest.Delete(del, "Artist");
                    break;
                case "Publisher":
                    _rest.Delete(del, "Publisher");
                    break;
                default:
                    Console.WriteLine($"Invalid request called {entity}");
                    break;
            }
        }
        #endregion

        #region STATS
        internal static void GetOldestArtistAge()
        {
            try
            {
                var oldest = _rest.GetSingle<int?>("/Stat/OldestArtistAge");
                Console.WriteLine($"The oldest artist's age is: {oldest}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Some kind of error occured");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        internal static void GetLongestSong()
        {
            try
            {
                var oldest = _rest.GetSingle<Song>("/Stat/LongestSong");
                Console.WriteLine($"The longest song is: {oldest.Title} by {oldest.Artist.Name}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Some kind of error occured");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        internal static void GetArtistHomeCity()
        {
            try
            {
                Console.Write("Enter the name of the artist: ");
                string artistName = Console.ReadLine();
                var homeCity = _rest.GetString<string>(artistName, "/Stat/ArtistHomeCity?artistName=");
                Console.WriteLine($"The home town of {artistName} is: {homeCity}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Some kind of error occured");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        internal static void GetArtistWithMostSongs()
        {
            try
            {
                var mostSongArtist = _rest.GetSingle<Artist>("/Stat/ArtistWithMostSongs").Name;
                Console.WriteLine($"The artist with most songs is: {mostSongArtist}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Some kind of error occured");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        internal static void GetAvgSongLengthForArtist()
        {
            try
            {
                Console.Write("Enter the name of the artist: ");
                string artistName = Console.ReadLine();
                var avgLenght = _rest.GetString<double?>(artistName, "/Stat/AvgSongLengthForArtist?artistName=");
                Console.WriteLine($"The average song-length of {artistName} is: {avgLenght}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Some kind of error occured");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        internal static void GetMinutesListenedToPublisher()
        {
            try
            {
                Console.Write("Enter the name of the studio (=publisher): ");
                string studioName = Console.ReadLine();
                var minutes = _rest.GetString<string>(studioName, "/Stat/MinutesListenedToPublisher?publisherName=");
                Console.WriteLine($"The total amount of minutes listened to {studioName} is: {minutes}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Some kind of error occured");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        internal static void MostPopularArtist()
        {
        }

        internal static void MostPopularSongOfArtist(string artistName)
        {
        }
        #endregion

    }
}
