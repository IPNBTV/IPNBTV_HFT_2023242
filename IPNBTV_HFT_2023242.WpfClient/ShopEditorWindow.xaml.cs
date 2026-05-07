using IPNBTV_HFT_2023242.Models;
using System.Windows;

namespace IPNBTV_HFT_2023242.WpfClient
{
    public partial class ShopEditorWindow : Window
    {
        public ShopEditorWindow(RentalShop shop)
        {
            InitializeComponent();
            this.DataContext = shop;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}