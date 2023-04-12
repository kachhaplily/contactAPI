using contactAPI.Data;
using contactAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contactAPI.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactsApidbContext dbContext;

        public ContactsController(ContactsApidbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());

        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult>GetSingleContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact == null) { return NotFound(id); } return Ok(contact);


        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateContact([FromRoute] Guid id, UpdateContactsRequest updateContactsRequest)
        {
          var contact=await dbContext.Contacts.FindAsync(id);
            if(contact != null)
            {
                contact.FullName=updateContactsRequest.FullName;
                contact.Phone=updateContactsRequest.Phone;  
                contact.Email=updateContactsRequest.Email;
                contact.Address=updateContactsRequest.Address;
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteContact([FromRoute]Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null) {dbContext.Remove(contact);dbContext.SaveChangesAsync();return Ok("delete sucessfully");}
            return NotFound();
        }
             
        }

       

    }
    

    

