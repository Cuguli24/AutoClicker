using System.Runtime.InteropServices;

namespace AutoClicker1._0
{
    public partial class Form1 : Form
    {
        int i = 0;
        int total = 0;
        bool çalışıyor = false;
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        public Form1()
        {
            InitializeComponent();
        }

        private void kapat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void aşağıal_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            setInterval();
        }

        private void setInterval()
        {
            int saatgiriş = Convert.ToInt32(saat.Value);
            saatgiriş = 3600000 * saatgiriş;

            int dakikagiriş = Convert.ToInt32(dakika.Value);
            dakikagiriş = 60000 * dakikagiriş;

            int saniyegiriş = Convert.ToInt32(saniye.Value);
            saniyegiriş = 1000 * saniyegiriş;

            int salisegirş = Convert.ToInt32(milisaniye.Value);

            total = saatgiriş + dakikagiriş + saniyegiriş + salisegirş;

            if (total == 0)
            {
                MessageBox.Show("Please Enter A Time");
            }

            else
            {
                timer1.Interval = total;
                this.WindowState = FormWindowState.Minimized;
                timer1.Start();
                çalışıyor = true;
            }
        }

        private void çalışma()
        {
            if (çalışıyor == false)
            {
                if (GetAsyncKeyState(0x26) < 0)
                {
                    setInterval();
                    timer1.Start();
                    çalışıyor = true;
                }
            }
            else
            {
                if (GetAsyncKeyState(0x28) < 0)
                {
                    timer1.Stop();
                    çalışıyor = false;
                }
            }
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Top Arrow (↑) (Start)//Down Arrow (↓) (Stop)");
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            çalışma();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (!Bounds.Contains(PointToClient(MousePosition)))
            {

                if (leftbtn.Checked)
                {
                    mouse_event(0x02, 0, 0, 0, 0);
                    mouse_event(0x04, 0, 0, 0, 0);
                }

                else if (rightbtn.Checked)
                {
                    mouse_event(0x08, 0, 0, 0, 0);
                    mouse_event(0x10, 0, 0, 0, 0);
                }

                else
                {
                    MessageBox.Show("Something Went Wrong");
                }

                if (repeatsev.Checked)
                {
                    if (i < Convert.ToInt32(sevTime.Value))
                    {
                        i++;
                    }

                    else
                    {
                        timer1.Stop();
                        i = 0;
                        total = 0;
                        this.WindowState = FormWindowState.Normal;
                    }

                }

                else if (repeatİnf.Checked)
                {
                    return;
                }

                else
                {
                    MessageBox.Show("Something Went Wrong !");
                }
            }
        }
    }
}
