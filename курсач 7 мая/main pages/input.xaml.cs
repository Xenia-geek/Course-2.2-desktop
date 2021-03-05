using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using курсач_7_мая.main_pages;

namespace курсач_7_мая
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class input : Window
    {
        public input()
        {
            InitializeComponent();
        }
        
        private void Button_input_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT COUNT(1) FROM USERS WHERE Login=@Login AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Login", Login.Text);
                sqlCmd.Parameters.AddWithValue("@Password", Password.Password);               
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if(count==1)
                {
                    ApplicationState.SetValue("currentCustomerName", Login.Text);
                    string us=ApplicationState.GetValue<string>("currentCustomerName");
                    string Admin = "Ksyusha8";
                    if (Admin == us)
                    {
                        AllUsers input = new AllUsers();
                        Close();
                        input.Show();
                    }
                    else
                    {
                       myserials myserials = new myserials();
                        Close();
                        myserials.Show();
                    }                    
                }
                else
                {
                    MessageBox.Show("Логин или пароль введены не верно");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }


           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            registration registration = new registration();
            Close();
            registration.Show();
        }

        private void Login_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             Login.Text = "";
        }

        private void Password_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Password.Password = "";
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if( !Regex.Match(Login.Text, @"^[а-яА-ЯёЁa-zA-Z]+[а-яА-ЯёЁa-zA-Z0-9]+$").Success || Login.Text.Length>10)
            {
                if (borderlogin != null)
                {
                    borderlogin.Background = Brushes.Red;
                }
                if (LoginNOTcorrect != null)
                {
                    LoginNOTcorrect.Opacity = 1;
                }
            }
            else 
            {
                if (borderlogin != null)
                {
                    borderlogin.Background = Brushes.HotPink;
                    borderlogin.Opacity = 0.7;
                }
                if (LoginNOTcorrect != null)
                {
                    LoginNOTcorrect.Opacity = 0;
                }

            }
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!Regex.Match(Password.Password, @"^[0-9]+$").Success || Password.Password.Length>8)
            {
                if (borderpassword != null)
                {
                    borderpassword.Background = Brushes.Red;
                }
                if (PasswordNOTcorrect != null)
                {
                    PasswordNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (borderpassword != null)
                {
                    borderpassword.Background = Brushes.HotPink;
                    borderpassword.Opacity = 0.7;
                }
                if (PasswordNOTcorrect != null)
                {
                    PasswordNOTcorrect.Opacity = 0;
                }

            }
        }

        public static class ApplicationState
        {
            private static Dictionary<string, object> _values =
                       new Dictionary<string, object>();
            public static void SetValue(string key, object value)
            {
                if (_values.ContainsKey(key))
                {
                    _values.Remove(key);
                }
                _values.Add(key, value);
            }
            public static T GetValue<T>(string key)
            {
                if (_values.ContainsKey(key))
                {
                    return (T)_values[key];
                }
                else
                {
                    return default(T);
                }
            }
        }

        private void LoginNOTcorrect_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
