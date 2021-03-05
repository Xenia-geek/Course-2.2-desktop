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
using курсач_7_мая.Tests;
using курсач_7_мая.third_pages;
using static курсач_7_мая.input;

namespace курсач_7_мая.main_pages
{
    /// <summary>
    /// Логика взаимодействия для DIRECTORS.xaml
    /// </summary>
    public partial class DIRECTORS : Window
    {
        public DIRECTORS()
        {
            InitializeComponent();
            LoadAllSerials();
            Avtorizacia();
        }
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";
        DataTable db = new DataTable("Director");

        public void LoadAllSerials()
        {
            string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";


            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select ID_Director[ID], Photo[Фото], concat(Name, Surname)[ФИО], Date_of_Birth[День Рождения], Biography[Биография] from Director ORDER BY Surname;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Director");
                dataadp.Fill(db);
                ALLDirectors.ItemsSource = db.DefaultView;
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


        
        

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddDirectors add = new AddDirectors();
            
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
        

    


private void AllActors_Click(object sender, RoutedEventArgs e)
        {
            ACTORS actor = new ACTORS();
            Close();
            actor.Show();
        }

        private void AllSerials_Click(object sender, RoutedEventArgs e)
        {
            allserials serials = new allserials();
            Close();
            serials.Show();
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
        private string Avtorizacia()
        {
            string us;
            username.Header = ApplicationState.GetValue<string>("currentCustomerName");
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

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
                ALLDirectors.ItemsSource = null;
                try
                {
                    sqlCon.Open();
                    string Query = "select ID_Director[ID], Photo[Фото], concat(Name, Surname)[ФИО], Date_of_Birth[День Рождения], Biography[Биография] from Director where concat(Name, Surname) like @NameANDSurname ORDER BY Surname;";

                    SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                    createCommand.CommandType = CommandType.Text;

                    string nameser1 = "%" + Search.Text + "%";
                    createCommand.Parameters.AddWithValue("@NameANDSurname", nameser1);

                    SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                    DataTable db = new DataTable("Director");
                    dataadp.Fill(db);
                    ALLDirectors.ItemsSource = db.DefaultView;
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
            ALLDirectors.ItemsSource = null;
            string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";


            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select ID_Director[ID], Photo[Фото], concat(Name, Surname)[ФИО], Date_of_Birth[День Рождения], Biography[Биография] from Director ORDER BY Surname;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Director");
                dataadp.Fill(db);
                ALLDirectors.ItemsSource = db.DefaultView;
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
                for (int i = 0; i < ALLDirectors.SelectedItems.Count; i++)
                {
                    DataRowView dataro = ALLDirectors.SelectedItems[i] as DataRowView;
                    if (dataro != null)
                    {
                        DataRow dataRow = dataro.Row;

                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        try
                        {

                            sqlCon.Open();
                            string Query3 = "delete  from DIRECTORS where ID_Director=@ID; delete  from Director where ID_Director=@ID; ";

                            SqlCommand createCommand = new SqlCommand(Query3, sqlCon);
                            createCommand.CommandType = CommandType.Text;

                            int g = Convert.ToInt32(dataRow[i]);

                            createCommand.Parameters.AddWithValue("@ID", g);


                            SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                            DataTable db = new DataTable("Director");
                            dataadp.Fill(db);
                            //ALLDirectors.ItemsSource = db.DefaultView;
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
