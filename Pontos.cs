using System.Drawing;

namespace Grafos
{
    class Points
    {
        private Point point;
        private Points next;
        private string label;

        public Points(Point point, Points next, string label)
        {
            this.Point = point;
            this.Next = next;
            this.Label = label;
        }

        public Points(Point point, string label)
        {
            this.Point = point;
            this.Next = null;
            this.Label = label;
        }

        public Points()
        {
            this.Point = new Point();
            this.Next = null;
            this.Label = "";
        }

        public Point Point { get => point; set => point = value; }
        public Points Next { get => next; set => next = value; }
        public string Label { get => label; set => label = value; }
    }
}
