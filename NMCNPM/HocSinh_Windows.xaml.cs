using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NMCNPM
{
    /// <summary>
    /// Interaction logic for HocSinh_Windows.xaml
    /// </summary>
    public partial class HocSinh_Windows : Window
    {
        
        public string username = "hs01";
        public string mahs;
        DBConnect db = new DBConnect();
        public HocSinh_Windows()
        {
            InitializeComponent();
        }
        private void bt_mini_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void bt_max_click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }
        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void bt_close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Tt_getThongTin()
        {
            try
            {
                string query = "Exec HocSinh_LayThongTin '" + username + "'";
                DataTable dt = db.sql_select(query);
                TT_tb_mahs.Text = dt.Rows[0]["MaHS"].ToString();
                TT_tb_hoten.Text = dt.Rows[0]["HoTen"].ToString();
                TT_cb_gioitinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                TT_tb_email.Text = dt.Rows[0]["Email"].ToString();
                TT_tb_ngaysinh.Text = dt.Rows[0]["NgaySinh"].ToString();
                TT_tb_sodienthoai.Text = dt.Rows[0]["SDT"].ToString();
                TT_tb_diachi.Text = dt.Rows[0]["DiaChi"].ToString();
                mahs = dt.Rows[0]["MaHS"].ToString();
            }
            catch
            {
                Tt_lb_thongtincanhan_errorout.Content = "Không tìm được thông tin!!!";
                Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
            }
        }

        private void Tt_doimatkhau_loaded(object sender, RoutedEventArgs e)
        {
            TT_tb_taikhoan.Text = username;
        }
        private void Tt_capnhatmatkhau_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn đổi mật khẩu???", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    //do no stuff
                    return;
                }
                else
                {

                    //do yes stuff            
                    string query = "Exec DoiMatKhau '" + username + "','" + TT_tb_matkhaucu.Password + "','" + TT_tb_matkhaumoi.Password + "' ,'" + TT_tb_matkhaumoi_2.Password + "'";
                    DataTable dt = db.sql_select(query);
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {

                        Tt_lb_doimatkhau_errorout.Content = "Sai mật khẩu cũ!!!";
                        Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-2")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Mật khẩu mới bị trống!!!";
                        Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-3")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Mật khẩu nhập lại không đúng!!!";
                        Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Lỗi giao tác!!!";
                        Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                    }
                    else
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Cập nhật thành công.";
                        Tt_lb_doimatkhau_errorout.Background = Brushes.LightGreen;
                    }
                }
            }
            catch
            {

            }
        }
        private void Tt_loaded(object sender, RoutedEventArgs e)
        {
            Tt_getThongTin();
        }
        private void Tt_capnhatthongtincanhan_click(object sender, RoutedEventArgs e)
        {

        }
        /*======================================BẢNG ĐIỂM========================================
 * ======================================================================================
 * ======================================================================================*/
        private void Bd_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_bd();
            //get_Bd_datagrid();
        }

        private void get_Bd_datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("select * from Diem_HocSinh_MonHoc");
                Bd_datagird_tonghop.ItemsSource = dt.DefaultView;
                Bd_datagird_tonghop.Visibility = Visibility.Visible;
                //Bd_timkiem_datagird.Visibility = Visibility.Hidden;
                Bd_bt_in.Visibility = Visibility.Hidden;

            }
            catch
            { }
        }
        private void Bd_datagrid_loaded(object sender, RoutedEventArgs e)
        {

        }
        private void get_namhoc_bd()
        {
            var ds = new List<string>();
            DataTable dt1 = db.sql_select("select distinct Nam from Diem_HocSinh_MonHoc where MaHocSinh =" + mahs.ToString());
            DataRow r;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                r = dt1.Rows[i];
                ds.Add(r[0].ToString());
            }
            Bd_cb_namhoc.DataContext = ds;
        }
        private void Bd_timkiem_datagird_loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Bd_in_click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string duongdan = "D:/" + Bd_cb_namhoc.Text + "_" + Bd_cb_lop.Text + "_" + Bd_cb_kihoc.Text + "_" + Bd_cb_mon.Text + ".csv";
            //    ExportToCsv("Exec QTV_LayBangDiem '" + Bd_cb_namhoc.Text + "','" + Bd_cb_lop.Text + "'," +
            //  "'" + Bd_cb_kihoc.Text + "',N'" + Bd_cb_mon.Text + "' ", Bd_timkiem_datagird, duongdan);
            //    Bd_errorout.Content = "In thành công.";
            //    Bd_errorout.Background = Brushes.LightGreen;
            //}
            //catch
            //{
            //    Bd_errorout.Content = "In thất bại!!!";
            //    Bd_errorout.Background = Brushes.IndianRed;
            //}
        }
        private void Bd_cb_namhoc_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                try
                {
                    string query = "exec Student_BangDiem '" + mahs.ToString() + "','" + Bd_cb_namhoc.Text + "','" + Bd_cb_kihoc.Text + "'";
                    DataTable dt = db.sql_select(query);
                    Bd_datagird_tonghop.ItemsSource = dt.DefaultView;
                }
                catch { }
            }
            catch
            {

            }

        }
        private void Bd_timkiem_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "exec Student_BangDiem '" + mahs.ToString() + "','" + Bd_cb_namhoc.Text + "','" + Bd_cb_kihoc.Text + "'";
                DataTable dt = db.sql_select(query);
                Bd_datagird_tonghop.ItemsSource = dt.DefaultView;
                //if (dt.Rows.Count == 0)
                //{
                //    Bd_errorout.Content = "Bảng điểm rỗng!!!";
                //    Bd_errorout.Background = Brushes.IndianRed;
                //}
                //else
                //{
                //    string loi = dt.Rows[0][0].ToString();
                //    if (loi == "-1")
                //    {
                //        Bd_errorout.Content = "Vui lòng chọn năm học!!!";
                //        Bd_errorout.Background = Brushes.IndianRed;
                //    }
                //    else if (loi == "-10")
                //    {
                //        Bd_errorout.Content = "Lỗi giao tác!!!";
                //        Bd_errorout.Background = Brushes.IndianRed;
                //    }
                //    else if (loi == "-2")
                //    {
                //        Bd_errorout.Content = "Vui lòng chọn lớp học!!!";
                //        Bd_errorout.Background = Brushes.IndianRed;
                //    }
                //    else if (loi == "-3")
                //    {
                //        Bd_errorout.Content = "Vui lòng chọn kì học!!!";
                //        Bd_errorout.Background = Brushes.IndianRed;
                //    }
                //    else if (loi == "-4")
                //    {
                //        Bd_errorout.Content = "Vui lòng chọn môn học!!!";
                //        Bd_errorout.Background = Brushes.IndianRed;
                //    }
                //else
                //    {
                //        Bd_errorout.Content = "Tìm kiếm thành công.";
                //        Bd_errorout.Background = Brushes.LightGreen;
                //        Bd_timkiem_datagird.ItemsSource = dt.DefaultView;
                //        Bd_datagird_tonghop.Visibility = Visibility.Hidden;
                //        Bd_timkiem_datagird.Visibility = Visibility.Visible;
                //        Bd_bt_in.Visibility = Visibility.Visible;

                //    }
                //}

            }
            catch
            {

            }
        }

        private void Bd_cb_kihoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string query = "exec Student_BangDiem '" + mahs.ToString() + "','" + Bd_cb_namhoc.Text + "','" + Bd_cb_kihoc.Text + "'";
                DataTable dt = db.sql_select(query);
                Bd_datagird_tonghop.ItemsSource = dt.DefaultView;
            }
            catch { }
        }
    }
}
