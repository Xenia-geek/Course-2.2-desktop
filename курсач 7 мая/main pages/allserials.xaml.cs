using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using курсач_7_мая.secondary_pages;
using static курсач_7_мая.input;
using курсач_7_мая.main_pages;
using курсач_7_мая.fourth_pages;
using курсач_7_мая.Tests;
using курсач_7_мая.offers;

namespace курсач_7_мая
{
    /// <summary>
    /// Логика взаимодействия для allserials.xaml
    /// </summary>
    public partial class allserials : Window
    {
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";
        DataTable db = new DataTable("SERIALS");

        public allserials()
        {
            InitializeComponent();
            LoadAllSerials();
            Avtorizacia();

        }

        private void LoadAllSerials()
        {

            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select distinct s.ID_Serials[ID], se.Photo[Обложка], s.Name[Сериал], (select count(f.Number_Seasone) from Seasone f where se.ID_Serials = f.ID_Serials) [Сезон],g.Name[Жанр],concat(dir.Name, dir.Surname)[Режиссер], concat(act.Name, act.Surname)[Актеры]  from SERIALS s join Seasone se on s.ID_Serials=se.ID_Serials join Genre g on se.ID_Serials= g.ID_Serials join DIRECTORS di on g.ID_Serials= di.ID_Serials join Director dir on di.ID_Director= dir.ID_Director join ACTORS ac on ac.ID_Serials=s.ID_Serials join Actor act on act.ID_Actor=ac.ID_Actor ORDER BY s.Name; ";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("SERIALS");
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
                    UpdAllSerials.Opacity = 0.8;
                    UpdAllSerials.IsEnabled = true;
                    DelAllSerials.Opacity = 0.8;
                    DelAllSerials.IsEnabled = true;
                    CommentAllSerials.Opacity = 0;
                    CommentAllSerials.IsEnabled = false;
                    Zampred.Opacity = 1;
                    Zampred.IsEnabled = true;
                    Myserials.Header = "Статистика";

                    

                }
                else
                {
                    AddAllSerials.Opacity = 0;
                    AddAllSerials.IsEnabled = false;
                    UpdAllSerials.Opacity = 0;
                    UpdAllSerials.IsEnabled = false;
                    DelAllSerials.Opacity = 0;
                    DelAllSerials.IsEnabled = false;
                    CommentAllSerials.Opacity = 0.8;
                    CommentAllSerials.IsEnabled = true;
                    Zampred.Opacity = 0;
                    Zampred.IsEnabled = true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            ADDallserials add = new ADDallserials();
            add.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            input input = new input();
            Close();
            input.Show();
        }

        private void AllSerials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void SerAllSerials_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (Search.Text != "" || Search.Text != "Поиск по названию...")
            {
                AllSerials.ItemsSource = null;
                try
                {
                    sqlCon.Open();
                    string Query = "select distinct  s.ID_Serials[ID], se.Photo[Обложка], s.Name[Сериал], (select count(f.Number_Seasone) from Seasone f where se.ID_Serials = f.ID_Serials) [Сезон],g.Name[Жанр],concat(dir.Name, dir.Surname)[Режиссер],concat(act.Name, act.Surname)[Актеры]  from SERIALS s join Seasone se on s.ID_Serials=se.ID_Serials join Genre g on se.ID_Serials= g.ID_Serials join DIRECTORS di on g.ID_Serials= di.ID_Serials join Director dir on di.ID_Director= dir.ID_Director join ACTORS ac on ac.ID_Serials=s.ID_Serials join Actor act on act.ID_Actor=ac.ID_Actor Where s.Name like @NameOfSerial  ORDER BY s.Name;";

                    SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                    createCommand.CommandType = CommandType.Text;

                    string nameser = "%" + Search.Text + "%";
                    createCommand.Parameters.AddWithValue("@NameOfSerial", nameser);

                    SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                    DataTable db = new DataTable("SERIALS");
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

        private void Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Value_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Filtr.SelectedItem == null)
            {
                MessageBox.Show("Выберите категорию");
            }
            else
            {
                if (Filtr.Text == "Жанр")
                {
                    Value.Items.Clear();

                    Value.Items.Add("Боевик");


                    Value.Items.Add("Детектив");
                    Value.Items.Add("Драма");
                    Value.Items.Add("История");
                    Value.Items.Add("Комедия");
                    Value.Items.Add("Криминал");
                    Value.Items.Add("Мелодрама");
                    Value.Items.Add("Приключения");
                    Value.Items.Add("Спорт");
                    Value.Items.Add("Триллер");
                    Value.Items.Add("Ужасы");
                    Value.Items.Add("Фантастика");
                    Value.Items.Add("Фэнтези");

                }
                if (Filtr.Text == "Актер")
                {
                    Value.Items.Clear();
                    SqlConnection sqlCon = new SqlConnection(connectionString);
                    try
                    {
                        sqlCon.Open();
                        string Query = "select concat(Name, Surname) from Actor  ORDER BY Name;";

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

                                    string NUMBSEAS = myreader1.GetString(0);
                                    Value.Items.Add(NUMBSEAS);

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

                if (Filtr.Text == "Режиссер")
                {
                    Value.Items.Clear();
                    SqlConnection sqlCon = new SqlConnection(connectionString);
                    try
                    {
                        sqlCon.Open();
                        string Query = "select concat(Name, Surname) from Director  ORDER BY Name;";

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

                                    string NUMBSEAS = myreader1.GetString(0);
                                    Value.Items.Add(NUMBSEAS);

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

        private void Filtr_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Value.Items.Clear();
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
                if (Filtr.Text == "Жанр")
                {


                    AllSerials.ItemsSource = null;
                    try
                    {
                        sqlCon.Open();
                        string Query = "select distinct s.ID_Serials[ID], se.Photo[Обложка], s.Name[Сериал], (select count(f.Number_Seasone) from Seasone f where se.ID_Serials = f.ID_Serials) [Сезон],isnull(g.Name,'нет')[Жанр],isnull(concat(dir.Name, dir.Surname),'нет')[Режиссер],isnull(concat(act.Name, act.Surname),'нет')[Актеры]  from SERIALS s join Seasone se on s.ID_Serials=se.ID_Serials join Genre g on se.ID_Serials= g.ID_Serials join DIRECTORS di on g.ID_Serials= di.ID_Serials join Director dir on di.ID_Director= dir.ID_Director join ACTORS ac on ac.ID_Serials=s.ID_Serials join Actor act on act.ID_Actor=ac.ID_Actor Where g.Name= @NameOfSerial  ORDER BY s.Name;";

                        SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                        createCommand.CommandType = CommandType.Text;


                        createCommand.Parameters.AddWithValue("@NameOfSerial", Value.Text);

                        SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                        DataTable db = new DataTable("Genre");
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
                if (Filtr.Text == "Актер")
                {
                    AllSerials.ItemsSource = null;
                    try
                    {
                        sqlCon.Open();
                        string Query = "select distinct s.ID_Serials[ID], se.Photo[Обложка], s.Name[Сериал], (select count(f.Number_Seasone) from Seasone f where se.ID_Serials = f.ID_Serials) [Сезон],isnull(g.Name,'нет')[Жанр], concat(dir.Name, dir.Surname)[Режиссер], concat(act.Name, act.Surname)[Актеры]  from SERIALS s join Seasone se on s.ID_Serials=se.ID_Serials join Genre g on se.ID_Serials= g.ID_Serials join DIRECTORS di on g.ID_Serials= di.ID_Serials join Director dir on di.ID_Director= dir.ID_Director join ACTORS ac on ac.ID_Serials=s.ID_Serials join Actor act on act.ID_Actor=ac.ID_Actor Where concat(act.Name, act.Surname)= @NameOfSerial  ORDER BY s.Name;";

                        SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                        createCommand.CommandType = CommandType.Text;


                        createCommand.Parameters.AddWithValue("@NameOfSerial", Value.Text);

                        SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                        DataTable db = new DataTable("Actors");
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
                if (Filtr.Text == "Режиссер")
                {
                    AllSerials.ItemsSource = null;
                    try
                    {
                        sqlCon.Open();
                        string Query = "select distinct s.ID_Serials[ID], se.Photo[Обложка], s.Name[Сериал], (select count(f.Number_Seasone) from Seasone f where se.ID_Serials = f.ID_Serials) [Сезон],g.Name[Жанр],concat(dir.Name, dir.Surname)[Режиссер],concat(act.Name, act.Surname)[Актеры]  from SERIALS s join Seasone se on s.ID_Serials=se.ID_Serials join Genre g on se.ID_Serials= g.ID_Serials join DIRECTORS di on g.ID_Serials= di.ID_Serials join Director dir on di.ID_Director= dir.ID_Director join ACTORS ac on ac.ID_Serials=s.ID_Serials join Actor act on act.ID_Actor=ac.ID_Actor Where concat(dir.Name, dir.Surname)= @NameOfSerial  ORDER BY s.Name;";

                        SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                        createCommand.CommandType = CommandType.Text;


                        createCommand.Parameters.AddWithValue("@NameOfSerial", Value.Text);

                        SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                        DataTable db = new DataTable("Directors");
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

        private void Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ReLoad(object sender, RoutedEventArgs e)
        {

            SqlConnection sqlCon = new SqlConnection(connectionString);
            try
            {
                sqlCon.Open();
                string Query = "select distinct s.ID_Serials[ID], se.Photo[Обложка], s.Name[Сериал], (select count(f.Number_Seasone) from Seasone f where se.ID_Serials = f.ID_Serials) [Сезон],g.Name[Жанр],concat(dir.Name, dir.Surname)[Режиссер], concat(act.Name, act.Surname)[Актеры]  from SERIALS s join Seasone se on s.ID_Serials=se.ID_Serials join Genre g on se.ID_Serials= g.ID_Serials join DIRECTORS di on g.ID_Serials= di.ID_Serials join Director dir on di.ID_Director= dir.ID_Director join ACTORS ac on ac.ID_Serials=s.ID_Serials join Actor act on act.ID_Actor=ac.ID_Actor ORDER BY s.Name; ";
                //select se.Photo [Обложка], s.Name [Сериал], se.Number_Seasone [Сезон], g.Name [Жанр], concat(dir.Name, dir.Surname)[Режиссер] from SERIALS s join Seasone se on s.ID_Serials = se.ID_Serials join Genre g on s.ID_Serials = g.ID_Serials join DIRECTORS di on s.ID_Serials = di.ID_Serials join Director dir on di.ID_Director = dir.ID_Director ORDER BY s.Name; ";
                SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                DataTable db = new DataTable("SERIALS");
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
                            string Query3 = "delete from Serials_for_Users where ID_Serials=@ID;  declare @s int; declare Seas CURSOR for  select  ID_Seasone from Seasone where ID_Serials=@ID; open Seas; fetch Seas into @s; while @@FETCH_STATUS=0 begin delete Series where ID_Seasone=@s; delete Team where ID_Seasone=@s; fetch Seas into @s; end; close Seas; deallocate Seas; delete from Seasone where ID_Serials=@ID; delete from Genre where ID_Serials=@ID; delete from ACTORS where ID_Serials=@ID; delete from DIRECTORS where ID_Serials=@ID; delete from SERIALS where ID_Serials=@ID;  ";

                            SqlCommand createCommand = new SqlCommand(Query3, sqlCon);
                            createCommand.CommandType = CommandType.Text;

                            int g = Convert.ToInt32(dataRow[i]);

                            createCommand.Parameters.AddWithValue("@ID", g);

                            SqlDataAdapter dataadp = new SqlDataAdapter(createCommand);
                            DataTable db = new DataTable("SERIALS");
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

        private void AllSerials_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdAllSerials_Click(object sender, RoutedEventArgs e)
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
                            string Query = "select Year_of_End  from SERIALS  where ID_Serials=@ID ;";
                            SqlCommand createCommand = new SqlCommand(Query, sqlCon);
                            createCommand.CommandType = CommandType.Text;

                            
                            createCommand.CommandType = CommandType.Text;

                            int g = Convert.ToInt32(dataRow[i]);

                            createCommand.Parameters.AddWithValue("@ID", g);

                            string count = Convert.ToString(createCommand.ExecuteScalar());
                            IDSerial.SetValue("currentCustomerName", g);

                            if (count == "")
                            {
                                ChangeSerials change = new ChangeSerials();
                                change.Show();
                            }
                            else
                            {
                                MessageBox.Show("Низзя редачить сериал, если он закончен");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }



        public static class IDSerial
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

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            testtslist test = new testtslist();
            Close();
            test.Show();
        }

        private void Zamech(object sender, RoutedEventArgs e)
        {
            offersANDcomplaints offer = new offersANDcomplaints();
            Close();
            offer.Show();
        }
    }
}
