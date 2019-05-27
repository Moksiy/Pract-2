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

       

 
    public struct Planet
    {
        //Координаты
        
        public double x { get; set; }
        public double y { get; set; }

        //Скорость
        public double vx { get; set; }
        public double vy { get; set; }

        //Масса
        public double m { get; set; }
    }

    public partial class MainWindow : Window
    {
        Ellipse Earth1 = new Ellipse();
        Ellipse Sputnik = new Ellipse();
        Line Trajectory = new Line();
        Line Vector = new Line();
        Planet Earth = new Planet();
        Planet Satellite = new Planet();
        bool isCollision = false;
        bool Stop = false;
        //Координаты мыши по осям x и y для вектора
        double xmouse, ymouse;
        //Ускорение и коэффициент
        double acc, k, a;
        //Расстояние
        double distance = 0;
        const double G = 6.67e-11;
        double xvector, yvector;
        

        public MainWindow()
        {
            InitializeComponent();

            //PLANET
            Earth1.Width = 150;
            Earth1.Height = 150;
            Earth1.Stroke = System.Windows.Media.Brushes.AliceBlue;
            Canvas.SetLeft(Earth1, 425);
            Canvas.SetTop(Earth1, 325);
            AnimationCanvas.Children.Add(Earth1);
            
            //SPUTNIK
            Sputnik.Width = 30;
            Sputnik.Height = 30;
            Sputnik.Fill = System.Windows.Media.Brushes.LightGray;
            Sputnik.Stroke = System.Windows.Media.Brushes.DarkSlateGray;
            Canvas.SetLeft(Sputnik, 485);
            Canvas.SetTop(Sputnik, 200);
            AnimationCanvas.Children.Add(Sputnik);
            //Параметры для Земли
            Earth.x = 425;
            Earth.y = 325;
            Earth.vx = 0;
            Earth.vy = 0;
            Earth.m = 20000;

            //Параметры для спутника
            Satellite.x = 485;
            Satellite.y = 200;
            Satellite.vx = 0;
            Satellite.vy = 0;
            Satellite.m = 1;
            acc = 0;
        }

        /// <summary>
        /// Обработчик нажатия по кнопке СТОП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void STOP(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(Sputnik, 485);
            Canvas.SetTop(Sputnik, 200);
            distance = 0;
            Stop = true;
            xmouse = 0; ymouse = 0;
            Vector.X1 = 0; Vector.X2 = 0; Vector.Y1 = 0; Vector.Y2 = 0;
            Distance.Text = "0";
        }

        /// <summary>
        /// Обработчик нажатия по кнопке СТАРТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void START(object sender, RoutedEventArgs e)
        {
            if (TryParse())
            {
                if (xmouse != 0 && ymouse != 0)
                {
                    Satellite.vx = ((xmouse) - 485)/100;
                    Satellite.vy = ((ymouse) - 200)/100;
                    acc = Convert.ToDouble(Acceleration.Text);
                    Stop = false;
                    AnimationCanvas.Children.Remove(Vector);
                    //Передача введенных переменных
                    acc = Convert.ToDouble(Acceleration.Text);
                    k = Convert.ToDouble(K.Text);
                    var timer = new DispatcherTimer();
                    timer.Stop();
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }else { MessageBox.Show("Укажите вектор движения спутника"); }            
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
            }

        }

        /// <summary>
        /// Таймер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(Radius() <= 90)
            {
                isCollision = true;
            }
            if(!isCollision && !Stop)
            {
                Distance.Text = Convert.ToString((int)distance);
                DistanceCounter();                
                a = k/ (Math.Pow(Radius(),2))/Satellite.m;
                xvector = 500 - (Canvas.GetLeft(Sputnik)+15);
                yvector = 400 - (Canvas.GetTop(Sputnik)+15);
                xvector /= Radius();
                yvector /= Radius();
                Satellite.vx +=  a * xvector;
                Satellite.vy +=  a * yvector;
                Satellite.x += Satellite.vx;
                Satellite.y += Satellite.vy;
                Canvas.SetLeft(Sputnik, Satellite.x);
                Canvas.SetTop(Sputnik, Satellite.y);
            }
        }

        /// <summary>
        /// Проверка корректности ввода
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Обработчик нажатия на кнопку мыши для задания вектора движения спутника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// /Добавление ускорения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Satellite.vx += acc; 
            Satellite.vy += acc;
        }

        /// <summary>
        /// Метод обновления расстояния от спутника до планеты
        /// </summary>
        private void DistanceCounter()
        {
            double X1, X2, Y1, Y2;
            X1 = 500; Y1 = 400;
            X2 = Canvas.GetLeft(Sputnik);
            Y2 = Canvas.GetTop(Sputnik);
            distance = Math.Sqrt(Math.Pow((X2-X1),2)+Math.Pow((Y2-Y1),2));
            Distance.Text = Convert.ToString(Convert.ToInt16(distance));
        }

        /// <summary>
        /// Метод расчета расстояния между планетами
        /// </summary>
        /// <returns></returns>
        private double Radius()
        {
            double x1, y1, x2, y2;
            x1 = Canvas.GetLeft(Sputnik) + 15;
            y1 = Canvas.GetTop(Sputnik) + 15;
            x2 = 500;
            y2 = 400;
            return Math.Sqrt(Math.Pow((x2-x1),2)+Math.Pow((y2-y1),2));
        }
    }


}
