using ConstructionRadar_App._2_ApplicationServices.Components.DataProviders;
using ConstructionRadar_App._2_ApplicationServices.Components.Services;
using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;
using ConstructionRadar_App.Services;
using ConstructionRadar_App.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MotoApp.Components.CsvReader;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();
services.AddSingleton<IShowDataProvider, ShowDataProvider>();
services.AddSingleton<IRepository<Employee>, SqlRepository<Employee>>();
services.AddSingleton<IRepository<Contract>, SqlRepository<Contract>>();
services.AddSingleton<ITxtReader, TxtReader>();
services.AddSingleton<IEmployeeProvider, EmployeeProvider>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddDbContext<ConstructionRadarDbContext>(options => options
        .UseSqlServer("Data Source=DESKTOP-S8KROC7\\SQLEXPRESS;Initial Catalog=ConstructionRadarApp;Integrated Security=True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
app.Close();