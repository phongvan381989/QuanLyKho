using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.ViewModelCommon
{

    public class ViewModelSubWindow : ViewModelBase
    {
        public ViewModelSubWindow()
        {
            commandEscapse = new CommandSubWindow(this);
        }

        private CommandSubWindow pcommandEscapse;
        public CommandSubWindow commandEscapse
        {
            get
            {
                return pcommandEscapse;
            }

            set
            {
                pcommandEscapse = value;
            }

        }
        public void Escapse(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
