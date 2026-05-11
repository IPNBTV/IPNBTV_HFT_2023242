using IPNBTV_HFT_2023242.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace IPNBTV_HFT_2023242.WpfClient
{
    public partial class CarEditorWindow : Window
    {
        private Car currentCar;
        private string selectedImagePath = null;

        public CarEditorWindow(Car car)
        {
            InitializeComponent();
            this.currentCar = car;
            this.DataContext = currentCar;
        }


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Image|*.jpg";

            if (ofd.ShowDialog() == true)
            {
                selectedImagePath = ofd.FileName;
                SelectedFileLabel.Content = Path.GetFileName(selectedImagePath);
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedImagePath != null)
            {
                string targetFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);

                if (currentCar.Id == 0)
                {
                    string tempPath = Path.Combine(targetFolder, "temp.jpg");
                    File.Copy(selectedImagePath, tempPath, overwrite: true);
                }
                else
                {
                    string targetPath = Path.Combine(targetFolder, $"{currentCar.Id}.jpg");
                    File.Copy(selectedImagePath, targetPath, overwrite: true);
                }
            }

            this.DialogResult = true;
        }
    }
}