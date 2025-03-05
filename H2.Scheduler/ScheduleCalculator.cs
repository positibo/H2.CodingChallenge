namespace H2.Scheduler
{
    public class ScheduleCalculator
    {
        private readonly List<PlanTask> tasks;

        public ScheduleCalculator(List<PlanTask> tasks) => this.tasks = tasks;

        public void Calculate(DateTime startDate)
        {
            var queue = new Queue<PlanTask>(tasks.Where(o => o.Dependencies.Count == 0));
            while (queue.Count > 0)
            {
                var task = queue.Dequeue();
                task.StartDate = startDate;
                task.EndDate = startDate.AddDays(task.Duration);
                Console.WriteLine($"Plan Task: {task.Name}");
                Console.WriteLine($"Start Date: {task.StartDate.Value.ToShortDateString()}");
                Console.WriteLine($"End Date: {task.EndDate.Value.ToShortDateString()}");
                Console.WriteLine("==========================");

                foreach (var dependencyTask in tasks.Where(o => o.Dependencies.Contains(task)))
                {
                    dependencyTask.Dependencies.Remove(task);
                    if(dependencyTask.Dependencies.Count == 0)
                    {
                        queue.Enqueue(dependencyTask);
                        dependencyTask.StartDate = task.EndDate.Value.AddDays(1);
                    }
                }

                startDate = task.EndDate.Value.AddDays(1);
            }
        }
    }
}
