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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using курсач_7_мая.secondary_pages;
using static курсач_7_мая.input;
using static курсач_7_мая.registration;
using курсач_7_мая.main_pages;
using курсач_7_мая.fourth_pages;
using курсач_7_мая.Tests;
using курсач_7_мая.offers;

namespace курсач_7_мая
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class myserials : Window
    {

        public myserials()
        {
            InitializeComponent();
            
            Avtorizacia();
            LoadAllSerials();
            
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";


        private void LoadAllSerials()
        {

            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select su.ID[ID], s.Name[Сериал], sea.Number_seasone[Текущий сезон], ser.Number_Series[Текущая серия] from USERS u join Serials_for_Users su on u.Login=su.LoginUser join SERIALS s on su.ID_Serials=s.ID_Serials join Seasone sea on su.Seasone_of_User=sea.ID_Seasone join Series ser on su.Series_of_User=ser.ID_Series where u.Login=@ID;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.CommandType = CommandType.Text;
                createCommand.Parameters.AddWithValue("@ID", Avtorizacia());
                createCommand.ExecuteNonQuery();
                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("SERIALS");
                dataadp.Fill(db);
                dgToDoList.ItemsSource = db.DefaultView;
                dataadp.Update(db);
                
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



            private void MySerials_Click(object sender, RoutedEventArgs e)
        {


            myserials myserials = new myserials();
            Close();
            myserials.Show();

        }



        private void AllSerials_Click(object sender, RoutedEventArgs e)
        {
            allserials allserials = new allserials();
            Close();
            allserials.Show();
        }

        private void Button_Click_input(object sender, RoutedEventArgs e)
        {
            input input = new input();
            Close();
            input.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ADDmyserials add = new ADDmyserials();
            add.Show();
        }

        private string Avtorizacia()
        {

            string us;
            username.Header = ApplicationState.GetValue<string>("currentCustomerName");
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }


        private string REGISTRATURE()
        {

            string us;
            username.Header = registrationuser.GetValue<string>("currentCustomerName");
            us = registrationuser.GetValue<string>("currentCustomerName");
            return us;
        }


        private void Actors_Click(object sender, RoutedEventArgs e)
        {
            ACTORS actor = new ACTORS();
            Close();
            actor.Show();
        }

        private void AllDirectors_Click(object sender, RoutedEventArgs e)
        {
            DIRECTORS dir = new DIRECTORS();
            Close();
            dir.Show();
        }
        private void Search_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Search.Text = "";
        }

        private void SerAllSerials_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (Search.Text != "" || Search.Text!= "Поиск по названию...")
            {
                dgToDoList.ItemsSource = null;
                try
                {
                    sqlCon.Open();
                    string Query = "select su.ID[ID], s.Name[Сериал], sea.Number_seasone[Текущий сезон], ser.Number_Series[Текущая серия] from USERS u join Serials_for_Users su on u.Login=su.LoginUser join SERIALS s on su.ID_Serials=s.ID_Serials join Seasone sea on su.Seasone_of_User=sea.ID_Seasone join Series ser on su.Series_of_User=ser.ID_Series where u.Login=@ID and s.Name like @Nam;";

                    SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                    createCommand.CommandType = CommandType.Text;
                    createCommand.Parameters.AddWithValue("@ID", Avtorizacia());
                    string nameser1 = "%" + Search.Text + "%";
                    createCommand.Parameters.AddWithValue("@Nam", nameser1);

                    SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                    DataTable db = new DataTable("SERIALS");
                    dataadp.Fill(db);
                    dgToDoList.ItemsSource = db.DefaultView;
                    dataadp.Update(db);

                    sqlCon.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Введите строку для поиска");
            }
        }

        private void ReLoad(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select su.ID[ID], s.Name[Сериал], sea.Number_seasone[Текущий сезон], ser.Number_Series[Текущая серия] from USERS u join Serials_for_Users su on u.Login=su.LoginUser join SERIALS s on su.ID_Serials=s.ID_Serials join Seasone sea on su.Seasone_of_User=sea.ID_Seasone join Series ser on su.Series_of_User=ser.ID_Series where u.Login=@ID;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.CommandType = CommandType.Text;
                createCommand.Parameters.AddWithValue("@ID", Avtorizacia());
                createCommand.ExecuteNonQuery();
                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("SERIALS");
                dataadp.Fill(db);
                dgToDoList.ItemsSource = db.DefaultView;
                dataadp.Update(db);

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DelAllSerials_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgToDoList.SelectedItems.Count; i++)
                {
                    DataRowView dataro = dgToDoList.SelectedItems[i] as DataRowView;
                    if (dataro != null)
                    {
                        DataRow dataRow = dataro.Row;

                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        try
                        {

                            sqlCon.Open();
                            string Query3 = "delete from Serials_for_Users where ID=@ID; ";

                            SqlCommand createCommand = new SqlCommand(Query3, sqlCon);
                            createCommand.CommandType = CommandType.Text;

                            int g = Convert.ToInt32(dataRow[i]);

                            createCommand.Parameters.AddWithValue("@ID", g);

                            SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                            DataTable db = new DataTable("Serials_for_Users");
                            dataadp.Fill(db);
                            //dgToDoList.ItemsSource = db.DefaultView;
                            dataadp.Update(db);


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


                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void UpdAllSerials_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                for (int i = 0; i < dgToDoList.SelectedItems.Count; i++)
                {
                    DataRowView dataro = dgToDoList.SelectedItems[i] as DataRowView;
                    if (dataro != null)
                    {

                        DataRow dataRow = dataro.Row;
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        try
                        {
                            int g = Convert.ToInt32(dataRow[i]);
                            IDSerial.SetValue("currentCustomerName", g);
                            ChangeMySerials change = new ChangeMySerials();
                            change.Show();
                           
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
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static class IDSerial
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            testtslist test = new testtslist();
            Close();
            test.Show();
        }

        private void OFFERS(object sender, RoutedEventArgs e)
        {
            ADDOffers off = new ADDOffers();
            off.Show();
        }
    }
    
}
