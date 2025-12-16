using Microsoft.AspNetCore.Mvc;
using SilverPlatter.Server.Models;
using SilverPlatter.Server.Repositories;

[ApiController]
[Route("api/[controller]")]
public class RestaurantFavoriteController : ControllerBase
{
    private readonly IRestaurantFavoriteRepository _repo;

    public RestaurantFavoriteController(IRestaurantFavoriteRepository favoriteRepository)
    {
        _repo = favoriteRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var favorites = _repo.GetAll();
            return Ok(favorites);
        }
        catch
        {
            return BadRequest("Failed to get all restaurant favorites");
        }
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetByUserId(int userId)
    {
        try
        {
            var favorites = _repo.GetByUserId(userId);
            return Ok(favorites);
        }
        catch
        {
            return BadRequest("Failed to get favorites for user");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var favorite = _repo.GetById(id);
            if (favorite == null)
            {
                return NotFound();
            }

            return Ok(favorite);
        }
        catch
        {
            return BadRequest("Failed to get restaurant favorite by id");
        }
    }

    [HttpPost]
    public IActionResult Create(RestaurantFavorite favorite)
    {
        try
        {
            var created = _repo.Add(favorite);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
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
            return BadRequest("Failed to delete restaurant favorite");
        }
    }

    // Extra endpoint: delete by (userId, restaurantId)
    [HttpDelete("{userId}/{restaurantId}")]
    public IActionResult DeleteByUserAndRestaurant(int userId, int restaurantId)
    {
        try
        {
            bool removed = _repo.RemoveByUserAndRestaurant(userId, restaurantId);
            if (!removed)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch
        {
            return BadRequest("Failed to delete restaurant favorite by user+restaurant");
        }
    }
}
