using IPNBTV_HFT_2023242.Models;
using System.Windows;

namespace IPNBTV_HFT_2023242.WpfClient
{
    public partial class EventEditorWindow : Window
    {
        public EventEditorWindow(RentalEvent rentalEvent)
        {
            InitializeComponent();
            this.DataContext = rentalEvent;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}