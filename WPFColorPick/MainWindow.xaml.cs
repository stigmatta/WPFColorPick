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
using System.Windows.Threading;

namespace WPFColorPick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List <string>colors;
        public MainWindow()
        {
            InitializeComponent();

            colors = new List<string>();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += Timer_Tick;

            timer.Start();


        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte alpha = (byte)alphaSlider.Value;
            byte red = (byte)redSlider.Value;
            byte green = (byte)greenSlider.Value;
            byte blue = (byte)blueSlider.Value;

            Color color = Color.FromArgb(alpha, red, green, blue);

            rectColor.Fill = new SolidColorBrush(color);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)rectColor.Fill;
            Color color = brush.Color;

            colors.Add(color.ToString());

            DockPanel dock = new DockPanel();
            dock.Margin = new Thickness(0, 0, 0, 15);

            Label label = new Label();
            label.Content = color.ToString();
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Margin = new Thickness(0, 0, 5, 0);

            Button button = new Button();
            button.Content = "Delete";
            button.Margin = new Thickness(5, 0, 10, 0);
            button.Padding = new Thickness(15, 5, 15, 5);
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Click += Delete_Click;

            Rectangle rectangle = new Rectangle();
            rectangle.Fill = new SolidColorBrush(color);
            rectangle.HorizontalAlignment = HorizontalAlignment.Stretch;

            DockPanel.SetDock(label, Dock.Left);
            DockPanel.SetDock(button, Dock.Right);

            dock.Children.Add(label);
            dock.Children.Add(button);
            dock.Children.Add(rectangle);

            listbox.Children.Add(dock);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            DockPanel dock = btn.Parent as DockPanel;
            Label label = dock.Children[0] as Label;
            colors.Remove(label.Content.ToString());

            listbox.Children.Remove(dock);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)rectColor.Fill;
            Color color = brush.Color;

            if (colors.Contains(color.ToString()))
                addButton.IsEnabled = false;
            else
                addButton.IsEnabled = true;
        }

    }
}