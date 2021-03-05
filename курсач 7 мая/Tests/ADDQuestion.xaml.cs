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
using static курсач_7_мая.Tests.ADDTest;

namespace курсач_7_мая.Tests
{
    /// <summary>
    /// Логика взаимодействия для ADDQuestion.xaml
    /// </summary>
    public partial class ADDQuestion : Window
    {
        public ADDQuestion()
        {
            InitializeComponent();
            NameofTeam();
            NumbofSeas();
        }

        private void AboutSeasone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private string NameofTeam()
        {
            string us;
            NameSer.Content = NameTeam.GetValue<string>("currentCustomerName");
            us = NameTeam.GetValue<string>("currentCustomerName");
            return us;

        }
        private int NumbofSeas()
        {
            int us;
            SeasNumb.Content = Convert.ToString(Numb_Seas.GetValue<int>("currentCustomerName"))+" сезон";
            us = Numb_Seas.GetValue<int>("currentCustomerName");
            return us;

        }
        private int IDTESTS()
        {
            int us;
            us = ID_test.GetValue<int>("currentCustomerName") ; 
            return us;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(Name.Text, @"^[0-9]+$").Success || Convert.ToInt32(Name.Text) > 5)
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

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Name.Text = "";

        }

        private void AboutSeasone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextQuestion.Text = "";
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";


        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == null || TextQuestion.Text == null || Name.Text == "Введите номер вопроса..." || TextQuestion.Text == "Введите формулировку вопроса...")
            {
                MessageBox.Show("Введите все данные");
            }

            else if (NameNOTcorrect.Opacity == 0 && DateStartNOTcorrect.Opacity == 0)
            {

                SqlConnection sqlCon = new SqlConnection(connectionString);

                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "SELECT COUNT(1) FROM Questions WHERE ID_Test=@IDTESTT AND Number_of_Question=@NUMBQUEST;";

                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;


                    int value3 = Convert.ToInt32(Name.Text);

                    sqlCmd.Parameters.AddWithValue("@NUMBQUEST", value3);
                    sqlCmd.Parameters.AddWithValue("@IDTESTT", IDTESTS());
                    QUESTION_NUMBER.SetValue("currentCustomerName", value3);



                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {

                        if (bordername != null)
                        {
                            bordername.Background = Brushes.Red;
                        }
                        if (NameNOTcorrect != null)
                        {
                            NameNOTcorrect.Opacity = 1;
                        }
                        MessageBox.Show("Вопрос уже есть в базе");
                    }
                    else
                    {
                        try
                        {
                            if (sqlCon.State == ConnectionState.Closed)
                                sqlCon.Open();
                            string querySS = "SELECT COUNT(1) FROM Questions WHERE ID_Test=@IDTESTT;";

                            SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);
                            sqlCmd5.CommandType = CommandType.Text;
                            sqlCmd5.Parameters.AddWithValue("@IDTESTT", IDTESTS());

                            int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                            int p = value3 - 1;

                            if (countS == p)
                            {

                                SqlConnection sqlCon2 = new SqlConnection(connectionString);

                                sqlCon2.Open();
                                
                                    SqlCommand sqlCmd2 = new SqlCommand("QuestionADD", sqlCon2);
                                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                                    sqlCmd2.Parameters.AddWithValue("@Name", value3);
                                    sqlCmd2.Parameters.AddWithValue("@TextQuestion", TextQuestion.Text.Trim());
                                    sqlCmd2.Parameters.AddWithValue("@IDTESTS", IDTESTS());

                                    //sqlCmd2.ExecuteNonQuery();
                                    int IDQUESTION = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                    ID_QUEST.SetValue("currentCustomerName", IDQUESTION);
                                    string s = TextQuestion.Text;
                                    QUEST_TEXT.SetValue("currentCustomerName", s);



                                ADDAnswer answ = new ADDAnswer();
                                Close();
                                answ.Show();
                            }
                            else
                            {
                                MessageBox.Show("Вы пропустили вопрос");
                                if (bordername != null)
                                {
                                    bordername.Background = Brushes.Red;
                                }
                                if (NameNOTcorrect != null)
                                {
                                    NameNOTcorrect.Opacity = 1;
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
            else if (NameNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введен номер вопроса");
            }
            else if (DateStartNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введено вопрос");
            }
        }
        public static class ID_QUEST
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
        public static class QUEST_TEXT
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
        public static class QUESTION_NUMBER
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
