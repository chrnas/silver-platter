using Microsoft.AspNetCore.Mvc;
using SilverPlatter.Server.Models;
using SilverPlatter.Server.Repositories;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _repo;

    public RestaurantController(IRestaurantRepository restaurantRepository)
    {
        _repo = restaurantRepository;   
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var restaurants = _repo.GetAll();
            return Ok(restaurants);
        } catch
        {
            return BadRequest("Failed to get all restaurants");
        }      
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var restaurant = _repo.GetById(id);
            if (restaurant == null)
            {
                return NotFound(); // No restaurant found
            }
            return Ok(restaurant);
        } catch
        {
            return BadRequest("Failed to get restaurant by id");
        }
    }

    [HttpPost]
    public IActionResult Create(Restaurant restaurant)
    {
        try
        {
            var created = _repo.Add(restaurant);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        } catch
        {
            return BadRequest("Failed to add new resturant");
        }
    }

    [HttpPost]
    public IActionResult Update(int id, Restaurant restaurant)
    {
        if (id != restaurant.Id)
        {
            return BadRequest("given id and restaurant id are not the same");
        }

        try
        {
            var updated = _repo.Update(restaurant);
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
            return BadRequest("Failed to remove restaurant by id");
        }
    }
}