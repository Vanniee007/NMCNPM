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
        
        public string username = "";
        public string mahs;
        DBConnect db = new DBConnect();
        public HocSinh_Windows(string username_)
        {
            InitializeComponent();
            username = username_;  
            mahs = TT_layMa(username);
            if (mahs == null)
            {
                MessageBox.Show("Học sinh không tồn tại");
                this.Close();
            }
        }
        private string TT_layMa(string username)
        {
            try
            {
                string str;
                str = db.sql_select("select MaHS from HocSinh where username = '"+username+"'").Rows[0][0].ToString();
                return str;
            }
            catch
            {
                return null;
            }
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

            LoginWindows lg = new LoginWindows(username);
            this.Close();
            lg.Show();
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
                Tt_lb_thongtincanhan_errorout.Foreground = Brushes.IndianRed;
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
                        Tt_lb_doimatkhau_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-2")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Mật khẩu mới bị trống!!!";
                        Tt_lb_doimatkhau_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-3")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Mật khẩu nhập lại không đúng!!!";
                        Tt_lb_doimatkhau_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Lỗi giao tác!!!";
                        Tt_lb_doimatkhau_errorout.Foreground = Brushes.IndianRed;
                    }
                    else
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Cập nhật thành công.";
                        Tt_lb_doimatkhau_errorout.Foreground = Brushes.MediumSeaGreen;
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
            try
            {
                if (MessageBox.Show("Bạn có muốn cập nhật thông tin???", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    //do no stuff
                    return;
                }
                else
                {

                    //do yes stuff            
                    string query = "Exec HocSinh_CapNhatThongTinCaNhan '" + mahs + "',N'" + TT_tb_hoten.Text + "','" + TT_tb_ngaysinh.Text + "','" + TT_cb_gioitinh.Text + "'" +
                        ",'" + TT_tb_email.Text + "','" + TT_tb_sodienthoai.Text + "','" + TT_tb_diachi.Text + "'";
                    DataTable dt = db.sql_select(query);
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {
                        Tt_lb_thongtincanhan_errorout.Content = "Thông tin bị trống!!!";
                        Tt_lb_thongtincanhan_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Tt_lb_thongtincanhan_errorout.Content = "Lỗi giao tác!!!";
                        Tt_lb_thongtincanhan_errorout.Foreground = Brushes.IndianRed;
                    }
                    else
                    {
                        Tt_lb_thongtincanhan_errorout.Content = "Cập nhật thành công.";
                        Tt_lb_thongtincanhan_errorout.Foreground = Brushes.MediumSeaGreen;

                    }
                }
            }
            catch
            {

            }
        }
        /*======================================BẢNG ĐIỂM========================================
 * ======================================================================================
 * ======================================================================================*/
        private void get_namhoc_bd()
        {
            try
            {
                var ds = new List<string>();
                DataTable dt1 = db.sql_select("select distinct Nam from Diem_HocSinh_MonHoc where MaHocSinh ='" + mahs + "'");
                DataRow r;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    r = dt1.Rows[i];
                    ds.Add(r[0].ToString());
                }
                Bd_cb_namhoc.DataContext = ds;
            }
            catch { }
           
        }
        private void Bd_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_bd();
            get_Bd_datagrid_tonghop();
        }

        private void get_Bd_datagrid_tonghop()
        {
            try
            {
                DataTable dt = db.sql_select("Exec Student_BangDiem_TongHop '"+mahs+"'");
                Bd_datagird_tonghop.ItemsSource = dt.DefaultView;
                Bd_datagird_tonghop.Visibility = Visibility.Visible;
                Bd_datagird.Visibility = Visibility.Hidden;

            }
            catch
            { }
        }
        private void Bd_datagrid_loaded(object sender, RoutedEventArgs e)
        {

        }
       
        private void Bd_timkiem_datagird_loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Bd_in_click(object sender, RoutedEventArgs e)
        {
           
        }
     
        private void Bd_timkiem_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "exec Student_BangDiem '" + mahs + "','" + Bd_cb_namhoc.Text + "','" + Bd_cb_kihoc.Text + "'";
                DataTable dt = db.sql_select(query);
                if (dt.Rows.Count == 0)
                {
                    Bd_errorout.Content = "Bảng điểm rỗng!!!";
                    Bd_errorout.Foreground = Brushes.IndianRed;
                }
                else
                {
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {
                        Bd_errorout.Content = "Vui lòng chọn năm học!!!";
                        Bd_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Bd_errorout.Content = "Lỗi giao tác!!!";
                        Bd_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-2")
                    {
                        Bd_errorout.Content = "Vui lòng chọn kì học!!!";
                        Bd_errorout.Foreground = Brushes.IndianRed;
                    }
                    else
                    {
                        Bd_errorout.Content = "Tìm kiếm thành công.";
                        Bd_errorout.Foreground = Brushes.MediumSeaGreen;
                        Bd_datagird.ItemsSource = dt.DefaultView;
                        Bd_datagird_tonghop.Visibility = Visibility.Hidden;
                        Bd_datagird.Visibility = Visibility.Visible;
                        float diemTongKet = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            diemTongKet += float.Parse(dt.Rows[i]["DiemTongKetMon"].ToString());
                        }
                        diemTongKet /= dt.Rows.Count;
                                                                     
                        Bd_diemtongket.Content = "Điểm tổng kết : " +  Math.Round(diemTongKet, 3).ToString();

                    }
                }

            }
            catch
            {

            }
        }

       
        private void Bd_datagrid_tonghop_loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Bd_refresh_click(object sender, RoutedEventArgs e)
        {
            get_Bd_datagrid_tonghop();
            Bd_diemtongket.Content = "";
        }

        /*======================================DANH SÁCH LỚP========================================
* ======================================================================================
* ======================================================================================*/
        private void get_namhoc_dslop()
        {
            try
            {
                var ds = new List<string>();
                DataTable dt1 = db.sql_select("select distinct Nam from Diem_HocSinh_MonHoc where MaHocSinh ='" + mahs + "'");
                DataRow r;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    r = dt1.Rows[i];
                    ds.Add(r[0].ToString());
                }
                Dslop_cb_namhoc.DataContext = ds;
            }
            catch { }

        }
        private void Dslop_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_dslop();
        }
        private void Dslop_timkiem_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "exec HocSinh_DanhSachLop '" + mahs + "','" + Dslop_cb_namhoc.Text + "'";
                DataTable dt = db.sql_select(query);

                if (dt.Rows.Count == 0)
                {
                    Dslop_errorout.Content = "Bảng điểm rỗng!!!";
                    Dslop_errorout.Foreground = Brushes.IndianRed;
                    Dslop_datagrid.ItemsSource = dt.DefaultView;
                    Dslop_lb.Content = "Danh sách lớp: " + dt.Rows[0]["TenLop"].ToString() + " " + dt.Rows[0]["Nam"].ToString();
                }
                else
                {
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {
                        Dslop_errorout.Content = "Vui lòng chọn năm học!!!";
                        Dslop_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Dslop_errorout.Content = "Lỗi giao tác!!!";
                        Dslop_errorout.Foreground = Brushes.IndianRed;
                    }
                    else
                    {

                        Dslop_errorout.Content = "Tìm kiếm thành công.";
                        Dslop_errorout.Foreground = Brushes.MediumSeaGreen;
                        Dslop_datagrid.ItemsSource = dt.DefaultView;
                        Dslop_lb.Content = "Danh sách lớp: " + dt.Rows[0]["TenLop"].ToString() + " " + dt.Rows[0]["Nam"].ToString();
                    }


                }


            }
            catch
            {

            }
        }

        private void TT_shortcut_Click(object sender, RoutedEventArgs e)
        {
            TT_tabitem.Focus();
        }

        private void Windows_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Windows_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
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
        }

        private void TT_button_background()
        {
            if (Tabcontrol.SelectedItem == TT_tabitem)
            {
                TT_shortcut.Background =Brushes.Navy;
            }
            else
                TT_shortcut.Background =Brushes.Transparent;
        }
        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TT_button_background();
        }
    }
}
