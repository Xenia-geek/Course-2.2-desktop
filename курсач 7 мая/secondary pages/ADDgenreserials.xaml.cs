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
    /// Логика взаимодействия для ADDgenreserials.xaml
    /// </summary>
    public partial class ADDgenreserials : Window
    {
        public ADDgenreserials()
        {
            InitializeComponent();
            NameSerial();
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";
 

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

        private void Genre_Click(object sender, RoutedEventArgs e)
        {
            if (NameGenre.SelectedItem == null)
            {
                MessageBox.Show("Выберите жанр");
            }

            else if (GenreNOTcorrect.Opacity == 0)
            {

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "SELECT COUNT(1) FROM Genre WHERE Name=@Name AND ID_Serials=@ID;";

                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;

                        
                        sqlCmd.Parameters.AddWithValue("@Name", NameGenre.Text);

                        sqlCmd.Parameters.AddWithValue("@ID", IDSerialls());



                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                        if (count == 1)
                        {

                            if (bordergenre != null)
                            {
                                bordergenre.Background = Brushes.Red;
                            }
                            if (GenreNOTcorrect != null)
                            {
                                GenreNOTcorrect.Opacity = 1;
                            }
                            MessageBox.Show("Жанр уже есть в базе");
                        }
                        else
                        {
                            if (bordergenre != null)
                            {
                                bordergenre.Background = null;
                            }
                            if (GenreNOTcorrect != null)
                            {
                                GenreNOTcorrect.Opacity = 0;
                            }
                            using (SqlConnection sqlCon2 = new SqlConnection(connectionString))
                            {
                                sqlCon2.Open();

                                SqlCommand sqlCmd2 = new SqlCommand("GENREADD", sqlCon2);

                                sqlCmd2.CommandType = CommandType.StoredProcedure;

                                sqlCmd2.Parameters.AddWithValue("@Genre", NameGenre.Text);
                                sqlCmd2.Parameters.AddWithValue("@ID_Serials", IDSerialls());


                                sqlCmd2.ExecuteNonQuery();

                            }
                            //////////////////////////////////////

                            ADDgenreserials genre = new ADDgenreserials();
                            Close();
                            genre.Show();

                            /////////////////////////////////////


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

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (NameGenre.SelectedItem == null)
            {
                MessageBox.Show("Выберите жанр");
            }

            else if (GenreNOTcorrect.Opacity == 0)
            {

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "SELECT COUNT(1) FROM Genre WHERE Name=@Name AND ID_Serials=@ID;";

                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;

                        
                        sqlCmd.Parameters.AddWithValue("@Name", NameGenre.Text);

                        sqlCmd.Parameters.AddWithValue("@ID", IDSerialls());
                       


                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        
                        if (count == 1)
                        {

                            if (bordergenre != null)
                            {
                                bordergenre.Background = Brushes.Red;
                            }
                            if (GenreNOTcorrect != null)
                            {
                                GenreNOTcorrect.Opacity = 1;
                            }
                            MessageBox.Show("Жанр уже есть в базе");
                        }
                        else
                        {
                            if (bordergenre != null)
                            {
                                bordergenre.Background = null;
                            }
                            if (GenreNOTcorrect != null)
                            {
                                GenreNOTcorrect.Opacity = 0;
                            }
                            using (SqlConnection sqlCon2 = new SqlConnection(connectionString))
                            {
                                sqlCon2.Open();

                                SqlCommand sqlCmd2 = new SqlCommand("GENREADD", sqlCon);
                                
                                sqlCmd2.CommandType = CommandType.StoredProcedure;

                                
                                sqlCmd2.Parameters.AddWithValue("@Genre", NameGenre.Text);
                                sqlCmd2.Parameters.AddWithValue("@ID_Serials", IDSerialls());


                                sqlCmd2.ExecuteNonQuery();

                            }
                            //////////////////////////////////////

                            ADDactorsserials actor = new ADDactorsserials();
                            Close();
                            actor.Show();

                            /////////////////////////////////////


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

        private void NameGenre_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameGenre.SelectedItem = null;
            if (bordergenre != null)
            {
                bordergenre.Background = null;
            }
            if (GenreNOTcorrect != null)
            {
                GenreNOTcorrect.Opacity = 0;
            }
        }
    }
}
