using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using ScottPlot;
using System.Drawing;
using org.mariuszgromada.math.mxparser;



namespace Regula_falsi_metodu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        double interval1, interval2;
        List<int> cmbdata = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };
        int decimalPlaces;
        string decimalFormat;
        List<Result> rList = new List<Result>();
        public MainWindow()
        {
            InitializeComponent();
            cmbAccuracy.ItemsSource = cmbdata;
            grid1.Visibility = Visibility.Visible;
            grid1.IsEnabled = true;
            gridStep.Visibility = Visibility.Hidden;
            gridStep.IsEnabled = false;
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            rList.Clear();
            StackPanelIteration.Children.Clear();
            ScrollViewerIteration.IsEnabled = true;
            ScrollViewerIteration.Visibility = Visibility.Visible;
            btnGraph.Content = "Grafiği göster";

            txtFunc.Text = string.Empty;
            txtInterval1.Text = string.Empty;
            txtInterval2.Text = string.Empty;
            cmbAccuracy.SelectedItem = null;

            gridGraph.Visibility = Visibility.Hidden;
            gridGraph.IsEnabled = false;

            gridStep.Visibility = Visibility.Hidden;
            gridStep.IsEnabled = false;
            
            grid1.Visibility = Visibility.Visible;
            grid1.IsEnabled = true;
            
        }

        private void btnGraph_Click(object sender, RoutedEventArgs e)
        {
            if (ScrollViewerIteration.IsEnabled)
            {
                ScrollViewerIteration.IsEnabled = false;
                ScrollViewerIteration.Visibility = Visibility.Hidden;
                btnGraph.Content = "Grafiği gizle";
                drawGraph(interval1, interval2, txtFunc.Text);
                gridGraph.IsEnabled = true;
                gridGraph.Visibility = Visibility.Visible;
               
            }
            else
            {
                ScrollViewerIteration.IsEnabled = true;
                ScrollViewerIteration.Visibility = Visibility.Visible;
                btnGraph.Content = "Grafiği göster";
                gridGraph.IsEnabled = false;
                gridGraph.Visibility = Visibility.Hidden;
                
            }
            
            
        }

        private async void btnHesapla_Click(object sender, RoutedEventArgs e)
        {

            if (double.TryParse(txtInterval1.Text, out interval1) &&
               double.TryParse(txtInterval2.Text, out interval2) &&
               Calculate.controlFuncSyntax(txtFunc.Text))
            {
                if (Calculate.controlIntervals(interval1, interval2, txtFunc.Text))
                { 
                    grid1.Visibility = Visibility.Hidden;
                    grid1.IsEnabled = false;
                    rList.Clear();
                    if ((int)cmbAccuracy.SelectedValue <= 4)
                    {
                        decimalPlaces = 4;
                        decimalFormat = "F" + 4.ToString();
                    }
                    else
                    {
                        decimalPlaces = (int)cmbAccuracy.SelectedValue;
                        decimalFormat = "F" + decimalPlaces.ToString();
                    }
                       rList= Calculate.calcResult(interval1, interval2, txtFunc.Text, (int)cmbAccuracy.SelectedValue, decimalPlaces);
                    gridStep.Visibility = Visibility.Visible;
                    gridStep.IsEnabled = true;
                    for(int i = 0; i < rList.Count; i++)
                    {
                        UserControlStep ucs = new UserControlStep();
                        ucs.txt1.Text = $"{i+1}. c = {(rList[i].a).ToString(decimalFormat)} - f({(rList[i].a).ToString(decimalFormat)})*({(rList[i].b).ToString(decimalFormat)}-{(rList[i].a).ToString(decimalFormat)})/(f({(rList[i].b).ToString(decimalFormat)})-f({(rList[i].a).ToString(decimalFormat)})) ";
                        ucs.txt2.Text = $"c = {(rList[i].a).ToString(decimalFormat)} - {(rList[i].fa).ToString(decimalFormat)}*({(rList[i].b).ToString(decimalFormat)}-{(rList[i].a).ToString(decimalFormat)})/({(rList[i].fb).ToString(decimalFormat)})-{(rList[i].fa).ToString(decimalFormat)}) ";
                        ucs.txt3.Text = rList[i].new_interval;
                        ucs.txt4.Text = $"c{i+1} = {(rList[i].c).ToString(decimalFormat)}";
                        if (i == rList.Count-1)
                        {
                            ucs.txt4.TextDecorations = TextDecorations.Underline;
                        }
                       
                        StackPanelIteration.Children.Add(ucs);
                        for(double a = 0; a < 100; a++)
                        {
                            ucs.txt1.Opacity = a/100;
                            ucs.txt2.Opacity = a/100;
                            ucs.txt3.Opacity = a/100;
                            ucs.txt4.Opacity = a / 100;
                            await Task.Delay(5);
                        }
                        ScrollViewerIteration.ScrollToBottom();
                        await Task.Delay(30);
                        
                        
                    }

                }
                else { MessageBox.Show("Aralık uçlarından biri negatif diğeri pozitif sonuç vermelidir.\nf(a)*f(b)<0", "Hata", MessageBoxButton.OK); }
            
            }
            else
            {
                MessageBox.Show("Lütfen aralıkların sayısal bir değerde ve fonksiyonun uygun yazılmış olduğundan emin olunuz.", "Hata", MessageBoxButton.OK);
            }
        }

        private void drawGraph(double initialA, double finalB, string func)
        {
           
            WpfPlot1.Plot.Clear();

            var axisLine = WpfPlot1.Plot.AddHorizontalLine(0);
            axisLine.Color = System.Drawing.Color.Black; 
            axisLine.LineWidth = 2; 
            axisLine.LineStyle = ScottPlot.LineStyle.Dash;

            
            double margin = (finalB - initialA) * 0.2;
            double xMin = initialA - margin;
            double xMax = finalB + margin;

            
            double[] xData = ScottPlot.DataGen.Range(xMin, xMax, 0.05);
            double[] yData = new double[xData.Length];

            Function f = new Function("f(x)=" + func);

            for (int i = 0; i < xData.Length; i++)
            {
                yData[i] = f.calculate(xData[i]);
            }

            
            WpfPlot1.Plot.AddScatter(xData, yData, System.Drawing.Color.Blue, lineWidth: 2, label: "f(x)",markerSize:0);

            
            for (int i = 0; i < rList.Count; i++)
            {
                var r = rList[i];
                int step = i + 1;

                
                WpfPlot1.Plot.AddPoint(r.c, r.fc, System.Drawing.Color.Red, size: 10);

                
                if (i < 3 || i == rList.Count - 1)
                {
                    WpfPlot1.Plot.AddText($"c{step}", r.c, r.fc, size: 12, color: System.Drawing.Color.Black);
                }
            }

           
            WpfPlot1.Plot.Title("Regula Falsi Metodu");
            WpfPlot1.Plot.XLabel("X Ekseni");
            WpfPlot1.Plot.YLabel("f(x)");

            
            WpfPlot1.Plot.AxisAuto();

            
            WpfPlot1.Refresh();
        }



    }
}