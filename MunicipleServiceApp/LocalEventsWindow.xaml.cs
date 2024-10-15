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
        private ObservableCollection<Event> filteredEvents; 
        private Dictionary<string, List<Event>> eventDictionary; // Store events by category using a Dictionary
        private Stack<string> searchHistory; // Stores the user's search history using a Stack 

        public LocalEventsWindow()
        {
            InitializeComponent();
            searchHistory = new Stack<string>(); 
            LoadDummyData(); 
        }

        // Define Event model
        public class Event
        {
            public string Title { get; set; }
            public string Date { get; set; }
            public string Category { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public DateTime EventDate { get; set; }
        }

        // Loads and organizes the event data into a Dictionary
        private void LoadDummyData()
        {
            eventDictionary = new Dictionary<string, List<Event>>(); // Initialize dictionary

            var events = new List<Event>
            {
                new Event
                {
                    Title = "Music Festival 2024",
                    Date = "12 October 2024",
                    Category = "Music",
                    Location = "Downtown Park, City Center",
                    Description = "Join us for an exciting music festival with live performances and food stalls!",
                    EventDate = new DateTime(2024, 10, 12)
                },
                new Event
                {
                    Title = "Community Clean-up",
                    Date = "15 October 2024",
                    Category = "Community",
                    Location = "Main Beachfront",
                    Description = "Help us clean up our beautiful beaches. All volunteers are welcome!",
                    EventDate = new DateTime(2024, 10, 15)
                },
                new Event
                {
                    Title = "Tech Expo 2024",
                    Date = "20 November 2024",
                    Category = "Technology",
                    Location = "City Convention Center",
                    Description = "Explore the latest innovations in technology at the annual Tech Expo.",
                    EventDate = new DateTime(2024, 11, 20)
                },
                new Event
                {
                    Title = "Marathon 2024",
                    Date = "1 December 2024",
                    Category = "Sports",
                    Location = "City Stadium",
                    Description = "Participate in the annual marathon event. Open to all ages.",
                    EventDate = new DateTime(2024, 12, 1)
                },
                new Event
                {
                    Title = "Health & Wellness Fair",
                    Date = "10 January 2025",
                    Category = "Health",
                    Location = "City Health Center",
                    Description = "Discover the latest trends in health and wellness at our annual fair.",
                    EventDate = new DateTime(2025, 1, 10)
                },
                new Event
                {
                    Title = "Fitness Bootcamp",
                    Date = "25 February 2025",
                    Category = "Health",
                    Location = "Riverside Park",
                    Description = "Join us for a full-body workout at our community fitness bootcamp!",
                    EventDate = new DateTime(2025, 2, 25)
                },
                  new Event
                {
                    Title = "Tech Conference 2025",
                    Date = "15 March 2025",
                    Category = "Technology",
                    Location = "TechWorld Convention Center",
                    Description = "The latest in AI, robotics, and cybersecurity will be showcased at this event.",
                    EventDate = new DateTime(2025, 3, 15)
                },
                 new Event
                {
                    Title = "AI in Healthcare Symposium",
                    Date = "1 April 2025",
                    Category = "Technology",
                    Location = "Medical Research Institute",
                    Description = "Learn how AI is revolutionizing healthcare at this special event.",
                    EventDate = new DateTime(2025, 4, 1)
                },
                new Event
                {
                    Title = "Music in the Park",
                    Date = "22 May 2025",
                    Category = "Music",
                    Location = "Green Meadows Park",
                    Description = "Enjoy a relaxing afternoon of live music in the park.",
                    EventDate = new DateTime(2025, 5, 22)
                },
                new Event
                {
                    Title = "Community Blood Drive",
                    Date = "30 June 2025",
                    Category = "Health",
                    Location = "City Hospital",
                    Description = "Save lives by donating blood at our community blood drive.",
                    EventDate = new DateTime(2025, 6, 30)
                },
                new Event
                {
                    Title = "Startup Pitch Day",
                    Date = "12 August 2025",
                    Category = "Technology",
                    Location = "Tech Innovation Hub",
                    Description = "Watch local startups pitch their ideas to investors and industry leaders.",
                    EventDate = new DateTime(2025, 8, 12)
                }
                };

            // Organize events by category in the dictionary
            foreach (var ev in events)
            {
                if (!eventDictionary.ContainsKey(ev.Category))
                {
                    eventDictionary[ev.Category] = new List<Event>();
                }
                eventDictionary[ev.Category].Add(ev);
            }

            filteredEvents = new ObservableCollection<Event>(events);
            eventsPanel.ItemsSource = filteredEvents; 
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedCategory = (categoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                DateTime? selectedDate = eventDatePicker.SelectedDate; 

                filteredEvents.Clear();

                // Track user search history using the stack
                if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All")
                {
                    searchHistory.Push(selectedCategory); 
                }

               
                if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All" && eventDictionary.ContainsKey(selectedCategory))
                {
                    var categoryEvents = eventDictionary[selectedCategory];

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
            
                    foreach (var evList in eventDictionary.Values)
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
            GetRecommendationsWindow recommendationsWindow = new GetRecommendationsWindow(searchHistory);
            recommendationsWindow.Show();
            this.Close();
        }
    }
}