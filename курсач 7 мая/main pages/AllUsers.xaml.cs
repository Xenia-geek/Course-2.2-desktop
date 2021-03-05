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
using static курсач_7_мая.input;

namespace курсач_7_мая.main_pages
{
    /// <summary>
    /// Логика взаимодействия для AllUsers.xaml
    /// </summary>
    public partial class AllUsers : Window
    {
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        public AllUsers()
        {
            InitializeComponent();
            Avtorizacia();
            LoadAllSerials();
        }



        private void LoadAllSerials()
        {

            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select COUNT(Login)-1 from USERS;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();
                string User;
                User = Convert.ToString(createCommand.ExecuteScalar());
                Count.Content = User;
                sqlCon.Close();

                try
                {
                    sqlCon.Open();
                    string Query1 = "declare @w int; set @w=(select COUNT(1) from USERS)-1; select se.Name[сериал], (((COUNT(su.ID_Serials))*100)/@w)[зрителей к пользователям(%)] from SERIALS se join Serials_for_Users su on se.ID_Serials=su.ID_Serials  GROUP BY se.Name;";
                    SqlCommand createCommand1 = new SqlCommand(Query1, sqlCon);
                    createCommand1.ExecuteNonQuery();

                    SqlDataAdapter dataadp = new SqlDataAdapter(createCommand1);
                    DataTable db = new DataTable("SERIALS");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ////правильно, но сначало вывести имя юзера


        }


        private void MySerials_Click(object sender, RoutedEventArgs e)
            {


                AllUsers alluser = new AllUsers();
                Close();
                alluser.Show();

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

        private void Allusers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AllActors_Click(object sender, RoutedEventArgs e)
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

        private void ReLoad(object sender, RoutedEventArgs e)
        {
            Count.Content = null;
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select COUNT(Login)-1 from USERS;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();
                string User;
                User = Convert.ToString(createCommand.ExecuteScalar());
                Count.Content = User;
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
