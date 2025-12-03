
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
        } catch
        {
            return BadRequest("Failed to get all tables");
        }
        
        return Ok(tables);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var bookingTable = _repo.GetById(id);
        } catch
        {
            return BadRequest("Failed to get booking table by id");
        }
        if (bookingTable == null)
        {
            return NotFound(); // No item found
        }

        return Ok(bookingTable);
    }

    [HttpPost]
    public IActionResult Create(bookingTable table)
    {
        try
        {
            var created = _repo.Add(table);
        } catch
        {
            return BadRequest("Failed to add new table to the resturant");
        }
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost]
    public IActionResult Update(int id, bookingTable table)
    {
        if (id != table.Id)
        {
            return BadRequest("given id and table id are not the same");
        }

        try
        {
            var updated = _repo.Update(table);
        } catch
        {
            return BadRequest("Bad request for update to the repo");
        }

        return Ok(updated);
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
        } catch
        {
            return BadRequest("Failed to remove booking table by id");
        }
        return NoContent();
    }
}