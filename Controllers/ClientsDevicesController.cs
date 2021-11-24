using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_GOPR_SERVICE.Models;
using System.Text.Json;

namespace API_GOPR_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsDevicesController : ControllerBase
    {
        private readonly GoprServiceContext _context;

        public ClientsDevicesController(GoprServiceContext context)
        {
            _context = context;
        }

        // GET ALL BY CLIENT(phone number)
        [HttpGet]
        public ActionResult<List<ClientsDevice>> GetClientsDevice(string phoneNumber)
        {
            var clientsDevice =  _context.ClientsDevices
                .Include(c => c.Client)
                .Include(d => d.Device)
                .Where(c => c.Client.PhoneNumber == phoneNumber)
                .ToList();

            if (clientsDevice == null)
            {
                return NotFound();
            }

            return clientsDevice;
        }

        // PUT: api/ClientsDevices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientsDevice(int id, ClientsDevice clientsDevice)
        {
            if (id != clientsDevice.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientsDevice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientsDeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ClientsDevices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientsDevice>> PostClientsDevice(
            ClientsDevice clientsDevice, string phoneNumber, string deviceName)
        {

           
            _context.ClientsDevices.Add(clientsDevice);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetClientsDevice", new { id = clientsDevice.Id }, clientsDevice);
            

        }

        // DELETE: api/ClientsDevices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientsDevice(int id)
        {
            var clientsDevice = await _context.ClientsDevices.FindAsync(id);
            if (clientsDevice == null)
            {
                return NotFound();
            }

            _context.ClientsDevices.Remove(clientsDevice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientsDeviceExists(int id)
        {
            return _context.ClientsDevices.Any(e => e.Id == id);
        }
    }
}
