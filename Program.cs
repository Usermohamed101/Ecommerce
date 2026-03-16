
using Ecommerce.infrastruction;
using Ecommerce.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Ecommerce.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ecommerce.Middleware;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Ecommerce.Services;

namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(opt =>
            opt.AddDefaultPolicy(o =>
            {
                o.AllowAnyHeader();
                o.AllowAnyOrigin();
                o.AllowAnyMethod();


            }
           
            )
            );
            builder.Services.AddIdentity<User, IdentityRole>(
                
                opt=> {
                    opt.SignIn.RequireConfirmedEmail = true;
                    opt.Password.RequiredLength = 8;
                  
                }
                    
                ).AddEntityFrameworkStores<ECommerceContext>().AddDefaultTokenProviders();
            builder.Services.AddDbContext<ECommerceContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default")).UseLazyLoadingProxies(false);


            }
            );
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.Configure<jwtOption>(
                builder.Configuration.GetSection("jwt")
                );


            var jwtOptios = builder.Configuration.GetSection("jwt").Get<jwtOption>();


            builder.Services.Configure<EmailOptions>(opt =>

            builder.Configuration.GetSection("EmailOptions")
            );


            builder.Services.AddAuthentication(
          options => {
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



              }


                ).AddJwtBearer(o =>
                {

                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ValidIssuer = jwtOptios.issuer,
                        ValidAudience = jwtOptios.audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptios.key)),

                        ClockSkew=TimeSpan.Zero,

                    };

                });
            builder.Services.AddAuthorization();
            builder.Services.AddServices();

            builder.Services.AddMemoryCache();
            builder.Services.AddRateLimiter(option =>
            {

                {
                    option.RejectionStatusCode=429;
                    option.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                        RateLimitPartition.GetSlidingWindowLimiter(

                            partitionKey: context.Connection.RemoteIpAddress.ToString(),
                            factory: _ => new SlidingWindowRateLimiterOptions
                            {
                                PermitLimit = 100,
                                QueueLimit = 5,
                                Window = TimeSpan.FromMinutes(1),
                                SegmentsPerWindow = 6,
                                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                            }




                            )
                );


                    option.AddSlidingWindowLimiter("loginWindow", opt =>
                    {
                        opt.PermitLimit = 5;
                        opt.QueueLimit = 0;
                        opt.Window = TimeSpan.FromSeconds(30);
                        opt.SegmentsPerWindow = 3;
                        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                    });

                    option.AddConcurrencyLimiter("concurrency", opt =>
                    {
                        opt.QueueLimit = 2;
                        opt.PermitLimit = 5;
                        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    });


                    option.AddSlidingWindowLimiter("payment-window", opt =>
                    {
                        opt.PermitLimit = 1;
                        opt.Window = TimeSpan.FromSeconds(10);
                        opt.SegmentsPerWindow = 2;
                        opt.QueueLimit = 0;
                    });


                }
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                DataBaseMigrationManagmentService.initImigration(service);
                await Seeder.SeedAsync(service);
                await Seeder.SeedCategoriesAsync(service);
                await Seeder.SeedProductsAsync(service);
                await Seeder.SeedDeliveryOptionsAsync(service);
            }




                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())  { }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRateLimiter();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseCors();
            app.MapControllers();

            app.Run();
        }
    }
}
