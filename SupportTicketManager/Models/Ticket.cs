
namespace SupportTicketManager.Models
{
    internal class Ticket
    {
        public Guid Id { get; set; }


        public string? TicketDescription { get; set; } = null;
        public string? TicketTitle { get; set; } = null;

        public string? BuildingId { get; set; } = null;
        public string? BuildingName { get; set; } = null;
        public string? PropertyCode { get; set; } = null;
        

        public DateTime TicketCreated { get; set; }


        public string? CustomerFirstName { get; set; } = null;
        public string? CustomerLastName { get; set; } = null;
        public string? CustomerEmail { get; set; } = null;
        public string? CustomerPhone { get; set; } = null;


        public string? TicketReference { get; set; }


        public string? TicketStatus { get; set; }
        public int TicketStatusId { get; set; }


        public string? TicketComment { get; set; } = null;
        public DateTime TicketCommentUpdated { get; set; }

    }
}
