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
using static курсач_7_мая.Tests.testtslist;

namespace курсач_7_мая.Tests
{
    /// <summary>
    /// Логика взаимодействия для TestsForUsers.xaml
    /// </summary>
    public partial class TestsForUsers : Window
    {
        public TestsForUsers()
        {
            InitializeComponent();
            LoadData();
        }
        int COUNTRIGHT=0;
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if(SeasoneSS.SelectedItem==null || AnswerSS.SelectedItem==null)
            {
                MessageBox.Show("что ты хочешь блин сохранить? ты ж ничего не выбрал");
            }
            else
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                try
                {
                    sqlCon.Open();
                    string Query = "select IS_Right from Answer where  ID_Question=(select ID_Questions from Questions where ID_Test=@tes and Question=@queest) and Answer=@answ;";

                    SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                    createCommand.CommandType = CommandType.Text;


                    createCommand.Parameters.AddWithValue("@tes", IDSIDTEESTTER());
                    createCommand.Parameters.AddWithValue("@queest", SeasoneSS.Text);
                    createCommand.Parameters.AddWithValue("@answ", AnswerSS.Text);

                    int count = Convert.ToInt32(createCommand.ExecuteScalar());
                    if(count==1)
                    {
                        COUNTRIGHT = COUNTRIGHT + 1;
                    }
                    MessageBox.Show("Ответ принят, продолжайте))");
                    string nowques = SeasoneSS.Text;
                    SeasoneSS.Items.Remove(nowques);
                    string nowansw = AnswerSS.Text;
                    AnswerSS.Items.Remove(nowansw);
                   
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
            if (AnswerSS.Items == null && SeasoneSS.Items == null)
            {
                MessageBox.Show("Нажмите на 'итог', чтобы узнать результаты");
            }



        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Que_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Load_Quest()
        {
            AnswerSS.Items.Clear();
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select Answer from Answer where  ID_Question=(select top(1) ID_Questions from Questions where ID_Test=@TTST and Question=@QQUESS);";

                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.CommandType = CommandType.Text;


                createCommand.Parameters.AddWithValue("@TTST", IDSIDTEESTTER());
                createCommand.Parameters.AddWithValue("@QQUESS", Convert.ToString(SeasoneSS.SelectedItem));


                SqlDataReader myreader1;
                
                    try
                    {
                   
                    myreader1 = createCommand.ExecuteReader();
                        while (myreader1.Read())
                        {

                            string NUMBSEAS = myreader1.GetString(0);
                            AnswerSS.Items.Add(NUMBSEAS);

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

        private int IDSIDTEESTTER()
        {

            int us;
            us = IDTEEST.GetValue<int>("currentCustomerName");

            return us;

        }

        int QUESID;
        private void LoadData()
        {
            IDSIDTEESTTER();
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();

                string Query1 = "select tm.Name, s.Number_seasone, q.Question from Test t join Team tm on t.ID_Team=tm.ID_Team join Seasone s on tm.ID_Seasone=s.ID_Seasone join Questions q on t.ID_Test=q.ID_Test  where t.ID_Test=@TESSTIID;";
                

                SqlCommand createCommand1 = new SqlCommand(Query1, sqlCon);
                createCommand1.CommandType = CommandType.Text;
               
                createCommand1.Parameters.AddWithValue("@TESSTIID", IDSIDTEESTTER());
                

                SqlDataReader myreader1;
                
                try
                {
                    
                    myreader1 = createCommand1.ExecuteReader();
                   
                    while (myreader1.Read())
                    {
                        //AnswerSS.Items.Clear();
                        string Name = myreader1.GetString(0);
                        Team.Content = Name;
                        int NUMBSEAS = myreader1.GetInt32(1);
                        Seasone.Content = Convert.ToString(NUMBSEAS)+" сезон";
                        string NUMQUE = myreader1.GetString(2);
                        SeasoneSS.Items.Add(NUMQUE);
                        
                        
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

        private void SeasoneSS_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //AnswerSS.Items.Clear();
        }

        private void SeasoneSS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            Load_Quest();
            

        }

        private void AnswerSS_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(SeasoneSS.SelectedItem==null)
            {
                MessageBox.Show("Вопрос не выбран");
            }
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            if (SeasoneSS.Items.Count == 0)
            {
                byte Resultss;


                using (SqlConnection sqlCon1 = new SqlConnection(connectionString))
                {

                    sqlCon1.Open();

                    if(COUNTRIGHT>=3)
                    {
                         Resultss = 1;
                        MessageBox.Show("Поздравляем, вы прошли тест");
                        
                    }
                    else
                    {
                        Resultss = 0;
                        MessageBox.Show("Увы, но вы провалились");
                    }

                   
                    SqlCommand sqlCmd2 = new SqlCommand("ADDPROGRESSTEST", sqlCon1);
                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                    sqlCmd2.Parameters.AddWithValue("@Login", Avtorizacia().Trim());
                    sqlCmd2.Parameters.AddWithValue("@ID_Test", IDSIDTEESTTER());
                    sqlCmd2.Parameters.AddWithValue("@Data_of_Test", DateTime.Now);
                    sqlCmd2.Parameters.AddWithValue("@IS_Right", Resultss);
                    sqlCmd2.Parameters.AddWithValue("@Count_of_Right_Answer", COUNTRIGHT);

                    
                    TESTISDONE.SetValue("currentCustomerName", Resultss);
                    sqlCmd2.ExecuteNonQuery();
                    Close();

                }
            }
            else
            {
                MessageBox.Show("Еще остались неотвеченные вопросы");
            }
        }
        private string Avtorizacia()
        {
            string us;
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }
        public static class TESTISDONE
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
