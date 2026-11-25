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
        public string ConnectionString = $"Server={serverName};Port={port};Database={dbname};User ID={userName};Password={password};AllowPublicKeyRetrieval=True;";

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            string log = loginn.Text;
            string pw = passw.Password;





            string commandText2 = $"SELECT idUsers ,  Name, password, isadmin, firstlog FROM Users WHERE name='{log}' AND password = '{pw}';";

            MySqlConnection connection = new MySqlConnection(ConnectionString);

            connection.Open();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commandText2, connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                SByte adm = (SByte)dt.Rows[0]["isadmin"];
                switch (adm)
                {
                    case 1:
                        Window1 window1 = new Window1();    
                        window1.Show();
                        this.Hide();
                        connection.Close();
                        break;

                    case 0:

                        SByte firstLog =(SByte)dt.Rows[0]["firstlog"];
                        switch (firstLog)
                        {
                            case 1:
                                Window2 window4 = new Window2();
                                window4.Show();
                                
                                Window3 window3 = new Window3();
                                window3.ShowDialog();
                                
                                
                                this.Close();
                                connection.Close();
                                break;
                            case 0:
                                Window2 window2 = new Window2();
                                window2.Show();
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