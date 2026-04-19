namespace BTL
{
    public partial class QuanLyNhanVien : Form
    {
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Đã ẩn dòng báo lỗi tài nguyên
            // System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanLyNhanVien));

            pictureBox1 = new PictureBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            label9 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
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
            button6 = new Button();
            label10 = new Label();
            column1 = new DataGridViewTextBoxColumn();
            column2 = new DataGridViewTextBoxColumn();
            column3 = new DataGridViewTextBoxColumn();
            column4 = new DataGridViewTextBoxColumn();
            column5 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();

            // pictureBox1
            // Đã xóa đoạn Load Image gây crash Build
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(143, 93);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.UseWaitCursor = true;
            pictureBox1.Click += pictureBox1_Click;

            // flowLayoutPanel1
            flowLayoutPanel1.BackColor = SystemColors.ActiveCaption;
            flowLayoutPanel1.Controls.Add(pictureBox1);
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(label9);
            flowLayoutPanel1.ForeColor = SystemColors.ControlDarkDark;
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1158, 99);
            flowLayoutPanel1.TabIndex = 1;
            flowLayoutPanel1.UseWaitCursor = true;

            // label1
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 22.2F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(152, 0);
            label1.Name = "label1";
            label1.Size = new Size(923, 93);
            label1.TabIndex = 1;
            label1.Text = "QUẢN LÝ NHÂN VIÊN - LUXURY BOUTIQUE.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.UseWaitCursor = true;
            label1.Click += label1_Click_1;

            // label9
            label9.AutoSize = true;
            label9.Location = new Point(1081, 0);
            label9.Name = "label9";
            label9.Size = new Size(50, 20);
            label9.TabIndex = 2;
            label9.Text = "label9";
            label9.UseWaitCursor = true;

            // label3
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(48, 149);
            label3.Name = "label3";
            label3.Size = new Size(207, 25);
            label3.TabIndex = 3;
            label3.Text = "Thông Tin Nhân VIên";
            label3.TextAlign = ContentAlignment.TopCenter;
            label3.Click += label3_Click;

            // label4
            label4.AutoSize = true;
            label4.Location = new Point(11, 125);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 4;
            label4.Text = "Chức Vụ";
            label4.Click += label4_Click;

            // label5
            label5.Location = new Point(4, 29);
            label5.Name = "label5";
            label5.Size = new Size(102, 20);
            label5.TabIndex = 5;
            label5.Text = "Mã Nhân Viên";
            label5.Click += label5_Click;

            // label6
            label6.AutoSize = true;
            label6.Location = new Point(11, 75);
            label6.Name = "label6";
            label6.Size = new Size(76, 20);
            label6.TabIndex = 6;
            label6.Text = "Họ Và Tên";
            label6.Click += label6_Click;

            // panel1
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.BackgroundImageLayout = ImageLayout.Center;
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
            panel1.Location = new Point(45, 162);
            panel1.Name = "panel1";
            panel1.Size = new Size(397, 579);
            panel1.TabIndex = 7;

            // comboBox2
            comboBox2.CausesValidation = false;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Chính Thức", "Chính Thức - Bán Thời Gian", "Thử Việc", "Thử Việc - Bán Thời Gian" });
            comboBox2.Location = new Point(141, 169);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(218, 28);
            comboBox2.TabIndex = 16;

            // comboBox1
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Giám Đốc", "Quản Lý", "Nhân Viên", "Thực Tập", "Tạp Vụ" });
            comboBox1.Location = new Point(141, 117);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(218, 28);
            comboBox1.TabIndex = 15;

            // label8
            label8.AutoSize = true;
            label8.Location = new Point(11, 236);
            label8.Name = "label8";
            label8.Size = new Size(95, 20);
            label8.TabIndex = 14;
            label8.Text = "Ảnh Cá Nhân";
            label8.Click += label8_Click;

            // button1
            button1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(141, 508);
            button1.Name = "button1";
            button1.Size = new Size(190, 40);
            button1.TabIndex = 13;
            button1.Text = "Chụp ảnh";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;

            // pictureBox2
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(141, 236);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(190, 234);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;

            // textBox2
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(141, 75);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(218, 27);
            textBox2.TabIndex = 9;
            textBox2.TextChanged += textBox2_TextChanged;

            // textBox1
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(141, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(218, 27);
            textBox1.TabIndex = 8;

            // label7
            label7.AutoSize = true;
            label7.Location = new Point(11, 177);
            label7.Name = "label7";
            label7.Size = new Size(78, 20);
            label7.TabIndex = 7;
            label7.Text = "Tình Trạng";
            label7.Click += label7_Click;

            // dataGridView1
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { column1, column2, column3, column4, column5 });
            dataGridView1.GridColor = SystemColors.WindowText;
            dataGridView1.Location = new Point(448, 162);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(645, 460);
            dataGridView1.TabIndex = 8;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

            // label2
            label2.AutoSize = true;
            label2.Location = new Point(448, 139);
            label2.Name = "label2";
            label2.Size = new Size(151, 20);
            label2.TabIndex = 9;
            label2.Text = "Danh Sách Nhân Viên";
            label2.Click += label2_Click;

            // button3
            button3.BackColor = SystemColors.ActiveCaption;
            button3.Location = new Point(483, 670);
            button3.Name = "button3";
            button3.Size = new Size(119, 43);
            button3.TabIndex = 11;
            button3.Text = "Thêm";
            button3.UseVisualStyleBackColor = false;

            // button4
            button4.BackColor = SystemColors.ScrollBar;
            button4.BackgroundImageLayout = ImageLayout.Center;
            button4.Location = new Point(631, 670);
            button4.Name = "button4";
            button4.Size = new Size(111, 43);
            button4.TabIndex = 12;
            button4.Text = "Xoá";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;

            // button5
            button5.BackColor = SystemColors.Info;
            button5.Location = new Point(772, 669);
            button5.Name = "button5";
            button5.Size = new Size(102, 43);
            button5.TabIndex = 13;
            button5.Text = "Sửa";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;

            // button6
            button6.BackColor = SystemColors.GradientInactiveCaption;
            button6.Location = new Point(902, 669);
            button6.Name = "button6";
            button6.Size = new Size(110, 43);
            button6.TabIndex = 14;
            button6.Text = "Lưu";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;

            // label10
            label10.BackColor = SystemColors.Info;
            label10.Location = new Point(1038, 670);
            label10.Name = "label10";
            label10.Size = new Size(93, 40);
            label10.TabIndex = 15;
            label10.Text = "Quay Lại";
            label10.TextAlign = ContentAlignment.MiddleCenter;

            // column1
            column1.FillWeight = 80F;
            column1.HeaderText = "Mã Nhân Viên";
            column1.MinimumWidth = 6;
            column1.Name = "column1";

            // column2
            column2.FillWeight = 44.7860947F;
            column2.HeaderText = "Họ Tên";
            column2.MinimumWidth = 6;
            column2.Name = "column2";

            // column3
            column3.FillWeight = 44.7860947F;
            column3.HeaderText = "Chức Vụ";
            column3.MinimumWidth = 6;
            column3.Name = "column3";

            // column4
            column4.FillWeight = 44.7860947F;
            column4.HeaderText = "Tình Trạng";
            column4.MinimumWidth = 6;
            column4.Name = "column4";

            // column5
            column5.FillWeight = 44.7860947F;
            column5.HeaderText = "Lương";
            column5.MinimumWidth = 6;
            column5.Name = "column5";

            // QuanLyNhanVien
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1182, 753);
            Controls.Add(label10);
            Controls.Add(button6);
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

            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e) { }
        private void button6_Click(object sender, EventArgs e) { }
        private void button5_Click(object sender, EventArgs e) { }

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
        private Button button6;
        private Label label9;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label10;
        private DataGridViewTextBoxColumn column1;
        private DataGridViewTextBoxColumn column2;
        private DataGridViewTextBoxColumn column3;
        private DataGridViewTextBoxColumn column4;
        private DataGridViewTextBoxColumn column5;
        private Label label8;
    }
}