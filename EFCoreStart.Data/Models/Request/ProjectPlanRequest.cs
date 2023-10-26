using System;
namespace EFCoreStart.Data.Models.Request
{
	public class ProjectPlanRequest
	{
        public int ProjectId { get; set; }
        public string? PhaseName { get; set; }
        public int NoOfWeeks { get; set; } = 0;
    }
}

