
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportTicketManager.Models.Entities
{
    [Index(nameof(CustomerEmail), IsUnique = true)]
    internal class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? CustomerFirstName { get; set; }

        [StringLength(50)]
        public string? CustomerLastName { get; set; }

        [StringLength(100)]
        public string? CustomerEmail { get; set; }

        [Column(TypeName = "char(13)")]
        public string? CustomerPhone { get; set; }


        public ICollection<TicketEntity> Tickets = new HashSet<TicketEntity>();
    }
}
