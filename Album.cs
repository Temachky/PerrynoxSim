using System.IO;
using System.Windows.Media.Imaging;

namespace MusicPlayer
{
    public class Album
    {
        BitmapImage coverAlbum;
        string title;
        DirectoryInfo dir;

        public Album(string titel, BitmapImage image, DirectoryInfo dir)
        {
            this.title = titel;
            coverAlbum = image;
             this.dir = dir;      
        }
    }
}