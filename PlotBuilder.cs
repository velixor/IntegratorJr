using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Threading.Tasks;

namespace IntegratorJr
{
    class PlotBuilder
    {
        public Task<PlotModel> BuildPlotAsync(FunctionData functionData)
        {
            return Task.Run(() => BuildPlot(functionData));
        }

        private PlotModel BuildPlot(FunctionData functionData)
        {
            var plotModel = BuildBasicPlotModel();

            plotModel.Series.Add(GetFunctionSeries(functionData));

            return plotModel;
        }

        private PlotModel BuildBasicPlotModel()
        {
            var plotModel = new PlotModel();

            plotModel.Axes.Add(GetAxis(AxisPosition.Left));
            plotModel.Axes.Add(GetAxis(AxisPosition.Bottom));

            return plotModel;
        }

        private static LinearAxis GetAxis(AxisPosition position)
        {
            return new LinearAxis()
            {
                Position = position, // означает, будет ли это ось X, или Y 
                AxislineStyle = LineStyle.Solid, // отвечает за тип отрисовки осей (Сплошная линия)
                PositionAtZeroCrossing = true // означает, сможем ли мы увидеть, как оси пересекаются друг с другом
            };
        }

        private Series GetFunctionSeries(FunctionData functionData)
        {
            var function = ParseFunction(functionData.Function);

            return new FunctionSeries(function, functionData.Left, functionData.Right, functionData.Step);
        }

        // TODO make function 
        private Func<double, double> ParseFunction(string function)
        {
            return x => 
            {
                var arg = new Argument("x", x);
                var e = new Expression(function, arg);

                return e.calculate();
            };
        }
    }
}
