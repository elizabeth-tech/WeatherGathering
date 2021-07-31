using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherGathering.DAL;
using WeatherGathering.Interfaces.Base.Repositories;

namespace WeatherGathering.WPF.ViewModels
{
    public class MainWindowViewModel : TitledViewModel
    {
        private readonly IRepository<DataSource> dataSourceRepository;

        public MainWindowViewModel(IRepository<DataSource> dataSourceRepository)
        {
            Title = "Главное окно";
            this.dataSourceRepository = dataSourceRepository;
        }

        public ObservableCollection<DataSource> dataSources { get; } = new ObservableCollection<DataSource>();

        #region Команды

        #region Загрузить данные по источникам
        private LambdaCommand loadDataSourcesCommand;

        public ICommand LoadDataSourcesCommand => loadDataSourcesCommand ??= new(OnLoadDataSourcesCommandExecuted);

        // Логика выполнения команды
        private async void OnLoadDataSourcesCommandExecuted(object p)
        {
            dataSources.Clear();
            foreach(var source in await dataSourceRepository.GetAll())
                dataSources.Add(source);

        }
        #endregion Загрузить данные по источникам

        #endregion Команды

    }
}
