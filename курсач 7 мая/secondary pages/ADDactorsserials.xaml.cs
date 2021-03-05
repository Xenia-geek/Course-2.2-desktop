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
using static курсач_7_мая.secondary_pages.ADDallserials;

namespace курсач_7_мая.secondary_pages
{
    /// <summary>
    /// Логика взаимодействия для ADDactorsserials.xaml
    /// </summary>
    public partial class ADDactorsserials : Window
    {
        public ADDactorsserials()
        {
            InitializeComponent();
            NameSerial();
            ToComboBox();
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";
        string nameandsurname;
        string NameAct;
        string Surname; 

        private void FIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private string NameSerial()
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


        public void ToComboBox()
        {
            SqlConnection con = new SqlConnection(@"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;");
            string sql = "select * from Actor;";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while(myreader.Read())
                {
                    NameAct = myreader.GetString(1);
                    Surname = myreader.GetString(2);
                    nameandsurname = NameAct + " " + Surname;
                    FIO.Items.Add(nameandsurname);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        #region ContinueClic

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (FIO.SelectedItem == null)
            {
                MessageBox.Show("Выберите актера");
            }

            else if (ActorNOTcorrect.Opacity == 0)
            {

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    try
                    {
                        int IDAct;
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "SELECT top(1) ID_Actor FROM Actor WHERE Name=@Name and Surname=@Surname;";

                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;

                        String s = FIO.Text;
                        String[] words = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);



                        sqlCmd.Parameters.AddWithValue("@Name", words[0]);
                        sqlCmd.Parameters.AddWithValue("@Surname", words[1]);

                         IDAct = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        

                                try
                                {
                                    if (sqlCon.State == ConnectionState.Closed)
                                        sqlCon.Open();
                                    string query1 = "SELECT COUNT(1) FROM ACTORS WHERE ID_Serials=@IDSER AND ID_Actor=@IDACT;";

                                    SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                                    sqlCmd1.CommandType = CommandType.Text;
                                    

                                    sqlCmd1.Parameters.AddWithValue("@IDSER", IDSerialls());
                                    

                                    sqlCmd1.Parameters.AddWithValue("@IDACT", IDAct);



                                    int count = Convert.ToInt32(sqlCmd1.ExecuteScalar());

                                    if (count == 1)
                                    {

                                        if (borderlogin != null)
                                        {
                                            borderlogin.Background = Brushes.Red;
                                        }
                                        if (ActorNOTcorrect != null)
                                        {
                                            ActorNOTcorrect.Opacity = 1;
                                        }
                                        MessageBox.Show("Актер уже есть в базе");
                                    }
                                    else
                                    {
                                        if (borderlogin != null)
                                            {
                                                  borderlogin.Background = null;
                                                }
                                         if (ActorNOTcorrect != null)
                                            {
                                                 ActorNOTcorrect.Opacity = 0;
                                            }


                                using (SqlConnection sqlCon2 = new SqlConnection(connectionString))
                                        {
                                            sqlCon2.Open();

                                            SqlCommand sqlCmd2 = new SqlCommand("AACTORSADD", sqlCon2);

                                            sqlCmd2.CommandType = CommandType.StoredProcedure;
                                            
                                            sqlCmd2.Parameters.AddWithValue("@IDActor", IDAct);
                                            
                                            sqlCmd2.Parameters.AddWithValue("@IDSerials", IDSerialls());


                                            sqlCmd2.ExecuteNonQuery();

                                        }
                                        //////////////////////////////////////

                                        ADDdirectorserials dir = new ADDdirectorserials();
                                        Close();
                                        dir.Show();

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
                        sqlCon.Close();
                    }



                }

            }
        }
        #endregion

        #region MoreClick


       private void Genre_Click(object sender, RoutedEventArgs e)
        {
            if (FIO.SelectedItem == null)
            {
                MessageBox.Show("Выберите актера");
            }

            else if (ActorNOTcorrect.Opacity == 0)
            {

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    try
                    {
                        int IDAct;
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();

                        string query = "SELECT top(1) ID_Actor FROM Actor WHERE Name=@Name and Surname=@Surname;";

                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;

                        String s = FIO.Text;
                        String[] words = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);



                        sqlCmd.Parameters.AddWithValue("@Name", words[0]);
                        sqlCmd.Parameters.AddWithValue("@Surname", words[1]);

                        IDAct = Convert.ToInt32(sqlCmd.ExecuteScalar());


                        try
                        {
                            if (sqlCon.State == ConnectionState.Closed)
                                sqlCon.Open();
                            string query1 = "SELECT COUNT(1) FROM ACTORS WHERE ID_Serials=@IDSER AND ID_Actor=@IDACT;";

                            SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                            sqlCmd1.CommandType = CommandType.Text;


                            sqlCmd1.Parameters.AddWithValue("@IDSER", IDSerialls());
                            sqlCmd1.Parameters.AddWithValue("@IDACT", IDAct);



                            int count = Convert.ToInt32(sqlCmd1.ExecuteScalar());

                            if (count == 1)
                            {

                                if (borderlogin != null)
                                {
                                    borderlogin.Background = Brushes.Red;
                                }
                                if (ActorNOTcorrect != null)
                                {
                                    ActorNOTcorrect.Opacity = 1;
                                }
                                MessageBox.Show("Актер уже есть в базе");
                            }
                            else
                            {
                                if (borderlogin != null)
                                {
                                    borderlogin.Background = null;
                                }
                                if (ActorNOTcorrect != null)
                                {
                                    ActorNOTcorrect.Opacity = 0;
                                }
                                using (SqlConnection sqlCon2 = new SqlConnection(connectionString))
                                {
                                    sqlCon2.Open();

                                    SqlCommand sqlCmd2 = new SqlCommand("AACTORSADD", sqlCon2);

                                    sqlCmd2.CommandType = CommandType.StoredProcedure;

                                    sqlCmd2.Parameters.AddWithValue("@IDActor", IDAct);

                                    sqlCmd2.Parameters.AddWithValue("@IDSerials", IDSerialls());


                                    sqlCmd2.ExecuteNonQuery();

                                }
                                //////////////////////////////////////

                                ADDactorsserials actr = new ADDactorsserials();
                                Close();
                                actr.Show();

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
                        sqlCon.Close();
                    }



                }

            }
        }





        #endregion

        private void ActorNOTcorrect_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ActorNOTcorrect_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void FIO_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FIO.SelectedItem = null;
            if (borderlogin != null)
            {
                borderlogin.Background = null;
            }
            if (ActorNOTcorrect != null)
            {
                ActorNOTcorrect.Opacity = 0;
            }
        }
    }


}
