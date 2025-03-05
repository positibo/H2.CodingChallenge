namespace H2.Scheduler
{
    public class PlanTask
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public List<PlanTask> Dependencies { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public PlanTask(string name, int duration) 
        {
            Name = name;
            Duration = duration;
            Dependencies = new List<PlanTask>();
        }

        public void AddDependency(PlanTask task) 
        {
            Dependencies.Add(task);
        }
    }
}
