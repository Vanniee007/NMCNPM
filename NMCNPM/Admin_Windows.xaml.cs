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


//       16:17:15

namespace NMCNPM
{
    /// <summary>
    /// Interaction logic for Admin_Windows.xaml
    /// </summary>
    public partial class Admin_Windows : Window
    {
        public string username = "";
        public string maqt;
        DBConnect db = new DBConnect();

        //public  { get; set; }
        public Admin_Windows(string username_)
        {
            InitializeComponent();
            username = username_;   
            maqt = TT_layMa(username);
            if (maqt == null)
            {
                MessageBox.Show("Admin không tồn tại");
                this.Close();
            }
        }
        private string TT_layMa(string username)
        {
            try
            {
                string str;
                str = db.sql_select("select MaQT from QuanTri where username = '"+username+"'").Rows[0][0].ToString();
                return str;
            }
            catch
            {
                return null;
            }
        }
        /*_________________________________________________________________________ 
         __________________________________________________________________________
         _________________________________ Windows ________________________________
         __________________________________________________________________________
         __________________________________________________________________________*/


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

        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindows lg = new LoginWindows(username);
            this.Close();
            lg.Show();
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

        /*_________________________________________________________________________ 
         __________________________________________________________________________
         ________________________________ Tài Khoản _______________________________
         __________________________________________________________________________
         __________________________________________________________________________*/


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

                string query = "qtv_danhsachtaikhoan N'"+ Tk_tb_search.Text+"','"+ role+"','"+username+"'";
                Tk_datagird.ItemsSource = db.sql_select(query).DefaultView;
                //Lh_button_them.IsEnabled = false;
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
            try
            {
                if (Lh_LayTaiKhoanDaChon().Count() > 0)
                {
                    var dstaikhoan = Lh_LayTaiKhoanDaChon();
                    int ketqua;
                    int success = 0, fail = 0;
                    foreach (string username in dstaikhoan)
                    {
                        ketqua = (int)db.sql_select("qtv_xoataikhoan '"+username+"'").Rows[0][0];
                        if (ketqua ==0)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                    if (fail == 0)
                        Tk_lb_errorout.Content = "Đã xoá "+success.ToString()+" dòng";
                    else
                        Tk_lb_errorout.Content = "Đã xoá "+success.ToString()+" dòng, "+fail.ToString()+" dòng không thể xoá";
                    if (success==0)
                        Tk_lb_errorout.Foreground=Brushes.IndianRed;
                    else
                    {
                        Tk_load_datagrid();
                        Tk_lb_errorout.Foreground=Brushes.MediumSeaGreen;
                    }
                }
            }
            catch { }
        }

        private void Tk_button_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tk_Combobox_role.Text == "")
                {
                    Tk_lb_errorout.Content = "Chọn role cần thêm";
                    Tk_lb_errorout.Foreground = Brushes.IndianRed;
                    return;
                }
                DataRowView rowview = (DataRowView)Tk_datagird.SelectedItem;
                Admin_themTaiKhoan manager = new Admin_themTaiKhoan(Tk_Combobox_role.Text.ToLower(), 0, "Thêm");
                manager.ShowDialog();
                Tk_load_datagrid();
            }
            catch { }
        }

        private void Tk_button_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Tk_datagird.SelectedItem;
                Admin_themTaiKhoan manager = new Admin_themTaiKhoan(Tk_Combobox_role.Text.ToLower(), (int)rowview[0], "Sửa");
                //Admin_themTaiKhoan ad = new Admin_themTaiKhoan("gia", (int)rowview[0], "Sửa");
                manager.ShowDialog();
                Tk_load_datagrid();
            }
            catch
            {
                Tk_lb_errorout.Content = "Chọn dòng cần sửa";
                Tk_lb_errorout.Foreground = Brushes.IndianRed;
            }
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


        /*_________________________________________________________________________ 
         __________________________________________________________________________
         __________________________________Lớp học_________________________________
         __________________________________________________________________________
         __________________________________________________________________________*/
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
            Lh_button_them.Visibility = Visibility.Hidden;
                Lh_button_xoa.IsEnabled = true;
            Lh_button_xoa.Visibility = Visibility.Visible;
            }
            catch { }
        }
        private void Lh_load_datagrid_refresh()
        {
            try
            {

                Lh_button_them.IsEnabled = true;
                Lh_button_them.Visibility = Visibility.Visible;

                Lh_button_xoa.IsEnabled = false;
                Lh_button_xoa.Visibility = Visibility.Hidden;
                Lh_datagird_dshocsinh.ItemsSource = db.sql_select("qtv_dshs '"+Lh_Combobox_dsnam.SelectedItem.ToString()+"'").DefaultView;
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

        }

        private void Lh_Combobox_dslop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lh_load_datagrid();
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
        List<string> Lh_LayTaiKhoanDaChon()
        {
            /// Lấy list index dòng đã chọn
            var SelectedList = new List<string>();
            for (int i = 0; i < Tk_datagird.Items.Count; i++)
            {
                var item = Tk_datagird.Items[i];
                var mycheckbox = Tk_datagird.Columns[9].GetCellContent(item) as CheckBox;
                if ((bool)mycheckbox.IsChecked)
                {
                    TextBlock x = Tk_datagird.Columns[8].GetCellContent(Tk_datagird.Items[i]) as TextBlock;
                    SelectedList.Add(x.Text);
                }
            }
            return SelectedList;
        }
        private void Lh_HienMaLoi(int MaLoi)
        {

        }
        private void Lh_themDiemHocSinh(string MaHS)
        {
            var ds = new List<string>();
            ds.Add("Toán");
            ds.Add("Lí");
            ds.Add("Hóa");
            ds.Add("Sinh");
            ds.Add("Sử");
            ds.Add("Địa");
            ds.Add("Văn");
            ds.Add("Đạo đức");
            ds.Add("Thể dục"); 
            foreach (string TenMon in ds)
            {
                db.sql_select("QTV_ThemDiemHocSinh '"+MaHS+"',N'"+TenMon+"','"+Lh_Combobox_dsnam.SelectedItem.ToString()+"'");

            }

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
                            Lh_themDiemHocSinh(MaHS);
                            /*
                            QTV_ThemDiemHocSinh
                            @MaHS int,
                            @TenMon nvarchar(30),
                            @Nam varchar(12)
                            */
                        }
                        else
                        {
                            fail++;
                        }

                    }
                    Lh_load_datagrid();
                    if (fail == 0)
                        Lh_lb_errorout.Content = "Đã thêm "+success.ToString()+" dòng";
                    else
                        Lh_lb_errorout.Content = "Đã thêm "+success.ToString()+" dòng, "+fail.ToString()+" dòng không thể thêm";
                    if (success==0)
                        Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    else 
                        Lh_lb_errorout.Foreground=Brushes.MediumSeaGreen;
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
                    if (fail == 0)
                        Lh_lb_errorout.Content = "Đã xoá "+success.ToString()+" dòng";
                    else

                        Lh_lb_errorout.Content = "Đã xoá "+success.ToString()+" dòng, "+fail.ToString()+" dòng không thể xoá";
                    if (success==0)
                        Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    else
                        Lh_lb_errorout.Foreground=Brushes.MediumSeaGreen;
                }
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


        /*_________________________________________________________________________ 
         __________________________________________________________________________
         _________________________________ Tra cứu ________________________________
         __________________________________________________________________________
         __________________________________________________________________________*/

        private void Tc_timkiem()
        {
            try
            {
                Tc_datagrid_searchresult.ItemsSource = db.sql_select("qtv_tracuu N'" + Tc_tb_search.Text + "'").DefaultView;
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

        private void Tc_button_tracuu_click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Tc_datagrid_searchresult.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Tc_datagrid_searchresult.SelectedItem;
                    if (rowview != null)
                    {
                        MessageBox.Show(rowview["username"].ToString());
                    }
                }
            }
            catch
            { }
        }















        /*======================================KHÓA HỌC========================================
         * ======================================================================================
         * ======================================================================================*/

        /*======================================KH_Năm học========================================*/
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




        private void get_datagrid_kh_nh()
        {
            try
            {
                DataTable dt = db.sql_select("select * from NamHoc");
                Kh_nh_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            { }
        }
        private void Kh_nh_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            get_datagrid_kh_nh();
        }

        private void Kh_nh_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Kh_nh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_nh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Kh_nh_tb_namhoc.Text = rowview["Nam"].ToString();
                        Kh_nh_tb_tuoitoithieu.Text = rowview["TuoiToiThieu"].ToString();
                        Kh_nh_tb_tuoitoida.Text = rowview["TuoiToiDa"].ToString();
                    }
                }
            }
            catch
            {

            }
        }

        private void Kh_nh_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_ThemNamHoc '" + Kh_nh_tb_namhoc.Text + "'," +
               "'" + Kh_nh_tb_tuoitoithieu.Text + "' ," +"'" + Kh_nh_tb_tuoitoida.Text + "'";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Kh_nh_errorout.Content = "Có trường thông tin bị trống!!!";
                    Kh_nh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Kh_nh_errorout.Content = "Lỗi giao tác!!!";
                    Kh_nh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-2")
                {
                    Kh_nh_errorout.Content = "Năm học đã tồn tại!!!";
                    Kh_nh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-3")
                {
                    Kh_nh_errorout.Content = "Tuổi bạn nhập không hợp lệ!!!";
                    Kh_nh_errorout.Foreground = Brushes.IndianRed;
                }
                else
                {
                    Kh_nh_errorout.Content = "Thêm năm học thành công.";
                    Kh_nh_errorout.Foreground = Brushes.MediumSeaGreen;
                    get_datagrid_kh_nh();


                }
            }
            catch { }
        }

        private void Kh_nh_btn_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Kh_nh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_nh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec QTV_SuaNamHoc  '" + rowview["Nam"].ToString() + "','" + Kh_nh_tb_tuoitoithieu.Text + "', '" + Kh_nh_tb_tuoitoida.Text + "'";
                        DataTable dt = db.sql_select(query);
                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Kh_nh_errorout.Content = "Tuổi bạn nhập không hợp lệ!!!";
                            Kh_nh_errorout.Foreground = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Kh_nh_errorout.Content = "Lỗi giao tác!!!";
                            Kh_nh_errorout.Foreground = Brushes.IndianRed;
                        }

                        else
                        {
                            Kh_nh_errorout.Content = "Sửa thành công.";
                            Kh_nh_errorout.Foreground = Brushes.MediumSeaGreen;
                            get_datagrid_kh_nh();
                        }

                    }
                    else
                    {
                        Kh_nh_errorout.Content = "Vui lòng chọn user cần sửa!!!";
                        Kh_nh_errorout.Foreground = Brushes.IndianRed;
                    }

                }
            }
            catch
            {

            }
        }

        private void Kh_nh_btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Kh_nh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_nh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec QTV_XoaNamHoc  '" + rowview["Nam"].ToString() + "'";
                        DataTable dt = db.sql_select(query);
                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Kh_nh_errorout.Content = "Không thể xóa!!!";
                            Kh_nh_errorout.Foreground = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Kh_nh_errorout.Content = "Lỗi giao tác!!!";
                            Kh_nh_errorout.Foreground = Brushes.IndianRed;
                        }
                        else
                        {
                            Kh_nh_errorout.Content = "Xóa thành công.";
                            Kh_nh_errorout.Foreground = Brushes.MediumSeaGreen;
                            get_datagrid_kh_nh();
                        }

                    }
                    else
                    {
                        Kh_nh_errorout.Content = "Vui lòng chọn user cần sửa!!!";
                        Kh_nh_errorout.Foreground = Brushes.IndianRed;
                    }

                }
            }
            catch
            {

            }
        }
        /*======================================KH_Lớp học========================================*/
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
            Kh_lh_cb_nam.DataContext = ds;
        }
        private void get_datagrid_kh_lh()
        {
            try
            {
                DataTable dt = db.sql_select("select * from Lop");
                Kh_lh_datagrid.ItemsSource = dt.DefaultView;

            }
            catch
            { }
        }
        private void Kh_lh_loaded(object sender, RoutedEventArgs e)
        {
            get_datagrid_kh_lh();
            get_namhoc_kh_lh();
        }

        private void Kh_lh_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_ThemLopHoc '" + Kh_lh_tb_tenlop.Text + "','" + Kh_lh_cb_nam.Text + "'," +
               "'" + Kh_lh_tb_siso.Text + "' ";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Kh_lh_errorout.Content = "Có trường thông tin bị trống!!!";
                    Kh_lh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Kh_lh_errorout.Content = "Lỗi giao tác!!!";
                    Kh_lh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-2")
                {
                    Kh_lh_errorout.Content = "Lớp học đã tồn tại!!!";
                    Kh_lh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-3")
                {
                    Kh_lh_errorout.Content = "Sĩ số tối đa không hợp lệ!!!";
                    Kh_lh_errorout.Foreground = Brushes.IndianRed;
                }
                else
                {
                    Kh_lh_errorout.Content = "Thêm lớp học thành công.";
                    Kh_lh_errorout.Foreground = Brushes.MediumSeaGreen;
                    get_datagrid_kh_lh();


                }

            }
            catch { }
        }

        private void Kh_lh_btn_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Kh_lh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_lh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec QTV_SuaLopHoc  '" + rowview["TenLop"].ToString() + "','" + rowview["Nam"].ToString() + "', '" + Kh_lh_tb_siso.Text + "'";
                        DataTable dt = db.sql_select(query);
                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Kh_lh_errorout.Content = "Sĩ số tối đa không hợp lệ!!!";
                            Kh_lh_errorout.Foreground = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Kh_lh_errorout.Content = "Lỗi giao tác!!!";
                            Kh_lh_errorout.Foreground = Brushes.IndianRed;
                        }
                        else if (loi == "-2")
                        {
                            Kh_lh_errorout.Content = "Sĩ số hiện lớp hiện tại lớn hơn bạn nhập!!!";
                            Kh_lh_errorout.Foreground = Brushes.IndianRed;
                        }

                        else
                        {
                            Kh_lh_errorout.Content = "Sửa thành công.";
                            Kh_lh_errorout.Foreground = Brushes.MediumSeaGreen;
                            get_datagrid_kh_lh();
                        }

                    }
                    else
                    {
                        Kh_lh_errorout.Content = "Vui lòng chọn user cần sửa!!!";
                        Kh_lh_errorout.Foreground = Brushes.IndianRed;
                    }

                }
            }
            catch
            {

            }
        }

        private void Kh_lh_btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Kh_lh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_lh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec QTV_XoaLopHoc  '" + rowview["TenLop"].ToString() + "','" + rowview["Nam"].ToString() + "'";
                        DataTable dt = db.sql_select(query);
                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Kh_lh_errorout.Content = "Không thể xóa!!!";
                            Kh_lh_errorout.Foreground = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Kh_lh_errorout.Content = "Lỗi giao tác!!!";
                            Kh_lh_errorout.Foreground = Brushes.IndianRed;
                        }

                        else
                        {
                            Kh_lh_errorout.Content = "Xóa thành công.";
                            Kh_lh_errorout.Foreground = Brushes.MediumSeaGreen;
                            get_datagrid_kh_lh();
                        }

                    }
                    else
                    {
                        Kh_lh_errorout.Content = "Vui lòng chọn user cần sửa!!!";
                        Kh_lh_errorout.Foreground = Brushes.IndianRed;
                    }

                }
            }
            catch
            {

            }
        }

        private void Kh_lh_datagrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Kh_lh_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Kh_lh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_lh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Kh_lh_tb_tenlop.Text = rowview["TenLop"].ToString();
                        Kh_lh_cb_nam.Text = rowview["Nam"].ToString();
                        Kh_lh_tb_siso.Text = rowview["SiSo"].ToString();

                    }
                }
            }
            catch
            {

            }
        }
        /*======================================KH_Môn học========================================*/
        private void get_namhoc_mh_lh()
        {
            var ds = new List<string>();
            DataTable dt1 = db.sql_select("select distinct Nam from NamHoc");
            DataRow r;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                r = dt1.Rows[i];
                ds.Add(r[0].ToString());
            }
            Kh_mh_cb_nam.DataContext = ds;
        }
        private void get_datagrid_kh_mh()
        {
            try
            {
                DataTable dt = db.sql_select("select * from MonHoc");
                Kh_mh_datagrid.ItemsSource = dt.DefaultView;

            }
            catch
            { }
        }
        private void Kh_mh_loaded(object sender, RoutedEventArgs e)
        {
            get_datagrid_kh_mh();
            get_namhoc_mh_lh();
        }

        private void Kh_mh_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_ThemMonHoc N'" + Kh_mh_cb_tenmon.Text + "','" + Kh_mh_cb_nam.Text + "'," +
               "'" + Kh_mh_tb_diemdat.Text + "' ";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Kh_mh_errorout.Content = "Có trường thông tin bị trống!!!";
                    Kh_mh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Kh_mh_errorout.Content = "Lỗi giao tác!!!";
                    Kh_mh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-2")
                {
                    Kh_mh_errorout.Content = "Môn học đã tồn tại!!!";
                    Kh_mh_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-3")
                {
                    Kh_mh_errorout.Content = "Điểm đạt bạn nhập không hợp lệ!!!";
                    Kh_mh_errorout.Foreground = Brushes.IndianRed;
                }
                else
                {
                    Kh_mh_errorout.Content = "Thêm môn học thành công.";
                    Kh_mh_errorout.Foreground = Brushes.MediumSeaGreen;

                    List<string> dshocsinh = new List<string>();
                    DataTable dt1 = db.sql_select("select MaHocSinh from DanhSachLopHoc where Nam='" + Kh_mh_cb_nam.Text+"'");
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        dshocsinh.Add(dt1.Rows[i][0].ToString());
                    }
                    get_datagrid_kh_mh();
                    foreach (var i in dshocsinh)
                    {
                        query = "Exec QTV_ThemDiemChoHocSinh '" + i + "',N'" + Kh_mh_cb_tenmon.Text + "','" + Kh_mh_cb_nam.Text + "'";
                        dt1 = db.sql_select(query);
                    }
                }

            }
            catch { }
        }

        private void Kh_mh_btn_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Kh_mh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_mh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec QTV_SuaMonHoc  N'" + rowview["TenMon"].ToString() + "','" + rowview["Nam"].ToString() + "', '" + Kh_mh_tb_diemdat.Text + "'";
                        DataTable dt = db.sql_select(query);
                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Kh_mh_errorout.Content = "Điểm đạt không hợp lệ!!!";
                            Kh_mh_errorout.Foreground = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Kh_mh_errorout.Content = "Lỗi giao tác!!!";
                            Kh_mh_errorout.Foreground = Brushes.IndianRed;
                        }

                        else
                        {
                            Kh_mh_errorout.Content = "Sửa thành công.";
                            Kh_mh_errorout.Foreground = Brushes.MediumSeaGreen;
                            get_datagrid_kh_mh();
                        }

                    }
                    else
                    {
                        Kh_mh_errorout.Content = "Vui lòng chọn user cần sửa!!!";
                        Kh_mh_errorout.Foreground = Brushes.IndianRed;
                    }

                }
            }
            catch
            {

            }
        }

        private void Kh_mh_btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Kh_mh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_mh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec QTV_XoaMonHoc  N'" + rowview["TenMon"].ToString() + "','" + rowview["Nam"].ToString() + "'";
                        DataTable dt = db.sql_select(query);
                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Kh_mh_errorout.Content = "Không thể xóa!!!";
                            Kh_mh_errorout.Foreground = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Kh_mh_errorout.Content = "Lỗi giao tác!!!";
                            Kh_mh_errorout.Foreground = Brushes.IndianRed;
                        }

                        else
                        {
                            Kh_mh_errorout.Content = "Xóa thành công.";
                            Kh_mh_errorout.Foreground = Brushes.MediumSeaGreen;
                            get_datagrid_kh_mh();
                        }

                    }
                    else
                    {
                        Kh_mh_errorout.Content = "Vui lòng chọn user cần sửa!!!";
                        Kh_mh_errorout.Foreground = Brushes.IndianRed;
                    }

                }
            }
            catch
            {

            }
        }

        private void Kh_mh_datagrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Kh_mh_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Kh_mh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Kh_mh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Kh_mh_cb_tenmon.Text = rowview["TenMon"].ToString();
                        Kh_mh_cb_nam.Text = rowview["Nam"].ToString();
                        Kh_mh_tb_diemdat.Text = rowview["DiemDat"].ToString();

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
        private void Bd_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_bd();
            get_Bd_datagrid();
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
        private void Bd_datagrid_loaded(object sender, RoutedEventArgs e)
        {

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
                Bd_cb_mon.DataContext = ds;

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
              "'" + Bd_cb_kihoc.Text + "',N'"+Bd_cb_mon.Text+"' ";
                DataTable dt = db.sql_select(query);
                if (dt.Rows.Count == 0)
                {
                    Bd_errorout.Content = "Bảng điểm rỗng!!!";
                    Bd_errorout.Foreground = Brushes.IndianRed;
                    Bd_timkiem_datagird.ItemsSource = dt.DefaultView;
                    Bd_datagird_tonghop.Visibility = Visibility.Hidden;
                    Bd_timkiem_datagird.Visibility = Visibility.Visible;
                    Bd_bt_in.Visibility = Visibility.Visible;
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
                        Bd_errorout.Content = "Vui lòng chọn lớp học!!!";
                        Bd_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-3")
                    {
                        Bd_errorout.Content = "Vui lòng chọn kì học!!!";
                        Bd_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-4")
                    {
                        Bd_errorout.Content = "Vui lòng chọn môn học!!!";
                        Bd_errorout.Foreground = Brushes.IndianRed;
                    }
                    else
                    {
                        Bd_errorout.Content = "Tìm kiếm thành công.";
                        Bd_errorout.Foreground = Brushes.MediumSeaGreen;
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

        private void Bd_timkiem_datagird_loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Bd_refresh_click(object sender, RoutedEventArgs e)
        {
            get_Bd_datagrid();
        }

        private void Bd_in_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string folderPath = ChonFolder();
                if (folderPath ==null)
                    return;
                string duongdan = folderPath+"\\BangDiem_" + Bd_cb_namhoc.Text + "_" + Bd_cb_lop.Text + "_" + Bd_cb_kihoc.Text + "_" + Bd_cb_mon.Text + ".csv";
                if (MessageBox.Show("Bạn có muốn in vào " + duongdan + "?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    //do no stuff
                    return;
                }
                else
                {
                    ExportToCsv("Exec QTV_LayBangDiem '" + Bd_cb_namhoc.Text + "','" + Bd_cb_lop.Text + "'," +
                  "'" + Bd_cb_kihoc.Text + "',N'" + Bd_cb_mon.Text + "' ", Bd_timkiem_datagird, duongdan);
                    Bd_errorout.Content = "In thành công.";
                    Bd_errorout.Foreground = Brushes.MediumSeaGreen;
                }
            }
            catch
            {
                Bd_errorout.Content = "In thất bại!!!";
                Bd_errorout.Foreground = Brushes.IndianRed;
            }

        }
        /*======================================TỔNG KẾT========================================
         * ======================================================================================
         * ======================================================================================*/
        /*==========================================TỔNG KẾT- THEO MÔN=========================================*/

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

        private void Tk_mon_timkiem_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_TongKetMon '" + Tk_mon_cb_namhoc.Text + "','" + Tk_mon_cb_kihoc.Text + "'";
                DataTable dt = db.sql_select(query);
                string loi;
                if (dt.Rows.Count == 0)
                {
                    Tk_mon_lb_errorout.Content = "Bảng rỗng :("; 
                    Tk_mon_lb_errorout.Foreground = Brushes.IndianRed;
                    Tk_mon_timkiem_datagird.ItemsSource = dt.DefaultView;
                    Tk_mon_timkiem_datagird.Visibility=Visibility.Visible;
                    return;
                }
                else
                    loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Tk_mon_lb_errorout.Content = "Vui lòng chọn năm học!!!";
                    Tk_mon_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Tk_mon_lb_errorout.Content = "Lỗi giao tác!!!";
                    Tk_mon_lb_errorout.Foreground = Brushes.IndianRed;
                }

                else if (loi == "-2")
                {
                    Tk_mon_lb_errorout.Content = "Vui lòng chọn kì học!!!";
                    Tk_mon_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else
                {
                    Tk_mon_lb_errorout.Content = "Tìm kiếm thành công.";
                    Tk_mon_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                    Tk_mon_timkiem_datagird.ItemsSource = dt.DefaultView;
                    Tk_mon_datagird.Visibility = Visibility.Hidden;
                    Tk_mon_timkiem_datagird.Visibility = Visibility.Visible;
                    Tk_mon_bt_in.Visibility = Visibility.Visible;

                }

            }
            catch
            {

            }
        }

        private void Tk_mon_refresh_click(object sender, RoutedEventArgs e)
        {
            get_Tk_mon_datagrid();
        }

        private string ChonFolder()
        {
            var dlg = new FolderPicker();
            dlg.InputPath = @"c:\windows\system32";
            if (dlg.ShowDialog() == true)
            {
                return dlg.ResultPath;
            }
            return null;
        }
        private void Tk_mon_in_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string folderPath = ChonFolder();
                if (folderPath ==null)
                    return;
                string duongdan = folderPath+"\\BangTongKetMon_" + Tk_mon_cb_namhoc.Text + "_" + Tk_mon_cb_kihoc.Text + ".csv";
                if (MessageBox.Show("Bạn có muốn in vào "+duongdan+ "?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    //do no stuff
                    return;
                }
                else
                {
                    ExportToCsv("Exec QTV_TongKetMon '" + Tk_mon_cb_namhoc.Text + "','" + Tk_mon_cb_kihoc.Text + "'", Tk_mon_timkiem_datagird, duongdan);
                    Tk_mon_lb_errorout.Content = "In thành công.";
                    Tk_mon_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                }
            }
            catch
            {
                Tk_mon_lb_errorout.Content = "In thất bại!!!";
                Tk_mon_lb_errorout.Foreground = Brushes.IndianRed;
            }
        }




        /*==========================================TỔNG KẾT- THEO LỚP=========================================*/

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
        private void Tk_lop_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_tk_lop();
        }

        private void Tk_lop_timkiem_click(object sender, RoutedEventArgs e)
        {
            get_tk_lop_datagrid();
        }

        private void Tk_lop_refresh_click(object sender, RoutedEventArgs e)
        {
            get_tk_lop_datagrid();
        }

        private void Tk_lop_in_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tk_lop_datagird.Items.Count == 0)
                {
                    Tk_lop_lb_errorout.Content = "Bảng rỗng !!!";
                    Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
                    return;
                }
                else
                {
                    string folderPath = ChonFolder();
                    if (folderPath ==null)
                        return;
                    string duongdan = folderPath+"\\BangTongKetLop_" + Tk_lop_cb_namhoc.Text + "_" + Tk_lop_cb_lop.Text + "_"+Tk_lop_cb_kihoc.Text+ ".csv";
                    if (MessageBox.Show("Bạn có muốn in vào " + duongdan + "?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        //do no stuff
                        return;
                    }
                    else
                    {
                        ExportToCsv("Exec QTV_LayBangTongKetLop '" + Tk_lop_cb_namhoc.Text + "','" + Tk_lop_cb_lop.Text + "',N'" + Tk_lop_cb_kihoc.Text + "'", Tk_lop_datagird, duongdan);
                        Tk_lop_lb_errorout.Content = "In thành công.";
                        Tk_lop_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                    }
                }

            }
            catch
            {
                Tk_lop_lb_errorout.Content = "In thất bại!!!";
                Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
            }
        }
        private void get_tk_lop_datagrid()
        {
            try
            {
                string query = "Exec QTV_LayBangTongKetLop '" + Tk_lop_cb_namhoc.Text + "','" + Tk_lop_cb_lop.Text + "',N'" + Tk_lop_cb_kihoc.Text + "'";
                DataTable dt = db.sql_select(query); 
                string loi;
                if (dt.Rows.Count == 0)
                {
                    Tk_lop_lb_errorout.Content = "Bảng rỗng :(";
                    Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
                    Tk_lop_datagird.ItemsSource = dt.DefaultView;
                    return;
                }
                else
                    loi = dt.Rows[0][0].ToString();

                if (loi == "-1")
                {
                    Tk_lop_lb_errorout.Content = "Vui lòng chọn năm học!!!";
                    Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Tk_lop_lb_errorout.Content = "Lỗi giao tác!!!";
                    Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
                }

                else if (loi == "-2")
                {
                    Tk_lop_lb_errorout.Content = "Vui lòng chọn lớp học!!!";
                    Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-3")
                {
                    Tk_lop_lb_errorout.Content = "Vui lòng chọn kì học!!!";
                    Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else
                {
                    Tk_lop_lb_errorout.Content = "Tìm kiếm thành công.";
                    Tk_lop_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                    Tk_lop_datagird.ItemsSource = dt.DefaultView;
                }
            }
            catch
            { }
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
                TT_tb_sodienthoai.Text = "0"+ dt.Rows[0]["SDT"].ToString();
                TT_tb_diachi.Text = dt.Rows[0]["DiaChi"].ToString();
                maqt= dt.Rows[0]["MaQT"].ToString();
            }
            catch
            {
                Tt_lb_thongtincanhan_errorout.Content = "Không tìm được thông tin!!!";
                Tt_lb_thongtincanhan_errorout.Foreground = Brushes.IndianRed;
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

        private void Tt_doimatkhau_loaded(object sender, RoutedEventArgs e)
        {
            TT_tb_taikhoan.Text = username;
        }

        private void TT_shortcut_Click(object sender, RoutedEventArgs e)
        {
            TT_tabitem.Focus();
            TT_shortcut.Background = Brushes.Navy;
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
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TT_button_background();
        }

        private void Dashboard_media_MediaEnded(object sender, RoutedEventArgs e)
        {
            Dashboard_media.Position = new TimeSpan(0, 0, 1);
        }
    }
}
