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

namespace курсач_7_мая.Tests
{
    /// <summary>
    /// Логика взаимодействия для ADDTest.xaml
    /// </summary>
    public partial class ADDTest : Window
    {
        public ADDTest()
        {
            InitializeComponent();
            ToComboBox_Serial();
        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Name.Text = "";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(Name.Text, @"(^[а-яА-ЯёЁa-zA-Z0-9]+[а-яА-ЯёЁa-zA-Z0-9,.*""-:/ ]*[а-яА-ЯёЁa-zA-Z0-9!?""]+$)").Success || Name.Text.Length > 50)
            {
                if (bordername != null)
                {
                    bordername.Background = Brushes.Red;
                }
                if (NameNOTcorrect != null)
                {
                    NameNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (bordername != null)
                {
                    bordername.Background = Brushes.HotPink;
                    bordername.Opacity = 0.7;
                }
                if (NameNOTcorrect != null)
                {
                    NameNOTcorrect.Opacity = 0;
                }

            }
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
                    TeamSerial.Items.Add(n_y);
                }

            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }
        }

        #region ForSeasoneComboBox
        private void TestSeasone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (TeamSerial.SelectedItem == null)
            {
                MessageBox.Show("Выберите название");
            }

            else
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        TestSeasone.Items.Clear();


                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        string sql1 = "select top(1) ID_Serials from SERIALS where Name=@Name and Year_of_Start=@Year_of_start;";
                        SqlCommand cmd1 = new SqlCommand(sql1, con);
                        cmd1.CommandType = CommandType.Text;

                        String s = TeamSerial.Text;
                        String[] words = s.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        NameTeam.SetValue("currentCustomerName", TeamSerial.Text);

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


                            if (TestSeasone.Items.Count == 0)
                            {

                                SqlDataReader myreader1;
                                try
                                {
                                    myreader1 = sqlCmd1.ExecuteReader();
                                    while (myreader1.Read())
                                    {

                                        int NUMBSEAS = myreader1.GetInt32(2);
                                        TestSeasone.Items.Add(NUMBSEAS);

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

        

        

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == null || Name.Text == "Введите название теста..." || TeamSerial.SelectedItem == null || TestSeasone.SelectedItem == null)
            {
                MessageBox.Show("Введите все данные");
            }



            else if (NameNOTcorrect.Opacity == 0 && DateStartNOTcorrect.Opacity == 0 && TestSeasone.SelectedItem != null)
            {


                SqlConnection con = new SqlConnection(connectionString);
                            try
                            {
                                int IDSEASONEEEES;

                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                string query6 = "SELECT top(1) ID_Seasone FROM Seasone  WHERE Number_seasone=@SeasoneS and  ID_Serials=@IDSER;";

                                
                                SqlCommand sqlCmd9 = new SqlCommand(query6, con);
                                sqlCmd9.CommandType = CommandType.Text;

                                    
                                    sqlCmd9.Parameters.AddWithValue("@IDSER", IDAct);
                                   
                                    int seasone = Convert.ToInt32(TestSeasone.Text);
                                    
                                    Numb_Seas.SetValue("currentCustomerName", seasone);
                                    
                                    sqlCmd9.Parameters.AddWithValue("@SeasoneS", seasone);

                                    
                                    IDSEASONEEEES = Convert.ToInt32(sqlCmd9.ExecuteScalar());
                    try
                    {

                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        string query66 = "SELECT COUNT(1) FROM Test  WHERE   Name=@SeasID;";


                        SqlCommand sqlCmd97 = new SqlCommand(query66, con);
                        sqlCmd97.CommandType = CommandType.Text;


                        sqlCmd97.Parameters.AddWithValue("@SeasID", Name.Text);
                        int count = Convert.ToInt32(sqlCmd97.ExecuteScalar());
                        if (count == 1)
                        {
                            MessageBox.Show("К этому сериалу к этому сезону есть тест, сорри");
                            DateStartNOTcorrect.Opacity = 1;
                        }
                        else
                        {
                            try
                            {
                                if (con.State == ConnectionState.Closed)
                                    con.Open();

                                SqlCommand sqlCmd2 = new SqlCommand("TeamADD", con);

                                sqlCmd2.CommandType = CommandType.StoredProcedure;

                                sqlCmd2.Parameters.AddWithValue("@IDSEASONEES", IDSEASONEEEES);

                                sqlCmd2.Parameters.AddWithValue("@TeamSeasone", TeamSerial.Text);

                                int IDTEAM = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                //sqlCmd2.ExecuteNonQuery();
                                try
                                {
                                    if (con.State == ConnectionState.Closed)
                                        con.Open();

                                    SqlCommand sqlCmd3 = new SqlCommand("TestADD", con);

                                    sqlCmd3.CommandType = CommandType.StoredProcedure;

                                    sqlCmd3.Parameters.AddWithValue("@Name", Name.Text);

                                    sqlCmd3.Parameters.AddWithValue("@t", IDTEAM);
                                    int IDTEST = Convert.ToInt32(sqlCmd3.ExecuteScalar());

                                    ID_test.SetValue("currentCustomerName", IDTEST);


                                    ///////////////////////////////



                                    /////////////////////////////////////

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


                            ADDQuestion que = new ADDQuestion();
                            Close();
                            que.Show();

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
        public static class ID_test
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
        public static class NameTeam
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
        public static class Numb_Seas
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

        private void TeamSerial_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TeamSerial.SelectedItem = null;
            TestSeasone.SelectedItem = null;
        }

        private void TeamSerial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

