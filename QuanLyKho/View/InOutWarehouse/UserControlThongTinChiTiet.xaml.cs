using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using QuanLyKho.General;

namespace QuanLyKho.View.InOutWarehouse
{
    /// <summary>
    /// Interaction logic for UserControlThongTinChiTiet.xaml
    /// </summary>
    public partial class UserControlThongTinChiTiet : System.Windows.Controls.UserControl
    {
        public UserControlThongTinChiTiet()
        {
            InitializeComponent();
        }

        private void MediaBrowse_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ThuMucMedia.Text = dialog.FileName;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            bool b = MaSanPham.Focus();
            IInputElement focusedElement = FocusManager.GetFocusedElement(this);
            int x = 10;

            //Keyboard.Focus(MaSanPham);
        }

        private void MaSanPham_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MyLogger.GetInstance().Debug("MaSanPham_GotKeyboardFocus");
            IInputElement focusedElement = FocusManager.GetFocusedElement(MaSanPham);
            var obj2 = FocusManager.GetFocusScope(MaSanPham);
        }
    }
}
