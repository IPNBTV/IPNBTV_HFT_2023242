using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using IPNBTV_HFT_2023242.WpfClient.Services;

namespace IPNBTV_HFT_2023242.WpfClient
{
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddTransient<IEditorService, EditorService>()
                    .BuildServiceProvider()
            );
        }
    }
}
