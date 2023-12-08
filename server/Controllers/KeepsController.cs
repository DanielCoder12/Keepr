
namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase
{

    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _a0;

    public KeepsController(KeepsService keepsService, Auth0Provider a0)
    {
        _keepsService = keepsService;
        _a0 = a0;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = _keepsService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps()
    {
        try
        {
            List<Keep> keeps = _keepsService.GetAllKeeps();
            return Ok(keeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = _keepsService.GetKeepByIdAndIncrement(keepId);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [Authorize]
    [HttpPut("{keepId}")]
    public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep KeepData, int keepId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.EditKeep(KeepData, userInfo.Id, keepId);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{keepId}")]
    public async Task<ActionResult<string>> DestroyKeep(int keepId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string message = _keepsService.DestroyKeep(keepId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}