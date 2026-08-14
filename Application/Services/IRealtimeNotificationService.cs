namespace Application.Services;

public interface IRealtimeNotificationService
{
    Task BroadcastNotificationAsync(string title, string message, string type = "info");
    Task BroadcastActivityAsync(string user, string action, string target, string avatarBg = "#00A896");
    IEnumerable<object> GetRecentActivities();
}
