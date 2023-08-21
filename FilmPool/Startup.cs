using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using FilmPool.Data;
using Microsoft.EntityFrameworkCore;
using FilmPool.Services;
using Microsoft.OpenApi.Models;
using FilmPool.Repositories;
using FilmPool.JWT;
using Microsoft.AspNetCore.Identity;
using FilmPool.DbModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using FilmPool.RequestModels;

namespace Project.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(options => options.EnableEndpointRouting = false);

      services.AddCors();


      services.AddDbContext<FilmPoolDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:sqlConnection"]));



      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
      });

      services.AddAuthentication(opt => {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
  
      })
    .AddJwtBearer(options =>
    {
      options.SaveToken = true;
      options.RequireHttpsMetadata = false;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Configuration["JwtSetting:validIssuer"],
        ValidAudience = Configuration["JwtSetting:validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey@12345"))
      };
    });

      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IFilmsService, FilmsService>();
      services.AddScoped<IFilmsRepository, FilmsRepository>();
      services.AddScoped<IGenresRepository, GenresRepository>();
      services.AddScoped<IRatingService, RatingService>();
      services.AddScoped<IRatingRepository, RatingRepository>();
      services.AddScoped<IGenresService, GenresService>();
      services.AddScoped<ICommentsService, CommentsService>();
      services.AddScoped<ICommentsRepository, CommentsRepository>();
      services.AddScoped<ICollectionsService, CollectionsService>();
      services.AddScoped<ICollectionsRepository, CollectionsRepository>();
      services.AddScoped<AuthService>();
      var mapperConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new UserProfile());
      });

      IMapper mapper = mapperConfig.CreateMapper();
      services.AddSingleton(mapper);

      var emailConfig = Configuration
     .GetSection("EmailConfiguration")
     .Get<EmailConfiguration>(); 
      services.AddSingleton(emailConfig);
      services.AddScoped<IEmailSender, EmailSender>();

      services.AddControllers();

    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();

      app.UseAuthentication();
   
      app.UseAuthorization();
      app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

      app.UseMvc();
      app.UseEndpoints(endpoints =>
      {

        endpoints.MapControllers();
      });
      app.UseDefaultFiles(); 
      app.UseStaticFiles();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });


      app.UseHttpsRedirection();






    }
  }
}
