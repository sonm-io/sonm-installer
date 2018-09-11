using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonmInstaller.UI
{
    public partial class ProgressBar : UserControl
    {
        public ProgressBar()
        {
            InitializeComponent();
        }

        public int _progressTotal;
        public int ProgressTotal
        {
            get
            {
                return _progressTotal;
            }
            set
            {
                _progressTotal = value;
                updateView();
            }
        }

        public int _progressCurrent;
        public int ProgressCurrent
        {
            get
            {
                return _progressCurrent;
            }
            set
            {
                _progressCurrent = value;
                updateView();
            }
        }

        private string _labelTpl;
        public string LabelTpl
        {
            get
            {
                return _labelTpl;
            }
            set
            {
                _labelTpl = value;
                lblProgress.Text = value;
                updateView();
            }
        }

        private void updateView()
        {
            lblProgress.Text = string.Format(_labelTpl, ProgressCurrent, ProgressTotal);
            double percentage = ProgressCurrent / ProgressTotal * 100;
            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }
    }
}
