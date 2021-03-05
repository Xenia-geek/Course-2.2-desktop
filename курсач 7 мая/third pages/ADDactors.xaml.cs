using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using курсач_7_мая.main_pages;
using курсач_7_мая.secondary_pages;

namespace курсач_7_мая.third_pages
{
    /// <summary>
    /// Логика взаимодействия для ADDactors.xaml
    /// </summary>
    public partial class ADDactors : Window
    {
        public ADDactors()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";


        private void Continue_Click(object sender, RoutedEventArgs e)
        {

            if (Name.Text == null || Birthdat.SelectedDate == null || Name.Text == "Введите имя" || Surnameame.Text == null || Surnameame.Text == "Введите фамилию" )
            {
                MessageBox.Show("ФИО или дата рождения не введены");
            }

            else if (NameNOTcorrect.Opacity == 0 && SurNameNOTcorrect.Opacity == 0 && BirthdayNOTcorrect.Opacity == 0)
            {


                SqlConnection sqlCon = new SqlConnection(@"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;");

                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    string query = "SELECT COUNT(1) FROM Actor WHERE Name=@Name AND Surname=@Surnsme AND Date_of_Birth=@BDay;";

                    
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Name", Name.Text);
                    
                    sqlCmd.Parameters.AddWithValue("@Surnsme", Surnameame.Text);
                    sqlCmd.Parameters.AddWithValue("@BDay", Birthdat.SelectedDate);
                   

                    int count3 = Convert.ToInt32(sqlCmd.ExecuteScalar());
                   
                    if (count3 == 1)
                    {
                        MessageBox.Show("Такой актер уже есть в базе");
                    }
                    else
                    {
                        
                        using (SqlConnection sqlCon1 = new SqlConnection(connectionString))
                        {

                            sqlCon1.Open();

                            if (PhotoActor.Source == null && Biography.Text==null)
                            {
                                SqlCommand sqlCmd2 = new SqlCommand("ActorADD", sqlCon1);
                                sqlCmd2.CommandType = CommandType.StoredProcedure;
                                sqlCmd2.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                sqlCmd2.Parameters.AddWithValue("@Surname", Surnameame.Text.Trim());
                                sqlCmd2.Parameters.AddWithValue("@Date_of_Birth", Birthdat.SelectedDate);


                                //sqlCmd2.ExecuteNonQuery();
                                int IDACTOR = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                IDofACTORS.SetValue("currentCustomerName", IDACTOR);
                            }
                            else if(PhotoActor.Source == null)
                            {
                                SqlCommand sqlCmd2 = new SqlCommand("ActorADDwithBiogr", sqlCon1);
                                sqlCmd2.CommandType = CommandType.StoredProcedure;
                                sqlCmd2.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                sqlCmd2.Parameters.AddWithValue("@Surname", Surnameame.Text.Trim());
                                sqlCmd2.Parameters.AddWithValue("@Date_of_Birth", Birthdat.SelectedDate);
                                sqlCmd2.Parameters.AddWithValue("@Biography", Biography.Text.Trim());

                                //sqlCmd2.ExecuteNonQuery();
                                int IDACTOR = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                IDofACTORS.SetValue("currentCustomerName", IDACTOR);

                               
                            }

                            else if(Biography.Text == "")
                            {
                                try
                                {
                                    //byte pictur = Convert.ToByte(PhotoActor.Source);
                                    SqlCommand sqlCmd2 = new SqlCommand("ActorADDwithPict", sqlCon1);
                                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                                    sqlCmd2.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                    sqlCmd2.Parameters.AddWithValue("@Surname", Surnameame.Text.Trim());
                                    sqlCmd2.Parameters.AddWithValue("@Date_of_Birth", Birthdat.SelectedDate);
                                    sqlCmd2.Parameters.AddWithValue("@Photo", foto);

                                    //sqlCmd2.ExecuteNonQuery();
                                    int IDACTOR = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                    IDofACTORS.SetValue("currentCustomerName", IDACTOR);
                                    MessageBox.Show("Я еще не добавила фото сорри");
                                }
                                catch
                                {
                                    MessageBox.Show("bed");
                                }
                                
                            }
                            else if(Biography.Text != null && PhotoActor.Source != null)
                            {
                                //ImageClass images = new ImageClass();
                                //byte pictur = Convert.ToByte(PhotoActor.Source);
                                //SqlCommand sqlCmd2 = new SqlCommand("ActorADDwithPictandBio", sqlCon1);
                                //sqlCmd2.CommandType = CommandType.StoredProcedure;
                                //sqlCmd2.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                //sqlCmd2.Parameters.AddWithValue("@Surname", Surnameame.Text.Trim());
                                //sqlCmd2.Parameters.AddWithValue("@Date_of_Birth", Birthdat.SelectedDate);
                                //sqlCmd2.Parameters.AddWithValue("@Photo", images.ImageToByte=File.ReadAllBytes(pic.FileName));
                                //sqlCmd2.Parameters.AddWithValue("@Biography", Biography.Text.Trim());

                                ////sqlCmd2.ExecuteNonQuery();
                                //int IDACTOR = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                //IDofACTORS.SetValue("currentCustomerName", IDACTOR);
                            }

                        }
                        
                        Close();
                        


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
                MessageBox.Show("Не корректно введено имя");
            }
            else if (SurNameNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введена фамилия");
            }
            else if (BirthdayNOTcorrect.Opacity == 1)
            {
                MessageBox.Show("Не корректно введена дата рождения");
            }
            
        }

        private void Name_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Name.Text = "";
        }

        private void Surname_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Surnameame.Text = "";
        }

        private void Biography_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Biography.Text = "";
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(Name.Text, @"(^[А-ЯЁA-Z][^0-9 ]*[а-яёa-z]+$)").Success || Name.Text.Length > 15)
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

        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(Surnameame.Text, @"(^[А-ЯЁA-Z][^0-9 ]*[а-яёa-z]+$)").Success || Surnameame.Text.Length > 15)
            {
                if (bordersurname != null)
                {
                    bordersurname.Background = Brushes.Red;
                }
                if (SurNameNOTcorrect != null)
                {
                    SurNameNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (bordersurname != null)
                {
                    bordersurname.Background = Brushes.HotPink;
                    bordersurname.Opacity = 0.7;
                }
                if (SurNameNOTcorrect != null)
                {
                    SurNameNOTcorrect.Opacity = 0;
                }

            }
        }

        private void Birthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Birthdat.SelectedDate > DateTime.Now)
            {
                Birthdat.Background = Brushes.Red;
                if (BirthdayNOTcorrect != null)
                {
                    BirthdayNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                Birthdat.Background = null;
                if (BirthdayNOTcorrect != null)
                {
                    BirthdayNOTcorrect.Opacity = 0;
                }
            }
        }
        public static class IDofACTORS
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
        OpenFileDialog pic = new OpenFileDialog();

        byte[] foto;

        private void AddPicture_Click(object sender, RoutedEventArgs e)
        {
            
            pic.Filter = "Image Files(*.BMP;*JPG;*GIF;*.PNG)|*.BMP;*JPG;*GIF;*.PNG|All files(*.*)|*.*";
            if(pic.ShowDialog()==true)
            {
                try
                {
                    foto = File.ReadAllBytes(System.IO.Path.GetFullPath(pic.FileName));
                }
                catch
                {
                    MessageBox.Show("Что-то не так");
                }
            }
        }
        

    }

    
}
