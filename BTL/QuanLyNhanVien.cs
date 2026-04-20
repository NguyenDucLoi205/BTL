using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace BTL
{
    public partial class QuanLyNhanVien : Form
    {
        string strCon = @"Data Source=NguyenLoi;Initial Catalog=LUXURY_BOUTIQUE;Integrated Security=True;TrustServerCertificate=True";
        private SqlConnection? sqlCon = null;

        // Enum quản lý chế độ hiện tại
        private enum CheDoForm { Rong, DangThem, DangSua }
        private CheDoForm cheDo = CheDoForm.Rong;
        // giữ DataTable hiện tại để có thể thêm hàng placeholder vào DataGridView
        private DataTable? dtNhanVien = null;
        private bool placeholderRowAdded = false;

        public QuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // ================== TẢI DỮ LIỆU CƠ BẢN ==================
        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadData();
            // Mặc định ở chế độ rỗng, chưa chọn gì
            DatCheDo(CheDoForm.Rong);
        }

        private void LoadComboBox()
        {
            try
            {
                MoKetNoi();
                SqlDataAdapter daCV = new SqlDataAdapter("SELECT Ma_Chuc_Vu, Ten_Chuc_Vu FROM Chuc_Vu", sqlCon);
                DataTable dtCV = new DataTable();
                daCV.Fill(dtCV);
                comboBox1.DataSource = dtCV;
                comboBox1.DisplayMember = "Ten_Chuc_Vu";
                comboBox1.ValueMember = "Ma_Chuc_Vu";

                SqlDataAdapter daTT = new SqlDataAdapter("SELECT Ma_Tinh_Trang, Loai_Hop_Dong FROM Tinh_Trang_Nhan_Vien", sqlCon);
                DataTable dtTT = new DataTable();
                daTT.Fill(dtTT);
                comboBox2.DataSource = dtTT;
                comboBox2.DisplayMember = "Loai_Hop_Dong";
                comboBox2.ValueMember = "Ma_Tinh_Trang";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi ComboBox: " + ex.Message); }
        }

        private void LoadData()
        {
            try
            {
                MoKetNoi();
                string query = @"SELECT 
                                    nv.Ma_Nhan_Vien AS [Mã Nhân Viên], 
                                    nv.Ho_Ten AS [Họ Tên], 
                                    cv.Ten_Chuc_Vu AS [Chức Vụ], 
                                    tt.Loai_Hop_Dong AS [Tình Trạng],
                                    cv.Luong AS [Lương],
                                    nv.Ma_Chuc_Vu, 
                                    nv.Ma_Tinh_Trang 
                                 FROM Nhan_Vien nv
                                 INNER JOIN Chuc_Vu cv ON nv.Ma_Chuc_Vu = cv.Ma_Chuc_Vu
                                 INNER JOIN Tinh_Trang_Nhan_Vien tt ON nv.Ma_Tinh_Trang = tt.Ma_Tinh_Trang";

                SqlDataAdapter adapter = new SqlDataAdapter(query, sqlCon);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // lưu DataTable để thao tác khi cần (ví dụ thêm hàng placeholder)
                dtNhanVien = dt;
                placeholderRowAdded = false;

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dtNhanVien;

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 45;
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.MultiSelect = false;

                // Thay thế cột text hiển thị "Chức Vụ" và "Tình Trạng" bằng DataGridViewComboBoxColumn
                // Loại bỏ các cột tự động tạo có tên liên quan để tránh trùng
                if (dataGridView1.Columns.Contains("Chức Vụ")) dataGridView1.Columns.Remove("Chức Vụ");
                if (dataGridView1.Columns.Contains("Tình Trạng")) dataGridView1.Columns.Remove("Tình Trạng");
                if (dataGridView1.Columns.Contains("Ma_Chuc_Vu")) dataGridView1.Columns.Remove("Ma_Chuc_Vu");
                if (dataGridView1.Columns.Contains("Ma_Tinh_Trang")) dataGridView1.Columns.Remove("Ma_Tinh_Trang");

                // Tạo cột ComboBox cho Chức Vụ
                var colCV = new DataGridViewComboBoxColumn();
                colCV.Name = "Ma_Chuc_Vu";
                colCV.HeaderText = "Chức Vụ";
                colCV.DataPropertyName = "Ma_Chuc_Vu"; // liên kết tới cột Ma_Chuc_Vu trong DataTable
                colCV.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                colCV.DataSource = comboBox1.DataSource;
                colCV.DisplayMember = comboBox1.DisplayMember;
                colCV.ValueMember = comboBox1.ValueMember;
                dataGridView1.Columns.Insert(2, colCV);

                // Tạo cột ComboBox cho Tình Trạng
                var colTT = new DataGridViewComboBoxColumn();
                colTT.Name = "Ma_Tinh_Trang";
                colTT.HeaderText = "Tình Trạng";
                colTT.DataPropertyName = "Ma_Tinh_Trang"; // liên kết tới cột Ma_Tinh_Trang trong DataTable
                colTT.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                colTT.DataSource = comboBox2.DataSource;
                colTT.DisplayMember = comboBox2.DisplayMember;
                colTT.ValueMember = comboBox2.ValueMember;
                dataGridView1.Columns.Insert(3, colTT);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load dữ liệu: " + ex.Message); }
        }

        private void LamTrong()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            pictureBox2.Image = null;
        }

        // ==================== QUẢN LÝ CHẾ ĐỘ VÀ HIGHLIGHT NÚT ====================

        /// <summary>
        /// Đặt chế độ form và highlight nút tương ứng.
        /// Nút đang active → màu nổi bật (xanh đậm / text trắng).
        /// Nút khác → màu mờ/xám.
        /// </summary>
        private void DatCheDo(CheDoForm cheDoMoi)
        {
            cheDo = cheDoMoi;

            // Reset tất cả nút về màu mặc định mờ
            DatStyleNutMo(button3); // Thêm
            DatStyleNutMo(button4); // Xoá
            DatStyleNutMo(button5); // Sửa
            DatStyleNutMo(button2); // Quay về

            switch (cheDo)
            {
                case CheDoForm.DangThem:
                    DatStyleNutActive(button3); // Highlight nút Thêm
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    LamTrong();
                    // Thêm hàng placeholder vào DataGridView để user thấy có hàng "NULL" ở cuối
                    if (dtNhanVien != null && !placeholderRowAdded)
                    {
                        DataRow newRow = dtNhanVien.NewRow();
                        // Các cột theo truy vấn LoadData: [Mã Nhân Viên], [Họ Tên], [Chức Vụ], [Tình Trạng], [Lương], Ma_Chuc_Vu, Ma_Tinh_Trang
                        for (int i = 0; i < dtNhanVien.Columns.Count; i++)
                        {
                            newRow[i] = DBNull.Value;
                        }
                        dtNhanVien.Rows.Add(newRow);
                        placeholderRowAdded = true;

                    // Cập nhật giao diện DataGridView và chọn hàng mới
                        dataGridView1.ClearSelection();
                        int last = dataGridView1.Rows.Count - 1;
                        if (last >= 0)
                        {
                            dataGridView1.Rows[last].Selected = true;
                            try { dataGridView1.FirstDisplayedScrollingRowIndex = last; } catch { }
                            // cho phép sửa trực tiếp trên DataGridView
                            dataGridView1.ReadOnly = false;
                            // đặt ô hiện tại về cột đầu (Mã Nhân Viên) để user nhập mã ngay
                            try
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[last].Cells[0];
                            }
                            catch { }
                            dataGridView1.BeginEdit(true);
                        }
                    }
                    // Nếu đã thêm placeholder thì focus đang ở DataGridView (không cần focus textBox1)
                    if (!placeholderRowAdded)
                        textBox1.Focus();
                    break;

                case CheDoForm.DangSua:
                    DatStyleNutActive(button5); // Highlight nút Sửa
                    textBox1.ReadOnly = true;  // Không cho sửa Mã NV
                    textBox2.ReadOnly = false;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    textBox2.Focus();
                    break;

                case CheDoForm.Rong:
                default:
                    // Không highlight nút nào, khóa form
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    LamTrong();
                    break;
            }
        }

        private void DatStyleNutActive(Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 120, 215);   // Xanh Windows nổi bật
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font(btn.Font, FontStyle.Bold);
        }

        private void DatStyleNutMo(Button btn)
        {
            btn.BackColor = Color.FromArgb(200, 200, 200); // Xám mờ
            btn.ForeColor = Color.FromArgb(100, 100, 100); // Chữ xám tối
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font(btn.Font, FontStyle.Regular);
        }

        // ==================== SỰ KIỆN 4 NÚT CHÍNH ====================

        // NÚT THÊM → kích hoạt chế độ Thêm
        private void button3_Click(object sender, EventArgs e)
        {
            if (cheDo == CheDoForm.DangThem)
            {
                // Đang ở chế độ Thêm → thực hiện lưu vào SQL
                LuuThem();
            }
            else
            {
                // Chuyển sang chế độ Thêm
                DatCheDo(CheDoForm.DangThem);
            }
        }

        private void LuuThem()
        {
            string ma = string.Empty;
            string ten = string.Empty;
            object cvVal = DBNull.Value;
            object ttVal = DBNull.Value;

            // Nếu có placeholder và hàng được chọn trong DataGridView thì đọc từ đó
            if (placeholderRowAdded && dataGridView1.SelectedRows.Count > 0)
            {
                var r = dataGridView1.SelectedRows[0];
                ma = r.Cells["Mã Nhân Viên"].Value?.ToString() ?? string.Empty;
                ten = r.Cells["Họ Tên"].Value?.ToString() ?? string.Empty;
                cvVal = r.Cells["Ma_Chuc_Vu"].Value ?? DBNull.Value;
                ttVal = r.Cells["Ma_Tinh_Trang"].Value ?? DBNull.Value;
            }
            else
            {
                ma = textBox1.Text.Trim();
                ten = textBox2.Text.Trim();
                cvVal = comboBox1.SelectedValue ?? DBNull.Value;
                ttVal = comboBox2.SelectedValue ?? DBNull.Value;
            }

            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten))
            {
                MessageBox.Show("Vui lòng nhập Mã và Họ tên nhân viên!", "Thiếu thông tin");
                return;
            }
            try
            {
                MoKetNoi();
                string sql = "INSERT INTO Nhan_Vien (Ma_Nhan_Vien, Ho_Ten, Ma_Chuc_Vu, Ma_Tinh_Trang) VALUES (@ma, @ten, @cv, @tt)";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@cv", cvVal);
                cmd.Parameters.AddWithValue("@tt", ttVal);

                cmd.ExecuteNonQuery();
                MessageBox.Show("✅ Đã thêm nhân viên vào cơ sở dữ liệu!", "Thành công");
                // Nếu có hàng placeholder trong dtNhanVien thì loại bỏ trước khi tải lại từ DB
                if (dtNhanVien != null && placeholderRowAdded)
                {
                    // xóa tất cả hàng có toàn giá trị DBNull (phần placeholder)
                    for (int i = dtNhanVien.Rows.Count - 1; i >= 0; i--)
                    {
                        bool allNull = true;
                        foreach (var item in dtNhanVien.Rows[i].ItemArray)
                        {
                            if (item != DBNull.Value) { allNull = false; break; }
                        }
                        if (allNull) dtNhanVien.Rows.RemoveAt(i);
                    }
                    placeholderRowAdded = false;
                }

                LoadData();
                DatCheDo(CheDoForm.Rong); // Sau khi thêm xong → về chế độ rỗng
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm dữ liệu: " + ex.Message); }
        }

        // NÚT XOÁ → Xoá bản ghi đang chọn trong bảng
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên trong danh sách để xoá!", "Chưa chọn nhân viên");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xoá nhân viên '{textBox2.Text}' ({textBox1.Text})?",
                "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    MoKetNoi();
                    string sql = "DELETE FROM Nhan_Vien WHERE Ma_Nhan_Vien = @ma";
                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Đã xoá thành công!", "Thành công");
                    LoadData();
                    DatCheDo(CheDoForm.Rong);
                }
                catch (Exception)
                {
                    MessageBox.Show("❌ Không thể xóa! Nhân viên này đang có dữ liệu liên kết (hóa đơn, v.v.).", "Lỗi xoá");
                }
            }
        }

        // NÚT SỬA → kích hoạt chế độ Sửa (hoặc lưu nếu đang sửa)
        private void button5_Click(object sender, EventArgs e)
        {
            if (cheDo == CheDoForm.DangSua)
            {
                // Đang ở chế độ Sửa → thực hiện lưu vào SQL
                LuuSua();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên trong danh sách để sửa!", "Chưa chọn nhân viên");
                    return;
                }
                DatCheDo(CheDoForm.DangSua);
            }
        }

        private void LuuSua()
        {
            try
            {
                MoKetNoi();
                string sql = "UPDATE Nhan_Vien SET Ho_Ten=@ten, Ma_Chuc_Vu=@cv, Ma_Tinh_Trang=@tt WHERE Ma_Nhan_Vien=@ma";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@ten", textBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@cv", comboBox1.SelectedValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tt", comboBox2.SelectedValue ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show("✅ Đã cập nhật thông tin nhân viên!", "Thành công");

                LoadData();
                DatCheDo(CheDoForm.Rong);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi cập nhật: " + ex.Message); }
        }

        // NÚT QUAY VỀ
        private void button2_Click(object sender, EventArgs e)
        {
            DatCheDo(CheDoForm.Rong);
            this.Close();
        }

        // ==================== CLICK VÀO HÀNG TRONG BẢNG ====================

        // Dùng SelectionChanged để đảm bảo click bất kỳ ô nào trong hàng đều hoạt động
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dataGridView1.SelectedRows[0];

                // Tránh lỗi khi row là hàng trống (new row)
                if (r.IsNewRow) return;

                // Điền thông tin từ bảng lên form
                textBox1.Text = r.Cells["Mã Nhân Viên"].Value?.ToString() ?? "";
                textBox2.Text = r.Cells["Họ Tên"].Value?.ToString() ?? "";

                if (r.Cells["Ma_Chuc_Vu"].Value != null)
                    comboBox1.SelectedValue = r.Cells["Ma_Chuc_Vu"].Value.ToString()!;

                if (r.Cells["Ma_Tinh_Trang"].Value != null)
                    comboBox2.SelectedValue = r.Cells["Ma_Tinh_Trang"].Value.ToString()!;

                // Khi chọn hàng → highlight nút Xoá (chế độ chờ chọn hành động)
                // Không đặt chế độ Sửa ngay, chỉ highlight Xoá để user biết đã chọn hàng
                DatStyleNutMo(button3); // Thêm mờ
                DatStyleNutActive(button4); // Xoá nổi bật
                DatStyleNutActive(button5); // Sửa cũng nổi bật (cho user chọn)
                DatStyleNutMo(button2); // Quay về mờ

                // Mở khóa cho phép sửa khi user bấm nút Sửa
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                cheDo = CheDoForm.Rong; // Chờ user chọn Sửa hoặc Xoá
            }
        }

        // ==================== CÁC NÚT KHÁC ====================

        private void button1_Click(object sender, EventArgs e) // Nút chọn ảnh
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Ảnh (*.jpg;*.png)|*.jpg;*.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(open.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ==================== CÁC HÀM TRỐNG FIX LỖI DESIGNER ====================
        private void label1_Click_1(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
    }
}