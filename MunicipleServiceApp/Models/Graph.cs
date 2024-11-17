using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipleServiceApp.DataStructures
{
    public class Graph<T>
    {
        private Dictionary<T, List<T>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<T, List<T>>();
        }

        public void AddNode(T node)
        {
            if (!adjacencyList.ContainsKey(node))
            {
                adjacencyList[node] = new List<T>();
            }
        }

        public void AddEdge(T from, T to)
        {
            if (adjacencyList.ContainsKey(from) && adjacencyList.ContainsKey(to))
            {
                adjacencyList[from].Add(to);
            }
        }

        public List<T> GetNeighbors(T node)
        {
            return adjacencyList.ContainsKey(node) ? adjacencyList[node] : new List<T>();
        }

        public IEnumerable<T> Nodes => adjacencyList.Keys;
    }
}
