using Microsoft.Win32;
using MyContract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms.VisualStyles;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;

namespace Paint
{
    public class MyFile
    {
        public const int BINARY_FILE = 1;
        public const int XML_FILE = 2;
        public const string MPXML_EXT = ".mpxml";
        public const string MPBIT_EXT = ".mpbit";
        public const string BITMAP_EXT = ".bmp";
        public const string PNG_EXT = ".png";
        public const string JPG_EXT = ".jpg";
        public const int CREATE_BITMAP = 1001;
        public const int CREATE_PNG = 1002;
        public const int CREATE_JPG = 1003;


        private const char big_seperator = '|';
        private const char minor_seperator_1 = ':';
        private const char minor_seperator_2 = ';';
        private const int BUFFER_SIZE = 1024 * 2;

        public double ImageDpiX { get; set; }
        public double ImageDpiY { get; set; }
        public System.Windows.Media.PixelFormat ImagePixelFormat { get; set; }
        

        public Dictionary<string, IShape>? ReferenceAbilities { get; set; } = null;

        public string ?CurrentStoredPath { get; set; } = null;

        public SaveFileDialog SaveFileDialog { get; set; }

        public OpenFileDialog OpenFileDialog { get; set; }

        public SaveFileDialog SaveImageDialog { get; set; }

        public MyFile()
        {
            SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.InitialDirectory = @"C:\";
            SaveFileDialog.Title = "Save your file";
            SaveFileDialog.RestoreDirectory = true;
            SaveFileDialog.DefaultExt = "mpbin";
            SaveFileDialog.CheckFileExists= false;
            SaveFileDialog.CheckPathExists= true;
            SaveFileDialog.Filter = "Binary file (*.mpbin)|*.mpbin|Xml file (*.xml)|*.xml";
            SaveFileDialog.FilterIndex = 1;
            SaveFileDialog.AddExtension = true;

            OpenFileDialog= new OpenFileDialog();
            OpenFileDialog.Filter = "Binary file (*.mpbin)|*.mpbin|Xml file (*.xml)|*.xml";
            OpenFileDialog.Title = "Open file";
            OpenFileDialog.FilterIndex = 1;
            OpenFileDialog.AddExtension = true;
            OpenFileDialog.CheckFileExists= false;
            OpenFileDialog.CheckPathExists= true;
            OpenFileDialog.DefaultExt = "xml";

            SaveImageDialog= new SaveFileDialog();
            SaveImageDialog.InitialDirectory = @"C:\";
            SaveImageDialog.Title = "Export to image";
            SaveImageDialog.RestoreDirectory = true;
            SaveImageDialog.DefaultExt = "bmp";
            SaveImageDialog.CheckFileExists= false;
            SaveImageDialog.CheckPathExists= true;
            SaveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp|PNG file (*.png)|*.png";
            SaveImageDialog.FilterIndex = 1;
            SaveImageDialog.AddExtension = true;

            ImageDpiX = 1 / 96;
            ImageDpiY = 1 / 96;
            ImagePixelFormat = PixelFormats.Pbgra32;
        }

        public MyFile(string OpenPath)
        {
            SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.InitialDirectory = @"C:\";
            SaveFileDialog.Title = "Save your file";
            SaveFileDialog.RestoreDirectory = true;
            SaveFileDialog.CheckFileExists = true;
            SaveFileDialog.CheckPathExists = true;
            SaveFileDialog.AddExtension = true;
            SaveFileDialog.DefaultExt = "mpbin";
            SaveFileDialog.Filter = "Binary file (*.mpbin)|*.mpbin|Xml file (*.xml)|*.xml";
            CurrentStoredPath = OpenPath;

            OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Binary file (*.mpbin)|*.mpbin|Xml file (*.xml)|*.xml";
            OpenFileDialog.Title = "Open file";
            OpenFileDialog.FilterIndex = 1;
            OpenFileDialog.AddExtension = true;
            OpenFileDialog.CheckFileExists = false;
            OpenFileDialog.CheckPathExists = true;
            OpenFileDialog.DefaultExt = "mpbin";

            SaveImageDialog = new SaveFileDialog();
            SaveImageDialog.InitialDirectory = @"C:\";
            SaveImageDialog.Title = "Export to image";
            SaveImageDialog.RestoreDirectory = true;
            SaveImageDialog.DefaultExt = "bmp";
            SaveImageDialog.CheckFileExists = false;
            SaveImageDialog.CheckPathExists = true;
            SaveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp|PNG file (*.png)|*.png";
            SaveImageDialog.FilterIndex = 1;
            SaveImageDialog.AddExtension = true;

            ImageDpiX = 1 / 96;
            ImageDpiY = 1 / 96;
            ImagePixelFormat = PixelFormats.Pbgra32;
        }

        public bool isExist(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Exists;
        }

        public bool isNewFile()
        {
            if(CurrentStoredPath == null)
            {
                return true;
            }
            else
            { return false; }
        }

        public bool WriteTo(string filepath, List<IShape> shapes, int write_mode)
        {
            bool result = true;
            try
            {
                switch (write_mode)
                {
                    case XML_FILE:
                        {

                            StreamWriter writer = new StreamWriter(filepath);
                            //clear old data
                            writer.Flush();
                            for (int i = 0; i < shapes.Count; i++)
                            {
                                Debug.WriteLine(shapes[i].GetType().ToString());
                                XmlSerializer s = new XmlSerializer(shapes[i].GetType());

                                s.Serialize(writer, shapes[i]);
                            }

                            writer.Close();
                            result = true;
                            break;
                        }
                    case BINARY_FILE:
                        {
                            //storage structure: <Type>:<ShapeColor>,<Thickness>,<Start>,<End>|....
                            string construct_string = "";
                            for (int i = 0; i < shapes.Count; i++)
                            {
                                string type = shapes[i].Name;
                                string shape_color = shapes[i].ShapeColor.ToString();
                                int thickness = shapes[i].Thickness;
                                System.Windows.Point start = shapes[i].Start;
                                System.Windows.Point End = shapes[i].End;
                                string storage_item = new StringBuilder().Append(big_seperator).Append(type).Append(minor_seperator_1).Append(shape_color)
                                    .Append(minor_seperator_2).Append(thickness).Append(minor_seperator_2).Append(start).Append(minor_seperator_2).Append(End).ToString();
                                Debug.WriteLine(storage_item);

                                construct_string = construct_string + storage_item;
                            }

                            Debug.WriteLine(construct_string);

                            if(construct_string.Length <= 0) { break; }
                            //convert to array of bytes
                            byte[] buffer = Encoding.Unicode.GetBytes(construct_string);


                            FileStream file = File.Open(filepath, FileMode.OpenOrCreate);
                            
                            BinaryWriter writer = new BinaryWriter(file, Encoding.UTF8, false);
                            writer.Seek(0, SeekOrigin.Begin);
                            writer.Flush();

                            /*writer.Write(construct_string);*/
                            writer.Write(buffer);
                            writer.Close();

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result = false;
            }

            return result;
        }

        public List<IShape> ReadFrom(string filepath, int read_mode)
        {
            if(ReferenceAbilities == null)
            {
                throw new ArgumentNullException("To use the function, please use the set ReferenceAbilities variable with the abilities of the main program.");    
            }

            List<IShape> shapes = new List<IShape>();
            try
            {
                switch (read_mode)
                {
                    case XML_FILE:
                        {

                            XmlDocument xmlDocument= new XmlDocument();
                            xmlDocument.Load(filepath);
                            if(xmlDocument.DocumentElement == null)
                            {
                                shapes = null;
                                break;
                            }
                            //List<string> node_label = new List<string>();
                            //cycle through each child nodes
                            foreach (XmlDocument node in xmlDocument.DocumentElement.ChildNodes)
                            {
                                Debug.WriteLine(node.Name);
                            }


                          /*  StreamReader reader = new StreamReader(filepath);
                            
                            XmlSerializer s = new XmlSerializer()*/
                            break;
                        }
                    case BINARY_FILE:
                        {
                            FileStream file = File.Open(filepath, FileMode.Open);
                            BinaryReader reader = new BinaryReader(file, Encoding.Unicode);
                            reader.BaseStream.Position = 0;
                            /*List<char> buffer = new List<char>();
                            char[] temp_buffer = new char[BUFFER_SIZE];
                            int read_chars = BUFFER_SIZE;

                            do
                            {
                                read_chars = reader.ReadBlock(temp_buffer, buffer.Count, BUFFER_SIZE);
                                
                            }
                            while (read_chars == BUFFER_SIZE);*/
                            List<byte> buffer = new List<byte>();

                            long file_size = file.Length;
                            do
                            {
                                byte[] task = reader.ReadBytes(BUFFER_SIZE);
                                buffer.AddRange(task);
                            }
                            while (buffer.Count < file_size);
                            
                            string convertedString = Encoding.Unicode.GetString(buffer.ToArray());
                            Debug.WriteLine(convertedString);
                            reader.Close();

                            if (convertedString != null)
                            {
                                string[] items = convertedString.Split(big_seperator);
                                //as the first item is an empty string
                                for (int i = 1; i < items.Length; i++)
                                {
                                    Debug.WriteLine(items[i]);
                                    string[] details = items[i].Split(new char[] { minor_seperator_1, minor_seperator_2 });
                                    //details[0] == Name (Type)
                                    //details[1] == ShapeColor
                                    //details[2] == Thickness
                                    //details[3] == Start
                                    //details[4] == End

                                    IShape shape = (IShape)ReferenceAbilities[details[0]].Clone();
                                    System.Windows.Media.Color shape_color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(details[1]);
                                    System.Windows.Point start = System.Windows.Point.Parse(details[3]);
                                    System.Windows.Point end = System.Windows.Point.Parse(details[4]);
                                    int thickness = Convert.ToInt32(details[2]);
                                    shape.UpdateStart(start);
                                    shape.UpdateEnd(end);
                                    shape.Thickness = thickness;
                                    shape.ShapeColor = shape_color;
                                    shapes.Add(shape);

                                }
                            }

                            break;
                        }
                }
            }
            catch(Exception ex)
            {

            }

            return shapes;
        }

        public void SaveImage(string filepath, Canvas actual_canvas, int width, int height, int mode)
        {
            if(filepath == null || isExist(filepath))
            {
                return;
            }
            FileStream file = new FileStream(filepath, FileMode.OpenOrCreate);
            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, ImageDpiX, ImageDpiY, ImagePixelFormat);
            bmp.Render(actual_canvas);
            BitmapEncoder encoder = new TiffBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            switch (mode)
            {
                case CREATE_BITMAP:
                    {
                        encoder.Save(file);
                        file.Close();
                        break;
                    }
                case CREATE_PNG:
                    {
                        using(MemoryStream memoryStream= new MemoryStream())
                        {
                            encoder.Save(memoryStream);
                            Bitmap bitmap = new Bitmap(memoryStream);
                            
                            bitmap.Save(file, ImageFormat.Png);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            memoryStream.Dispose();
                        }
                        file.Close();
                        break;
                    }
            }
        }
    }
}
