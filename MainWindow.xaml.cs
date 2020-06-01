using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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

namespace IntegratorJr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawPlot();
        }

        private void DrawPlot()
        {
            var model = new PlotModel();
            model.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom, // означает, будет ли это ось X, или Y 
                AxislineStyle = LineStyle.Solid, // отвечает за тип отрисовки осей (Сплошная линия)
                PositionAtZeroCrossing = true // означает, сможем ли мы увидеть, как оси пересекаются друг с другом
            });
            model.Series.Add(new FunctionSeries(x => Math.Sin(x), -5, 5, 0.01, "sin(x)"));

            PlotViewer.Model = model;
        }
    }
}
