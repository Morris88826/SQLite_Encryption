using DatabaseEncryption.Context;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace DatabaseEncryption
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=exampleDb.db";
        private readonly ExampleDbContextFactory _exampleDbContextFactory;
        public App()
        {
            _exampleDbContextFactory = new ExampleDbContextFactory(CONNECTION_STRING);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (ExampleDbContext dbContext = _exampleDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            MainWindow = new MainWindow(_exampleDbContextFactory);
            MainWindow.Show();
            base.OnStartup(e);
        }

    }
}
