using Microsoft.AspNetCore.SignalR;
using Models = TaskManagerBackend.Models;

namespace TaskManagerBackend.Hubs
{
    public class TaskHub : Hub
    {
        public async Task AddTask(string id, string name)
        {
            try
            {
                await Clients.All.SendAsync("addTask", id, name);
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.Error.WriteLine($"Error in AddTask: {ex.Message}");
                throw new HubException("Failed to add task on the server.", ex);
            }
        }

        public async Task UpdateTask(string id, string name)
        {
            await Clients.All.SendAsync("updateTask", id, name);
        }

        public async Task DeleteTask(string taskId)
        {
            await Clients.All.SendAsync("deleteTask", taskId);
        }

        public async Task Test()
        {
            Console.WriteLine("Test method called");
            await Clients.All.SendAsync("ReceivedTest", "Test successful");
        }
    }
}
