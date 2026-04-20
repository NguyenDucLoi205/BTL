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

            // Gán sự kiện cho các nút
            button2.Click += new EventHandler(btnThem_Click);   // Thêm
            button3.Click += new EventHandler(btnXoa_Click);    // Xoá
            button4.Click += new EventHandler(btnHuy_Click);    // Huỷ
            button5.Click += new EventHandler(btnChinhSua_Click); // Chỉnh Sửa
            button6.Click += new EventHandler(btnLamMoi_Click); // Làm Mới

            // Mặc định: khoá các ô nhập liệu sản phẩm khi chưa nhấn Thêm hoặc Chỉnh Sửa
            // Mặc định: khoá các ô nhập liệu sản phẩm khi chưa nhấn Thêm hoặc Chỉnh Sửa
            SetFormReadOnly(true);

            // Ensure product add button is wired (designer also hooks it) — defensive
           uct add button is wired (designer also hooks it) — defensive
            try
            {
                if (buttonAddProduct != null)
                    buttonAddProduct.Click += AddProductButton_Click;
            }
            catch { }

            // Hook selection changed to show products for selected invoice
            try { dataGridView1.SelectionChanged += DataGridView1_SelectionChanged; } catch { }
        }

        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            var row = dataGridView1.SelectedRows[0];

            // --- Điền thông tin Hóa Đơn vào các TextBox ---
            try
            {
                textBox1.Text = row.Cells["Ma_Hop_Dong"]?.Value?.ToString() ?? "";
                textBox4.Text = row.Cells["Ma_Nhan_Vien"]?.Value?.ToString() ?? "";
                textBox5.Text = row.Cells["Ten_Nhan_Vien"]?.Value?.ToString() ?? "";
                textBox2.Text = row.Cells["Ten_Khach_Hang"]?.Value?.ToString() ?? "";
                textBox3.Text = row.Cells["So_Dien_Thoai"]?.Value?.ToString() ?? "";

                // Ngày nhập
                var ngayVal = row.Cells["Ngay_Nhap"]?.Value;
                if (ngayVal != null && ngayVal != DBNull.Value)
                {
                    if (DateTime.TryParse(ngayVal.ToString(), out DateTime ngay))
                        dateTimePicker1.Value = ngay;
                }
            }
            catch { }

            // --- Lấy Mã Hợp Đồng để load sản phẩm ---
            var ma = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(ma))
            {
                ShowProductsForInvoice(ma);
                DieuDienThongTinSanPham(ma); // Điền sản phẩm đầu tiên ra các ô
            }
        }

        /// <summary>
        /// Điền thông tin sản phẩm đầu tiên của hóa đơn ra các ô nhập liệu bên trái
        /// </summary>
        private void DieuDienThongTinSanPham(string maHopDong)
        {
            string[] queries = new string[]
            {
                // Bảng chi tiết hóa đơn JOIN với San_Pham
                @"SELECT TOP 1 sp.Ma_San_Pham, sp.Loai_Do, sp.Loai_San_Pham, sp.Ten_San_Pham, sp.Mo_Ta, sp.Mau_Sac, sp.Kich_Co, sp.Chat_Lieu, c.So_Luong, c.Gia FROM Chi_Tiet_Hoa_Don c INNER JOIN San_Pham sp ON c.Ma_San_Pham = sp.Ma_San_Pham WHERE c.Ma_Hop_Dong = @ma",
                @"SELECT TOP 1 sp.Ma_San_Pham, sp.Loai_Do, sp.Loai_San_Pham, sp.Ten_San_Pham, sp.Mo_Ta, sp.Mau_Sac, sp.Kich_Co, sp.Chat_Lieu, c.So_Luong, c.Gia FROM CT_Hoa_Don c INNER JOIN San_Pham sp ON c.Ma_San_Pham = sp.Ma_San_Pham WHERE c.Ma_Hop_Dong = @ma",
                @"SELECT TOP 1 sp.Ma_San_Pham, sp.Loai_Do, sp.Loai_San_Pham, sp.Ten_San_Pham, sp.Mo_Ta, sp.Mau_Sac, sp.Kich_Co, sp.Chat_Lieu, c.So_Luong, c.Gia FROM Hoa_Don_Chi_Tiet c INNER JOIN San_Pham sp ON c.Ma_San_Pham = sp.Ma_San_Pham WHERE c.Ma_Hop_Dong = @ma",
                @"SELECT TOP 1 sp.Ma_San_Pham, sp.Loai_Do, sp.Loai_San_Pham, sp.Ten_San_Pham, sp.Mo_Ta, sp.Mau_Sac, sp.Kich_Co, sp.Chat_Lieu, c.So_Luong, c.Gia FROM CTHD c INNER JOIN San_Pham sp ON c.Ma_San_Pham = sp.Ma_San_Pham WHERE c.Ma_Hop_Dong = @ma",
                // Fallback: San_Pham có cột Ma_Hop_Dong trực tiếp
                @"SELECT TOP 1 Ma_San_Pham, Loai_Do, Loai_San_Pham, Ten_San_Pham, Mo_Ta, Mau_Sac, Kich_Co, Chat_Lieu, So_Luong, Gia FROM San_Pham WHERE Ma_Hop_Dong = @ma"
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
                            // Mã sản phẩm
                            textBox7.Text = reader["Ma_San_Pham"]?.ToString() ?? "";

                            // Loại Đồ -> RadioButton
                            string loaiDo = reader["Loai_Do"]?.ToString() ?? "";
                            radioButton1.Checked = loaiDo == "Nam";
                            radioButton2.Checked = loaiDo == "Nữ";
                            radioButton3.Checked = loaiDo == "Unisex";

                            // Loại sản phẩm
                            textBox11.Text = reader["Loai_San_Pham"]?.ToString() ?? "";

                            // Tên sản phẩm
                            textBox10.Text = reader["Ten_San_Pham"]?.ToString() ?? "";

                            // Mô tả
                            textBox9.Text = reader["Mo_Ta"]?.ToString() ?? "";

                            // Màu sắc
                            textBox13.Text = reader["Mau_Sac"]?.ToString() ?? "";

                            // Kích cỡ
                            string kichCo = reader["Kich_Co"]?.ToString() ?? "";
                            int idx = comboBox1.Items.IndexOf(kichCo);
                            comboBox1.SelectedIndex = idx >= 0 ? idx : -1;

                            // Chất liệu
                            textBox8.Text = reader["Chat_Lieu"]?.ToString() ?? "";

                            // Số lượng
                            if (decimal.TryParse(reader["So_Luong"]?.ToString(), out decimal sl))
                                numericUpDown2.Value = Math.Min(sl, numericUpDown2.Maximum);

                            // Giá
                            textBox6.Text = reader["Gia"]?.ToString() ?? "";

                            reader.Close();
                            return; // Đã điền xong, thoát
                        }
                        reader.Close();
                    }
                    catch { /* thử tên bảng tiếp theo */ }
                }

                // Nếu không tìm thấy chi tiết, xóa trắng các ô sản phẩm
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
                    // common detail table names JOIN San_Pham
                    @"SELECT sp.Ma_San_Pham AS [Mã SP], sp.Loai_Do AS [Loại Đồ], sp.Loai_San_Pham AS [Loại SP], sp.Ten_San_Pham AS [Tên SP], sp.Mo_Ta AS [Mô Tả], sp.Mau_Sac AS [Màu Sắc], sp.Kich_Co AS [SIZE], sp.Chat_Lieu AS [Chất Liệu], c.So_Luong AS [SL], c.Gia AS [Giá] FROM Chi_Tiet_Hoa_Don c INNER JOIN San_Pham sp ON c.Ma_San_Pham = sp.Ma_San_Pham WHERE c.Ma_Hop_Dong = @ma",
                    @"SELECT sp.Ma_San_Pham AS [Mã SP], sp.Loai_Do AS [Loại Đồ], sp.Loai_San_Pham AS [Loại SP], sp.Ten_San_Pham AS [Tên SP], sp.Mo_Ta AS [Mô Tả], sp.Mau_Sac AS [Màu Sắc], sp.Kich_Co AS [SIZE], sp.Chat_Lieu AS [Chất Liệu], c.So_Luong AS [SL], c.Gia AS [Giá] FROM CT_Hoa_Don c INNER JOIN San_Pham sp ON c.Ma_San_Pham = sp.Ma_San_Pham WHERE c.Ma_Hop_Dong = @ma",
                    @"SELECT sp.Ma_San_Pham AS [Mã SP], sp.Loai_Do AS [Loại Đồ], sp.Loai_San_Pham AS [Loại SP], sp.Ten_San_Pham AS [Tên SP], sp.Mo_Ta AS [Mô Tả], sp.Mau_Sac AS [Màu Sắc], sp.Kich_Co AS [SIZE], sp.Chat_Lieu AS [Chất Liệu], c.So_Luong AS [SL], c.Gia AS [Giá] FROM Hoa_Don_Chi_Tiet c INNER JOIN San_Pham sp ON c.Ma_San_Pham = sp.Ma_San_Pham WHERE c.Ma_Hop_Dong = @ma",
                    @"SELECT sp.Ma_San_Pham AS [Mã SP], sp.Loai_Do AS [Loại Đồ], sp.Loai_San_Pham AS [Loại SP], sp.Ten_San_Pham AS [Tên SP], sp.Mo_Ta AS [Mô Tả], sp.Mau_Sac AS [Màu Sắc], sp.Kich_Co AS [SIZE], sp.Chat_Lieu AS [Chất Liệu], c.So_Luong AS [SL], c.Gia AS [Giá] FROM CTHD c INNER JOIN San_Pham sp ON c.Ma_San_Pham = sp.Ma_San_Pham WHERE c.Ma_Hop_Dong = @ma",
                    // Fallback: San_Pham có cột Ma_Hop_Dong trực tiếp
                    @"SELECT Ma_San_Pham AS [Mã SP], Loai_Do AS [Loại Đồ], Loai_San_Pham AS [Loại SP], Ten_San_Pham AS [Tên SP], Mo_Ta AS [Mô Tả], Mau_Sac AS [Màu Sắc], Kich_Co AS [SIZE], Chat_Lieu AS [Chất Liệu], So_Luong AS [SL], Gia AS [Giá] FROM San_Pham WHERE Ma_Hop_Dong = @ma"
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
                            dataGridViewProducts.DataSource = dt;
                            dataGridViewProducts.AllowUserToAddRows = false;
                            dataGridViewProducts.RowHeadersVisible = false;
                            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            return;
                        }
                    }
                    catch { /* try next query name */ }
                }

                // If none found, clear products grid
                dataGridViewProducts.DataSource = null;
            }
            catch { dataGridViewProducts.DataSource = null; }
        }

        // Nút Thêm Sản Phẩm (riêng)
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
                        (Ma_San_Pham, Loai_Do, Loai_San_Pham, Ten_San_Pham, Mo_Ta, Mau_Sac, Kich_Co, Chat_Lieu, So_Luong, Gia)
                        VALUES (@maSP, @loaiDo, @loaiSP, @tenSP, @moTa, @mauSac, @kichCo, @chatLieu, @soLuong, @gia)";

                SqlCommand cmdSP = new SqlCommand(sqlSP, sqlCon);
                cmdSP.Parameters.AddWithValue("@maSP", textBox7.Text.Trim());
                cmdSP.Parameters.AddWithValue("@loaiDo", gioiTinh);
                cmdSP.Parameters.AddWithValue("@loaiSP", textBox11.Text.Trim());
                cmdSP.Parameters.AddWithValue("@tenSP", textBox10.Text.Trim());
                cmdSP.Parameters.AddWithValue("@moTa", textBox9.Text.Trim());
                cmdSP.Parameters.AddWithValue("@mauSac", textBox13.Text.Trim());
                cmdSP.Parameters.AddWithValue("@kichCo", comboBox1.Text);
                cmdSP.Parameters.AddWithValue("@chatLieu", textBox8.Text.Trim());
                cmdSP.Parameters.AddWithValue("@soLuong", numericUpDown2.Value);
                decimal gia = 0; decimal.TryParse(textBox6.Text.Trim(), out gia);
                cmdSP.Parameters.AddWithValue("@gia", gia);

                cmdSP.ExecuteNonQuery();

                MessageBox.Show("Đã thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật products grid
                try { LoadData(); } catch { }
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
                dataGridView1.DataSource = dt;
                // Không hiển thị hàng thêm mới (dấu *) và ẩn row header nếu không cần
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersVisible = false;
                // Tùy chọn: cho các cột rộng đều
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Load products into product grid
                try
                {
                    string q2 = "SELECT Ma_San_Pham AS [Mã SP], Loai_Do AS [Loại Đồ], Loai_San_Pham AS [Loại SP], Ten_San_Pham AS [Tên SP], Mo_Ta AS [Mô Tả], Mau_Sac AS [Màu Sắc], Kich_Co AS [SIZE], Chat_Lieu AS [Chất Liệu], So_Luong AS [SL], Gia AS [Giá] FROM San_Pham";
                    SqlDataAdapter da2 = new SqlDataAdapter(q2, sqlCon);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    dataGridViewProducts.DataSource = dt2;
                    dataGridViewProducts.AllowUserToAddRows = false;
                    dataGridViewProducts.RowHeadersVisible = false;
                    dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch { /* bỏ qua nếu không có bảng San_Pham */ }
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

        // ===================== KHOÁ / MỞ KHOÁ FORM =====================
        /// <summary>
        /// readOnly = true: khoá tất cả ô nhập liệu sản phẩm
        /// readOnly = false: mở khoá để nhập/sửa
        /// </summary>
        private void SetFormReadOnly(bool readOnly)
        {
            bool enable = !readOnly;

            // Thông tin hóa đơn
            textBox1.ReadOnly = readOnly;  // Mã hợp đồng
            textBox2.ReadOnly = readOnly;  // Tên khách hàng
            textBox3.ReadOnly = readOnly;  // Số điện thoại
            textBox4.ReadOnly = readOnly;  // Mã nhân viên
            textBox5.ReadOnly = readOnly;  // Tên nhân viên
            dateTimePicker1.Enabled = enable;

            // Thông tin sản phẩm
            textBox7.ReadOnly = readOnly;  // Mã sản phẩm
            textBox11.ReadOnly = readOnly; // Loại sản phẩm
            textBox10.ReadOnly = readOnly; // Tên sản phẩm
            textBox9.ReadOnly = readOnly;  // Mô tả
            textBox13.ReadOnly = readOnly; // Màu sắc
            comboBox1.Enabled = enable;    // Kích cỡ
            textBox8.ReadOnly = readOnly;  // Chất liệu
            numericUpDown2.ReadOnly = readOnly; // Số lượng
            textBox6.ReadOnly = readOnly;  // Giá

            radioButton1.Enabled = enable;
            radioButton2.Enabled = enable;
            radioButton3.Enabled = enable;
            button1.Enabled = enable; // Thêm ảnh
        }

        // ===================== NÚT THÊM (button2) =====================
        private void btnThem_Click(object? sender, EventArgs e)
        {
            if (button2.Text == "Thêm")
            {
                // Trạng thái chuẩn bị nhập
                SetFormReadOnly(false);
                LamTrong();
                button2.Text = "Lưu";
                button5.Enabled = false; // Khóa nút Chỉnh Sửa khi đang thêm
                textBox1.Focus(); // Cho con trỏ vào ô đầu tiên
            }
            else
            {
                // Trạng thái đang là "Lưu" -> Thực hiện insert DB
                ThemDuLieu();
            }
        }

        private void ThemDuLieu()
        {
            // Kiểm tra dữ liệu bắt buộc
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Hợp Đồng!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MoKetNoi();

                // Xác định giới tính
                string gioiTinh = "Nam";
                if (radioButton2.Checked) gioiTinh = "Nữ";
                else if (radioButton3.Checked) gioiTinh = "Unisex";

                // === BƯỚC 1: Thêm hóa đơn ===
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

                // === BƯỚC 2: Thêm sản phẩm (nếu có mã sản phẩm) ===
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    string sqlSP = @"INSERT INTO San_Pham
                        (Ma_San_Pham, Loai_Do, Loai_San_Pham, Ten_San_Pham, Mo_Ta, Mau_Sac, Kich_Co, Chat_Lieu, So_Luong, Gia)
                        VALUES (@maSP, @loaiDo, @loaiSP, @tenSP, @moTa, @mauSac, @kichCo, @chatLieu, @soLuong, @gia)";

                    SqlCommand cmdSP = new SqlCommand(sqlSP, sqlCon);
                    cmdSP.Parameters.AddWithValue("@maSP", textBox7.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@loaiDo", gioiTinh);
                    cmdSP.Parameters.AddWithValue("@loaiSP", textBox11.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@tenSP", textBox10.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@moTa", textBox9.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@mauSac", textBox13.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@kichCo", comboBox1.Text);
                    cmdSP.Parameters.AddWithValue("@chatLieu", textBox8.Text.Trim());
                    cmdSP.Parameters.AddWithValue("@soLuong", numericUpDown2.Value);
                    cmdSP.Parameters.AddWithValue("@gia", tongTien);

                    try
                    {
                        cmdSP.ExecuteNonQuery();

                        // Cập nhật UI ngay: nếu products grid đang dùng DataTable làm DataSource thì thêm hàng mới
                        try
                        {
                            if (dataGridViewProducts.DataSource is DataTable dtProducts)
                            {
                                DataRow nr = dtProducts.NewRow();
                                nr["Mã SP"] = textBox7.Text.Trim();
                                nr["Loại Đồ"] = gioiTinh;
                                nr["Loại SP"] = textBox11.Text.Trim();
                                nr["Tên SP"] = textBox10.Text.Trim();
                                nr["Mô Tả"] = textBox9.Text.Trim();
                                nr["Màu Sắc"] = textBox13.Text.Trim();
                                nr["SIZE"] = comboBox1.Text;
                                nr["Chất Liệu"] = textBox8.Text.Trim();
                                nr["SL"] = numericUpDown2.Value;
                                nr["Giá"] = tongTien;
                                dtProducts.Rows.Add(nr);
                            }
                        }
                        catch { }
                    }
                    catch { /* Bỏ qua nếu bảng San_Pham không tồn tại */ }
                }

                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset về trạng thái khoá
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
                    btnLamMoi_Click(null, null); // Reset lại form sau khi xoá
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
                textBox1.ReadOnly = true; // Không cho sửa Khóa chính (Mã hợp đồng)
                button5.Text = "Cập Nhật";
                button2.Enabled = false; // Khóa nút Thêm khi đang sửa
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
                    MessageBox.Show("Không tìm thấy hóa đơn để cập nhật. Hãy kiểm tra lại Mã Hợp Đồng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
            DungChinhSua(); // Khóa form lại
            button2.Enabled = true;
            button5.Enabled = true;
            button2.Text = "Thêm";
            button5.Text = "Chỉnh Sửa";
            // Tải lại dữ liệu cũ nếu cần
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

        // ===================== HÀM PHỤ TRỢ =====================

        /// <summary>
        /// Kết thúc chế độ nhập/sửa, khoá form và reset nút
        /// </summary>
        private void DungChinhSua()
        {
            SetFormReadOnly(true);
        }

        /// <summary>
        /// Xoá trắng tất cả ô nhập liệu
        /// </summary>
        private void LamTrong()
        {
            // Xóa hết chữ trong TextBox
            foreach (Control c in this.Controls)
            {
                if (c is TextBox) ((TextBox)c).Clear();
            }
            // Riêng các ô trong GroupBox/Panel nếu có
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
            textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear();
            textBox9.Clear(); textBox10.Clear(); textBox11.Clear(); textBox13.Clear();

            numericUpDown2.Value = 0;

            // Xử lý ComboBox Kích cỡ
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = -1;

            radioButton1.Checked = true;
            pictureBox2.Image = null;
            dateTimePicker1.Value = DateTime.Now;
        }

        // ===================== NÚT THÊM ẢNH (button1) =====================
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