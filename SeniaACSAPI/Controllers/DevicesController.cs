#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniaACSAPI.Data;
using SeniaACSAPI.DTOs;
using SeniaACSAPI.Models;

namespace SeniaACSAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Devices.ToListAsync());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _context.Devices.FindAsync(id));
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeviceDTOCreate device)
        {

            _context.Devices.Add(device.Adapt<Device>());

            await _context.SaveChangesAsync();
            return Ok(device);
        }


        // EDIT
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Device device)
        {
            if (device.Id != id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Devices.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            var device = await _context.Devices.FindAsync(id);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool DeviceExists(string id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
