using contactAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace contactAPI.Data
{
    public class ContactsApidbContext:DbContext
    {
        public ContactsApidbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Contact>Contacts { get; set; }

    }
}
