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
using курсач_7_мая.offers;
using static курсач_7_мая.input;

namespace курсач_7_мая.Tests
{
    /// <summary>
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        public Results()
        {
            InitializeComponent();
            Avtorizacia();
            LoadResult();
            
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

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            input input = new input();
            Close();
            input.Show();
        }

        private void AllSerials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private string Avtorizacia()
        {
            string us;
            username.Header = ApplicationState.GetValue<string>("currentCustomerName");
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ACTORS act = new ACTORS();
            Close();
            act.Show();
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

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            testtslist test = new testtslist();
            Close();
            test.Show();
        }
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        private void LoadResult()
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select t.Name[тема(сериал)], se.Number_seasone[сезон],pr.Data_of_Test[Дата прохождения], pr.Count_of_Right_Answer[кол-во прав отв], pr.IS_Right[пройдено(нет)] from Progress pr join Test tes on pr.ID_Test=tes.ID_Test join Team t on tes.ID_Team=t.ID_Team  join Seasone se on  t.ID_Seasone=se.ID_Seasone where pr.Login=@IDLOGIN ORDER BY pr.Data_of_Test;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.CommandType = CommandType.Text;

               
                createCommand.Parameters.AddWithValue("@IDLOGIN", Avtorizacia());
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Progress");
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

        private void AllSerials_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

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
                    string Query2 = "select t.Name[тема(сериал)], se.Number_seasone[сезон],pr.Data_of_Test[Дата прохождения], pr.Count_of_Right_Answer[кол-во прав отв], pr.IS_Right[пройдено(нет)] from Progress pr join Test tes on pr.ID_Test=tes.ID_Test join Team t on tes.ID_Team=t.ID_Team  join Seasone se on  t.ID_Seasone=se.ID_Seasone where pr.Login=@IDLOGIN and t.Name like @NAMESEARCH ORDER BY pr.Data_of_Test;";

                    SqlCommand createCommand = new SqlCommand(Query2, sqlCon);
                    createCommand.CommandType = CommandType.Text;
                    createCommand.Parameters.AddWithValue("@IDLOGIN", Avtorizacia());
                    string nameser = "%" + Search.Text + "%";
                    createCommand.Parameters.AddWithValue("@NAMESEARCH", nameser);

                    SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                    DataTable db = new DataTable("Progress");
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

        private void ReloadAllSerials_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select t.Name[тема(сериал)], se.Number_seasone[сезон],pr.Data_of_Test[Дата прохождения], pr.Count_of_Right_Answer[кол-во прав отв], pr.IS_Right[пройдено(нет)] from Progress pr join Test tes on pr.ID_Test=tes.ID_Test join Team t on tes.ID_Team=t.ID_Team  join Seasone se on  t.ID_Seasone=se.ID_Seasone where pr.Login=@IDLOGIN ORDER BY pr.Data_of_Test;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.CommandType = CommandType.Text;


                createCommand.Parameters.AddWithValue("@IDLOGIN", Avtorizacia());
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Progress");
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

        private void Zamech(object sender, RoutedEventArgs e)
        {
            offersANDcomplaints offer = new offersANDcomplaints();
            Close();
            offer.Show();
        }
    }
}
