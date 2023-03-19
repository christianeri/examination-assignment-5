using SupportTicketManager.Contexts;
using SupportTicketManager.Services;

//var context = new DataContext();

var menu = new MenuService();

while (true)
{
    Console.Clear();
    await menu.MainMenu();
}