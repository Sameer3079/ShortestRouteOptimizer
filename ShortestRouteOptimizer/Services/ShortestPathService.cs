using ShortestRouteOptimizer.Models;

namespace ShortestRouteOptimizer.Services
{
    public class ShortestPathService
    {
        public ShortestPathData FindShortestPath(string fromNodeName, string toNodeName, List<GraphNode> graphNodes)
        {
            var distances = new Dictionary<string, int>();
            var previousNodes = new Dictionary<string, string>();
            var priorityQueue = new SortedSet<(int Distance, string Node)>();

            foreach (var node in graphNodes)
            {
                distances[node.Name] = int.MaxValue;
            }
            distances[fromNodeName] = 0;
            priorityQueue.Add((0, fromNodeName));

            while (priorityQueue.Count > 0)
            {
                var (currentDistance, currentNode) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                if (currentNode == toNodeName)
                    break;

                var currentGraphNode = graphNodes.First(n => n.Name == currentNode);

                foreach (var edge in currentGraphNode.Edges)
                {
                    int newDist = currentDistance + edge.Distance;
                    if (newDist < distances[edge.To])
                    {
                        priorityQueue.Remove((distances[edge.To], edge.To));
                        distances[edge.To] = newDist;
                        previousNodes[edge.To] = currentNode;
                        priorityQueue.Add((newDist, edge.To));
                    }
                }
            }

            var path = new List<string>();
            var current = toNodeName;

            while (!string.IsNullOrEmpty(current))
            {
                path.Add(current);
                previousNodes.TryGetValue(current, out current);
            }

            path.Reverse();

            return new ShortestPathData
            {
                NodeNames = path,
                Distance = distances[toNodeName]
            };
        }
    }
}
