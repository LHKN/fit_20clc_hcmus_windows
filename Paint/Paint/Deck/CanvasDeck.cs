using Paint.DeckTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Deck
{
    class CanvasDeck : DeckWithTemplate
    {
        public CanvasDeck(UIElement adornedElement) :
            base(
                adornedElement,
                new CanvasTemplate()
            )
        {
        }
    }
}