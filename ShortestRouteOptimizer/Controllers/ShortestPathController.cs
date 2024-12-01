using Microsoft.AspNetCore.Mvc;
using ShortestRouteOptimizer.Models;
using ShortestRouteOptimizer.Services;

namespace ShortestRouteOptimizer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortestPathController : ControllerBase
    {
        private readonly ShortestPathService _service;

        public ShortestPathController()
        {
            _service = new ShortestPathService();
        }

        [HttpPost("find")]
        public IActionResult FindShortestPath([FromBody] ShortestPathRequest request)
        {
            var result = _service.FindShortestPath(request.FromNode, request.ToNode, request.GraphNodes);
            return Ok(result);
        }
    }
    public class ShortestPathRequest
    {
        public string FromNode { get; set; }
        public string ToNode { get; set; }
        public List<GraphNode> GraphNodes { get; set; }
    }
}