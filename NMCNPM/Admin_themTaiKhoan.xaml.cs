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
        public Admin_themTaiKhoan(string role, int Ma, string LoaiTruyVan)
        {
            InitializeComponent();
            label_title.Content = LoaiTruyVan+" "+role;
            Tn_gv_bt_them.Content = LoaiTruyVan;
            if (LoaiTruyVan == "Sửa")
            {
                Tn_gv_tb_username.IsReadOnly=true;
                Tn_gv_tb_username.Background = Brushes.LightGray;
            }
            loadTextBox(role, Ma);
        }
        private void laydanhsachmonhoc()
        {
            var ds = new List<string>();
            DataTable dt = db.sql_select("select distinct TenMon from MonHoc");
            //foreach(DataRow r in dt.Rows)
            DataRow r;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = dt.Rows[i];
                ds.Add(r[0].ToString());
            }
            Tn_gv_tb_daymon.DataContext = ds;
        }
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
                    laydanhsachmonhoc(); 
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
                Tn_gv_tb_sodienthoai.Text = r["SDT"].ToString();
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

        }
    }
}
