// See https://aka.ms/new-console-template for more information
using H2.Scheduler;

Console.WriteLine("Project Task Scheduler");

var task1 = new PlanTask("Task 1", 6);
var task2 = new PlanTask("Task 2", 3);
var task3 = new PlanTask("Task 3", 2);
var task4 = new PlanTask("Task 4", 4);

task2.AddDependency(task1);
task3.AddDependency(task1);
task4.AddDependency(task2);
task4.AddDependency(task3);

var tasks = new List<PlanTask>() { task1, task2, task3, task4 };

var scheduler = new ScheduleCalculator(tasks);
scheduler.Calculate(DateTime.Now);