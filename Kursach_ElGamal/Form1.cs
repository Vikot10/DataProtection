namespace Kursach_ElGamal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Receiver _receiver = new Receiver();
        Sender _sender = new Sender();

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "P=" + _receiver.GenerateP().ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_receiver.p == 0)
            {
                MessageBox.Show("Сгенерируйте P");
                return;
            }
            label5.Text = "X=" + _receiver.GenerateX().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_receiver.p == 0)
            {
                MessageBox.Show("Сгенерируйте P");
                return;
            }
            label4.Text = "G=" + _receiver.GenerateG().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_receiver.p == 0 || _receiver.g == 0 || _receiver.x == 0)
            {
                MessageBox.Show("Сгенерируйте P,G,X");
                return;
            }
            label6.Text = "Y=" + _receiver.GenerateY().ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_receiver.p == 0 || _receiver.g == 0 || _receiver.y == 0)
            {
                MessageBox.Show("Сгенерируйте P,G,Y");
                return;
            }
            label7.Text = "G=" + _receiver.g.ToString();
            label8.Text = "P=" + _receiver.p.ToString();
            label9.Text = "Y=" + _receiver.y.ToString();
            _sender.Receive(_receiver.g, _receiver.p, _receiver.y);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (_receiver.a == 0 || _receiver.b == 0)
            {
                MessageBox.Show("a,b нет у получаетля");
                return;
            }
            textBox2.Text = _receiver.Decode().ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = _sender.GenerateMessage().ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (_sender.p == 0)
            {
                MessageBox.Show("Ключа P нет у отправителя");
                return;
            }
            label12.Text = "K=" + _sender.GenerateK().ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (_sender.a == 0 || _sender.b == 0)
            {
                MessageBox.Show("a,b не сгенерированы");
                return;
            }
            label15.Text = "a=" + _sender.a.ToString();
            label16.Text = "b=" + _sender.b.ToString();
            _receiver.Receive(_sender.a, _sender.b);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (_sender.g == 0 || _sender.k == 0 || _sender.p == 0 || _sender.y == 0 || _sender.message == 0)
            {
                MessageBox.Show("Ключей G,K,P,Y и сообщения нет у отправителя");
                return;
            }
            label13.Text = "a=" + _sender.GenerateA().ToString();
            label14.Text = "b=" + _sender.GenerateB().ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            _sender.message = uint.Parse(textBox1.Text);
        }
    }
}