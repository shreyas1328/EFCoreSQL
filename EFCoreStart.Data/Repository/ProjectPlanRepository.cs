using System;
using System.Linq;
using EFCoreStart.Data.Data;
using EFCoreStart.Data.Interface;
using EFCoreStart.Data.Models.Entity;
using EFCoreStart.Data.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace EFCoreStart.Data.Repository
{
	public class ProjectPlanRepository: IProjectPlanRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectPlanRepository(ApplicationDbContext dbContext)
		{
            _dbContext = dbContext;

        }

        public async Task<List<ProjectPlanDto>> GetAllProjectPlan(int projectId)
        {
            try
            {
                var getAll = await _dbContext.ProjectPlan.ToListAsync();
                return getAll;
            }
            catch(Exception ex)
            {
                return new List<ProjectPlanDto>();
            }
        }

        public async Task<int> AddProjectPlan(ProjectPlanRequest projectPlanRequest)
        {
            try
            {
                var _projectPlan = await _dbContext.ProjectPlan.Where(x => x.ProjectId == projectPlanRequest.ProjectId).ToListAsync();

                var _phaseNameExits = _projectPlan.Any(x => x.PhaseName.ToLower() == projectPlanRequest.PhaseName.ToLower());
                if (_phaseNameExits)
                {
                    return -1;
                }

                var projectPlan = new ProjectPlanDto
                {
                    PhaseName = projectPlanRequest.PhaseName,
                    ProjectId = projectPlanRequest.ProjectId
                };
                await _dbContext.ProjectPlan.AddAsync(projectPlan);
                await _dbContext.SaveChangesAsync();

                var ids = _projectPlan.Select(x => x.Id).ToList();
                var _existingMaxWeek = (await _dbContext.ProjectPlanDetails.Where(x => ids.Contains(x.PhasePlanId)).ToListAsync())?.MaxBy(x => x.WeekIndex);
                int maxWeek = _existingMaxWeek == null ? projectPlanRequest.NoOfWeeks : projectPlanRequest.NoOfWeeks + _existingMaxWeek.WeekIndex;
                if (maxWeek > 0)
                {
                    var projectPhaseDetails = new List<ProjectPlanDetailsDto>();
                    for (var i = 1; i <= maxWeek; i++)
                    {
                        projectPhaseDetails.Add(new ProjectPlanDetailsDto
                        {
                            IsActive = false,
                            PhasePlanId = projectPlan.Id,
                            WeekIndex = i
                        });
                    }
                    await _dbContext.ProjectPlanDetails.AddRangeAsync(projectPhaseDetails);
                    await _dbContext.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                var exs = ex;
                return -1;
            }
        }

        public async Task<int> AddProjectPlanWeeks(ProjectPlanRequest projectPlanRequest)
        {
            try
            {
                var _projectPlan = await _dbContext.ProjectPlan.Where(x => x.ProjectId == projectPlanRequest.ProjectId).ToListAsync();

                var ids = _projectPlan.Select(x => x.Id).ToList();
                var _existingMaxWeek = (await _dbContext.ProjectPlanDetails.Where(x => ids.Contains(x.PhasePlanId)).ToListAsync())?.MaxBy(x => x.WeekIndex);

                int maxWeek = _existingMaxWeek == null ? projectPlanRequest.NoOfWeeks : projectPlanRequest.NoOfWeeks + _existingMaxWeek.WeekIndex;

                if (maxWeek > 0)
                {
                    var projectPhaseDetails = new List<ProjectPlanDetailsDto>();
                    for (var i = _existingMaxWeek.WeekIndex + 1; i <= maxWeek; i++)
                    {
                        foreach(var item in _projectPlan)
                        {
                            projectPhaseDetails.Add(new ProjectPlanDetailsDto
                            {
                                IsActive = false,
                                PhasePlanId = item.Id,
                                WeekIndex = i
                            });
                        }
                    }
                    await _dbContext.ProjectPlanDetails.AddRangeAsync(projectPhaseDetails);
                    await _dbContext.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                var exs = ex;
                return -1;
            }
        }
    }
}

