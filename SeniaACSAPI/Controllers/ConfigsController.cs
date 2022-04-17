#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using SeniaACSAPI.Data;
using SeniaACSAPI.DTOs;
using SeniaACSAPI.Models;

namespace SeniaACSAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class ConfigsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ConfigsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Configs.ToListAsync());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _context.Configs.FindAsync(id));
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConfigDTOCreate config)
        {

            _context.Configs.Add(config.Adapt<Config>());

            await _context.SaveChangesAsync();
            return Ok(config);
        }

        // POST
        [HttpPost("command")]
        public async Task<IActionResult> ExecuteConfig([FromBody] ConfigDTORun config)
        {
            try
            {
                using (var client = new SshClient(config.IPAddress, config.Username, config.Password))
                {
                    client.Connect();

                    var cmdResult = "";

                    var reader = "";

                    string[] commandArray = config.Command.Split(';');

                    foreach (var command in commandArray)
                    {
                        if(command != "")
                        {
                            Console.WriteLine(command);
                            var cmd = client.CreateCommand(command);

                            cmdResult += cmd.Execute();

                            var readerStream = new StreamReader(cmd.ExtendedOutputStream);

                            reader = readerStream.ReadToEnd();
                        }
                    }

                    return Ok(reader.ToString());
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
                throw;
            }
        }

        // EDIT
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Config config)
        {
            if (config.Id != id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Configs.Update(config);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigExists(config.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var config = await _context.Configs.FindAsync(id);
            _context.Configs.Remove(config);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ConfigExists(string id)
        {
            return _context.Configs.Any(e => e.Id == id);
        }
    }
}
