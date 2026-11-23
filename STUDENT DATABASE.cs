using System;
using System.Drawing;
using System.Windows.Forms;
using ScottPlot;

namespace BUCS_SIS
{
    public partial class STUDENT_DATABASE : Form
    {
        public STUDENT_DATABASE()
        {
            InitializeComponent();
            SetupTransparentChart();
        }

        private void SetupTransparentChart()
        {
            // 1. Sample line graph data
            double[] dataX = { 1, 2, 3, 4, 5 };
            double[] dataY = { 120, 150, 140, 180, 210 };

            var line = formsPlot1.Plot.Add.Scatter(dataX, dataY);
            line.Color = Colors.Yellow;
            line.LineWidth = 3f;
            line.MarkerSize = 10;
            line.LegendText = "Enrolled Students";

            // 2. Make the background completely transparent
            formsPlot1.BackColor = System.Drawing.Color.Transparent;
            formsPlot1.Plot.FigureBackground.Color = Colors.Transparent;
            formsPlot1.Plot.DataBackground.Color = Colors.Transparent;

            // 3. Set base axis colors FIRST (before setting Title and Labels!)
            formsPlot1.Plot.Axes.Color(Colors.White);
            formsPlot1.Plot.Grid.MajorLineColor = Colors.White.WithAlpha(0.25);

            // 4. Now set Title and Axis Labels explicitly in WHITE
            formsPlot1.Plot.Axes.Title.IsVisible = true;
            formsPlot1.Plot.Axes.Title.Label.Text = "BUCS Student Enrollment Trend";
            formsPlot1.Plot.Axes.Title.Label.ForeColor = Colors.White;
            formsPlot1.Plot.Axes.Title.Label.FontSize = 18;
            formsPlot1.Plot.Axes.Title.Label.Bold = true;

            formsPlot1.Plot.Axes.Bottom.Label.IsVisible = true;
            formsPlot1.Plot.Axes.Bottom.Label.Text = "Academic Year / Semester";
            formsPlot1.Plot.Axes.Bottom.Label.ForeColor = Colors.White;
            formsPlot1.Plot.Axes.Bottom.Label.FontSize = 14;

            formsPlot1.Plot.Axes.Left.Label.IsVisible = true;
            formsPlot1.Plot.Axes.Left.Label.Text = "Number of Students";
            formsPlot1.Plot.Axes.Left.Label.ForeColor = Colors.White;
            formsPlot1.Plot.Axes.Left.Label.FontSize = 14;

            // 5. Add extra top margin so point labels don't get cut off
            formsPlot1.Plot.Axes.Margins(bottom: 0.15, top: 0.25);

            // 6. Add white text labels above each data point
            for (int i = 0; i < dataX.Length; i++)
            {
                var txt = formsPlot1.Plot.Add.Text(dataY[i].ToString(), dataX[i], dataY[i]);
                txt.LabelFontColor = Colors.White;
                txt.LabelFontSize = 13;
                txt.LabelBold = true;
                txt.OffsetY = -15;
            }

            // 7. Show transparent legend with white text
            formsPlot1.Plot.ShowLegend(Alignment.UpperLeft);
            formsPlot1.Plot.Legend.FontColor = Colors.White;
            formsPlot1.Plot.Legend.BackgroundColor = Colors.Transparent;
            formsPlot1.Plot.Legend.OutlineColor = Colors.White.WithAlpha(0.5);

            // 8. Refresh to render everything
            formsPlot1.Refresh();
        }
    }
}
