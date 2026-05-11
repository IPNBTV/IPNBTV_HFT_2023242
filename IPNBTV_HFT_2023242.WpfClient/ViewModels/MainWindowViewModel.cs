using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IPNBTV_HFT_2023242.Models;
using IPNBTV_HFT_2023242.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace IPNBTV_HFT_2023242.WpfClient.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        private RestService rest;
        private IEditorService editorService;

        [ObservableProperty] private ObservableCollection<Car> cars;
        [ObservableProperty] private ObservableCollection<RentalShop> rentalShops;
        [ObservableProperty] private ObservableCollection<RentalEvent> rentalEvents;

        [ObservableProperty][NotifyCanExecuteChangedFor(nameof(UpdateCarCommand), nameof(DeleteCarCommand))] private Car selectedCar;
        [ObservableProperty][NotifyCanExecuteChangedFor(nameof(UpdateShopCommand), nameof(DeleteShopCommand))] private RentalShop selectedShop;
        [ObservableProperty][NotifyCanExecuteChangedFor(nameof(UpdateEventCommand), nameof(DeleteEventCommand))] private RentalEvent selectedEvent;

        [ObservableProperty] private int searchShopId = 1;
        [ObservableProperty] private int searchCarId = 1;
        [ObservableProperty] private string searchColor = "white";
        [ObservableProperty] private DateTime searchStartDate = new DateTime(2022, 1, 1);
        [ObservableProperty] private DateTime searchEndDate = new DateTime(2024, 1, 1);
        [ObservableProperty] private string queryResult = "Run a query to see the results here...";

        public MainWindowViewModel(IEditorService editorService)
        {
            this.editorService = editorService;

            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                rest = new RestService("http://localhost:34728/");
                RestoreBaseImages();
                RefreshAll();
            }
        }

        public MainWindowViewModel() : this(new EditorService()) { }

        private void RefreshAll()
        {
            Cars = new ObservableCollection<Car>(rest.Get<Car>("car"));
            RentalShops = new ObservableCollection<RentalShop>(rest.Get<RentalShop>("rentalshop"));
            RentalEvents = new ObservableCollection<RentalEvent>(rest.Get<RentalEvent>("rentalevent"));
        }

        //car
        private bool CanExecuteCarAction() => SelectedCar != null;

        [RelayCommand]
        public void CreateCar()
        {
            var newItem = new Car();
            if (editorService.EditCar(newItem))
            {
                rest.Post(newItem, "car");
                RefreshAll();

                string tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "temp.jpg");
                if (File.Exists(tempPath))
                {
                    int newId = Cars.Count > 0 ? Cars.Max(x => x.Id) : 1;
                    string finalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", $"{newId}.jpg");

                    File.Copy(tempPath, finalPath, overwrite: true);
                    File.Delete(tempPath);

                    SelectedCar = Cars.FirstOrDefault(c => c.Id == newId);
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteCarAction))]
        public void UpdateCar()
        {
            var clone = new Car() { Id = SelectedCar.Id, Brand = SelectedCar.Brand, Model = SelectedCar.Model, Year = SelectedCar.Year, Color = SelectedCar.Color, IsAutomatic = SelectedCar.IsAutomatic, RentalShopId = SelectedCar.RentalShopId };
            if (editorService.EditCar(clone)) { rest.Put(clone, "car"); RefreshAll(); }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteCarAction))]
        public void DeleteCar()
        {
            int idToDelete = SelectedCar.Id;

            rest.Delete(idToDelete, "car");
            RefreshAll();

            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", $"{idToDelete}.jpg");
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        //rental shop
        private bool CanExecuteShopAction() => SelectedShop != null;

        [RelayCommand]
        public void CreateShop()
        {
            var newItem = new RentalShop();
            if (editorService.EditRentalShop(newItem)) { rest.Post(newItem, "rentalshop"); RefreshAll(); }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteShopAction))]
        public void UpdateShop()
        {
            var clone = new RentalShop() { Id = SelectedShop.Id, Name = SelectedShop.Name, City = SelectedShop.City, Address = SelectedShop.Address, Phone = SelectedShop.Phone };
            if (editorService.EditRentalShop(clone)) { rest.Put(clone, "rentalshop"); RefreshAll(); }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteShopAction))]
        public void DeleteShop() { rest.Delete(SelectedShop.Id, "rentalshop"); RefreshAll(); }

        // rental event
        private bool CanExecuteEventAction() => SelectedEvent != null;

        [RelayCommand]
        public void CreateEvent()
        {
            var newItem = new RentalEvent() { RentalDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(1) };
            if (editorService.EditRentalEvent(newItem)) { rest.Post(newItem, "rentalevent"); RefreshAll(); }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteEventAction))]
        public void UpdateEvent()
        {
            var clone = new RentalEvent() { Id = SelectedEvent.Id, CarId = SelectedEvent.CarId, RentalDate = SelectedEvent.RentalDate, ReturnDate = SelectedEvent.ReturnDate, CustomerName = SelectedEvent.CustomerName };
            if (editorService.EditRentalEvent(clone)) { rest.Put(clone, "rentalevent"); RefreshAll(); }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteEventAction))]
        public void DeleteEvent() { rest.Delete(SelectedEvent.Id, "rentalevent"); RefreshAll(); }


        // non-crud

        [RelayCommand]
        public void GetMostRentedCarInRentalShop()
        {
            var result = rest.Get<IEnumerable<MostRentedCarInfo>>(SearchShopId, "noncrud/getmostrentedcarinrentalshop").FirstOrDefault();
            if (result != null)
                QueryResult = $"Most Rented Car in Shop [{SearchShopId}]:\n\nBrand: {result.Car.Brand}\nModel: {result.Car.Model}\nTotal Rentals: {result.NumberOfRentalEvents}";
            else
                QueryResult = "No data found for this shop.";
        }

        [RelayCommand]
        public void GetNumberOfRentalEventsForCarInShop()
        {
            var result = rest.GetSingle<int>(SearchCarId, SearchShopId, "noncrud/getnumberofrentaleventsforcarinshop");
            QueryResult = $"Car [{SearchCarId}] in Shop [{SearchShopId}] has been rented:\n\n{result} times.";
        }

        [RelayCommand]
        public void OrderByMostRentals()
        {
            var result = rest.Get<IEnumerable<MostRentedCarInfo>>(SearchStartDate, SearchEndDate, "noncrud/orderbymostrentals").ToList();
            if (result.Count > 0)
                QueryResult = $"Most Rentals between {SearchStartDate:yyyy-MM-dd} and {SearchEndDate:yyyy-MM-dd}:\n\n" +
                              string.Join("\n", result.Select((x, index) => $"{index + 1}. {x.Car.Brand} {x.Car.Model} - {x.NumberOfRentalEvents} rentals"));
            else
                QueryResult = "No rentals found in this period.";
        }

        [RelayCommand]
        public void GetCustomersWithAutomaticTransmission()
        {
            var result = rest.GetSingle<IEnumerable<string>>("noncrud/getcustomerswithautomatictransmission").ToList();
            if (result.Count > 0)
                QueryResult = "Customers who rented automatic cars:\n\n" + string.Join("\n", result.Select(x => "• " + x));
            else
                QueryResult = "No customers found.";
        }

        [RelayCommand]
        public void GetRentalShopsWithCarColor()
        {
            var result = rest.Get<IEnumerable<RentalShop>>(SearchColor, "noncrud/getrentalshopswithcarcolor").ToList();
            if (result.Count > 0)
                QueryResult = $"Rental Shops with '{SearchColor}' cars:\n\n" + string.Join("\n\n", result.Select(x => $"🏢 {x.Name}\n📍 {x.City}, {x.Address}"));
            else
                QueryResult = $"No shops found with '{SearchColor}' cars.";
        }


        private void RestoreBaseImages()
        {
            try
            {
                
                string projectImagesFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Images"));

                
                string targetFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

                if (Directory.Exists(projectImagesFolder))
                {
                    if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);

                    
                    foreach (string sourceFile in Directory.GetFiles(projectImagesFolder, "*.jpg"))
                    {
                        string fileName = Path.GetFileName(sourceFile);
                        string destFile = Path.Combine(targetFolder, fileName);
                        File.Copy(sourceFile, destFile, overwrite: true);
                    }
                }
            }
            catch
            {
                
            }
        }
    }


    public class MostRentedCarInfo
    {
        public Car Car { get; set; }
        public int NumberOfRentalEvents { get; set; }
    }
}