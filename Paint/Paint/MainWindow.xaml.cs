using Microsoft.Win32;
using MyContract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow, INotifyPropertyChanged
    {
        Dictionary<string, IShape> _abilities =
           new Dictionary<string, IShape>();
        Stack<IShape> _storeShapes = new Stack<IShape>();
        bool isActionStorable = true;
        bool isExecuteStoreAction = false;
        Dictionary<string, string> _icons = new Dictionary<string, string>
        {
            { "Line", "Assets/diagonal-line.png" },
            { "Ellipse", "Assets/ellipse.png" },
            { "Rectangle", "Assets/rectangle.png" },
            { "Heart", "Assets/heart.png" },
            { "Pencil", "Assets/pencil.png" },
            { "Image", "Assets/image.png" }
        };
        private Matrix originalMatrix;

        bool _isDrawing = false;
        IShape? _prototype = null;
        string _selectedType;
        Color _selectedColor;
        int _selectedThickness;
        DoubleCollection? _selectedStroke;
        private string _mousePos;

        Point _start;
        Point _end;
        string _newPathAbsolute;

        private MyFile MyFile;

        private string collection_of_pressed_keys = "";

        List<IShape> _shapes;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        public string MousePos
        {
            get => _mousePos; 
            set 
            {
                if (_mousePos != value)
                {
                    _mousePos = value;
                    OnPropertyChanged(nameof(MousePos));
                }
            }
        }

        private void defaultSettings()
        {
            _isDrawing = false;
            _selectedType = "";
            _selectedColor = Colors.Black;
            _selectedThickness = 1;
            _selectedStroke = null;
            _shapes = new List<IShape>();
        }
        public MainWindow()
        {
            InitializeComponent();
            var zoomedMatrix = aboveCanvas.RenderTransform as MatrixTransform;
            if (zoomedMatrix != null) originalMatrix = zoomedMatrix.Matrix;
            defaultSettings();

            DataContext = this;
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
                var button = new Fluent.ToggleButton()
                {
                    Header = ability.Value.Name,
                    Content = ability.Value.Name,
                    Tag = ability.Value.Name,
                    Icon = _icons[ability.Value.Name],
                    GroupName = "Abilities"
                };

                button.Click += ability_Click;
                button.Size = Fluent.RibbonControlSize.Small;
                RenderOptions.SetBitmapScalingMode(button, BitmapScalingMode.HighQuality);
                shapeAbilityGrid.Children.Add(button);
            }

            //var folder = AppDomain.CurrentDomain.BaseDirectory;
            _newPathAbsolute = $"{folder}Assets/image.png";

            MyFile = new MyFile();
            MyFile.ReferenceAbilities = _abilities;
        }

        private void ability_Click(object sender, RoutedEventArgs e)
        {
            var button = (Fluent.ToggleButton)sender;
            string name = (string)button.Tag;
            _selectedType = name;
        }

        private void drawOldShapes()
        {
            actualCanvas.Children.Clear();

            foreach (var shape in _shapes)
            {
                UIElement oldShape = shape.Draw(_selectedColor, _selectedThickness, _selectedStroke, _newPathAbsolute);
                actualCanvas.Children.Add(oldShape);
            }
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (String.IsNullOrEmpty(_selectedType)) { _isDrawing = false; return; }

            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    // Left mouse button was clicked
            //    SolidColorBrush scb = (SolidColorBrush)primaryColor.Background;
            //    _selectedColor = scb.Color;
            //}
            //else if (e.RightButton == MouseButtonState.Pressed)
            //{
            //    // Right mouse button was clicked
            //    SolidColorBrush scb = (SolidColorBrush)secondaryColor.Background;
            //    _selectedColor = scb.Color;
            //}


            _isDrawing = true;
            isActionStorable = true;
            _start = e.GetPosition(actualCanvas);

            _prototype = (IShape)
                _abilities[_selectedType].Clone();
            _prototype.UpdateStart(_start);
        }

        private void canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(actualCanvas);
            MousePos = $"{Math.Round(mousePos.X,0)}px , {Math.Round(mousePos.Y,0)}px";

            if (String.IsNullOrEmpty(_selectedType)) { _isDrawing = false; return; }
            
            if (_isDrawing)
            {
                drawOldShapes();
               
                _end = e.GetPosition(actualCanvas);
                _prototype?.UpdateEnd(_end);

                UIElement newShape = _prototype.Draw(_selectedColor, _selectedThickness, _selectedStroke, _newPathAbsolute);
                actualCanvas.Children.Add(newShape);
            }
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (String.IsNullOrEmpty(_selectedType)) { _isDrawing = false; return; }

            _shapes.Add((IShape)_prototype.Clone());

            _isDrawing = false;

            if (isExecuteStoreAction) { 
                //isActionStorable = false;
                _storeShapes.Clear();
            }
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

        public void UndoAction()
        {
            if (isActionStorable && _shapes.Count != 0)
            {
                var item = _shapes.Last().Clone();
                _shapes.RemoveAt(_shapes.Count - 1);
                _storeShapes.Push(item as IShape);
                isExecuteStoreAction = true;
                drawOldShapes();
            }
            else { isExecuteStoreAction = false; }
        }

        public void RedoAction() 
        {
            if (isActionStorable && _storeShapes.Count != 0)
            {
                var item = _storeShapes.Pop();
                _shapes.Add(item);
                isExecuteStoreAction = true;
                drawOldShapes();
            }
            else { isExecuteStoreAction = false; }
        }
        private void undoCommand_Click(object sender, RoutedEventArgs e)
        {
            UndoAction();
        }

        private void redoCommand_Click(object sender, RoutedEventArgs e)
        {
            RedoAction();
        }

        private void redColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.Red;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }

        private void whiteColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.White;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }

        private void blackColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.Black;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }

        private void orangeColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.Orange;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }

        private void yellowColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.Yellow;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }

        private void greenColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.Green;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }

        private void blueColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.Blue;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }

        private void indigoColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.Indigo;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }

        private void violetColor_Click(object sender, RoutedEventArgs e)
        {
            _selectedColor = Colors.Violet;
            primaryColor.Background = new SolidColorBrush(_selectedColor);
        }


        private void Screen_KeyDown_Handler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            collection_of_pressed_keys += e.Key.ToString();
        }


        private void Screen_KeyUp_Handler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("Key pressed: " + collection_of_pressed_keys);

            if(collection_of_pressed_keys.Equals(Constants.SAVE))
            {
                Save_File();
            }
            else if (collection_of_pressed_keys.Equals(Constants.UNDO))
            {
                UndoAction();
            }
            else if (collection_of_pressed_keys.Equals(Constants.REDO))
            {
                RedoAction();
            }
            collection_of_pressed_keys = "";
            
        }
        
        
        private void Menu_Button_Handler(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(sender.ToString());
            Debug.WriteLine(e.Source.ToString());
            string SenderContent = sender.ToString().Split(':')[1].Trim();

            if (SenderContent.Equals(Constants.MENU_SAVE))
            {
                Save_File();
            }
            else if(SenderContent.Equals(Constants.MENU_OPEN))
            {
                Open_File();
            }
            else if(SenderContent.Equals(Constants.MENU_SAVE_AS))
            {

            }
            else if(SenderContent.Equals(Constants.MENU_EXPORT_TO))
            {
                Save_Image();
            }
            else if(SenderContent.Equals(Constants.MENU_NEW))
            {
                New_File();
            }
        }

        private void New_File()
        {
            //Ask to save current file
            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to save your work?", "Save", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes) {
                Save_Image();
                _shapes.Clear();
                _storeShapes.Clear();
                defaultSettings();
                drawOldShapes();

            }
            else if (result == MessageBoxResult.No)
            {
                defaultSettings();
                drawOldShapes();
            }
            else
            {
                //Do nothing..
            }
        }


        /// <execute>
        private void Save_File()
        {
            if (MyFile.isNewFile())
            {
                MyFile.SaveFileDialog.CheckFileExists = false;
                bool? check = MyFile.SaveFileDialog.ShowDialog();
                Debug.WriteLine("Check= " + check.ToString());
                if (check != null && check == true)
                {
                    string path = MyFile.SaveFileDialog.FileName;
                    MyFile.CurrentStoredPath = path;
                    int write_mode = 1;
                    if (MyFile.SaveFileDialog.FilterIndex == 1)
                    {
                        write_mode = MyFile.BINARY_FILE;
                    }
                    else if(MyFile.SaveFileDialog.FilterIndex == 2)
                    {
                        write_mode = MyFile.XML_FILE;
                    }
                    MyFile.WriteTo(MyFile.CurrentStoredPath, _shapes, write_mode);
                }
            }
            else
            {
                string? ext = System.IO.Path.GetExtension(MyFile.CurrentStoredPath);
                if (ext != null)
                {
                    Debug.WriteLine(ext);
                    /* MyFile.WriteTo(MyFile.CurrentStoredPath, _shapes, )*/
                    if (ext.Equals(MyFile.MPXML_EXT)) // xml mode
                    {
                        MyFile.WriteTo(MyFile.CurrentStoredPath!, _shapes, MyFile.XML_FILE);
                    }
                    else //binary mode
                    {
                        MyFile.WriteTo(MyFile.CurrentStoredPath!, _shapes, MyFile.BINARY_FILE);
                    }
                }

            }
        }
        
        private void Open_File()
        {
            string SelectedFile = "";
            bool? check = MyFile.OpenFileDialog.ShowDialog();
            if(check!= null && check == true)
            {
                int selectedIndex = MyFile.OpenFileDialog.FilterIndex;
                int mode = 0;
                if(selectedIndex == 1)
                {
                    mode = MyFile.BINARY_FILE;
                }
                else if(selectedIndex == 2)
                {
                    mode = MyFile.XML_FILE;
                }

                SelectedFile = MyFile.OpenFileDialog.FileName;
                Debug.WriteLine($"{SelectedFile} is selected");
                MyFile.CurrentStoredPath = SelectedFile;
                _shapes = MyFile.ReadFrom(MyFile.CurrentStoredPath, mode);
                drawOldShapes();
            }

        }

        private void Save_Image()
        {
            bool? check = MyFile.SaveImageDialog.ShowDialog();
            if(check!= null && check == true )
            {
                string path = MyFile.SaveImageDialog.FileName;
                Debug.WriteLine(path);
                string? ext = System.IO.Path.GetExtension(path);
                int mode = MyFile.CREATE_BITMAP;
                if(ext != null )
                {
                    if(ext.Equals(MyFile.BITMAP_EXT))
                    {
                        mode = MyFile.CREATE_BITMAP;
                    }
                    else if(ext.Equals(MyFile.PNG_EXT))
                    {
                        mode = MyFile.CREATE_PNG;
                    }
                    else if(ext.Equals(MyFile.JPG_EXT))
                    {
                        mode = MyFile.CREATE_JPG;
                    }
                }
                int canvas_width = Convert.ToInt32(Math.Ceiling(aboveCanvas.ActualWidth));
                int canvas_height = Convert.ToInt32(Math.Ceiling(aboveCanvas.ActualHeight));
                MyFile.SaveImage(path, actualCanvas, canvas_width, canvas_height, mode);

            }
        }


        /// </execute>

        private void strokeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = strokeComboBox.SelectedIndex;

            switch (index)
            {
                case 0:
                    _selectedStroke = new DoubleCollection();
                    break;
                case 1:
                    _selectedStroke = new DoubleCollection() { 4, 2, 1, 2, 1, 2 };
                    break;
                case 2:
                    _selectedStroke = new DoubleCollection() { 1, 2 };
                    break;
                case 3:
                    _selectedStroke = new DoubleCollection() { 6, 2 };
                    break;
                default:
                    break;
            }
        }

        private void zoomInCommand_Click(object sender, RoutedEventArgs e)
        {
            Point center = actualCanvas.TransformToAncestor(whiteBoard).Transform(new Point(actualCanvas.ActualWidth / 2, actualCanvas.ActualHeight / 2));

            var curMatrix = actualCanvas.RenderTransform as MatrixTransform;
            var matrix = curMatrix.Matrix;
            var scale = 1.1;
            matrix.ScaleAt(scale, scale, center.X, center.Y);
            curMatrix.Matrix = matrix;
            e.Handled = true;
        }

        private void zoomOutCommand_Click(object sender, RoutedEventArgs e)
        {
            Point center = actualCanvas.TransformToAncestor(whiteBoard).Transform(new Point(actualCanvas.ActualWidth / 2, actualCanvas.ActualHeight / 2));

            var curMatrix = actualCanvas.RenderTransform as MatrixTransform;
            var matrix = curMatrix.Matrix;
            var scale = 1 / 1.1;
            matrix.ScaleAt(scale, scale, center.X, center.Y);
            curMatrix.Matrix = matrix;
            e.Handled = true;
        }

        private void zoomOriginalCommand_Click(object sender, RoutedEventArgs e)
        {
            var curMatrix = actualCanvas.RenderTransform as MatrixTransform;
            curMatrix.Matrix = originalMatrix;
            e.Handled = true;
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var curMatrix = actualCanvas.RenderTransform as MatrixTransform;
            var pos1 = e.GetPosition(whiteBoard);

            var scale = e.Delta > 0 ? 1.1 : 1 / 1.1;

            var matrix = curMatrix.Matrix;
            matrix.ScaleAt(scale, scale, pos1.X, pos1.Y);
            curMatrix.Matrix = matrix;
            e.Handled = true;
        }

        private void insertImage_Click(object sender, RoutedEventArgs e)
        {
            FileInfo? _selectedImage = null;
            BitmapImage imageBitmap;

            var screen = new System.Windows.Forms.OpenFileDialog();
            screen.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
            "|GIF Graphics Interchange Format (*.gif)|*.gif";

            if (screen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _selectedImage = new FileInfo(screen.FileName);
            }

            if (_selectedImage == null) { return; };
            Random rng = new Random();
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            _newPathAbsolute = $"{folder}Assets\\{_selectedImage.Name}";
            string relativePath = $"Assets\\{_selectedImage.Name}";


            if (File.Exists(_newPathAbsolute))
            {
                _newPathAbsolute = $"{folder}Assets\\{rng.Next()}{_selectedImage.Name}";
            }
            File.Copy(_selectedImage.FullName, _newPathAbsolute);
        }



    }
}
