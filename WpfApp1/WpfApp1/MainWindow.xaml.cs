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

        //Координаты мыши по осям x и y
        int xmouse, ymouse;
        //Скорость спутника по осям x и y
        double xspeed, yspeed;
        //Ускорение
        double acc;
        //Скорость
        double speed = 0;
        //Расстояние
        double distance = 0;

        public MainWindow()
        {
            InitializeComponent();

            //PLANET
            Planet.Width = 150;
            Planet.Height = 150;
            Planet.Stroke = System.Windows.Media.Brushes.AliceBlue;
            Canvas.SetLeft(Planet, 425);
            Canvas.SetTop(Planet, 325);
            AnimationCanvas.Children.Add(Planet);

            //SPUTNIK
            Sputnik.Width = 30;
            Sputnik.Height = 30;
            Sputnik.Fill = System.Windows.Media.Brushes.LightGray;
            Sputnik.Stroke = System.Windows.Media.Brushes.DarkSlateGray;
            Canvas.SetLeft(Sputnik, 485);
            Canvas.SetTop(Sputnik, 200);
            AnimationCanvas.Children.Add(Sputnik);

            
        }

        private void STOP(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(Sputnik, 485);
            Canvas.SetTop(Sputnik, 200);
            speed = 0;
            distance = 0;
            
        }

        private void START(object sender, RoutedEventArgs e)
        {
            if (TryParse())
            {
                //Передача введенных переменных
                acc = Convert.ToDouble(Acceleration.Text);
                var timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 0, 0, 4);
                timer.Tick += Timer_Tick;
                timer.Start();  
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Speed.Text = Convert.ToString(speed);
            Distance.Text = Convert.ToString(distance);
            Canvas.SetLeft(Sputnik, Canvas.GetLeft(Sputnik) - 1);
            Canvas.SetTop(Sputnik, Canvas.GetTop(Sputnik) + 1);
        }

        private bool TryParse()
        {
            bool result = false;
            bool x1b, x2b;
            double x1, x2;
            x1b = double.TryParse(Acceleration.Text, out x1);
            x2b = double.TryParse(K.Text, out x2);
            if (x1b == true && x2b == true) { result = true; }
            return result;
        }
    }
}
