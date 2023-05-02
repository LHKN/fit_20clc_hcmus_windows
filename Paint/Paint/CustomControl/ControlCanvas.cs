﻿using System.Windows.Controls;
using System.Windows;

namespace Paint.CustomControl
{
    class ControlCanvas : Canvas
    {
        static ControlCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ControlCanvas), new FrameworkPropertyMetadata(typeof(ControlCanvas)));
        }
    }
}