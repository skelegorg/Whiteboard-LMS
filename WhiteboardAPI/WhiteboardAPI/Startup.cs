using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WhiteboardAPI.Models.Assignments;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Models.Other;
using WhiteboardAPI.Models.Classrooms;

namespace WhiteboardAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
	        services.AddDbContext<AnnouncementContext>(options =>
		        options.UseInMemoryDatabase("WhiteboardAPI"));
	        services.AddDbContext<CourseContext>(options =>
		        options.UseInMemoryDatabase("WhiteboardAPI"));
            services.AddDbContext<PollContext>(options =>
                options.UseInMemoryDatabase("WhiteboardAPI"));
	        services.AddDbContext<AccountContext>(options =>
                options.UseInMemoryDatabase("WhiteboardAPI"));
            services.AddDbContext<CommentContext>(options =>
                options.UseInMemoryDatabase("WhiteboardAPI"));
            services.AddControllers(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
