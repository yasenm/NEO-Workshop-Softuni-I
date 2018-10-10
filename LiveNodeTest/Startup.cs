using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neo;
using Neo.Ledger;
using Neo.Persistence.LevelDB;

namespace LiveNodeTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            InitializeNeoSystem();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static NeoSystem NeoSystem { get; private set; }
        internal LiveNodeTest.Settings.Settings Settings = new LiveNodeTest.Settings.Settings();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void InitializeNeoSystem()
        {
            LevelDBStore store = new LevelDBStore(Settings.Paths.Chain);
            NeoSystem = new NeoSystem(store);
            
            NeoSystem.ActorSystem.ActorOf(NoticationBroadcaster.Props(NeoSystem.Blockchain));
            NeoSystem.StartNode(Settings.P2P.Port, Settings.P2P.WsPort);
        }

    }
}
