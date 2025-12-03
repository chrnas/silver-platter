using Microsoft.AspNetCore.Mvc;
using SilverPlatter.Server.Models;
using SilverPlatter.Server.Repositories;

[ApiController]
[Route("api/[controller]")]
public class MenuEntryController : ControllerBase
{
    private readonly IMenuEntryRepository _repo;

    public MenuEntryController(IMenuEntryRepository menuEntryRepository)
    {
        _repo = menuEntryRepository;   
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var menuEntries = _repo.GetAll();
            return Ok(menuEntries);
        } catch
        {
            return BadRequest("Failed to get all menuEntries");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var menuEntry = _repo.GetById(id);
            if (menuEntry == null)
            {
                return NotFound(); // No item found
            }

            return Ok(menuEntry);
        } catch
        {
            return BadRequest("Failed to get menuEntry by id");
        }
    }

    [HttpPost]
    public IActionResult Create(MenuEntry entry)
    {
        try
        {
            var created = _repo.Add(entry);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        } catch
        {
            return BadRequest("Failed to add new item to the menu");
        }
    }

    [HttpPut]
    public IActionResult Update(int id, MenuEntry entry)
    {
        if (id != entry.Id)
        {
            return BadRequest("given id and entry id are not the same");
        }

        try
        {
            var updated = _repo.Update(entry);
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
            return BadRequest("Failed to remove MenuEntry by id");
        }
    }
}