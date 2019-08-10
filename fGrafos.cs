using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Grafos
{    
    public partial class fGrafos : Form
    {
        private const int MAXLEN = 10;
        List<Vertice> vertices = new List<Vertice>();
        List<Aresta>[] arestas = new List<Aresta>[MAXLEN];
        List<Label> labels = new List<Label>();
        int[,] ma = new int[MAXLEN, MAXLEN];
        int[,] mi = new int[MAXLEN, MAXLEN * (MAXLEN - 1) / 2];
        char[] rotulos = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        string[] milabel = new string[MAXLEN * (MAXLEN - 1) / 2];

        int idx1 = -1, idx2 = -1;
        int tl = 0;
        int miCol = 0;
        int cont = 0; // Auxilia na seleção dos pontos

        public fGrafos()
        {
            InitializeComponent();
            for (int i = 0; i < MAXLEN; i++)
                arestas[i] = new List<Aresta>();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            Pen p = new Pen(Color.Black);
            if (e.Location.Y < groupBox1.Location.Y)
                if (e.Button == MouseButtons.Left && e.Y < groupBox1.Location.Y - 20 && e.Y > 20 && e.X > 20 && e.X < this.Width - 20)
                {
                    if (tl < MAXLEN)
                    {
                        if (find(e.Location) == vertices.Count)
                        {
                            Label l = new Label();
                            l.Location = new Point(e.Location.X - 7, e.Location.Y - 5);
                            l.Name = "" + rotulos[vertices.Count];
                            l.Size = new Size(13, 13);
                            l.Text = "" + rotulos[vertices.Count];
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
                            TextBox tb = new TextBox();
                            tb.BackColor = Color.FromArgb(255, 240, 240, 240);
                            tb.TextChanged += new EventHandler(lost_Focus);
                            tb.Size = new Size(15, 13);

                            if (cont % 2 == 0)
                                idx1 = f;
                            else
                            {
                                idx2 = f;
                                tb.Location = new Point(Math.Abs((vertices[idx1].Location.X - vertices[idx2].Location.X)) / 2 + Math.Min(vertices[idx1].Location.X, vertices[idx2].Location.X),
                                                        Math.Abs((vertices[idx1].Location.Y - vertices[idx2].Location.Y)) / 2 + Math.Min(vertices[idx1].Location.Y, vertices[idx2].Location.Y));
                                tb.Name = idx1 + "" + idx2 ;

                                if (idx1 != idx2)
                                {           
                                    if(!verificaLista(arestas[idx1], vertices[idx2].Label))
                                    {
                                        
                                        g.DrawLine(p, vertices[idx1].Location, vertices[idx2].Location);
                                        arestas[idx1].Add(new Aresta(vertices[idx2], tb));
                                        arestas[idx2].Add(new Aresta(vertices[idx1], tb));

                                        ma[idx1, idx2] = tb.Text == "" ? 1 : int.Parse(tb.Text);
                                        ma[idx2, idx1] = tb.Text == "" ? 1 : int.Parse(tb.Text);

                                        milabel[miCol] = "(" + vertices[idx1].Label + "," + vertices[idx2].Label + ")";
                                        mi[idx1, miCol] = tb.Text == "" ? 1 : int.Parse(tb.Text);
                                        mi[idx2, miCol++] = tb.Text == "" ? 1 : int.Parse(tb.Text);
                                    }
                                }
                                else
                                {
                                    if(!verificaLista(arestas[idx1], vertices[idx2].Label))
                                    {
                                        g.DrawEllipse(p, new Rectangle(vertices[idx1].Location.X - 55, vertices[idx1].Location.Y, 40, 40));
                                        arestas[idx1].Add(new Aresta(vertices[idx2], tb));
                                        ma[idx1, idx1] = tb.Text == "" ? 1 : int.Parse(tb.Text);

                                        milabel[miCol] = "(" + vertices[idx1].Label + "," + vertices[idx2].Label + ")";
                                        mi[idx1, miCol++] = tb.Text == "" ? 1 : int.Parse(tb.Text);
                                    }
                                }

                                Controls.Add(tb);
                                idx1 = idx2 = -1;
                            }
                            cont++;
                        }
                        else
                            cont = 0;
                    }
                }
            showList();
            showMA();
            showMI();
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
        
        private void showList()
        {
            lbxLista.Items.Clear();
            String S;
            for (int i = 0; i < tl; i++)
            {
                S = rotulos[i] + " | ";
                foreach (Aresta aux in arestas[i])
                    S += " -> (" + aux.Vertice.Label + "," + aux.Value.Text + ")" ;
                lbxLista.Items.Add(S);
            }            
        }

        private void showMA()
        {
            lbxMA.Items.Clear();
            String S = "    | ";
            for (int i = 0; i < tl; i++)
                S += "  " + rotulos[i];
            lbxMA.Items.Add(S);
            S = "------";
            for (int i = 0; i < tl; i++)
                S += "----";

            lbxMA.Items.Add(S);
            for(int i = 0; i < tl; i++)
            {
                S = rotulos[i] + "  | ";
                for(int  j = 0; j < tl; j++)
                    S += "  " + ma[i, j];
                lbxMA.Items.Add(S);
            }
        }

        private void showMI()
        {
            lbxMI.Items.Clear();

            String S = "    | ";

            for (int i = 0; i < miCol; i++)
                S += " " + milabel[i];

            lbxMI.Items.Add(S);

            S = "------";
            for (int i = 0; i < miCol; i++)
                S += "--------";

            lbxMI.Items.Add(S);

            for (int i = 0; i < tl; i++)
            {
                S = rotulos[i] + "  | ";
                for (int j = 0; j < miCol; j++)
                    S += "   " + mi[i, j] + "    ";
                lbxMI.Items.Add(S);
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

        private void lost_Focus(object sender, EventArgs e)
        {            
            int i1 = 0, i2 = 0, num, i;
            try
            {
                num = int.Parse(((TextBox)sender).Name);
                i1 = num / 10;
                i2 = num % 10;

                i = 0;
                while (i < arestas[i1].Count && !arestas[i1][i].Vertice.Label.Equals("" + rotulos[i2]))
                    i++;

                num = int.Parse(arestas[i1][i].Value.Text);
            }
            catch(Exception ex)
            {
                num = 1;
            }

            ma[i1, i2] = num;
            ma[i2, i1] = num;


            for(i = 0; i < miCol; i++)
                if (milabel[i].Replace("(", "").Replace(")", "").Equals(rotulos[i1] + "," + rotulos[i2]) ||
                    milabel[i].Replace("(", "").Replace(")", "").Equals(rotulos[i2] + "," + rotulos[i1]))
                    break;

            mi[i1, i] = num;
            mi[i2, i] = num;

            showList();
            showMA();
            showMI();
        }
    }    
}
