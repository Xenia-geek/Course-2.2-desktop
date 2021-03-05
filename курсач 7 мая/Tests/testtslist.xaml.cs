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
using курсач_7_мая.main_pages;
using курсач_7_мая.offers;
using курсач_7_мая.secondary_pages;
using static курсач_7_мая.input;
using static курсач_7_мая.Tests.TestsForUsers;

namespace курсач_7_мая.Tests
{
    /// <summary>
    /// Логика взаимодействия для testtslist.xaml
    /// </summary>
    public partial class testtslist : Window
    {
        public testtslist()
        {
            InitializeComponent();
            Avtorizacia();
            LoadAllSerials();
        }
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";
        DataTable db = new DataTable("Test");
        private void LoadAllSerials()
        {

            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select te.ID_Test[ID], te.Name[Название теста], t.Name[Тема(сериал)], se.Number_seasone[Номер сезона], count(question)[Кол-во вопросов]  from Team t join Test te on t.ID_Team=te.ID_Team join Questions q on q.ID_Test=te.ID_Test join Seasone se on t.ID_Seasone=se.ID_Seasone group by te.ID_Test, te.Name, t.Name, se.Number_seasone ;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                
                dataadp.Fill(db);
                AllSerials.ItemsSource = db.DefaultView;
                dataadp.Update(db);

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string Admin = "Ksyusha8";

                string us = Avtorizacia();
                if (Admin == us)
                {
                    AddAllSerials.Opacity = 0.8;
                    AddAllSerials.IsEnabled = true;
                   
                    DelAllSerials.Opacity = 0.8;
                    DelAllSerials.IsEnabled = true;

                    TestMeAllSerials.Opacity = 0;
                    TestMeAllSerials.IsEnabled = false;

                    Rez.Header = "Замечания предложения";
                    Myserials.Header = "Статистика";


                }
                else
                {
                    AddAllSerials.Opacity = 0;
                    AddAllSerials.IsEnabled = false;
                    
                    DelAllSerials.Opacity = 0;
                    DelAllSerials.IsEnabled = false;

                    TestMeAllSerials.Opacity = 0.8;
                    TestMeAllSerials.IsEnabled = true;

                    

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if(TESTisDONE()==1)
            {
                
            }
           
        }




        private void MySerials_Click(object sender, RoutedEventArgs e)
        {
            string Admin = "Ksyusha8";

            string us = Avtorizacia();
            if (Admin == us)
            {
                AllUsers input = new AllUsers();
                Close();
                input.Show();
            }
            else
            {
                myserials myserials = new myserials();
                Close();
                myserials.Show();
            }
        }
        private void AllSerials_Click(object sender, RoutedEventArgs e)
        {
            allserials allserials = new allserials();
            Close();
            allserials.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ADDTest test = new ADDTest();
            
            test.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            input input = new input();
            Close();
            input.Show();
        }
        private string Avtorizacia()
        {
            string us;
            username.Header = ApplicationState.GetValue<string>("currentCustomerName");
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ACTORS act = new ACTORS();
            Close();
            act.Show();
        }

        private void AllDirectors_Click(object sender, RoutedEventArgs e)
        {
            DIRECTORS dir = new DIRECTORS();
            Close();
            dir.Show();
        }

        private void Search_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Search.Text = "";

        }

        private void ReLoad(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select te.ID_Test[ID], te.Name[Название теста], t.Name[Тема(сериал)], se.Number_seasone[Номер сезона], count(question)[Кол-во вопросов]  from Team t join Test te on t.ID_Team=te.ID_Team join Questions q on q.ID_Test=te.ID_Test join Seasone se on t.ID_Seasone=se.ID_Seasone group by te.ID_Test, te.Name, t.Name, se.Number_seasone ;";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("Test");
                dataadp.Fill(db);
                AllSerials.ItemsSource = db.DefaultView;
                dataadp.Update(db);

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DelAllSerials_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < AllSerials.SelectedItems.Count; i++)
                {
                    DataRowView dataro = AllSerials.SelectedItems[i] as DataRowView;
                    if (dataro != null)
                    {
                        DataRow dataRow = dataro.Row;

                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        try
                        {

                            sqlCon.Open();
                            string Query3 = " declare @s int; declare Te CURSOR for select ID_Questions from Questions where ID_Test=@t; open Te; fetch Te into @s; while @@FETCH_STATUS=0  begin  delete Answer where ID_Question=@s; fetch Te into @s; end; close Te; deallocate Te; delete from Questions where ID_Test=@t; delete from Progress where ID_Test=@t; delete from Test where ID_Test=@t;";

                            SqlCommand createCommand = new SqlCommand(Query3, sqlCon);
                            createCommand.CommandType = CommandType.Text;

                            int g = Convert.ToInt32(dataRow[i]);

                            createCommand.Parameters.AddWithValue("@t", g);

                            SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                            DataTable db = new DataTable("Test");
                            dataadp.Fill(db);

                            dataadp.Update(db);


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

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }



        private void Filtr_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Value.Items.Clear();

        }

        private void Value_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Filtr.SelectedItem == null)
            {
                MessageBox.Show("Выберите категорию");
            }
            else
            {
                
            if (Filtr.Text == "Сезон")
            {
                Value.Items.Clear();
                SqlConnection sqlCon = new SqlConnection(connectionString);
                try
                {
                    sqlCon.Open();
                    string Query = "select distinct Number_seasone from Seasone;";

                    SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                    createCommand.CommandType = CommandType.Text;

                    SqlDataReader myreader1;
                    if (Value.Items.Count == 0)
                    {
                        try
                        {
                            myreader1 = createCommand.ExecuteReader();
                            while (myreader1.Read())
                            {

                                int NUMBSEAS = myreader1.GetInt32(0);
                                Value.Items.Add(Convert.ToString(NUMBSEAS));

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

            if (Filtr.Text == "Сериал")
            {
                Value.Items.Clear();
                SqlConnection sqlCon = new SqlConnection(connectionString);
                try
                {
                    sqlCon.Open();
                    string Query = "select distinct Name, Year_of_Start from SERIALS  ORDER BY Name;";

                    SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                    createCommand.CommandType = CommandType.Text;

                    SqlDataReader myreader1;
                    if (Value.Items.Count == 0)
                    {
                        try
                        {
                            myreader1 = createCommand.ExecuteReader();
                            while (myreader1.Read())
                            {

                                string name = myreader1.GetString(0);
                                DateTime year = myreader1.GetDateTime(1);
                                string n_y = name + "|" + year.ToString("dd/MM/yyyy") + "";
                                Value.Items.Add(n_y);

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
        }

    }

    private void Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SerAllSerials_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (Search.Text != "" || Search.Text != "Поиск по теме...")
            {
                AllSerials.ItemsSource = null;
                try
                {
                    sqlCon.Open();
                    string Query2 = "select te.ID_Test[ID], te.Name[Название теста], t.Name[Тема(сериал)], se.Number_seasone[Номер сезона], count(question)[Кол-во вопросов]  from Team t join Test te on t.ID_Team=te.ID_Team join Questions q on q.ID_Test=te.ID_Test join Seasone se on t.ID_Seasone=se.ID_Seasone  where  t.Name like @NameTeam group by te.ID_Test, te.Name, t.Name, se.Number_seasone ;";

                    SqlCommand createCommand = new SqlCommand(Query2, sqlCon);
                    createCommand.CommandType = CommandType.Text;

                    string nameser = "%" + Search.Text + "%";
                    createCommand.Parameters.AddWithValue("@NameTeam", nameser);

                    SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                    DataTable db = new DataTable("Test");
                    dataadp.Fill(db);
                    AllSerials.ItemsSource = db.DefaultView;
                    dataadp.Update(db);

                    sqlCon.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Введите строку для поиска");
            }
            
                
        }

        private void AllSerials_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FiltrAllSerials_Click(object sender, RoutedEventArgs e)
        {
            if (Filtr.Text == "" || Value.Text == "")
            {
                MessageBox.Show("Заполните поля фильтрации");
            }
            else
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                if (Filtr.Text == "Сериал")
                {

                    AllSerials.ItemsSource = null;
                    try
                    {
                        sqlCon.Open();
                        string Query = "select te.ID_Test[ID], te.Name[Название теста], t.Name[Тема(сериал)], se.Number_seasone[Номер сезона], count(question)[Кол-во вопросов]  from Team t join Test te on t.ID_Team=te.ID_Team join Questions q on q.ID_Test=te.ID_Test join Seasone se on t.ID_Seasone=se.ID_Seasone where t.Name=@NAEM group by te.ID_Test, te.Name, t.Name, se.Number_seasone ;";

                        SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                        createCommand.CommandType = CommandType.Text;


                        createCommand.Parameters.AddWithValue("@NAEM", Value.Text);

                        SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                        DataTable db = new DataTable("Test");
                        dataadp.Fill(db);
                        AllSerials.ItemsSource = db.DefaultView;
                        dataadp.Update(db);

                        sqlCon.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
                if (Filtr.Text == "Сезон")
                {
                    AllSerials.ItemsSource = null;
                    try
                    {
                        sqlCon.Open();
                        string Query = "select te.ID_Test[ID], te.Name[Название теста], t.Name[Тема(сериал)], se.Number_seasone[Номер сезона], count(question)[Кол-во вопросов]  from Team t join Test te on t.ID_Team=te.ID_Team join Questions q on q.ID_Test=te.ID_Test join Seasone se on t.ID_Seasone=se.ID_Seasone where se.Number_seasone=@NUMSEAS group by te.ID_Test, te.Name, t.Name, se.Number_seasone ;";

                        SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                        createCommand.CommandType = CommandType.Text;


                        createCommand.Parameters.AddWithValue("@NUMSEAS", Convert.ToInt32(Value.Text));

                        SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                        DataTable db = new DataTable("Test");
                        dataadp.Fill(db);
                        AllSerials.ItemsSource = db.DefaultView;
                        dataadp.Update(db);

                        sqlCon.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
               

            }
        }

        private void TestME_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < AllSerials.SelectedItems.Count; i++)
                {
                    DataRowView dataro = AllSerials.SelectedItems[i] as DataRowView;
                    if (dataro != null)
                    {
                        DataRow dataRow = dataro.Row;

                        int g = Convert.ToInt32(dataRow[i]);
                        IDTEEST.SetValue("currentCustomerName", g);
                        try
                        {
                            SqlConnection sqlCon = new SqlConnection(connectionString);
                            try
                            {
                                sqlCon.Open();
                                string Query = "select COUNT(1) from Serials_for_Users where  ID_Serials=(select s.ID_Serials from Team t join Test te on t.ID_Team=te.ID_Team  join Seasone s on t.ID_Seasone=s.ID_Seasone where te.ID_Test=@TTT) and Seasone_of_User>(select s.ID_Seasone from Seasone s join Team t on t.ID_Seasone=s.ID_Seasone where t.ID_Team=(select ID_Team from Test te where te.ID_Test=@TTT)) and LoginUser=@USS;";

                                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                                createCommand.CommandType = CommandType.Text;


                                createCommand.Parameters.AddWithValue("@TTT", g);
                                createCommand.Parameters.AddWithValue("@USS", Avtorizacia());

                                int count = Convert.ToInt32(createCommand.ExecuteScalar());
                                if (count == 1)
                                {
                                    MessageBox.Show("Можете пройти тест");

                                    TestsForUsers tes = new TestsForUsers();
                                    tes.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Увы, но вы еще не начинали смотреть этот сериал. Мы не хотим проспойлирить вам. Поэтому сначало добавьте сериал к вам в список");
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

        public static class IDTEEST
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
        private byte TESTisDONE()
        {
            byte us;
            us = TESTISDONE.GetValue<byte>("currentCustomerName");
            return us;

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string Admin = "Ksyusha8";

            string us = Avtorizacia();
            if (Admin == us)
            {
                offersANDcomplaints offer = new offersANDcomplaints();
                Close();
                offer.Show();
            }
            else
            {
                Results rez = new Results();
                Close();
                rez.Show();
            }
            
        }

        private void Zamech(object sender, RoutedEventArgs e)
        {
            offersANDcomplaints offer = new offersANDcomplaints();
            Close();
            offer.Show();
        }
    }
}
