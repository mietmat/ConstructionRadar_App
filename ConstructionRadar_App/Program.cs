using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;
using ConstructionRadar_App.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Employee>, SqlRepository<Employee>>();
services.AddSingleton<ITxtReader, TxtReader>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddDbContext<ConstructionRadarDbContext>(options => options
        .UseSqlServer("Data Source=DESKTOP-S8KROC7\\SQLEXPRESS;Initial Catalog=ConstructionRadarApp;Integrated Security=True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
app.Close();