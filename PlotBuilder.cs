using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;

namespace IntegratorJr
{
    class PlotBuilder
    {
        public PlotModel BuildPlot(FunctionData functionData)
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
            var function = ParseFunction();

            return new FunctionSeries(function, functionData.Left, functionData.Right, functionData.Step);
        }

        // TODO make function 
        private Func<double, double> ParseFunction()
        {
            return x => Math.Sin(x);
        }
    }
}
