using Microsoft.AspNetCore.Mvc;
using SilverPlatter.Server.Models;
using SilverPlatter.Server.Repositories;

[ApiController]
[Route("api/[controller]")]
public class AllergyController : ControllerBase
{
    private readonly IAllergyRepository _repo;

    public AllergyController(IAllergyRepository allergyRepository)
    {
        _repo = allergyRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var allergies = _repo.GetAll();
            return Ok(allergies);
        }
        catch
        {
            return BadRequest("Failed to get all allergies");
        }
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetByUserId(int userId)
    {
        try
        {
            var allergies = _repo.GetByUserId(userId);
            return Ok(allergies);
        }
        catch
        {
            return BadRequest("Failed to get allergies by userId");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var allergy = _repo.GetById(id);
            if (allergy == null)
            {
                return NotFound();
            }

            return Ok(allergy);
        }
        catch
        {
            return BadRequest("Failed to get allergy by id");
        }
    }

    [HttpPost]
    public IActionResult Create(Allergy allergy)
    {
        try
        {
            var created = _repo.Add(allergy);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch
        {
            return BadRequest("Failed to add new allergy");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Allergy allergy)
    {
        if (id != allergy.Id)
        {
            return BadRequest("Given id and allergy id do not match");
        }

        try
        {
            var updated = _repo.Update(allergy);
            return Ok(updated);
        }
        catch
        {
            return BadRequest("Failed to update allergy");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            bool removed = _repo.RemoveById(id);
            if (!removed)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch
        {
            return BadRequest("Failed to delete allergy");
        }
    }
}
