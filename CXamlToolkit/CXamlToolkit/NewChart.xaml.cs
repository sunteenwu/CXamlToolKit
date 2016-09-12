using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using WinRTXamlToolkit.Tools;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CXamlToolkit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewChart : Page
    {
        private Random _random = new Random();
        private EventThrottler _updateThrottler = new EventThrottler();
        private readonly DispatcherTimer _timer;
        private int total = 1;
        public NewChart()
        {
            this.InitializeComponent();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            _timer.Tick += AddlineChat;
            _timer.Start();
        }

        private void AddlineChat(object sender, object e)
        {
            total += 1;
            _updateThrottler.Run(
                async () =>
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    this.UpdateCharts(total);
                    sw.Stop();
                    await Task.Delay(sw.Elapsed);
                });
        }

        private void RunIfSelected(UIElement element, Action action)
        {
            action.Invoke();
        }
        private void UpdateCharts(int n)
        {
            var items1 = new List<NameValueItem>();
            var items2 = new List<NameValueItem>();
            var items3 = new List<NameValueItem>();
            for (int i = 0; i < n; i++)
            {
                items1.Add(new NameValueItem { Name = "Name" + i, Value = _random.Next(10, 100) });
            }
            this.RunIfSelected(this.LineChart, () => ((LineSeries)this.LineChart.Series[0]).ItemsSource = items1);
        }
    }

    //public class NameValueItem
    //{
    //    public string Name { get; set; }
    //    public int Value { get; set; }
    //}
}
