using IPNBTV_HFT_2023242.Models;

namespace IPNBTV_HFT_2023242.WpfClient.Services
{
    public interface IEditorService
    {
        bool EditCar(Car car);
        bool EditRentalShop(RentalShop shop);
        bool EditRentalEvent(RentalEvent rentalEvent);
    }
}