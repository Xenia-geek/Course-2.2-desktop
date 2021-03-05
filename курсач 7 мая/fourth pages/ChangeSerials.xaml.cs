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
using static курсач_7_мая.allserials;

namespace курсач_7_мая.fourth_pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeSerials.xaml
    /// </summary>
    public partial class ChangeSerials : Window
    {
        public ChangeSerials()
        {
            InitializeComponent();
            LoadData();
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        private void LoadData()
        {
            IDSER();
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                
                    string Query1 = "select s.Name, se.Number_seasone, se.Photo, se.About_seasone, ser.Number_Series, ser.Name_Series from SERIALS s join Seasone se on s.ID_Serials=se.ID_Serials join Series ser on se.ID_Seasone=ser.ID_Seasone where s.ID_Serials=@ID order by s.Name;";
                    

                    SqlCommand createCommand1 = new SqlCommand(Query1, sqlCon);
                    createCommand1.CommandType = CommandType.Text;
                    createCommand1.Parameters.AddWithValue("@ID", IDSER());


                    SqlDataReader myreader1;
                    try
                    {
                        
                        myreader1 = createCommand1.ExecuteReader();
                        while (myreader1.Read())
                        {

                            string Name = myreader1.GetString(0);
                            NameSerials.Content = Name;
                            int NUMBSEAS = myreader1.GetInt32(1);
                            SeasoneNumber.Text = Convert.ToString(NUMBSEAS);
                            //string Photo = myreader1.GetString(0);
                            string AboutSeas = myreader1.GetString(3);
                            AboutSeasone1.Text = AboutSeas;
                            int NumbSeries = myreader1.GetInt32(4);
                            NumberSeries.Text = Convert.ToString(NumbSeries);
                            string NameSeria = myreader1.GetString(5);
                            NameSeries.Text = NameSeria;

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


        private int IDSER()
        {

            int us;
            us = IDSerial.GetValue<int>("currentCustomerName");
            return us;

        }

        private void SeasoneNumber_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SeasoneNumber.Text = "";
            AboutSeasone1.IsEnabled = true;
            AboutSeasone1.Text = "";
            AboutNOTcorrect.Opacity = 1;
            addPicture.IsEnabled = true;
            
            NumberSeries.IsEnabled = true;
            NumberSeries.Text = "";
            NumbNOTcorrect.Opacity = 1;
            NameSeries.IsEnabled = true;
            NameSeries.Text = "";
            NameSerNOTcorrect1.Opacity = 1;

        }

        private void SeasoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (!Regex.Match(SeasoneNumber.Text, @"^[0-9]+$").Success || SeasoneNumber.Text.Length > 2)
            {
                if (bordernumbersesone != null)
                {
                    bordernumbersesone.Background = Brushes.Red;
                }
                if (NOTcorrect != null)
                {
                    NOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (bordernumbersesone != null)
                {
                    bordernumbersesone.Background = Brushes.HotPink;
                    bordernumbersesone.Opacity = 0.7;
                }
                if (NOTcorrect != null)
                {
                    NOTcorrect.Opacity = 0;
                }

            }
        }

        private void AboutSeasone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AboutSeasone1.Text = "";

        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);

            try
            {
                sqlCon.Open();
                string Query = "select Year_of_start  from SERIALS  where ID_Serials=@ID ;";

                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.CommandType = CommandType.Text;
                createCommand.Parameters.AddWithValue("@ID", IDSER());
                DateTime count = Convert.ToDateTime(createCommand.ExecuteScalar());

                if (EndDate.SelectedDate > DateTime.Now || EndDate.SelectedDate <= count)
                {
                    EndDate.Background = Brushes.Red;
                    if (DateEndNOTcorrect != null)
                    {
                        DateEndNOTcorrect.Opacity = 1;
                    }
                }
                else
                {
                    EndDate.Background = null;
                    if (DateEndNOTcorrect != null)
                    {
                        DateEndNOTcorrect.Opacity = 0;
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

        private void NumberSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(SeasoneNumber.Text, @"^[0-9]+$").Success || SeasoneNumber.Text.Length > 2)
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

        private void NameSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameSeries.Text == "" || NameSeries.Text == "Введите название")
            {
                if (bordernameser != null)
                {
                    bordernameser.Background = Brushes.Red;
                }
                if (NameSerNOTcorrect1 != null)
                {
                    NameSerNOTcorrect1.Opacity = 1;
                }
            }
            else
            {
                if (bordernameser != null)
                {
                    bordernameser.Background = Brushes.HotPink;
                    bordernameser.Opacity = 0.7;
                }
                if (NameSerNOTcorrect1 != null)
                {
                    NameSerNOTcorrect1.Opacity = 0;
                }

            }
        }

        private void NameSeries_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameSeries.Text = "";
        }

        private void NumberSeries_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NumberSeries.Text = "";
        }

        private void AboutSeasone1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AboutSeasone1.Text == null || AboutSeasone1.Text == "Введите немного о сезоне")
            {
               
                if (AboutNOTcorrect != null)
                {
                    AboutNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (AboutNOTcorrect != null)
                {
                    AboutNOTcorrect.Opacity = 0;
                }

            }
        }



        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (EndDate.SelectedDate == null && AboutSeasone1.IsEnabled == false)
            {
                MessageBox.Show("Никакие данные не были введены, проверьте введенность либо даты либо блока про сезон и сериал");
                
            }
            else if (DateEndNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("конечная дата введена не корректно");

            }
            #region ForOnlyData
            else if (EndDate.SelectedDate != null && AboutSeasone1.IsEnabled == false)
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);

                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "update SERIALS set Year_of_End = @EndDat where  ID_Serials=@ID;";

                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;

                    sqlCmd.Parameters.AddWithValue("@EndDat", EndDate.SelectedDate);
                    sqlCmd.Parameters.AddWithValue("@ID", IDSER());

                    SqlDataAdapter dataadp = new SqlDataAdapter(sqlCmd);
                    DataTable db = new DataTable("SERIALS");
                    dataadp.Fill(db);
                    dataadp.Update(db);

                    MessageBox.Show("Дата окончания изменена");
                    Close();
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
            #endregion

            #region OnlyForSeasoneAndSeries
            else if (EndDate.SelectedDate == null && AboutSeasone1.IsEnabled == true)
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);

                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "SELECT COUNT(1) FROM Seasone WHERE Number_seasone=@Number AND ID_Serials=@ID;";

                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;


                    int value3 = Convert.ToInt32(SeasoneNumber.Text);

                    sqlCmd.Parameters.AddWithValue("@Number", value3);
                    sqlCmd.Parameters.AddWithValue("@ID", IDSER());



                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {

                        if (bordernumbersesone != null)
                        {
                            bordernumbersesone.Background = Brushes.Red;
                        }
                        if (NOTcorrect != null)
                        {
                            NOTcorrect.Opacity = 1;
                        }
                        MessageBox.Show("Сезон уже есть в базе");
                        try
                        {
                            if (sqlCon.State == ConnectionState.Closed)
                                sqlCon.Open();
                            string query1 = "SELECT COUNT(1) FROM Series WHERE Number_Series=@Number AND ID_Seasone=@ID;";

                            SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                            sqlCmd1.CommandType = CommandType.Text;


                            int value1 = Convert.ToInt32(NumberSeries.Text);

                            sqlCmd1.Parameters.AddWithValue("@Number", value1);

                            int ii = SeasoneNum.GetValue<int>("currentCustomerName");
                            /////////////////////////
                            sqlCmd1.Parameters.AddWithValue("@ID", ii);
                            /////////////////////////


                            int count1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                            if (count1 == 1)
                            {

                                if (bordernumbser != null)
                                {
                                    bordernumbser.Background = Brushes.Red;
                                }
                                if (NumbNOTcorrect != null)
                                {
                                    NumbNOTcorrect.Opacity = 1;
                                }
                                MessageBox.Show("Такая серия для этого сезона уже есть в базе");
                            }
                            else
                            {

                                try
                                {
                                    if (sqlCon.State == ConnectionState.Closed)
                                        sqlCon.Open();
                                    string queryS1 = "SELECT COUNT(1) FROM Series WHERE ID_Seasone=@ID;";

                                    SqlCommand sqlCmd2 = new SqlCommand(queryS1, sqlCon);

                                    sqlCmd2.CommandType = CommandType.Text;
                                    sqlCmd2.Parameters.AddWithValue("@ID", ii);

                                    int value4 = Convert.ToInt32(NumberSeries.Text);

                                    int countS11 = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                    int o = value4 - 1;
                                    if (countS11 == o)
                                    {


                                        SqlConnection sqlCon6 = new SqlConnection(connectionString);

                                        //sqlCon.Open();

                                        SqlCommand sqlCmd6 = new SqlCommand("SERIESADD", sqlCon);
                                        sqlCmd6.CommandType = CommandType.StoredProcedure;
                                        sqlCmd6.Parameters.AddWithValue("@Number", value4);
                                        sqlCmd6.Parameters.AddWithValue("@Name", NameSeries.Text.Trim());
                                        sqlCmd6.Parameters.AddWithValue("@ID_Seasone", ii);

                                        sqlCmd6.ExecuteNonQuery();

                                        //int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                        //ID_SEASONE.SetValue("currentCustomerName", IDSEASONE);

                                        //SeasoneNum.SetValue("currentCustomerName", NumberSeries.Text);

                                        //////////////////////////////////////
                                        
                                        
                                        MessageBox.Show("Серия к существующему сезону добавлена");

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
                    else
                    {
                        try
                        {
                            if (sqlCon.State == ConnectionState.Closed)
                                sqlCon.Open();
                            string querySS = "SELECT COUNT(1) FROM Seasone WHERE ID_Serials=@ID;";

                            SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);
                            sqlCmd5.CommandType = CommandType.Text;
                            sqlCmd5.Parameters.AddWithValue("@ID", IDSER());

                            int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                            int p = value3 - 1;

                            if (countS == p)
                            {

                                
                                //sqlCon.Open();
                                if (PhotoSeasone.Source == null)
                                {
                                    SqlCommand sqlCmd2 = new SqlCommand("SEASONEADDwithoutPhoto", sqlCon);
                                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                                    sqlCmd2.Parameters.AddWithValue("@Number", value3);
                                    sqlCmd2.Parameters.AddWithValue("@About_seasone", AboutSeasone1.Text.Trim());
                                    sqlCmd2.Parameters.AddWithValue("@ID_Serials", IDSER());

                                    //sqlCmd2.ExecuteNonQuery();
                                    
                                    int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                    SeasoneNum.SetValue("currentCustomerName", IDSEASONE);
                                }
                                else
                                {
                                    //SqlCommand sqlCmd1 = new SqlCommand("SERIALSADD", sqlCon);
                                    //sqlCmd1.CommandType = CommandType.StoredProcedure;
                                    //sqlCmd1.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                    //sqlCmd1.Parameters.AddWithValue("@Year_of_Start", StartDate.SelectedDate);
                                    //sqlCmd1.Parameters.AddWithValue("@Year_of_End", EndDate.SelectedDate);
                                    //sqlCmd1.Parameters.AddWithValue("@About_serials", AboutSerials.Text.Trim());

                                    //sqlCmd1.ExecuteNonQuery();
                                    MessageBox.Show("Пока не сделала функцию");
                                }
                                MessageBox.Show("Новый сезон добавлен");
                                #region SeriesControl
                                try
                                {
                                    if (sqlCon.State == ConnectionState.Closed)
                                        sqlCon.Open();
                                    string query1 = "SELECT COUNT(1) FROM Series WHERE Number_Series=@Number AND ID_Seasone=@ID;";

                                    SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                                    sqlCmd1.CommandType = CommandType.Text;


                                    int value1 = Convert.ToInt32(NumberSeries.Text);

                                    sqlCmd1.Parameters.AddWithValue("@Number", value1);

                                    int ii = SeasoneNum.GetValue<int>("currentCustomerName");
                                    /////////////////////////
                                    sqlCmd1.Parameters.AddWithValue("@ID", ii);
                                    /////////////////////////


                                    int count1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                                    if (count1 == 1)
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
                                            string queryS1 = "SELECT COUNT(1) FROM Series WHERE ID_Seasone=@ID;";

                                            SqlCommand sqlCmd2 = new SqlCommand(queryS1, sqlCon);

                                            sqlCmd2.CommandType = CommandType.Text;
                                            sqlCmd2.Parameters.AddWithValue("@ID", ii);

                                            int value4 = Convert.ToInt32(NumberSeries.Text);

                                            int countS11 = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                            int o = value4 - 1;
                                            if (countS11 == o)
                                            {


                                                
                                                //sqlCon.Open();

                                                SqlCommand sqlCmd6 = new SqlCommand("SERIESADD", sqlCon);
                                                sqlCmd6.CommandType = CommandType.StoredProcedure;
                                                sqlCmd6.Parameters.AddWithValue("@Number", value4);
                                                sqlCmd6.Parameters.AddWithValue("@Name", NameSeries.Text.Trim());
                                                sqlCmd6.Parameters.AddWithValue("@ID_Seasone", ii);

                                                sqlCmd6.ExecuteNonQuery();

                                                //int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                                //ID_SEASONE.SetValue("currentCustomerName", IDSEASONE);

                                                //SeasoneNum.SetValue("currentCustomerName", NumberSeries.Text);

                                                //////////////////////////////////////
                                               
                                               
                                                MessageBox.Show("Серия к новому сезону добавлена");

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
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("Вы пропустили сезон");
                                if (bordernumbersesone != null)
                                {
                                    bordernumbersesone.Background = Brushes.Red;
                                }
                                if (NOTcorrect != null)
                                {
                                    NOTcorrect.Opacity = 1;
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
            #endregion

            #region ForDataAndSeasoneAndSeries

            else if(EndDate.SelectedDate != null && AboutSeasone1.IsEnabled == true)
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);

                try
                {
                    MessageBox.Show("rgthj");
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "update SERIALS set Year_of_End = @EndDat where  ID_Serials=@ID;";

                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;

                    sqlCmd.Parameters.AddWithValue("@EndDat", EndDate.SelectedDate);
                    sqlCmd.Parameters.AddWithValue("@ID", IDSER());

                    SqlDataAdapter dataadp = new SqlDataAdapter(sqlCmd);
                    DataTable db = new DataTable("SERIALS");
                    dataadp.Fill(db);
                    dataadp.Update(db);

                    MessageBox.Show("Дата обнавлена");

                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query10 = "SELECT COUNT(1) FROM Seasone WHERE Number_seasone=@Number AND ID_Serials=@ID;";

                        SqlCommand sqlCmd12 = new SqlCommand(query10, sqlCon);
                        sqlCmd12.CommandType = CommandType.Text;


                        int value3 = Convert.ToInt32(SeasoneNumber.Text);

                        sqlCmd12.Parameters.AddWithValue("@Number", value3);
                        sqlCmd12.Parameters.AddWithValue("@ID", IDSER());



                        int count = Convert.ToInt32(sqlCmd12.ExecuteScalar());
                        if (count == 1)
                        {

                            if (bordernumbersesone != null)
                            {
                                bordernumbersesone.Background = Brushes.Red;
                            }
                            if (NOTcorrect != null)
                            {
                                NOTcorrect.Opacity = 1;
                            }
                            MessageBox.Show("Сезон уже есть в базе");

                            try
                            {
                                if (sqlCon.State == ConnectionState.Closed)
                                    sqlCon.Open();
                                string query1 = "SELECT COUNT(1) FROM Series WHERE Number_Series=@Number AND ID_Seasone=@ID;";

                                SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                                sqlCmd1.CommandType = CommandType.Text;


                                int value1 = Convert.ToInt32(NumberSeries.Text);

                                sqlCmd1.Parameters.AddWithValue("@Number", value1);

                                int ii = SeasoneNum.GetValue<int>("currentCustomerName");
                                /////////////////////////
                                sqlCmd1.Parameters.AddWithValue("@ID", ii);
                                /////////////////////////


                                int count1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                                if (count1 == 1)
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
                                        string queryS1 = "SELECT COUNT(1) FROM Series WHERE ID_Seasone=@ID;";

                                        SqlCommand sqlCmd2 = new SqlCommand(queryS1, sqlCon);

                                        sqlCmd2.CommandType = CommandType.Text;
                                        sqlCmd2.Parameters.AddWithValue("@ID", ii);

                                        int value4 = Convert.ToInt32(NumberSeries.Text);

                                        int countS11 = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                        int o = value4 - 1;
                                        if (countS11 == o)
                                        {


                                            
                                            //sqlCon.Open();

                                            SqlCommand sqlCmd6 = new SqlCommand("SERIESADD", sqlCon);
                                            sqlCmd6.CommandType = CommandType.StoredProcedure;
                                            sqlCmd6.Parameters.AddWithValue("@Number", value4);
                                            sqlCmd6.Parameters.AddWithValue("@Name", NameSeries.Text.Trim());
                                            sqlCmd6.Parameters.AddWithValue("@ID_Seasone", ii);

                                            sqlCmd6.ExecuteNonQuery();

                                            //int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                            //ID_SEASONE.SetValue("currentCustomerName", IDSEASONE);

                                            //SeasoneNum.SetValue("currentCustomerName", NumberSeries.Text);

                                            //////////////////////////////////////
                                            MessageBox.Show("Серия  добавлена");
                                            
                                            

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
                        }
                        else
                        {
                            try
                            {
                                if (sqlCon.State == ConnectionState.Closed)
                                    sqlCon.Open();
                                string querySS = "SELECT COUNT(1) FROM Seasone WHERE ID_Serials=@ID;";

                                SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);
                                sqlCmd5.CommandType = CommandType.Text;
                                sqlCmd5.Parameters.AddWithValue("@ID", IDSER());

                                int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                                int p = value3 - 1;
                                if (EndDate.SelectedDate != null)
                                {
                                    countS = countS + 1;

                                    if (countS == p)
                                    {


                                        //sqlCon.Open();
                                        if (PhotoSeasone.Source == null)
                                        {
                                            SqlCommand sqlCmd2 = new SqlCommand("SEASONEADDwithoutPhoto", sqlCon);
                                            sqlCmd2.CommandType = CommandType.StoredProcedure;
                                            sqlCmd2.Parameters.AddWithValue("@Number", value3);
                                            sqlCmd2.Parameters.AddWithValue("@About_seasone", AboutSeasone1.Text.Trim());
                                            sqlCmd2.Parameters.AddWithValue("@ID_Serials", IDSER());

                                            //sqlCmd2.ExecuteNonQuery();

                                            int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                            SeasoneNum.SetValue("currentCustomerName", IDSEASONE);
                                        }
                                        else
                                        {
                                            //SqlCommand sqlCmd1 = new SqlCommand("SERIALSADD", sqlCon);
                                            //sqlCmd1.CommandType = CommandType.StoredProcedure;
                                            //sqlCmd1.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                            //sqlCmd1.Parameters.AddWithValue("@Year_of_Start", StartDate.SelectedDate);
                                            //sqlCmd1.Parameters.AddWithValue("@Year_of_End", EndDate.SelectedDate);
                                            //sqlCmd1.Parameters.AddWithValue("@About_serials", AboutSerials.Text.Trim());

                                            //sqlCmd1.ExecuteNonQuery();
                                            MessageBox.Show("Пока не сделала функцию");
                                        }
                                        MessageBox.Show("Новый сезон добавлен");
                                        #region SeriesControl
                                        try
                                        {
                                            if (sqlCon.State == ConnectionState.Closed)
                                                sqlCon.Open();
                                            string query1 = "SELECT COUNT(1) FROM Series WHERE Number_Series=@Number AND ID_Seasone=@ID;";

                                            SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                                            sqlCmd1.CommandType = CommandType.Text;


                                            int value1 = Convert.ToInt32(NumberSeries.Text);

                                            sqlCmd1.Parameters.AddWithValue("@Number", value1);

                                            int ii = SeasoneNum.GetValue<int>("currentCustomerName");
                                            /////////////////////////
                                            sqlCmd1.Parameters.AddWithValue("@ID", ii);
                                            /////////////////////////


                                            int count1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                                            if (count1 == 1)
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
                                                    string queryS1 = "SELECT COUNT(1) FROM Series WHERE ID_Seasone=@ID;";

                                                    SqlCommand sqlCmd2 = new SqlCommand(queryS1, sqlCon);

                                                    sqlCmd2.CommandType = CommandType.Text;
                                                    sqlCmd2.Parameters.AddWithValue("@ID", ii);

                                                    int value4 = Convert.ToInt32(NumberSeries.Text);

                                                    int countS11 = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                                                    int o = value4 - 1;
                                                    if (countS11 == o)
                                                    {


                                                        //sqlCon.Open();

                                                        SqlCommand sqlCmd6 = new SqlCommand("SERIESADD", sqlCon);
                                                        sqlCmd6.CommandType = CommandType.StoredProcedure;
                                                        sqlCmd6.Parameters.AddWithValue("@Number", value4);
                                                        sqlCmd6.Parameters.AddWithValue("@Name", NameSeries.Text.Trim());
                                                        sqlCmd6.Parameters.AddWithValue("@ID_Seasone", ii);

                                                        sqlCmd6.ExecuteNonQuery();

                                                        //int IDSEASONE = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                                        //ID_SEASONE.SetValue("currentCustomerName", IDSEASONE);

                                                        //SeasoneNum.SetValue("currentCustomerName", NumberSeries.Text);

                                                        //////////////////////////////////////
                                                        MessageBox.Show("Серия к новому сезону добавлена");



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
                                        #endregion
                                    }

                                    else
                                    {
                                        MessageBox.Show("Сериал закрыт, больше сезоны низзя добавлять");
                                        if (bordernumbersesone != null)
                                        {
                                            bordernumbersesone.Background = Brushes.Red;
                                        }
                                        if (NOTcorrect != null)
                                        {
                                            NOTcorrect.Opacity = 1;
                                        }

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

            #endregion
        }
        public static class SeasoneNum
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
    }

        
        
    }
    

