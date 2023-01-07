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
    /// Interaction logic for Admin_themTaiKhoan.xaml
    /// </summary>
    public partial class Admin_themTaiKhoan : Window
    {
        DBConnect db = new DBConnect();
        InputValidation validation = new InputValidation();
        string Role, LoaiTruyVan;
        int Ma;
        public Admin_themTaiKhoan(string role_, int Ma_, string LoaiTruyVan_)
        {                           
            InitializeComponent();
            Role = role_;
            Ma = Ma_;
            LoaiTruyVan = LoaiTruyVan_;
            label_title.Content = LoaiTruyVan_+" "+role_;
            Tn_gv_bt_them.Content = LoaiTruyVan_;
            if (LoaiTruyVan_ == "Sửa")
            {
                Tn_gv_tb_username.IsReadOnly=true;
                Tn_gv_tb_username.Background = Brushes.LightGray;
            }
            if(LoaiTruyVan_ == "Thêm")
            {
                Datagrid_column_MonDangDay.Visibility=Visibility.Hidden;
            }
                
            loadTextBox(role_, Ma_);
        }

        List<string> LayLopHocDaChon(string MaGiaoVien)
        {
            var SelectedList = new List<string>();
            int count = 0;
            try
            {
                /// Lấy list index dòng đã chọn
                for (int i = 0; i < datagrid_lopday.Items.Count; i++)
                {
                    var item = datagrid_lopday.Items[i];
                    //var mycheckbox_ = Lh_datagird_dshocsinh.Columns[0].S
                    var mycheckbox = datagrid_lopday.Columns[3].GetCellContent(item) as CheckBox;
                    if ((bool)mycheckbox.IsChecked)
                    {
                        TextBlock x = datagrid_lopday.Columns[0].GetCellContent(datagrid_lopday.Items[i]) as TextBlock;
                        SelectedList.Add(i.ToString());
                        if (MaGiaoVien != "")
                        {
                            TextBlock cl0 = datagrid_lopday.Columns[0].GetCellContent(datagrid_lopday.Items[i]) as TextBlock;
                            TextBlock cl1 = datagrid_lopday.Columns[1].GetCellContent(datagrid_lopday.Items[i]) as TextBlock;
                            int check = (int)db.sql_select("qtv_ThemGiaoVienLopHoc '"+MaGiaoVien
                                +"','"+cl0.Text
                                +"','"+cl1.Text+"'").Rows[0][0];
                            if (check == 0)
                                count++;
                        }

                    }
                }
            }
            catch { }
            return SelectedList;
        }
        //private void laydanhsachmonhoc()
        //{
        //    var ds = new List<string>();
        //    DataTable dt = db.sql_select("select distinct TenMon from MonHoc");
        //    //foreach(DataRow r in dt.Rows)
        //    DataRow r;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        r = dt.Rows[i];
        //        ds.Add(r[0].ToString());
        //    }
        //    Tn_gv_tb_daymon.DataContext = ds;
        //}
        private void load_datagrid(string MaGV)
        {
            try
            {
                DataTable dt = db.sql_select("qtv_dslopgvdangday "+MaGV);
                datagrid_lopday.ItemsSource = dt.DefaultView;
            }
            catch { }
        }
        private void loadTextBox(string role, int Ma)
        {
            try
            {
                if (role != "giáo viên")
                {
                    //Không là giáo viên                      
                    datagrid_lopday.Visibility = Visibility.Hidden;
                    lb_daylop.Visibility = Visibility.Hidden;
                    lb_daymon.Visibility = Visibility.Hidden;
                    Tn_gv_tb_daymon.Visibility= Visibility.Hidden;
                }
                else 
                {
                    //laydanhsachmonhoc(); 
                    load_datagrid(Ma.ToString());
                }

                if (Ma==0)
                    return;
                
                string query = "select * from "; // +role+" where '"+Ma+"' = ";
                switch (role)
                {
                    case "học sinh":
                        role = "hocsinh";
                        query = query + role+" where '"+Ma.ToString()+"'="+ "MaHS";
                        break;
                    case "quản trị":
                        role = "quantri";
                        query = query + role+" where '"+Ma.ToString()+"'="+ "MaQT";
                        break;
                    case "giáo viên":
                        role = "giaovien";
                        query = query + role+" where '"+Ma.ToString()+"'="+ "MaGV";
                        break;
                    default:
                        role = "hocsinh";
                        query = query + role+" where '"+Ma.ToString()+"'="+ "MaHS";
                        break;
                }
                DataTable dt = db.sql_select(query);
                DataRow r = dt.Rows[0];
                Tn_gv_tb_username.Text = r["username"].ToString();
                Tn_gv_tb_hoten.Text = r["HoTen"].ToString();
                Tn_gv_tb_ngaysinh.Text = r["NgaySinh"].ToString();
                Tn_gv_cb_gioitinh.Text = r["GioiTinh"].ToString();
                Tn_gv_tb_email.Text = r["Email"].ToString();
                Tn_gv_tb_sodienthoai.Text ="0"+ r["SDT"].ToString();
                Tn_gv_tb_diachi.Text = r["DiaChi"].ToString();
                Tn_gv_tb_daymon.Text = r["MonDay"].ToString();
            }
            catch { }
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

        private void Tn_gv_them_click(object sender, RoutedEventArgs e)
        {

            try
            {
                string truyvan="";
                if (LoaiTruyVan == "Thêm")
                {
                    truyvan="them";
                }
                else if (LoaiTruyVan == "Sửa")
                {
                    truyvan="sua";
                }


                string querry;
                string MaRole = "";

                switch (Role)
                {
                    case "quản trị":
                        MaRole = "1";
                        break;
                    case "giáo viên":
                        MaRole = "2";
                        break;
                    case "học sinh":
                        MaRole = "3";
                        break;
                }

                //Check điều kiện trước khi thêm      
                if (Tn_gv_tb_username.Text== "" ||  Tn_gv_tb_hoten.Text == "" ||
                   Convert.ToDateTime(Tn_gv_tb_ngaysinh.Text).ToString("MM/dd/yyyy") == "" || Tn_gv_cb_gioitinh.Text== "" ||
                   Tn_gv_tb_email.Text == "" || Tn_gv_tb_sodienthoai.Text == "" ||
                   Tn_gv_tb_diachi.Text == "")
                {
                    Lh_lb_errorout.Content = "Có trường rỗng";
                    Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    return;
                }
                if (Tn_gv_pb_password.Password == "" && truyvan != "sua")
                {
                    Lh_lb_errorout.Content = "Có trường rỗng";
                    Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    return;
                }
                if (Tn_gv_pb_password.Password != "" && truyvan == "sua")
                {
                    if (!validation.ValidPassword(Tn_gv_pb_password.Password))
                    {
                        Lh_lb_errorout.Content = "Mật khẩu không hợp lệ.\nMật khẩu phải có từ 5 đến 50 ký tự, không chứa khoảng trắng";
                        Lh_lb_errorout.Foreground=Brushes.IndianRed;
                        return;
                    }
                }
                if (!validation.ValidUsername(Tn_gv_tb_username.Text))
                {
                    Lh_lb_errorout.Content = "Tên đăng nhập không hợp lệ.\nTên đăng nhập phải có từ 3 đến 20 ký tự, không chứa ký tự đặc biệt";
                    Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    return;
                }
                if (!validation.ValidPassword(Tn_gv_pb_password.Password) && truyvan != "sua")
                {
                    Lh_lb_errorout.Content = "Mật khẩu không hợp lệ.\nMật khẩu phải có từ 5 đến 50 ký tự, không chứa khoảng trắng";
                    Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    return;
                }
                int birthday = Int32.Parse(Convert.ToDateTime(Tn_gv_tb_ngaysinh.Text).ToString("yyyy"));
                int thisyear = Int32.Parse(DateTime.Now.ToString("yyyy"));
                if ((thisyear - birthday < 15 || 20 < thisyear - birthday) && Role == "học sinh")
                {
                    Lh_lb_errorout.Content = "Độ tuổi phải từ 15 đến 20";
                    Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    return;
                }
                if (!validation.ValidEmail(Tn_gv_tb_email.Text))
                {
                    Lh_lb_errorout.Content = "Email không hợp lệ";
                    Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    return;
                }
                if (!validation.ValidPhoneNumber(Tn_gv_tb_sodienthoai.Text))
                {
                    Lh_lb_errorout.Content = "Số điện thoại không hợp lệ";
                    Lh_lb_errorout.Foreground=Brushes.IndianRed;
                    return;
                }



                querry = "qtv_"+truyvan+"taikhoan '"+ MaRole  +"','"  +Tn_gv_tb_username.Text +"','"+
                    Tn_gv_pb_password.Password  +"',N'"+ Tn_gv_tb_hoten.Text +"','"+
                    Convert.ToDateTime(Tn_gv_tb_ngaysinh.Text).ToString("MM/dd/yyyy") +"',N'"+Tn_gv_cb_gioitinh.Text         +"','"+
                    Tn_gv_tb_email.Text +"','"+Tn_gv_tb_sodienthoai.Text +"',N'"+
                    Tn_gv_tb_diachi.Text +"',N'"+Tn_gv_tb_daymon.Text +"'";
                int maloi = (int)db.sql_select(querry).Rows[0][0];
                if (maloi >= 0)
                {
                    Lh_lb_errorout.Foreground=Brushes.MediumSeaGreen;
                    Lh_lb_errorout.Content = LoaiTruyVan+ " "+Role+" thành công";
                    if (Role == "giáo viên")
                    {
                        if (truyvan=="sua")
                            db.sql_select("delete GiaoVien_LopHoc where MaGV = '"+maloi.ToString()+"'");
                        LayLopHocDaChon(maloi.ToString());
                    }
                }
                else
                {
                    switch (maloi)
                    {
                        case -1:
                            Lh_lb_errorout.Content = "Có trường trống";
                            break;
                        case -2:
                            Lh_lb_errorout.Content = "username đã tồn tại";
                            break;
                        case -10:
                            Lh_lb_errorout.Content = "Thông tin nhập lỗi";
                            break;
                    }
                    Lh_lb_errorout.Foreground=Brushes.IndianRed;
                }
            }
            catch
            {
                Lh_lb_errorout.Content = "Thông tin nhập lỗi";
                Lh_lb_errorout.Foreground=Brushes.IndianRed;
            }
        
                
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        private void Grid_MouseDown_errorremove(object sender, MouseButtonEventArgs e)
        {
            Lh_lb_errorout.Content="";
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
