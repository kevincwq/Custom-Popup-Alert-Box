using CustomAlertBoxDemo.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomAlertBoxDemo
{
    public partial class PopupAlert : Form
    {
        public PopupAlert()
        {
            InitializeComponent();

            lblTitle.Font = new Font(SystemFonts.DefaultFont.FontFamily, 12);
            lblMsg.Font = new Font(SystemFonts.DefaultFont.FontFamily, 10);
        }

        enum State
        {
            Waiting,
            Starting,
            Closing
        }

        public enum InfoType
        {
            Success,
            Warning,
            Error,
            Info
        }

        private State action;
        private int x, y;
        private const int AnimationInterval = 10;
        private int timeout;

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (this.action)
            {
                case State.Waiting:
                    timer1.Interval = timeout;
                    action = State.Closing;
                    break;
                case PopupAlert.State.Starting:
                    this.timer1.Interval = AnimationInterval;
                    this.Opacity += 0.1;
                    if (this.x < this.Location.X)
                    {
                        this.Left--;
                    }
                    else
                    {
                        if (this.Opacity == 1.0)
                        {
                            action = PopupAlert.State.Waiting;
                        }
                    }
                    break;
                case State.Closing:
                    timer1.Interval = AnimationInterval;
                    this.Opacity -= 0.1;
                    this.Left -= 3;
                    if (base.Opacity == 0.0)
                    {
                        base.Close();
                    }
                    break;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            action = State.Closing;
        }

        public void ShowAlert(string title, string msg, InfoType type, int timeout = 5000)
        {
            this.timeout = timeout;
            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 1; i < 8; i++)
            {
                fname = "alert" + i.ToString();
                PopupAlert frm = (PopupAlert)Application.OpenForms[fname];

                if (frm == null)
                {
                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                    // this.y = this.Height * (i - 1) + 5 * i;
                    this.Location = new Point(this.x, this.y);
                    break;
                }
            }
            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            switch (type)
            {
                case InfoType.Success:
                    this.pictureBox1.Image = Resources.success;
                    this.BackColor = Color.SeaGreen;
                    break;
                case InfoType.Error:
                    this.pictureBox1.Image = Resources.error;
                    this.BackColor = Color.DarkRed;
                    break;
                case InfoType.Info:
                    this.pictureBox1.Image = Resources.info;
                    this.BackColor = Color.RoyalBlue;
                    break;
                case InfoType.Warning:
                    this.pictureBox1.Image = Resources.warning;
                    this.BackColor = Color.DarkOrange;
                    break;
            }

            this.lblTitle.Text = title;
            this.lblMsg.Text = msg;

            this.Show();
            this.action = State.Starting;
            this.timer1.Interval = 1;
            this.timer1.Start();
        }
    }
}
