using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace курсач_7_мая
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        

        public registration()
        {
            InitializeComponent();

        }

        private void Button_input_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" || Password.Password == "")
            {
                MessageBox.Show("Логин или пароль не введены");
            }
            else if (Password.Password != PasswordAgain.Password)
            {
                MessageBox.Show("Пароли не совпадают");
            }
            else if (PasswordNOTcorrect.Opacity == 0 && LoginNOTcorrect.Opacity == 0 && PasswordAgainNOTcorrect.Opacity == 0)
            {
                SqlConnection sqlCon = new SqlConnection(@"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;");
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "SELECT COUNT(1) FROM USERS WHERE Login=@Login";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Login", Login.Text);
                    
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("Пользователь с таким логином уже есть");

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
                        using (SqlConnection sqlCon1 = new SqlConnection(connectionString))
                        {
                            sqlCon1.Open();

                            SqlCommand sqlCmd1 = new SqlCommand("UserAdd", sqlCon1);
                            sqlCmd1.CommandType = CommandType.StoredProcedure;
                            sqlCmd1.Parameters.AddWithValue("@Login", Login.Text.Trim());
                            sqlCmd1.Parameters.AddWithValue("@Password", Password.Password.Trim());
                            sqlCmd1.ExecuteNonQuery();
                            registrationuser.SetValue("currentCustomerName", Login.Text);
                            MessageBox.Show("Поздравляем, вы в нашей базе");
                           
                        }
                        myserials serials = new myserials();
                        Close();
                        serials.Show();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
                

            }
            else if(PasswordNOTcorrect.Opacity==1)
            {
                MessageBox.Show("пароль не корректный");
            }
            else if (LoginNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("логин не корректный");
            }
            
            
        }
        void Clear()
        {
            Login.Text = Password.Password = "";
        }

        
        private void Login_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login.Text = "";
        }

        private void Password_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Password.Password = "";
        }

        private void PasswordAgain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordAgain.Password = "";
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!Regex.Match(Password.Password, @"^[0-9]+$").Success || Password.Password.Length > 8)
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

        private void PasswordAgain_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!Regex.Match(PasswordAgain.Password, @"^[0-9]+$").Success || PasswordAgain.Password.Length > 8 || Password.Password!=PasswordAgain.Password)
            {
                
                if (borderpasswordagain != null)
                {
                    borderpasswordagain.Background = Brushes.Red;
                }
                if (PasswordAgainNOTcorrect != null)
                {
                    PasswordAgainNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                
                if (borderpasswordagain != null)
                {
                    borderpasswordagain.Background = Brushes.HotPink;
                    borderpasswordagain.Opacity = 0.7;
                }
                if (PasswordAgainNOTcorrect != null)
                {
                    PasswordAgainNOTcorrect.Opacity = 0;
                }

            }
        }

        private void Login_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(Login.Text, @"^[а-яА-ЯёЁa-zA-Z]+[а-яА-ЯёЁa-zA-Z0-9]+$").Success || Login.Text.Length >= 10)
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
        public static class registrationuser
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
    }

   
}
