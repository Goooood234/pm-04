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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ПрактикаКатаргинНкикита
{
    /// <summary>
    /// Логика взаимодействия для Выбор2.xaml
    /// </summary>
    public partial class Выбор2 : Window
    {

        private DispatcherTimer countdownTimer = new DispatcherTimer();
        private TimeSpan timeLeft = TimeSpan.FromMinutes(2);

        public Выбор2()
        {
            InitializeComponent();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
            timerLabel.Content = timeLeft.ToString(@"mm\:ss");
            if (timeLeft <= TimeSpan.Zero)
            {
                countdownTimer.Stop();
                MessageBox.Show("Время вышло!");
                // Закрыть форму или выполнить другие действия при окончании времени
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}