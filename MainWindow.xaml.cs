using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using IntegratorJr.Exceptions;
using IntegratorJr.Models;
using IntegratorJr.Services;
using org.mariuszgromada.math.mxparser;
using Expression = org.mariuszgromada.math.mxparser.Expression;
using Function = IntegratorJr.Models.Function;

namespace IntegratorJr
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IntegralSolver _integralSolver;
        private readonly PlotBuilder _plotBuilder;

        public MainWindow()
        {
            InitializeComponent();

            _plotBuilder = new PlotBuilder();
            _integralSolver = new IntegralSolver();
        }

        private async void CalculateIntegral_BtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                await DrawPlotAndCalculateIntegrals();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private async Task DrawPlotAndCalculateIntegrals()
        {
            var functionData = BuildFunctionData();
            await DrawPlot(functionData);
            await CalculateIntegralValues(functionData);
        }

        private async Task CalculateIntegralValues(FunctionData function)
        {
            IntegralSolutions_lv.ItemsSource = await _integralSolver.BuildIntegralSolutionsAsync(function);
        }

        private async Task DrawPlot(FunctionData functionData)
        {
            PlotViewer.Model = await _plotBuilder.BuildPlotAsync(functionData);
        }

        private FunctionData BuildFunctionData()
        {
            return new FunctionData
            {
                Function = ParseFunction(tb_Function.Text),
                Left = tb_Left.Number(),
                Right = tb_Right.Number(),
                Step = tb_Step.Number()
            };
        }

        private Function ParseFunction(string function)
        {
            ValidateAndThrow(function);

            double Func(double x)
            {
                var arg = new Argument("x", x);
                var e = new Expression(function, arg);

                return e.calculate();
            }

            return new Function(Func);
        }


        private void ValidateAndThrow(string function)
        {
            var arg = new Argument("x", 0);
            var e = new Expression(function, arg);

            if (!e.checkSyntax()) throw new FunctionStringInvalidException(e.getErrorMessage());
        }
    }
}