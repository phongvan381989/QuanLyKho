using System.Windows;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace QuanLyKho.View
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
                //DisplayMedia.mediaFolder = dialog.FileName;
                //DisplayMedia.InitDisplay();
                //vmM
            }
        }
    }
}
