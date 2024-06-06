using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using project;
using Repositories;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<ICategoriesRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IRatingRepository, RatingRepository>();
builder.Services.AddTransient<IRatingService, RatingService>();

builder.Services.AddDbContext<WebApiProjectContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString"]));

//Cookie auth
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("key").Value);

builder.Services
    .AddAuthentication(option =>
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme

   )
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents()
        {

            //get cookie value
            OnMessageReceived = context =>
            {
                var a = "";
                context.Request.Cookies.TryGetValue("X-Access-Token", out a);
                context.Token = a;
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });

builder.Services.AddControllers();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseNLog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.UseRatingMiddleware();

app.UseErrorHandlingMiddleware();

app.Run();
