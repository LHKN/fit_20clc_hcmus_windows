using Paint.CustomControl;
using System;
using System.Windows;

namespace Paint.Actions
{
    public class Drawing
    {
        private ControlItem _context { get; set; }
        private Point _startPosition { get; set; }

        public Drawing(ControlItem itemsControl)
        {
            _context = itemsControl;
        }

        public void OnMouseDown(Point startPosition)
        {
            _startPosition = startPosition;
            _context.DrawingNode.IsDrawing = true;
            _context.DrawingNode.IsCommitChanged = false;
        }

        public void OnMouseMove(Point movingPosition)
        {
            double top = _startPosition.Y;
            double left = _startPosition.X;
            double width = Math.Abs(movingPosition.X - _startPosition.X);
            double height = Math.Abs(movingPosition.Y - _startPosition.Y);

            if (_startPosition.X < movingPosition.X)
                left = _startPosition.X;

            if (_startPosition.X >= movingPosition.X)
                left = movingPosition.X;

            if (_startPosition.Y < movingPosition.Y)
                top = _startPosition.Y;

            if (_startPosition.Y >= movingPosition.Y)
                top = movingPosition.Y;

            _context.DrawingNode.Top = top;
            _context.DrawingNode.Left = left;
            _context.DrawingNode.Width = width;
            _context.DrawingNode.Height = height;
        }

        public void OnMouseUp(Point endPosition)
        {
            _context.DrawingNode.IsSelected = true;
            _context.DrawingNode.IsDrawing = false;
            _context.DrawingNode.IsCommitChanged = true;
            _context.SelectedItems.Add(_context.DrawingNode);
            _context.DrawingNode = null;
        }
    }
}
