using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ПрактикаКатаргинНкикита
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 467;
            this.ResizeMode = ResizeMode.NoResize;
        }
        private DispatcherTimer timer;
        private TimeSpan sessionTime;
        private DateTime GetBlockedUntil(string username)
        {
            DateTime blockedUntil = DateTime.MaxValue;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT заблокирован_до FROM Сотрудники WHERE логин=@Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        DateTime.TryParse(result.ToString(), out blockedUntil);
                    }
                }
            }
            return blockedUntil;
        }
        private readonly string connectionString = "Data Source=DESKTOP-PDI82PU\\SQLEXPRESS01;Initial Catalog=MEFFLAB;Integrated Security=True";
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string username = LogUser.Text;
            string password = PasUser.Text;
            PasUser.Text = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT Id_Отдела FROM Сотрудники WHERE логин=@Username AND пароль=@Password", connection);

                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);


                int id_Отдела = (int?)command.ExecuteScalar() ?? -1;
                DateTime blockedUntil = GetBlockedUntil(username);

                if (blockedUntil != DateTime.MaxValue && blockedUntil > DateTime.Now)
                {
                    MessageBox.Show($"Учетная запись заблокирована до {blockedUntil}");
                    return;
                }
                else
                {

                    switch (id_Отдела)
                    {

                        case 1:
                            Выбор choice1 = new Выбор();
                            sessionTime = TimeSpan.FromSeconds(0);
                            timer = new DispatcherTimer();
                            timer.Interval = TimeSpan.FromSeconds(1);
                            timer.Tick += Timer_Tick;
                            timer.Start();
                            choice1.Show();
                            Close();
                            break;

                        case 2:
                            Выбор2 choice2 = new Выбор2();
                            sessionTime = TimeSpan.FromSeconds(0);
                            timer = new DispatcherTimer();
                            timer.Interval = TimeSpan.FromSeconds(1);
                            timer.Tick += Timer_Tick;
                            timer.Start();
                            choice2.Show();
                            Close();
                            break;
                        case 3:
                            Выбор3 choice3 = new Выбор3();
                            choice3.Show();
                            Close();
                            break;
                        case 4:
                            Выбор_4 choice4 = new Выбор_4();
                            choice4.Show();
                            Close();
                            break;
                        default:
                            MessageBox.Show("Неверное имя пользователя или пароль.");
                            break;
                    }
                }
            }
        }
        private bool isWarned = false;

        private void Timer_Tick(object sender, EventArgs e)
        {
            sessionTime = sessionTime.Add(TimeSpan.FromSeconds(1));
            if (sessionTime >= TimeSpan.FromMinutes(2))
            {
                timer.Stop();
                sessionTime = TimeSpan.Zero;
                DateTime blockedUntil = DateTime.Now.AddMinutes(30);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Сотрудники SET заблокирован_до=@BlockedUntil WHERE логин=@Username", connection);
                    command.Parameters.AddWithValue("@BlockedUntil", blockedUntil);
                    command.Parameters.AddWithValue("@Username", LogUser.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Время сеанса истекло. Аккаунт заблокирован на 30 минут.");
                Application.Current.Shutdown();
            }
            else if (sessionTime >= TimeSpan.FromMinutes(1) && !isWarned) 
            {
                isWarned = true;
                MessageBox.Show("До окончания времени сеанса осталось 15 минут.");
            }
        }
        private void Выход_Click(object sender, RoutedEventArgs e)
        {
            if (timer != null) 
            {
                timer.Stop();
                sessionTime = TimeSpan.Zero;
            }
            Close();
        }
    }
}
