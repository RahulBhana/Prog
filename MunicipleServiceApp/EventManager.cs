using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipleServiceApp
{
    public class EventManager
    {
        public Dictionary<string, List<Event>> EventDictionary { get; private set; }

        public EventManager()
        {
            EventDictionary = new Dictionary<string, List<Event>>();
            LoadEvents(); 
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

        // This loads the events into their category
        private void LoadEvents()
        {
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
                    Title = "Jazz Night",
                    Date = "5 November 2024",
                    Category = "Music",
                    Location = "Jazz Lounge, Main Street",
                    Description = "An intimate jazz night with performances by local musicians.",
                    EventDate = new DateTime(2024, 11, 5)
                },
                new Event
                {
                    Title = "Rock Concert",
                    Date = "20 December 2024",
                    Category = "Music",
                    Location = "City Stadium",
                    Description = "Rock out with the biggest bands in the city!",
                    EventDate = new DateTime(2024, 12, 20)
                },
                new Event
                {
                    Title = "Folk Music Fair",
                    Date = "18 January 2025",
                    Category = "Music",
                    Location = "Riverside Park",
                    Description = "Celebrate folk music with performances, food, and fun!",
                    EventDate = new DateTime(2025, 1, 18)
                },
                new Event
                {
                    Title = "Classical Evening",
                    Date = "10 March 2025",
                    Category = "Music",
                    Location = "City Theater",
                    Description = "Enjoy an evening of classical music by renowned composers.",
                    EventDate = new DateTime(2025, 3, 10)
                },
                new Event
                {
                    Title = "Country Music Show",
                    Date = "15 May 2025",
                    Category = "Music",
                    Location = "Farmers Market Arena",
                    Description = "A country music extravaganza featuring local and national artists.",
                    EventDate = new DateTime(2025, 5, 15)
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
                    Title = "Neighborhood BBQ",
                    Date = "30 October 2024",
                    Category = "Community",
                    Location = "Central Park",
                    Description = "Join your neighbors for a BBQ. Bring a dish and make new friends!",
                    EventDate = new DateTime(2024, 10, 30)
                },
                new Event
                {
                    Title = "Park Beautification Day",
                    Date = "12 November 2024",
                    Category = "Community",
                    Location = "Green Meadows Park",
                    Description = "Volunteer to help beautify our local parks by planting trees and flowers.",
                    EventDate = new DateTime(2024, 11, 12)
                },
                new Event
                {
                    Title = "Charity Auction",
                    Date = "20 December 2024",
                    Category = "Community",
                    Location = "City Hall",
                    Description = "Bid on items donated by local businesses to raise money for charity.",
                    EventDate = new DateTime(2024, 12, 20)
                },
                new Event
                {
                    Title = "Community Craft Fair",
                    Date = "5 February 2025",
                    Category = "Community",
                    Location = "City Square",
                    Description = "A craft fair showcasing local artisans and their handmade goods.",
                    EventDate = new DateTime(2025, 2, 5)
                },
                new Event
                {
                    Title = "Volunteer Appreciation Dinner",
                    Date = "25 March 2025",
                    Category = "Community",
                    Location = "Town Hall",
                    Description = "Celebrate the volunteers who make our community a better place.",
                    EventDate = new DateTime(2025, 3, 25)
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
                    Title = "AI in Healthcare Symposium",
                    Date = "1 April 2025",
                    Category = "Technology",
                    Location = "Medical Research Institute",
                    Description = "Learn how AI is revolutionizing healthcare at this special event.",
                    EventDate = new DateTime(2025, 4, 1)
                },
                new Event
                {
                    Title = "Startup Pitch Day",
                    Date = "12 August 2025",
                    Category = "Technology",
                    Location = "Tech Innovation Hub",
                    Description = "Watch local startups pitch their ideas to investors and industry leaders.",
                    EventDate = new DateTime(2025, 8, 12)
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
                    Title = "Robotics Workshop",
                    Date = "20 June 2025",
                    Category = "Technology",
                    Location = "City Innovation Center",
                    Description = "Hands-on robotics workshop for tech enthusiasts.",
                    EventDate = new DateTime(2025, 6, 20)
                },
                new Event
                {
                    Title = "Coding Hackathon",
                    Date = "10 July 2025",
                    Category = "Technology",
                    Location = "Tech Valley",
                    Description = "Participate in a 48-hour coding hackathon and win exciting prizes!",
                    EventDate = new DateTime(2025, 7, 10)
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
                    Title = "Winter Soccer League",
                    Date = "15 December 2024",
                    Category = "Sports",
                    Location = "City Sports Complex",
                    Description = "Join the winter soccer league. Teams for all skill levels!",
                    EventDate = new DateTime(2024, 12, 15)
                },
                new Event
                {
                    Title = "Basketball Tournament",
                    Date = "10 February 2025",
                    Category = "Sports",
                    Location = "Indoor Sports Arena",
                    Description = "Sign up for the city's annual 3-on-3 basketball tournament.",
                    EventDate = new DateTime(2025, 2, 10)
                },
                new Event
                {
                    Title = "Spring Cycling Race",
                    Date = "5 March 2025",
                    Category = "Sports",
                    Location = "Mountain Pass",
                    Description = "Compete in the challenging spring cycling race through the mountains.",
                    EventDate = new DateTime(2025, 3, 5)
                },
                new Event
                {
                    Title = "Tennis Open",
                    Date = "25 April 2025",
                    Category = "Sports",
                    Location = "Grand Tennis Club",
                    Description = "Register for the city's premier tennis open.",
                    EventDate = new DateTime(2025, 4, 25)
                },
                new Event
                {
                    Title = "Youth Football Camp",
                    Date = "15 July 2025",
                    Category = "Sports",
                    Location = "City Stadium",
                    Description = "A summer football camp for young athletes.",
                    EventDate = new DateTime(2025, 7, 15)
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
                    Title = "Community Blood Drive",
                    Date = "30 June 2025",
                    Category = "Health",
                    Location = "City Hospital",
                    Description = "Save lives by donating blood at our community blood drive.",
                    EventDate = new DateTime(2025, 6, 30)
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
                    Title = "Yoga in the Park",
                    Date = "5 April 2025",
                    Category = "Health",
                    Location = "Central Park",
                    Description = "Free yoga session in the park for all skill levels.",
                    EventDate = new DateTime(2025, 4, 5)
                },
                new Event
                {
                    Title = "Mental Health Awareness Walk",
                    Date = "15 May 2025",
                    Category = "Health",
                    Location = "City Walkway",
                    Description = "Join the walk to raise awareness for mental health.",
                    EventDate = new DateTime(2025, 5, 15)
                },
                new Event
                {
                    Title = "Healthy Eating Workshop",
                    Date = "10 June 2025",
                    Category = "Health",
                    Location = "City Health Center",
                    Description = "Learn about healthy eating habits and meal planning.",
                    EventDate = new DateTime(2025, 6, 10)
                }
            };

            foreach (var ev in events)
            {
                if (!EventDictionary.ContainsKey(ev.Category))
                {
                    EventDictionary[ev.Category] = new List<Event>();
                }
                EventDictionary[ev.Category].Add(ev); // Stores the events into dictionary by category
            }
        }
    }
}