using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Grafos
{    
    public partial class Form1 : Form
    {
        private const int MAXLEN = 10;
        List<Vertice> vertices = new List<Vertice>();
        List<Aresta>[] arestas = new List<Aresta>[MAXLEN];
        List<Label> labels = new List<Label>();
        int[,] ma = new int[MAXLEN, MAXLEN];

        int idx1 = -1, idx2 = -1;
        int tl = 0;
        int cont = 0; // Auxilia na seleção dos pontos

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < MAXLEN; i++)
                arestas[i] = new List<Aresta>();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            Pen p = new Pen(Color.Black);
            if (e.Location.Y < groupBox1.Location.Y - 20 && e.Y > 20)
                if (e.Button == MouseButtons.Left)
                {
                    if (tl < MAXLEN)
                    {
                        if (find(e.Location) == vertices.Count)
                        {
                            Label l = new Label();
                            l.Location = new Point(e.Location.X - 7, e.Location.Y - 5);
                            l.Name = "" + vertices.Count;
                            l.Size = new Size(13, 13);
                            l.Text = "" + vertices.Count;
                            l.MouseClick += new MouseEventHandler(label_mouseClicl);
                            labels.Add(l);
                            Controls.Add(l);
                            vertices.Add(new Vertice(e.Location, l.Name));
                            g.DrawEllipse(p, new Rectangle(e.X - 20, e.Y - 20, 40, 40));
                            tl++;
                        }
                    }
                    else
                        MessageBox.Show("Você não pode utilizar mais de 10 vértices!!", "Atenção");
                }
                else
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        int f;
                        String s = sender.ToString().Split(',')[0]; // Sabe se o evento vem do click do label
                        if (s.Equals("System.Windows.Forms.Label"))
                        {
                            Label a = (Label)sender;
                            f = find(new Point(a.Location.X + e.X, a.Location.Y + e.Y));
                        }
                        else
                            f = find(e.Location);

                        if (f < vertices.Count)
                        {
                            if (cont % 2 == 0)
                                idx1 = f;
                            else
                            {
                                idx2 = f;
                                if(idx1 != idx2)
                                {           
                                    if(!verificaLista(arestas[idx1], vertices[idx2].Label))
                                    {
                                        g.DrawLine(p, vertices[idx1].Location, vertices[idx2].Location);
                                        arestas[idx1].Add(new Aresta(vertices[idx2], ""));
                                        arestas[idx2].Add(new Aresta(vertices[idx1], ""));

                                        ma[idx1, idx2] = "" == "" ? 1 : int.Parse("1");
                                        ma[idx2, idx1] = "" == "" ? 1 : int.Parse("1");
                                    }
                                }
                                else
                                {
                                    if(!verificaLista(arestas[idx1], vertices[idx2].Label))
                                    {
                                        g.DrawEllipse(p, new Rectangle(vertices[idx1].Location.X - 55, vertices[idx1].Location.Y, 40, 40));
                                        arestas[idx1].Add(new Aresta(vertices[idx2], ""));
                                        ma[idx1, idx1] = "" == "" ? 1 : int.Parse("1");
                                    }
                                }

                                showMA();

                                /*for (int i = 0; i < tl; i++)
                                    showList(arestas[i], i);*/

                                idx1 = idx2 = -1;
                            }
                            cont++;
                        }
                        else
                            cont = 0;
                    }
                }
        }

        private int find(Point p)
        {
            int i = 0;
            while (i < vertices.Count && Math.Sqrt(Math.Pow(p.X - vertices[i].Location.X, 2) + 
                Math.Pow(p.Y - vertices[i].Location.Y, 2)) > 20)
                i++;

            return i;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if (e.KeyChar == 127 && idx1 != -1)
            {
                Graphics g = CreateGraphics();
                Pen p = new Pen(Color.White);

                g.DrawEllipse(p, new Rectangle(points[idx1].X - 20, points[idx1].Y - 20, 40, 40));

                int idx = points.IndexOf(points[idx1]);
                points.RemoveAt(idx);
                Controls.Remove(labels[idx]);
                labels.RemoveAt(idx);
            }*/
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Delete && idx1 != -1)
            {
                Graphics g = CreateGraphics();
                Pen p = new Pen(Color.White);

                g.DrawEllipse(p, new Rectangle(points[idx1].X - 20, points[idx1].Y - 20, 40, 40));

                int idx = points.IndexOf(points[idx1]);
                points.RemoveAt(idx);
                Controls.Remove(labels[idx]);
                labels.RemoveAt(idx);
            }*/
        }

        private void label_mouseClicl(object sender, MouseEventArgs e)
        {
            Form1_MouseClick(sender, e);
        }
        
        private void showList(List<Aresta> a, int idx)
        {
            Console.Write(idx + ":");
            foreach (Aresta aux in a)
                Console.Write(" " + aux.Vertice.Label);

            Console.WriteLine("");
        }

        private void showMA()
        {
            for(int i = 0; i < tl; i++)
            {
                for(int  j = 0; j < tl; j++)
                    Console.Write(ma[i, j]);
                Console.WriteLine("");
            }
        }

        private bool verificaLista(List<Aresta> la, string label)
        {
            bool flag = false;

            int i = 0;
            while (i < la.Count && !flag)
            {
                if (la[i].Vertice.Label.Equals(label))
                    flag = true;

                i++;
            }
            return flag;
        }
    }    
}
