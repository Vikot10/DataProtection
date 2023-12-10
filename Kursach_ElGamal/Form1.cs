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
            textBox4.Text = _receiver.GenerateP().ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_receiver.p == zero)
            {
                MessageBox.Show("Сгенерируйте P");
                return;
            }
            textBox5.Text = _receiver.GenerateX().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_receiver.p == zero)
            {
                MessageBox.Show("Сгенерируйте P");
                return;
            }
            textBox3.Text = _receiver.GenerateG().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_receiver.p == zero || _receiver.g == zero || _receiver.x == zero)
            {
                MessageBox.Show("Сгенерируйте P,G,X");
                return;
            }
            textBox6.Text = _receiver.GenerateY().ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_receiver.p == zero || _receiver.g == zero || _receiver.y == zero)
            {
                MessageBox.Show("Сгенерируйте P,G,Y");
                return;
            }
            textBox7.Text = _receiver.g.ToString();
            textBox8.Text = _receiver.p.ToString();
            textBox9.Text = _receiver.y.ToString();
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
            textBox12.Text = _sender.GenerateK().ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (_sender.a == zero || _sender.b == null)
            {
                MessageBox.Show("a,b не сгенерированы");
                return;
            }
            textBox10.Text = _sender.a.ToString();
            textBox11.Text = "";
            var bb = _sender.b;
            for (int i = 0; i < bb.Count; i++)
            {
                textBox11.Text += $"b{i} = {bb[i]} \n";
            }
            _receiver.Receive(_sender.a, _sender.b);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (_sender.g == zero || _sender.k == zero || _sender.p == zero || _sender.y == zero || _sender.message == "")
            {
                MessageBox.Show("Ключей G,K,P,Y и сообщения нет у отправителя");
                return;
            }
            textBox13.Text = _sender.GenerateA().ToString();
            textBox14.Text = "";
            var bb = _sender.GenerateB();
            for (int i = 0; i < bb.Count; i++)
            {
                textBox14.Text += $"b{i} = {bb[i]} \n";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            _sender.message = textBox1.Text;
        }
    }
}