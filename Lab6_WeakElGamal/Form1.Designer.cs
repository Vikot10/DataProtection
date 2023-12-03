namespace Lab6_WeakElGamal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            textBox2 = new TextBox();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            button6 = new Button();
            button5 = new Button();
            label6 = new Label();
            label5 = new Label();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            label4 = new Label();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            button11 = new Button();
            button10 = new Button();
            button9 = new Button();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            button8 = new Button();
            button7 = new Button();
            textBox1 = new TextBox();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(textBox2);
            splitContainer1.Panel1.Controls.Add(label17);
            splitContainer1.Panel1.Controls.Add(label16);
            splitContainer1.Panel1.Controls.Add(label15);
            splitContainer1.Panel1.Controls.Add(button6);
            splitContainer1.Panel1.Controls.Add(button5);
            splitContainer1.Panel1.Controls.Add(label6);
            splitContainer1.Panel1.Controls.Add(label5);
            splitContainer1.Panel1.Controls.Add(button4);
            splitContainer1.Panel1.Controls.Add(button3);
            splitContainer1.Panel1.Controls.Add(button2);
            splitContainer1.Panel1.Controls.Add(label4);
            splitContainer1.Panel1.Controls.Add(button1);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(label3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(button11);
            splitContainer1.Panel2.Controls.Add(button10);
            splitContainer1.Panel2.Controls.Add(button9);
            splitContainer1.Panel2.Controls.Add(label14);
            splitContainer1.Panel2.Controls.Add(label13);
            splitContainer1.Panel2.Controls.Add(label12);
            splitContainer1.Panel2.Controls.Add(button8);
            splitContainer1.Panel2.Controls.Add(button7);
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Panel2.Controls.Add(label11);
            splitContainer1.Panel2.Controls.Add(label10);
            splitContainer1.Panel2.Controls.Add(label9);
            splitContainer1.Panel2.Controls.Add(label8);
            splitContainer1.Panel2.Controls.Add(label7);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Size = new Size(804, 537);
            splitContainer1.SplitterDistance = 402;
            splitContainer1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(12, 447);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(370, 73);
            textBox2.TabIndex = 20;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(12, 421);
            label17.Name = "label17";
            label17.Size = new Size(266, 23);
            label17.TabIndex = 19;
            label17.Text = "Декодированное сообщение:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(14, 369);
            label16.Name = "label16";
            label16.Size = new Size(35, 23);
            label16.TabIndex = 18;
            label16.Text = "b=";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(12, 324);
            label15.Name = "label15";
            label15.Size = new Size(34, 23);
            label15.TabIndex = 18;
            label15.Text = "a=";
            // 
            // button6
            // 
            button6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button6.Location = new Point(202, 283);
            button6.Name = "button6";
            button6.Size = new Size(187, 31);
            button6.TabIndex = 11;
            button6.Text = "Декодировать";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button5.Location = new Point(12, 283);
            button5.Name = "button5";
            button5.Size = new Size(187, 31);
            button5.TabIndex = 10;
            button5.Text = "Отправить G,P,Y";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(12, 247);
            label6.Name = "label6";
            label6.Size = new Size(35, 23);
            label6.TabIndex = 9;
            label6.Text = "Y=";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 202);
            label5.Name = "label5";
            label5.Size = new Size(35, 23);
            label5.TabIndex = 8;
            label5.Text = "X=";
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button4.Location = new Point(205, 72);
            button4.Name = "button4";
            button4.Size = new Size(187, 31);
            button4.TabIndex = 7;
            button4.Text = "Вычислить Y";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(12, 72);
            button3.Name = "button3";
            button3.Size = new Size(187, 31);
            button3.TabIndex = 6;
            button3.Text = "Сгенерировать X";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(205, 35);
            button2.Name = "button2";
            button2.Size = new Size(187, 31);
            button2.TabIndex = 5;
            button2.Text = "Сгенерировать G";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(12, 106);
            label4.Name = "label4";
            label4.Size = new Size(37, 23);
            label4.TabIndex = 4;
            label4.Text = "G=";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(12, 35);
            button1.Name = "button1";
            button1.Size = new Size(187, 31);
            button1.TabIndex = 3;
            button1.Text = "Сгенерировать Р";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 154);
            label2.Name = "label2";
            label2.Size = new Size(34, 23);
            label2.TabIndex = 2;
            label2.Text = "Р=";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(135, 9);
            label3.Name = "label3";
            label3.Size = new Size(113, 23);
            label3.TabIndex = 1;
            label3.Text = "Получатель";
            // 
            // button11
            // 
            button11.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button11.Location = new Point(286, 287);
            button11.Name = "button11";
            button11.Size = new Size(107, 31);
            button11.TabIndex = 22;
            button11.Text = "записать";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button10
            // 
            button10.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button10.Location = new Point(16, 477);
            button10.Name = "button10";
            button10.Size = new Size(187, 31);
            button10.TabIndex = 21;
            button10.Text = "Отправить a,b";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // button9
            // 
            button9.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button9.Location = new Point(208, 324);
            button9.Name = "button9";
            button9.Size = new Size(187, 31);
            button9.TabIndex = 21;
            button9.Text = "Вычислить a,b";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(17, 451);
            label14.Name = "label14";
            label14.Size = new Size(35, 23);
            label14.TabIndex = 17;
            label14.Text = "b=";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(19, 408);
            label13.Name = "label13";
            label13.Size = new Size(34, 23);
            label13.TabIndex = 16;
            label13.Text = "a=";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(19, 369);
            label12.Name = "label12";
            label12.Size = new Size(35, 23);
            label12.TabIndex = 15;
            label12.Text = "K=";
            // 
            // button8
            // 
            button8.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button8.Location = new Point(16, 324);
            button8.Name = "button8";
            button8.Size = new Size(187, 31);
            button8.TabIndex = 12;
            button8.Text = "Сгенерировать K";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button7
            // 
            button7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button7.Location = new Point(16, 287);
            button7.Name = "button7";
            button7.Size = new Size(264, 31);
            button7.TabIndex = 12;
            button7.Text = "Сгенерировать сообщение";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(16, 208);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(370, 73);
            textBox1.TabIndex = 14;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(16, 182);
            label11.Name = "label11";
            label11.Size = new Size(115, 23);
            label11.TabIndex = 13;
            label11.Text = "Сообщение:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(16, 32);
            label10.Name = "label10";
            label10.Size = new Size(99, 16);
            label10.TabIndex = 12;
            label10.Text = "От получателя:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(16, 144);
            label9.Name = "label9";
            label9.Size = new Size(35, 23);
            label9.TabIndex = 12;
            label9.Text = "Y=";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(16, 102);
            label8.Name = "label8";
            label8.Size = new Size(34, 23);
            label8.TabIndex = 12;
            label8.Text = "Р=";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(16, 55);
            label7.Name = "label7";
            label7.Size = new Size(37, 23);
            label7.TabIndex = 12;
            label7.Text = "G=";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(139, 9);
            label1.Name = "label1";
            label1.Size = new Size(125, 23);
            label1.TabIndex = 0;
            label1.Text = "Отправитель";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 537);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Label label1;
        private Label label3;
        private Button button1;
        private Label label2;
        private Button button6;
        private Button button5;
        private Label label6;
        private Label label5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Label label4;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Button button8;
        private Button button7;
        private TextBox textBox1;
        private Label label11;
        private TextBox textBox2;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Button button9;
        private Button button10;
        private Button button11;
    }
}