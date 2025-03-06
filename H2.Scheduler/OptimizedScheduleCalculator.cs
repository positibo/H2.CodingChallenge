using System.Threading.Tasks;

namespace H2.Scheduler
{
    public class OptimizedScheduleCalculator
    {
        private readonly List<PlanTask> tasks;

        public OptimizedScheduleCalculator(List<PlanTask> tasks) => this.tasks = tasks;

        public void Calculate(DateTime startDate)
        {
            var sortedTasks = SortTasks(tasks);
            foreach (var task in sortedTasks) 
            {
                DateTime? maxEndDate = null;
                if (task.Dependencies.Any())
                {
                    maxEndDate = task.Dependencies.Max(o => o.EndDate);
                }
                task.StartDate = maxEndDate ?? startDate;
                task.EndDate = task.StartDate.Value.AddDays(task.Duration);

                Console.WriteLine($"Plan Task: {task.Name}");
                Console.WriteLine($"Start Date: {task.StartDate.Value.ToShortDateString()}");
                Console.WriteLine($"End Date: {task.EndDate.Value.ToShortDateString()}");
                Console.WriteLine("==========================");
            }
        }

        public List<PlanTask> SortTasks(List<PlanTask> tasks) 
        {
            var sortedTasks = new List<PlanTask>();
            var visitedTask = new HashSet<PlanTask>();
            foreach (var task in tasks)
            {
                SortLogic(task, visitedTask, sortedTasks);
            }

            return sortedTasks;
        }

        private void SortLogic(PlanTask task, HashSet<PlanTask> visitedTask, List<PlanTask> sortedTasks)
        {
            if (!visitedTask.Contains(task))
            {
                visitedTask.Add(task);
                foreach (var dependencyTask in task.Dependencies)
                {
                    SortLogic(dependencyTask, visitedTask, sortedTasks);
                }

                sortedTasks.Add(task);
            }
        }
    }
}
