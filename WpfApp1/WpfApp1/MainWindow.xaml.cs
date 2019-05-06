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
сила в направлении первого шара, имеющая величину kr2 , где k вводится пользователем, а r – рассто-
яние от шара до неподвижного шара. Движение прекращается в случае, если шары соприкасаются.
При этом, если пользователь нажимает клавишу пробел, то спутнику сообщается дополнительное
ускорение в направлении его движения (величина ускорения вводится пользователем предваритель-
но).Движение рассчитывается на каждом кванте времени исходя из того, что оно в течении кванта рав-
ноускоренное и прямолинейное.*/



    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
