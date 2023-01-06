using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaction logic for GiaoVien_Windows.xaml
    /// </summary>
    
    public partial class GiaoVien_Windows : Window
    {
        public string username = "gv01";
        public string magv;
        DBConnect db = new DBConnect();

        public GiaoVien_Windows()
        {
            InitializeComponent();
        }

        private void Tt_getThongTin()
        {
            try
            {
                string query = "Exec GV_LayThongTin '" + username + "'";
                DataTable dt = db.sql_select(query);
                TT_tb_maqt.Text = dt.Rows[0]["MaGV"].ToString();
                TT_tb_hoten.Text = dt.Rows[0]["HoTen"].ToString();
                TT_cb_gioitinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                TT_tb_email.Text = dt.Rows[0]["Email"].ToString();
                TT_tb_ngaysinh.Text = dt.Rows[0]["NgaySinh"].ToString();
                TT_tb_sodienthoai.Text = dt.Rows[0]["SDT"].ToString();
                TT_tb_diachi.Text = dt.Rows[0]["DiaChi"].ToString();
                magv = dt.Rows[0]["MaGV"].ToString();
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
        private void get_namhoc_kh_lh()
        {
            var ds = new List<string>();
            DataTable dt1 = db.sql_select("select distinct Nam from NamHoc");
            DataRow r;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                r = dt1.Rows[i];
                ds.Add(r[0].ToString());
            }
        }
        private void get_datagrid_kh_lh()
        {
            try
            {
                DataTable dt = db.sql_select("select * from Lop");
            }
            catch
            { }
        }

        private void Kh_lh_loaded(object sender, RoutedEventArgs e)
        {
            get_datagrid_kh_lh();
            get_namhoc_kh_lh();
        }

        private void get_namhoc_bd()
        {
            var ds = new List<string>();
            DataTable dt1 = db.sql_select("select distinct Nam from NamHoc");
            DataRow r;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                r = dt1.Rows[i];
                ds.Add(r[0].ToString());
            }
            Bd_cb_namhoc.DataContext = ds;
        }
        private void get_Bd_datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("select * from Diem_HocSinh_MonHoc");
                Bd_datagird_tonghop.ItemsSource = dt.DefaultView;
                Bd_datagird_tonghop.Visibility = Visibility.Visible;
                Bd_timkiem_datagird.Visibility = Visibility.Hidden;
                Bd_bt_in.Visibility = Visibility.Hidden;

            }
            catch
            { }
        }
        private void Bd_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_bd();
            get_Bd_datagrid();
        }

        private void ExportToCsv(string query, DataGrid dataGrid, string filePath)
        {
            DataTable dataTable = db.sql_select(query);

            using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Lấy danh sách các cột trong bảng
                var columns = dataGrid.Columns;
                // Tạo một mảng chứa tên của từng cột
                string[] header = columns.Select(column => column.Header.ToString()).ToArray();
                // Ghi tên cột vào file CSV
                streamWriter.WriteLine(string.Join(",", header));

                // Duyệt qua từng dòng dữ liệu trong DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    // Tạo một mảng chứa dữ liệu của từng cột trong dòng
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                    // Ghi dòng dữ liệu vào file CSV
                    streamWriter.WriteLine(string.Join(",", fields));
                }
            }
        }

        private void Bd_in_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string duongdan = "D:/BangDiem_" + Bd_cb_namhoc.Text + "_" + Bd_cb_lop.Text + "_" + Bd_cb_kihoc.Text + ".csv";
                if (MessageBox.Show("Bạn có muốn in vào " + duongdan + "?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    //do no stuff
                    return;
                }
                else
                {
                    ExportToCsv("Exec QTV_LayBangDiem '" + Bd_cb_namhoc.Text + "','" + Bd_cb_lop.Text + "'," +
                  "'" + Bd_cb_kihoc.Text + "',N'" + "' ", Bd_timkiem_datagird, duongdan);
                    Bd_errorout.Content = "In thành công.";
                    Bd_errorout.Background = Brushes.LightGreen;
                }
            }
            catch
            {
                Bd_errorout.Content = "In thất bại!!!";
                Bd_errorout.Background = Brushes.IndianRed;
            }

        }

        private void get_monhoc_bd(string nam)
        {
            try
            {
                var ds = new List<string>();
                DataTable dt1 = db.sql_select("select distinct TenMon from MonHoc where Nam='" + nam + "'");
                DataRow r;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    r = dt1.Rows[i];
                    ds.Add(r[0].ToString());
                }
            }
            catch { }

        }
        private void get_lophoc_bd(string nam)
        {
            try
            {
                var ds = new List<string>();
                DataTable dt1 = db.sql_select("select distinct TenLop from Lop where Nam='" + nam + "'");
                DataRow r;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    r = dt1.Rows[i];
                    ds.Add(r[0].ToString());
                }
                Bd_cb_lop.DataContext = ds;

            }
            catch { }

        }

        private void Bd_cb_namhoc_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string nam = Bd_cb_namhoc.SelectedItem.ToString();
                get_lophoc_bd(nam);
                get_monhoc_bd(nam);
            }
            catch
            {

            }

        }

        private void Bd_timkiem_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_LayBangDiem '" + Bd_cb_namhoc.Text + "','" + Bd_cb_lop.Text + "'," +
              "'" + Bd_cb_kihoc.Text + "',N'" + "' ";
                DataTable dt = db.sql_select(query);
                if (dt.Rows.Count == 0)
                {
                    Bd_errorout.Content = "Bảng điểm rỗng!!!";
                    Bd_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {
                        Bd_errorout.Content = "Vui lòng chọn năm học!!!";
                        Bd_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Bd_errorout.Content = "Lỗi giao tác!!!";
                        Bd_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-2")
                    {
                        Bd_errorout.Content = "Vui lòng chọn lớp học!!!";
                        Bd_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-3")
                    {
                        Bd_errorout.Content = "Vui lòng chọn kì học!!!";
                        Bd_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-4")
                    {
                        Bd_errorout.Content = "Vui lòng chọn môn học!!!";
                        Bd_errorout.Background = Brushes.IndianRed;
                    }
                    else
                    {
                        Bd_errorout.Content = "Tìm kiếm thành công.";
                        Bd_errorout.Background = Brushes.LightGreen;
                        Bd_timkiem_datagird.ItemsSource = dt.DefaultView;
                        Bd_datagird_tonghop.Visibility = Visibility.Hidden;
                        Bd_timkiem_datagird.Visibility = Visibility.Visible;
                        Bd_bt_in.Visibility = Visibility.Visible;

                    }
                }

            }
            catch
            {

            }
        }

        private void Bd_refresh_click(object sender, RoutedEventArgs e)
        {
            get_Bd_datagrid();
        }

        private void Bd_datagrid_loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Bd_timkiem_datagird_loaded(object sender, RoutedEventArgs e)
        {

        }
        private void get_namhoc_tk_mon()
        {
            var ds = new List<string>();
            DataTable dt1 = db.sql_select("select distinct Nam from NamHoc");
            DataRow r;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                r = dt1.Rows[i];
                ds.Add(r[0].ToString());
            }
            Tk_mon_cb_namhoc.DataContext = ds;
        }
        private void get_Tk_mon_datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("Exec QTV_TatCaTongKetMon");
                Tk_mon_datagird.ItemsSource = dt.DefaultView;
                Tk_mon_datagird.Visibility = Visibility.Visible;
                Tk_mon_timkiem_datagird.Visibility = Visibility.Hidden;
                Tk_mon_bt_in.Visibility = Visibility.Hidden;

            }
            catch
            { }

        }


        private void Tongket_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_tk_mon();
            get_Tk_mon_datagrid();
        }

        private void get_namhoc_tk_lop()
        {
            var ds = new List<string>();
            DataTable dt1 = db.sql_select("select distinct Nam from NamHoc");
            DataRow r;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                r = dt1.Rows[i];
                ds.Add(r[0].ToString());
            }
            Tk_lop_cb_namhoc.DataContext = ds;
        }
        private void Tk_lop_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_tk_lop();
        }

        private void Tk_mon_refresh_click(object sender, RoutedEventArgs e)
        {
            get_Tk_mon_datagrid();
        }

        private void Tk_mon_in_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string duongdan = "D:/BangTongKetMon_" + Tk_mon_cb_namhoc.Text + "_" + Tk_mon_cb_kihoc.Text + ".csv";
                if (MessageBox.Show("Bạn có muốn in vào " + duongdan + "?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    //do no stuff
                    return;
                }
                else
                {
                    ExportToCsv("Exec QTV_TongKetMon '" + Tk_mon_cb_namhoc.Text + "','" + Tk_mon_cb_kihoc.Text + "'", Tk_mon_timkiem_datagird, duongdan);
                    Tk_mon_lb_errorout.Content = "In thành công.";
                    Tk_mon_lb_errorout.Background = Brushes.LightGreen;
                }
            }
            catch
            {
                Tk_mon_lb_errorout.Content = "In thất bại!!!";
                Tk_mon_lb_errorout.Background = Brushes.IndianRed;
            }
        }

        private void Tk_mon_timkiem_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_TongKetMon '" + Tk_mon_cb_namhoc.Text + "','" + Tk_mon_cb_kihoc.Text + "'";
                DataTable dt = db.sql_select(query);
                if (dt.Rows.Count == 0)
                {
                    Tk_mon_lb_errorout.Content = "Bảng tổng kết môn rỗng!!!";
                    Tk_mon_lb_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {
                        Tk_mon_lb_errorout.Content = "Vui lòng chọn năm học!!!";
                        Tk_mon_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Tk_mon_lb_errorout.Content = "Lỗi giao tác!!!";
                        Tk_mon_lb_errorout.Background = Brushes.IndianRed;
                    }

                    else if (loi == "-2")
                    {
                        Tk_mon_lb_errorout.Content = "Vui lòng chọn kì học!!!";
                        Tk_mon_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else
                    {
                        Tk_mon_lb_errorout.Content = "Tìm kiếm thành công.";
                        Tk_mon_lb_errorout.Background = Brushes.LightGreen;
                        Tk_mon_timkiem_datagird.ItemsSource = dt.DefaultView;
                        Tk_mon_datagird.Visibility = Visibility.Hidden;
                        Tk_mon_timkiem_datagird.Visibility = Visibility.Visible;
                        Tk_mon_bt_in.Visibility = Visibility.Visible;

                    }
                }

            }
            catch
            {

            }
        }

        private void get_lophoc_tk_lop(string nam)
        {
            try
            {
                var ds = new List<string>();
                DataTable dt1 = db.sql_select("select distinct TenLop from Lop where Nam='" + nam + "'");
                DataRow r;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    r = dt1.Rows[i];
                    ds.Add(r[0].ToString());
                }
                Tk_lop_cb_lop.DataContext = ds;

            }
            catch { }

        }

        private void Tk_lop_cb_namhoc_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string nam = Tk_lop_cb_namhoc.SelectedItem.ToString();
                get_lophoc_tk_lop(nam);
            }
            catch
            {

            }
        }

        private void get_tk_lop_datagrid()
        {
            try
            {
                string query = "Exec QTV_LayBangTongKetLop '" + Tk_lop_cb_namhoc.Text + "','" + Tk_lop_cb_lop.Text + "',N'" + Tk_lop_cb_kihoc.Text + "'";
                DataTable dt = db.sql_select(query);
                if (dt.Rows.Count == 0)
                {
                    Tk_lop_lb_errorout.Content = "Bảng rỗng :(";
                    Tk_lop_lb_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {
                        Tk_lop_lb_errorout.Content = "Vui lòng chọn năm học!!!";
                        Tk_lop_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Tk_lop_lb_errorout.Content = "Lỗi giao tác!!!";
                        Tk_lop_lb_errorout.Background = Brushes.IndianRed;
                    }

                    else if (loi == "-2")
                    {
                        Tk_lop_lb_errorout.Content = "Vui lòng chọn lớp học!!!";
                        Tk_lop_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-2")
                    {
                        Tk_lop_lb_errorout.Content = "Vui lòng chọn kì học!!!";
                        Tk_lop_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else
                    {
                        Tk_lop_lb_errorout.Content = "Tìm kiếm thành công.";
                        Tk_lop_lb_errorout.Background = Brushes.LightGreen;
                        Tk_lop_datagird.ItemsSource = dt.DefaultView;

                    }
                }
            }
            catch
            { }
        }


        private void Tk_lop_timkiem_click(object sender, RoutedEventArgs e)
        {
            get_tk_lop_datagrid();
        }

        private void Tk_lop_refresh_click(object sender, RoutedEventArgs e)
        {
            get_tk_lop_datagrid();
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
                    string query = "Exec QTV_CapNhatThongTinCaNhan '" + magv + "',N'" + TT_tb_hoten.Text + "','" + TT_tb_ngaysinh.Text + "','" + TT_cb_gioitinh.Text + "'" +
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

        private void bt_close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tn_laydanhsachmon()
        {
            var ds = new List<string>();
            DataTable dt = db.sql_select("select TenLop+' / '+Nam from Lop");
            //foreach(DataRow r in dt.Rows)
            DataRow r;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = dt.Rows[i];
                ds.Add(r[0].ToString());
            }
            Lh_Combobox_dslop.DataContext = ds;
        }
        private void Lh_Load(object sender, RoutedEventArgs e)
        {
            tn_laydanhsachmon();
        }

        private void Tk_lop_in_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tk_lop_datagird.Items.Count == 0)
                {
                    Tk_lop_lb_errorout.Content = "Bảng rỗng !!!";
                    Tk_lop_lb_errorout.Background = Brushes.IndianRed;
                    return;
                }
                else
                {
                    string duongdan = "D:/BangTongKetLop_" + Tk_lop_cb_namhoc.Text + "_" + Tk_lop_cb_lop.Text + "_" + Tk_lop_cb_kihoc.Text + ".csv";
                    if (MessageBox.Show("Bạn có muốn in vào " + duongdan + "?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        //do no stuff
                        return;
                    }
                    else
                    {
                        ExportToCsv("Exec QTV_LayBangTongKetLop '" + Tk_lop_cb_namhoc.Text + "','" + Tk_lop_cb_lop.Text + "',N'" + Tk_lop_cb_kihoc.Text + "'", Tk_lop_datagird, duongdan);
                        Tk_lop_lb_errorout.Content = "In thành công.";
                        Tk_lop_lb_errorout.Background = Brushes.LightGreen;
                    }
                }

            }
            catch
            {
                Tk_lop_lb_errorout.Content = "In thất bại!!!";
                Tk_lop_lb_errorout.Background = Brushes.IndianRed;
            }
        }

        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TT_button_background()
        {
            if (TT_tabitem.IsFocused)
            {
                TT_shortcut.Background =Brushes.Navy;
            }
            else
                TT_shortcut.Background =Brushes.Transparent;
        }
        private void TT_shortcut_Click(object sender, RoutedEventArgs e)
        {
            TT_tabitem.Focus(); 
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TT_button_background();
        }
    }
}
