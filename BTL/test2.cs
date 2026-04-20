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

                // Load Hoa_Don
                string query = @"SELECT Ma_Hop_Dong, Ngay_Nhap, Ma_Nhan_Vien, Ten_Nhan_Vien,
                                So_Dien_Thoai_KH, Ten_Khach_Hang, Tong_Tien_Thanh_Toan
                                FROM Hoa_Don";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                dataGridView2.Rows.Clear();
                while (reader.Read())
                {
                    int idx = dataGridView2.Rows.Add();
                    dataGridView2.Rows[idx].Cells["Column1"].Value = reader["Ma_Hop_Dong"];
                    dataGridView2.Rows[idx].Cells["Column2"].Value = reader["Ngay_Nhap"];
                    dataGridView2.Rows[idx].Cells["Column3"].Value = reader["Ma_Nhan_Vien"];
                    dataGridView2.Rows[idx].Cells["Column4"].Value = reader["Ten_Nhan_Vien"];
                    dataGridView2.Rows[idx].Cells["Column5"].Value = reader["Ten_Khach_Hang"];
                    dataGridView2.Rows[idx].Cells["Column6"].Value = reader["Tong_Tien_Thanh_Toan"];
                }
                reader.Close();

                // Load San_Pham vao dataGridView3
                try
                {
                    string q2 = @"SELECT Ma_Gioi_Tinh, Ma_Loai, Ma_SP, Ma_Bien_The,
                        Ten_San_Pham, Chat_Lieu, Size, Mau_Sac,
                        So_Luong, Gia_San_Pham FROM San_Pham";
                    SqlCommand cmd2 = new SqlCommand(q2, sqlCon);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    dataGridView3.Rows.Clear();
                    while (reader2.Read())
                    {
                        int idx = dataGridView3.Rows.Add();
                        dataGridView3.Rows[idx].Cells["Column7"].Value = reader2["Ma_Gioi_Tinh"];
                        dataGridView3.Rows[idx].Cells["Column8"].Value = reader2["Ma_Loai"];
                        dataGridView3.Rows[idx].Cells["Column9"].Value = reader2["Ma_SP"];
                        dataGridView3.Rows[idx].Cells["Column10"].Value = reader2["Ma_Bien_The"];
                        dataGridView3.Rows[idx].Cells["Column11"].Value = reader2["Ten_San_Pham"];
                        dataGridView3.Rows[idx].Cells["Column12"].Value = reader2["Chat_Lieu"];
                        dataGridView3.Rows[idx].Cells["Column13"].Value = reader2["Size"];
                        dataGridView3.Rows[idx].Cells["Column14"].Value = reader2["Mau_Sac"];
                        dataGridView3.Rows[idx].Cells["Column15"].Value = reader2["So_Luong"];
                        dataGridView3.Rows[idx].Cells["Column16"].Value = reader2["Gia_San_Pham"];
                    }
                    reader2.Close();
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
                decimal tongTien = 0;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Cells["Column16"].Value != null &&
                        row.Cells["Column16"].Value != DBNull.Value)
                    {
                        if (decimal.TryParse(row.Cells["Column16"].Value.ToString(), out decimal gia))
                            tongTien += gia;
                    }
                }
                textBox12.Text = string.Format("{0:N0}", tongTien);
            }
            catch
            {
                textBox12.Text = "0";
            }
        }

        // ===================== FORM LOAD =====================
        private void test2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void SetFormReadOnly(bool readOnly)
        {
            bool enable = !readOnly;
            textBox1.ReadOnly = readOnly;
            textBox2.ReadOnly = readOnly;  // SĐT KH
            textBox3.ReadOnly = readOnly;  // Tên KH
            textBox4.ReadOnly = readOnly;  // Mã NV
            textBox5.ReadOnly = readOnly;  // Tên NV
            dateTimePicker1.Enabled = enable;
            textBox7.ReadOnly = readOnly;   // Mã SP
            textBox11.ReadOnly = readOnly;  // Loại SP
            textBox10.ReadOnly = readOnly;  // Tên SP
            textBox9.ReadOnly = readOnly;   // Mô Tả
            textBox13.ReadOnly = readOnly; // Màu Sắc
            comboBox1.Enabled = enable;     // Size
            textBox8.ReadOnly = readOnly;   // Chất Liệu
            numericUpDown2.ReadOnly = readOnly;
            textBox6.ReadOnly = readOnly;   // Giá
            radioButton1.Enabled = enable;
            radioButton2.Enabled = enable;
            radioButton3.Enabled = enable;
            button1.Enabled = enable;
        }

        // ===================== CHỌN HÓA ĐƠN =====================
        private void DataGridView2_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0) return;
            var row = dataGridView2.SelectedRows[0];

            try
            {
                textBox1.Text = row.Cells["Column1"]?.Value?.ToString() ?? "";
                textBox4.Text = row.Cells["Column3"]?.Value?.ToString() ?? "";
                textBox5.Text = row.Cells["Column4"]?.Value?.ToString() ?? "";

                // Đổ dữ liệu Hoa_Don: textBox2 = SĐT KH, textBox3 = Tên KH
                MoKetNoi();
                string queryHD = "SELECT So_Dien_Thoai_KH, Ten_Khach_Hang FROM Hoa_Don WHERE Ma_Hop_Dong = @ma";
                SqlCommand cmdHD = new SqlCommand(queryHD, sqlCon);
                cmdHD.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                SqlDataReader readerHD = cmdHD.ExecuteReader();
                if (readerHD.Read())
                {
                    textBox2.Text = readerHD["So_Dien_Thoai_KH"]?.ToString() ?? "";
                    textBox3.Text = readerHD["Ten_Khach_Hang"]?.ToString() ?? "";
                }
                readerHD.Close();

                var ngayVal = row.Cells["Column2"]?.Value;
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
                TinhTongTien();
            }
        }

        // ===================== LOAD SẢN PHẨM THEO HÓA ĐƠN =====================
        private void ShowProductsForInvoice(string maHopDong)
        {
            try
            {
                MoKetNoi();
                string query = @"SELECT sp.Ma_Gioi_Tinh, sp.Ma_Loai, sp.Ma_SP, sp.Ma_Bien_The,
                    sp.Ten_San_Pham, sp.Chat_Lieu, sp.Size, sp.Mau_Sac,
                    ctd.So_Luong, ctd.Gia_San_Pham
                    FROM Chi_Tiet_Hoa_Don ctd
                    INNER JOIN San_Pham sp ON ctd.Ma_SP = sp.Ma_SP
                    WHERE ctd.Ma_Hop_Dong = @ma";

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@ma", maHopDong);
                SqlDataReader reader = cmd.ExecuteReader();

                dataGridView3.Rows.Clear();
                while (reader.Read())
                {
                    int idx = dataGridView3.Rows.Add();
                    dataGridView3.Rows[idx].Cells["Column7"].Value = reader["Ma_Gioi_Tinh"];
                    dataGridView3.Rows[idx].Cells["Column8"].Value = reader["Ma_Loai"];
                    dataGridView3.Rows[idx].Cells["Column9"].Value = reader["Ma_SP"];
                    dataGridView3.Rows[idx].Cells["Column10"].Value = reader["Ma_Bien_The"];
                    dataGridView3.Rows[idx].Cells["Column11"].Value = reader["Ten_San_Pham"];
                    dataGridView3.Rows[idx].Cells["Column12"].Value = reader["Chat_Lieu"];
                    dataGridView3.Rows[idx].Cells["Column13"].Value = reader["Size"];
                    dataGridView3.Rows[idx].Cells["Column14"].Value = reader["Mau_Sac"];
                    dataGridView3.Rows[idx].Cells["Column15"].Value = reader["So_Luong"];
                    dataGridView3.Rows[idx].Cells["Column16"].Value = reader["Gia_San_Pham"];
                }
                reader.Close();
            }
            catch { dataGridView3.Rows.Clear(); }
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

                // Lấy tổng tiền từ dataGridView3
                decimal tongTien = 0;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Cells["Column16"].Value != null &&
                        row.Cells["Column16"].Value != DBNull.Value &&
                        decimal.TryParse(row.Cells["Column16"].Value.ToString(), out decimal gia))
                    {
                        tongTien += gia;
                    }
                }

                // INSERT Hoa_Don
                string sqlHoaDon = @"INSERT INTO Hoa_Don
                    (Ma_Hop_Dong, Ngay_Nhap, Ma_Nhan_Vien, Ten_Nhan_Vien, So_Dien_Thoai_KH, Ten_Khach_Hang, Tong_Tien_Thanh_Toan)
                    VALUES (@ma, @ngay, @manv, @tenNV, @sdt, @tenKH, @tongtien)";

                SqlCommand cmdHD = new SqlCommand(sqlHoaDon, sqlCon);
                cmdHD.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                cmdHD.Parameters.AddWithValue("@ngay", dateTimePicker1.Value);
                cmdHD.Parameters.AddWithValue("@manv", textBox4.Text.Trim());
                cmdHD.Parameters.AddWithValue("@tenNV", textBox5.Text.Trim());
                cmdHD.Parameters.AddWithValue("@sdt", textBox2.Text.Trim());
                cmdHD.Parameters.AddWithValue("@tenKH", textBox3.Text.Trim());
                cmdHD.Parameters.AddWithValue("@tongtien", tongTien);
                cmdHD.ExecuteNonQuery();

                // INSERT Chi_Tiet_Hoa_Don cho từng sản phẩm trong dataGridView3
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["Column9"].Value == null) continue;

                    string maSP = row.Cells["Column9"].Value?.ToString() ?? "";
                    decimal soLuong = 0;
                    decimal giaSP = 0;
                    decimal.TryParse(row.Cells["Column15"].Value?.ToString(), out soLuong);
                    decimal.TryParse(row.Cells["Column16"].Value?.ToString(), out giaSP);

                    string sqlCT = @"INSERT INTO Chi_Tiet_Hoa_Don (Ma_Hop_Dong, Ma_SP, So_Luong, Gia_San_Pham)
                                     VALUES (@maHD, @maSP, @sl, @gia)";
                    SqlCommand cmdCT = new SqlCommand(sqlCT, sqlCon);
                    cmdCT.Parameters.AddWithValue("@maHD", textBox1.Text.Trim());
                    cmdCT.Parameters.AddWithValue("@maSP", maSP);
                    cmdCT.Parameters.AddWithValue("@sl", soLuong);
                    cmdCT.Parameters.AddWithValue("@gia", giaSP);
                    try { cmdCT.ExecuteNonQuery(); } catch { }
                }

                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DungChinhSua();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================== NÚT XOÁ (button3) =====================
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Bạn có chắc muốn xoá hoá đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    MoKetNoi();

                    // Xoá chi tiết hoá đơn trước
                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        string sqlDelCT = "DELETE FROM Chi_Tiet_Hoa_Don WHERE Ma_Hop_Dong = @ma";
                        SqlCommand cmdDelCT = new SqlCommand(sqlDelCT, sqlCon);
                        cmdDelCT.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                        cmdDelCT.ExecuteNonQuery();

                        // Xoá hoá đơn
                        string sqlXoa = "DELETE FROM Hoa_Don WHERE Ma_Hop_Dong = @ma";
                        SqlCommand cmd = new SqlCommand(sqlXoa, sqlCon);
                        cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }

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

                // Tính lại tổng tiền
                decimal tongTien = 0;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["Column16"].Value != null &&
                        row.Cells["Column16"].Value != DBNull.Value &&
                        decimal.TryParse(row.Cells["Column16"].Value.ToString(), out decimal gia))
                        tongTien += gia;
                }

                string sqlUpdate = @"UPDATE Hoa_Don SET
                    Ngay_Nhap = @ngay,
                    Ma_Nhan_Vien = @manv,
                    Ten_Nhan_Vien = @tenNV,
                    So_Dien_Thoai_KH = @sdt,
                    Ten_Khach_Hang = @tenKH,
                    Tong_Tien_Thanh_Toan = @tongtien
                    WHERE Ma_Hop_Dong = @ma";

                SqlCommand cmd = new SqlCommand(sqlUpdate, sqlCon);
                cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@ngay", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@manv", textBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@tenNV", textBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@sdt", textBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@tenKH", textBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@tongtien", tongTien);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    MessageBox.Show("Đã lưu chỉnh sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Không tìm thấy hóa đơn để cập nhật.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DungChinhSua();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu chỉnh sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================== NÚT CHỌN SẢN PHẨM (button1) =====================
        private void btnChonSanPham_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Sản Phẩm!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Vui lòng nhập Giá Sản Phẩm!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string gioiTinh = radioButton2.Checked ? "Nữ" : radioButton3.Checked ? "Unisex" : "Nam";
                decimal soLuong = numericUpDown2.Value;
                decimal gia = 0;
                decimal.TryParse(textBox6.Text.Trim(), out gia);

                // Thêm vào dataGridView3
                int idx = dataGridView3.Rows.Add();
                dataGridView3.Rows[idx].Cells["Column7"].Value = gioiTinh;
                dataGridView3.Rows[idx].Cells["Column8"].Value = textBox11.Text.Trim();
                dataGridView3.Rows[idx].Cells["Column9"].Value = textBox7.Text.Trim();
                dataGridView3.Rows[idx].Cells["Column10"].Value = textBox7.Text.Trim();
                dataGridView3.Rows[idx].Cells["Column11"].Value = textBox10.Text.Trim();
                dataGridView3.Rows[idx].Cells["Column12"].Value = textBox8.Text.Trim();
                dataGridView3.Rows[idx].Cells["Column13"].Value = comboBox1.Text;
                dataGridView3.Rows[idx].Cells["Column14"].Value = textBox13.Text.Trim();
                dataGridView3.Rows[idx].Cells["Column15"].Value = soLuong;
                dataGridView3.Rows[idx].Cells["Column16"].Value = gia;

                TinhTongTien();
                LamTrongTextBoxSP();
                MessageBox.Show("Đã thêm sản phẩm vào danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            button2.Text = "Thêm";
            button5.Text = "Chỉnh Sửa";
            button2.Enabled = true;
            button5.Enabled = true;
        }

        private void LamTrong()
        {
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

        private void LamTrongTextBoxSP()
        {
            textBox7.Clear();
            textBox11.Clear();
            textBox10.Clear();
            textBox9.Clear();
            textBox13.Clear();
            textBox8.Clear();
            comboBox1.SelectedIndex = -1;
            numericUpDown2.Value = 0;
            textBox6.Clear();
            radioButton1.Checked = true;
            pictureBox2.Image = null;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Chọn ảnh sản phẩm";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFile.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(openFile.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Tag = openFile.FileName;
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
