
using System.ComponentModel.DataAnnotations;


namespace SupportTicketManager.Models.Entities
{
    internal class StatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? TicketStatus { get; set; }

        public ICollection<TicketEntity> Tickets = new HashSet<TicketEntity>();
    }
}
