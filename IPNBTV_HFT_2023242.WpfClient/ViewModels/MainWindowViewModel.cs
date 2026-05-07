using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IPNBTV_HFT_2023242.Models;
using IPNBTV_HFT_2023242.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public MainWindowViewModel(IEditorService editorService)
        {
            this.editorService = editorService;

            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                rest = new RestService("http://localhost:34728/");
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
            if (editorService.EditCar(newItem)) { rest.Post(newItem, "car"); RefreshAll(); }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteCarAction))]
        public void UpdateCar()
        {
            var clone = new Car() { Id = SelectedCar.Id, Brand = SelectedCar.Brand, Model = SelectedCar.Model, Year = SelectedCar.Year, Color = SelectedCar.Color, IsAutomatic = SelectedCar.IsAutomatic, RentalShopId = SelectedCar.RentalShopId };
            if (editorService.EditCar(clone)) { rest.Put(clone, "car"); RefreshAll(); }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteCarAction))]
        public void DeleteCar() { rest.Delete(SelectedCar.Id, "car"); RefreshAll(); }

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
                MessageBox.Show($"Car: {result.Car.Brand} {result.Car.Model}\nRentals: {result.NumberOfRentalEvents}", "Most Rented Car", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No data found.", "Result", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        [RelayCommand]
        public void GetNumberOfRentalEventsForCarInShop()
        {
            var result = rest.GetSingle<int>(SearchCarId, SearchShopId, "noncrud/getnumberofrentaleventsforcarinshop");
            MessageBox.Show($"Number of events: {result}", "Rental Count", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        public void OrderByMostRentals()
        {
            var result = rest.Get<IEnumerable<MostRentedCarInfo>>(SearchStartDate, SearchEndDate, "noncrud/orderbymostrentals").ToList();
            var text = string.Join("\n", result.Select(x => $"{x.Car.Brand} {x.Car.Model} - {x.NumberOfRentalEvents} rentals"));
            MessageBox.Show(text, "Most Rentals in Period", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        public void GetCustomersWithAutomaticTransmission()
        {
            var result = rest.GetSingle<IEnumerable<string>>("noncrud/getcustomerswithautomatictransmission").ToList();
            MessageBox.Show(string.Join("\n", result), "Automatic Car Customers", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        public void GetRentalShopsWithCarColor()
        {
            var result = rest.Get<IEnumerable<RentalShop>>(SearchColor, "noncrud/getrentalshopswithcarcolor").ToList();
            var text = string.Join("\n", result.Select(x => $"{x.Name} ({x.City})"));
            MessageBox.Show(text, $"Shops with {SearchColor} cars", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }


    public class MostRentedCarInfo
    {
        public Car Car { get; set; }
        public int NumberOfRentalEvents { get; set; }
    }
}