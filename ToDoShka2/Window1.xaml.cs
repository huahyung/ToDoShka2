using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoShka2
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        static string serverName = "localhost";
        static string userName = "root";
        static string dbname = "etocto";
        static string port = "3306";
        static string password = "vika_17092007";
        public string ConnectionString = $"Server={serverName};Port={port};Database={dbname};User ID={userName};Password={password};AllowPublicKeyRetrieval=True;";
        private void changebutton_Click(object sender, RoutedEventArgs e)
        {
            string namex = name.Text;
            string passwordx = password2.Text;
            int isadmin = Convert.ToInt16(isadmin2.Text);
            int isblockedx = Convert.ToInt16(isblocked.Text);
            int ida = Convert.ToInt16(id.Text);
            string commandtext = $"update  USERS set  Name ='{namex}', password = '{passwordx}', isadmin = '{isadmin}', firstlog =  '{isblockedx}'  where idUsers ='{ida}'";
            
            try
            {

                MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(commandtext, connection);
                int number = mySqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Изменена {number} запись ");

                GridUpdate();
                clear2();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void savebutton_Click(object sender, RoutedEventArgs e)
        {
            string namex = name.Text;
            string passwordx = password2.Text;
            int isadmin = Convert.ToInt16(isadmin2.Text);
            int isblockedx = Convert.ToInt16(isblocked.Text);
            int ida = Convert.ToInt16(id.Text);
            string commandtext = $"INSERT INTO USERS VALUES ('{ida}','{namex}', '{passwordx}', '{isadmin}', '{isblockedx}')";
            string select = $"Select name from Users where name='{namex}'";
            
            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(commandtext, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(select, connection);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    int number = mySqlCommand.ExecuteNonQuery();
                    MessageBox.Show($"Добавлена {number} запись ");
                    GridUpdate();
                    clear2();
                }
                else
                {
                    MessageBox.Show($"Такая запись уже есть");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {

                    string command = "Select * from users;";
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(command, connection))
                    {
                        DataSet data = new DataSet();
                        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                        dataAdapter.Fill(data, "users");
                        gridofdata.ItemsSource = data.Tables["users"].DefaultView;

                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GridUpdate()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {

                string command = "Select * from users;";
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(command, connection))
                {
                    DataSet data = new DataSet();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    dataAdapter.Fill(data, "users");
                    gridofdata.ItemsSource = data.Tables["users"].DefaultView;

                }
                connection.Close();
            }
        }

        public void clear()
        {
            
            name.Text = "";
            password2.Text= "";
            isadmin2.Text = "";
            isblocked.Text = "";
            id.Text = "";
        }
        public void clear2()
        {

            name.Text = "Логин";
            password2.Text = "Пароль";
            isadmin2.Text = "Есть права админа (0-1)";
            isblocked.Text = "В блоке (0-1)";
            id.Text = "ID";
        }

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void gridofdata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = gridofdata.SelectedItem as DataRowView;
            
               
                    if (gridofdata.SelectedIndex != -1)
                    {
                        id.Text = dataRowView[0].ToString();
                        name.Text = dataRowView[1].ToString();
                        password2.Text = dataRowView[2].ToString();
                        isadmin2.Text = dataRowView[3].ToString();
                        isblocked.Text = dataRowView[4].ToString();
                       
                    }
            }

        private void id_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            clear();
        }
    }
}
