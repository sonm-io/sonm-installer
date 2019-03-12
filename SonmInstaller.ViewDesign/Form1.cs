using System.Diagnostics;
using System.Windows.Forms;

namespace SonmInstaller
{
    public partial class WizardFormDesign : Form
    {
        public WizardFormDesign()
        {
            InitializeComponent();

            linkWelcome.Links.Add(new LinkLabel.Link(197, 13, "http://docs.sonm.com"));
            linkLabel1.Links.Add(new LinkLabel.Link(45, 8, "https://sonm-io.github.io/gui/"));
            linkFaq.Links.Add(new LinkLabel.Link(199, 13, "http://docs.sonm.com"));
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void progressBarBottom_Load(object sender, System.EventArgs e)
        {

        }
    }
}
