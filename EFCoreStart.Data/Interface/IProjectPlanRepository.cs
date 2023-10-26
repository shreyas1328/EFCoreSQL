using System;
using EFCoreStart.Data.Models.Entity;
using EFCoreStart.Data.Models.Request;

namespace EFCoreStart.Data.Interface
{
	public interface IProjectPlanRepository
	{
		Task<List<ProjectPlanDto>> GetAllProjectPlan(int projectId);
        Task<int> AddProjectPlan(ProjectPlanRequest projectPlanRequest);
        Task<int> AddProjectPlanWeeks(ProjectPlanRequest projectPlanRequest);
    }
}

