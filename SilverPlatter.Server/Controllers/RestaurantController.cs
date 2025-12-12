using Microsoft.AspNetCore.Mvc;
using SilverPlatter.Server.Models;
using SilverPlatter.Server.Repositories;
using System;

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

    [HttpGet("random")]
    public IActionResult GetRandom()
    {
        try
        {
            var restaurants = _repo.GetAll().ToList();

            if (restaurants.Count == 0)
            {
                return NotFound("No restaurants available.");
            }

            var randomIndex = Random.Shared.Next(restaurants.Count);
            var randomRestaurant = restaurants[randomIndex];

            return Ok(randomRestaurant);
        }
        catch
        {
            return BadRequest("Failed to get random restaurant.");
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

    [HttpPut("{id}")]
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
            return BadRequest("Failed to remove restaurant by id");
        }
    }
}