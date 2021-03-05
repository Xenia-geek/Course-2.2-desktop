using Microsoft.Win32;
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
using static курсач_7_мая.secondary_pages.ADDallserials;

namespace курсач_7_мая.secondary_pages
{
    /// <summary>
    /// Логика взаимодействия для ADDsessoneallserials.xaml
    /// </summary>
    public partial class ADDsessoneallserials : Window
    {
        public ADDsessoneallserials()
        {
            InitializeComponent();
            NameSerialls();
        }
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        private void Continue3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Serialss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(SeasoneNumber.Text, @"^[0-9]+$").Success || SeasoneNumber.Text.Length > 2)
            {
                if (bordernumbersesone != null)
                {
                    bordernumbersesone.Background = Brushes.Red;
                }
                if (SeasoneNOTcorrect != null)
                {
                    SeasoneNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (bordernumbersesone != null)
                {
                    bordernumbersesone.Background = Brushes.HotPink;
                    bordernumbersesone.Opacity = 0.7;
                }
                if (SeasoneNOTcorrect != null)
                {
                    SeasoneNOTcorrect.Opacity = 0;
                }

            }
        }

        private void AddPicture_Click(object sender, RoutedEventArgs e)
        {
          
        }
        private string NameSerialls()
        {

            string us;
            SerialName.Content = SerialsNames.GetValue<string>("currentCustomerName");
            us = SerialsNames.GetValue<string>("currentCustomerName");
            return us;

        }
        private int IDSerialls()
        {

            int us;
            us = IDofSERIALS.GetValue<int>("currentCustomerName");
            return us;

        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SeasoneNumber.Text = "";
        }

        private void AboutSeasone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AboutSeasone.Text = "";
        }

        private void AboutSeasone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(AboutSeasone.Text==null )
            {
              
                if (AboutNOTcorrect != null)
                {
                    AboutNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (AboutNOTcorrect != null)
                {
                    AboutNOTcorrect.Opacity = 0;
                }
            }
        }

        private void Addseasone_Click(object sender, RoutedEventArgs e)
        {
            if (SeasoneNumber.Text == null || AboutSeasone.Text == null || AboutSeasone.Text == "Введите немного о сезоне")
            {
                MessageBox.Show("Номер или О сезоне не введены");
            }

            else if (AboutNOTcorrect.Opacity == 0 && SeasoneNOTcorrect.Opacity == 0)
            {

                SqlConnection sqlCon = new SqlConnection(connectionString);
                
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "SELECT COUNT(1) FROM Seasone WHERE Number_seasone=@Number AND ID_Serials=@ID;";
                        
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;
                        

                        int value3= Convert.ToInt32(SeasoneNumber.Text);

                        sqlCmd.Parameters.AddWithValue("@Number", value3);      
                        sqlCmd.Parameters.AddWithValue("@ID", IDSerialls());
                        
                        
                       
                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        if (count == 1)
                        {

                            if (bordernumbersesone != null)
                            {
                                bordernumbersesone.Background = Brushes.Red;
                            }
                            if (SeasoneNOTcorrect != null)
                            {
                                SeasoneNOTcorrect.Opacity = 1;
                            }
                            MessageBox.Show("Сезон уже есть в базе");
                        }
                        else
                        {
                            try
                            {
                                if (sqlCon.State == ConnectionState.Closed)
                                    sqlCon.Open();
                                string querySS = "SELECT COUNT(1) FROM Seasone WHERE ID_Serials=@ID;";

                                SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);
                                sqlCmd5.CommandType = CommandType.Text;
                            sqlCmd5.Parameters.AddWithValue("@ID", IDSerialls());

                            int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                                int p = value3 - 1;

                                if (countS == p)
                                {


                                        SqlConnection sqlCon2 = new SqlConnection(connectionString);
                                    
                                        sqlCon2.Open();
                                        if (PhotoSeasone.Source == null)
                                        {
                                            SqlCommand sqlCmd2 = new SqlCommand("SEASONEADDwithoutPhoto", sqlCon2);
                                            sqlCmd2.CommandType = CommandType.StoredProcedure;
                                            sqlCmd2.Parameters.AddWithValue("@Number", value3);
                                            sqlCmd2.Parameters.AddWithValue("@About_seasone", AboutSeasone.Text.Trim());
                                            sqlCmd2.Parameters.AddWithValue("@ID_Serials", IDSerialls());

                                    //sqlCmd2.ExecuteNonQuery();
                                        SeasoneNum.SetValue("currentCustomerName", SeasoneNumber.Text);
                                        int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                            ID_SEASONE.SetValue("currentCustomerName", IDSEASONE);
                                        }
                                        else
                                        {
                                            //SqlCommand sqlCmd1 = new SqlCommand("SERIALSADD", sqlCon);
                                            //sqlCmd1.CommandType = CommandType.StoredProcedure;
                                            //sqlCmd1.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                            //sqlCmd1.Parameters.AddWithValue("@Year_of_Start", StartDate.SelectedDate);
                                            //sqlCmd1.Parameters.AddWithValue("@Year_of_End", EndDate.SelectedDate);
                                            //sqlCmd1.Parameters.AddWithValue("@About_serials", AboutSerials.Text.Trim());

                                            //sqlCmd1.ExecuteNonQuery();
                                            MessageBox.Show("Пока не сделала функцию");
                                        }
                                        
                                    
                                    ADDseriesallserials series = new ADDseriesallserials();
                                    Close();
                                    series.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Вы пропустили сезон");
                                    if (bordernumbersesone != null)
                                    {
                                        bordernumbersesone.Background = Brushes.Red;
                                    }
                                    if (SeasoneNOTcorrect != null)
                                    {
                                        SeasoneNOTcorrect.Opacity = 1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            
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
            else if (SeasoneNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введен сезон");
            }
            else if (AboutNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введено о сезоне");
            }
            
        }
        public static class SeasoneNum
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
            public static class ID_SEASONE
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

