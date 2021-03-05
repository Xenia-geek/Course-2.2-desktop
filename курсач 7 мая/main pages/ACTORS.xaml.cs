using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using курсач_7_мая.offers;
using курсач_7_мая.secondary_pages;
using курсач_7_мая.Tests;
using курсач_7_мая.third_pages;
using static курсач_7_мая.input;
using static курсач_7_мая.third_pages.ADDactors;

namespace курсач_7_мая.main_pages
{
    /// <summary>
    /// Логика взаимодействия для ACTORS.xaml
    /// </summary>
    public partial class ACTORS : Window
    {
        public ACTORS()
        {
            InitializeComponent();
            LoadAllSerials();
            Avtorizacia();
            
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";
        string Query = "select ID_Actor[ID], Photo[Фото], concat(Name, Surname)[ФИО], Date_of_Birth[День Рождения], Biography[Биография] from Actor ORDER BY Surname;";
        SqlDataAdapter dataadp;
        DataTable db = new DataTable("Actor");
        
        public void LoadAllSerials()
        {
            

            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select ID_Actor[ID], Photo[Фото],concat(Name, Surname)[ФИО], Date_of_Birth[День Рождения], Biography[Биография] from Actor ORDER BY Surname;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();
                 
                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Actor");
                dataadp.Fill(db);
                AllSerials.ItemsSource = db.DefaultView;
                dataadp.Update(db);

                
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //правильно, но сначало вывести имя юзера

            try
            {
                string Admin = "Ksyusha8";

                string us = Avtorizacia();
                if (Admin == us)
                {
                    AddAllSerials.Opacity = 0.8;
                    AddAllSerials.IsEnabled = true;
                    
                    DelAllSerials.Opacity = 0.8;
                    DelAllSerials.IsEnabled = true;
                    Zampred.Opacity = 1;
                    Zampred.IsEnabled = true;
                    Myserials.Header = "Статистика";

                }
                else
                {
                    AddAllSerials.Opacity = 0;
                    AddAllSerials.IsEnabled = false;
                    
                    DelAllSerials.Opacity = 0;
                    DelAllSerials.IsEnabled = false;

                    Zampred.Opacity = 0;
                    Zampred.IsEnabled = true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void MySerials_Click(object sender, RoutedEventArgs e)
        {
            string Admin = "Ksyusha8";

            string us = Avtorizacia();
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
        private void AllSerials_Click(object sender, RoutedEventArgs e)
        {
            allserials allserials = new allserials();
            Close();
            allserials.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ADDactors add = new ADDactors();
           
            add.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            input input = new input();
            Close();
            input.Show();
        }

        private void AllSerials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public string Avtorizacia()
        {
            string us;
            username.Header = ApplicationState.GetValue<string>("currentCustomerName");
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }

        private void AllDirectors_Click(object sender, RoutedEventArgs e)
        {
            DIRECTORS dir = new  DIRECTORS();
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
            if (Search.Text != "" || Search.Text!= "Поиск по ФИО...")
            {
                AllSerials.ItemsSource = null;
                try
                {
                    sqlCon.Open();
                    string Query2 = "select ID_Actor[ID], Photo[Фото], concat(Name, Surname)[ФИО], Date_of_Birth[День Рождения], Biography[Биография] from Actor where concat(Name, Surname) like @NameANDSurname ORDER BY Surname;";

                    SqlCommand createCommand = new SqlCommand(Query2, sqlCon);
                    createCommand.CommandType = CommandType.Text;

                    string nameser = "%" + Search.Text + "%";
                    createCommand.Parameters.AddWithValue("@NameANDSurname", nameser);

                    SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                    DataTable db = new DataTable("Actor");
                    dataadp.Fill(db);
                    AllSerials.ItemsSource = db.DefaultView;
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

        private void DelAllSerials_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                for (int i = 0; i < AllSerials.SelectedItems.Count; i++)
                {
                    DataRowView dataro = AllSerials.SelectedItems[i] as DataRowView;
                    if (dataro != null)
                    {
                        DataRow dataRow = dataro.Row;
                        
                            SqlConnection sqlCon = new SqlConnection(connectionString);
                        try
                        {

                            sqlCon.Open();
                            string Query3 = "delete  from ACTORS where ID_Actor=@ID; delete  from Actor where ID_Actor=@ID; ";

                            SqlCommand createCommand = new SqlCommand(Query3, sqlCon);
                            createCommand.CommandType = CommandType.Text;
                            
                            int g = Convert.ToInt32(dataRow[i]);
                           
                            createCommand.Parameters.AddWithValue("@ID", g);


                            SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                            DataTable db = new DataTable("Actor");
                            dataadp.Fill(db);
                            //AllSerials.ItemsSource = db.DefaultView;
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


        private void ReLoad(object sender, RoutedEventArgs e)
        {
            AllSerials.ItemsSource = null;
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Actor");
                dataadp.Fill(db);
                AllSerials.ItemsSource = db.DefaultView;
                dataadp.Update(db);


                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            testtslist test = new testtslist();
            Close();
            test.Show();
        }

        private void Zamech(object sender, RoutedEventArgs e)
        {
            offersANDcomplaints offer = new offersANDcomplaints();
            Close();
            offer.Show();
        }
    }
}