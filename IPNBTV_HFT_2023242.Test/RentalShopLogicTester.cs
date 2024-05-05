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
                    RentalShopId = 1
                },
                new Car()
                {
                    Id = 2,
                    Brand = "Volkswagen",
                    Model = "Golf",
                    Year = 2021,
                    Color = "blue",
                    IsAutomatic = false,
                    RentalShopId = 1
                },
                new Car()
                {
                    Id = 3,
                    Brand = "Ford",
                    Model = "Focus",
                    Year = 2020,
                    Color = "silver",
                    IsAutomatic = true,
                    RentalShopId = 1
                },
                new Car()
                {
                    Id = 4,
                    Brand = "BMW",
                    Model = "320i",
                    Year = 2023,
                    Color = "black",
                    IsAutomatic = true,
                    RentalShopId = 2
                },
                new Car()
                {
                    Id = 5,
                    Brand = "Audi",
                    Model = "A4",
                    Year = 2022,
                    Color = "red",
                    IsAutomatic = false,
                    RentalShopId = 2
                },
                new Car()
                {
                    Id = 6,
                    Brand = "Mercedes-Benz",
                    Model = "C osztály",
                    Year = 2021,
                    Color = "silver",
                    IsAutomatic = true,
                    RentalShopId = 3
                },
                new Car()
                {
                    Id = 7,
                    Brand = "Renault",
                    Model = "Megane",
                    Year = 2020,
                    Color = "white",
                    IsAutomatic = false,
                    RentalShopId = 3
                }
            }.AsQueryable();

            mockShopRepo = new Mock<IRepository<RentalShop>>();
            mockCarRepo = new Mock<IRepository<Car>>();

            mockShopRepo.Setup(t => t.ReadAll()).Returns(shopInputdata);
            mockCarRepo.Setup(t => t.ReadAll()).Returns(carInputdata);

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
    }
}
