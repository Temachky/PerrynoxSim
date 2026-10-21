using System;
using System.IO;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MusicPlayer
{
    public partial class AddWindow : Window
    {
        public string SelectedCoverPath { get; private set; }

        public AddWindow()
        {
            InitializeComponent();
        }

        private void DropArea_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string filePath = files?.FirstOrDefault();

                if (!string.IsNullOrEmpty(filePath) && IsImageFile(filePath))
                {
                    try
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(filePath);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad; // Чтобы файл не блокировался Windows
                        bitmap.EndInit();

                        // Показываем картинку и сохраняем путь
                        AlbumCoverImage.Source = bitmap;
                        SelectedCoverPath = filePath;

                        // Скрываем текст-подсказку
                        HintText.Visibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки обложки: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        private bool IsImageFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            return ext == ".png" ;
        }

        public void Create(object sender, RoutedEventArgs e)
        {  
           string name = AlbumNameTextBox.Text;
        //    путь к папке
           string path = Path.Combine(MainWindow.fullPath, name);
           Directory.CreateDirectory(path);
           Album album = new Album(name, (BitmapImage) AlbumCoverImage.Source, new DirectoryInfo(path));
           AlbumManager.albums.Add(name, album);
        // путь к картинке   
           string imagePath = Path.Combine(path + @"\" + name + ".png");

           if(AlbumCoverImage.Source != null) {
              PngBitmapEncoder encoder = new PngBitmapEncoder();
              encoder.Frames.Add(BitmapFrame.Create((BitmapSource)AlbumCoverImage.Source));

              using(FileStream stream = new FileStream(imagePath, FileMode.Create))
                {
                    encoder.Save(stream);
                }
             }

            Close(); 
        }
    }
}
