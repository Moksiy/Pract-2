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
    /// Логика (eё нет) взаимодействия для MainWindow.xaml
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
        Line Vector = new Line();

        bool isCollision = false;
        bool Stop = false;
        //Координаты мыши по осям x и y для вектора
        double xmouse, ymouse;
        //Ускорение и коэффициент
        double acc, k;
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
            Stop = true;
            xmouse = 0; ymouse = 0;
            Vector.X1 = 0; Vector.X2 = 0; Vector.Y1 = 0; Vector.Y2 = 0;
            Distance.Text = "0";
            Speed.Text = "0";
        }

        private void START(object sender, RoutedEventArgs e)
        {
            if (TryParse())
            {
                if (xmouse != 0 && ymouse != 0)
                {                    
                    Stop = false;
                    AnimationCanvas.Children.Remove(Vector);
                    //Передача введенных переменных
                    acc = Convert.ToDouble(Acceleration.Text);
                    k = Convert.ToDouble(K.Text);
                    var timer = new DispatcherTimer();
                    timer.Stop();
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 4);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }else { MessageBox.Show("Укажите вектор движения спутника"); }            
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(/**/false)
            {
                isCollision = true;
            }
            if(!isCollision && !Stop)
            {
                Distance.Text = Convert.ToString((int)distance);
                Speed.Text = Convert.ToString(speed);
                Canvas.SetLeft(Sputnik, Canvas.GetLeft(Sputnik)+1);
                DistanceCounter();                
            }
        }

        //Проверка корректности ввода
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

        //Обработчик нажатия на кнопку мыши для задания вектора движения спутника
        private void canvas_Click(object sender, MouseButtonEventArgs e)
        {
            if (Vector != null) { AnimationCanvas.Children.Remove(Vector); }
            Vector.X1 = 0; Vector.X2 = 0;
            Vector.Y1 = 0; Vector.Y2 = 0;
            Point point = e.GetPosition(this);
            xmouse = point.X;
            ymouse = point.Y;
            Vector.X1 = 500; Vector.Y1 = 215;
            Vector.X2 = xmouse; Vector.Y2 = ymouse;
            Vector.Stroke = System.Windows.Media.Brushes.RoyalBlue;
            AnimationCanvas.Children.Add(Vector);
        }

        //Добавление ускорения
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //Метод обновления расстояния от спутника до планеты
        private void DistanceCounter()
        {
            double X1, X2, Y1, Y2;
            X1 = 500; Y1 = 400;
            X2 = Canvas.GetLeft(Sputnik);
            Y2 = Canvas.GetTop(Sputnik);
            distance = Math.Sqrt(Math.Pow((X2-X1),2)+Math.Pow((Y2-Y1),2));
            Distance.Text = Convert.ToString(Convert.ToInt16(distance));
        }

        //Метод обновления скорости 
        private void SpeedCounter()
        {

        }

        //обработчик нажатия на пробел
        private void Space(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                acc += 1;
            }
        }
    }


}
