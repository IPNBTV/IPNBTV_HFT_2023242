using IPNBTV_HFT_2023242.Models;
using System.Windows;

namespace IPNBTV_HFT_2023242.WpfClient
{
    public partial class CarEditorWindow : Window
    {
        public CarEditorWindow(Car car)
        {
            InitializeComponent();
            this.DataContext = car;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
