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
        private Graph<ServiceRequest> serviceRequestGraph; // Graph to manage dependencies

        public ServiceRequestStatusWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            // Initialize the graph
            serviceRequestGraph = new Graph<ServiceRequest>();

            // Create service requests
            var request1 = new ServiceRequest { ID = 1, Category = "Water Leak", SubmissionDate = DateTime.Now.AddDays(-5), Status = "Pending" };
            var request2 = new ServiceRequest { ID = 2, Category = "Pipe Blockage", SubmissionDate = DateTime.Now.AddDays(-6), Status = "Resolved" };
            var request3 = new ServiceRequest { ID = 3, Category = "Road Repair", SubmissionDate = DateTime.Now.AddDays(-7), Status = "Pending" };

            var request4 = new ServiceRequest { ID = 4, Category = "Electricity Outage", SubmissionDate = DateTime.Now.AddDays(-4), Status = "Pending" };
            var request5 = new ServiceRequest { ID = 5, Category = "Water Leak", SubmissionDate = DateTime.Now.AddDays(-3), Status = "Resolved" };
            var request6 = new ServiceRequest { ID = 6, Category = "Illegal Dumping", SubmissionDate = DateTime.Now.AddDays(-8), Status = "Pending" };
            var request7 = new ServiceRequest { ID = 7, Category = "Tree Obstruction", SubmissionDate = DateTime.Now.AddDays(-10), Status = "Pending" };
            var request8 = new ServiceRequest { ID = 8, Category = "Potholes", SubmissionDate = DateTime.Now.AddDays(-6), Status = "Pending" };
            var request9 = new ServiceRequest { ID = 9, Category = "Road Repair", SubmissionDate = DateTime.Now.AddDays(-2), Status = "Pending" };
            var request10 = new ServiceRequest { ID = 10, Category = "Street Light Outage", SubmissionDate = DateTime.Now.AddDays(-1), Status = "Resolved" };
            var request11 = new ServiceRequest { ID = 11, Category = "Broken Sewer", SubmissionDate = DateTime.Now.AddDays(-12), Status = "Pending" };
            var request12 = new ServiceRequest { ID = 12, Category = "Pipe Blockage", SubmissionDate = DateTime.Now.AddDays(-14), Status = "Resolved" };
            var request13 = new ServiceRequest { ID = 13, Category = "Water Contamination", SubmissionDate = DateTime.Now.AddDays(-9), Status = "Pending" };
            var request14 = new ServiceRequest { ID = 14, Category = "Street Light Outage", SubmissionDate = DateTime.Now.AddDays(-7), Status = "Resolved" };
            var request15 = new ServiceRequest { ID = 15, Category = "Traffic Light Issue", SubmissionDate = DateTime.Now.AddDays(-11), Status = "Pending" };
            var request16 = new ServiceRequest { ID = 16, Category = "Electricity Outage", SubmissionDate = DateTime.Now.AddDays(-13), Status = "Pending" };
            var request17 = new ServiceRequest { ID = 17, Category = "Illegal Dumping", SubmissionDate = DateTime.Now.AddDays(-10), Status = "Resolved" };
            var request18 = new ServiceRequest { ID = 18, Category = "Water Contamination", SubmissionDate = DateTime.Now.AddDays(-15), Status = "Resolved" };

            var allRequests = new[] { request1, request2, request3, request4, request5, request6, request7, request8, request9, request10, request11, request12, request13, request14, request15, request16, request17, request18 };
            foreach (var request in allRequests)
            {
                serviceRequestGraph.AddNode(request);
            }

            serviceRequestGraph.AddEdge(request1, request2); // Request 1 depends on Request 2
            serviceRequestGraph.AddEdge(request3, request1); // Request 3 depends on Request 1
            serviceRequestGraph.AddEdge(request4, request2); // Request 4 depends on Request 2
            serviceRequestGraph.AddEdge(request5, request4); // Request 5 depends on Request 4
            serviceRequestGraph.AddEdge(request6, request3); // Request 6 depends on Request 3
            serviceRequestGraph.AddEdge(request7, request6); // Request 7 depends on Request 6
            serviceRequestGraph.AddEdge(request8, request7); // Request 8 depends on Request 7
            serviceRequestGraph.AddEdge(request9, request8); // Request 9 depends on Request 8
            serviceRequestGraph.AddEdge(request10, request9); // Request 10 depends on Request 9
            serviceRequestGraph.AddEdge(request11, request10); // Request 11 depends on Request 10
            serviceRequestGraph.AddEdge(request12, request11); // Request 12 depends on Request 11
            serviceRequestGraph.AddEdge(request13, request12); // Request 13 depends on Request 12
            serviceRequestGraph.AddEdge(request14, request13); // Request 14 depends on Request 13
            serviceRequestGraph.AddEdge(request15, request14); // Request 15 depends on Request 14
            serviceRequestGraph.AddEdge(request16, request15); // Request 16 depends on Request 15
            serviceRequestGraph.AddEdge(request17, request16); // Request 17 depends on Request 16
            serviceRequestGraph.AddEdge(request18, request17); // Request 18 depends on Request 17

            foreach (var request in allRequests)
            {
                foreach (var dependency in serviceRequestGraph.GetNeighbors(request))
                {
                    request.Dependencies.Add(dependency);
                }
            }

            // Display all service requests
            DisplayAllRequests();
        }

        private void DisplayAllRequests()
        {
            var allRequests = serviceRequestGraph.Nodes.ToList();

            serviceRequestGrid.ItemsSource = allRequests.Select(r => new
            {
                r.ID,
                r.Category,
                SubmissionDate = r.SubmissionDate.ToString("yyyy-MM-dd"), 
                r.Status
            });
        }

        private void DisplayDependencies(ServiceRequest selectedRequest)
        {
            if (selectedRequest.Dependencies.Any())
            {
                dependenciesListBox.ItemsSource = selectedRequest.Dependencies.Select(d => $"Dependency ID: {d.ID} - {d.Category}");
            }
            else
            {
                dependenciesListBox.ItemsSource = new List<string> { "No dependencies found." };
            }
        }

        private void serviceRequestGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serviceRequestGrid.SelectedItem != null)
            {
                var selectedRow = serviceRequestGrid.SelectedItem;
                var selectedRequestID = (int)selectedRow.GetType().GetProperty("ID").GetValue(selectedRow);

                var selectedRequest = serviceRequestGraph.Nodes.FirstOrDefault(r => r.ID == selectedRequestID);

                if (selectedRequest != null)
                {
                    DisplayDependencies(selectedRequest);
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string category = (typeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (category != "All Types" && serviceRequestGraph.Nodes.Any(n => n.Category == category))
            {
                serviceRequestGrid.ItemsSource = serviceRequestGraph.Nodes
                    .Where(n => n.Category == category)
                    .Select(r => new
                    {
                        r.ID,
                        r.Category,
                        SubmissionDate = r.SubmissionDate.ToString("yyyy-MM-dd"), 
                        r.Status
                    });
            }
            else
            {
                DisplayAllRequests(); 
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            requestIDTextBox.Clear();
            typeComboBox.SelectedIndex = 0; // Reset to "All Types"
            datePicker.SelectedDate = null; 
            DisplayAllRequests();
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the Main Menu
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