using MunicipleServiceApp.DataStructures;
using MunicipleServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MunicipleServiceApp
{
    /// <summary>
    /// Interaction logic for ServiceRequestStatusWindow.xaml
    /// </summary>
    public partial class ServiceRequestStatusWindow : Window
    {
        private Dictionary<string, BinarySearchTree<ServiceRequest>> serviceRequests;

        public ServiceRequestStatusWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            serviceRequests = new Dictionary<string, BinarySearchTree<ServiceRequest>>
            {
                { "Water Leak", new BinarySearchTree<ServiceRequest>() },
                { "Electricity Outage", new BinarySearchTree<ServiceRequest>() },
                { "Potholes", new BinarySearchTree<ServiceRequest>() },
            };

            serviceRequests["Water Leak"].Insert(new ServiceRequest
            {
                ID = 1,
                Category = "Water Leak",
                SubmissionDate = DateTime.Now.AddDays(-5),
                Status = "Pending",
                LastUpdated = DateTime.Now
            });

            serviceRequests["Electricity Outage"].Insert(new ServiceRequest
            {
                ID = 2,
                Category = "Electricity Outage",
                SubmissionDate = DateTime.Now.AddDays(-10),
                Status = "Resolved",
                LastUpdated = DateTime.Now.AddDays(-2)
            });

            DisplayAllRequests();
        }

        private void DisplayAllRequests()
        {
            var allRequests = serviceRequests.Values
                .SelectMany(tree => tree.InOrderTraversal())
                .ToList();

            serviceRequestGrid.ItemsSource = allRequests;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string category = (typeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (category != "All Types" && serviceRequests.ContainsKey(category))
            {
                serviceRequestGrid.ItemsSource = serviceRequests[category].InOrderTraversal();
            }
            else
            {
                DisplayAllRequests(); 
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            requestIDTextBox.Clear();
            typeComboBox.SelectedIndex = 0; 
            datePicker.SelectedDate = null; 
            DisplayAllRequests();
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void requestIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderRequestID.Visibility = string.IsNullOrWhiteSpace(requestIDTextBox.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}