using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static MunicipleServiceApp.LocalEventsWindow;

namespace MunicipleServiceApp
{
    public partial class LocalEventsWindow : Window
    {
        private ObservableCollection<EventManager.Event> filteredEvents;
        private EventManager eventManager;
        private Stack<string> searchHistory;
        private HashSet<string> uniqueSearchHistory; // Uses the HashSet to track unique searches

        public LocalEventsWindow()
        {
            InitializeComponent();
            eventManager = new EventManager(); 
            searchHistory = new Stack<string>();
            uniqueSearchHistory = new HashSet<string>(); 
            LoadEvents();
        }
        private void LoadEvents()
        {
            filteredEvents = new ObservableCollection<EventManager.Event>(eventManager.EventDictionary.Values.SelectMany(e => e).ToList());
            eventsPanel.ItemsSource = filteredEvents;
        }

        // Search method for filtering events based on category and date
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedCategory = (categoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                DateTime? selectedDate = eventDatePicker.SelectedDate;

                filteredEvents.Clear();

                // Track the selected category if it's unique
                if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All")
                {
                    if (!uniqueSearchHistory.Contains(selectedCategory))
                    {
                        uniqueSearchHistory.Add(selectedCategory);
                        searchHistory.Push(selectedCategory);
                    }
                }

                // Track the selected date if it's unique
                if (selectedDate.HasValue)
                {
                    string dateKey = selectedDate.Value.ToShortDateString();
                    if (!uniqueSearchHistory.Contains(dateKey))
                    {
                        uniqueSearchHistory.Add(dateKey);
                        searchHistory.Push(dateKey); // Add the date to the search history
                    }
                }

                // Filter events by category and/or date
                if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All" && eventManager.EventDictionary.ContainsKey(selectedCategory))
                {
                    var categoryEvents = eventManager.EventDictionary[selectedCategory];
                    foreach (var ev in categoryEvents)
                    {
                        bool matchesDate = !selectedDate.HasValue || ev.EventDate == selectedDate.Value;
                        if (matchesDate)
                        {
                            filteredEvents.Add(ev);
                        }
                    }
                }
                else
                {
                    foreach (var evList in eventManager.EventDictionary.Values)
                    {
                        foreach (var ev in evList)
                        {
                            bool matchesDate = !selectedDate.HasValue || ev.EventDate == selectedDate.Value;
                            if (matchesDate)
                            {
                                filteredEvents.Add(ev);
                            }
                        }
                    }
                }

                // Show error message if no events match
                if (filteredEvents.Count == 0)
                {
                    MessageBox.Show("No events found matching your criteria.", "No Results", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnGetRecommendations_Click(object sender, RoutedEventArgs e)
        {
            GetRecommendationsWindow recommendationsWindow = new GetRecommendationsWindow(searchHistory, uniqueSearchHistory, eventManager);
            recommendationsWindow.Show();
            this.Close();
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); 
            mainWindow.Show();
            this.Close();
        }
    }
}