using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            //myMediaElement.Source = new Uri(Environment.CurrentDirectory+@"\nguoilun.gif");
        }
        private void myMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            myMediaElement.Position = new TimeSpan(0, 0, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("adaksdjlasjdklasdj");
        }
    }
}
