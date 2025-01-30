var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // mvc şablonunu tanıtır

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.MapDefaultControllerRoute();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
