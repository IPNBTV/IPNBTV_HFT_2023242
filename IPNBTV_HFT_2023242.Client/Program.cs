using ConsoleTools;
using IPNBTV_HFT_2023242.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace IPNBTV_HFT_2023242.Client
{
    class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            if (entity == "RentalShop")
            {
                Console.Write("Enter RentalShop Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter RentalShop City: ");
                string city = Console.ReadLine();

                Console.Write("Enter RentalShop Address: ");
                string address = Console.ReadLine();

                Console.Write("Enter RentalShop Phone: ");
                string phone = Console.ReadLine();

                rest.Post(new RentalShop()
                {
                    Name = name,
                    City = city,
                    Address = address,
                    Phone = phone
                }, "rentalshop");
            }
            else if (entity == "Car")
            {
                Console.Write("Enter Car Brand: ");
                string brand = Console.ReadLine();

                Console.Write("Enter Car Model: ");
                string model = Console.ReadLine();

                Console.Write("Enter Car Year: ");
                int year = int.Parse(Console.ReadLine());

                Console.Write("Enter Car Color: ");
                string color = Console.ReadLine();

                Console.Write("Enter Car IsAutomatic: ");
                bool isautomatic = bool.Parse(Console.ReadLine());

                Console.Write("Enter Car RentalShopId: ");
                int rentalshopid = int.Parse(Console.ReadLine());

                rest.Post(new Car()
                {
                    Brand = brand,
                    Model = model,
                    Year = year,
                    Color = color,
                    IsAutomatic = isautomatic,
                    RentalShopId = rentalshopid
                }, "car");
            }
            else if (entity == "RentalEvent")
            {
                Console.Write("Enter RentalEvent CarId: ");
                int carid = int.Parse(Console.ReadLine());

                Console.Write("Enter RentalEvent RentalDate: ");
                DateTime rentaldate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter RentalEvent ReturnDate: ");
                DateTime returndate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter RentalEvent CustomerName: ");
                string customername = Console.ReadLine();

                rest.Post(new RentalEvent()
                {
                    CarId = carid,
                    RentalDate = rentaldate,
                    ReturnDate = returndate,
                    CustomerName = customername
                }, "rentalevent");
            }
        }

        static void List(string entity)
        {
            if (entity == "RentalShop")
            {
                List<RentalShop> rentalShops = rest.Get<RentalShop>("rentalshop");
                foreach (var item in rentalShops)
                {
                    Console.WriteLine($"Id: {item.Id}\n" +
                                      $"Name: {item.Name}\n" +
                                      $"City: {item.City}\n" +
                                      $"Address: {item.Address}\n" +
                                      $"Phone: {item.Phone}\n");
                }
            }
            else if (entity == "Car")
            {
                List<Car> cars = rest.Get<Car>("car");
                foreach (var item in cars)
                {
                    Console.WriteLine($"Id: {item.Id}\n" +
                                      $"Brand: {item.Brand}\n" +
                                      $"Model: {item.Model}\n" +
                                      $"Year: {item.Year}\n" +
                                      $"Color: {item.Color}\n" +
                                      $"IsAutomatic: {item.IsAutomatic}\n" +
                                      $"RentalShopId: {item.RentalShopId}\n");
                }
            }
            else if (entity == "RentalEvent")
            {
                List<RentalEvent> rentalEvents = rest.Get<RentalEvent>("rentalevent");
                foreach (var item in rentalEvents)
                {
                    Console.WriteLine($"Id: {item.Id}\n" +
                                      $"CarId: {item.CarId}\n" +
                                      $"RentalDate: {item.RentalDate}\n" +
                                      $"ReturnDate: {item.ReturnDate}\n" +
                                      $"CustomerName: {item.CustomerName}\n");
                }
            }
            Console.ReadLine();
        }

        static void Update(string entity)
        {
            if (entity == "RentalShop")
            {
                Console.Write("Enter RentalShop's id to update: ");
                int id = int.Parse(Console.ReadLine());
                RentalShop one = rest.Get<RentalShop>(id, "rentalshop");

                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;

                Console.Write($"New city [old: {one.City}]: ");
                string city = Console.ReadLine();
                one.City = city;

                Console.Write($"New address [old: {one.Address}]: ");
                string address = Console.ReadLine();
                one.Address = address;

                Console.Write($"New phone [old: {one.Phone}]: ");
                string phone = Console.ReadLine();
                one.Phone = phone;

                rest.Put(one, "rentalshop");
            }
            else if (entity == "Car")
            {
                Console.Write("Enter Car's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Car one = rest.Get<Car>(id, "car");

                Console.Write($"New brand [old: {one.Brand}]: ");
                string brand = Console.ReadLine();
                one.Brand = brand;

                Console.Write($"New model [old: {one.Model}]: ");
                string model = Console.ReadLine();
                one.Model = model;

                Console.Write($"New year [old: {one.Year}]: ");
                int year = int.Parse(Console.ReadLine());
                one.Year = year;

                Console.Write($"New color [old: {one.Color}]: ");
                string color = Console.ReadLine();
                one.Color = color;

                Console.Write($"New isautomatic [old: {one.IsAutomatic}]: ");
                bool isautomatic = bool.Parse(Console.ReadLine());
                one.IsAutomatic = isautomatic;

                Console.Write($"New rentalshopid [old: {one.RentalShopId}]: ");
                int rentalshopid = int.Parse(Console.ReadLine());
                one.RentalShopId = rentalshopid;

                rest.Put(one, "car");
            }
            else if (entity == "RentalEvent")
            {
                Console.Write("Enter RentalEvent's id to update: ");
                int id = int.Parse(Console.ReadLine());
                RentalEvent one = rest.Get<RentalEvent>(id, "rentalevent");

                Console.Write($"New carid [old: {one.CarId}]: ");
                int carid = int.Parse(Console.ReadLine());
                one.CarId = carid;

                Console.Write($"New rentaldate [old: {one.RentalDate}]: ");
                DateTime rentaldate = DateTime.Parse(Console.ReadLine());
                one.RentalDate = rentaldate;

                Console.Write($"New returndate [old: {one.ReturnDate}]: ");
                DateTime returndate = DateTime.Parse(Console.ReadLine());
                one.ReturnDate = returndate;

                Console.Write($"New customername [old: {one.CustomerName}]: ");
                string customername = Console.ReadLine();
                one.CustomerName = customername;

                rest.Put(one, "rentalevent");
            }
        }

        static void Delete(string entity)
        {
            if (entity == "RentalShop")
            {
                Console.Write("Enter RentalShop's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "rentalshop");
            }
            else if (entity == "Car")
            {
                Console.Write("Enter Car's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "car");
            }
            else if (entity == "RentalEvent")
            {
                Console.Write("Enter RentalEvent's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "rentalevent");
            }
        }

        static void GetMostRentedCarInRentalShop()
        {
            Console.Write("Enter RentalShop's id: ");
            int id = int.Parse(Console.ReadLine());

            MostRentedCarInfo result = rest.Get<IEnumerable<MostRentedCarInfo>>(id, "noncrudcar/getmostrentedcarinrentalshop").ToList().FirstOrDefault();

            Console.WriteLine($"\nMost rented car in rentalshop[{id}]:" +
                              $"\nId: {result.Car.Id}" +
                              $"\nBrand: {result.Car.Brand}" +
                              $"\nModel: {result.Car.Model}" +
                              $"\nYear: {result.Car.Year}" +
                              $"\nColor: {result.Car.Color}" +
                              $"\nIsAutomatic: {result.Car.IsAutomatic}" +
                              $"\n\nNumber of rental events: {result.NumberOfRentalEvents}");
            Console.ReadLine();
        }

        static void GetNumberOfRentalEventsForCarInShop()
        {
            Console.Write("Enter Car's id: ");
            int carId = int.Parse(Console.ReadLine());
            Console.Write("Enter RentalShop's id: ");
            int rentalShopId = int.Parse(Console.ReadLine());
            Console.WriteLine($"\nNumber of rentalevents for car[{carId}] in shop[{rentalShopId}]:" +
                $" {rest.GetSingle<int>(carId, rentalShopId, "noncrudcar/getnumberofrentaleventsforcarinshop")}");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:34728/", "swagger");

            var rentalshopSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("RentalShop"))
                .Add("Create", () => Create("RentalShop"))
                .Add("Delete", () => Delete("RentalShop"))
                .Add("Update", () => Update("RentalShop"))
                .Add("Exit", ConsoleMenu.Close);

            var carSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Car"))
                .Add("Create", () => Create("Car"))
                .Add("Delete", () => Delete("Car"))
                .Add("Update", () => Update("Car"))
                .Add("GetMostRentedCarsInRentalShop", () => GetMostRentedCarInRentalShop())
                .Add("GetNumberOfRentalEventsForCarInShop", ()=> GetNumberOfRentalEventsForCarInShop())
                .Add("Exit", ConsoleMenu.Close);

            var rentaleventSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("RentalEvent"))
                .Add("Create", () => Create("RentalEvent"))
                .Add("Delete", () => Delete("RentalEvent"))
                .Add("Update", () => Update("RentalEvent"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("RentalShops", () => rentalshopSubMenu.Show())
                .Add("Cars", () => carSubMenu.Show())
                .Add("RentalEvents", () => rentaleventSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }

    public class MostRentedCarInfo
    {
        public Car Car { get; set; }
        public int NumberOfRentalEvents { get; set; }

        public override bool Equals(object obj)
        {
            MostRentedCarInfo b = obj as MostRentedCarInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return Car.Id == b.Car.Id
                    && Car.Brand == b.Car.Brand
                    && Car.Model == b.Car.Model
                    && Car.Year == b.Car.Year
                    && Car.Color == b.Car.Color
                    && Car.IsAutomatic == b.Car.IsAutomatic
                    && Car.RentalShopId == b.Car.RentalShopId
                    && NumberOfRentalEvents == b.NumberOfRentalEvents;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Car, NumberOfRentalEvents);
        }
    }
}
