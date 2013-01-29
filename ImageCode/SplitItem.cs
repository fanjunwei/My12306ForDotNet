using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageCode
{
    class SplitItem : IComparable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public SplitItem(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
        public int CompareTo(object obj)
        {
            SplitItem oo = (SplitItem)obj;
            return Width.CompareTo(oo.Width);
        }

        public Rectangle toRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
    }
}
