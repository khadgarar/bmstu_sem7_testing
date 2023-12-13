using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using ComponentBuisinessLogic.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;

namespace TechnologicalUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var _config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("dbappsettings.json")
                   .Build();
            var db = new transfersystemContext(Connection.GetConnection(0, _config));
            EmployeeRepository rep = new EmployeeRepository(db);
            Employee user = rep.GetEmployeeByLogin("admin");
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Start>();
                    services.AddSingleton(provider => { return user; });
                    DiExtensions.AddRepositoryExtensions(services);
                    DiExtensions.AddControllerExtensions(services);
                    services.AddDbContext<transfersystemContext>(option => option.UseNpgsql(Connection.GetConnection(user.Permission, _config)));

                    var serilogLogger = new LoggerConfiguration()
                        .WriteTo.File(_config["Logger"]) // Console()
                        .CreateLogger();
                    services.AddLogging(x =>
                    {
                        x.AddSerilog(logger: serilogLogger, dispose: true);
                    });
                });

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var prog = new Start(db);
                    Console.WriteLine("Successfully opened");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                }
            }
        }
        
    }
    class Start
    {
        private EmployeeController _employee;
        private ResponsibleController _responsible;
        private ManagerController _manager;
        private HRController _HR;
        private transfersystemContext _db;
        public Start(transfersystemContext db)
        {
            _db = db;
            Employee user = new Employee();
            IEmployeeRepository employeeRep = new EmployeeRepository(db);
            IObjectiveRepository objectiveRep = new ObjectiveRepository(db);
            IResponsibilityRepository timeSpentRep = new ResponsibilityRepository(db);
            IUserRepository employeeDepartmentRep = new UserRepository(db);

            _employee = new EmployeeController(user, employeeRep, timeSpentRep, objectiveRep);
            _responsible = new ResponsibleController(user, employeeRep, timeSpentRep, objectiveRep);
            _manager = new ManagerController(user, employeeRep, timeSpentRep, objectiveRep);
            _HR = new HRController(user, employeeRep, timeSpentRep, objectiveRep, employeeDepartmentRep);
            _db = db;
            Run();
        }
        public void Run()
        {
            int need = 0;
            while (need != 5)
            {
                PrintMenuMain();
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        CheckEmployee();
                        break;
                    case 2:
                        CheckResponsible();
                        break;
                    case 3:
                        CheckManager();
                        break;
                    case 4:
                        CheckHR();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void PrintMenuMain()
        {
            Console.WriteLine("1 - employee\n2 - responsible\n3 - manager\n4 - HR\n5 - Exit");
        }

        void CheckEmployee()
        {
            int need = 0;
            while (need != 5)
            {
                Console.WriteLine("1 - Получить информацию о пользователе\n" +
                    "2 - Просмотреть все проекты\n" +
                    "3 - Получить проект по ID\n" +
                    "4 - Получить проект по названию\n" +
                    "5 - Exit\n");
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        GetEmployeeByLogin();
                        break;
                    case 2:
                        GetAllTasks();
                        break;
                    case 3:
                        GetTaskByID();
                        break;
                    case 4:
                        GetTaskByTitle();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void GetEmployeeByLogin()
        {
            Employee employee = _employee.GetEmployeeByLogin();
            if (employee != null)
            {
                Console.WriteLine(Convert.ToString(employee.EmployeeId) + " " +
                        Convert.ToString(employee.Login) + " " +
                        Convert.ToString(employee.Permission) + " " +
                        Convert.ToString(employee.Name) + " " +
                        Convert.ToString(employee.Surname));
            }
            else
            {
                Console.WriteLine("Сотрудник не найден");
            }
        }
        void GetAllTasks()
        {
            List<Objective> tasks = _employee.GetAllTasks();
            if (tasks != null)
            {
                foreach (Objective _task in tasks)
                {
                    Console.WriteLine(Convert.ToString(_task.TaskID) + " " +
                        Convert.ToString(_task.ParentTaskId) + " " +
                        Convert.ToString(_task.Title) + " " +
                        Convert.ToString(_task.TermBegin) + " " +
                        Convert.ToString(_task.TermEnd) + " " +
                        Convert.ToString(_task.EstimatedTime));
                }
            }
            else
            {
                Console.WriteLine("Проекты не найдены");
            }
        }
        void GetTaskByID()
        {
            Console.WriteLine("Введите ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            List<Objective> tasks = _employee.GetTaskByID(id);
            if (tasks != null)
            {
                foreach (Objective _task in tasks)
                {
                    Console.WriteLine(Convert.ToString(_task.TaskID) + " " +
                        Convert.ToString(_task.ParentTaskId) + " " +
                        Convert.ToString(_task.Title) + " " +
                        Convert.ToString(_task.TermBegin) + " " +
                        Convert.ToString(_task.TermEnd) + " " +
                        Convert.ToString(_task.EstimatedTime));
                }
            }
            else
            {
                Console.WriteLine("Проект не найден");
            }
        }
        void GetTaskByTitle()
        {
            Console.WriteLine("Введите название:");
            string title = Console.ReadLine();
            List<Objective> tasks = _employee.GetTaskByTitle(title);
            if (tasks != null)
            {
                foreach (Objective _task in tasks)
                {
                    Console.WriteLine(Convert.ToString(_task.TaskID) + " " +
                        Convert.ToString(_task.ParentTaskId) + " " +
                        Convert.ToString(_task.Title) + " " +
                        Convert.ToString(_task.TermBegin) + " " +
                        Convert.ToString(_task.TermEnd) + " " +
                        Convert.ToString(_task.EstimatedTime));
                }
            }
            else
            {
                Console.WriteLine("Проект не найден");
            }
        }

        void CheckResponsible()
        {
            int need = 0;
            while (need != 6)
            {
                Console.WriteLine("1 - Добавить подзадачу\n" +
                    "2 - Изменить задачу\n" +
                    "3 - Добавить ответственного к задаче\n" +
                    "4 - Добавить затраченное время\n" +
                    "5 - Удалить подзадачу\n" +
                    "6 - Exit\n");
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        AddSubtask();
                        break;
                    case 2:
                        UpdateTask();
                        break;
                    case 3:
                        AddResponsible();
                        break;
                    case 4:
                        AddTimeSpent();
                        break;
                    case 5:
                        DeleteSubtask();
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void AddSubtask()
        {
            Console.WriteLine("Введите ID родительской задачи:");
            int parentTaskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите название:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите дату начала:");
            string termBegin = Console.ReadLine();
            Console.WriteLine("Введите дату окончания:");
            string termEnd = Console.ReadLine();
            Console.WriteLine("Введите оценочное время:");
            string estimatedTime = Console.ReadLine();

            var res = _responsible.AddSubtask(parentTaskId, title, termBegin, termEnd, estimatedTime);
            if (res)
            {
                Console.WriteLine("Подзадача создана");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void UpdateTask()
        {
            Console.WriteLine("Введите ID изменяемой задачи:");
            int taskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ID родительской задачи:");
            int parentTaskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите название:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите дату начала:");
            string termBegin = Console.ReadLine();
            Console.WriteLine("Введите дату окончания:");
            string termEnd = Console.ReadLine();
            Console.WriteLine("Введите оценочное время:");
            string estimatedTime = Console.ReadLine();

            var res = _responsible.UpdateTask(taskId, parentTaskId, title, termBegin, termEnd, estimatedTime);
            if (res)
            {
                Console.WriteLine("Подзадача изменена");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void AddResponsible()
        {
            Console.WriteLine("Введите ID задачи:");
            int taskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ID ответственного:");
            int employeeId = Convert.ToInt32(Console.ReadLine());

            var res = _responsible.AddTimeSpent(taskId, employeeId);
            if (res)
            {
                Console.WriteLine("Ответственный добавлен");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void AddTimeSpent()
        {
            Console.WriteLine("Введите ID задачи:");
            int taskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ID ответственного:");
            int employeeId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите затраченное время:");
            string timeAmount = Console.ReadLine();

            var res = _responsible.AddTimeSpent(taskId, employeeId, timeAmount);
            if (res)
            {
                Console.WriteLine("Добавлено потраченное время");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void DeleteSubtask()
        {
            Console.WriteLine("Введите ID подзадачи: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            var res = _responsible.DeleteSubtask(taskId);
            if (res)
            {
                Console.WriteLine("Подзадача удалена");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }

        void CheckManager()
        {
            int need = 0;
            while (need != 9)
            {
                Console.WriteLine("1 - Добавить проект\n" +
                    "2 - Добавить подзадачу\n" +
                    "3 - Изменить задачу\n" +
                    "4 - Добавить ответственного к задаче\n" +
                    "5 - Удалить проект\n" +
                    "6 - Удалить подзадачу\n" +
                    "7 - Получить затраченное сотрудником время\n" +
                    "8 - Получить ответственных за задачу\n" +
                    "9 - Exit\n");
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        AddProject();
                        break;
                    case 2:
                        AddSubtaskM();
                        break;
                    case 3:
                        UpdateTaskM();
                        break;
                    case 4:
                        AddResponsibleM();
                        break;
                    case 5:
                        DeleteProject();
                        break;
                    case 6:
                        DeleteSubtaskM();
                        break;
                    case 7:
                        GetTimeSpentByEmployee();
                        break;
                    case 8:
                        GetResponsibleEmployeesM();
                        break;
                    case 9:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void AddProject()
        {
            Console.WriteLine("Введите название:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите дату начала:");
            string termBegin = Console.ReadLine();
            Console.WriteLine("Введите дату окончания:");
            string termEnd = Console.ReadLine();
            Console.WriteLine("Введите оценочное время:");
            string estimatedTime = Console.ReadLine();

            var res = _manager.AddProject(title, termBegin, termEnd, estimatedTime);
            if (res)
            {
                Console.WriteLine("Проект создан");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void AddSubtaskM()
        {
            Console.WriteLine("Введите ID родительской задачи:");
            int parentTaskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите название:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите дату начала:");
            string termBegin = Console.ReadLine();
            Console.WriteLine("Введите дату окончания:");
            string termEnd = Console.ReadLine();
            Console.WriteLine("Введите оценочное время:");
            string estimatedTime = Console.ReadLine();

            var res = _manager.AddSubtask(parentTaskId, title, termBegin, termEnd, estimatedTime);
            if (res)
            {
                Console.WriteLine("Подзадача создана");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void UpdateTaskM()
        {
            Console.WriteLine("Введите ID изменяемой задачи:");
            int taskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ID родительской задачи:");
            int parentTaskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите название:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите дату начала:");
            string termBegin = Console.ReadLine();
            Console.WriteLine("Введите дату окончания:");
            string termEnd = Console.ReadLine();
            Console.WriteLine("Введите оценочное время:");
            string estimatedTime = Console.ReadLine();

            var res = _manager.UpdateTask(taskId, parentTaskId, title, termBegin, termEnd, estimatedTime);
            if (res)
            {
                Console.WriteLine("Подзадача изменена");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void AddResponsibleM()
        {
            Console.WriteLine("Введите ID задачи:");
            int taskId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ID ответственного:");
            int employeeId = Convert.ToInt32(Console.ReadLine());

            var res = _manager.AddResponsible(taskId, employeeId);
            if (res)
            {
                Console.WriteLine("Ответственный добавлен");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void DeleteProject()
        {
            Console.WriteLine("Введите ID проекта: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            var res = _manager.DeleteProject(taskId);
            if (res)
            {
                Console.WriteLine("Проект удален");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void DeleteSubtaskM()
        {
            Console.WriteLine("Введите ID подзадачи: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            var res = _manager.DeleteSubtask(taskId);
            if (res)
            {
                Console.WriteLine("Подзадача удалена");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void GetTimeSpentByEmployee()
        {
            Console.WriteLine("Введите ID сотрудника:");
            int id = Convert.ToInt32(Console.ReadLine());
            List<Responsibility> timeSpents = _manager.GetTimeSpentByEmployee(id);
            if (timeSpents != null)
            {
                foreach (Responsibility timeSpent in timeSpents)
                {
                    Console.WriteLine(Convert.ToString(timeSpent.TimeSpentID) + " " +
                        Convert.ToString(timeSpent.TaskID) + " " +
                        Convert.ToString(timeSpent.EmployeeID) + " " +
                        Convert.ToString(timeSpent.TimeAmount));
                }
            }
            else
            {
                Console.WriteLine("Затраченное время не найдено");
            }
        }
        void GetResponsibleEmployeesM()
        {
            Console.WriteLine("Введите ID задачи:");
            int id = Convert.ToInt32(Console.ReadLine());
            List<Employee> employees = _manager.GetResponsibleEmployees(id);
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    Console.WriteLine(Convert.ToString(employee.EmployeeId) + " " +
                            Convert.ToString(employee.Login) + " " +
                            Convert.ToString(employee.Permission) + " " +
                            Convert.ToString(employee.Name) + " " +
                            Convert.ToString(employee.Surname));
                }
            }
            else
            {
                Console.WriteLine("Ответственные сотрудники не найдены");
            }
        }

        void CheckHR()
        {
            int need = 0;
            while (need != 8)
            {
                Console.WriteLine("1 - Получить ответственных за задачу\n" +
                    "2 - Получить всех сотрудников\n" +
                    "3 - Добавить сотрудника\n" +
                    "4 - Изменить сотрудника\n" +
                    "5 - Удалить сотрудника\n" +
                    "6 - Посмотреть отделы сотрудников\n" +
                    "7 - Добавить отдел сотрудника\n" +
                    "8 - Exit\n");
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        GetResponsibleEmployees();
                        break;
                    case 2:
                        GetAllEmployees();
                        break;
                    case 3:
                        AddEmployee();
                        break;
                    case 4:
                        UpdateEmployee();
                        break;
                    case 5:
                        DeleteEmployee();
                        break;
                    case 6:
                        GetEmployeesDepartments();
                        break;
                    case 7:
                        AddEmployeeDepartment();
                        break;
                    case 8:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void GetResponsibleEmployees()
        {
            Console.WriteLine("Введите ID задачи:");
            int id = Convert.ToInt32(Console.ReadLine());
            List<Employee> employees = _HR.GetResponsibleEmployees(id);
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    Console.WriteLine(Convert.ToString(employee.EmployeeId) + " " +
                            Convert.ToString(employee.Login) + " " +
                            Convert.ToString(employee.Permission) + " " +
                            Convert.ToString(employee.Name) + " " +
                            Convert.ToString(employee.Surname));
                }
            }
            else
            {
                Console.WriteLine("Ответственные сотрудники не найдены");
            }
        }
        void GetAllEmployees()
        {
            List<Employee> employees = _HR.GetAllEmployees();
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    Console.WriteLine(Convert.ToString(employee.EmployeeId) + " " +
                            Convert.ToString(employee.Login) + " " +
                            Convert.ToString(employee.Permission) + " " +
                            Convert.ToString(employee.Name) + " " +
                            Convert.ToString(employee.Surname));
                }
            }
            else
            {
                Console.WriteLine("Сотрудники не найдены");
            }
        }
        void AddEmployee()
        {
            Console.WriteLine("Введите логин:");
            string Login = Console.ReadLine();
            Console.WriteLine("Введите хеш:");
            string Hash = Console.ReadLine();
            Console.WriteLine("Введите права (0 - сотрудник, 1 - ответственный, 2 - менеджер, 3- HR):");
            int Permission = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите имя:");
            string Name = Console.ReadLine();
            Console.WriteLine("Введите фамилию:");
            string Surname = Console.ReadLine();

            var res = _HR.AddEmployee(Login, Hash, Permission, Name, Surname);
            if (res)
            {
                Console.WriteLine("Сотрудник добавлен");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void UpdateEmployee()
        {
            Console.WriteLine("Введите ID изменяемого сотрудника:");
            int employeeId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите логин:");
            string Login = Console.ReadLine();
            Console.WriteLine("Введите хеш:");
            string Hash = Console.ReadLine();
            Console.WriteLine("Введите права (0 - сотрудник, 1 - ответственный, 2 - менеджер, 3- HR):");
            int Permission = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите имя:");
            string Name = Console.ReadLine();
            Console.WriteLine("Введите фамилию:");
            string Surname = Console.ReadLine();

            var res = _HR.UpdateEmployee(employeeId, Login, Hash, Permission, Name, Surname);
            if (res)
            {
                Console.WriteLine("Сотрудник изменен");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void DeleteEmployee()
        {
            Console.WriteLine("Введите ID сотрудника: ");
            int employeeId = Convert.ToInt32(Console.ReadLine());

            var res = _HR.DeleteEmployee(employeeId);
            if (res)
            {
                Console.WriteLine("Сотрудник удален");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void GetEmployeesDepartments()
        {
            List<EmployeeDepartment> employees = _HR.GetEmployeesDepartments();
            if (employees != null)
            {
                foreach (EmployeeDepartment employee in employees)
                {
                    Console.WriteLine(Convert.ToString(employee.EmployeeDepartmentID) + " " +
                            Convert.ToString(employee.EmployeeID) + " " +
                            Convert.ToString(employee.Department));
                }
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
        void AddEmployeeDepartment()
        {
            Console.WriteLine("Введите ID сотрудника:");
            int EmployeeID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите название отдела:");
            string Department = Console.ReadLine();

            var res = _HR.AddEmployeeDepartment(EmployeeID, Department);
            if (res)
            {
                Console.WriteLine("Отдел сотрудника добавлен");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
        }
    }
    public static class DiExtensions
    {
        public static void AddRepositoryExtensions(IServiceCollection services)
        {
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IObjectiveRepository, ObjectiveRepository>();
            services.AddSingleton<IResponsibilityRepository, ResponsibilityRepository>();
        }
        public static void AddControllerExtensions(IServiceCollection services)
        {
            services.AddScoped<EmployeeController>();
            services.AddScoped<ManagerController>();
            services.AddScoped<ResponsibleController>();
            services.AddScoped<HRController>();
        }
    }
}

