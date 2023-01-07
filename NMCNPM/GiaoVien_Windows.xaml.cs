using Microsoft.Win32;
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
        public string username = "";
        public string magv;
        public string mon;
        DBConnect db = new DBConnect();

        public GiaoVien_Windows(string username_)
        {
            InitializeComponent();
            username = username_;
            magv = TT_layMa(username); 
            if (magv == null)
            {
                MessageBox.Show("Giáo viên không tồn tại");
                this.Close();
            }
        }
        private string TT_layMa(string username)
        {
            try
            {
                string str;
                str = db.sql_select("select MaGV from GiaoVien where username = '"+username+"'").Rows[0][0].ToString();
                return str;
            }
            catch
            {
                return null;
            }
        }
        private void Tt_loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ádasdsad");
            Tt_getThongTin();
            TT_button_background();
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


        private void Bd_datagrid_loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Bd_timkiem_datagird_loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Bc_get_datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("Exec GV_TongKetMon N'"+TT_get_monGVDay()+"'");
                Tk_mon_datagird.ItemsSource = dt.DefaultView;

            }
            catch
            { }

        }
        private void get_Tk_mon_datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("Exec GV_TongKetMon '"+TT_get_monGVDay()+"'");
                Tk_mon_datagird.ItemsSource = dt.DefaultView;

            }
            catch
            { }

        }


        private void Tongket_loaded(object sender, RoutedEventArgs e)
        {

            TT_button_background(); get_Tk_mon_datagrid();
        }

        private void get_namhoc_tk_lop()
        {
            var ds = new List<string>();
            DataTable dt = db.sql_select("select distinct Nam from GiaoVien_LopHoc where MaGV = '"+magv+"'");
            DataRow r;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = dt.Rows[i];
                ds.Add(r[0].ToString());
            }
            Tk_lop_cb_namhoc.DataContext = ds;

            var ds_KyHoc = new List<string>();
            ds_KyHoc.Add("1");
            ds_KyHoc.Add("2");
            ds_KyHoc.Add("Cả năm");
            Tk_lop_cb_kihoc.DataContext = ds_KyHoc;
        }
        private void Tk_lop_loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_tk_lop();
        }

        private void Tk_mon_refresh_click(object sender, RoutedEventArgs e)
        {
            get_Tk_mon_datagrid();
        }


        private void Tk_mon_timkiem_click(object sender, RoutedEventArgs e)
        {
            Bc_get_datagrid();
            //try
            //{
            //    string query = "Exec QTV_TongKetMon '" + Tk_mon_cb_namhoc.Text + "','" + Tk_mon_cb_kihoc.Text + "'";
            //    DataTable dt = db.sql_select(query);
            //    if (dt.Rows.Count == 0)
            //    {
            //        Tk_mon_lb_errorout.Content = "Bảng tổng kết môn rỗng!!!";
            //        Tk_mon_lb_errorout.Background = Brushes.IndianRed;
            //    }
            //    else
            //    {
            //        string loi = dt.Rows[0][0].ToString();
            //        if (loi == "-1")
            //        {
            //            Tk_mon_lb_errorout.Content = "Vui lòng chọn năm học!!!";
            //            Tk_mon_lb_errorout.Background = Brushes.IndianRed;
            //        }
            //        else if (loi == "-10")
            //        {
            //            Tk_mon_lb_errorout.Content = "Lỗi giao tác!!!";
            //            Tk_mon_lb_errorout.Background = Brushes.IndianRed;
            //        }

            //        else if (loi == "-2")
            //        {
            //            Tk_mon_lb_errorout.Content = "Vui lòng chọn kì học!!!";
            //            Tk_mon_lb_errorout.Background = Brushes.IndianRed;
            //        }
            //        else
            //        {
            //            Tk_mon_lb_errorout.Content = "Tìm kiếm thành công.";
            //            Tk_mon_lb_errorout.Background = Brushes.LightGreen;
            //            Tk_mon_timkiem_datagird.ItemsSource = dt.DefaultView;
            //            Tk_mon_datagird.Visibility = Visibility.Hidden;
            //            Tk_mon_timkiem_datagird.Visibility = Visibility.Visible;
            //            Tk_mon_bt_in.Visibility = Visibility.Visible;

            //        }
            //    }

            //}
            //catch
            //{

            //}
        }

        private void get_lophoc_tk_lop(string nam)
        {
            try
            {
                var ds = new List<string>();
                DataTable dt = db.sql_select("select distinct TenLop from GiaoVien_LopHoc where Nam='" + nam + "' and MaGV = '"+magv+"'");
                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];
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
        private string TT_get_monGVDay()
        {
            string str="";
            try
            {
                str = db.sql_select("select MonDay from GiaoVien where username = '"+username+"'").Rows[0][0].ToString();
               
            }
            catch { }
            return str;
        }
        private void tk_get_lop_datagrid()
        {
            try
            {
                string query = "GV_DiemTrungBinh '" + Tk_lop_cb_namhoc.SelectedItem.ToString() + "','" + Tk_lop_cb_lop.SelectedItem.ToString() + "'," +
              "'" + Tk_lop_cb_kihoc.SelectedItem.ToString() + "',N'"+TT_get_monGVDay()+"' ";
                DataTable dt = db.sql_select(query);
                if (dt.Rows.Count == 0)
                {
                    Tk_lop_lb_errorout.Content = "Bảng điểm rỗng!!!";
                    Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
                    DataTable dt_ = db.sql_select("");
                    Tk_lop_datagird.ItemsSource =  dt_.DefaultView;
                }
                else
                {
                    string loi = dt.Rows[0][0].ToString();
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
                    else if (loi == "-4")
                    {
                        Tk_lop_lb_errorout.Content = "Vui lòng chọn môn học!!!";
                        Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
                    }
                    else
                    {
                        Tk_lop_lb_errorout.Content = "Tìm kiếm thành công.";
                        Tk_lop_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                        Tk_lop_datagird.ItemsSource = dt.DefaultView;
                        return;
                    }
                }

            }
            catch
            {

            }
        }


        private void Tk_lop_timkiem_click(object sender, RoutedEventArgs e)
        {
            tk_get_lop_datagrid();
        }

        private void Tk_lop_refresh_click(object sender, RoutedEventArgs e)
        {
            tk_get_lop_datagrid();
        }
        //private void Tt_capnhatthongtincanhan_click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Bạn có muốn cập nhật thông tin???", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
        //        {
        //            //do no stuff
        //            return;
        //        }
        //        else
        //        {

        //            //do yes stuff            
        //            string query = "Exec QTV_CapNhatThongTinCaNhan '" + magv + "',N'" + TT_tb_hoten.Text + "','" + TT_tb_ngaysinh.Text + "','" + TT_cb_gioitinh.Text + "'" +
        //                ",'" + TT_tb_email.Text + "','" + TT_tb_sodienthoai.Text + "','" + TT_tb_diachi.Text + "'";
        //            DataTable dt = db.sql_select(query);
        //            string loi = dt.Rows[0][0].ToString();
        //            if (loi == "-1")
        //            {
        //                Tt_lb_thongtincanhan_errorout.Content = "Thông tin bị trống!!!";
        //                Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
        //            }
        //            else if (loi == "-10")
        //            {
        //                Tt_lb_thongtincanhan_errorout.Content = "Lỗi giao tác!!!";
        //                Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
        //            }
        //            else
        //            {
        //                Tt_lb_thongtincanhan_errorout.Content = "Cập nhật thành công.";
        //                Tt_lb_thongtincanhan_errorout.Background = Brushes.LightGreen;
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        //private void Tt_capnhatmatkhau_click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Bạn có muốn đổi mật khẩu???", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
        //        {
        //            //do no stuff
        //            return;
        //        }
        //        else
        //        {

        //            //do yes stuff            
        //            string query = "Exec DoiMatKhau '" + username + "','" + TT_tb_matkhaucu.Password + "','" + TT_tb_matkhaumoi.Password + "' ,'" + TT_tb_matkhaumoi_2.Password + "'";
        //            DataTable dt = db.sql_select(query);
        //            string loi = dt.Rows[0][0].ToString();
        //            if (loi == "-1")
        //            {

        //                Tt_lb_doimatkhau_errorout.Content = "Sai mật khẩu cũ!!!";
        //                Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
        //            }
        //            else if (loi == "-2")
        //            {
        //                Tt_lb_doimatkhau_errorout.Content = "Mật khẩu mới bị trống!!!";
        //                Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
        //            }
        //            else if (loi == "-3")
        //            {
        //                Tt_lb_doimatkhau_errorout.Content = "Mật khẩu nhập lại không đúng!!!";
        //                Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
        //            }
        //            else if (loi == "-10")
        //            {
        //                Tt_lb_doimatkhau_errorout.Content = "Lỗi giao tác!!!";
        //                Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
        //            }
        //            else
        //            {
        //                Tt_lb_doimatkhau_errorout.Content = "Cập nhật thành công.";
        //                Tt_lb_doimatkhau_errorout.Background = Brushes.LightGreen;
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

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
                    SaveDataGridToCsv(Tk_lop_datagird);
                    Tk_lop_lb_errorout.Content = "In thành công.";
                    Tk_lop_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                }

            }
            catch
            {
                Tk_lop_lb_errorout.Content = "In thất bại!!!";
                Tk_lop_lb_errorout.Foreground = Brushes.IndianRed;
            }
        }

        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {

            LoginWindows lg = new LoginWindows(username);
            this.Close();
            lg.Show();
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
        private void TT_shortcut_Click(object sender, RoutedEventArgs e)
        {
            TT_tabitem.Focus(); 
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TT_button_background();
        }

        private void Tk_lop_cb_lop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tk_get_lop_datagrid();
        }

        private void Tk_lop_cb_kihoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tk_get_lop_datagrid();
        }

        private void Tk_lop_datagird_Loaded(object sender, RoutedEventArgs e)
        {
            get_namhoc_tk_lop();

        }


        /*=======================================THÔNG TIN====================================
         * ========================================================================
         * ========================================================================
         * ========================================================================*/
        private void Tt_listbox_lopday_get()
        {
            try
            {
                string query = "Exec GV_LayLopDay '" + magv + "'";
                DataTable dt = db.sql_select(query);
                TT_listbox_lopday.ItemsSource = dt.DefaultView;
                TT_listbox_lopday.DisplayMemberPath = "Lop";
                TT_listbox_lopday.SelectedValuePath = "Lop";
            }
            catch
            {

            }
        }
        private void Tt_getThongTin()
        {
            try
            {
                string query = "Exec GV_LayThongTin '" + username + "'";
                DataTable dt = db.sql_select(query);
                TT_tb_magv.Text = dt.Rows[0]["MaGV"].ToString();
                TT_tb_hoten.Text = dt.Rows[0]["HoTen"].ToString();
                TT_cb_gioitinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                TT_tb_email.Text = dt.Rows[0]["Email"].ToString();
                TT_tb_ngaysinh.Text = dt.Rows[0]["NgaySinh"].ToString();
                TT_tb_sodienthoai.Text = dt.Rows[0]["SDT"].ToString();
                TT_tb_diachi.Text = dt.Rows[0]["DiaChi"].ToString();
                TT_tb_mon.Text = dt.Rows[0]["MonDay"].ToString();
                magv = dt.Rows[0]["MaGV"].ToString();
                mon= dt.Rows[0]["MonDay"].ToString();
                Tt_listbox_lopday_get();
            }
            catch
            {
                Tt_lb_thongtincanhan_errorout.Content = "Không tìm được thông tin!!!";
                Tt_lb_thongtincanhan_errorout.Foreground=Brushes.IndianRed;
            }



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
                    string query = "Exec GV_CapNhatThongTinCaNhan '" + magv + "',N'" + TT_tb_hoten.Text + "','" + TT_tb_ngaysinh.Text + "','" + TT_cb_gioitinh.Text + "'" +
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
                        Tt_lb_doimatkhau_errorout.Foreground=Brushes.IndianRed;
                    }
                    else if (loi == "-2")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Mật khẩu mới bị trống!!!";
                        Tt_lb_doimatkhau_errorout.Foreground=Brushes.IndianRed;
                    }
                    else if (loi == "-3")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Mật khẩu nhập lại không đúng!!!";
                        Tt_lb_doimatkhau_errorout.Foreground=Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Tt_lb_doimatkhau_errorout.Content = "Lỗi giao tác!!!";
                        Tt_lb_doimatkhau_errorout.Foreground=Brushes.IndianRed;
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





        /*=========================================LỚP HỌC=========================================
* ==================================================================================
* ==================================================================================
* ==================================================================================*/
        private void SaveDataGridToCsv(DataGrid dataGrid)
        {
            // Tạo một StringBuilder để chứa dữ liệu CSV
            StringBuilder csvContent = new StringBuilder();

            // Lấy danh sách các cột trong DataGrid
            List<string> columns = new List<string>();
            DataTable dataTable = ((DataView)dataGrid.ItemsSource).Table;
            foreach (DataColumn column in dataTable.Columns)
            {
                columns.Add(column.ColumnName);
            }

            // Thêm tiêu đề cột vào chuỗi CSV
            csvContent.AppendLine(string.Join(",", columns));

            // Lấy dữ liệu từng dòng trong DataGrid và thêm vào chuỗi CSV
            foreach (DataRowView row in dataGrid.Items)
            {
                List<string> cells = new List<string>();
                foreach (string column in columns)
                {
                    // Lấy giá trị từng ô trong dòng
                    string cellValue = row[column].ToString();
                    cells.Add(cellValue);
                }

                // Thêm dòng vào chuỗi CSV
                csvContent.AppendLine(string.Join(",", cells));
            }

            // Tạo một hộp thoại SaveFileDialog để lấy đường dẫn tệp CSV
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Mở một StreamWriter để ghi chuỗi CSV vào tệp
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    writer.Write(csvContent.ToString());
                }
            }
        }

        private void Lh_cb_nam_get()
        {
            try
            {
                var ds = new List<string>();
                DataTable dt1 = db.sql_select("select distinct Nam from GiaoVien_LopHoc where MaGV='" + magv + "'");
                DataRow r;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    r = dt1.Rows[i];
                    ds.Add(r[0].ToString());
                }
                Lh_cb_nam.DataContext = ds;
            }
            catch
            {

            }


        }
        private void Lh_cb_lop_get(string nam)
        {
            try
            {
                var ds = new List<string>();
                string query = "select distinct TenLop from GiaoVien_LopHoc where Nam='" + nam + "' and MaGV='" + magv + "'";
                DataTable dt1 = db.sql_select(query);
                DataRow r;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    r = dt1.Rows[i];
                    ds.Add(r[0].ToString());
                }
                Lh_cb_lop.DataContext = ds;

            }
            catch { }

        }
        private void Lh_cb_nam_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string nam = Lh_cb_nam.SelectedItem.ToString();
                Lh_cb_lop_get(nam);
            }
            catch
            {

            }
        }
        private void Lh_load_datagrid()
        {
            try
            {
                string query = "Exec GV_danhsachhslop '" + Lh_cb_nam.SelectedItem.ToString() + "','" + Lh_cb_lop.SelectedItem.ToString() + "'";
                Lh_datagird_dshocsinh.ItemsSource = db.sql_select(query).DefaultView;

            }
            catch { }
        }
        private void Lh_cb_lop_changed(object sender, SelectionChangedEventArgs e)
        {
            Lh_load_datagrid();
        }
        private void Lh_datagrid_loaded(object sender, RoutedEventArgs e)
        {

        }


        private void Lh_bt_in_click(object sender, RoutedEventArgs e)
        {
            if (Lh_cb_nam.Text == "")
            {
                Lh_lb_errorout.Content = "Vui lòng chọn năm học!!!";
                Lh_lb_errorout.Foreground = Brushes.IndianRed;
            }
            else if (Lh_cb_lop.Text == "")
            {

                Lh_lb_errorout.Content = "Vui lòng chọn lớp học!!!";
                Lh_lb_errorout.Foreground = Brushes.IndianRed;
            }
            else
            {
                SaveDataGridToCsv(Lh_datagird_dshocsinh);
                Lh_lb_errorout.Content = "Lưu thành công";
                Lh_lb_errorout.Foreground = Brushes.MediumSeaGreen;
            }
        }
        private void Lh_timkiem()
        {
            try
            {
                string Nam = "", Lop = "";
                try
                {
                    Nam = Lh_cb_nam.SelectedItem.ToString();
                    Lop = Lh_cb_lop.SelectedItem.ToString();
                }
                catch { }
                Lh_datagird_dshocsinh.ItemsSource = db.sql_select("qtv_tracuudanhsachlop N'" + Lh_tb_search.Text + "','" + Nam + "','" + Lop + "'").DefaultView;
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

        private void Lh_Load(object sender, RoutedEventArgs e)
        {
            Lh_cb_nam_get();
            TT_button_background();
        }
        /*=======================================BẢNG ĐIỂM====================================
       * ========================================================================
       * ========================================================================
       * ========================================================================*/


        private void Bd_loaded(object sender, RoutedEventArgs e)
        {
            Bd_cb_namhoc_get();
            TT_button_background();
            Bd_ld_tenbang.Content = mon;
        }

        private void Bd_cb_namhoc_get()
        {
            try
            {
                var ds = new List<string>();
                DataTable dt1 = db.sql_select("select distinct Nam from GiaoVien_LopHoc where MaGV='" + magv + "'");
                DataRow r;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    r = dt1.Rows[i];
                    ds.Add(r[0].ToString());
                }
                Bd_cb_namhoc.DataContext = ds;
            }
            catch
            {
            }
        }
        private void Bd_cb_lop_get(string nam)
        {
            try
            {
                var ds = new List<string>();
                string query = "select distinct TenLop from GiaoVien_LopHoc where Nam='" + nam + "' and MaGV='" + magv + "'";
                DataTable dt1 = db.sql_select(query);
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
                Bd_cb_lop_get(nam);
            }
            catch
            {
            }
        }
        private void Bd_datagrid_get(string ki)
        {
            string query = "Exec GV_LayBangDiem  '" + Bd_cb_namhoc.Text + "','" + Bd_cb_lop.Text + "',N'" + ki + "',N'" + mon + "'";
            DataTable dt = db.sql_select(query);
            if (dt.Rows.Count == 0)
            {
                Bd_datagird.ItemsSource = dt.DefaultView;
                Bd_lb_errorout.Content = "Bảng rỗng";
                Bd_lb_errorout.Foreground = Brushes.IndianRed;
            }
            else
            {
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {

                    Bd_lb_errorout.Content = "Vui lòng chọn năm học!!!";
                    Bd_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-2")
                {
                    Bd_lb_errorout.Content = "Vui lòng chọn lớp học!!!";
                    Bd_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Bd_lb_errorout.Content = "Lỗi giao tác!!!";
                    Bd_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else
                {
                    Bd_datagird.ItemsSource = dt.DefaultView;
                    Bd_lb_errorout.Content = "Tìm bảng điểm thành công.";
                    Bd_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                }
            }
        }

        private void Bd_cb_kihoc_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)e.AddedItems[0];
                string kihoc = comboBoxItem.Content.ToString();
                Bd_datagrid_get(kihoc);
            }
            catch
            {
            }
        }





        private void Bd_bt_capnhatdiem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Bd_datagird.SelectedItem;
                if (rowview != null)
                {
                    string query = "Exec GV_CapNhatDiem  '" + Bd_cb_namhoc.Text + "',N'" + Bd_tb_kihoc.Text + "',N'" + mon + "'," +
                      "'" + Bd_tb_mahs.Text + "','" + Bd_tb_diem15p.Text + "','" + Bd_tb_diem1tiet.Text + "','" + Bd_tb_diemcuoiki.Text + "'";
                    DataTable dt = db.sql_select(query);
                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {

                        Bd_lb_errorout.Content = "Điểm bạn nhập không hợp lệ!!!";
                        Bd_lb_errorout.Foreground = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Bd_lb_errorout.Content = "Lỗi giao tác!!!";
                        Bd_lb_errorout.Foreground = Brushes.IndianRed;
                    }
                    else
                    {
                        Bd_datagrid_get(Bd_cb_kihoc.Text);
                        Bd_lb_errorout.Content = "Cập nhật điểm thành công.";
                        Bd_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                    }
                }
                else
                {
                    Bd_lb_errorout.Content = "Bạn chưa chọn học sinh!!!";
                    Bd_lb_errorout.Foreground = Brushes.IndianRed;
                }

            }
            catch
            {
            }


        }

        private void Bd_datagird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Bd_datagird.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Bd_datagird.SelectedItem;
                    if (rowview != null)
                    {

                        Bd_tb_kihoc.Text = rowview["KiHoc"].ToString();
                        Bd_tb_mahs.Text = rowview["MaHS"].ToString();
                        Bd_tb_hoten.Text = rowview["HoTen"].ToString();
                        Bd_tb_diem15p.Text = rowview["Diem15"].ToString();
                        Bd_tb_diem1tiet.Text = rowview["Diem1Tiet"].ToString();
                        Bd_tb_diemcuoiki.Text = rowview["DiemCuoiKi"].ToString();

                    }
                }
            }
            catch
            {

            }
        }
        private void Bd_in_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Bd_cb_namhoc.Text == "")
                {
                    Bd_lb_errorout.Content = "Vui lòng chọn năm học!!!";
                    Bd_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else if (Bd_cb_lop.Text == "")
                {

                    Bd_lb_errorout.Content = "Vui lòng chọn lớp học!!!";
                    Bd_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else if (Bd_cb_kihoc.Text == "")
                {
                    Bd_lb_errorout.Content = "Vui lòng chọn kì học!!!";
                    Bd_lb_errorout.Foreground = Brushes.IndianRed;
                }
                else
                {
                    SaveDataGridToCsv(Bd_datagird);
                    Bd_lb_errorout.Content = "Lưu thành công";
                    Bd_lb_errorout.Foreground = Brushes.MediumSeaGreen;
                }
            }
            catch
            {

            }

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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

        private void Bc_Loaded(object sender, RoutedEventArgs e)
        {

            TT_button_background();
        }
    }
}
