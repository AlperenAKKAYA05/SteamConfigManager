// ss.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SteamConfigManager
{
    public partial class PopUp : Form
    {
        public Label xx;
        private Timer popupTimer;
        private int timerInterval = 75;
        private int progressBarMaxValue = 108;
        private int progressBarx = 0;
        private string receivedText;

        public PopUp(string text)
        {
            InitializeComponent();
            InitializePopupTimer();
            receivedText = text;
        }

        private void InitializePopupTimer()
        {
            popupTimer = new Timer();
            popupTimer.Interval = timerInterval;
            popupTimer.Tick += PopupTimer_Tick;
        }

        private void PopupMessage_Load(object sender, EventArgs e)
        {
            messageLabel.Text = receivedText;
            messageLabel.AutoSize = false;

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 0);
            StartProgressBarAnimation();
        }

        private void StartProgressBarAnimation()
        {
            progressBar1.Value = 0;
            popupTimer.Start();
        }

        private void PopupTimer_Tick(object sender, EventArgs e)
        {
            if (progressBarx < progressBarMaxValue)
            {
                if (progressBar1.Value < 100)
                {
                    progressBarx++;
                    progressBar1.Value++;
                }
                else
                {
                    progressBarx++;
                    progressBar1.Value = 100;
                }
            }
            else
            {
                popupTimer.Stop();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetMessage(string message)
        {
            messageLabel.Text = message;
        }
    }
}
