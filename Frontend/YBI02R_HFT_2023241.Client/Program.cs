using ConsoleTools;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Client
{
    internal class Program
    {
        static RestService _rest;
        static ClientRequests _client;

        static void Main(string[] args)
        {
            _rest = new RestService("http://localhost:53910/", "swagger");
            _client = new ClientRequests(_rest);

            var mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Songs", () => EntityMenu(args,"Song").Show())
                .Add("Artists", () => EntityMenu(args, "Artist").Show())
                .Add("Publishers", () => EntityMenu(args, "Publisher").Show())
                //.Add("Stats", () => subMenu(args).Show())
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
                .Add("List", () => _client.Read(entity))
                .Add("Create", () => _client.Create(entity))
                .Add("Update", () => _client.Update(entity))
                .Add("Delete", () => _client.Delete(entity))
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
                    .Add("Oldest artist", () => _client.GetOldestArtistAge())
                    .Add("Artist home town", () => _client.GetArtistHomeCity())
                    .Add("Artist with most songs", () => _client.GetArtistWithMostSongs())
                    .Add("Average song length for artist", () => _client.GetAvgSongLengthForArtist())
                    .Add("Most popular artist", () => _client.GetMostPopularArtist())
                    .Add("Exit", ConsoleMenu.Close);
                case ("Song"):
                    return new ConsoleMenu(args, level: 2)
                    .Add("Longest song", () => _client.GetLongestSong())
                    .Add("Most popular song of artist", () => _client.GetMostPopularSongOfArtist())
                    .Add("Exit", ConsoleMenu.Close);
                case ("Publisher"):
                    return new ConsoleMenu(args, level: 2)
                    .Add("Minutes listened to studio", () => _client.GetMinutesListenedToPublisher())
                    .Add("Exit", ConsoleMenu.Close);
                default:
                     return new ConsoleMenu(args, level: 2)
                    .Add("Exit", ConsoleMenu.Close);
            }
        }
        #endregion



    }
}
