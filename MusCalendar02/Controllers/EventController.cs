using Microsoft.AspNetCore.Mvc;
using MusCalendar02.Models;
using MusCalendar02.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusCalendar02.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : Controller
    {
        private readonly EventService _eService;

        public EventController(EventService eServices) =>
            _eService = eServices;

        [HttpGet]
        public async Task<List<Event>> GetEvents() =>
            await _eService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Event>> GetEventById(string id)
        {
            var evento = await _eService.GetByIdAsync(id);

            if (evento is null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [HttpGet("/title/{title}")]
        public async Task<ActionResult<Event>> GetEventByTitle(string title)
        {
            var evento = await _eService.GetByTitleAsync(title);

            if (evento is null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        //[HttpGet("/month/{month}")]
        //public async Task<ActionResult<Event>> GetEventByMonth(int month)
        //{
        //    var evento = await _eService.GetByDateMonthAsync(month);

        //    if (evento is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(evento);
        //}

        [HttpPost]
        public async Task<IActionResult> PostEvent(Event evento, int day, int month, int year)
        {
            //Implementação da Data
            evento.Data = new DateTime(year, month, day);
            //*****

            await _eService.InsertOneAsync(evento);

            Console.WriteLine(evento);

            return CreatedAtAction(nameof(GetEventById), new { id = evento.Id }, evento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(string id, Event alterEvento)
        {
            var evento = await _eService.GetByIdAsync(id);

            alterEvento.Id = evento.Id;

            await _eService.UpdateOneAsync(id, alterEvento);

            if (evento is null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var evento = await _eService.GetByIdAsync(id);

            if (evento is null)
            {
                return NotFound();
            }

            await _eService.DeleteAsync(id);

            return NoContent();
        }
    }
}
