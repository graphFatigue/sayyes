using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sayyes.DAL;
using sayyes.DAL.Interfaces;
using sayyes.Domain.Entity;
using sayyes.DAL.Repositories;
using sayyes.Service.Interfaces;
using sayyes.Service.Implementations;

namespace sayyes
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
            services.AddControllersWithViews();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IBaseRepository<Author>, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBaseRepository<Book>, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBaseRepository<Writing>, WritingRepository>();
            services.AddScoped<IWritingService, WritingService>();            
            services.AddScoped<IBaseRepository<Publisher>, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IBaseRepository<BookPublisher>, BookPublisherRepository>();
            services.AddScoped<IBaseRepository<BookAuthor>, BookAuthorRepository>();
            services.AddScoped<IBaseRepository<WritingAuthor>, WritingAuthorRepository>();
            services.AddScoped<IBaseRepository<BookWriting>, BookWritingRepository>();
            //services.AddScoped<IBookPublisherService, BookPublisherService>();
            
            //services.AddScoped<IWritingService, WritingService>();


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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
