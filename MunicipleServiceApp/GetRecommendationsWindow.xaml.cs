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

namespace MunicipleServiceApp
{
    public partial class GetRecommendationsWindow : Window
    {
        private Queue<Event> recommendedEventsQueue; // Store recommended events using a Queue 

        public GetRecommendationsWindow(Stack<string> searchHistory)
        {
            InitializeComponent();
            LoadRecommendations(searchHistory);
        }
        public class Event
        {
            public string Title { get; set; }
            public string Date { get; set; }
            public string Category { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public DateTime EventDate { get; set; }
        }

        private void LoadRecommendations(Stack<string> searchHistory)
        {
            var allEvents = new ObservableCollection<Event>
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

            recommendedEventsQueue = new Queue<Event>();

            //This recommends events based on search history useing the stack
            if (searchHistory.Any())
            {
                foreach (var category in searchHistory)
                {
                    foreach (var ev in allEvents)
                    {
                        if (ev.Category == category)
                        {
                            recommendedEventsQueue.Enqueue(ev);
                        }
                    }
                }
            }

            // Ensures that at least 5 events are recommended
            if (recommendedEventsQueue.Count < 5)
            {
                var additionalEvents = allEvents.Where(ev => ev.EventDate > DateTime.Now && !recommendedEventsQueue.Contains(ev))
                                                .Take(5 - recommendedEventsQueue.Count);

                foreach (var ev in additionalEvents)
                {
                    recommendedEventsQueue.Enqueue(ev); 
                }
            }

            recommendationsPanel.ItemsSource = recommendedEventsQueue.ToList(); // Convert Queue to List for binding
        }

        private void ReturnToLocalEvents_Click(object sender, RoutedEventArgs e)
        {
            LocalEventsWindow localEventsWindow = new LocalEventsWindow();
            localEventsWindow.Show(); 
            this.Close();
        }
    }
}