using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Domain.Hubs;
using Microsoft.AspNetCore.SignalR;


    [Authorize]
    public class SupportHub(ITicketChatService _chatService) : Hub
    {
        private static string GroupName(int ticketId) => $"ticket-{ticketId}";

        public async Task JoinTicket(int ticketId)
        {
            var userId = Context.UserIdentifier!;
            if (!await _chatService.UserCanAccessTicketAsync(ticketId, int.Parse(userId)))
                throw new HubException("Not authorized for this ticket.");

            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName(ticketId));
            await Clients.Caller.SendAsync("JoinedTicket", ticketId);
        }

        public async Task LeaveTicket(int ticketId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName(ticketId));
            await Clients.Caller.SendAsync("LeftTicket", ticketId);
        }

        public async Task SendMessage(int ticketId, string body, bool isInternalNote = false)
        {
            var userId = Context.UserIdentifier!;
            if (string.IsNullOrWhiteSpace(body))
                throw new HubException("Empty message.");

            if (!await _chatService.UserCanAccessTicketAsync(ticketId, int.Parse(userId)))
                throw new HubException("Not authorized.");

            var msg = await _chatService.AddMessageAsync(ticketId, int.Parse(userId), body);

            await Clients.Group(GroupName(ticketId))
                .SendAsync("MessageReceived", msg);
        }

        public async Task Typing(int ticketId, bool isTyping)
        {
            var userId = Context.UserIdentifier!;
            if (!await _chatService.UserCanAccessTicketAsync(ticketId, int.Parse(userId))) return;

            await Clients.GroupExcept(GroupName(ticketId), Context.ConnectionId)
                .SendAsync("Typing", new { ticketId, userId, isTyping });
        }
    }