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
    /// Interaction logic for Admin_Windows.xaml
    /// </summary>
    public partial class Admin_Windows : Window
    {
        public string username = "admin";
        public string maqt;
        DBConnect db = new DBConnect();

        //public  { get; set; }
        public Admin_Windows()
        {
            InitializeComponent(); 
        }
        private void tn_laydanhsachmon()
        {
            var ds = new List<string>();
            DataTable dt = db.sql_select("select distinct TenMon from MonHoc");
            foreach(DataRow r in dt.Rows)
            {
                ds.Add(r[0].ToString());
            }
            Tn_gv_tb_daymon.DataContext = ds;
        }

        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {

        }

      

        private void Tn_ad_them_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_ThemQuanTriVien '" +Tn_ad_tb_username.Text + "','" + Tn_ad_tb_password.Password + "',N'" + Tn_ad_tb_hoten.Text + "','" + Tn_ad_tb_ngaysinh.Text + "','" + Tn_ad_cb_gioitinh.Text + "'" +
                        ",'" + Tn_ad_tb_email.Text + "','" + Tn_ad_tb_sodienthoai.Text + "','" + Tn_ad_tb_diachi.Text + "'";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Tn_ad_lb_errorout.Content = "Thông tin bị trống!!!";
                    Tn_ad_lb_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-2")
                {
                    Tn_ad_lb_errorout.Content = "Tên tài khoản đã tồn tại!!!";
                    Tn_ad_lb_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Tn_ad_lb_errorout.Content = "Lỗi giao tác!!!";
                    Tn_ad_lb_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    Tn_ad_lb_errorout.Content = "Cập nhật thành công.";
                    Tn_ad_lb_errorout.Background = Brushes.LightGreen;
                }
            }
            catch
            {

            }
        }

        private void Tn_hs_them_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_ThemHocSinh '" + Tn_hs_tb_username.Text + "','" + Tn_hs_tb_password.Password + "',N'" + Tn_hs_tb_hoten.Text + "','" + Tn_hs_tb_ngaysinh.Text + "','" + Tn_hs_cb_gioitinh.Text + "'" +
                        ",'" + Tn_hs_tb_email.Text + "','" + Tn_hs_tb_sodienthoai.Text + "','" + Tn_hs_tb_diachi.Text + "'";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Tn_hs_lb_errorout.Content = "Thông tin bị trống!!!";
                    Tn_hs_lb_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-2")
                {
                    Tn_hs_lb_errorout.Content = "Tên tài khoản đã tồn tại!!!";
                    Tn_hs_lb_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Tn_hs_lb_errorout.Content = "Lỗi giao tác!!!";
                    Tn_hs_lb_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    Tn_hs_lb_errorout.Content = "Cập nhật thành công.";
                    Tn_hs_lb_errorout.Background = Brushes.LightGreen;
                }
            }
            catch
            {

            }
        }

        private void tn_laydanhsachlopdachon()
        {
            /// Lấy list index dòng đã chọn
            var SelectedList = new List<string>();
            for (int i = 0; i < Tn_datagrid_giaovien.Items.Count; i++)
            {
                var item = Tn_datagrid_giaovien.Items[i];
                var mycheckbox = Tn_datagrid_giaovien.Columns[2].GetCellContent(item) as CheckBox;
                if ((bool)mycheckbox.IsChecked)
                {
                    TextBlock x = Tn_datagrid_giaovien.Columns[0].GetCellContent(Tn_datagrid_giaovien.Items[i]) as TextBlock;
                    TextBlock y = Tn_datagrid_giaovien.Columns[1].GetCellContent(Tn_datagrid_giaovien.Items[i]) as TextBlock;
                    SelectedList.Add(x.Text);
                    MessageBox.Show(x.Text+"|"+y.Text);
                }
            }
            //return SelectedList;
        }
        private void Tn_gv_them_click(object sender, RoutedEventArgs e)
        {
            try
            {

                tn_laydanhsachlopdachon();


                //foreach (var item in Tn_gv_lb_daylop.SelectedItems)
                //{
                //    // Lấy giá trị của item
                //    var value = item.ToString();
                //}

            }
            catch { }
        }
        private void Tt_getThongTin()
        {
            try
            {
                string query = "Exec QTV_LayThongTin '" + username + "'";
                DataTable dt = db.sql_select(query);
                TT_tb_maqt.Text = dt.Rows[0]["MaQT"].ToString();
                TT_tb_hoten.Text = dt.Rows[0]["HoTen"].ToString();
                TT_cb_gioitinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                TT_tb_email.Text = dt.Rows[0]["Email"].ToString();
                TT_tb_ngaysinh.Text = dt.Rows[0]["NgaySinh"].ToString();
                TT_tb_sodienthoai.Text = dt.Rows[0]["SDT"].ToString();
                TT_tb_diachi.Text = dt.Rows[0]["DiaChi"].ToString();
                maqt= dt.Rows[0]["MaQT"].ToString();
            }
            catch
            {
                Tt_lb_thongtincanhan_errorout.Content = "Không tìm được thông tin!!!";
                Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
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
                    string query = "Exec QTV_CapNhatThongTinCaNhan '" + maqt+ "',N'" + TT_tb_hoten.Text + "','" + TT_tb_ngaysinh.Text + "','" + TT_cb_gioitinh.Text + "'" +
                        ",'" + TT_tb_email.Text + "','" + TT_tb_sodienthoai.Text + "','" + TT_tb_diachi.Text + "'";
                    DataTable dt = db.sql_select(query);
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {
                        Tt_lb_thongtincanhan_errorout.Content = "Thông tin bị trống!!!";
                        Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Tt_lb_thongtincanhan_errorout.Content = "Lỗi giao tác!!!";
                        Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
                    }
                    else
                    {
                        Tt_lb_thongtincanhan_errorout.Content = "Cập nhật thành công.";
                        Tt_lb_thongtincanhan_errorout.Background = Brushes.LightGreen;
                    }
                }
            }
            catch
            {

            }
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

        private void Tt_doimatkhau_loaded(object sender, RoutedEventArgs e)
        {
            TT_tb_taikhoan.Text = username;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_mini_click(object sender, RoutedEventArgs e)
        {
            this.WindowState =  WindowState.Minimized;
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

        private void bt_close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tn_giaovien_TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            tn_laydanhsachmon(); 
            tn_load_datagrid_giaovien();
        }

        private void tn_load_datagrid_giaovien()
        {
            try
            {
                Tn_datagrid_giaovien.ItemsSource = db.sql_select("select TenLop, Nam from Lop").DefaultView;
            }
            catch { }
        }
        private void Tn_datagrid_giaovien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
