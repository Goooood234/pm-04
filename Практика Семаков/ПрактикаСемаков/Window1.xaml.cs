using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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

namespace ПрактикаКатаргинНкикита
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ПриёмкаБиоМат выбор = new ПриёмкаБиоМат();
            выбор.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // получаем фамилию и имя клиента из TextBox'ов
            string lastName = tbLastName.Text;
            string firstName = tbFirstName.Text;

            // создаем подключение к базе данных
            using (M1EFFLABEntities db = new M1EFFLABEntities())
            {
                // создаем нового клиента и заполняем его поля
                Клиенты клиент = new Клиенты
                {
                    Фамилия = lastName,
                    имя = firstName
                };

                // добавляем клиента в контекст базы данных и сохраняем изменения
                db.Клиенты.Add(клиент);
                db.SaveChanges();

                // выводим сообщение об успешном добавлении клиента
                MessageBox.Show("Клиент успешно добавлен в базу данных!");
            }
        }
    }
}
