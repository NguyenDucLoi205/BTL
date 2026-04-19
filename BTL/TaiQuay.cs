using System;
using System.Drawing;
using System.Windows.Forms;

namespace luxury
{
    public partial class TaiQuay : Form
    {
        public TaiQuay()
        {

            SetupMyCustomLayout(); // Gọi hàm tạo giao diện
            this.Show(); // Thêm dòng này để chắc chắn nó hiện ra
        }

        private void SetupMyCustomLayout()
        {
            // Cấu hình Form chính
            this.Text = "QUẢN LÝ NHÂN VIÊN - LUXURY BOUTIQUE";
            this.Size = new Size(1000, 600);
            this.BackColor = Color.FromArgb(240, 240, 240);

            // 1. Tiêu đề (Header)
            Label lblHeader = new Label();
            lblHeader.Text = "QUẢN LÝ NHÂN VIÊN - LUXURY BOUTIQUE";
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Height = 60;
            lblHeader.BackColor = Color.FromArgb(44, 62, 80);
            lblHeader.ForeColor = Color.White;
            this.Controls.Add(lblHeader);

            // 2. GroupBox Thông tin chi tiết
            GroupBox gbDetails = new GroupBox { Text = "THÔNG TIN CHI TIẾT", Location = new Point(20, 80), Size = new Size(350, 450), Font = new Font("Segoe UI", 10) };
            this.Controls.Add(gbDetails);

            // Các Control nhập liệu
            AddInputLabel(gbDetails, "Mã NV:", 40, new TextBox { Name = "txtMaNV", Width = 200, Location = new Point(120, 37) });
            AddInputLabel(gbDetails, "Họ Tên:", 90, new TextBox { Name = "txtHoTen", Width = 200, Location = new Point(120, 87) });
            AddInputLabel(gbDetails, "Chức Vụ:", 140, new ComboBox { Name = "cbChucVu", Width = 200, Location = new Point(120, 137) });
            AddInputLabel(gbDetails, "Tình Trạng:", 190, new ComboBox { Name = "cbTinhTrạng", Width = 200, Location = new Point(120, 187) });

            PictureBox pic = new PictureBox { Name = "picAnh", Size = new Size(120, 150), Location = new Point(120, 240), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.White };
            Button btnAnh = new Button { Text = "Chọn ảnh", Location = new Point(120, 400), Width = 120 };
            gbDetails.Controls.AddRange(new Control[] { pic, btnAnh });

            // 3. GroupBox Danh sách
            GroupBox gbList = new GroupBox { Text = "DANH SÁCH NHÂN VIÊN", Location = new Point(390, 80), Size = new Size(570, 380), Font = new Font("Segoe UI", 10) };
            DataGridView dgv = new DataGridView { Name = "dgvNhanVien", Dock = DockStyle.Fill, BackgroundColor = Color.White, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gbList.Controls.Add(dgv);
            this.Controls.Add(gbList);

            // 4. Các nút bấm
            FlowLayoutPanel flp = new FlowLayoutPanel { Location = new Point(390, 475), Size = new Size(570, 60) };
            string[] btns = { "THÊM", "SỬA", "XÓA", "LƯU" };
            foreach (var b in btns)
            {
                Button btn = new Button { Text = b, Width = 100, Height = 45, Margin = new Padding(5) };
                if (b == "LƯU") btn.BackColor = Color.LightGreen;
                flp.Controls.Add(btn);
            }
            this.Controls.Add(flp);
        }

        private void AddInputLabel(GroupBox gb, string text, int y, Control input)
        {
            gb.Controls.Add(new Label { Text = text, Location = new Point(20, y), AutoSize = true });
            gb.Controls.Add(input);
        }

        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            radioButton3 = new RadioButton();
            label23 = new Label();
            label12 = new Label();
            pictureBox2 = new PictureBox();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            numericUpDown2 = new NumericUpDown();
            comboBox1 = new ComboBox();
            textBox8 = new TextBox();
            textBox6 = new TextBox();
            label9 = new Label();
            label13 = new Label();
            label14 = new Label();
            textBox7 = new TextBox();
            textBox9 = new TextBox();
            textBox10 = new TextBox();
            textBox11 = new TextBox();
            label15 = new Label();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            label11 = new Label();
            label10 = new Label();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            label21 = new Label();
            label22 = new Label();
            textBox12 = new TextBox();
            textBox13 = new TextBox();
            label24 = new Label();
            column1 = new DataGridViewTextBoxColumn();
            column2 = new DataGridViewTextBoxColumn();
            column3 = new DataGridViewTextBoxColumn();
            column4 = new DataGridViewTextBoxColumn();
            column5 = new DataGridViewTextBoxColumn();
            column6 = new DataGridViewTextBoxColumn();
            column7 = new DataGridViewTextBoxColumn();
            column8 = new DataGridViewTextBoxColumn();
            column9 = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1419, 70);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(121, 23);
            label1.Name = "label1";
            label1.Size = new Size(1295, 47);
            label1.TabIndex = 1;
            label1.Text = "Quản Lý Thanh Toán - Cửa Hàng Luxury Boutique\r\n\r\n";
            label1.TextAlign = ContentAlignment.BottomCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = BTL.Properties.Resources.Logo1;
            pictureBox1.Location = new Point(0, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(115, 71);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBox13);
            panel2.Controls.Add(label24);
            panel2.Controls.Add(radioButton3);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(button6);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(numericUpDown2);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(textBox8);
            panel2.Controls.Add(textBox6);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(label14);
            panel2.Controls.Add(textBox7);
            panel2.Controls.Add(textBox9);
            panel2.Controls.Add(textBox10);
            panel2.Controls.Add(textBox11);
            panel2.Controls.Add(label15);
            panel2.Controls.Add(radioButton2);
            panel2.Controls.Add(radioButton1);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(label17);
            panel2.Controls.Add(label18);
            panel2.Controls.Add(label19);
            panel2.Controls.Add(label20);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(textBox5);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(12, 79);
            panel2.Name = "panel2";
            panel2.Size = new Size(743, 762);
            panel2.TabIndex = 1;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.BackColor = SystemColors.ActiveCaption;
            radioButton3.Location = new Point(295, 246);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(73, 24);
            radioButton3.TabIndex = 58;
            radioButton3.TabStop = true;
            radioButton3.Text = "Unisex";
            radioButton3.UseVisualStyleBackColor = false;
            // 
            // label23
            // 
            label23.BackColor = SystemColors.ActiveCaption;
            label23.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label23.Location = new Point(1276, 762);
            label23.Name = "label23";
            label23.Size = new Size(152, 41);
            label23.TabIndex = 57;
            label23.Text = "Quay Lại";
            label23.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(528, 589);
            label12.Name = "label12";
            label12.Size = new Size(104, 20);
            label12.TabIndex = 56;
            label12.Text = "Ảnh Sản Phẩm";
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(436, 290);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(280, 272);
            pictureBox2.TabIndex = 55;
            pictureBox2.TabStop = false;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.MenuBar;
            button6.Location = new Point(188, 693);
            button6.Name = "button6";
            button6.Size = new Size(115, 45);
            button6.TabIndex = 53;
            button6.Text = "Làm Mới";
            button6.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.GradientInactiveCaption;
            button5.Location = new Point(515, 650);
            button5.Name = "button5";
            button5.Size = new Size(131, 37);
            button5.TabIndex = 52;
            button5.Text = "Chỉnh Sửa";
            button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.InactiveBorder;
            button4.Location = new Point(414, 701);
            button4.Name = "button4";
            button4.Size = new Size(126, 37);
            button4.TabIndex = 51;
            button4.Text = "Huỷ";
            button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ControlLight;
            button3.Location = new Point(295, 650);
            button3.Name = "button3";
            button3.Size = new Size(136, 37);
            button3.TabIndex = 50;
            button3.Text = "Xoá";
            button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.Location = new Point(28, 650);
            button2.Name = "button2";
            button2.Size = new Size(151, 37);
            button2.TabIndex = 49;
            button2.Text = "Thêm";
            button2.UseVisualStyleBackColor = false;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(142, 548);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(238, 27);
            numericUpDown2.TabIndex = 47;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "S", "L", "XL", "XXL" });
            comboBox1.Location = new Point(142, 457);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(238, 28);
            comboBox1.TabIndex = 46;
            // 
            // textBox8
            // 
            textBox8.BorderStyle = BorderStyle.FixedSingle;
            textBox8.Location = new Point(142, 500);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(238, 27);
            textBox8.TabIndex = 45;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(145, 590);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(235, 27);
            textBox6.TabIndex = 44;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(25, 597);
            label9.Name = "label9";
            label9.Size = new Size(31, 20);
            label9.TabIndex = 43;
            label9.Text = "Giá";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(25, 507);
            label13.Name = "label13";
            label13.Size = new Size(70, 20);
            label13.TabIndex = 42;
            label13.Text = "Chất Liệu";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(25, 555);
            label14.Name = "label14";
            label14.Size = new Size(72, 20);
            label14.TabIndex = 41;
            label14.Text = "Số Lượng";
            // 
            // textBox7
            // 
            textBox7.BorderStyle = BorderStyle.FixedSingle;
            textBox7.Location = new Point(515, 239);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(201, 27);
            textBox7.TabIndex = 40;
            // 
            // textBox9
            // 
            textBox9.BorderStyle = BorderStyle.FixedSingle;
            textBox9.Location = new Point(142, 366);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(238, 27);
            textBox9.TabIndex = 39;
            // 
            // textBox10
            // 
            textBox10.BorderStyle = BorderStyle.FixedSingle;
            textBox10.Location = new Point(142, 327);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(238, 27);
            textBox10.TabIndex = 38;
            // 
            // textBox11
            // 
            textBox11.BorderStyle = BorderStyle.FixedSingle;
            textBox11.Location = new Point(142, 283);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(238, 27);
            textBox11.TabIndex = 37;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(396, 246);
            label15.Name = "label15";
            label15.Size = new Size(99, 20);
            label15.TabIndex = 33;
            label15.Text = "Mã Sản Phẩm";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.BackColor = SystemColors.ActiveCaption;
            radioButton2.Location = new Point(220, 246);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(50, 24);
            radioButton2.TabIndex = 36;
            radioButton2.TabStop = true;
            radioButton2.Text = "Nữ";
            radioButton2.UseVisualStyleBackColor = false;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.BackColor = SystemColors.ActiveCaption;
            radioButton1.Location = new Point(142, 246);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(62, 24);
            radioButton1.TabIndex = 35;
            radioButton1.TabStop = true;
            radioButton1.Text = "Nam";
            radioButton1.UseVisualStyleBackColor = false;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(25, 465);
            label16.Name = "label16";
            label16.Size = new Size(59, 20);
            label16.TabIndex = 34;
            label16.Text = "Kích Cỡ";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(25, 371);
            label17.Name = "label17";
            label17.Size = new Size(51, 20);
            label17.TabIndex = 32;
            label17.Text = "Mô Tả";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(25, 325);
            label18.Name = "label18";
            label18.Size = new Size(101, 20);
            label18.TabIndex = 31;
            label18.Text = "Tên Sản Phẩm";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(25, 283);
            label19.Name = "label19";
            label19.Size = new Size(106, 20);
            label19.TabIndex = 30;
            label19.Text = "Loại Sản Phẩm";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(25, 246);
            label20.Name = "label20";
            label20.Size = new Size(61, 20);
            label20.TabIndex = 29;
            label20.Text = "Loại Đồ";
            // 
            // label11
            // 
            label11.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(28, 212);
            label11.Name = "label11";
            label11.Size = new Size(133, 34);
            label11.TabIndex = 15;
            label11.Text = "Sản Phẩm";
            // 
            // label10
            // 
            label10.BorderStyle = BorderStyle.Fixed3D;
            label10.Location = new Point(1, 192);
            label10.Name = "label10";
            label10.Size = new Size(742, 14);
            label10.TabIndex = 14;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(505, 119);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(193, 27);
            textBox5.TabIndex = 12;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(505, 74);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(193, 27);
            textBox4.TabIndex = 11;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(396, 17);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 10;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(231, 151);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(230, 27);
            textBox3.TabIndex = 9;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(160, 112);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(220, 27);
            textBox2.TabIndex = 8;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(160, 74);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(220, 27);
            textBox1.TabIndex = 7;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(28, 158);
            label8.Name = "label8";
            label8.Size = new Size(186, 20);
            label8.TabIndex = 6;
            label8.Text = "Số Điện Thoại Khách Hàng";
            // 
            // label7
            // 
            label7.Location = new Point(396, 74);
            label7.Name = "label7";
            label7.Size = new Size(123, 20);
            label7.TabIndex = 5;
            label7.Text = "Mã Nhân Viên";
            // 
            // label6
            // 
            label6.Location = new Point(396, 119);
            label6.Name = "label6";
            label6.Size = new Size(112, 20);
            label6.TabIndex = 4;
            label6.Text = "Tên Nhân Viên";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(266, 17);
            label5.Name = "label5";
            label5.Size = new Size(103, 20);
            label5.TabIndex = 3;
            label5.Text = "Ngày Hiện Tại";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 74);
            label4.Name = "label4";
            label4.Size = new Size(104, 20);
            label4.TabIndex = 2;
            label4.Text = "Mã Hợp Đồng";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 119);
            label3.Name = "label3";
            label3.Size = new Size(116, 20);
            label3.TabIndex = 1;
            label3.Text = "Tên Khách Hàng";
            // 
            // label2
            // 
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(28, 22);
            label2.Name = "label2";
            label2.Size = new Size(213, 25);
            label2.TabIndex = 0;
            label2.Text = "Thông Tin Hoá Đơn";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { column1, column2, column3, column4, column5, column6, column7, column8, column9 });
            dataGridView1.Location = new Point(761, 79);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(670, 492);
            dataGridView1.TabIndex = 4;
            // 
            // label21
            // 
            label21.BackColor = SystemColors.Control;
            label21.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label21.Location = new Point(761, 589);
            label21.Name = "label21";
            label21.Size = new Size(171, 35);
            label21.TabIndex = 5;
            label21.Text = "Thanh Toán";
            label21.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            label22.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label22.Location = new Point(780, 665);
            label22.Name = "label22";
            label22.Size = new Size(133, 25);
            label22.TabIndex = 6;
            label22.Text = "Tổng Cộng";
            label22.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox12
            // 
            textBox12.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox12.BorderStyle = BorderStyle.FixedSingle;
            textBox12.Location = new Point(1001, 651);
            textBox12.Multiline = true;
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(233, 44);
            textBox12.TabIndex = 7;
            textBox12.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox13
            // 
            textBox13.BorderStyle = BorderStyle.FixedSingle;
            textBox13.Location = new Point(142, 409);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(238, 27);
            textBox13.TabIndex = 60;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(28, 416);
            label24.Name = "label24";
            label24.Size = new Size(65, 20);
            label24.TabIndex = 59;
            label24.Text = "Màu Sắc";
            // 
            // column1
            // 
            column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column1.FillWeight = 40F;
            column1.HeaderText = "Giới Tính";
            column1.MinimumWidth = 6;
            column1.Name = "column1";
            column1.Width = 85;
            // 
            // column2
            // 
            column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column2.HeaderText = "Mã SP";
            column2.MinimumWidth = 6;
            column2.Name = "column2";
            column2.Width = 88;
            // 
            // column3
            // 
            column3.HeaderText = "Tên SP";
            column3.MinimumWidth = 6;
            column3.Name = "column3";
            // 
            // column4
            // 
            column4.HeaderText = "Mô Tả";
            column4.MinimumWidth = 6;
            column4.Name = "column4";
            // 
            // column5
            // 
            column5.HeaderText = "Màu Sắc";
            column5.MinimumWidth = 6;
            column5.Name = "column5";
            // 
            // column6
            // 
            column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column6.FillWeight = 50F;
            column6.HeaderText = "SIZE";
            column6.MinimumWidth = 6;
            column6.Name = "column6";
            column6.Width = 44;
            // 
            // column7
            // 
            column7.HeaderText = "Material";
            column7.MinimumWidth = 6;
            column7.Name = "column7";
            // 
            // column8
            // 
            column8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column8.HeaderText = "SL";
            column8.MinimumWidth = 6;
            column8.Name = "column8";
            column8.Width = 50;
            // 
            // column9
            // 
            column9.HeaderText = "Giá";
            column9.MinimumWidth = 6;
            column9.Name = "column9";
            // 
            // TaiQuay
            // 
            ClientSize = new Size(1442, 853);
            Controls.Add(textBox12);
            Controls.Add(label23);
            Controls.Add(label22);
            Controls.Add(label21);
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "TaiQuay";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label1;
        private Panel panel2;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox5;
        private TextBox textBox4;
        private Label label11;
        private Label label10;
        private PictureBox pictureBox2;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private NumericUpDown numericUpDown2;
        private ComboBox comboBox1;
        private TextBox textBox8;
        private TextBox textBox6;
        private Label label9;
        private Label label13;
        private Label label14;
        private TextBox textBox7;
        private TextBox textBox9;
        private TextBox textBox10;
        private TextBox textBox11;
        private Label label15;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label12;
        private DataGridView dataGridView1;
        private Label label21;
        private Label label22;
        private TextBox textBox12;
        private Label label23;
        private RadioButton radioButton3;
        private TextBox textBox13;
        private Label label24;
        private DataGridViewTextBoxColumn column1;
        private DataGridViewTextBoxColumn column2;
        private DataGridViewTextBoxColumn column3;
        private DataGridViewTextBoxColumn column4;
        private DataGridViewTextBoxColumn column5;
        private DataGridViewTextBoxColumn column6;
        private DataGridViewTextBoxColumn column7;
        private DataGridViewTextBoxColumn column8;
        private DataGridViewTextBoxColumn column9;
    }
}