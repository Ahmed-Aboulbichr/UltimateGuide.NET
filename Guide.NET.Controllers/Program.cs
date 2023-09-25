
var builder = WebApplication.CreateBuilder(args);
/*builder.Services.AddTransient<HomeController>();*/
builder.Services.AddControllers(); // adds all the controller classes as service
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
/*app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});*/
app.MapControllers();

app.Run();
