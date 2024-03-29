﻿using System.Drawing;
using System.Windows.Forms;

namespace Grafos
{
    class Aresta
    {
        Vertice vertice;
        TextBox value;
        Point p1, p2;

        public Aresta(Vertice vertice, TextBox value, Point p1, Point p2) : this(vertice, value)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public Aresta(Vertice vertice, TextBox value)
        {
            this.Vertice = vertice;
            this.value = value;
        }

        public Vertice Vertice { get => vertice; set => vertice = value; }
        public TextBox Value { get => value; set => this.value = value; }
        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
    }
}
