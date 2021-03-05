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
    /// Логика взаимодействия для ADDdirectorserials.xaml
    /// </summary>
    public partial class ADDdirectorserials : Window
    {
        public ADDdirectorserials()
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
            string sql = "select * from Director;";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    NameAct = myreader.GetString(1);
                    Surname = myreader.GetString(2);
                    nameandsurname = NameAct + " " + Surname;
                    FIO.Items.Add(nameandsurname);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        #region ContinueClic

        private void Continue_Click_1(object sender, RoutedEventArgs e)
        {
            if (FIO.SelectedItem == null)
            {
                MessageBox.Show("Выберите режиссера");
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
                        string query = "SELECT top(1) ID_Director FROM Director WHERE  Name=@Name and Surname=@Surname;;";

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
                            string query1 = "SELECT COUNT(1) FROM DIRECTORS WHERE ID_Serials=@IDSER AND ID_Director=@IDACT;";

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
                                MessageBox.Show("Режиссер уже есть в базе");
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

                                    SqlCommand sqlCmd2 = new SqlCommand("DIRRECTADD", sqlCon2);

                                    sqlCmd2.CommandType = CommandType.StoredProcedure;

                                    sqlCmd2.Parameters.AddWithValue("@IDDIRECTOR", IDAct);

                                    sqlCmd2.Parameters.AddWithValue("@IDSerials", IDSerialls());


                                    sqlCmd2.ExecuteNonQuery();

                                }
                                //////////////////////////////////////

                                Close();

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

        

        

        
    }
}
