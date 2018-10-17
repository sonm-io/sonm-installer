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

        public double _progressTotal = 100;
        public double ProgressTotal
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

        public double _progressCurrent = 0;
        public double ProgressCurrent
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

        private string _labelTpl = "Progress {0:0.0} of {1:0.0} ({2:0}%)";
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
            if (string.IsNullOrEmpty(LabelTpl) || ProgressTotal == 0)
                return;
            double percentage = ProgressCurrent / ProgressTotal * 100;
            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            lblProgress.Text = string.Format(_labelTpl, ProgressCurrent, ProgressTotal, percentage);
        }
    }
}
