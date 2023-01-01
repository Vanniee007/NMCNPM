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
        private void Lh_laydanhsachnamhoc()
        {
            var ds = new List<string>();
            DataTable dt = db.sql_select("select distinct Nam from Lop");
            //foreach(DataRow r in dt.Rows)
            DataRow r;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = dt.Rows[i];
                ds.Add(r[0].ToString());
            }
            Lh_Combobox_dsnam.DataContext = ds;
        }
        private void Lh_laydanhsachlophoc()
        {
            try
            {
                var ds = new List<string>();
                DataTable dt = db.sql_select("select TenLop from Lop where Nam = '"+Lh_Combobox_dsnam.SelectedItem.ToString()+"'");
                //foreach(DataRow r in dt.Rows)
                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];
                    ds.Add(r[0].ToString());
                }
                Lh_Combobox_dslop.DataContext = ds;
            }
            catch { }
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
            Lh_laydanhsachlophoc(); 
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

        private void Lh_Load(object sender, RoutedEventArgs e)
        {
            Lh_laydanhsachnamhoc();
        }

        private void Lh_load_datagrid()
        {
            try
            {
                string query = "qtv_danhsachhslop '"+ Lh_Combobox_dsnam.SelectedItem.ToString()+"','"+ Lh_Combobox_dslop.SelectedItem.ToString()+"'";
                Lh_datagird_dshocsinh.ItemsSource = db.sql_select(query).DefaultView;
                Lh_button_them.IsEnabled = false;
            }
            catch { }
        }
        private void Tk_load_datagrid()
        {
            try
            {
                string role = "";
                role = Tk_Combobox_role.SelectedItem.ToString();
                switch (role)
                {
                    case "Quản trị":
                        role = "1";
                        Tk_column_monday.Visibility = Visibility.Hidden;
                        break;
                    case "Giáo viên":
                        role = "2";
                        Tk_column_monday.Visibility = Visibility.Visible;
                        break;
                    case "Học sinh":
                        role = "3";
                        Tk_column_monday.Visibility = Visibility.Hidden;
                        break;
                }
                
                string query = "qtv_danhsachtaikhoan '"+ Tk_tb_search.Text+"','"+ role+"'";
                Tk_datagird.ItemsSource = db.sql_select(query).DefaultView;
                //Lh_button_them.IsEnabled = false;
            }
            catch { }
        }
        private void Lh_load_datagrid_refresh()
        {
            try
            {
                Lh_datagird_dshocsinh.ItemsSource = db.sql_select("select * from HocSinh").DefaultView;
            }
            catch { }
        }
        private void Lh_datagird_dshocsinh_Loaded(object sender, RoutedEventArgs e)
        {
            Lh_load_datagrid_refresh();
        }

        private void Lh_button_refresh_Click(object sender, RoutedEventArgs e)
        {
            Lh_load_datagrid_refresh();
            //Lh_Combobox_dslop.Text = "";
            Lh_button_them.IsEnabled = true;

        }

        private void Lh_Combobox_dslop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lh_load_datagrid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Lh_laydanhsach_Click(object sender, RoutedEventArgs e)
        {
        }

        List<string> Lh_LayHocSinhDaChon()
        {
            /// Lấy list index dòng đã chọn
            var SelectedList = new List<string>();
            for (int i = 0; i < Lh_datagird_dshocsinh.Items.Count; i++)
            {
                var item = Lh_datagird_dshocsinh.Items[i];
                //var mycheckbox_ = Lh_datagird_dshocsinh.Columns[0].S
                var mycheckbox = Lh_datagird_dshocsinh.Columns[7].GetCellContent(item) as CheckBox;
                if ((bool)mycheckbox.IsChecked)
                {
                    TextBlock x = Lh_datagird_dshocsinh.Columns[0].GetCellContent(Lh_datagird_dshocsinh.Items[i]) as TextBlock;
                    SelectedList.Add(x.Text);
                }
            }
            return SelectedList;
        }
        private void Lh_HienMaLoi(int MaLoi)
        {

        }
        private void Lh_button_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Lh_LayHocSinhDaChon().Count() > 0)
                {
                    var dshocsinh = Lh_LayHocSinhDaChon();
                    int ketqua;
                    int success =0, fail=0;
                    foreach (string MaHS in dshocsinh)
                    {
                        ketqua = (int)db.sql_select("exec qtv_themHSvaoLop '"+Lh_Combobox_dsnam.SelectedItem.ToString()+"','"+ Lh_Combobox_dslop.SelectedItem.ToString()+"','"+MaHS+"'").Rows[0][0];
                        if (ketqua ==0)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }

                    }
                    Lh_load_datagrid();
                    Lh_lb_errorout.Content = "Đã thêm "+success.ToString()+" dòng, "+fail.ToString()+" lỗi";
                    if (success==0)
                        Lh_lb_errorout.Background=Brushes.IndianRed;
                    else 
                        Lh_lb_errorout.Background=Brushes.LightGreen;
                }
            }
            catch { }
        }

        private void Lh_button_xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Lh_LayHocSinhDaChon().Count() > 0)
                {
                    var dshocsinh = Lh_LayHocSinhDaChon();
                    int ketqua;
                    int success = 0, fail = 0;
                    foreach (string MaHS in dshocsinh)
                    {
                        ketqua = (int)db.sql_select("exec qtv_xoaHSkhoiLop '"+Lh_Combobox_dsnam.SelectedItem.ToString()+"','"+ Lh_Combobox_dslop.SelectedItem.ToString()+"','"+MaHS+"'").Rows[0][0];
                        if (ketqua ==0)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }

                    }
                    Lh_load_datagrid();
                    Lh_lb_errorout.Content = "Đã xoá "+success.ToString()+" dòng, "+fail.ToString()+" lỗi";
                    if (success==0)
                        Lh_lb_errorout.Background=Brushes.IndianRed;
                    else
                        Lh_lb_errorout.Background=Brushes.LightGreen;
                }
            }
            catch { }
        }
        private void Tc_timkiem()
        {
            try
            {
                Tc_datagrid_searchresult.ItemsSource = db.sql_select("qtv_tracuu N'" + Tc_tb_search.Text + "'").DefaultView;
            }
            catch { }
        }
        private void Lh_timkiem()
        {
            try
            {
                string Nam="", Lop="";
                try
                {
                    Nam = Lh_Combobox_dsnam.SelectedItem.ToString();
                    Lop = Lh_Combobox_dslop.SelectedItem.ToString();
                }
                catch { }
                Lh_datagird_dshocsinh.ItemsSource = db.sql_select("qtv_tracuudanhsachlop N'" + Lh_tb_search.Text + "','"+ Nam +"','"+ Lop +"'").DefaultView;
            }
            catch { }
        }

        private void Tc_tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Tc_tb_search.Text) && Tc_tb_search.Text.Length > 0)
            {
                Tc_lb_search.Visibility = Visibility.Collapsed;
            }
            else
            {
                Tc_lb_search.Visibility = Visibility.Visible;
            }
            Tc_timkiem();

        }

        private void Tc_datagrid_searchresult_Loaded(object sender, RoutedEventArgs e)
        {
            Tc_timkiem();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }    
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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


        private void Bd_button_refresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bd_button_them_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bd_button_xoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bd_Combobox_dslop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Bd_datagird_dshocsinh_Loaded(object sender, RoutedEventArgs e)
        {
            Bd_Combobox_dslop.Text = "Chọn lớp học";
        }

        private void Lh_Combobox_dsnam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Lh_laydanhsachlophoc();
                string query = "qtv_danhsachhslop '"+ Lh_Combobox_dsnam.SelectedItem.ToString()+"','"+ Lh_Combobox_dslop.SelectedItem.ToString()+"'";
                Lh_datagird_dshocsinh.ItemsSource = db.sql_select(query).DefaultView;
                Lh_button_them.IsEnabled = false;
            }
            catch { }
        }

        private void Tk_Combobox_dsnam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tk_Combobox_dslop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tk_button_xoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tk_button_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Tk_Combobox_role.Text == "")
                {
                    Tk_lb_errorout.Content = "Chọn role cần thêm";
                    Tk_lb_errorout.Background = Brushes.IndianRed;
                    return;
                }
                DataRowView rowview = (DataRowView)Tk_datagird.SelectedItem;
                Admin_themTaiKhoan ad = new Admin_themTaiKhoan(Tk_Combobox_role.Text.ToLower(),0, "Thêm");
                ad.ShowDialog();
            }
            catch { }
        }

        private void Tk_button_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Tk_datagird.SelectedItem;
                Admin_themTaiKhoan ad = new Admin_themTaiKhoan(Tk_Combobox_role.Text.ToLower(), (int)rowview[0], "Sửa");
                //Admin_themTaiKhoan ad = new Admin_themTaiKhoan("gia", (int)rowview[0], "Sửa");
                ad.ShowDialog();
            }
            catch
            {
                Tk_lb_errorout.Content = "Chọn dòng cần sửa";
                Tk_lb_errorout.Background = Brushes.IndianRed;
            }
        }


        private void Lh_tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Lh_tb_search.Text) && Lh_tb_search.Text.Length > 0)
            {
                Lh_lb_search.Visibility = Visibility.Collapsed;
            }
            else
            {
                Lh_lb_search.Visibility = Visibility.Visible;
            }
            Lh_timkiem();
        }

        private void Tk_tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Tk_tb_search.Text) && Tk_tb_search.Text.Length > 0)
            {
                Tk_lb_search.Visibility = Visibility.Collapsed;
            }
            else
            {
                Tk_lb_search.Visibility = Visibility.Visible;
            }
            Tk_load_datagrid();
        }

        private void Tk_datagird_Loaded(object sender, RoutedEventArgs e)
        {

            var ds = new List<string>(); 
            ds.Add("Quản trị");
            ds.Add("Giáo viên");
            ds.Add("Học sinh");
            Tk_Combobox_role.DataContext = ds;
            Tk_load_datagrid();
        }

        private void Tk_Combobox_role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tk_load_datagrid();
        }

    }
}
