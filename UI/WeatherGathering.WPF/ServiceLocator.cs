using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherGathering.WPF.ViewModels;

namespace WeatherGathering.WPF
{
    class ServiceLocator
    {
        public MainWindowViewModel MainModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
