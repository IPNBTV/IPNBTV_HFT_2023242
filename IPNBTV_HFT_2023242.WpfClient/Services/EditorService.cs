using IPNBTV_HFT_2023242.Models;

namespace IPNBTV_HFT_2023242.WpfClient.Services
{
    public class EditorService : IEditorService
    {
        public bool EditCar(Car car)
        {
            var win = new CarEditorWindow(car);
            return win.ShowDialog() == true;
        }

        public bool EditRentalShop(RentalShop shop)
        {
            var win = new ShopEditorWindow(shop);
            return win.ShowDialog() == true;
        }

        public bool EditRentalEvent(RentalEvent rentalEvent)
        {
            var win = new EventEditorWindow(rentalEvent);
            return win.ShowDialog() == true;
        }
    }
}
