using WebApplication2.IRepository;
using WebApplication2.Models;
using WebApplication2.Repository;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IDepartmentMasterRpository_IQ, DepartmentMasterRepository_IQ>();
builder.Services.AddScoped<IDesignationMasterRepository_IQ, DesignationMasterRepository_IQ>();
builder.Services.AddScoped<IEmployeeMasterUSP, EmployeeMasterRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
