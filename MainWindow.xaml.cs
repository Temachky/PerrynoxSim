using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;

namespace MusicPlayer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static string rootPath = @"C:\Users\Acer\AppData\Roaming";
        
    static string folderName = "PerrynoxData"; 

    public static string fullPath = Path.Combine(rootPath, folderName);

    public MainWindow()
    {  
      if(!Directory.Exists(fullPath))
      {  
        Directory.CreateDirectory(fullPath);
      }
        InitializeComponent();
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
       AddWindow add = new AddWindow();
       add.ShowDialog();
    }
}