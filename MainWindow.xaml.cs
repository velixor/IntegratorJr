using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using IntegratorJr.Models;
using IntegratorJr.Services;
using org.mariuszgromada.math.mxparser;
using OxyPlot;
using Expression = org.mariuszgromada.math.mxparser.Expression;
using Function = IntegratorJr.Models.Function;

namespace IntegratorJr
{
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
        }

        private async void CalculateIntegral_BtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadingLabel.Opacity = 100; 
                await DrawPlotAndCalculateIntegrals();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadingLabel.Opacity = 0;
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
            NormalizeLimits(functionData);
            NormalizeStep(functionData);
            ReflectChangesInForm(functionData);
        }


        private void NormalizeStep(FunctionData fd)
        {
            var range = fd.Right - fd.Left;
            var stepCount = (int) Math.Ceiling(range / fd.Step);
            if (stepCount % 2 != 0) stepCount++;
            fd.Step = range / stepCount;
        }

        private static void NormalizeLimits(FunctionData functionData)
        {
            if (!(functionData.Left > functionData.Right)) return;

            var t = functionData.Left;
            functionData.Left = functionData.Right;
            functionData.Right = t;
        }

        private void ReflectChangesInForm(FunctionData functionData)
        {
            tb_Left.Text = functionData.Left.ToString();
            tb_Right.Text = functionData.Right.ToString();
            tb_Step.Text = functionData.Step.ToString();
        }

        private async Task CalculateIntegralValues(FunctionData function)
        {
            var values = await _integralSolver.BuildIntegralSolutionsAsync(function);
            var roundedValues = values.ToArray();
            
            foreach (var integralSolution in roundedValues)
            {
                integralSolution.Value = Math.Round(integralSolution.Value, 2);
            }

            IntegralSolutions_lv.ItemsSource = roundedValues;
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