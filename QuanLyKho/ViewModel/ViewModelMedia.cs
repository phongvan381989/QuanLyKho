using QuanLyKho.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
    public class ViewModelMedia : ViewModelBase
    {
        public ViewModelMedia()
        {
            listMediaFiles = new List<string>();
            _commandLeft = new CommandMedia_Left(this);
            _commandRight = new CommandMedia_Right(this);
        }
        private string _imagePath;
        public string imagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                if(_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged("imagePath");
                }
            }
        }

        private string _folderPath;

        public string folderPath
        {
            get
            {
                return _folderPath;
            }
            set
            {
                if(_folderPath != value)
                {
                    _folderPath = value;
                    //OnPropertyChanged("folderPath");
                    InitDisplay();
                }
            }
        }

        private CommandMedia_Left _commandLeft;
        public CommandMedia_Left commandLeft
        {
            get
            {
                return _commandLeft;
            }
        }

        private CommandMedia_Right _commandRight;
        public CommandMedia_Right commandRight
        {
            get
            {
                return _commandRight;
            }
        }

        // index ảnh hiển thị
        public int index { get; set; } // default-1
        public List<string> listMediaFiles;// default: số phần tử là 0
        public void Left()
        {
            if (index == -1 || listMediaFiles.Count == 0)
                return;
            if (index == 0)
                index = listMediaFiles.Count - 1;
            else
                index--;

            imagePath = System.IO.Path.Combine(folderPath, listMediaFiles.ElementAt(index));
            if (!File.Exists(imagePath))
            {
                // Ảnh bị xóa trong lúc đang xem thông tin
                // Cập nhật lại danh sách file ảnh
                GetAllMediaFiles();
            }
        }

        public void Right()
        {
            if (index == -1 || listMediaFiles.Count == 0)
                return;

            if (index == listMediaFiles.Count - 1)
                index = 0;
            else
                index++;

            imagePath = System.IO.Path.Combine(folderPath, listMediaFiles.ElementAt(index));
            if (!File.Exists(imagePath))
            {
                // Ảnh bị xóa trong lúc đang xem thông tin
                // Cập nhật lại danh sách file ảnh
                GetAllMediaFiles();
            }
        }

        public void InitDisplay()
        {
            GetAllMediaFiles();
            if (index != -1)
            {
                imagePath = System.IO.Path.Combine(folderPath, listMediaFiles.ElementAt(index));
            }
        }

        // Lấy tất cả file ảnh trong thư mục lưu vào list
        public void GetAllMediaFiles()
        {
            index = -1;
            listMediaFiles.Clear();

            // Check thư mục chứa tồn tại
            if (String.IsNullOrEmpty(folderPath))
                return;
            if (!Directory.Exists(folderPath))
                return;

            listMediaFiles.AddRange(Directory.GetFiles(folderPath, "*.png").ToList());
            listMediaFiles.AddRange(Directory.GetFiles(folderPath, "*.jpg").ToList());
            listMediaFiles.AddRange(Directory.GetFiles(folderPath, "*.jpeg").ToList());
            listMediaFiles.Sort();
            if (listMediaFiles.Count() != 0)
                index = 0;
        }
    }
}
