using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FileAnalizer.Messager.Services;
using FileAnalizer.Services;
using FileAnalizer.ViewModels;
using SimpleInjector;

namespace FileAnalizer;
public partial class App : Application
{
    public static Container ServiceContainer { get; set; } = new Container();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ConfigureContainer();

        StartWindow<FileViewModel>();
    }

    private void ConfigureContainer()
    {
        ServiceContainer.RegisterSingleton<IMessenger, Messenger>();

        ServiceContainer.RegisterSingleton<MainViewModel>();
        ServiceContainer.RegisterSingleton<FileViewModel>();

        ServiceContainer.Verify();
    }

    private void StartWindow<T>() where T : ViewModelBase
    {
        var startView = new MainWindow();

        var startViewModel = ServiceContainer.GetInstance<MainViewModel>();
        startViewModel.ActiveViewModel = ServiceContainer.GetInstance<T>();
        startView.DataContext = startViewModel;

        startView.ShowDialog();
    }
}
