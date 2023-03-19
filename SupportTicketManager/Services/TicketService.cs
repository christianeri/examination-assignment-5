
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SupportTicketManager.Contexts;
using SupportTicketManager.Models;
using SupportTicketManager.Models.Entities;
using System.Runtime.CompilerServices;

namespace SupportTicketManager.Services
{
    internal class TicketService
    {

        private static DataContext _context = new DataContext();


        #region Create New Ticket

        public static async Task<BuildingEntity> GetBuildingAsync(string buildingName)
        {
            var _building = await _context.Buildings.FirstOrDefaultAsync(x => x.BuildingName == buildingName);
            if (_building != null)
                return new BuildingEntity
                {
                    Id = _building.Id,
                    BuildingName = _building.BuildingName,
                    PropertyCode = _building.PropertyCode
                };
            else
                return null;
        }



        public static async Task SaveAsync(Ticket ticket)
        {
            var _ticketEntity = new TicketEntity
            {
                TicketDescription = ticket.TicketDescription,
                TicketTitle = ticket.TicketTitle,
                TicketCreated = DateTime.Now,
                TicketReference = ticket.TicketReference,
                TicketComment = ticket.TicketComment,
            };


            var _buildingEntity = await _context.Buildings.FirstOrDefaultAsync(x => x.BuildingName == ticket.BuildingName);
            _ticketEntity.BuildingId = _buildingEntity.Id;            


            //Setting default status of newly created ticket
            _ticketEntity.StatusId = 1;


            var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => 
                x.CustomerFirstName == ticket.CustomerFirstName && 
                x.CustomerLastName == ticket.CustomerLastName && 
                x.CustomerEmail == ticket.CustomerEmail && 
                x.CustomerPhone == ticket.CustomerPhone);

            if (_customerEntity != null)
                _ticketEntity.CustomerId = _customerEntity.Id;
            else
                _ticketEntity.Customer = new CustomerEntity
                {
                    CustomerFirstName = ticket.CustomerFirstName,
                    CustomerLastName = ticket.CustomerLastName,
                    CustomerEmail = ticket.CustomerEmail,
                    CustomerPhone = ticket.CustomerPhone
                };

            _context.Add(_ticketEntity);
            await _context.SaveChangesAsync();  
        }

        #endregion


        #region Get All Tickets

        public static async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            var _tickets = new List<Ticket>();

            foreach (var _ticket in await _context.Tickets.Include(x => x.Customer).Include(y => y.Building).Include(z => z.Status).ToListAsync())            
            //foreach (var _ticket in await _context.Tickets.Include(x => x.Customer).Include(y => y.Building).ToListAsync())
                _tickets.Add(new Ticket
                {
                    Id = _ticket.Id,
                    TicketReference = _ticket.TicketReference,
                    TicketDescription = _ticket.TicketDescription,
                    TicketTitle = _ticket.TicketTitle,
                    TicketCreated = _ticket.TicketCreated,

                    TicketStatusId = _ticket.Status.Id,
                    TicketStatus = _ticket.Status.TicketStatus,

                    PropertyCode = _ticket.Building.PropertyCode,
                    BuildingName = _ticket.Building.BuildingName,

                        TicketComment = _ticket.TicketComment,
                        TicketCommentUpdated = _ticket.TicketCommentUpdated,
                    
                    CustomerFirstName = _ticket.Customer.CustomerFirstName,
                    CustomerLastName = _ticket.Customer.CustomerLastName,
                    CustomerEmail = _ticket.Customer.CustomerEmail,
                    CustomerPhone = _ticket.Customer.CustomerPhone
                    
                });

            return _tickets;
        }

        #endregion


        #region Get Specific Ticket

        public static async Task<Ticket> GetAsync(string reference)
        {
            var _ticket = await _context.Tickets.Include(x => x.Customer).Include(y => y.Building).Include(z => z.Status).FirstOrDefaultAsync(x => x.TicketReference == reference);
            if (_ticket != null)
                return new Ticket
                {
                    Id = _ticket.Id,
                    TicketReference = _ticket.TicketReference,
                    TicketDescription = _ticket.TicketDescription,
                    TicketTitle = _ticket.TicketTitle,

                    TicketStatusId = _ticket.Status.Id,
                    TicketStatus = _ticket.Status.TicketStatus,

                    TicketCreated = _ticket.TicketCreated,
                    TicketComment = _ticket.TicketComment,
                    TicketCommentUpdated = _ticket.TicketCommentUpdated,
                    CustomerFirstName = _ticket.Customer.CustomerFirstName,
                    CustomerLastName = _ticket.Customer.CustomerLastName,
                    CustomerEmail = _ticket.Customer.CustomerEmail,
                    CustomerPhone = _ticket.Customer.CustomerPhone
                };
            else
                return null;
        }





        public static async Task UpdateAsync(Ticket ticket)
        {
            var _ticketEntity = await _context.Tickets.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == ticket.Id);
            
            if (_ticketEntity != null)
            {
                if (!string.IsNullOrEmpty(ticket.TicketDescription))
                    _ticketEntity.TicketDescription = ticket.TicketDescription;

                if (!string.IsNullOrEmpty(ticket.TicketTitle))
                    _ticketEntity.TicketTitle = ticket.TicketTitle;

                if (!string.IsNullOrEmpty(ticket.TicketComment))
                    _ticketEntity.TicketComment = ticket.TicketComment;



                    _ticketEntity.TicketCommentUpdated = ticket.TicketCommentUpdated;



                if (ticket.TicketStatus != null)
                    _ticketEntity.StatusId = ticket.TicketStatusId;



                if (!string.IsNullOrEmpty(ticket.CustomerFirstName) || !string.IsNullOrEmpty(ticket.CustomerLastName) || !string.IsNullOrEmpty(ticket.CustomerEmail) || !string.IsNullOrEmpty(ticket.CustomerPhone))
                {
                    var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerFirstName == ticket.CustomerFirstName && x.CustomerLastName == ticket.CustomerLastName && x.CustomerEmail == ticket.CustomerEmail && x.CustomerPhone == ticket.CustomerPhone);
                    if (_customerEntity != null)
                        _ticketEntity.CustomerId = _customerEntity.Id;
                    else
                        _ticketEntity.Customer = new CustomerEntity
                        {
                            CustomerFirstName = ticket.CustomerFirstName,
                            CustomerLastName = ticket.CustomerLastName,
                            CustomerEmail = ticket.CustomerEmail,
                            CustomerPhone = ticket.CustomerPhone
                        };
                }

                _context.Update(_ticketEntity);
                await _context.SaveChangesAsync();
            }
        }

        #endregion


        public static async Task DeleteAsync(string reference)
        {
            var ticket = await _context.Tickets.Include(x => x.Customer).FirstOrDefaultAsync(x => x.TicketReference == reference);
            if (ticket != null)
            {
                _context.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
