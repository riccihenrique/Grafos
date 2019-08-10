using System.Drawing;

namespace Grafos
{
    class Aresta
    {
        Vertice vertice;
        string value;

        public Aresta(Vertice vertice, string value)
        {
            this.Vertice = vertice;
            this.value = value;
        }

        public Vertice Vertice { get => vertice; set => vertice = value; }
        public string Value { get => value; set => this.value = value; }
    }
}
