using QuanLyKho.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuanLyKho.ViewModel
{
    public class ViewModelMedia : ViewModelBase
    {
        public ViewModelMedia()
        {
            listMediaFiles = new List<string>();
            _commandLeft = new CommandMedia_Left(this);
            _commandRight = new CommandMedia_Right(this);
            _commandRotateLeft = new CommandMedia_RotateLeft(this);
            _commandRotateRight = new CommandMedia_RotateRight(this);
            rotateAngle = 0;
            visililityRotates = Visibility.Collapsed;
            visililityLeftRigth = Visibility.Collapsed;
        }
        private string _mediaPath;
        public string mediaPath
        {
            get
            {
                return _mediaPath;
            }
            set
            {
                if(_mediaPath != value)
                {
                    _mediaPath = value;
                    rotateAngle = 0;
                    if (string.IsNullOrWhiteSpace(_mediaPath))
                    {
                        visililityLeftRigth = Visibility.Hidden;
                        visililityRotates = Visibility.Hidden;

                        visDisplayImage = Visibility.Collapsed;
                        visDisplayVideo = Visibility.Collapsed;
                        return;
                    }

                    visililityLeftRigth = Visibility.Visible;
                    visililityRotates = Visibility.Visible;
                    string strExtension = "*" + Path.GetExtension(_mediaPath);
                    OnPropertyChanged("mediaPath");

                    // Check phần mở rộng là file ảnh
                    List<string> listTemp = ((App)Application.Current).GetListImageFormats();
                    if (listTemp != null && listTemp.Count() != 0)
                    {
                        if (listTemp.Exists(str=>str.ToUpper() == strExtension.ToUpper()))
                        {
                            visDisplayImage = Visibility.Visible;
                            visDisplayVideo = Visibility.Collapsed;
                            return;
                        }
                    }

                    visDisplayImage = Visibility.Collapsed;
                    visDisplayVideo = Visibility.Visible;
                    visililityRotates = Visibility.Hidden;
                }
            }
        }

        private Visibility _visDisplayVideo;
        public Visibility visDisplayVideo
        {
            get
            {
                return _visDisplayVideo;
            }
            set
            {
                if(_visDisplayVideo != value)
                {
                    _visDisplayVideo = value;
                    OnPropertyChanged("visDisplayVideo");
                }
            }
        }

        private Visibility _visDisplayImage;
        public Visibility visDisplayImage
        {
            get
            {
                return _visDisplayImage;
            }
            set
            {
                if (_visDisplayImage != value)
                {
                    _visDisplayImage = value;
                    OnPropertyChanged("visDisplayImage");
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

        private CommandMedia_RotateLeft _commandRotateLeft;
        public CommandMedia_RotateLeft commandRotateLeft
        {
            get
            {
                return _commandRotateLeft;
            }
        }

        private CommandMedia_RotateRight _commandRotateRight;
        public CommandMedia_RotateRight commandRotateRight
        {
            get
            {
                return _commandRotateRight;
            }
        }

        private Visibility _visililityLeftRigth;
        public Visibility visililityLeftRigth
        {
            get
            {
                return _visililityLeftRigth;
            }
            set
            {
                if(_visililityLeftRigth != value)
                {
                    _visililityLeftRigth = value;
                    OnPropertyChanged("visililityLeftRigth");
                }
            }
        }

        private Visibility _visililityRotates;
        public Visibility visililityRotates
        {
            get
            {
                return _visililityRotates;
            }

            set
            {
                if(_visililityRotates != value)
                {
                    _visililityRotates = value;
                    OnPropertyChanged("visililityRotates");
                }
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

            mediaPath = System.IO.Path.Combine(folderPath, listMediaFiles.ElementAt(index));
            if (!File.Exists(mediaPath))
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

            mediaPath = System.IO.Path.Combine(folderPath, listMediaFiles.ElementAt(index));
            if (!File.Exists(mediaPath))
            {
                // Ảnh bị xóa trong lúc đang xem thông tin
                // Cập nhật lại danh sách file ảnh
                GetAllMediaFiles();
            }
        }

        private double _rotateAngle;
        public double rotateAngle
        {
            get
            {
                return _rotateAngle;
            }

            set
            {
                if(_rotateAngle != value)
                {
                    _rotateAngle = value;
                    OnPropertyChanged("rotateAngle");
                }
            }
        }

        public void RotateLeft(Object parameter)
        {
            rotateAngle = rotateAngle - 90;
        }

        public void RotateRight(Object parameter)
        {
            rotateAngle = rotateAngle + 90;
        }

        private void InitDisplay()
        {
            GetAllMediaFiles();
            if (index != -1)
            {
                mediaPath = System.IO.Path.Combine(folderPath, listMediaFiles.ElementAt(index));
            }
            else
            {
                mediaPath = string.Empty;
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

            // Image formats
            List<string> listTemp = ((App)Application.Current).GetListImageFormats();
            if (listTemp != null && listTemp.Count() != 0)
            {
                foreach(string str in listTemp)
                {
                    listMediaFiles.AddRange(Directory.GetFiles(folderPath, str).ToList());
                }
                //listMediaFiles.AddRange(Directory.GetFiles(folderPath, "*.png").ToList());
                //listMediaFiles.AddRange(Directory.GetFiles(folderPath, "*.jpg").ToList());
                //listMediaFiles.AddRange(Directory.GetFiles(folderPath, "*.jpeg").ToList());
            }
            // Video formats
            listTemp = ((App)Application.Current).GetListVideoFormats();
            if (listTemp != null && listTemp.Count() != 0)
            {
                foreach (string str in listTemp)
                {
                    listMediaFiles.AddRange(Directory.GetFiles(folderPath, str).ToList());
                }
            }
            listMediaFiles.Sort();
            if (listMediaFiles.Count() != 0)
                index = 0;
        }
    }
}
