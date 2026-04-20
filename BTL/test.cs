using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BTL
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var data = new List<SanPham>
            {
                new SanPham { Ma = "SP001", Ten = "Sữa tươi", Gia = 15000 },
                new SanPham { Ma = "SP002", Ten = "Bánh mì", Gia = 12000 },
                new SanPham { Ma = "SP003", Ten = "Nước ngọt", Gia = 10000 },
                new SanPham { Ma = "SP004", Ten = "Cà phê", Gia = 25000 },
            };
            dgvData.DataSource = data;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = txtUsername.Text.Trim();
            var pass = txtPassword.Text;

            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Placeholder: xử lý đăng nhập
            MessageBox.Show($"Đã nhấn đăng nhập với user: {user}", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cardPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class SanPham
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public decimal Gia { get; set; }
    }
}
