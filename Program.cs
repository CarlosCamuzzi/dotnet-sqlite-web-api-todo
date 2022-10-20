var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllerRoute(
  name:"default",
  pattern: "{controller=Home}/{action=Index}/{id?}"
);

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
app.Run();