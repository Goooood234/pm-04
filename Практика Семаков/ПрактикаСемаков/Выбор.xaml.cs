using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Логика взаимодействия для Выбор.xaml
    /// </summary>
    public partial class Выбор : Window
{
    private DispatcherTimer countdownTimer = new DispatcherTimer();
    private TimerManager timerManager = TimerManager.Instance;

    public Выбор()
    {
        InitializeComponent();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
        countdownTimer.Tick += CountdownTimer_Tick;

        timerLabel.Content = timerManager.CurrentTime.ToString(@"mm\:ss");

        countdownTimer.Start();

        Closing += Выбор_Closing;
    }
        public class TimerManager
        {
            private static TimerManager instance;
            private TimeSpan currentTime = TimeSpan.FromMinutes(2);

            public static TimerManager Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new TimerManager();
                    }
                    return instance;
                }
            }

            public TimeSpan CurrentTime
            {
                get => currentTime;
                set => currentTime = value;
            }
        }
        private void Выбор_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        countdownTimer.Stop();
    }

    private void CountdownTimer_Tick(object sender, EventArgs e)
    {
        timerManager.CurrentTime = timerManager.CurrentTime.Subtract(TimeSpan.FromSeconds(1));
        timerLabel.Content = timerManager.CurrentTime.ToString(@"mm\:ss");
        if (timerManager.CurrentTime == TimeSpan.Zero)
        {
            countdownTimer.Stop();
            MessageBox.Show("Время вышло!");
            // Закрыть форму или выполнить другие действия при окончании времени
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
  
        ПриёмкаБиоМат приёмкаБиоМат = new ПриёмкаБиоМат();
        приёмкаБиоМат.Closed += ПриёмкаБиоМат_Closed;
        приёмкаБиоМат.ShowDialog();
    }

    private void ПриёмкаБиоМат_Closed(object sender, EventArgs e)
    {
        timerManager = TimerManager.Instance;
        countdownTimer.Start();
        timerLabel.Content = timerManager.CurrentTime.ToString(@"mm\:ss");
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.Key == Key.Escape)
        {
            Close();
        }
    }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        
    }

}

