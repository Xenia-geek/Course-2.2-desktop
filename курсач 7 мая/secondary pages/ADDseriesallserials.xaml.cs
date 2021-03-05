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
using static курсач_7_мая.secondary_pages.ADDsessoneallserials;

namespace курсач_7_мая.secondary_pages
{
    /// <summary>
    /// Логика взаимодействия для ADDseriesallserials.xaml
    /// </summary>
    public partial class ADDseriesallserials : Window
    {
        public ADDseriesallserials()
        {
            InitializeComponent();
            NumberSeasone();
            NameSerial();
            IDSeasone();
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        private int IDSeasone()
        {

            int us;
            us = ID_SEASONE.GetValue<int>("currentCustomerName");
            return us;

        }
        #region AddSerialClick

        private void Addseasone_Click(object sender, RoutedEventArgs e)
        {
            if (NumberSeries.Text == null || NameSeries.Text == null || NameSeries.Text == "Введите название")
            {
                MessageBox.Show("Номер серии или название введены не верно");
            }

            else if (NameSerNOTcorrect1.Opacity == 0 && NumbNOTcorrect.Opacity == 0)
            {

                SqlConnection sqlCon = new SqlConnection(connectionString);
                
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "SELECT COUNT(1) FROM Series WHERE Number_Series=@Number AND ID_Seasone=@ID;";

                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;


                        int value3 = Convert.ToInt32(NumberSeries.Text);

                        sqlCmd.Parameters.AddWithValue("@Number", value3);
                        sqlCmd.Parameters.AddWithValue("@ID", IDSeasone());



                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        if (count == 1)
                        {

                            if (bordernumbser != null)
                            {
                                bordernumbser.Background = Brushes.Red;
                            }
                            if (NumbNOTcorrect != null)
                            {
                                NumbNOTcorrect.Opacity = 1;
                            }
                            MessageBox.Show("Серия уже есть в базе");
                        }
                        else
                        {
                           
                        try
                        {
                                if (sqlCon.State == ConnectionState.Closed)
                                    sqlCon.Open();
                                string querySS = "SELECT COUNT(1) FROM Series WHERE ID_Seasone=@ID;";

                                SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);
                               
                                sqlCmd5.CommandType = CommandType.Text;
                                sqlCmd5.Parameters.AddWithValue("@ID", IDSeasone());

                                int value4 = Convert.ToInt32(NumberSeries.Text);

                                int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                                int o = value4 - 1;
                                if (countS == o)
                                {


                                        SqlConnection sqlCon2 = new SqlConnection(connectionString);
                                    
                                        sqlCon2.Open();

                                        SqlCommand sqlCmd2 = new SqlCommand("SERIESADD", sqlCon);
                                        sqlCmd2.CommandType = CommandType.StoredProcedure;
                                        sqlCmd2.Parameters.AddWithValue("@Number", value4);
                                        sqlCmd2.Parameters.AddWithValue("@Name", NameSeries.Text.Trim());
                                        sqlCmd2.Parameters.AddWithValue("@ID_Seasone", IDSeasone());

                                        sqlCmd2.ExecuteNonQuery();

                                        //int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                        //ID_SEASONE.SetValue("currentCustomerName", IDSEASONE);

                                        //SeasoneNum.SetValue("currentCustomerName", NumberSeries.Text);
                                    
                                    //////////////////////////////////////

                                    ADDsessoneallserials seasone = new ADDsessoneallserials();
                                    Close();
                                    seasone.Show();

                                    /////////////////////////////////////
                                }
                                else
                                {
                                    MessageBox.Show("Вы пропустили cерию");
                                    if (bordernumbser != null)
                                    {
                                        bordernumbser.Background = Brushes.Red;
                                    }
                                    if (NumbNOTcorrect != null)
                                    {
                                        NumbNOTcorrect.Opacity = 1;
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
            else if (NumbNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введена серия");
            }
            else if (NumberSeries.Opacity == 1)
            {
                MessageBox.Show("Не корректно введено название серии");
            }

        }

        #endregion


        #region AddSeriesClick
        private void Addsries_Click(object sender, RoutedEventArgs e)
        {
            if (NumberSeries.Text == null || NameSeries.Text == null || NameSeries.Text == "Введите название")
            {
                MessageBox.Show("Номер серии или название введены не верно");
            }

            else if (NameSerNOTcorrect1.Opacity == 0 && NumbNOTcorrect.Opacity == 0)
            {

                SqlConnection sqlCon = new SqlConnection(connectionString);
                
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "SELECT COUNT(1) FROM Series WHERE Number_Series=@Number AND ID_Seasone=@ID;";

                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;


                        int value3 = Convert.ToInt32(NumberSeries.Text);

                        sqlCmd.Parameters.AddWithValue("@Number", value3);
                        sqlCmd.Parameters.AddWithValue("@ID", IDSeasone());



                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        if (count == 1)
                        {

                            if (bordernumbser != null)
                            {
                                bordernumbser.Background = Brushes.Red;
                            }
                            if (NumbNOTcorrect != null)
                            {
                                NumbNOTcorrect.Opacity = 1;
                            }
                            MessageBox.Show("Серия уже есть в базе");
                        }
                        else
                        {
                        try
                            {
                                if (sqlCon.State == ConnectionState.Closed)
                                    sqlCon.Open();
                                string querySS = "SELECT COUNT(1) FROM Series WHERE ID_Seasone=@ID_Seasone;";

                                SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);

                                sqlCmd5.CommandType = CommandType.Text;
                                sqlCmd5.Parameters.AddWithValue("@ID_Seasone", IDSeasone());

                                int value4 = Convert.ToInt32(NumberSeries.Text);

                                int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                            int o = value4 - 1;
                            if (countS == o)
                            {


                                    SqlConnection sqlCon2 = new SqlConnection(connectionString);
                                    
                                        sqlCon2.Open();

                                        SqlCommand sqlCmd2 = new SqlCommand("SERIESADD", sqlCon2);
                                        sqlCmd2.CommandType = CommandType.StoredProcedure;
                                        sqlCmd2.Parameters.AddWithValue("@Number", value4);
                                        sqlCmd2.Parameters.AddWithValue("@Name", NameSeries.Text.Trim());
                                        sqlCmd2.Parameters.AddWithValue("@ID_Seasone", IDSeasone());


                                        sqlCmd2.ExecuteNonQuery();

                                        //int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                        //ID_SEASONE.SetValue("currentCustomerName", IDSEASONE);

                                        //SeasoneNum.SetValue("currentCustomerName", NumberSeries.Text);
                                    
                                    //////////////////////////////////////

                                    ADDseriesallserials series = new ADDseriesallserials();
                                    Close();
                                    series.Show();

                                    /////////////////////////////////////
                                }
                                else
                                {
                                    MessageBox.Show("Вы пропустили cерию");
                                    if (bordernumbser != null)
                                    {
                                        bordernumbser.Background = Brushes.Red;
                                    }
                                    if (NumbNOTcorrect != null)
                                    {
                                        NumbNOTcorrect.Opacity = 1;
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
            else if (NumbNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введена серия");
            }
            else if (NumberSeries.Opacity == 1)
            {
                MessageBox.Show("Не корректно введено название серии");
            }

        }
        #endregion


        #region ContinueToGenre
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (NumberSeries.Text == null || NameSeries.Text == null || NameSeries.Text == "Введите название")
            {
                MessageBox.Show("Номер серии или название введены не верно");
            }

            else if (NameSerNOTcorrect1.Opacity == 0 && NumbNOTcorrect.Opacity == 0)
            {

                SqlConnection sqlCon = new SqlConnection(connectionString);
                
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "SELECT COUNT(1) FROM Series WHERE Number_Series=@Number AND ID_Seasone=@ID;";

                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;


                        int value3 = Convert.ToInt32(NumberSeries.Text);

                        sqlCmd.Parameters.AddWithValue("@Number", value3);
                        sqlCmd.Parameters.AddWithValue("@ID", IDSeasone());



                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        if (count == 1)
                        {

                            if (bordernumbser != null)
                            {
                                bordernumbser.Background = Brushes.Red;
                            }
                            if (NumbNOTcorrect != null)
                            {
                                NumbNOTcorrect.Opacity = 1;
                            }
                            MessageBox.Show("Серия уже есть в базе");
                        }
                        else
                        {
                                
                        try
                            {
                                if (sqlCon.State == ConnectionState.Closed)
                                    sqlCon.Open();
                                string querySS = "SELECT COUNT(1) FROM Series WHERE ID_Seasone=@ID;";

                                SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);

                                sqlCmd5.CommandType = CommandType.Text;
                                sqlCmd5.Parameters.AddWithValue("@ID", IDSeasone());

                                int value4 = Convert.ToInt32(NumberSeries.Text);

                                int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());

                                //MessageBox.Show(countS.ToString());
                                //MessageBox.Show(value4.ToString());
                            int o = value4 - 1;
                            if (countS == o)
                            {


                                    SqlConnection sqlCon2 = new SqlConnection(connectionString);
                                    
                                        sqlCon2.Open();

                                        SqlCommand sqlCmd2 = new SqlCommand("SERIESADD", sqlCon2);
                                        sqlCmd2.CommandType = CommandType.StoredProcedure;
                                        sqlCmd2.Parameters.AddWithValue("@Number", value4);
                                        sqlCmd2.Parameters.AddWithValue("@Name", NameSeries.Text.Trim());
                                        sqlCmd2.Parameters.AddWithValue("@ID_Seasone", IDSeasone());


                                        sqlCmd2.ExecuteNonQuery();

                                        //int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                        //ID_SEASONE.SetValue("currentCustomerName", IDSEASONE);

                                        //SeasoneNum.SetValue("currentCustomerName", NumberSeries.Text);
                                    
                                    //////////////////////////////////////

                                    ADDgenreserials genre = new ADDgenreserials();
                                    Close();
                                    genre.Show();

                                    /////////////////////////////////////
                                }
                                else
                                {
                                    MessageBox.Show("Вы пропустили cерию");
                                    if (bordernumbser != null)
                                    {
                                        bordernumbser.Background = Brushes.Red;
                                    }
                                    if (NumbNOTcorrect != null)
                                    {
                                        NumbNOTcorrect.Opacity = 1;
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
            else if (NumbNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введена серия");
            }
            else if (NumberSeries.Opacity == 1)
            {
                MessageBox.Show("Не корректно введено название серии");
            }


        }
        #endregion

        private void NumberSeries_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NumberSeries.Text = "";
        }

        private void NameSeries_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameSeries.Text = "";
        }

        #region Valid

        private void NumberSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(NumberSeries.Text, @"^[0-9]+$").Success || NumberSeries.Text.Length > 2)
            {
                if (bordernumbser != null)
                {
                    bordernumbser.Background = Brushes.Red;
                }
                if (NumbNOTcorrect != null)
                {
                    NumbNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (bordernumbser != null)
                {
                    bordernumbser.Background = Brushes.HotPink;
                    bordernumbser.Opacity = 0.7;
                }
                if (NumbNOTcorrect != null)
                {
                    NumbNOTcorrect.Opacity = 0;
                }

            }
        }

        private void NameSeries_TextChange(object sender, TextChangedEventArgs e)
        {
            if ( NameSeries.Text  == null || NameSeries.Text== "Введите название")
            {
                if (bordernumbser != null)
                {
                    bordernumbser.Background = Brushes.Red;
                }
                if (NumbNOTcorrect != null)
                {
                    NumbNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (bordernumbser != null)
                {
                    bordernumbser.Background = Brushes.HotPink;
                    bordernumbser.Opacity = 0.7;
                }
                if (NumbNOTcorrect != null)
                {
                    NumbNOTcorrect.Opacity = 0;
                }

            }
        }
        #endregion

        #region IDandName

        private string NameSerial()
        {

            string us;
            SerialName.Content = SerialsNames.GetValue<string>("currentCustomerName");
            us = SerialsNames.GetValue<string>("currentCustomerName");
            return us;

        }
        private string NumberSeasone()
        {

            string us;
            SerialSeasoneNumber.Content = SeasoneNum.GetValue<string>("currentCustomerName")+" сезон";
            us= SeasoneNum.GetValue<string>("currentCustomerName");
            return us;

        }

       
        #endregion

    }
}
