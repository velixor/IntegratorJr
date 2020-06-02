using System.Windows;

namespace IntegratorJr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PlotBuilder _plotBuilder;
        public MainWindow()
        {
            InitializeComponent();

            _plotBuilder = new PlotBuilder();
        }

        private void CalculateIntegral_BtnClick(object sender, RoutedEventArgs e)
        {
            DrawPlot();
        }

        private void DrawPlot()
        {
            var functionData = BuildFunctionData();

            PlotViewer.Model = _plotBuilder.BuildPlot(functionData);
        }

        private FunctionData BuildFunctionData()
        {
            return new FunctionData
            {
                Function = tb_Function.Text,
                Left = tb_Left.Number(),
                Right = tb_Right.Number(),
                Step = tb_Step.Number()
            };
        }
    }
}
