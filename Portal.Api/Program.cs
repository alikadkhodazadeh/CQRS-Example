using Portal.Api.Base;
using SimpleInjector;

var builder = WebApplication.CreateBuilder(args);
Container container = new();

const string myCorsPolicy = "MyCorsPolicy";

// Add services to the container.

builder.Services.AddScoped<IApiProcessor, ApiProcessor>();

builder.Services.AddSimpleInjector(container, options =>
{
    options.AddAspNetCore();
});

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.TagActionsBy(api => new[] { api.GroupName });
    options.DocInclusionPredicate((name, api) => true);
});

initializeContainer();

void initializeContainer()
{
    container.Register(typeof(IApiHandler<,>), typeof(IApiHandler<,>).Assembly);
}

var app = builder.Build();

app.Services.UseSimpleInjector(container);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

container.Verify();

app.Run();
