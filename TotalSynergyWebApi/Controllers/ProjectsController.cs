using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotalSynergyWebApi.Models;
using TotalSynergyWebApi.Models.DataModels;
using TotalSynergyWebApi.Models.Interfaces;
using TotalSynergyWebApi.Repository;

namespace TotalSynergyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IProjectService _projectservice; 

        public ProjectsController(ProjectDbContext context, IProjectService projectservice)
        {
            _context = context;
            _projectservice = projectservice;
        }

        // GET: api/Projects
        [HttpGet]
        public IEnumerable<IProject> GetProjects()
        {
            return _projectservice.GetAllProjects();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _projectservice.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
       

        // POST: api/Projects
        [HttpPost("UpsertProject")]
        public async Task<IActionResult> UpsertProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ifExis = await _context.Projects.AnyAsync(p => p.Id == project.Id);

            if (project.Id > 0 && !ifExis) {
                project.Id = 0;
            }

            
            var listOfrojectContactItems = project.ProjectContactItems.ToList();

            project.ProjectContactItems = null;
            listOfrojectContactItems.ForEach(l => l.Contact = null);

            try {
                var rtProject = await _projectservice.UpsertObj(project.Id, project);

                if (!ifExis && listOfrojectContactItems != null)
                {
                    
                    listOfrojectContactItems.ForEach(l => l.ProjectId = rtProject.Id);
                    await _projectservice.AddRangeProjectContact(listOfrojectContactItems);
                }
                else if (ifExis && listOfrojectContactItems != null) {
                    await _projectservice.DeleteRangeProjectContact(project.Id);
                    await _projectservice.AddRangeProjectContact(listOfrojectContactItems);
                }
            }
            catch (DbUpdateException) {
                throw;
            }

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            try
            {
                await  _projectservice.DeleteRangeProjectContact(project.Id);
                await _projectservice.RemoveObj(project);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return Ok(project);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}