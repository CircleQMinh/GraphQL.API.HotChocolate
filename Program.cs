using GraphQL.API.HotChocolate.Data;
using GraphQL.API.HotChocolate.GraphQL;
using GraphQL.API.HotChocolate.GraphQL.Commands;
using GraphQL.API.HotChocolate.GraphQL.Platforms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
//});
builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<AppDbContext>()
    .AddQueryType<Query>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .AddProjections() // command -> flatform
    .AddSorting()
    .AddFiltering();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseWebSockets();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();


app.MapGraphQL("/graphql");
app.MapGraphQLVoyager("/ui/voyager");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
