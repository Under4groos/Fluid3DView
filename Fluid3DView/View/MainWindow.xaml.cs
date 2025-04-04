using _3DPrintConnect.ComConnector;
using _3DPrintConnect.ComConnector.Structures;
using HelixToolkit.Wpf;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Fluid3DView.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Random random = new Random();
    private COMConnector connector = new COMConnector();
    private string COMPORT = "COM21";

    public MainWindow()
    {
        InitializeComponent();

     
        
        
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        //printer.Transform = new ScaleTransform3D(new Vector3D(1, 1, 22), printer.Center);
        center.Transform = new ScaleTransform3D(new Vector3D(0.1, 0.1, 22), printer.Center);
        startpoint.Transform = new ScaleTransform3D(new Vector3D(0.1, 0.1, 22), printer.Center);
        foreach (TextVisual3D item in labels.Children)
        {
            var rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90); // Rotate around the Y-axis
            var rotateTransform = new RotateTransform3D(rotation, item.Position);
            item.Transform = rotateTransform;
        }



        connector.OnError += (result) =>
        {
            Debug.WriteLine(result);
        };
        connector.OnDisposed += () =>
        {
            Debug.WriteLine("IDisposable");
        };
        connector.OnCommandComplite += async (command) =>
        {
            //Debug.WriteLine("\n-----------\n");
            //Debug.WriteLine(command.StringResult);
            if (command.Command.StartsWith("M114"))
            {
                await Task.Delay(100);
                GVector gvector = GParser.ParseGVector(command.StringResult);

                var point = new Point3D(110 - (double)gvector.X, 110 - (double)gvector.Y, (double)gvector.Z);
                Debug.WriteLine(point);
                
                this.Dispatcher.Invoke(() =>
                {
                   
                  
                    printer.Center = point;
                     
                });
            }
            

        };

        if (connector.Connect(COMPORT))
        {
            connector.AddCommand($"G28").AddCommand($"M114").Run();
       
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        connector.Disconnect();

        base.OnClosing(e);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        connector.AddCommand($"G28").AddCommand($"M114").Run();

    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        string runcmd = $"G1 X{random.Next(-110, 110)} Y{random.Next(-110, 110)}";
        //string runcmd = $"G1 X{200} Y{200}";
        Debug.WriteLine(runcmd);
        connector.AddCommand(runcmd).AddCommand($"M114").Run();

       
       

    }
}