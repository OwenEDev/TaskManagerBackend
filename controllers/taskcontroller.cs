using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaskManagerBackend.Hubs;
using TaskManagerBackend.Models;
using Models = TaskManagerBackend.Models;

namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
    private static List<Models.Task> _tasks = new List<Models.Task>();  // Use Models.Task here
    private readonly IHubContext<TaskHub> _hubContext;

    public TaskController(IHubContext<TaskHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(_tasks);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] Models.Task task) // Use Models.Task here as well
    {
        _tasks.Add(task);

        await _hubContext.Clients.All.SendAsync("AddTask", task);

        return Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(string id, [FromBody] Models.Task updatedTask)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        task.Name = updatedTask.Name;

        await _hubContext.Clients.All.SendAsync("UpdateTask", task);

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(string id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        _tasks.Remove(task);

        await _hubContext.Clients.All.SendAsync("DeleteTask", id);

        return Ok(id);
    }
    }
}
