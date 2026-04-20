using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTL
{
    public class MenuBar : Form
    {
        private Button btnDangNhap = null!;
        private Button btnTrangChu = null!;
        private Button btnTaiQuay = null!;
        private Button btnQuanLyNhanVien = null!;
        private Button btnTest2 = null!;
        private Button btnLoiNhuan = null!;
        private Button btnThoiTrang = null!;
        private Button btnDangXuat = null!;

        public MenuBar()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Menu - Luxury Boutique";
            Size = new Size(320, 520);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(45, 45, 48);

            Label title = new Label
            {
                Text = "LUXURY BOUTIQUE",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Gold,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60
            };

            Panel btnPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 33)
            };

            btnDangNhap        = CreateBtn("Đăng Nhập",       Color.FromArgb(0, 120, 215));
            btnTrangChu       = CreateBtn("Trang Chủ",         Color.FromArgb(16, 124, 16));
            btnTaiQuay         = CreateBtn("Tại Quầy",          Color.FromArgb(0, 153, 188));
            btnQuanLyNhanVien  = CreateBtn("QL Nhân Viên",    Color.FromArgb(136, 23, 152));
            btnTest2           = CreateBtn("Thanh Toán",         Color.FromArgb(232, 97, 0));
            btnLoiNhuan        = CreateBtn("Lợi Nhuận BH",      Color.FromArgb(0, 153, 75));
            btnThoiTrang       = CreateBtn("Thời Trang",         Color.FromArgb(194, 57, 179));
            btnDangXuat        = CreateBtn("Đăng Xuất",          Color.FromArgb(197, 27, 27));

            btnDangNhap.Click        += (s, e) => OpenForm(new Dangnhap());
            btnTrangChu.Click       += (s, e) => OpenForm(new TrangChu());
            btnTaiQuay.Click         += (s, e) => OpenForm(new TaiQuay());
            btnQuanLyNhanVien.Click  += (s, e) => OpenForm(new QuanLyNhanVien());
            btnTest2.Click           += (s, e) => OpenForm(new Test2());
            btnLoiNhuan.Click        += (s, e) => OpenForm(new LoiNhuanBannha());
            btnThoiTrang.Click       += (s, e) => OpenForm(new ThoiTrang());
            btnDangXuat.Click        += (s, e) =>
            {
                var r = MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes) Application.Exit();
            };

            btnDangNhap.Location       = new Point(20, 15);
            btnTrangChu.Location      = new Point(20, 67);
            btnTaiQuay.Location       = new Point(20, 119);
            btnQuanLyNhanVien.Location = new Point(20, 171);
            btnTest2.Location         = new Point(20, 223);
            btnLoiNhuan.Location      = new Point(20, 275);
            btnThoiTrang.Location     = new Point(20, 327);
            btnDangXuat.Location      = new Point(20, 379);

            btnPanel.Controls.AddRange(new Control[]
            {
                btnDangNhap, btnTrangChu, btnTaiQuay, btnQuanLyNhanVien,
                btnTest2, btnLoiNhuan, btnThoiTrang, btnDangXuat
            });

            Controls.Add(btnPanel);
            Controls.Add(title);
        }

        private Button CreateBtn(string text, Color color)
        {
            return new Button
            {
                Text = text,
                Size = new Size(270, 42),
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private void OpenForm(Form form)
        {
            using (form)
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }
    }
}
