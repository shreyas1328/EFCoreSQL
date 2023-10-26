namespace EFCoreStart.Data.Models.Entity
{
	public class ProjectPlanDetailsDto
	{
		public int Id { get; set; }
		public int PhasePlanId { get; set; }
		public int WeekIndex { get; set; }
		public bool IsActive { get; set; }
	}
}

