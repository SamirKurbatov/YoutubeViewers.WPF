using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YoutubeViewers.WPF.HostBuilders;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;
using YouTubeViewers.Domain.Commands;
using YouTubeViewers.Domain.Queries;
using YouTubeViewers.EntityFramework;
using YouTubeViewers.EntityFramework.Commands;
using YouTubeViewers.EntityFramework.Queries;

namespace YoutubeViewers.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .AddDbContext()
                .ConfigureServices((context, services) =>
                {
                    string connectionString = "DataSource = YouTubeViewerss.db";

                    services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                    services.AddSingleton<YouTubeViewersDbContextFactory>();

                    services.AddSingleton<IGetAllYouTubeViewersQuery, GetAllYouTubeViewersQuery>();
                    services.AddSingleton<ICreateYouTubeViewerCommand, CreateYouTubeViewerCommand>();
                    services.AddSingleton<IDeleteYouTubeViewerCommand, DeleteYouTubeViewerCommand>();
                    services.AddSingleton<IUpdateYouTubeViewerCommand, UpdateYouTubeViewerCommand>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<YouTubeViewersStore>();
                    services.AddSingleton<SelectedYouTubeViewerStore>();
                    services.AddSingleton<MainViewModel>();

                    services.AddTransient(CreateYouTubeViewersViewModel);
                    services.AddSingleton((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });

                })
                .Build();
        }

        private YouTubeViewersViewModel CreateYouTubeViewersViewModel(IServiceProvider services)
        {
            return YouTubeViewersViewModel.LoadViewModel(
                services.GetRequiredService<YouTubeViewersStore>(),
                services.GetRequiredService<SelectedYouTubeViewerStore>(),
                services.GetRequiredService<ModalNavigationStore>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            host.Start();

            using (YouTubeViewersDbContext context = host.Services.GetRequiredService<YouTubeViewersDbContextFactory>().Create())
            {
                context.Database.Migrate();
            }
            MainWindow = host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            host.StopAsync();
            host.Dispose();

            base.OnExit(e);
        }
    }
}
