using Microsoft.AspNetCore.Mvc;
using SilverPlatter.Server.Models;
using SilverPlatter.Server.Repositories;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UserController(IUserRepository userRepository)
    {
        _repo = userRepository;   
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var users = _repo.GetAll();
            return Ok(users);
        } catch
        {
            return BadRequest("Failed to get all users");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var user = _repo.GetById(id);
            if (user == null)
            {
                return NotFound(); // No item found
            }

            return Ok(user);
        } catch
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
        } catch
        {
            return BadRequest("Failed to add new user to the users list ");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest("given id and user id are not the same");
        }

        try
        {
            var updated = _repo.Update(user);
            return Ok(updated);
        } catch
        {
            return BadRequest("Bad request for update to the repo");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            bool isRemoved = _repo.RemoveById(id);
            if (!isRemoved)
            {
                return NotFound();
            }
            return NoContent();
        } catch
        {
            return BadRequest("Failed to remove MenuEntry by id");
        }
    }
}
