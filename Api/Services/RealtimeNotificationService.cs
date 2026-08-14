using System.Collections.Concurrent;
using Api.Hubs;
using Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace Api.Services;

public class RealtimeNotificationService : IRealtimeNotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private static readonly ConcurrentQueue<object> _recentActivities = new();
    private static readonly object _recentActivitiesLock = new();

    public RealtimeNotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task BroadcastNotificationAsync(string title, string message, string type = "info")
    {
        var payload = new
        {
            id = Guid.NewGuid().ToString(),
            title,
            message,
            timestamp = DateTime.UtcNow.ToString("o"),
            type,
            read = false
        };

        await _hubContext.Clients.All.SendAsync("ReceiveNotification", payload);
    }

    public async Task BroadcastActivityAsync(string user, string action, string target, string avatarBg = "#00A896")
    {
        var payload = new
        {
            id = Guid.NewGuid().ToString(),
            user,
            action,
            target,
            timestamp = DateTime.UtcNow.ToString("o"),
            timeAgo = "Hace un momento",
            avatarBg
        };

        lock (_recentActivitiesLock)
        {
            _recentActivities.Enqueue(payload);
            while (_recentActivities.Count > 30)
                _recentActivities.TryDequeue(out _);
        }

        await _hubContext.Clients.All.SendAsync("ReceiveActivity", payload);
    }

    public IEnumerable<object> GetRecentActivities()
    {
        lock (_recentActivitiesLock)
            return _recentActivities.ToArray().Reverse().ToArray();
    }
}
