using H2.Scheduler;

namespace H2.UnitTests
{
    public class OptimizedScheduleTests
    {
        [Fact]
        public void PlanTasksWithDependencies()
        {
            // Arrange
            var task1 = new PlanTask("Task 1", 5);
            var task2 = new PlanTask("Task 2", 3);
            var task3 = new PlanTask("Task 3", 4);
            var task4 = new PlanTask("Task 4", 2);

            task2.AddDependency(task1);
            task3.AddDependency(task1);
            task4.AddDependency(task2);
            task4.AddDependency(task3);

            var tasks =  new List<PlanTask>() { task1, task2, task3, task4 };
            var scheduler = new OptimizedScheduleCalculator(tasks);

            // Act
            scheduler.Calculate(new DateTime(2025, 3 ,6));

            // Assert
            Assert.Equal(new DateTime(2025, 3, 6), task1.StartDate);
            Assert.Equal(new DateTime(2025, 3, 11), task1.EndDate);

            Assert.Equal(new DateTime(2025, 3, 11), task2.StartDate);
            Assert.Equal(new DateTime(2025, 3, 14), task2.EndDate);

            Assert.Equal(new DateTime(2025, 3, 11), task3.StartDate);
            Assert.Equal(new DateTime(2025, 3, 15), task3.EndDate);

            Assert.Equal(new DateTime(2025, 3, 15), task4.StartDate);
            Assert.Equal(new DateTime(2025, 3, 17), task4.EndDate);
        }
    }
}