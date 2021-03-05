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
using курсач_7_мая.main_pages;
using курсач_7_мая.Tests;
using static курсач_7_мая.input;

namespace курсач_7_мая.offers
{
    /// <summary>
    /// Логика взаимодействия для offersANDcomplaints.xaml
    /// </summary>
    public partial class offersANDcomplaints : Window
    {
        public offersANDcomplaints()
        {
            InitializeComponent();
            Avtorizacia();
            LoadAllOffers();
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        private string Avtorizacia()
        {

            string us;
            username.Header = ApplicationState.GetValue<string>("currentCustomerName");
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }


        private void LoadAllOffers()
        {

            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {

                sqlCon.Open();
                string Query = "select Login, Offer, DateofSend from Offers ORDER BY DateofSend;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.CommandType = CommandType.Text;
                string d = Avtorizacia();
                createCommand.Parameters.AddWithValue("@IDLOG",d);
                createCommand.ExecuteNonQuery();
                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Offers");
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

        private void Myserials_Click(object sender, RoutedEventArgs e)
        {
            AllUsers all = new AllUsers();
            Close();
            all.Show();
        }

        private void allserials(object sender, RoutedEventArgs e)
        {
            allserials serials = new allserials();
            Close();
            serials.Show();
        }

        private void Actorrs_Click(object sender, RoutedEventArgs e)
        {
            ACTORS actorrs = new ACTORS();
            Close();
            actorrs.Show();
        }

        private void AllDirectors_Click(object sender, RoutedEventArgs e)
        {
            DIRECTORS direct = new DIRECTORS();
            Close();
            direct.Show();

        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            testtslist listtest = new testtslist();
            Close();
            listtest.Show();
        }

        private void ReloadAllSerials_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {

                sqlCon.Open();
                string Query = "select Login, Offer, DateofSend from Offers ORDER BY DateofSend;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.CommandType = CommandType.Text;
                string d = Avtorizacia();
                createCommand.Parameters.AddWithValue("@IDLOG", d);
                createCommand.ExecuteNonQuery();
                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Offers");
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

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Search_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Search.Text = "";
        }

        private void SerAllSerials_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (Search.Text != "" || Search.Text != "Поиск по теме...")
            {
                AllSerials.ItemsSource = null;
                try
                {
                    sqlCon.Open();
                    string Query2 = "select Login, Offer, DateofSend from Offers where Offer like @NameTeam ORDER BY DateofSend ;";

                    SqlCommand createCommand = new SqlCommand(Query2, sqlCon);
                    createCommand.CommandType = CommandType.Text;

                    string nameser = "%" + Search.Text + "%";
                    createCommand.Parameters.AddWithValue("@NameTeam", nameser);

                    SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                    DataTable db = new DataTable("Test");
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            input inp = new input();
            Close();
            inp.Show();
        }
    }
}
