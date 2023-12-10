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
        BigInt zero = new BigInt(0);

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "P=" + _receiver.GenerateP().ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_receiver.p == zero)
            {
                MessageBox.Show("Сгенерируйте P");
                return;
            }
            label5.Text = "X=" + _receiver.GenerateX().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_receiver.p == zero)
            {
                MessageBox.Show("Сгенерируйте P");
                return;
            }
            label4.Text = "G=" + _receiver.GenerateG().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_receiver.p == zero || _receiver.g == zero || _receiver.x == zero)
            {
                MessageBox.Show("Сгенерируйте P,G,X");
                return;
            }
            label6.Text = "Y=" + _receiver.GenerateY().ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_receiver.p == zero || _receiver.g == zero || _receiver.y == zero)
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
            if (_receiver.a == zero || _receiver.b == null)
            {
                MessageBox.Show("a,b нет у получаетля");
                return;
            }
            textBox2.Text = _receiver.Decode();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (_sender.p == zero)
            {
                MessageBox.Show("Ключа P нет у отправителя");
                return;
            }
            label12.Text = "K=" + _sender.GenerateK().ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (_sender.a == zero || _sender.b == null)
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
            if (_sender.g == zero || _sender.k == zero || _sender.p == zero || _sender.y == zero || _sender.message == "")
            {
                MessageBox.Show("Ключей G,K,P,Y и сообщения нет у отправителя");
                return;
            }
            label13.Text = "a=" + _sender.GenerateA().ToString();
            label14.Text = "b=" + _sender.GenerateB().ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            _sender.message = textBox1.Text;
        }
    }
}