using ShortestRouteOptimizer.Models;
using ShortestRouteOptimizer.Services;
using Xunit;

namespace ShortestRouteOptimizerTests
{
    public class ShortestPathServiceTests
    {
        private readonly ShortestPathService _service;

        public ShortestPathServiceTests()
        {
            _service = new ShortestPathService();
        }

        [Fact]
        public void FindShortestPath_ShouldReturnCorrectPathAndDistance()
        {
            // Arrange
            var graphNodes = new List<GraphNode>
        {
            new GraphNode { Name = "A", Edges = new List<Edge> { new Edge { To = "B", Distance = 1 } } },
            new GraphNode { Name = "B", Edges = new List<Edge> { new Edge { To = "C", Distance = 2 } } },
            new GraphNode { Name = "C", Edges = new List<Edge> { new Edge { To = "D", Distance = 3 } } },
            new GraphNode { Name = "D", Edges = new List<Edge>() }
        };

            // Act
            var result = _service.FindShortestPath("A", "D", graphNodes);

            // Assert
            Assert.Equal(new List<string> { "A", "B", "C", "D" }, result.NodeNames);
            Assert.Equal(6, result.Distance);
        }

        [Fact]
        public void FindShortestPath_ShouldReturnInfinity_WhenNoPathExists()
        {
            // Arrange
            var graphNodes = new List<GraphNode>
        {
            new GraphNode { Name = "A", Edges = new List<Edge> { new Edge { To = "B", Distance = 1 } } },
            new GraphNode { Name = "B", Edges = new List<Edge>() },
            new GraphNode { Name = "C", Edges = new List<Edge> { new Edge { To = "D", Distance = 3 } } },
            new GraphNode { Name = "D", Edges = new List<Edge>() }
        };

            // Act
            var result = _service.FindShortestPath("A", "D", graphNodes);

            // Assert
            Assert.Equal(int.MaxValue, result.Distance);
            Assert.Empty(result.NodeNames);
        }
    }
}