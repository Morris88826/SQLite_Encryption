using DatabaseEncryption.API;
using DatabaseEncryption.Context;
using DatabaseEncryption.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseEncryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ExampleDbContextFactory _exampleContextFactory;
        public ObservableCollection<Entity.User> users { get; set; }

        public MainWindow(ExampleDbContextFactory exampleContextFactory)
        {
            InitializeComponent();
            _exampleContextFactory = exampleContextFactory;
            this.users = new ObservableCollection<Entity.User>();

            loadAllUsers();
        }

        private async void loadAllUsers()
        {
            users.Clear();
            var _users = await API.User.GetAllUsers(_exampleContextFactory);
            foreach (EncryptedEntity.EncryptedUser _user in _users)
            {
                users.Add(_user.Decrypted());
            }
            UserDataGrid.ItemsSource = users;
        }

        private async void addDataBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = new Entity.User("Morris", "Tseng", "morris88826@gmail.com", "9799337055", 178.4, 8, DateTime.Now);

            await API.User.CreateUser(_exampleContextFactory, user.Encrypted());

            loadAllUsers();

        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the current button
            Button? button = sender as Button;

            if (button != null)
            {
                // Get the current row item
                Entity.User item = (Entity.User)button.DataContext;

                if (item.ID.HasValue)
                {
                    await API.User.DeleteUserWithId(_exampleContextFactory, item.ID.Value);
                    loadAllUsers();
                }
            }

        }
    }
}
