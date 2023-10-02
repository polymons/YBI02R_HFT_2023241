using System;
using System.Reflection;

namespace YBI02R_HFT_2023241.Repository
{
    public class DbContext
    {
        public DbContext() { }

        public void OnModelCreating()
        {
//          töltse fel az
//          adatbázist minden indításkor tesztadatokkal! A felhasználó tudjon indításkor már meglévő
//          adatokból elindulni
        }
    }
}
