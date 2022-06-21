using main.Consumer;
using main.Helpers;
using main.Producer;
using main.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// Add services to the container.
services.AddControllers();
services.AddDbContext<MainDBContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IProductServices, ProductServices>();
services.AddScoped<IMessageProducer, RabbitMQProducer>();

services.AddHostedService<ProductConsumer>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
