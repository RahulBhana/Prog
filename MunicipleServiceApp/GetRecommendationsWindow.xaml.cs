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
        private Queue<EventManager.Event> recommendedEventsQueue; // Store recommended events using a Queue
        private HashSet<EventManager.Event> uniqueRecommendations; // Ensures unique recommendations with the use of a HashSet
        private EventManager eventManager;

        public GetRecommendationsWindow(Stack<string> searchHistory, HashSet<string> uniqueSearchHistory, EventManager eventManager)
        {
            InitializeComponent();
            this.eventManager = eventManager;
            uniqueRecommendations = new HashSet<EventManager.Event>();
            LoadRecommendations(searchHistory, uniqueSearchHistory);
        }

        private void LoadRecommendations(Stack<string> searchHistory, HashSet<string> uniqueSearchHistory)
        {
            var allEvents = eventManager.EventDictionary.Values.SelectMany(e => e).ToList(); 

            if (allEvents.Count == 0)
            {
                MessageBox.Show("No events available for recommendations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Initializes the queue to hold recommended events
            recommendedEventsQueue = new Queue<EventManager.Event>();

            List<EventManager.Event> possibleRecommendations = new List<EventManager.Event>();

            foreach (var category in uniqueSearchHistory)
            {
                // Filter events by category and adds to possible recommendations list
                foreach (var ev in allEvents)
                {
                    if (ev.Category == category && !uniqueRecommendations.Contains(ev)) // Avoids duplicates
                    {
                        uniqueRecommendations.Add(ev); // Adds to HashSet to ensure that there are no duplicate recommendations
                        possibleRecommendations.Add(ev); // Adds event to possible recommendations list
                    }
                }
            }

            // Shuffle the possible recommendations to randomize selection
            Random rand = new Random();
            possibleRecommendations = possibleRecommendations.OrderBy(x => rand.Next()).ToList();

            var selectedRecommendations = possibleRecommendations.Take(3).ToList();

            foreach (var recommendation in selectedRecommendations)
            {
                recommendedEventsQueue.Enqueue(recommendation);
            }

            recommendationsPanel.ItemsSource = recommendedEventsQueue.ToList();
        }

        private void ReturnToLocalEvents_Click(object sender, RoutedEventArgs e)
        {
            LocalEventsWindow localEventsWindow = new LocalEventsWindow();
            localEventsWindow.Show();
            this.Close();
        }
    }
}