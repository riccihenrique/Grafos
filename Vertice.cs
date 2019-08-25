using System.Drawing;
using System.Windows.Forms;

namespace Grafos
{
    class Vertice
    {
        private Point location;
        private Label label;
        int d1, d2;

        public Vertice(Point location, Label label)
        {
            Location = location;
            Label = label;
            this.d1 = this.d2 = 0;
        }

        public Point Location { get => location; set => location = value; }
        public Label Label { get => label; set => label = value; }
        public int D1 { get => d1; set => d1 = value; }
        public int D2 { get => d2; set => d2 = value; }
    }
}
