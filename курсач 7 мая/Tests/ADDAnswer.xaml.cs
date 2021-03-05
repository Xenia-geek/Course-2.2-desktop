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
using static курсач_7_мая.Tests.ADDQuestion;

namespace курсач_7_мая.Tests
{
    /// <summary>
    /// Логика взаимодействия для ADDAnswer.xaml
    /// </summary>
    public partial class ADDAnswer : Window
    {
        public ADDAnswer()
        {
            InitializeComponent();
            TEXTQUEST();
            IDQUEST();
            QUE_NUM();
        }
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        private string TEXTQUEST()
        {
            string us;
            TextofQuest.Content = QUEST_TEXT.GetValue<string>("currentCustomerName");
            us = QUEST_TEXT.GetValue<string>("currentCustomerName");
            return us;

        }
        private int IDQUEST()
        {
            int us;
            us = ID_QUEST.GetValue<int>("currentCustomerName");
            return us;

        }
        private int QUE_NUM()
        {
            int us;
            us = QUESTION_NUMBER.GetValue<int>("currentCustomerName");
            return us;

        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Name.Text = "";
        }
        public byte righ;
        private void Name_TextChange(object sender, TextChangedEventArgs e)
        {
            if (Name.Text != "верно" && Name.Text != "неверно")
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
            else if(Name.Text == "верно" || Name.Text == "неверно")
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string querySS = "SELECT COUNT(1) FROM Answer WHERE IS_Right=1 and ID_Question=@IDQQ;";

                SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);
                sqlCmd5.CommandType = CommandType.Text;
                sqlCmd5.Parameters.AddWithValue("@IDQQ", IDQUEST());

                int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());

                if (countS == 1)
                {
                    if (Name.Text == "верно")
                    {
                        if (bordername != null)
                        {
                            bordername.Background = Brushes.Red;
                            bordername.Opacity = 0.7;
                        }
                        if (NameNOTcorrect != null)
                        {
                            NameNOTcorrect.Opacity = 1;
                        }
                    }
                    if (Name.Text == "неверно")
                    {
                        righ = 0;
                        NameNOTcorrect.Opacity = 0;
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
                else
                {
                    if(Name.Text == "верно")
                    {
                        righ = 1;
                        NameNOTcorrect.Opacity = 0;
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
                    if (Name.Text == "неверно")
                    {
                        righ = 0;
                        NameNOTcorrect.Opacity = 0;
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
                


            }
        }

        private void AboutSeasone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextQuestion.Text = "";
            DateStartNOTcorrect.Opacity = 0;
        }

       

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == null || TextQuestion.Text == null || Name.Text == "Введите: 'верно'/'неверно'" || TextQuestion.Text == "Введите вариант ответа...")
            {
                MessageBox.Show("Введите все данные");
            }
            else if(NameNOTcorrect.Opacity==0 && DateStartNOTcorrect.Opacity==0)
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);

                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "SELECT COUNT(1) FROM Answer WHERE ID_Question=@IDQ AND Answer=@AnswT;";

                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;


                    

                    sqlCmd.Parameters.AddWithValue("@IDQ", IDQUEST());
                    sqlCmd.Parameters.AddWithValue("@AnswT", TextQuestion.Text);
                    


                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {

                        if (DateStartNOTcorrect != null)
                        {
                            DateStartNOTcorrect.Opacity = 1;
                        }
                        MessageBox.Show("Ответ уже есть в базе");
                    }
                    else
                    {
                        try
                        {
                            if (sqlCon.State == ConnectionState.Closed)
                                sqlCon.Open();
                            string querySS = "SELECT COUNT(1) FROM Answer WHERE ID_Question=@IDQQUEST;";

                            SqlCommand sqlCmd5 = new SqlCommand(querySS, sqlCon);
                            sqlCmd5.CommandType = CommandType.Text;
                            sqlCmd5.Parameters.AddWithValue("@IDQQUEST", IDQUEST());

                            int countS = Convert.ToInt32(sqlCmd5.ExecuteScalar());
                            int an = Convert.ToInt32(NumbAnsw.Text);
                            int p = an - 1;

                            if (countS == p)
                            {
                                if (sqlCon.State == ConnectionState.Closed)
                                    sqlCon.Open();
                                SqlCommand sqlCmd2 = new SqlCommand("AnswerADD", sqlCon);
                                sqlCmd2.CommandType = CommandType.StoredProcedure;
                                sqlCmd2.Parameters.AddWithValue("@IDQUEST", IDQUEST());
                                sqlCmd2.Parameters.AddWithValue("@TextQuestion", TextQuestion.Text.Trim());
                                sqlCmd2.Parameters.AddWithValue("@Name", righ);

                                //sqlCmd2.ExecuteNonQuery();
                                int IDANSWW = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                ISANSW.SetValue("currentCustomerName", IDANSWW);


                                if (Convert.ToInt32(NumbAnsw.Text) >= 5)
                                {
                                    if (QUE_NUM() >= 5)
                                    {
                                        Close();
                                    }
                                    else
                                    {
                                        ADDQuestion q = new ADDQuestion();
                                        Close();
                                        q.Show();
                                    }
                                }
                                else
                                {
                                    ADDAnswer a = new ADDAnswer();
                                    Close();
                                    a.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Вы пропустили ответ");
                                
                                if (DateStartNOTcorrect != null)
                                {
                                    DateStartNOTcorrect.Opacity = 1;
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
            else if (AnswNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введен номер ответа");
            }
            else if (NameNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введена оценка правильности");
            }
            else if (DateStartNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введен ответ");
            }
        }

    
       

        private void NumbAnsw_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NumbAnsw.Text = "";
        }

        private void NumbAnsw_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(NumbAnsw.Text, @"^[0-9]+$").Success || Convert.ToInt32(NumbAnsw.Text) > 5)
            {
                if (borderansw != null)
                {
                    borderansw.Background = Brushes.Red;
                }
                if (AnswNOTcorrect != null)
                {
                    AnswNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (borderansw != null)
                {
                    borderansw.Background = Brushes.HotPink;
                    borderansw.Opacity = 0.7;
                }
                if (AnswNOTcorrect != null)
                {
                    AnswNOTcorrect.Opacity = 0;
                }

            }
        }
        public static class ISANSW
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
