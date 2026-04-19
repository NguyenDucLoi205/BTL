using System;
using System.Windows.Forms;
using luxury;

namespace BTL
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Sửa dòng này để chương trình khởi chạy từ form Đăng Nhập đầu tiên
            Application.Run(new TaiQuay());
        }
    }
}