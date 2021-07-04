using QuanLyKho.General;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace QuanLyKho.View
{
    /// <summary>
    /// Interaction logic for Media.xaml
    /// Hiển thị ảnh, video về sản phẩm
    /// </summary>
    public partial class Media : UserControl
    {
        public string mediaFolder { get; set; }
        // index ảnh hiển thị
        public int index { get; set; } // default-1
        public List<string> listMediaFiles;// default: số phần từ là 0

        public Media()
        {
            InitializeComponent();
            Loaded += MyLoadedRoutedEventHandler;
        }
        void MyLoadedRoutedEventHandler(Object sender, RoutedEventArgs e)
        {
            index = -1;
            listMediaFiles = new List<string>();

            if (String.IsNullOrEmpty(mediaFolder))
                return;

            InitDisplay();
        }

        public void InitDisplay()
        {
            GetAllMediaFiles();
            if (index != -1)
            {
                string path = System.IO.Path.Combine(mediaFolder, listMediaFiles.ElementAt(index));
                DisplayAMedia(path);
            }
        }

        // Lấy tất cả file ảnh trong thư mục lưu vào list
        private void GetAllMediaFiles()
        {
            index = -1;
            listMediaFiles.Clear();

            // Check thư mục chứa tồn tại
            if (String.IsNullOrEmpty(mediaFolder))
                return;
            if (!Directory.Exists(mediaFolder))
                return;

            listMediaFiles.AddRange(Directory.GetFiles(mediaFolder, "*.png").ToList());
            listMediaFiles.AddRange(Directory.GetFiles(mediaFolder, "*.jpg").ToList());
            listMediaFiles.AddRange(Directory.GetFiles(mediaFolder, "*.jpeg").ToList());
            listMediaFiles.Sort();
            if(listMediaFiles.Count() != 0)
                index = 0;
        }
        private void DisplayAMedia(string path)
        {
            MyLogger.GetInstance().DebugFormat("Display meadia {0}", path);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            ContentMedia.Source = bitmap;
        }
        private void Click_Truoc(object sender, RoutedEventArgs e)
        {
            if (index == -1 || listMediaFiles.Count == 0)
                return;
            if (index == 0)
                index = listMediaFiles.Count - 1;
            else
                index--;

            string path = System.IO.Path.Combine(mediaFolder, listMediaFiles.ElementAt(index));
            if (!File.Exists(path))
            {
                // Ảnh bị xóa trong lúc đang xem thông tin
                // Cập nhật lại danh sách file ảnh
                GetAllMediaFiles();
            }
            DisplayAMedia(path);
        }

        private void Click_Sau(object sender, RoutedEventArgs e)
        {
            if (index == -1 || listMediaFiles.Count == 0)
                return;

            if (index == listMediaFiles.Count - 1)
                index = 0;
            else
                index++;

            string path = System.IO.Path.Combine(mediaFolder, listMediaFiles.ElementAt(index));
            if (!File.Exists(path))
            {
                // Ảnh bị xóa trong lúc đang xem thông tin
                // Cập nhật lại danh sách file ảnh
                GetAllMediaFiles();
            }
            DisplayAMedia(path);
        }
    }
}
