using IPNBTV_HFT_2023242.Logic;
using IPNBTV_HFT_2023242.Models;
using IPNBTV_HFT_2023242.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNBTV_HFT_2023242.Test
{
    [TestFixture]
    public class RentalShopLogicTester
    {
        IRentalShopLogic shopLogic;

        Mock<IRepository<RentalShop>> mockShopRepo;
        Mock<IRepository<Car>> mockCarRepo;

        [SetUp]
        public void Init()
        {
            var shopInputdata = new List<RentalShop>()
            {
                new RentalShop()
                {
                    Id = 1,
                    Name = "AutóExpressz Kölcsönző",
                    City = "Budapest",
                    Address = "Kossuth Lajos utca 12.",
                    Phone = "+36 20 234 5678"
                },
                new RentalShop()
                {
                    Id = 2,
                    Name = "CityRent Autókölcsönző",
                    City = "Debrecen",
                    Address = "Piac utca 5.",
                    Phone = "+36 30 345 6789"
                },
                new RentalShop()
                {
                    Id = 3,
                    Name = "PremiumAutó Kölcsönző",
                    City = "Szeged",
                    Address = "Dugonics tér 8.",
                    Phone = "+36 70 456 7890"
                }
            }.AsQueryable();
            var carInputdata = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2022,
                    Color = "white",
                    IsAutomatic = true,
                    RentalShopId = 1,
                    RentalShop = new RentalShop()
                    {
                        Id = 1,
                        Name = "AutóExpressz Kölcsönző",
                        City = "Budapest",
                        Address = "Kossuth Lajos utca 12.",
                        Phone = "+36 20 234 5678"
                    },
                    RentalEvents = new List<RentalEvent>()
                    {
                        new RentalEvent()
                        {
                            Id = 1,
                            CarId = 1,
                            RentalDate = DateTime.Parse("2022. 05. 01. 10:00:00"),
                            ReturnDate = DateTime.Parse("2022. 05. 05. 15:00:00"),
                            CustomerName = "Kovács Péter",
                            Car = new Car()
                            {
                                Id = 1,
                                Brand = "Toyota",
                                Model = "Corolla",
                                Year = 2022,
                                Color = "white",
                                IsAutomatic = true,
                                RentalShopId = 1
                            }
                        },
                        new RentalEvent()
                        {
                            Id = 2,
                            CarId = 1,
                            RentalDate = DateTime.Parse("2022. 05. 10. 08:30:00"),
                            ReturnDate = DateTime.Parse("2022. 05. 15. 12:30:00"),
                            CustomerName = "Nagy Márta",
                            Car = new Car()
                            {
                                Id = 1,
                                Brand = "Toyota",
                                Model = "Corolla",
                                Year = 2022,
                                Color = "white",
                                IsAutomatic = true,
                                RentalShopId = 1
                            }
                        }
                    }
                },
                new Car()
                {
                    Id = 2,
                    Brand = "Volkswagen",
                    Model = "Golf",
                    Year = 2021,
                    Color = "blue",
                    IsAutomatic = false,
                    RentalShopId = 1,
                    RentalShop = new RentalShop
                    {
                        Id = 1,
                        Name = "AutóExpressz Kölcsönző",
                        City = "Budapest",
                        Address = "Kossuth Lajos utca 12.",
                        Phone = "+36 20 234 5678"
                    },
                    RentalEvents = new List<RentalEvent>()
                    {
                        new RentalEvent()
                        {
                            Id = 3,
                            CarId = 2,
                            RentalDate = DateTime.Parse("2022. 05. 03. 14:45:00"),
                            ReturnDate = DateTime.Parse("2022. 05. 08. 17:45:00"),
                            CustomerName = "Szabó Gábor",
                            Car = new Car()
                            {
                                Id = 2,
                                Brand = "Volkswagen",
                                Model = "Golf",
                                Year = 2021,
                                Color = "blue",
                                IsAutomatic = false,
                                RentalShopId = 1
                            }
                        },
                        new RentalEvent()
                        {
                            Id = 4,
                            CarId = 2,
                            RentalDate = DateTime.Parse("2023. 05. 12. 09:15:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 18. 14:15:00"),
                            CustomerName = "Tóth Anna",
                            Car = new Car()
                            {
                                Id = 2,
                                Brand = "Volkswagen",
                                Model = "Golf",
                                Year = 2021,
                                Color = "blue",
                                IsAutomatic = false,
                                RentalShopId = 1
                            }
                        },
                        new RentalEvent()
                        {
                            Id = 5,
                            CarId = 2,
                            RentalDate = DateTime.Parse("2023. 05. 22. 13:30:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 26. 18:30:00"),
                            CustomerName = "Kiss István",
                            Car = new Car()
                            {
                                Id = 2,
                                Brand = "Volkswagen",
                                Model = "Golf",
                                Year = 2021,
                                Color = "blue",
                                IsAutomatic = false,
                                RentalShopId = 1
                            }
                        }
                    }
                },
                new Car()
                {
                    Id = 3,
                    Brand = "Ford",
                    Model = "Focus",
                    Year = 2020,
                    Color = "silver",
                    IsAutomatic = true,
                    RentalShopId = 1,
                    RentalShop = new RentalShop()
                    {
                        Id = 1,
                        Name = "AutóExpressz Kölcsönző",
                        City = "Budapest",
                        Address = "Kossuth Lajos utca 12.",
                        Phone = "+36 20 234 5678"
                    },
                    RentalEvents = new List<RentalEvent>()
                    {
                        new RentalEvent()
                        {
                            Id = 6,
                            CarId = 3,
                            RentalDate = DateTime.Parse("2023. 05. 05. 11:15:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 09. 10:15:00"),
                            CustomerName = "Varga Éva",
                            Car = new Car()
                            {
                                Id = 3,
                                Brand = "Ford",
                                Model = "Focus",
                                Year = 2020,
                                Color = "silver",
                                IsAutomatic = true,
                                RentalShopId = 1
                            }
                        }
                    }
                },
                new Car()
                {
                    Id = 4,
                    Brand = "BMW",
                    Model = "320i",
                    Year = 2023,
                    Color = "black",
                    IsAutomatic = true,
                    RentalShopId = 2,
                    RentalShop = new RentalShop()
                    {
                        Id = 2,
                        Name = "CityRent Autókölcsönző",
                        City = "Debrecen",
                        Address = "Piac utca 5.",
                        Phone = "+36 30 345 6789"
                    },
                    RentalEvents = new List<RentalEvent>()
                    {
                        new RentalEvent()
                        {
                            Id = 7,
                            CarId = 4,
                            RentalDate = DateTime.Parse("2023. 05. 06. 08:30:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 10. 18:45:00"),
                            CustomerName = "Papp János",
                            Car = new Car()
                            {
                                Id = 4,
                                Brand = "BMW",
                                Model = "320i",
                                Year = 2023,
                                Color = "black",
                                IsAutomatic = true,
                                RentalShopId = 2
                            }
                        }
                    }
                },
                new Car()
                {
                    Id = 5,
                    Brand = "Audi",
                    Model = "A4",
                    Year = 2022,
                    Color = "red",
                    IsAutomatic = false,
                    RentalShopId = 2,
                    RentalShop = new RentalShop()
                    {
                        Id = 2,
                        Name = "CityRent Autókölcsönző",
                        City = "Debrecen",
                        Address = "Piac utca 5.",
                        Phone = "+36 30 345 6789"
                    },
                    RentalEvents = new List<RentalEvent>()
                    {
                        new RentalEvent()
                        {
                            Id = 8,
                            CarId = 5,
                            RentalDate = DateTime.Parse("2023. 05. 07. 13:40:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 12. 11:30:00"),
                            CustomerName = "Molnár Orsolya",
                            Car = new Car()
                            {
                                Id = 5,
                                Brand = "Audi",
                                Model = "A4",
                                Year = 2022,
                                Color = "red",
                                IsAutomatic = false,
                                RentalShopId = 2
                            }
                        }
                    }
                },
                new Car()
                {
                    Id = 6,
                    Brand = "Mercedes-Benz",
                    Model = "C osztály",
                    Year = 2021,
                    Color = "silver",
                    IsAutomatic = true,
                    RentalShopId = 3,
                    RentalShop = new RentalShop()
                    {
                        Id = 3,
                        Name = "PremiumAutó Kölcsönző",
                        City = "Szeged",
                        Address = "Dugonics tér 8.",
                        Phone = "+36 70 456 7890"
                    },
                    RentalEvents = new List<RentalEvent>()
                    {
                        new RentalEvent()
                        {
                            Id = 9,
                            CarId = 6,
                            RentalDate = DateTime.Parse("2023. 05. 08. 10:45:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 13. 14:20:00"),
                            CustomerName = "Fekete Péter",
                            Car = new Car()
                            {
                                Id = 6,
                                Brand = "Mercedes-Benz",
                                Model = "C osztály",
                                Year = 2021,
                                Color = "silver",
                                IsAutomatic = true,
                                RentalShopId = 3
                            }
                        }
                    }
                },
                new Car()
                {
                    Id = 7,
                    Brand = "Renault",
                    Model = "Megane",
                    Year = 2020,
                    Color = "white",
                    IsAutomatic = false,
                    RentalShopId = 3,
                    RentalShop = new RentalShop()
                    {
                        Id = 3,
                        Name = "PremiumAutó Kölcsönző",
                        City = "Szeged",
                        Address = "Dugonics tér 8.",
                        Phone = "+36 70 456 7890"
                    },
                    RentalEvents = new List<RentalEvent>()
                    {
                        new RentalEvent()
                        {
                            Id = 10,
                            CarId = 7,
                            RentalDate = DateTime.Parse("2023. 05. 09. 12:30:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 15. 16:40:00"),
                            CustomerName = "Balogh Zsuzsanna",
                            Car = new Car()
                            {
                                Id = 7,
                                Brand = "Renault",
                                Model = "Megane",
                                Year = 2020,
                                Color = "white",
                                IsAutomatic = false,
                                RentalShopId = 3
                            }
                        },
                        new RentalEvent()
                        {
                            Id = 11,
                            CarId = 7,
                            RentalDate = DateTime.Parse("2023. 05. 17. 08:50:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 23. 16:20:00"),
                            CustomerName = "Horváth István",
                            Car = new Car()
                            {
                                Id = 7,
                                Brand = "Renault",
                                Model = "Megane",
                                Year = 2020,
                                Color = "white",
                                IsAutomatic = false,
                                RentalShopId = 3
                            }
                        },
                        new RentalEvent()
                        {
                            Id = 12,
                            CarId = 7,
                            RentalDate = DateTime.Parse("2023. 05. 25. 13:20:00"),
                            ReturnDate = DateTime.Parse("2023. 05. 31. 11:15:00"),
                            CustomerName = "Németh Ágnes",
                            Car = new Car()
                            {
                                Id = 7,
                                Brand = "Renault",
                                Model = "Megane",
                                Year = 2020,
                                Color = "white",
                                IsAutomatic = false,
                                RentalShopId = 3
                            }
                        }
                    }
                }
            }.AsQueryable();

            mockShopRepo = new Mock<IRepository<RentalShop>>();
            mockCarRepo = new Mock<IRepository<Car>>();

            mockShopRepo.Setup(t => t.ReadAll()).Returns(shopInputdata);
            mockCarRepo.Setup(t => t.ReadAll()).Returns(carInputdata);

            mockCarRepo.Setup(t => t.Read(It.IsAny<int>())).Returns<int>(id =>
            {
                return carInputdata.FirstOrDefault(car => car.Id == id);
            });

            mockShopRepo.Setup(t => t.Read(It.IsAny<int>())).Returns<int>(id =>
            {
                return shopInputdata.FirstOrDefault(shop => shop.Id == id);
            });

            shopLogic = new RentalShopLogic(mockShopRepo.Object, mockCarRepo.Object);
        }

        [Test]
        public void ShopCreateTest1()
        {
            //ARRANGE
            var newShop = new RentalShop
            {
                Id = 4,
                Name = "Zöld Autókölcsönző",
                City = "Napfényfalva",
                Address = "Öko út 22.",
                Phone = "+36 50 234 5678"
            };

            //ACT
            shopLogic.Create(newShop);

            //ASSERT
            mockShopRepo.Verify(t => t.Create(newShop), Times.Once);
        }

        [Test]
        public void ShopCreateTest2()
        {
            //ARRANGE
            var newShop = new RentalShop
            {
                Id = 4,
                Name = "Z",
                City = "Napfényfalva",
                Address = "Öko út 22.",
                Phone = "+36 50 234 5678"
            };

            //ACT
            //ASSERT
            Assert.Throws<ArgumentException>(() => shopLogic.Create(newShop));
        }

        [Test]
        public void ShopCreateTest3()
        {
            //ARRANGE
            var newShop = new RentalShop
            {
                Id = 0,
                Name = "Zöld Autókölcsönző",
                City = "Napfényfalva",
                Address = "Öko út 22.",
                Phone = "+36 50 234 5678"
            };

            //ACT
            //ASSERT
            Assert.Throws<ArgumentException>(() => shopLogic.Create(newShop));
        }

        [Test]
        public void GetRentalShopsWithCarColorTest()
        {
            var actual = shopLogic.GetRentalShopsWithCarColor("silver").ToList();

            var expected = new List<RentalShop>
            {
                new RentalShop
                {
                    Id = 1,
                    Name = "AutóExpressz Kölcsönző",
                    City = "Budapest",
                    Address = "Kossuth Lajos utca 12.",
                    Phone = "+36 20 234 5678"
                },
                new RentalShop
                {
                    Id = 3,
                    Name = "PremiumAutó Kölcsönző",
                    City = "Szeged",
                    Address = "Dugonics tér 8.",
                    Phone = "+36 70 456 7890"
                }
            };

            Assert.AreEqual(expected, actual);
        }
    }
}
