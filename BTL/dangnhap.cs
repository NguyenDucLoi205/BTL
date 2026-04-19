using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BTL
{
    public partial class Dangnhap : Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }

        // ĐÂY LÀ HÀM MÀ VISUAL STUDIO TỰ TẠO KHI BẠN NHÁY ĐÚP VÀO NÚT TRÊN GIAO DIỆN
        private void button1_Click(object sender, EventArgs e)
        {
            // Tạm thời bỏ qua bước kiểm tra tài khoản, cứ bấm nút là chuyển form luôn

            // 1. Khởi tạo form Trang Chủ
            TrangChu formTrangChu = new TrangChu();

            // 2. Hiển thị form Trang Chủ
            formTrangChu.Show();

            // 3. Ẩn form Đăng nhập hiện tại đi
            this.Hide();
        }
    }
}
