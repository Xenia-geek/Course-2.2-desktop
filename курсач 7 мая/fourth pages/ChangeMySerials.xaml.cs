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
using static курсач_7_мая.input;
using static курсач_7_мая.myserials;

namespace курсач_7_мая.fourth_pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeMySerials.xaml
    /// </summary>
    public partial class ChangeMySerials : Window
    {
        public ChangeMySerials()
        {
            InitializeComponent();
            LoadData();
        }
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";


        private int IDSER()
        {

            int us;
            us = IDSerial.GetValue<int>("currentCustomerName");
            return us;

        }
        private string Avtorizacia()
        {

            string us;
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }

        private void LoadData()
        {
            IDSER();
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();

                string Query1 = "select s.Name[Сериал], sea.Number_seasone[Текущий сезон], ser.Number_Series[Текущая серия] from USERS u join Serials_for_Users su on u.Login=su.LoginUser join SERIALS s on su.ID_Serials=s.ID_Serials join Seasone sea on su.Seasone_of_User=sea.ID_Seasone join Series ser on su.Series_of_User=ser.ID_Series where su.LoginUser=@ID and su.ID=@IDSERIAL;";


                SqlCommand createCommand1 = new SqlCommand(Query1, sqlCon);
                createCommand1.CommandType = CommandType.Text;
                createCommand1.Parameters.AddWithValue("@ID", Avtorizacia());
                createCommand1.Parameters.AddWithValue("@IDSERIAL", IDSER());


                SqlDataReader myreader1;
                try
                {

                    myreader1 = createCommand1.ExecuteReader();
                    while (myreader1.Read())
                    {

                        string Name = myreader1.GetString(0);
                        NameSerial.Content = Name;
                        int NUMBSEAS = myreader1.GetInt32(1);
                        //Serialsss.Text = Convert.ToString(NUMBSEAS);
                        Serialsss.SelectedItem = NUMBSEAS;
                        int NUMBSERIA = myreader1.GetInt32(2);
                        //Serialssss.Text = Convert.ToString(NUMBSERIA);
                        Serialssss.SelectedItem = NUMBSERIA;
                        

                    }

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
            finally
            {
                sqlCon.Close();
            }


        }

        int IDSERIALL;
        #region ForSeasoneComboBox
        private void Serialsss_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Serialssss.Text = null;


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        Serialsss.Items.Clear();


                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        string sql1 = "select top(1) ID_Serials from Serials_for_Users where ID=@ID;";
                        SqlCommand cmd1 = new SqlCommand(sql1, con);
                        cmd1.CommandType = CommandType.Text;

                       
                        cmd1.Parameters.AddWithValue("@ID", IDSER());

                        IDSERIALL = Convert.ToInt32(cmd1.ExecuteScalar());


                        try
                        {

                            if (con.State == ConnectionState.Closed)
                                con.Open();

                            string query1 = "SELECT * FROM Seasone WHERE ID_Serials=@IDSER;";
                            SqlCommand sqlCmd1 = new SqlCommand(query1, con);


                            sqlCmd1.Parameters.AddWithValue("@IDSER", IDSERIALL);


                            if (Serialsss.Items.Count == 0)
                            {

                                SqlDataReader myreader1;
                                try
                                {
                                    myreader1 = sqlCmd1.ExecuteReader();
                                    while (myreader1.Read())
                                    {

                                        int NUMBSEAS = myreader1.GetInt32(2);
                                        Serialsss.Items.Add(NUMBSEAS);

                                    }
                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                                /////////////////////////////////////
                            }
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
                    finally
                    {
                        con.Close();
                    }
                }

            
        }
        #endregion

        int IDSEASONE;
        int NUMBSEASS;
        int IDSERIESS;
        #region ForSeriesComboBox

        private void Serialssss_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Serialsss.SelectedItem == null)
            {
                MessageBox.Show("Выберите сезон");
            }

            else
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                     try
                        {


                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            string sql2 = "select top(1) ID_Seasone from Seasone where ID_Serials=@IDAct and Number_seasone=@Numb;";
                            SqlCommand cmd2 = new SqlCommand(sql2, con);
                            cmd2.CommandType = CommandType.Text;


                            cmd2.Parameters.AddWithValue("@IDAct", IDSERIALL);


                            int d = Convert.ToInt32(Serialsss.Text);

                            cmd2.Parameters.AddWithValue("@Numb", d);


                            IDSEASONE = Convert.ToInt32(cmd2.ExecuteScalar());



                            try
                            {

                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                string query1 = "SELECT * FROM Series WHERE ID_Seasone=@IDSEAS;";
                                SqlCommand sqlCmd1 = new SqlCommand(query1, con);

                                sqlCmd1.Parameters.AddWithValue("@IDSEAS", IDSEASONE);

                                SqlDataReader myreader2;

                                if (Serialssss.Items.Count == 0)
                                {

                                    try
                                    {
                                        myreader2 = sqlCmd1.ExecuteReader();
                                        while (myreader2.Read())
                                        {
                                            NUMBSEASS = myreader2.GetInt32(2);
                                            Serialssss.Items.Add(NUMBSEASS);
                                            IDSERIESS = myreader2.GetInt32(0);

                                    }
                                    }

                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                    /////////////////////////////////////
                                }
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
                    finally
                    {
                        con.Close();
                    }
                }
                   

            }
        }
        #endregion
        #region ContinueButton
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (Serialsss.SelectedItem == null || Serialssss.SelectedItem == null)
            {
                MessageBox.Show("Введите все данные");
            }



            else if (SeasoneNOTcorrect.Opacity == 0 && SeriesNOTcorrect.Opacity == 0)
            {
                
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            try
                            {
                                

                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                string query1 = "update Serials_for_Users  set Seasone_of_User=@IDSEASONE where  LoginUser=@LOGIN and ID=@ID; update Serials_for_Users set Series_of_User=@IDSERIA  where  LoginUser=@LOGIN and ID=@ID;";

                                SqlCommand sqlCmd1 = new SqlCommand(query1, con);
                                sqlCmd1.CommandType = CommandType.Text;

                                sqlCmd1.Parameters.AddWithValue("@LOGIN", Avtorizacia());

                                sqlCmd1.Parameters.AddWithValue("@ID", IDSER());
                                sqlCmd1.Parameters.AddWithValue("@IDSEASONE", IDSEASONE);
                                sqlCmd1.Parameters.AddWithValue("@IDSERIA", IDSERIESS);

                            SqlDataAdapter dataadp = new SqlDataAdapter(sqlCmd1);
                            DataTable db = new DataTable("Serials_for_Users");
                            dataadp.Fill(db);
                            dataadp.Update(db);

                        Close();
                        
                                }

                                catch (Exception ex)
                                 {
                                    MessageBox.Show(ex.Message);
                                    }

                                 finally
                                {
                                    con.Close();
                                }



                }

            }
        }
        #endregion
    }
}
