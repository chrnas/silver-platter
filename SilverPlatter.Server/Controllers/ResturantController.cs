
[ApiController]
[Route("api/[controller]")]
public class ResturantController : ControllerBase
{
    private readonly IResturantRepository _repo;

    public ResturantController(IResturantRepository resturantRepository)
    {
        _repo = resturantRepository;   
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var resturants = _repo.GetAll();
        } catch
        {
            return BadRequest("Failed to get all resturants");
        }
        
        return Ok(resturants);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var resturant = _repo.GetById(id);
        } catch
        {
            return BadRequest("Failed to get resturant by id");
        }
        if (resturant == null)
        {
            return NotFound(); // No resturant found
        }

        return Ok(resturant);
    }

    [HttpPost]
    public IActionResult Create(Restaurant restaurant)
    {
        try
        {
            var created = _repo.Add(restaurant);
        } catch
        {
            return BadRequest("Failed to add new resturant");
        }
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost]
    public IActionResult Update(int id, Resturant resturant)
    {
        if (id != resturant.Id)
        {
            return BadRequest("given id and resturant id are not the same");
        }

        try
        {
            var updated = _repo.Update(resturant);
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
            return BadRequest("Failed to remove resturant by id");
        }
        return NoContent();
    }
}