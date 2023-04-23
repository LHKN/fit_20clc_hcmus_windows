using MyContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        Dictionary<string, IShape> _abilities =
           new Dictionary<string, IShape>();
        Dictionary<string, string> _icons = new Dictionary<string, string>
        {
            { "Line", "Assets/diagonal-line.png" },
            { "Ellipse", "Assets/ellipse.png" },
            { "Rectangle", "Assets/rectangle.png" },
            { "Heart", "Assets/heart.png" }
        };
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Tự scan chương trình nạp lên các khả năng của mình
            var domain = AppDomain.CurrentDomain;
            var folder = domain.BaseDirectory;
            var folderInfo = new DirectoryInfo(folder);
            var dllFiles = folderInfo.GetFiles("*.dll");

            foreach (var dll in dllFiles)
            {
                Debug.WriteLine(dll.FullName);
                var assembly = Assembly.LoadFrom(dll.FullName);

                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass &&
                        typeof(IShape).IsAssignableFrom(type))
                    {
                        var shape = Activator.CreateInstance(type) as IShape;
                        _abilities.Add(shape!.Name, shape);
                    }
                }
            }

            foreach (var ability in _abilities)
            {
                var button = new Fluent.Button()
                {
                    Header = ability.Value.Name,
                    Content = ability.Value.Name,
                    Tag = ability.Value.Name,
                    Icon = _icons[ability.Value.Name],
                };

                button.Click += ability_Click;
                button.Size = Fluent.RibbonControlSize.Small;
                RenderOptions.SetBitmapScalingMode(button, BitmapScalingMode.HighQuality);
                shapesAbility.Items.Add(button);
            }
        }

        private void ability_Click(object sender, RoutedEventArgs e)
        {
            var button = (Fluent.Button)sender;
            string name = (string)button.Tag;
            _selectedType = name;
        }

        bool _isDrawing = false;
        IShape? _prototype = null;
        string _selectedType = "";
        Color _selectedColor = Colors.Black;
        int _selectedThickness = 1; //By default

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (String.IsNullOrEmpty(_selectedType)) { _isDrawing = false; return; }
            
            
           _isDrawing = true;
            _start = e.GetPosition(actualCanvas);

            _prototype = (IShape)
                _abilities[_selectedType].Clone();
            _prototype.UpdateStart(_start);
        }

        Point _start;
        Point _end;

        List<IShape> _shapes = new List<IShape>();
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (String.IsNullOrEmpty(_selectedType)) { _isDrawing = false; return; }

            if (_isDrawing)
            {
                actualCanvas.Children.Clear();

                foreach (var shape in _shapes)
                {
                    UIElement oldShape = shape.Draw(_selectedColor, _selectedThickness);
                    actualCanvas.Children.Add(oldShape);
                }

                _end = e.GetPosition(actualCanvas);
                _prototype?.UpdateEnd(_end);

                UIElement newShape = _prototype.Draw(_selectedColor, _selectedThickness);
                actualCanvas.Children.Add(newShape);
            }
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (String.IsNullOrEmpty(_selectedType)) { _isDrawing = false; return; }

            _shapes.Add((IShape)_prototype.Clone());

            _isDrawing = false;

            Title = "Up";
        }

        private void brushSize_Thin_Click(object sender, RoutedEventArgs e)
        {
            _selectedThickness = 1;
        }

        private void brushSize_Medium_Click(object sender, RoutedEventArgs e)
        {
            _selectedThickness = 3;
        }

        private void brushSize_Thick_Click(object sender, RoutedEventArgs e)
        {
            _selectedThickness = 5;
        }

        private void brushSize_ExtraThick_Click(object sender, RoutedEventArgs e)
        {
            _selectedThickness = 10;
        }

        private void undoCommand_Click(object sender, RoutedEventArgs e)
        {

        }

        private void redoCommand_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
