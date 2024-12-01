namespace ShortestRouteOptimizer.Models
{
    public class GraphNode
    {
        public string Name { get; set; }
        public List<Edge> Edges { get; set; } = new();
    }
}
