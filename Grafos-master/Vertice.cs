using System.Drawing;

namespace Grafos
{
    class Vertice
    {
        private Point location;
        private string label;

        public Vertice(Point location, string label)
        {
            Location = location;
            Label = label;
        }

        public Point Location { get => location; set => location = value; }
        public string Label { get => label; set => label = value; }
    }
}
