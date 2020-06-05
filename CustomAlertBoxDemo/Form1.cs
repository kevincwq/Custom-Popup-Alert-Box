using System;
using System.Windows.Forms;

namespace CustomAlertBoxDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Alert(string title, string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(title, msg, type);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Alert("Success Alert", "Request is conducted successfully", Form_Alert.enmType.Success);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Alert("Warning Alert", "This is a long message: there is a warning during downloading file from cloud server", Form_Alert.enmType.Warning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Alert("Error Alert", "Request failed because there is no internet connection", Form_Alert.enmType.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Alert("Info Alert", "The application will try again to download the license in 5 minutes", Form_Alert.enmType.Info);
        }
    }
}
