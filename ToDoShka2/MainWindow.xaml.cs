using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoShka2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        static string serverName = "localhost";
        static string userName = "root";
        static string dbname = "etocto";
        static string port = "3306";
        static string password = "vika_17092007";
        public string ConnectionString = $"Server={serverName};Port={port};Database={dbname};User ID={userName};Password={password};SslMode=None;AllowPublicKeyRetrieval=True;";

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            string log = loginn.Text;
            string pw = passw.Password;





            string commandText2 = $"SELECT idUsers ,  Name, password, isadmin, firstlog FROM Users WHERE login='{log}' AND password = '{pw}';";

            MySqlConnection connection = new MySqlConnection(ConnectionString);

            connection.Open();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commandText2, connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                bool adm = (bool)dt.Rows[0]["isadmin"];
                switch (adm)
                {
                    case true:
                        this.Hide();
                        connection.Close();
                        break;

                    case false:

                        bool firstLog =(bool)dt.Rows[0]["firstlog"];
                        switch (adm)
                        {
                            case true:
                                this.Show();
                                connection.Close();
                                break;
                            case false:
                                connection.Close();
                                break;
                        }
                        connection.Close();
                        this.Hide();
                        break;
                }

            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
                connection.Close();    
            }

        }
    }
}