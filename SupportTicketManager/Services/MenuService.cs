using SupportTicketManager.Models;


namespace SupportTicketManager.Services
{
    internal class MenuService
    {
        Ticket selectedTicket;

        public async Task MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Välkommen till funktionen för hantering av felanmälningsärenden!");
            Console.WriteLine();
            Console.WriteLine("1. Skapa nytt ärende");
            Console.WriteLine("2. Visa alla ärenden");
            Console.WriteLine("3. Visa specifikt ärende");
            Console.WriteLine("4. Uppdatera ärende");
            Console.WriteLine("5. Ta bort ärende");
            Console.WriteLine("6. Avsluta programmet");
            Console.WriteLine();
            Console.Write("Ange ditt val: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await OptionOne();
                    break;

                case "2":
                    await OptionTwo();
                    break;

                case "3":
                    await OptionThree();
                    break;

                case "4":
                    await OptionFour();
                    break;

                case "5":
                    await OptionFive();
                    break;

                case "6":
                    OptionSix();
                    break;
            }
        }


        private void NotFound(string referenceNumber)
        {
            Console.Clear();
            Console.WriteLine($"Inget ärende med ärendenummer {referenceNumber} hittades.");
            Console.WriteLine();
            Console.WriteLine("Tryck valfri tangent för att återgå till huvudmenyn");
            Console.ReadKey();
            MainMenu();
        }

        #region Option 1 - Create New Ticket

        private async Task OptionOne()
        {
            Console.Clear();
            Console.WriteLine("Skapa nytt ärende");
            Console.WriteLine();
            await CreateNewCustomerAsync();
            Console.WriteLine();
            Console.WriteLine("Tryck valfri tangent för att återgå till huvudmenyn");
            Console.ReadKey();
        }

        public async Task CreateNewCustomerAsync()
        {
            var ticket = new Ticket();

            Console.Write("Beskrivning av ärendet: ");
            ticket.TicketDescription = Console.ReadLine() ?? "";
            Console.WriteLine();
            Console.Write("Ange titel/nyckelord: ");
            ticket.TicketTitle = Console.ReadLine() ?? "";
            Console.WriteLine();



            bool state = true;

            do
            {
                Console.Write("Ange fastighet: ");
                string selectedBuilding = Console.ReadLine() ?? "";
                var _building = await TicketService.GetBuildingAsync(selectedBuilding);
                
                if(_building != null) 
                {
                    ticket.BuildingName = _building.BuildingName;
                    state = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Byggnaden finns inte i företagets fastighetsregister.");
                }
                
            } while (state);

            Console.WriteLine();
                                   
            
            Console.Write("Kundens förnamn: ");
            ticket.CustomerFirstName = Console.ReadLine() ?? "";
            Console.Write("Kundens efternamn: ");
            ticket.CustomerLastName = Console.ReadLine() ?? "";
            Console.Write("Kundens e-postadress: ");
            ticket.CustomerEmail = Console.ReadLine() ?? "";
            Console.Write("Kundens telefonnummer: ");
            ticket.CustomerPhone = Console.ReadLine() ?? "";
            Console.WriteLine();
            string referenceNumber = GenerateReferenceNumber();
            Console.WriteLine("Ärendets referensnummer: " + referenceNumber);
            ticket.TicketReference = referenceNumber;

            //Save ticket to database
            await TicketService.SaveAsync(ticket);
        }


        private string GenerateReferenceNumber()
        {
            string randomInt = "";

            Random rndInt = new Random();
            for (int i = 0; i < 4; i++)
            {
                randomInt += rndInt.Next(1, 9).ToString();
            }


            Random ran = new Random();
            String b = "abcdefghijklmnopqrstuvwxyz";
            String randomLetter = "";
            for (int i = 0; i < 1; i++)
            {
                int a = ran.Next(26);
                randomLetter = randomLetter + b.ElementAt(a);
            }
            return randomLetter.ToUpper() + randomInt;
        }

        #endregion


        #region Option 2 - List All Tickets

        private async Task OptionTwo()
        {
            Console.Clear();
            Console.WriteLine("Visar alla ärenden");
            await ListAllContactsAsync();
            Console.WriteLine();
            Console.WriteLine("Tryck valfri tangent för att återgå till huvudmenyn");
            Console.ReadKey();
        }

        private async Task ListAllContactsAsync()
        {
            var tickets = await TicketService.GetAllAsync();

            if (tickets.Any())
            {
                foreach (Ticket ticket in tickets)
                {
                    Console.WriteLine();
                    Console.WriteLine("===================================================");
                    Console.WriteLine();
                    Console.WriteLine($"Ärende {ticket.TicketReference} skapat {ticket.TicketCreated}");
                    Console.WriteLine();
                    Console.WriteLine($"Titel/Nyckelord: {ticket.TicketTitle}");
                    Console.WriteLine($"Beskrivning: {ticket.TicketDescription}");
                    Console.WriteLine($"Fastighet: {ticket.PropertyCode} {ticket.BuildingName}");

                    Console.WriteLine();
                    Console.WriteLine($"Status: {ticket.TicketStatus}");
                    if (ticket.TicketComment != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Kommentar: {ticket.TicketComment} (senast uppdaterat {ticket.TicketCommentUpdated})");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Kundinformation");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {ticket.CustomerFirstName}");
                    Console.WriteLine($"Efternamn: {ticket.CustomerLastName}");
                    Console.WriteLine($"E-postadress: {ticket.CustomerEmail}");
                    Console.WriteLine($"Telefonnummer: {ticket.CustomerPhone}");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Inga ärenden finns i databasen");
                Console.WriteLine();
            };
        }

        #endregion


        #region Option 3 - List Specific Ticket

        private async Task OptionThree()
        {
            Console.Clear();
            Console.WriteLine("Visa specifikt ärende");
            Console.WriteLine();
            Console.Write("Ange ärendenummer: ");
            var referenceNumber = Console.ReadLine() ?? "";
            Console.WriteLine();
            await ListSpecificContactAsync(referenceNumber);
            Console.WriteLine();
            Console.WriteLine("Tryck valfri tangent för att återgå till huvudmenyn");
            Console.ReadKey();
        }

        private async Task ListSpecificContactAsync(string referenceNumber)
        {
            if (!string.IsNullOrEmpty(referenceNumber))
            {
                var ticket = await TicketService.GetAsync(referenceNumber!);
                selectedTicket = ticket;

                if (ticket != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Ärende {ticket.TicketReference} skapat {ticket.TicketCreated}");
                    Console.WriteLine();
                    Console.WriteLine($"Titel/Nyckelord: {ticket.TicketTitle}");
                    Console.WriteLine($"Beskrivning: {ticket.TicketDescription}");
                    Console.WriteLine($"Fastighet: {ticket.PropertyCode} {ticket.BuildingName}");

                    Console.WriteLine();
                    Console.WriteLine($"Status: {ticket.TicketStatus}");
                    if (ticket.TicketComment != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Kommentar: {ticket.TicketComment} (senast uppdaterat {ticket.TicketCommentUpdated})");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Kundinformation");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {ticket.CustomerFirstName}");
                    Console.WriteLine($"Efternamn: {ticket.CustomerLastName}");
                    Console.WriteLine($"E-postadress: {ticket.CustomerEmail}");
                    Console.WriteLine($"Telefonnummer: {ticket.CustomerPhone}");
                    Console.WriteLine();
                }
                else
                {
                    NotFound(referenceNumber);
                }
            }
            else
            {
                Console.WriteLine("Inget ärendenummer angivet.");
                Console.ReadKey();
            }
        }

        #endregion


        #region Option 4 - Update Ticket

        private async Task OptionFour()
        {
            Console.Clear();
            Console.WriteLine("Uppdatera kundkontakt");
            Console.WriteLine();
            Console.Write("Ange ärendenummer: ");
            var referenceNumber = Console.ReadLine() ?? "";
            Console.WriteLine();
            await UpdateCycle(referenceNumber);
        }

        public async Task UpdateCycle(string referenceNumber)
        {
            Console.WriteLine();
            await ListSpecificContactAsync(referenceNumber);
            Console.WriteLine();
            await UpdateMenu();
            Console.WriteLine();
        }

        public async Task UpdateMenu()
        {
            Console.WriteLine("Vilken information vill du uppdatera?");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("1. Beskrivning och titel/nyckelord");
            Console.WriteLine("2. Kundinformation för ärendet");
            Console.WriteLine("3. Status");
            Console.WriteLine("4. Kommentar");
            Console.WriteLine();
            Console.Write("Ange ditt val eller tryck valfri tangent för att återgå till huvudmenyn: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await UpdateTicketDescription();
                    break;

                case "2":
                    await UpdateCustomerInfo();
                    break;

                case "3":
                    await UpdateTicketStatus();
                    break;

                case "4":
                    await UpdateTicketComment(); ;
                    break;

                case "5":
                    await MainMenu();
                    break;
            }
        }


        private async Task UpdateTicketDescription()
        {
            Console.Clear();
            Console.Write($"Ärendets nuvarande beskrivning: ({selectedTicket.TicketDescription})");
            Console.WriteLine();
            Console.Write($"Ny beskrivning: ");

            string ticketDescription = Console.ReadLine() ?? "";
            if (ticketDescription == "")
            {
                ticketDescription = selectedTicket.TicketDescription;
            }
            else
            {
                selectedTicket.TicketDescription = ticketDescription;
            }
            Console.WriteLine();

            Console.Write($"Ny titel/nyckelord: ");

            string ticketTitle = Console.ReadLine() ?? "";
            if (ticketDescription == "")
            {
                ticketTitle = selectedTicket.TicketTitle;
            }
            else
            {
                selectedTicket.TicketTitle = ticketTitle;
            }
            await TicketService.UpdateAsync(selectedTicket);
            Console.Clear();
            await UpdateCycle(selectedTicket.TicketReference);
        }


        private async Task UpdateTicketComment()
        {
            Console.Clear();
            if (selectedTicket.TicketComment != null)
                Console.Write($"Ärendets nuvarande kommentar: ({selectedTicket.TicketComment}): ");
                        
            Console.WriteLine();
            Console.Write($"Ny kommentar: ");

            string ticketComment = Console.ReadLine() ?? "";
            if (ticketComment == "")
            {
                ticketComment = selectedTicket.TicketComment;
            }
            else {
                selectedTicket.TicketComment = ticketComment;
                selectedTicket.TicketCommentUpdated = DateTime.Now;
            }
            await TicketService.UpdateAsync(selectedTicket);
            Console.Clear();
            await UpdateCycle(selectedTicket.TicketReference);
        }

        private async Task UpdateTicketStatus()
        {
            Console.Clear();
            Console.WriteLine($"Ärendets nuvarande status är: {selectedTicket.TicketStatus}");
            Console.WriteLine();

            //int statusId = await TicketService.GetTicketStatusAsync(selectedTicket);

            if (selectedTicket.TicketStatusId == 1)
            {
                Console.WriteLine("2. Pågående");
                Console.WriteLine("3. Avslutat");                

            }
            else if (selectedTicket.TicketStatusId == 2)
            {
                Console.WriteLine("1. Ej Påbörjat");
                Console.WriteLine("3. Avslutat");
            }

            else if (selectedTicket.TicketStatusId == 3)
            {
                Console.WriteLine("1. Ej Påbörjat"); 
                Console.WriteLine("2. Pågående");
            }

            Console.WriteLine();
            Console.Write("Ange ditt val eller tryck valfri tangent för att återgå till huvudmenyn: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    selectedTicket.TicketStatusId = 1;
                    break;

                case "2":
                    selectedTicket.TicketStatusId = 2;
                    break;

                case "3":
                    selectedTicket.TicketStatusId = 3;
                    break;

                case "9":
                    Console.Clear();
                    await ListSpecificContactAsync(selectedTicket.TicketReference);
                    Console.WriteLine();
                    await UpdateMenu();
                    Console.WriteLine();
                    break;
            }
            await TicketService.UpdateAsync(selectedTicket);
            Console.Clear();
            await UpdateCycle(selectedTicket.TicketReference);
        }



        private async Task UpdateCustomerInfo()
        {
            Console.Clear();

            Console.Write($"Förnamn ({selectedTicket.CustomerFirstName}): ");
            string customerFirstName = Console.ReadLine() ?? "";
            if (customerFirstName == "")
            {
                customerFirstName = selectedTicket.CustomerFirstName;
            }
            else
                selectedTicket.CustomerFirstName = customerFirstName;

            Console.Write($"Efternamn ({selectedTicket.CustomerLastName}): ");
            string customerLastName = Console.ReadLine() ?? "";
            if (customerLastName == "")
            {
                customerLastName = selectedTicket.CustomerLastName;
            }
            else
                selectedTicket.CustomerLastName = customerLastName;

            Console.Write($"E-postadress ({selectedTicket.CustomerEmail}): ");
            string CustomerEmail = Console.ReadLine() ?? "";
            if (CustomerEmail == "")
            {
                CustomerEmail = selectedTicket.CustomerEmail;
            }
            else
                selectedTicket.CustomerEmail = CustomerEmail;

            Console.Write($"Telefonnummer ({selectedTicket.CustomerPhone}): ");
            string phone = Console.ReadLine() ?? "";
            if (phone == "")
            {
                phone = selectedTicket.CustomerPhone;
            }
            else
                selectedTicket.CustomerPhone = phone;
        
            await TicketService.UpdateAsync(selectedTicket);
            Console.Clear();
            await UpdateCycle(selectedTicket.TicketReference);
        }

        #endregion


        #region Option 5 - Delete Ticket

        private async Task OptionFive()
        {
            Console.Clear();
            Console.WriteLine("Radera ärende");
            Console.WriteLine();
            await DeleteSpecificContact();
            Console.WriteLine();
            Console.WriteLine("Tryck valfri tangent för att återgå till huvudmenyn");
            Console.ReadKey();
        }

        private async Task DeleteSpecificContact()
        {
            Console.Write("Ange ärendenummer: ");
            var referenceNumber = Console.ReadLine() ?? "";



            
            if (!string.IsNullOrEmpty(referenceNumber))
            {
                var ticket = await TicketService.GetAsync(referenceNumber!);

                if (ticket != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Är du säker på att du vill ta bort ärendet med med ärendenummer {ticket.TicketReference}?");
                    Console.WriteLine();
                    Console.Write("Skriv RADERA för att ta bort ärendet (eller tryck valfri tangent för att avbryta): ");

                    var option = Console.ReadLine();

                    switch (option)
                    {
                        case "RADERA":
                            await TicketService.DeleteAsync(referenceNumber);

                            var ticketDeleted = await TicketService.GetAsync(referenceNumber!);
                            if (ticketDeleted != null)
                                Console.WriteLine("Någonting gick fel...");
                            else
                                Console.Clear();
                                Console.WriteLine();
                                Console.WriteLine("Ärendet raderat.");
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Inget ärende med ärendenummer {referenceNumber} hittades.");
                }
            }
            else
            {
                Console.WriteLine("Ingen ärendenummer angivet.");
            }
        }

        #endregion


        private void OptionSix()
        {
            Environment.Exit(0);
        }
    }
}

