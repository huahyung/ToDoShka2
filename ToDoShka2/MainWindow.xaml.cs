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
        int colvo = 0;
        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            string log = loginn.Text;
            string pw = passw.Password;

            colvo++;



            string commandText2 = $"SELECT idUsers ,  Name, password, isadmin, firstlog FROM Users WHERE name='{log}' AND password = '{pw}';";

            MySqlConnection connection = new MySqlConnection(ConnectionString);

            connection.Open();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commandText2, connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            SByte firstLog;
            firstLog = (SByte)dt.Rows[0]["firstlog"];
            if (p1.Background is ImageBrush b1 && b1.ImageSource.ToString().EndsWith("/1.png") &&
                p2.Background is ImageBrush b2 && b2.ImageSource.ToString().EndsWith("/2.png") &&
                p3.Background is ImageBrush b3 && b3.ImageSource.ToString().EndsWith("/3.png") &&
                p4.Background is ImageBrush b4 && b4.ImageSource.ToString().EndsWith("/4.png"))
            {
                 
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

                           
                            switch (firstLog)
                            {
                                case 1:
                                    MessageBox.Show("Вы заблокированы обратитесь к админу");
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
            else
            {
                
                

                if (colvo < 3)
                {
                    MessageBox.Show("Вы робот.");
                }
                else if (colvo == 3)
                {

                    MessageBox.Show("Вы заблокированы обратитесь к админу");
                    string commandText3 = $"Update users SET firstlog = '1'  WHERE name='{log}' AND password = '{pw}';";


                    MySqlCommand command = new MySqlCommand(commandText3, connection);

                    int rowsAffected = command.ExecuteNonQuery();


                }
                else if (firstLog == 1) {
                    MessageBox.Show("Вы заблокированы обратитесь к админу");
                }
            }
        }

       
        private void p4_Click_1(object sender, RoutedEventArgs e)
        {
            (p3.Background, p4.Background) = (p4.Background, p3.Background);

        }

        private void p3_Click(object sender, RoutedEventArgs e)
        {
            (p3.Background, p1.Background) = (p1.Background, p3.Background);
        }

        private void p1_Click(object sender, RoutedEventArgs e)
        {
            (p2.Background, p1.Background) = (p1.Background, p2.Background);
        }

        private void p2_Click(object sender, RoutedEventArgs e)
        {
            (p2.Background, p4.Background) = (p4.Background, p2.Background);
        }

        
    }
}