using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        private readonly FunctionDataValidator _functionDataValidator;
        private readonly IntegralSolver _integralSolver;
        private readonly PlotBuilder _plotBuilder;

        public MainWindow()
        {
            InitializeComponent();

            _plotBuilder = new PlotBuilder();
            _integralSolver = new IntegralSolver();
            _functionDataValidator = new FunctionDataValidator();
            ;
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
            var functionData = GetValidFunctionData();

            await DrawPlot(functionData);
            await CalculateIntegralValues(functionData);
        }

        private FunctionData GetValidFunctionData()
        {
            var functionData = BuildFunctionData();

            NormalizeFunctionData(functionData);
            _functionDataValidator.Validate(functionData);

            return functionData;
        }

        private void NormalizeFunctionData(FunctionData functionData)
        {
            if (!(functionData.Left > functionData.Right)) return;

            var t = functionData.Left;
            functionData.Left = functionData.Right;
            functionData.Right = t;
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
            double Func(double x)
            {
                var arg = new Argument("x", x);
                var e = new Expression(function, arg);

                return e.calculate();
            }

            return new Function(Func, function);
        }
    }
}