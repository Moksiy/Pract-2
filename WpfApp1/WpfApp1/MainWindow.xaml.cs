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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    /*ПП2.1-1-14 Моделирование движения спутника. Нарисуйте следующее движение. Один шар неподвижен,
другой шар имеет начальную скорость (вектор, задается пользователем), при этом на него дейтсвует
сила в направлении первого шара, имеющая величину k/(r^2) , где k вводится пользователем, а r – рассто-
яние от шара до неподвижного шара. Движение прекращается в случае, если шары соприкасаются.
При этом, если пользователь нажимает клавишу пробел, то спутнику сообщается дополнительное
ускорение в направлении его движения (величина ускорения вводится пользователем предваритель-
но).Движение рассчитывается на каждом кванте времени исходя из того, что оно в течении кванта рав-
ноускоренное и прямолинейное.*/



    public partial class MainWindow : Window
    {
        Ellipse Planet = new Ellipse();
        Ellipse Sputnik = new Ellipse();
        Line Trajectory = new Line();

        bool isCollision = false;

        public MainWindow()
        {
            InitializeComponent();
            Planet.Width = 100;
            Planet.Height = 100;
            Planet.Fill = System.Windows.Media.Brushes.Green;
            Planet.Stroke = System.Windows.Media.Brushes.Green;
            Canvas.SetLeft(Planet, 450);
            Canvas.SetTop(Planet, 350);
            AnimationCanvas.Children.Add(Planet);

        }

        //STOP
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //START
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
