using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotalSynergyWebApi.Models;
using TotalSynergyWebApi.Models.DataModels;
using TotalSynergyWebApi.Repository;

namespace TotalSynergyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IContactService _contactservice;

        public ContactsController(ProjectDbContext context, IContactService contactservice)
        {
            _context = context;
            _contactservice = contactservice;
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            return _contactservice.GetAllObj();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _contactservice.GetObjById(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            try
            {
                await _contactservice.UpsertObj(id, contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProject", new { id = contact.Id }, contact);
        }

        [HttpGet("GetProjectContactDependency/{id}")]
        public async Task<IActionResult> GetProjectContactDependency([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projects = await _contactservice.GetProjectContactDependency(id);

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        // POST: api/Contacts
        [HttpPost("UpsertContact")]
        public async Task<IActionResult> UpsertContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _contactservice.UpsertObj(contact.Id, contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            await _contactservice.RemoveObj(contact);

            return Ok(contact);
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}