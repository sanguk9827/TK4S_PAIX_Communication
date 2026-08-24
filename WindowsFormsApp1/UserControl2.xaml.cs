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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;





namespace WindowsFormsApp1
{
    /// <summary>
    /// UserControl2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public int angle = 0;
        
        DispatcherTimer timer;
        public UserControl2()
        {
            InitializeComponent();
            string imagePath = System.IO.Path.Combine(
             AppDomain.CurrentDomain.BaseDirectory,
             "Image",
             "RobotArm.png"

         );
            string imagePath2 = System.IO.Path.Combine(
             AppDomain.CurrentDomain.BaseDirectory,
             "Image",
             "BackGround.png"

         );
            if (File.Exists(imagePath))
            {
                Image1.Source = new BitmapImage(
                    new Uri(imagePath, UriKind.Absolute)
                );
            }
            if (File.Exists(imagePath2))
            {
                Image2.Source = new BitmapImage(
                    new Uri(imagePath2, UriKind.Absolute)
                );
            }
            angle = 42;
           // Image1.Source = bitmap;
            timer = new DispatcherTimer();    //객체생성
            timer.Interval = TimeSpan.FromMilliseconds(10);    //시간간격 설정
            timer.Tick += new EventHandler(timer_Tick);          //이벤트 추가
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            var rotate = new RotateTransform(angle);
            Image1.RenderTransform = rotate;
           // Image2.RenderTransform = rotate;
        }
    }
}
