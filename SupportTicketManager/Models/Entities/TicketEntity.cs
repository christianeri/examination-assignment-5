
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportTicketManager.Models.Entities
{
    [Index(nameof(TicketReference), IsUnique = true)]
    internal class TicketEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(50)]
        public string? TicketTitle { get; set; } = null;

        [StringLength(5000)]
        public string? TicketDescription { get; set; } = null;

        public DateTime TicketCreated { get; set; }

        [Required]
        [StringLength(5)]
        public string? TicketReference { get; set; }


        [StringLength(500)]
        public string? TicketComment { get; set; } = null;

        public DateTime TicketCommentUpdated { get; set; }


        [Required]
        public int BuildingId { get; set; }
        public BuildingEntity Building { get; set; } = null!;


        [Required]
        public int StatusId { get; set; }
        [StringLength(11)]
        public StatusEntity Status { get; set; } = null;        
        

        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;
    }
}
