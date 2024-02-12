using favors_and_fates_repositories;
using favors_and_fates_services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFavorsAndFatesRepository, FavorsAndFatesRepository>(x =>
{
  var favorsAndFatesRepository = new FavorsAndFatesRepository(
    builder.Configuration.GetSection("Settings").Get<Settings>()?.FavorsAndFatesAssetsPath ?? string.Empty);
  favorsAndFatesRepository.Initialize();
  return favorsAndFatesRepository;
});
builder.Services.AddScoped<IFavorsAndFatesService, FavorsAndFatesService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseCors(x => x
  .AllowAnyMethod()
  .AllowAnyHeader()
  .SetIsOriginAllowed(origin => true)
  .WithOrigins(builder.Configuration.GetSection("Settings").Get<Settings>()?.FavorsAndFatesAppUrl ?? string.Empty)
  .AllowCredentials());

app.Run();
