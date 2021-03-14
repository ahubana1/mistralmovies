using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies_Mistral.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Movies_Mistral.DataAccess;
using Movies_Mistral.Services;
using Movies_Mistral.Services.Implementations;

namespace Movies_Mistral
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<MoviesContext>
                (options => {
                    options.UseSqlServer(
                    "Server=(localdb)\\mssqllocaldb;Database=Movies;Trusted_Connection=True;");
                });

            //SeedData();

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //DI
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IShowsRepository, ShowsRepository>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IRatingService, RatingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller}/{action=Index}/{id?}"
                    pattern: "api/{controller}/{action}"
                    //pattern: ""
                    );
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        public void SeedData() 
        {
            using (var context = new MoviesContext())
            {
                context.Database.EnsureCreated();

                IEnumerable<Actor> actors = Seeder.SeederClass.SeedActors();
                IEnumerable<Movie> movies = Seeder.SeederClass.SeedMovies(context);

                foreach (var actor in actors)
                {
                    var testActor = context.Actors.FirstOrDefault(b => b.Id == actor.Id);
                    if (testActor == null)
                    {
                        context.Actors.Add(actor);
                    }
                }

                foreach (var movie in movies)
                {
                    context.Movies.Add(movie);
                }

                context.SaveChanges();
            }
        }
    }
}
