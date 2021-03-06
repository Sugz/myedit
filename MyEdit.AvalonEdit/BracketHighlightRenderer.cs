﻿﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media;

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;

namespace MyEdit.AvalonEdit
{
    public class BracketHighlightRenderer : IBackgroundRenderer
    {
        Pen borderPen;
        Brush backgroundBrush;


        public const string BracketHighlight = "Bracket highlight";

        public void SetHighlight(BracketSearchResult result)
        {
            if (this.result != result)
            {
                this.result = result;
            }
        }

        public BracketHighlightRenderer(Color? defaultBackground, Color? defaultBorder)
        {
            this.UpdateColors(Color.FromArgb(0,0,0,0), defaultBorder.Value);
        }

        void UpdateColors(Color background, Color foreground)
        {
            this.borderPen = new Pen(new SolidColorBrush(foreground), 1);
            this.borderPen.Freeze();

            this.backgroundBrush = new SolidColorBrush(background);
            this.backgroundBrush.Freeze();
        }

        public KnownLayer Layer
        {
            get
            {
                return KnownLayer.Selection;
            }
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (this.result == null)
                return;

            BackgroundGeometryBuilder builder = new BackgroundGeometryBuilder();

            builder.CornerRadius = 1;
            builder.AlignToMiddleOfPixels = true;

            builder.AddSegment(textView, new TextSegment() { StartOffset = result.OpeningBracketOffset, Length = result.OpeningBracketLength });
            builder.CloseFigure(); // prevent connecting the two segments
            builder.AddSegment(textView, new TextSegment() { StartOffset = result.ClosingBracketOffset, Length = result.ClosingBracketLength });

            Geometry geometry = builder.CreateGeometry();
            if (geometry != null)
            {
                drawingContext.DrawGeometry(backgroundBrush, borderPen, geometry);
            }
        }

        public BracketSearchResult result { get; set; }
    }
}