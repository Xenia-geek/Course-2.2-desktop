using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using static курсач_7_мая.input;

namespace курсач_7_мая.offers
{
    /// <summary>
    /// Логика взаимодействия для ADDOffers.xaml
    /// </summary>
    public partial class ADDOffers : Window
    {
        public ADDOffers()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=KSYUSHA_8\SQLEXPRESS; Initial Catalog=SERIALS; Integrated Security=True;";

        private string Avtorizacia()
        {
            string us;
            us = ApplicationState.GetValue<string>("currentCustomerName");
            return us;

        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if(Name.Text=="" || Name.Text== "Введите вашу почту..." || Password.Text=="" || Password.Text== "Введите пароль от почты..." || AboutSerials.Text==""|| AboutSerials.Text== "Ваше замечание или предложение...")
            {
                MessageBox.Show("Введите все данные");
            }
            else if(NameNOTcorrect.Opacity==0 && PassNOTcorrect.Opacity==0)
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                try
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd2 = new SqlCommand("OffersADD", sqlCon);
                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                    sqlCmd2.Parameters.AddWithValue("@Login", Avtorizacia().Trim());
                    sqlCmd2.Parameters.AddWithValue("@Offer", AboutSerials.Text.Trim());
                    sqlCmd2.Parameters.AddWithValue("@DateofSend", DateTime.Now);

                    sqlCmd2.ExecuteNonQuery();
                    try
                    {

                        MailAddress mailAddressFrom = new MailAddress(Name.Text, "Сериалы для задротов");

                        string Adm = "ksyushayakubenko.8@gmail.com";
                        MailAddress mailAddressTo = new MailAddress(Adm);

                        MailMessage message = new MailMessage(mailAddressFrom, mailAddressTo);

                        message.Subject = "Оповещение о жалобе";
                        message.Body = "Вам поступила жалоба или предложение \nНе забудьте проверить в приложении!";
                        SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(Name.Text, Password.Text);

                        smtp.Send(message);
                        MessageBox.Show("Успешное отправление");
                        Close();
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
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(Name.Text, @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$").Success )
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

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.Match(Password.Text, @"^[а-яА-ЯёЁa-zA-Z0-9]+[а-яА-ЯёЁa-zA-Z0-9]+$").Success || Password.Text.Length < 8)
            {
                if (borderpass != null)
                {
                    borderpass.Background = Brushes.Red;
                }
                if (PassNOTcorrect != null)
                {
                    PassNOTcorrect.Opacity = 1;
                }
            }
            else
            {
                if (borderpass != null)
                {
                    borderpass.Background = Brushes.HotPink;
                    borderpass.Opacity = 0.7;
                }
                if (PassNOTcorrect != null)
                {
                    PassNOTcorrect.Opacity = 0;
                }
            }
        }

        private void Name_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Name.Text = "";
        }

        private void Password_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Password.Text = "";
        }

        private void AboutSerials_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AboutSerials.Text = "";
        }

      
    }
}
