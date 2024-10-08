using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace MunicipleServiceApp
{
    public partial class ReportIssuesWindow : Window
    {
        private string? selectedImagePath;
        private byte[]? imageBytes;
        private int submissionCount = 0; 

        
        private BitmapImage Pot1 { get; set; }
        private BitmapImage Pot2 { get; set; }
        private BitmapImage Pot3 { get; set; }
        private BitmapImage Pot4 { get; set; }
        private BitmapImage Pot5 { get; set; }

        public ReportIssuesWindow()
        {
            InitializeComponent();

            Pot1 = new BitmapImage(new Uri("pack://application:,,,/Images/Pot1.png"));
            Pot2 = new BitmapImage(new Uri("pack://application:,,,/Images/Pot2.png"));
            Pot3 = new BitmapImage(new Uri("pack://application:,,,/Images/Pot3.png"));
            Pot4 = new BitmapImage(new Uri("pack://application:,,,/Images/Pot4.png"));
            Pot5 = new BitmapImage(new Uri("pack://application:,,,/Images/Pot5.png"));

            imgPot.Source = Pot1;

            cboxCategory.Items.Add("Water Leak");
            cboxCategory.Items.Add("Electricity Outage");
            cboxCategory.Items.Add("Potholes");
            cboxCategory.Items.Add("Road Blockage");
            cboxCategory.Items.Add("Illegal Dumping");
        }

        private void BtnAttachImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select an Image or File",
                Filter = "All Files|*.*|Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                txtSelectedFile.Visibility = Visibility.Collapsed;
                imgSelectedFile.Visibility = Visibility.Collapsed;

                try
                {
                    string extension = Path.GetExtension(selectedImagePath).ToLower();

                    
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp")
                    {
                        imageBytes = File.ReadAllBytes(selectedImagePath);
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(selectedImagePath);
                        bitmap.EndInit();

                        imgSelectedFile.Source = bitmap;
                        imgSelectedFile.Visibility = Visibility.Visible; 
                    }
                    else
                    {
                        
                        imageBytes = null; 
                        txtSelectedFile.Text = Path.GetFileName(selectedImagePath);
                        txtSelectedFile.Visibility = Visibility.Visible; 
                    }

                    MessageBox.Show("File attached successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
          
            if (string.IsNullOrEmpty(txtboxLocation.Text) ||
                cboxCategory.SelectedItem == null ||
                string.IsNullOrWhiteSpace(new TextRange(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd).Text))
            {
                MessageBox.Show("Please fill out all fields before submitting.", "Incomplete Form", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("Please attach a file or image before submitting.", "File Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            string location = txtboxLocation.Text;
            string category = cboxCategory.SelectedItem.ToString();
            string description = new TextRange(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd).Text.Trim();

            if (imageBytes != null)
            {
                SaveReport(location, category, description, imageBytes);
            }
            else
            {
                SaveReport(location, category, description, null);
            }
            switch (submissionCount)
            {
                case 0:
                    imgPot.Source = Pot2; 
                    break;
                case 1:
                    imgPot.Source = Pot3; 
                    break;
                case 2:
                    imgPot.Source = Pot4; 
                    break;
                case 3:
                    imgPot.Source = Pot5; 
                    break;
                case 4:
                    btnSubmit.IsEnabled = false; 
                    ProgressBar.Value = 100;
                    MessageBox.Show("You have completed all submissions. The Submit button is now disabled.", "Submission Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
            }

            submissionCount++;
            ProgressBar.Value += 25;

            MessageBox.Show("Report submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void SaveReport(string location, string category, string description, byte[] image)
        {
            Console.WriteLine("Report Saved:");
            Console.WriteLine($"Location: {location}, Category: {category}, Description: {description}");
            if (image != null)
            {
                Console.WriteLine($"Image bytes: {image.Length} bytes");
            }
            else
            {
                Console.WriteLine("Non-image file submitted.");
            }
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
