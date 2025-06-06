using Microsoft.AspNetCore.SignalR;

namespace LabProject.Domain.Hubs
{
    public class ReviewHub : Hub
    {
        public async Task SendReviewNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveReviewNotification", message);
        }
    }
}
