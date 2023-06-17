using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
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
using System.Windows.Threading;
using ZXing;
using static ПрактикаКатаргинНкикита.Выбор;
using System.IO;


namespace ПрактикаКатаргинНкикита
{
    /// <summary>
    /// Логика взаимодействия для ПриёмкаБиоМат.xaml
    /// </summary>
    public partial class ПриёмкаБиоМат : Window
    {

        public ПриёмкаБиоМат()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 467;
            this.ResizeMode = ResizeMode.NoResize;
            using (M1EFFLABEntities db = new M1EFFLABEntities())
            {
                // Выбрать нужные данные из таблицы Услуга
                var categories = db.Услуга.Select(b => new { b.Id_Услуги, b.Наименование1 }).ToList();
                // Добавить выбранные данные в ComboBox
                vidMat_Copy.ItemsSource = categories;
                vidMat_Copy.DisplayMemberPath = "Наименование1";
                vidMat_Copy.SelectedValuePath = "Id_Услуги";
                // Выбрать нужные данные из таблицы ВидМатериала
                var categories1 = db.ВидМатериала.Select(c => new { c.Id_видаМатериала, c.Наименование }).ToList();
                // Добавить выбранные данные в ComboBox
                vidMat.ItemsSource = categories1;
                vidMat.DisplayMemberPath = "Наименование";
                vidMat.SelectedValuePath = "Id_видаМатериала";
                var clients = db.Клиенты.Select(c => new { c.Фамилия, c.имя, c.Id_Клиента }).ToList();
                GTR.ItemsSource = clients;
            }
            
        }

        private readonly string connectionString = "Data Source=DESKTOP-PDI82PU\\SQLEXPRESS01;Initial Catalog=MEFFLAB;Integrated Security=True";


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            Выбор выбор = new Выбор();
            выбор.Show();
        
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // получаем данные из TextBox с кодом
            string code = tbCode.Text;

            // создаем объект генератора штрихкодов
            BarcodeWriter writer = new BarcodeWriter();

            // устанавливаем тип штрихкода (EAN13 в данном случае)
            writer.Format = BarcodeFormat.CODE_128;

            // генерируем изображение штрихкода на основе данных из TextBox
            var bmp = writer.Write(code);

            // конвертируем Bitmap в BitmapSource, чтобы можно было отобразить изображение на WPF форме
            var stream = new MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Position = 0;
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();

            // выводим изображение на Image элемент на WPF форме
            pictureBox1.Source = bitmapImage;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (M1EFFLABEntities db = new M1EFFLABEntities())
            {
                int intCode = int.Parse(tbCode.Text);
                int intBioma = int.Parse(bioma.Text);

                // Создаем новый штрихкод и сохраняем его в базе данных
                Штрихкод новыйШтрихкод = new Штрихкод
                {

                    Id_Штрихкода = intCode,
                    ДатаСоздания = DateTime.Now,
                    ВидМатериала = (int)vidMat.SelectedValue,
                    Id_услуги = (int)vidMat_Copy.SelectedValue,
                    Id_биоматериала = intBioma
                };
                db.Штрихкод.Add(новыйШтрихкод);
                db.SaveChanges();

                //Получаем Id нового штрихкода
                int штрихкодId = новыйШтрихкод.Id_Штрихкода;

                // получаем выбранного клиента из DataGrid
                Заказ новыйЗаказ = new Заказ { Id_Штрихкода = штрихкодId };
                db.Заказ.Add(новыйЗаказ);
                db.SaveChanges();

                // получаем Id нового заказа
                int заказId = новыйЗаказ.Id_заказа;

                // получаем выбранного клиента из DataGrid
                dynamic selectedClient = GTR.SelectedItem;
                int clientId = selectedClient.Id_Клиента;

                // находим клиента в базе данных по Id и присваиваем ему созданный заказ
                Клиенты клиент = db.Клиенты.FirstOrDefault(c => c.Id_Клиента == clientId);
                клиент.Id_Заказа = заказId;
                db.SaveChanges();

                // выводим сообщение об успешном добавлении заказа и штрихкода
                MessageBox.Show("Новый заказ и штрихкод успешно добавлены в базу данных!");

                // выводим сообщение об успешном добавлении заказа и штрихкода
                MessageBox.Show("Новый заказ и штрихкод успешно добавлены в базу данных!");
            }

        }

      
    }
}

