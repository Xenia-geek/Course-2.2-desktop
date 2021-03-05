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

namespace курсач_7_мая.secondary_pages
{
    /// <summary>
    /// Логика взаимодействия для ADDmyserials.xaml
    /// </summary>
    public partial class ADDmyserials : Window
    {
        public ADDmyserials()
        {
            InitializeComponent();
            ToComboBox_Serial();
        }

        
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        string name;
        string f;
        DateTime year;
        string n_y;
        int IDAct;
        int ID_SEAS;
        DateTime r;

        public void ToComboBox_Serial()
        {
            SqlConnection con = new SqlConnection(@"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;");
            
            string sql = "select distinct  Name, Year_of_Start from SERIALS  ORDER BY Name;";
            
            SqlCommand cmd = new SqlCommand(sql, con);
            
            SqlDataReader myreader;
            try
            {
               
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
            
                    name = myreader.GetString(0);
                    
                    year = myreader.GetDateTime(1);
                    n_y = name + "|" + year.ToString("dd/MM/yyyy") + "";
                    Serialss.Items.Add(n_y);
                }
               
            }
            catch (Exception ex)
            {
               

                MessageBox.Show(ex.Message);
            }
        }

        #region ForSeasoneComboBox
        private void Serialsss_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (Serialss.SelectedItem == null)
            {
                MessageBox.Show("Выберите название");
            }

            else
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        Serialsss.Items.Clear();


                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        string sql1 = "select top(1) ID_Serials from SERIALS where Name=@Name and Year_of_Start=@Year_of_start;";
                        SqlCommand cmd1 = new SqlCommand(sql1, con);
                        cmd1.CommandType = CommandType.Text;

                        String s = Serialss.Text;
                        String[] words = s.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);



                        cmd1.Parameters.AddWithValue("@Name", words[0]);

                        r = Convert.ToDateTime(words[1]);

                        cmd1.Parameters.AddWithValue("@Year_of_start", r);
                        
                        IDAct = Convert.ToInt32(cmd1.ExecuteScalar());
                        

                        try
                        {

                            if (con.State == ConnectionState.Closed)
                                con.Open();

                            string query1 = "SELECT * FROM Seasone WHERE ID_Serials=@IDSER;";
                            SqlCommand sqlCmd1 = new SqlCommand(query1, con);

                            
                            sqlCmd1.Parameters.AddWithValue("@IDSER", IDAct);
                            

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
        }
        #endregion

        #region ForSeriesComboBox
        int IDActf;

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
                        int IDSEAS;
                        Serialssss.Items.Clear();

                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        string sql1 = "select top(1) ID_Serials from SERIALS where Name=@Name and Year_of_Start=@Year_of_start;";
                        SqlCommand cmd1 = new SqlCommand(sql1, con);
                        cmd1.CommandType = CommandType.Text;

                        String s = Serialss.Text;
                        String[] words = s.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        cmd1.Parameters.AddWithValue("@Name", words[0]);

                        r = Convert.ToDateTime(words[1]);

                        cmd1.Parameters.AddWithValue("@Year_of_start", r);

                        IDActf = Convert.ToInt32(cmd1.ExecuteScalar());
                        
                        IDSetialss.SetValue("currentCustomerName", IDActf);


                        try
                        {


                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            string sql2 = "select top(1) ID_Seasone from Seasone where ID_Serials=@IDAct and Number_seasone=@Numb;";
                            SqlCommand cmd2 = new SqlCommand(sql2, con);
                            cmd2.CommandType = CommandType.Text;


                            cmd2.Parameters.AddWithValue("@IDAct", IDActf);

                           
                            int d = Convert.ToInt32(Serialsss.Text);
                           
                            cmd2.Parameters.AddWithValue("@Numb",d);
                            

                            IDSEAS = Convert.ToInt32(cmd2.ExecuteScalar());
                            


                            try
                            {

                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                string query1 = "SELECT * FROM Series WHERE ID_Seasone=@IDSEAS;";
                                SqlCommand sqlCmd1 = new SqlCommand(query1, con);

                                sqlCmd1.Parameters.AddWithValue("@IDSEAS", IDSEAS);
                               
                                SqlDataReader myreader2;

                                if (Serialssss.Items.Count == 0)
                                {
                                    
                                    try
                                    {
                                        myreader2 = sqlCmd1.ExecuteReader();
                                        while (myreader2.Read())
                                        {
                                            int NUMBSEASS = myreader2.GetInt32(2);
                                            Serialssss.Items.Add(NUMBSEASS);

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

        /// ////////////////////////////////////////////////////////////////

        #region ContinueButton
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (Serialss.SelectedItem == null || Serialsss.SelectedItem==null || Serialssss.SelectedItem==null)
            {
                MessageBox.Show("Введите все данные");
            }
            


            else if (ActorNOTcorrect.Opacity == 0 && SeasoneNOTcorrect.Opacity==0 && SeriesNOTcorrect.Opacity==0)
            {
                using (SqlConnection con1 = new SqlConnection(connectionString))
                {
                    try
                    {
                        //Serialsss.Items.Clear();


                        if (con1.State == ConnectionState.Closed)
                            con1.Open();
                        string sql1 = "select top(1) ID_Serials from SERIALS where Name=@Name and Year_of_Start=@Year_of_start;";
                        SqlCommand cmd1 = new SqlCommand(sql1, con1);
                        cmd1.CommandType = CommandType.Text;
                       
                        String s = Serialss.Text;
                        String[] words = s.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                       

                        cmd1.Parameters.AddWithValue("@Name", words[0]);
                        
                        r = Convert.ToDateTime(words[1]);
                        
                        cmd1.Parameters.AddWithValue("@Year_of_start", r);
                       
                        IDAct = Convert.ToInt32(cmd1.ExecuteScalar());
                        
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            try
                            {
                                int IDSEASONEEEES;


                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                string query1 = "SELECT COUNT(1) FROM Serials_for_Users WHERE ID_Serials=@IDSER and LoginUser=@Avtoriz;";

                                SqlCommand sqlCmd1 = new SqlCommand(query1, con);
                                sqlCmd1.CommandType = CommandType.Text;

                                
                                sqlCmd1.Parameters.AddWithValue("@IDSER", IDAct);
                                sqlCmd1.Parameters.AddWithValue("@Avtoriz", Avtorizacia());

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
                                    MessageBox.Show("Сериал уже есть в базе");
                                }
                                else
                                {
                                    if (con.State == ConnectionState.Closed)
                                        con.Open();
                                    string query6 = "SELECT top(1) ID_Seasone FROM Seasone  WHERE Number_seasone=@SeasoneS and  ID_Serials=@IDSER;";

                                    SqlCommand sqlCmd9 = new SqlCommand(query6, con);
                                    sqlCmd9.CommandType = CommandType.Text;

                                    
                                    sqlCmd9.Parameters.AddWithValue("@IDSER", IDAct);
                                   
                                    int seasone = Convert.ToInt32(Serialsss.Text);
                                    
                                    sqlCmd9.Parameters.AddWithValue("@SeasoneS", seasone);
                                    

                                    IDSEASONEEEES = Convert.ToInt32(sqlCmd9.ExecuteScalar());
                                    /////series/////
                                    

                                    string query7 = "SELECT top(1) ID_Series FROM Series WHERE  Number_Series=@SeriesS and ID_Seasone=@IDSESON;";

                                    SqlCommand sqlCmd8 = new SqlCommand(query7, con);
                                    sqlCmd8.CommandType = CommandType.Text;

                                   
                                    sqlCmd8.Parameters.AddWithValue("@IDSESON", IDSEASONEEEES);
                                    
                                    int series = Convert.ToInt32(Serialssss.Text);
                                    
                                    sqlCmd8.Parameters.AddWithValue("@SeriesS", series);
                                    
                                    int IDSERIIIIES = Convert.ToInt32(sqlCmd8.ExecuteScalar());

                                    
                                    using (SqlConnection sqlCon1 = new SqlConnection(connectionString))
                                    {
                                        try
                                        {
                                            sqlCon1.Open();

                                            SqlCommand sqlCmd2 = new SqlCommand("ADDMYLIST", sqlCon1);

                                            sqlCmd2.CommandType = CommandType.StoredProcedure;
                                            
                                            sqlCmd2.Parameters.AddWithValue("@LoginUser", Avtorizacia());
                                            
                                            sqlCmd2.Parameters.AddWithValue("@IDSerials", IDAct);
                                            
                                            sqlCmd2.Parameters.AddWithValue("@NUM_SERIA", IDSERIIIIES);
                                            
                                            sqlCmd2.Parameters.AddWithValue("@NUM_SESON", IDSEASONEEEES);
                                           
                                            sqlCmd2.ExecuteNonQuery();
                                            ///////////////////////////////

                                            Close();

                                            /////////////////////////////////////


                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
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

                    finally
                    {
                        con1.Close();
                    }



                }

            }
        }
        #endregion

        private void Serialss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Serialss.SelectedItem == null)
            {
                if (ActorNOTcorrect != null)
                {
                    ActorNOTcorrect.Opacity = 1;
                }
                if (borderlogin != null)
                {
                    borderlogin.Background = Brushes.Red;
                }
            }
            else
            {
                if (ActorNOTcorrect != null)
                {
                    ActorNOTcorrect.Opacity = 0;
                }
                if (borderlogin != null)
                {
                    borderlogin.Background = null;
                }
            }
        }

        private void Serialsss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Serialsss.SelectedItem == null)
            {
                if (SeasoneNOTcorrect != null)
                {
                    SeasoneNOTcorrect.Opacity = 1;
                }
                if (borderlogin1 != null)
                {
                    borderlogin1.Background = Brushes.Red;
                }
            }
            else
            {
                if (SeasoneNOTcorrect != null)
                {
                    SeasoneNOTcorrect.Opacity = 0;
                }
                if (borderlogin1 != null)
                {
                    borderlogin1.Background = null;
                }
            }
        }

        private void Serialssss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Serialssss.SelectedItem == null)
            {
                if (SeriesNOTcorrect != null)
                {
                    SeriesNOTcorrect.Opacity = 1;
                }
                if (borderlogin11 != null)
                {
                    borderlogin11.Background = Brushes.Red;
                }
            }
            else
            {
                if (SeriesNOTcorrect != null)
                {
                    SeriesNOTcorrect.Opacity = 0;
                }
                if (borderlogin11 != null)
                {
                    borderlogin11.Background = null;
                }
            }
        }


        private string Avtorizacia()
        {
            string us;
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }
        public static class IDSetialss
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

        public static class IDSeasones
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

        public static class IDSeriess
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

        #region MoreSerialsToMyList

        private void Continue3_Click(object sender, RoutedEventArgs e)
        {
            if (Serialss.SelectedItem == null || Serialsss.SelectedItem == null || Serialssss.SelectedItem == null)
            {
                MessageBox.Show("Введите все данные");
            }



            else if (ActorNOTcorrect.Opacity == 0 && SeasoneNOTcorrect.Opacity == 0 && SeriesNOTcorrect.Opacity == 0)
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query1 = "SELECT COUNT(1) FROM Serials_for_Users  WHERE ID_Serials=@IDSER and LoginUser=@Avtoriz;";

                        SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                        sqlCmd1.CommandType = CommandType.Text;

                        int us = IDSetialss.GetValue<int>("currentCustomerName");
                        sqlCmd1.Parameters.AddWithValue("@IDSER", IDActf);
                        sqlCmd1.Parameters.AddWithValue("@Avtoriz", Avtorizacia());

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
                            MessageBox.Show("Сериал уже есть в базе");
                        }
                        else
                        {
                            using (SqlConnection sqlCon1 = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    sqlCon1.Open();

                                    SqlCommand sqlCmd2 = new SqlCommand("ADDMYLIST", sqlCon1);

                                    sqlCmd2.CommandType = CommandType.StoredProcedure;

                                    sqlCmd2.Parameters.AddWithValue("@LoginUser", Avtorizacia());

                                    int us1 = IDSetialss.GetValue<int>("currentCustomerName");
                                    sqlCmd2.Parameters.AddWithValue("@IDSerials", us1);

                                    int us2 = IDSeriess.GetValue<int>("currentCustomerName");
                                    sqlCmd2.Parameters.AddWithValue("@NUM_SERIA", us2);

                                    int us3 = IDSeasones.GetValue<int>("currentCustomerName");
                                    sqlCmd2.Parameters.AddWithValue("@NUM_SESON", us3);
                                    sqlCmd2.ExecuteNonQuery();
                                    ///////////////////////////////

                                    Close();

                                    /////////////////////////////////////


                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
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

            }
        }
            #endregion
        }

}
