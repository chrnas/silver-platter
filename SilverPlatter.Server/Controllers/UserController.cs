using Microsoft.AspNetCore.Mvc;
using SilverPlatter.Server.Models;
using SilverPlatter.Server.Repositories;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UserController(IUserRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_repo.GetAll());
        }
        catch
        {
            return BadRequest("Failed to get users");
        }
    }

    [HttpGet("id={id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var user = _repo.GetById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        catch
        {
            return BadRequest("Failed to get user by id");
        }
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        try
        {
            var created = _repo.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch
        {
            return BadRequest("Failed to create user");
        }
    }

    [HttpPut("id={id}")]
    public IActionResult Update(int id, User user)
    {
        if (id != user.Id)
            return BadRequest("Id mismatch");

        try
        {
            return Ok(_repo.Update(user));
        }
        catch
        {
            return BadRequest("Failed to update user");
        }
    }

    [HttpDelete("id={id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            bool removed = _repo.RemoveById(id);
            if (!removed)
                return NotFound();

            return NoContent();
        }
        catch
        {
            return BadRequest("Failed to delete user");
        }
    }
}
