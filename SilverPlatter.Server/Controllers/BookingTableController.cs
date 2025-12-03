using Microsoft.AspNetCore.Mvc;
using SilverPlatter.Server.Models;
using SilverPlatter.Server.Repositories;

[ApiController]
[Route("api/[controller]")]
public class BookingTableController : ControllerBase
{
    private readonly IBookingTableRepository _repo;

    public BookingTableController(IBookingTableRepository bookingTableRepository)
    {
        _repo = bookingTableRepository;   
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var tables = _repo.GetAll();
            return Ok(tables);
        } catch
        {
            return BadRequest("Failed to get all tables");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var bookingTable = _repo.GetById(id);
            if (bookingTable == null)
            {
                return NotFound(); // No item found
            }
            return Ok(bookingTable);
        } catch
        {
            return BadRequest("Failed to get booking table by id");
        }
    }

    [HttpPost]
    public IActionResult Create(BookingTable table)
    {
        try
        {
            var created = _repo.Add(table);
                    
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        } catch
        {
            return BadRequest("Failed to add new table to the resturant");
        }

    }

    [HttpPut]
    public IActionResult Update(int id, BookingTable table)
    {
        if (id != table.Id)
        {
            return BadRequest("given id and table id are not the same");
        }

        try
        {
            var updated = _repo.Update(table);
            return Ok(updated);
        } catch
        {
            return BadRequest("Bad request for update to the repo");
        }    
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = _repo.GetById(id);
        if (existing == null)
        {
            return NotFound();
        }

        try
        {
            _repo.RemoveById(id);
            return NoContent();
        } catch
        {
            return BadRequest("Failed to remove booking table by id");
        }   
    }
}