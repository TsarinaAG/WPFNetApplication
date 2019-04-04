using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfNetApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int n = 10;
        Rectangle[] nodesView;
        int[][] matr;
        Random rnd;
        public MainWindow()
        {
            rnd = new Random();
            InitializeComponent();

            matr = new int[n][];
            for (int i=0; i<n; i++)
            {
                matr[i] = new int[n];
                var col = new DataGridTextColumn();
                col.Header = (i+1).ToString();
                col.Binding = new Binding(string.Format("[{0}]", i));
                DG_Data.Columns.Add(col);
                for (int j = 0; j < n; j++)
                    matr[i][j] = rnd.Next(0, 2);
            }           
            DG_Data.ItemsSource = matr;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            nodesView = new Rectangle[n];
            double angle = Math.PI * 2.0 / n;
            double w = canvas.RenderSize.Width;
            double h = canvas.RenderSize.Height;
            double size = (w < h ? w : h);
            Point center = new Point(w / 2, h / 2);
            double R = size / 2 - 20;
            for (int i = 0; i < n; i++)
            {
                nodesView[i] = new Rectangle();
                nodesView[i].Fill = Brushes.DarkOliveGreen;
                nodesView[i].Height = 20;
                nodesView[i].Width = 20;
                double alpha = i*angle;
                double x = R * Math.Sin(alpha);
                double y = R * Math.Cos(alpha);
                //nodesView[i].Margin = new Thickness(center.X - x , center.Y - y ,0,0);
                canvas.Children.Add(nodesView[i]);
                Canvas.SetLeft(nodesView[i], center.X - x-10);
                Canvas.SetTop(nodesView[i], center.Y - y-10); 
            }
            for (int i = 0; i < n; i++)
            {
                double x1 = Canvas.GetLeft(nodesView[i])+10;
                double y1 = Canvas.GetTop(nodesView[i])+10;
                for (int j = i + 1; j < n; j++)
                {
                    double x2 = Canvas.GetLeft(nodesView[j]) + 10;
                    double y2 = Canvas.GetTop(nodesView[j]) + 10;
                    Line line = new Line();
                    line.Stroke = Brushes.BurlyWood;
                    line.StrokeThickness = 2;
                    line.X1 = x1;
                    line.X2 = x2;
                    line.Y1 = y1;
                    line.Y2 = y2;
                    canvas.Children.Add(line);
                }
                  
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (nodesView != null)
            {
                OpenFile_Click(null, null);
            }
        }
    }
}
