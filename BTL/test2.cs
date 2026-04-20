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

            button1.Click += new EventHandler(btnChonAnh_Click);
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

                // Load Hoa_don vao dataGridView2
                string query = @"SELECT Ma_hop_dong, Ngay_nhap, Ma_Nhan_Vien, Ho_ten,
                                SDT_KH, Ten_KH, Tong_tien
                                FROM Hoa_don";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                dataGridView2.Rows.Clear();
                while (reader.Read())
                {
                    int idx = dataGridView2.Rows.Add();
                    dataGridView2.Rows[idx].Cells["Column1"].Value = reader["Ma_hop_dong"];
                    dataGridView2.Rows[idx].Cells["Column2"].Value = reader["Ngay_nhap"];
                    dataGridView2.Rows[idx].Cells["Column3"].Value = reader["Ma_Nhan_Vien"];
                    dataGridView2.Rows[idx].Cells["Column4"].Value = reader["Ho_ten"];
                    dataGridView2.Rows[idx].Cells["Column5"].Value = reader["Ten_KH"];
                    dataGridView2.Rows[idx].Cells["Column6"].Value = reader["Tong_tien"];
                }
                reader.Close();
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
                textBox3.Text = row.Cells["Column5"]?.Value?.ToString() ?? "";

                // Lay SDT_KH tu Hoa_don
                MoKetNoi();
                string queryHD = "SELECT SDT_KH FROM Hoa_don WHERE Ma_hop_dong = @ma";
                SqlCommand cmdHD = new SqlCommand(queryHD, sqlCon);
                cmdHD.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                var sdtVal = cmdHD.ExecuteScalar();
                textBox2.Text = sdtVal?.ToString() ?? "";

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
                // JOIN Chi_tiet_Hoa_don -> San_Pham -> Chatlieu_Size_Soluong_Gia
                string query = @"SELECT
                    sp.Ma_Gioi_Tinh,
                    sp.Ma_Loai,
                    sp.Ma_San_Pham,
                    ctd.Ma_bien_thee,
                    sp.Ten_San_Pham,
                    csvsg.Chat_lieu,
                    csvsg.Size,
                    csvsg.Mau_Sac,
                    ctd.So_luong_ban,
                    ctd.gia_san_pham
                FROM Chi_tiet_Hoa_don ctd
                INNER JOIN San_Pham sp ON ctd.Ma_San_Pham = sp.Ma_San_Pham
                INNER JOIN Chatlieu_Size_Soluong_Gia csvsg ON ctd.Ma_bien_thee = csvsg.Ma_Bien_The
                WHERE ctd.Ma_hop_dong = @ma";

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@ma", maHopDong);
                SqlDataReader reader = cmd.ExecuteReader();

                dataGridView3.Rows.Clear();
                while (reader.Read())
                {
                    int idx = dataGridView3.Rows.Add();
                    dataGridView3.Rows[idx].Cells["Column7"].Value = reader["Ma_Gioi_Tinh"];
                    dataGridView3.Rows[idx].Cells["Column8"].Value = reader["Ma_Loai"];
                    dataGridView3.Rows[idx].Cells["Column9"].Value = reader["Ma_San_Pham"];
                    dataGridView3.Rows[idx].Cells["Column10"].Value = reader["Ma_bien_thee"];
                    dataGridView3.Rows[idx].Cells["Column11"].Value = reader["Ten_San_Pham"];
                    dataGridView3.Rows[idx].Cells["Column12"].Value = reader["Chat_lieu"];
                    dataGridView3.Rows[idx].Cells["Column13"].Value = reader["Size"];
                    dataGridView3.Rows[idx].Cells["Column14"].Value = reader["Mau_Sac"];
                    dataGridView3.Rows[idx].Cells["Column15"].Value = reader["So_luong_ban"];
                    dataGridView3.Rows[idx].Cells["Column16"].Value = reader["gia_san_pham"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                dataGridView3.Rows.Clear();
            }
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

                // Tính tổng tiền từ dataGridView3
                decimal tongTien = 0;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["Column16"].Value != null &&
                        row.Cells["Column16"].Value != DBNull.Value &&
                        decimal.TryParse(row.Cells["Column16"].Value.ToString(), out decimal gia))
                    {
                        tongTien += gia;
                    }
                }

                // INSERT Hoa_don
                string sqlHoaDon = @"INSERT INTO Hoa_don
                    (Ma_hop_dong, Ngay_nhap, Ma_Nhan_Vien, Ho_ten, SDT_KH, Ten_KH, Tong_tien)
                    VALUES (@ma, @ngay, @manv, @hoTen, @sdt, @tenKH, @tongtien)";

                SqlCommand cmdHD = new SqlCommand(sqlHoaDon, sqlCon);
                cmdHD.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                cmdHD.Parameters.AddWithValue("@ngay", dateTimePicker1.Value);
                cmdHD.Parameters.AddWithValue("@manv", textBox4.Text.Trim());
                cmdHD.Parameters.AddWithValue("@hoTen", textBox5.Text.Trim());
                cmdHD.Parameters.AddWithValue("@sdt", textBox2.Text.Trim());
                cmdHD.Parameters.AddWithValue("@tenKH", textBox3.Text.Trim());
                cmdHD.Parameters.AddWithValue("@tongtien", tongTien);
                cmdHD.ExecuteNonQuery();

                // INSERT Chi_tiet_Hoa_don cho từng sản phẩm trong dataGridView3
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["Column9"].Value == null) continue;

                    string maSP = row.Cells["Column9"].Value?.ToString() ?? "";
                    string maBienThe = row.Cells["Column10"].Value?.ToString() ?? "";
                    decimal soLuong = 0;
                    decimal giaSP = 0;
                    decimal.TryParse(row.Cells["Column15"].Value?.ToString(), out soLuong);
                    decimal.TryParse(row.Cells["Column16"].Value?.ToString(), out giaSP);

                    string sqlCT = @"INSERT INTO Chi_tiet_Hoa_don (Ma_hop_dong, Ma_San_Pham, Ma_bien_thee, So_luong_ban, gia_san_pham)
                                     VALUES (@maHD, @maSP, @maBT, @sl, @gia)";
                    SqlCommand cmdCT = new SqlCommand(sqlCT, sqlCon);
                    cmdCT.Parameters.AddWithValue("@maHD", textBox1.Text.Trim());
                    cmdCT.Parameters.AddWithValue("@maSP", maSP);
                    cmdCT.Parameters.AddWithValue("@maBT", maBienThe);
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

                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        // Xoá chi tiết hoá đơn trước (khoa ngoai)
                        string sqlDelCT = "DELETE FROM Chi_tiet_Hoa_don WHERE Ma_hop_dong = @ma";
                        SqlCommand cmdDelCT = new SqlCommand(sqlDelCT, sqlCon);
                        cmdDelCT.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                        cmdDelCT.ExecuteNonQuery();

                        // Xoá hoá đơn
                        string sqlXoa = "DELETE FROM Hoa_don WHERE Ma_hop_dong = @ma";
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

                string sqlUpdate = @"UPDATE Hoa_don SET
                    Ngay_nhap = @ngay,
                    Ma_Nhan_Vien = @manv,
                    Ho_ten = @hoTen,
                    SDT_KH = @sdt,
                    Ten_KH = @tenKH,
                    Tong_tien = @tongtien
                    WHERE Ma_hop_dong = @ma";

                SqlCommand cmd = new SqlCommand(sqlUpdate, sqlCon);
                cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@ngay", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@manv", textBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@hoTen", textBox5.Text.Trim());
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

        // ===================== NÚT CHỌN ẢNH (button1) =====================
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
            dataGridView3.Rows.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
