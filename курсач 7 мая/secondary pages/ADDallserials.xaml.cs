using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace курсач_7_мая.secondary_pages
{
    /// <summary>
    /// Логика взаимодействия для ADDallserials.xaml
    /// </summary>
    public partial class ADDallserials : Window
    {
        public ADDallserials()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";


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

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Name.Text = "";
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if(StartDate.SelectedDate>DateTime.Now)
            {
                StartDate.Background = Brushes.Red;
                if (DateStartNOTcorrect != null)
                {
                    DateStartNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                StartDate.Background = null;
                if (DateStartNOTcorrect != null)
                {
                    DateStartNOTcorrect.Opacity = 0;
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate.SelectedDate > DateTime.Now || EndDate.SelectedDate<=StartDate.SelectedDate)
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

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == null || StartDate.SelectedDate == null || Name.Text == "Введите название" || AboutSerials.Text == null || AboutSerials.Text == "Введите немного о сериале")
            {
                MessageBox.Show("Название или Дата начала или О сериале не введены");
            }

            else if (DateEndNOTcorrect.Opacity == 0 && DateStartNOTcorrect.Opacity == 0 && NameNOTcorrect.Opacity == 0)
            {


                SqlConnection sqlCon = new SqlConnection(@"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;");

                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    string query = "SELECT COUNT(1) FROM SERIALS WHERE Name=@Name AND Year_of_Start=@Year_of_Start;";

                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Name", Name.Text);
                    sqlCmd.Parameters.AddWithValue("@Year_of_Start", StartDate.SelectedDate);

                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("Такой сериал уже есть в базе");
                    }
                    else
                    {

                        using (SqlConnection sqlCon1 = new SqlConnection(connectionString))
                        {

                            sqlCon1.Open();

                            if (EndDate.SelectedDate == null)
                            {
                                SqlCommand sqlCmd2 = new SqlCommand("SERIALSADDwithoutEndDate", sqlCon1);
                                sqlCmd2.CommandType = CommandType.StoredProcedure;
                                sqlCmd2.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                sqlCmd2.Parameters.AddWithValue("@Year_of_Start", StartDate.SelectedDate);
                                sqlCmd2.Parameters.AddWithValue("@About_serials", AboutSerials.Text.Trim());

                                //sqlCmd2.ExecuteNonQuery();
                                int IDSERIAL = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                IDofSERIALS.SetValue("currentCustomerName", IDSERIAL);
                                SerialsNames.SetValue("currentCustomerName", Name.Text);
                            }
                            else
                            {
                                SqlCommand sqlCmd2 = new SqlCommand("SERIALSADD", sqlCon1);
                                sqlCmd2.CommandType = CommandType.StoredProcedure;
                                sqlCmd2.Parameters.AddWithValue("@Name", Name.Text.Trim());
                                sqlCmd2.Parameters.AddWithValue("@Year_of_Start", StartDate.SelectedDate);
                                sqlCmd2.Parameters.AddWithValue("@Year_of_End", EndDate.SelectedDate);
                                sqlCmd2.Parameters.AddWithValue("@About_serials", AboutSerials.Text.Trim());

                                //sqlCmd2.ExecuteNonQuery();

                                int IDSERIAL = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                                IDofSERIALS.SetValue("currentCustomerName", IDSERIAL);
                                SerialsNames.SetValue("currentCustomerName", Name.Text);
                            }




                            

                        }


                        ADDsessoneallserials seasone = new ADDsessoneallserials();
                        Close();
                        seasone.Show();
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

            else if(NameNOTcorrect.Opacity==1)
            {
                MessageBox.Show("Не корректно введено название");
            }
            else if(DateStartNOTcorrect.Opacity==1 && DateEndNOTcorrect.Opacity==1)
            {
                MessageBox.Show("Что-то не так с датами, проверьте правильность");
            }
            else if(DateStartNOTcorrect.Opacity==1)
            {
                MessageBox.Show("Дата выхода должна быть меньше, чем текущая дата");
            }
            else if(DateEndNOTcorrect.Opacity==1)
            {
                MessageBox.Show("Дата конца не может быть меньше чем дата начала, и также ее нельзя поставить заранее");
            }
        }
        public static class IDofSERIALS
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
        public static class SerialsNames
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

        private void AboutSerials_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AboutSerials.Text = "";
        }
    }
}
