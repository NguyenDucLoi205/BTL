namespace BTL
{
    partial class QuanLyNhanVien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            textBox3 = new TextBox();
            label9 = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label8 = new Label();
            button1 = new Button();
            pictureBox2 = new PictureBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label7 = new Label();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Logo5;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(143, 93);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.UseWaitCursor = true;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.ActiveCaption;
            flowLayoutPanel1.Controls.Add(pictureBox1);
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.ForeColor = SystemColors.ControlDarkDark;
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1116, 99);
            flowLayoutPanel1.TabIndex = 1;
            flowLayoutPanel1.UseWaitCursor = true;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 22.2F);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(152, 0);
            label1.Name = "label1";
            label1.Size = new Size(948, 93);
            label1.TabIndex = 1;
            label1.Text = "QUẢN LÝ NHÂN VIÊN - LUXURY BOUTIQUE.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.UseWaitCursor = true;
            label1.Click += label1_Click_1;
            // 
            // label3
            // 
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(42, 128);
            label3.Name = "label3";
            label3.Size = new Size(207, 25);
            label3.TabIndex = 3;
            label3.Text = "Thông Tin Nhân VIên";
            label3.TextAlign = ContentAlignment.TopCenter;
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 125);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 4;
            label4.Text = "Chức Vụ";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.Location = new Point(4, 29);
            label5.Name = "label5";
            label5.Size = new Size(102, 20);
            label5.TabIndex = 5;
            label5.Text = "Mã Nhân Viên";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(11, 82);
            label6.Name = "label6";
            label6.Size = new Size(76, 20);
            label6.TabIndex = 6;
            label6.Text = "Họ Và Tên";
            label6.Click += label6_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.BackgroundImageLayout = ImageLayout.Center;
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Cursor = Cursors.SizeWE;
            panel1.Location = new Point(39, 141);
            panel1.Name = "panel1";
            panel1.Size = new Size(397, 490);
            panel1.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Location = new Point(141, 219);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(218, 27);
            textBox3.TabIndex = 18;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(11, 226);
            label9.Name = "label9";
            label9.Size = new Size(51, 20);
            label9.TabIndex = 17;
            label9.Text = "Lương";
            // 
            // comboBox2
            // 
            comboBox2.CausesValidation = false;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Chính Thức", "Chính Thức - Bán Thời Gian", "Thử Việc", "Thử Việc - Bán Thời Gian" });
            comboBox2.Location = new Point(141, 169);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(218, 28);
            comboBox2.TabIndex = 16;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Giám Đốc", "Quản Lý", "Nhân Viên", "Thực Tập", "Tạp Vụ" });
            comboBox1.Location = new Point(141, 117);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(218, 28);
            comboBox1.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(24, 270);
            label8.Name = "label8";
            label8.Size = new Size(95, 20);
            label8.TabIndex = 14;
            label8.Text = "Ảnh Cá Nhân";
            label8.Click += label8_Click;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Font = new Font("Times New Roman", 12F);
            button1.Location = new Point(11, 307);
            button1.Name = "button1";
            button1.Size = new Size(121, 42);
            button1.TabIndex = 13;
            button1.Text = "Chụp ảnh";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(180, 270);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(160, 190);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(141, 75);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(218, 27);
            textBox2.TabIndex = 9;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(141, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(218, 27);
            textBox1.TabIndex = 8;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 177);
            label7.Name = "label7";
            label7.Size = new Size(78, 20);
            label7.TabIndex = 7;
            label7.Text = "Tình Trạng";
            label7.Click += label7_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = SystemColors.WindowText;
            dataGridView1.Location = new Point(442, 141);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(645, 460);
            dataGridView1.TabIndex = 8;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(442, 118);
            label2.Name = "label2";
            label2.Size = new Size(151, 20);
            label2.TabIndex = 9;
            label2.Text = "Danh Sách Nhân Viên";
            label2.Click += label2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(200, 200, 200);
            button3.ForeColor = Color.FromArgb(100, 100, 100);
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.Location = new Point(464, 635);
            button3.Name = "button3";
            button3.Size = new Size(119, 43);
            button3.TabIndex = 11;
            button3.Text = "Thêm";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(200, 200, 200);
            button4.ForeColor = Color.FromArgb(100, 100, 100);
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.BackgroundImageLayout = ImageLayout.Center;
            button4.Location = new Point(627, 635);
            button4.Name = "button4";
            button4.Size = new Size(111, 43);
            button4.TabIndex = 12;
            button4.Text = "Xoá";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(200, 200, 200);
            button5.ForeColor = Color.FromArgb(100, 100, 100);
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button5.Location = new Point(761, 637);
            button5.Name = "button5";
            button5.Size = new Size(102, 43);
            button5.TabIndex = 13;
            button5.Text = "Sửa";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(200, 200, 200);
            button2.ForeColor = Color.FromArgb(100, 100, 100);
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Location = new Point(888, 635);
            button2.Name = "button2";
            button2.Size = new Size(102, 43);
            button2.TabIndex = 14;
            button2.Text = "Quay về";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // QuanLyNhanVien
            // 
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1112, 703);
            Controls.Add(button2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label3);
            Controls.Add(panel1);
            Name = "QuanLyNhanVien";
            Text = "Quản Lý Nhân Viên";
            Load += QuanLyNhanVien_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Khai báo biến
        private PictureBox pictureBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private Label label7;
        private TextBox textBox2;
        private TextBox textBox1;
        private PictureBox pictureBox2;
        private Button button1;
        private DataGridView dataGridView1;
        private Label label2;
        private Button button3;
        private Button button4;
        private Button button5;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label8;
        private TextBox textBox3;
        private Label label9;
        private Button button2;
    }
}
