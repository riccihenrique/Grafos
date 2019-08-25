using System.Drawing;
using System.Windows.Forms;

namespace Grafos
{
    class Aresta
    {
        Vertice vertice;
        TextBox value;

        public Aresta(Vertice vertice, TextBox value)
        {
            this.Vertice = vertice;
            this.value = value;
        }

        public Vertice Vertice { get => vertice; set => vertice = value; }
        public TextBox Value { get => value; set => this.value = value; }
    }
}
