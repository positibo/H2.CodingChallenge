using H2.Scheduler;

namespace H2.UnitTests
{
    public class ScheduleTests
    {
        [Fact]
        public void PlanTasksWithDependencies()
        {
            // Arrange
            var task1 = new PlanTask("Task 1", 5);
            var task2 = new PlanTask("Task 2", 3);
            task2.AddDependency(task1);

            var tasks =  new List<PlanTask>() { task1, task2 };
            var scheduler = new ScheduleCalculator(tasks);

            // Act
            scheduler.Calculate(new DateTime(2025, 3 ,5));

            // Assert
            Assert.NotNull(task1.StartDate);
            Assert.NotNull(task2.StartDate);
            Assert.True(task2.StartDate > task1.EndDate);
        }

        [Fact]
        public void PlanTasksNoDependencies()
        {
            // Arrange
            var task1 = new PlanTask("Task 1", 5);
            var task2 = new PlanTask("Task 2", 3);

            var tasks = new List<PlanTask>() { task1, task2 };
            var scheduler = new ScheduleCalculator(tasks);

            // Act
            scheduler.Calculate(new DateTime(2025, 3, 5));

            // Assert
            Assert.NotNull(task1.StartDate);
            Assert.NotNull(task2.StartDate);
            Assert.True(task1.StartDate < task2.EndDate);
        }
    }
}