
[ApiController]
[Route("api/[controller]")]
public class ResturantController : ControllerBase
{
    private readonly IMenuEntryRepository _repo;

    public ResturantController(IMenuEntryRepository menuEntryRepository)
    {
        _repo = menuEntryRepository;   
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var menuEntries = _repo.GetAll();
        } catch
        {
            return BadRequest("Failed to get all menuEntries");
        }
        
        return Ok(menuEntries);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var menuEntry = _repo.GetById(id);
        } catch
        {
            return BadRequest("Failed to get menuEntry by id");
        }
        if (menuEntry == null)
        {
            return NotFound(); // No item found
        }

        return Ok(menuEntry);
    }

    [HttpPost]
    public IActionResult Create(MenuEntry entry)
    {
        try
        {
            var created = _repo.Add(entry);
        } catch
        {
            return BadRequest("Failed to add new item to the menu");
        }
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost]
    public IActionResult Update(int id, MenuEntry entry)
    {
        if (id != entry.Id)
        {
            return BadRequest("given id and entry id are not the same");
        }

        try
        {
            var updated = _repo.Update(entry);
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
            return BadRequest("Failed to remove MenuEntry by id");
        }
        return NoContent();
    }
}