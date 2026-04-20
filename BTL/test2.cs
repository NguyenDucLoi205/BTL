using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BTL
{
    public partial class Test2 : Form
    {
        // ===================== FIELDS =====================
        private SqlConnection? sqlCon;
        private string strCon = @"Data Source=NguyenLoi;Initial Catalog=LUXURY_BOUTIQUE;Integrated Security=True;TrustServerCertificate=True";

        public Test2()
        {
            InitializeComponent();

            button2.Click += new EventHandler(btnThem_Click);
            button3.Click += new EventHandler(btnXoa_Click);
            button4.Click += new EventHandler(btnHuy_Click);
            button5.Click += new EventHandler(btnChinhSua_Click);
            button6.Click += new EventHandler(btnLamMoi_Click);

            SetFormReadOnly(true);

            try { dataGridView2.SelectionChanged += DataGridView2_SelectionChanged; } catch { }
        }

        private void DataGridView2_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0) return;
            var row = dataGridView2.SelectedRows[0];

            try
            {
                textBox1.Text = row.Cells["Ma_Hop_Dong"]?.Value?.ToString() ?? "";
                textBox4.Text = row.Cells["Ma_Nhan_Vien"]?.Value?.ToString() ?? "";
                textBox5.Text = row.Cells["Ten_Nhan_Vien"]?.Value?.ToString() ?? "";
                textBox2.Text = row.Cells["Ten_Khach_Hang"]?.Value?.ToString() ?? "";
                textBox3.Text = row.Cells["So_Dien_Thoai"]?.Value?.ToString() ?? "";

                var ngayVal = row.Cells["Ngay_Nhap"]?.Value;
                if (ngayVal != null && ngayVal != DBNull.Value)
                {
                    if (DateTime.TryParse(ngayVal.ToString(), out DateTime ngay))
                        dateTimePicker1.Value = ngay;
                }
            }
            catch { }

            var ma = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(ma))
            {
                ShowProductsForInvoice(ma);
                DieuDienThongTinSanPham(ma);
            }
        }

        private void DieuDienThongTinSanPham(string maHopDong)
        {
            string[] queries = new string[]
            {
                @"SELECT TOP 1 sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The, sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac, c.So_Luong, c.Gia_San_Pham FROM Chi_Tiet_Hoa_Don c INNER JOIN San_Pham sp ON c.Ma_SP = sp.Ma_SP WHERE c.Ma_Hop_Dong = @ma",
                @"SELECT TOP 1 sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The, sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac, c.So_Luong, c.Gia_San_Pham FROM CT_Hoa_Don c INNER JOIN San_Pham sp ON c.Ma_SP = sp.Ma_SP WHERE c.Ma_Hop_Dong = @ma",
                @"SELECT TOP 1 sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The, sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac, c.So_Luong, c.Gia_San_Pham FROM Hoa_Don_Chi_Tiet c INNER JOIN San_Pham sp ON c.Ma_SP = sp.Ma_SP WHERE c.Ma_Hop_Dong = @ma",
                @"SELECT TOP 1 sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The, sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac, c.So_Luong, c.Gia_San_Pham FROM CTHD c INNER JOIN San_Pham sp ON c.Ma_SP = sp.Ma_SP WHERE c.Ma_Hop_Dong = @ma",
                @"SELECT TOP 1 Ma_Gioi_Tinh, Ma_Loai, Ma_SP, Ma_Bien_The, Ten_San_Pham, Chat_Lieu, Size, Mau_Sac, So_Luong, Gia_San_Pham FROM San_Pham WHERE Ma_Hop_Dong = @ma"
            };

            try
            {
                MoKetNoi();
                foreach (var q in queries)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(q, sqlCon);
                        cmd.Parameters.AddWithValue("@ma", maHopDong);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            textBox7.Text = reader["Ma_SP"]?.ToString() ?? "";

                            string gioiTinh = reader["Ma_Gioi_Tinh"]?.ToString() ?? "";
                            radioButton1.Checked = gioiTinh == "Nam";
                            radioButton2.Checked = gioiTinh == "Nữ";
                            radioButton3.Checked = gioiTinh == "Unisex";

                            textBox11.Text = reader["Ma_Loai"]?.ToString() ?? "";
                            textBox10.Text = reader["Ten_San_Pham"]?.ToString() ?? "";
                            textBox9.Text = "";
                            textBox13.Text = reader["Mau_Sac"]?.ToString() ?? "";

                            string kichCo = reader["Size"]?.ToString() ?? "";
                            int idx = comboBox1.Items.IndexOf(kichCo);
                            comboBox1.SelectedIndex = idx >= 0 ? idx : -1;

                            textBox8.Text = reader["Chat_Lieu"]?.ToString() ?? "";

                            if (decimal.TryParse(reader["So_Luong"]?.ToString(), out decimal sl))
                                numericUpDown2.Value = Math.Min(sl, numericUpDown2.Maximum);

                            textBox6.Text = reader["Gia_San_Pham"]?.ToString() ?? "";

                            reader.Close();
                            return;
                        }
                        reader.Close();
                    }
                    catch { }
                }

                textBox7.Clear(); textBox11.Clear(); textBox10.Clear();
                textBox9.Clear(); textBox13.Clear(); textBox8.Clear();
                textBox6.Clear();
                numericUpDown2.Value = 0;
                radioButton1.Checked = true;
            }
            catch { }
        }

        private void ShowProductsForInvoice(string maHopDong)
        {
            try
            {
                MoKetNoi();
                string[] queries = new string[]
                {
                    @"SELECT sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The, sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac, c.So_Luong, c.Gia_San_Pham FROM Chi_Tiet_Hoa_Don c INNER JOIN San_Pham sp ON c.Ma_SP = sp.Ma_SP WHERE c.Ma_Hop_Dong = @ma",
                    @"SELECT sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The, sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac, c.So_Luong, c.Gia_San_Pham FROM CT_Hoa_Don c INNER JOIN San_Pham sp ON c.Ma_SP = sp.Ma_SP WHERE c.Ma_Hop_Dong = @ma",
                    @"SELECT sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The, sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac, c.So_Luong, c.Gia_San_Pham FROM Hoa_Don_Chi_Tiet c INNER JOIN San_Pham sp ON c.Ma_SP = sp.Ma_SP WHERE c.Ma_Hop_Dong = @ma",
                    @"SELECT sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The, sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac, c.So_Luong, c.Gia_San_Pham FROM CTHD c INNER JOIN San_Pham sp ON c.Ma_SP = sp.Ma_SP WHERE c.Ma_Hop_Dong = @ma",
                    @"SELECT Ma_Gioi_Tinh, Ma_Loai, Ma_SP, Ma_Bien_The, Ten_San_Pham, Chat_Lieu, Size, Mau_Sac, So_Luong, Gia_San_Pham FROM San_Pham WHERE Ma_Hop_Dong = @ma"
                };

                foreach (var q in queries)
                {
                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(q, sqlCon);
                        da.SelectCommand.Parameters.AddWithValue("@ma", maHopDong);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            dataGridView3.DataSource = dt;
                            dataGridView3.AllowUserToAddRows = false;
                            dataGridView3.RowHeadersVisible = false;
                            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            return;
                        }
                    }
                    catch { }
                }

                dataGridView3.DataSource = null;
            }
            catch { dataGridView3.DataSource = null; }
        }

        private void AddProductButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Sản Phẩm!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MoKetNoi();
                string gioiTinh = radioButton2.Checked ? "Nữ" : radioButton3.Checked ? "Unisex" : "Nam";
                string sqlSP = @"INSERT INTO San_Pham
                        (Ma_Gioi_Tinh, Ma_Loai, Ma_SP, Ma_Bien_The, Ten_San_Pham, Chat_Lieu, Size, Mau_Sac, So_Luong, Gia_San_Pham)
                        VALUES (@gioiTinh, @maLoai, @maSP, @maBienThe, @tenSP, @chatLieu, @size, @mauSac, @soLuong, @gia)";

                SqlCommand cmdSP = new SqlCommand(sqlSP, sqlCon);
                cmdSP.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                cmdSP.Parameters.AddWithValue("@maLoai", textBox11.Text.Trim());
                cmdSP.Parameters.AddWithValue("@maSP", textBox7.Text.Trim());
                cmdSP.Parameters.AddWithValue("@maBienThe", textBox7.Text.Trim());
                cmdSP.Parameters.AddWithValue("@tenSP", textBox10.Text.Trim());
                cmdSP.Parameters.AddWithValue("@chatLieu", textBox8.Text.Trim());
                cmdSP.Parameters.AddWithValue("@size", comboBox1.Text);
                cmdSP.Parameters.AddWithValue("@mauSac", textBox13.Text.Trim());
                cmdSP.Parameters.AddWithValue("@soLuong", numericUpDown2.Value);
                decimal gia = 0; decimal.TryParse(textBox6.Text.Trim(), out gia);
                cmdSP.Parameters.AddWithValue("@gia", gia);

                cmdSP.ExecuteNonQuery();
                MessageBox.Show("Đã thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================== KẾT NỐI DATABASE =====================
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // ===================== LOAD DỮ LIỆU =====================
        private void LoadData()
        {
            try
            {
                MoKetNoi();
                string query = "SELECT * FROM Hoa_Don";
                SqlDataAdapter adapter = new SqlDataAdapter(query, sqlCon);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView2.DataSource = dt;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                try
                {
                    string q2 = @"SELECT
                        Ma_Gioi_Tinh, Ma_Loai, Ma_SP, Ma_Bien_The,
                        Ten_San_Pham, Chat_Lieu, Size, Mau_Sac,
                        So_Luong, Gia_San_Pham FROM San_Pham";
                    SqlDataAdapter da2 = new SqlDataAdapter(q2, sqlCon);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    dataGridView3.DataSource = dt2;
                    dataGridView3.AllowUserToAddRows = false;
                    dataGridView3.RowHeadersVisible = false;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        // ===================== TÍNH TỔNG TIỀN =====================
        private void TinhTongTien()
        {
            try
            {
                MoKetNoi();
                string query = "SELECT SUM(Tong_Tien_Thanh_Toan) FROM Hoa_Don";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                    textBox12.Text = string.Format("{0:N0}", result);
                else
                    textBox12.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính tiền: " + ex.Message);
            }
        }

        // ===================== FORM LOAD =====================
        private void test2_Load(object sender, EventArgs e)
        {
            LoadData();
            TinhTongTien();
        }

        private void SetFormReadOnly(bool readOnly)
        {
            bool enable = !readOnly;
            textBox1.ReadOnly = readOnly;
            textBox2.ReadOnly = readOnly;
            textBox3.ReadOnly = readOnly;
            textBox4.ReadOnly = readOnly;
            textBox5.ReadOnly = readOnly;
            dateTimePicker1.Enabled = enable;
            textBox7.ReadOnly = readOnly;
            textBox11.ReadOnly = readOnly;
            textBox10.ReadOnly = readOnly;
            textBox9.ReadOnly = readOnly;
            textBox13.ReadOnly = readOnly;
            comboBox1.Enabled = enable;
            textBox8.ReadOnly = readOnly;
            numericUpDown2.ReadOnly = readOnly;
            textBox6.ReadOnly = readOnly;
            radioButton1.Enabled = enable;
            radioButton2.Enabled = enable;
            radioButton3.Enabled = enable;
            button1.Enabled = enable;
        }

        // ===================== NÚT THÊM (button2) =====================
        private void btnThem_Click(object? sender, EventArgs e)
        {
            if (button2.Text == "Thêm")
            {
                SetFormReadOnly(false);
                LamTrong();
                button2.Text = "Lưu";
                button5.Enabled = false;
                textBox1.Focus();
            }
            else
            {
                ThemDuLieu();
            }
        }

        private void ThemDuLieu()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Hợp Đồng!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MoKetNoi();
                string gioiTinh = "Nam";
                if (radioButton2.Checked) gioiTinh = "Nữ";
                else if (radioButton3.Checked) gioiTinh = "Unisex";

                string sqlHoaDon = @"INSERT INTO Hoa_Don
                    (Ma_Hop_Dong, Ngay_Nhap, Ma_Nhan_Vien, Ten_Khach_Hang, So_Dien_Thoai, Ten_Nhan_Vien, Tong_Tien_Thanh_Toan)
                    VALUES (@ma, @ngay, @manv, @tenKH, @sdt, @tenNV, @tongtien)";

                SqlCommand cmdHD = new SqlCommand(sqlHoaDon, sqlCon);
                cmdHD.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                cmdHD.Parameters.AddWithValue("@ngay", dateTimePicker1.Value);
                cmdHD.Parameters.AddWithValue("@manv", textBox4.Text.Trim());
                cmdHD.Parameters.AddWithValue("@tenKH", textBox2.Text.Trim());
                cmdHD.Parameters.AddWithValue("@sdt", textBox3.Text.Trim());
                cmdHD.Parameters.AddWithValue("@tenNV", textBox5.Text.Trim());

                decimal tongTien = 0;
                decimal.TryParse(textBox6.Text.Trim(), out tongTien);
                cmdHD.Parameters.AddWithValue("@tongtien", tongTien);
                cmdHD.ExecuteNonQuery();

                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    string sqlSP = @"INSERT INTO San_Pham
                        (Ma_Gioi_Tinh, Ma_Loai, Ma_SP, Ma_Bien_The, Ten_San_Pham, Chat_Lieu, Size, Mau_Sac, So_Luong, Gia_San_Pham)
                        VALUES (@gioiTinh, @maLoai, @maSP, @maBienThe, @tenSP, @chatLieu, @size, @mauSac, @soLuong, @gia)";

                    SqlCommand cmdSP = new SqlCommand(sqlSP, sqlCon);
                    cmdSP.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmdSP.Parameters.AddWithValue("@maLoai", textBox11.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@maSP", textBox7.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@maBienThe", textBox7.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@tenSP", textBox10.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@chatLieu", textBox8.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@size", comboBox1.Text);
                    cmdSP.Parameters.AddWithValue("@mauSac", textBox13.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@soLuong", numericUpDown2.Value);
                    cmdSP.Parameters.AddWithValue("@gia", tongTien);

                    try
                    {
                        cmdSP.ExecuteNonQuery();
                        try
                        {
                            if (dataGridView3.DataSource is DataTable dtProducts)
                            {
                                DataRow nr = dtProducts.NewRow();
                                nr["Ma_Gioi_Tinh"] = gioiTinh;
                                nr["Ma_Loai"] = textBox11.Text.Trim();
                                nr["Ma_SP"] = textBox7.Text.Trim();
                                nr["Ma_Bien_The"] = textBox7.Text.Trim();
                                nr["Ten_San_Pham"] = textBox10.Text.Trim();
                                nr["Chat_Lieu"] = textBox8.Text.Trim();
                                nr["Size"] = comboBox1.Text;
                                nr["Mau_Sac"] = textBox13.Text.Trim();
                                nr["So_Luong"] = numericUpDown2.Value;
                                nr["Gia_San_Pham"] = tongTien;
                                dtProducts.Rows.Add(nr);
                            }
                        }
                        catch { }
                    }
                    catch { }
                }

                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DungChinhSua();
                LoadData();
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================== NÚT XOÁ (button3) =====================
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập Mã Hợp Đồng cần xoá!", "Thông báo");
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc muốn xoá hoá đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    MoKetNoi();
                    string sqlXoa = "DELETE FROM Hoa_Don WHERE Ma_Hop_Dong = @ma";
                    SqlCommand cmd = new SqlCommand(sqlXoa, sqlCon);
                    cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xoá thành công!");
                    btnLamMoi_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xoá: " + ex.Message);
                }
            }
        }

        // ===================== NÚT CHỈNH SỬA (button5) =====================
        private void btnChinhSua_Click(object? sender, EventArgs e)
        {
            if (button5.Text == "Chỉnh Sửa")
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng chọn một hoá đơn để sửa!");
                    return;
                }
                SetFormReadOnly(false);
                textBox1.ReadOnly = true;
                button5.Text = "Cập Nhật";
                button2.Enabled = false;
            }
            else
            {
                LuuChinhSua();
            }
        }

        private void LuuChinhSua()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Hợp Đồng!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MoKetNoi();
                string sqlUpdate = @"UPDATE Hoa_Don SET
                    Ngay_Nhap = @ngay,
                    Ma_Nhan_Vien = @manv,
                    Ten_Khach_Hang = @tenKH,
                    So_Dien_Thoai = @sdt,
                    Ten_Nhan_Vien = @tenNV,
                    Tong_Tien_Thanh_Toan = @tongtien
                    WHERE Ma_Hop_Dong = @ma";

                SqlCommand cmd = new SqlCommand(sqlUpdate, sqlCon);
                cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@ngay", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@manv", textBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@tenKH", textBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@sdt", textBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@tenNV", textBox5.Text.Trim());

                decimal tongTien = 0;
                decimal.TryParse(textBox6.Text.Trim(), out tongTien);
                cmd.Parameters.AddWithValue("@tongtien", tongTien);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    MessageBox.Show("Đã lưu chỉnh sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Không tìm thấy hóa đơn để cập nhật.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DungChinhSua();
                LoadData();
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu chỉnh sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================== NÚT HUỶ (button4) =====================
        private void btnHuy_Click(object? sender, EventArgs e)
        {
            DungChinhSua();
            button2.Enabled = true;
            button5.Enabled = true;
            button2.Text = "Thêm";
            button5.Text = "Chỉnh Sửa";
            LoadData();
        }

        // ===================== NÚT LÀM MỚI (button6) =====================
        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            LamTrong();
            DungChinhSua();
            button2.Text = "Thêm";
            button5.Text = "Chỉnh Sửa";
            button2.Enabled = true;
            button5.Enabled = true;
            LoadData();
            TinhTongTien();
        }

        private void DungChinhSua()
        {
            SetFormReadOnly(true);
        }

        private void LamTrong()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox) ((TextBox)c).Clear();
            }
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
            textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear();
            textBox9.Clear(); textBox10.Clear(); textBox11.Clear(); textBox13.Clear();

            numericUpDown2.Value = 0;
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = -1;

            radioButton1.Checked = true;
            pictureBox2.Image = null;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Chọn ảnh từ máy tính";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFile.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(openFile.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Tag = openFile.FileName;
                MessageBox.Show("Đã chọn ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
