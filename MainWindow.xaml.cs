using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        private async void CalculateIntegral_BtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                await DrawPlot();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private async Task DrawPlot()
        {
            var functionData = BuildFunctionData();

            PlotViewer.Model = await _plotBuilder.BuildPlotAsync(functionData);
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
