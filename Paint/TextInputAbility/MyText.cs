using System;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using MyContract;
using System.Windows.Documents;
using System.Text;

namespace TextInputAbility
{
    public class MyText: IShape
    {
        private const char minor_separator_1 = '!';
        private const char minor_separator_2 = ';';
        public Point Start { get; set; }
        public Point End { get; set; }

        public string Name => "Text";
        

        public Color ShapeColor { get; set; } = Colors.Transparent;
        public int Thickness { get; set; } = -1;
        DoubleCollection IShape.Stroke { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private DoubleCollection? Stroke;
        private string textContent = "";
        private FontFamily fontFamily = null;
        private int fontSize = 0;
        bool isBold = false;
        bool isItalic = false;
        bool isUnderline = false;
        Color color;



        public void UpdateStart(Point p)
        {
            Start = p;
        }
        public void UpdateEnd(Point p)
        {
            End = p;
        }
        public void UpdateFontFamily(Color c)
        {
            color = c;
        }

        public void UpdateFontFamily(FontFamily ff)
        {
            fontFamily = ff;
        }
        
        public void UpdateFontSize(int size)
        {
            fontSize = size;
        }
        public void UpdateBold(bool bold)
        {
            isBold = bold;
        }
        public void UpdateItalic(bool italic)
        {
            isItalic= italic;
        }
        public void UpdateUnderline(bool underline)
        {
            isUnderline= underline;
        }

        public void SetContent(string text)
        {
            textContent = text;
        }

        public UIElement Draw(Color color, int thickness, DoubleCollection stroke, string source)
        {
            //handle color and thickness of redraw shape, only assign once
            if (ShapeColor == Colors.Transparent) { ShapeColor = color; }
            if (Thickness == -1) { Thickness = thickness; }
            if (Stroke == null) { Stroke = stroke; }

            double width = Math.Abs(End.X - Start.X);
            double height = Math.Abs(End.Y - Start.Y);
            double left = Math.Min(Start.X, End.X); // Use Math.Min to determine the left position
            double top = Math.Min(Start.Y, End.Y); // Use Math.Min to determine the top position

            TextBlock shape = new TextBlock();
            
            shape.Width = width;
            shape.Height = height;
            shape.Text = textContent;
            shape.Foreground = new SolidColorBrush(color);
            if (fontFamily != null)
            {
                shape.FontFamily = fontFamily;
            }
            
            if (fontSize > 0)
            {
                shape.FontSize = fontSize;
            }
            
            if (isBold)
            {
                shape.FontWeight = FontWeights.Bold;
            }
            
            if (isItalic)
            {
                shape.FontStyle = FontStyles.Italic;
            }
            
            if (isUnderline)
            {
                shape.TextDecorations = TextDecorations.Underline;
            }
            


            Canvas.SetLeft(shape, left);
            Canvas.SetTop(shape, top);

            return shape;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public string FromShapeToString()
        {
            string constructed_string = "";

            //storage structure: <Type>:<ShapeColor>;<Thickness>;<Start>;<End>;<Stroke>;<TextContent>;<FontFamily>;<FontSize>;<Bold>;<Italic>;<Underline>
            //                      0  :     1      ;      2    ;   3   ;  4  ;    5   ;     6              7           8        9       10        11
            constructed_string = new StringBuilder().Append(Name).Append(minor_separator_1).Append(ShapeColor.ToString()).
                Append(minor_separator_2).Append(Thickness).Append(minor_separator_2).Append(Start).
                Append(minor_separator_2).Append(End).Append(minor_separator_2).Append("None")
                .Append(minor_separator_2).Append(textContent).Append(minor_separator_2).Append(fontFamily).Append(minor_separator_2).
                Append(fontSize).Append(minor_separator_2).Append(isBold).Append(minor_separator_2).Append(isItalic).Append(minor_separator_2).Append(isUnderline)
                .ToString();

            return constructed_string;
        }

        public IShape FromStringToShape(string str)
        {
            if(str == null)
            {
                throw new ArgumentNullException("The input constructed string is referenced to null object");
            }
            string[] details = str.Split(new char[] { minor_separator_1, minor_separator_2 });

            MyText textObject = new MyText();
            textObject.ShapeColor = (Color)ColorConverter.ConvertFromString(details[1]);
            textObject.Thickness = Convert.ToInt32(details[2]);
            textObject.Start = Point.Parse(details[3]);
            textObject.End = Point.Parse(details[4]);
            textObject.textContent= details[6];
            textObject.fontFamily = new FontFamily(details[7]);
            textObject.fontSize = Convert.ToInt32(details[8]);
            textObject.isBold = Convert.ToBoolean(details[9]);
            textObject.isItalic = Convert.ToBoolean(details[10]);
            textObject.isUnderline  = Convert.ToBoolean(details[11]);

            return textObject;
        }
    }
}