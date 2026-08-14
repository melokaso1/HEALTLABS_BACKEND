using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class NotificationHub : Hub
{
    private readonly Application.Services.IRealtimeNotificationService _notificationService;

    public NotificationHub(Application.Services.IRealtimeNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        var recent = _notificationService.GetRecentActivities();
        await Clients.Caller.SendAsync("LoadActivities", recent);
        await base.OnConnectedAsync();
    }

    public async Task BroadcastActivity(string user, string action, string target, string avatarBg)
    {
        await _notificationService.BroadcastActivityAsync(user, action, target, avatarBg);
    }
}
