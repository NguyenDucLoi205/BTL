using System;
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
            dgvData.Rows.Add("SP001", "Sữa tươi", 15000);
            dgvData.Rows.Add("SP002", "Bánh mì", 12000);
            dgvData.Rows.Add("SP003", "Nước ngọt", 10000);
            dgvData.Rows.Add("SP004", "Cà phê", 25000);
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
}
