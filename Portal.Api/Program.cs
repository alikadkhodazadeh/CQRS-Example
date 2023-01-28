global using MediatR;
using Portal.Api.Behaviors;
using Portal.Api.CQRS.Queries;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
const string myCorsPolicy = "MyCorsPolicy";

// Add services to the container.


builder.Services.AddCors(options =>
{
    options.AddPolicy(myCorsPolicy,
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .WithExposedHeaders();
        });
});

builder.Services.AddMediatR(typeof(InputModelQuery).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));

builder.Services.AddMemoryCache();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.TagActionsBy(api => new[] { api.GroupName });
    options.DocInclusionPredicate((name, api) => true);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

app.UseCors(myCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
