using System.Drawing;
using System.Windows.Forms;

namespace Grafos
{
    class Vertice
    {
        private Point location;
        private Label label;

        public Vertice(Point location, Label label)
        {
            Location = location;
            Label = label;
        }

        public Point Location { get => location; set => location = value; }
        public Label Label { get => label; set => label = value; }
    }
}
