using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SonmInstaller.ViewModel;

namespace SonmInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var x = X.x;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void updateButton(ActionButtonState state, IEnumerable<ButtonChange> changes)
        {

        }

        private void updateView(UiState state, UiStateChange change)
        {
            if (change.IsBackButton)
            {
                updateButton(state.BackButton, ((UiStateChange.BackButton)change).Item);
            }
            else if (change.IsNextButton)
            {
                updateButton(state.NextButton, ((UiStateChange.NextButton)change).Item);
            }

        }

        private void updateView(UiState state, IEnumerable<UiStateChange> changes)
        {
            foreach (var change in changes)
            {
                updateView(state, change);
            }




            //if (containsType(changes, typeof(UiStateChange.BackButton))
            //{
            //    updateButton(state.BackButton, changes)
            //}
        }

    }
}
