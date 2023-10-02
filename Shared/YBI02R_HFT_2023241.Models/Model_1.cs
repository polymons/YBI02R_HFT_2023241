using System;
using System.Collections.Generic;

namespace YBI02R_HFT_2023241.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }

        // Navigation Property a Car entitáshoz
        public List<Car> Cars { get; set; }
    }

    public class Car
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public int BrandId { get; set; } // Idegen kulcs a márka azonosítójára

        // Navigation Property a márkahoz
        public Brand Brand { get; set; }

        // Navigation Property a bérlésekhez
        public List<Rental> Rentals { get; set; }
    }

    public class Rental
    {
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public int CarId { get; set; } // Idegen kulcs az autó azonosítójára

        // Navigation Property az autóhoz
        public Car Car { get; set; }
    }
}
