using EFCoreStart.Data.Interface;
using EFCoreStart.Data.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreStart.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IProjectPlanRepository _projectPlanRepository;

    public WeatherForecastController(IProjectPlanRepository projectPlanRepository)
    {
        _projectPlanRepository = projectPlanRepository;
    }

    [HttpGet]
    public async Task<bool> GetAll([FromQuery] int projectId)
    {
        try
        {
            var getAll = await _projectPlanRepository.GetAllProjectPlan(projectId);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    [HttpPost("/phase")]
    public async Task<bool> PostPhase([FromBody] ProjectPlanRequest projectPlanRequest)
    {
        try
        {
            var add = await _projectPlanRepository.AddProjectPlan(projectPlanRequest);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    [HttpPost("/phase-week")]
    public async Task<bool> PostPhaseWeek([FromBody] ProjectPlanRequest projectPlanRequest)
    {
        try
        {
            var add = await _projectPlanRepository.AddProjectPlanWeeks(projectPlanRequest);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}

