using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Grafos
{    
    public partial class fGrafos : Form
    {
        private const int MAXLEN = 10;
        private char[] rotulos = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        private string[] milabel = new string[MAXLEN * (MAXLEN - 1) / 2];

        private List<Vertice> vertices = new List<Vertice>();
        private List<Aresta>[] arestas = new List<Aresta>[MAXLEN];

        private int[,] ma = new int[MAXLEN, MAXLEN];
        private int[,] mi = new int[MAXLEN, MAXLEN * (MAXLEN - 1) / 2];

        private int idx1 = -1, idx2 = -1;
        private int idxSel = -1;
        private int miCol = 0; // Index de coluna da MI
        private int cont = 0; // Auxilia na seleção dos pontos

        public fGrafos()
        {
            InitializeComponent();
            atualizaTudo();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            Pen p = new Pen(Color.Black);
            int pos, i1, i2;
            if (e.Location.Y < groupBox1.Location.Y)
                if (e.Button == MouseButtons.Left && e.Y < groupBox1.Location.Y - 20 && 
                    e.Y > 20 && e.X > 20 && e.X < this.Width - 20) //Botão esquerdo cria novos vértices e seleciona-os para exclusão
                {
                    pos = find(e.Location);
                    if (pos == vertices.Count)
                    {
                        if (vertices.Count < MAXLEN)
                        {
                            int i = nextLabel();
                            Label l = new Label();
                            l.Location = new Point(e.Location.X - 7, e.Location.Y - 5);
                            l.Name = "" + rotulos[i];
                            l.Size = new Size(13, 13);
                            l.Text = l.Name;
                            l.MouseClick += new MouseEventHandler(label_mouseClicl);

                            Controls.Add(l);
                            vertices.Add(new Vertice(e.Location, l));

                            arestas[i] = new List<Aresta>();
                            g.DrawEllipse(p, new Rectangle(e.X - 20, e.Y - 20, 40, 40));

                            //Controla a seleção
                            idx1 = -1;
                            cont = 0;
                        }
                        else
                            MessageBox.Show("Você não pode utilizar mais de 10 vértices!!", "Atenção");
                    }
                    else // Deleção
                        idxSel = pos;
                }
                else
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

                    if (e.Button == MouseButtons.Right) // Botão direito faz a ligaçõa das arestas
                    {
                        if (f < vertices.Count)
                        {
                            if (cont % 2 == 0)
                                idx1 = f;
                            else
                            {
                                idx2 = f;

                                TextBox tb = new TextBox(); //TB que guarda os valores das arestas
                                tb.BackColor = Color.FromArgb(255, 240, 240, 240);
                                tb.TextChanged += new EventHandler(lost_Focus);
                                tb.Size = new Size(15, 13);
                                tb.Location = new Point(Math.Abs((vertices[idx1].Location.X - vertices[idx2].Location.X)) / 2 + Math.Min(vertices[idx1].Location.X, vertices[idx2].Location.X),
                                                        Math.Abs((vertices[idx1].Location.Y - vertices[idx2].Location.Y)) / 2 + Math.Min(vertices[idx1].Location.Y, vertices[idx2].Location.Y));

                                //Ids do vetor de arestas. Idx são os indices da lista de vertices
                                i1 = char.Parse(vertices[idx1].Label.Text) - 65;
                                i2 = char.Parse(vertices[idx2].Label.Text) - 65;

                                tb.Name = i1 + "" + i2;

                                if (idx1 != idx2) 
                                {
                                    if (!verificaLista(arestas[i1], vertices[idx2].Label.Text)) //Impede dupla aresta
                                    {
                                        g.DrawLine(p, vertices[idx1].Location, vertices[idx2].Location);
                                        arestas[i1].Add(new Aresta(vertices[idx2], tb));
                                        arestas[i2].Add(new Aresta(vertices[idx1], tb));

                                        ma[i1, i2] = 1;
                                        ma[i2, i1] = 1;
                                    }
                                }
                                else
                                {
                                    if(!verificaLista(arestas[i1], vertices[idx2].Label.Text))
                                    {
                                        g.DrawEllipse(p, new Rectangle(vertices[idx1].Location.X - 55, vertices[idx1].Location.Y, 40, 40));
                                        arestas[i1].Add(new Aresta(vertices[idx2], tb));
                                        ma[i1, i1] = 1;
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
                    else
                        idxSel = f;
                }

            atualizaTudo();
        }

        private int find(Point p) //Encontra o index na lista de vertices
        {
            int i = 0;
            while (i < vertices.Count && Math.Sqrt(Math.Pow(p.X - vertices[i].Location.X, 2) + 
                Math.Pow(p.Y - vertices[i].Location.Y, 2)) > 20)
                i++;

            return i;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) //Identifica a tecla para deleção
        {
            if (e.KeyChar.ToString().ToUpper().Equals("D") && idxSel != -1)
            {
                int id1, id2;
                Graphics g = CreateGraphics();
                Pen p = new Pen(Color.FromArgb(240, 240, 240));

                // Delete o vértice
                g.DrawEllipse(p, new Rectangle(vertices[idxSel].Location.X - 20, vertices[idxSel].Location.Y - 20, 40, 40));

                id1 = char.Parse(vertices[idxSel].Label.Text) - 65;
                
                //Deleta as arestas
                for (int i = 0, qtd = arestas[id1].Count; i < qtd; i++)
                {
                    if (arestas[id1][i].Vertice == vertices[idxSel])
                        g.DrawEllipse(p, new Rectangle(vertices[idxSel].Location.X - 55, vertices[idxSel].Location.Y, 40, 40));
                    else
                        g.DrawLine(p, arestas[id1][i].Vertice.Location, vertices[idxSel].Location);

                    id2 = char.Parse(arestas[id1][i].Vertice.Label.Text) - 65;

                    Controls.Remove(arestas[id1][i].Value); //Remove TB da tela

                    //Corrige lista
                    int j = 0;
                    while (j < arestas[id2].Count && !arestas[id2][j].Vertice.Label.Equals(vertices[idxSel].Label))
                        j++;
                    if(j < arestas[id2].Count)
                        arestas[id2].RemoveAt(j);

                    //Corrige MA e MI
                    ma[id1, id2] = 0;
                    ma[id2, id1] = 0;
                }
                
                Controls.Remove(vertices[idxSel].Label);
                arestas[id1] = null;
                vertices.RemoveAt(idxSel);

                idxSel = -1;
                atualizaTudo();
            }
        }

        private void label_mouseClicl(object sender, MouseEventArgs e) //Click no label do vértice. Mesmo efeito de clicar no vertice em si
        {
            Form1_MouseClick(sender, e);
        }
        
        private void showList()
        {
            lbxLista.Items.Clear();
            String S;
            for (int i = 0; i < MAXLEN; i++)
                if(arestas[i] != null)
                {
                    S = rotulos[i] + " | ";
                    foreach (Aresta aux in arestas[i])
                        S += " -> (" + aux.Vertice.Label.Text + "," + aux.Value.Text + ")";
                    lbxLista.Items.Add(S);
                }         
        }

        private void showMA()
        {
            lbxMA.Items.Clear();
            String S = "    | ";
            for (int i = 0; i < MAXLEN; i++)
                if(arestas[i] != null)
                    S += "  " + rotulos[i];

            lbxMA.Items.Add(S);
            S = "------";
            for (int i = 0; i < MAXLEN; i++)
                if (arestas[i] != null)
                    S += "----";

            lbxMA.Items.Add(S);
            for(int i = 0; i < MAXLEN; i++)
            {
                if (arestas[i] != null)
                {
                    S = rotulos[i] + "  | ";
                    for (int j = 0; j < MAXLEN; j++)
                        if (arestas[j] != null)
                            S += "  " + ma[i, j];
                    lbxMA.Items.Add(S);
                }
            }
        }

        private void showMI()
        {
            lbxMI.Items.Clear();

            //Criada a partir da MI
            miCol = 0;
            mi = new int[MAXLEN, MAXLEN * (MAXLEN - 1) / 2];
            for (int i = 0, j = 0; i < MAXLEN; i++, j++)
                if(arestas[i] != null)
                {
                    for (int k = j; k < MAXLEN; k++)
                        if (arestas[k] != null && ma[i, k] != 0)
                        {
                            milabel[miCol] = "(" + rotulos[i].ToString() + "," + rotulos[k].ToString() + ")";
                            mi[i, miCol] = ma[i, k];
                            mi[k, miCol++] = ma[i, k];
                        }
                }

            String S = "    | ";
            for (int i = 0; i < miCol; i++)
                S += " " + milabel[i];

            lbxMI.Items.Add(S);

            S = "------";
            for (int i = 0; i < miCol; i++)
                S += "--------";

            lbxMI.Items.Add(S);

            for (int i = 0; i < MAXLEN; i++)
                if(arestas[i] != null)
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

        private void lost_Focus(object sender, EventArgs e) //Altera os valores quando o foco da TB de valor é alterado
        {            
            int i1 = 0, i2 = 0, num, i;
            try
            {
                num = int.Parse(((TextBox)sender).Name);
                i1 = num / 10;
                i2 = num % 10;

                i = 0;
                while (i < arestas[i1].Count && !arestas[i1][i].Vertice.Label.Text.Equals("" + rotulos[i2]))
                    i++;

                num = int.Parse(arestas[i1][i].Value.Text);
            }
            catch(Exception ex)
            {
                num = 1;
            }

            ma[i1, i2] = num;
            ma[i2, i1] = num;

            atualizaTudo();
        }

        private int nextLabel() //Identifica o label do proximo vertice a ser adicionado
        {
            int i = 0, j = 0;
            bool flag = false;

            while(i < MAXLEN && !flag)
            {
                j = 0;
                while (j < vertices.Count && !vertices[j].Label.Text.Equals(rotulos[i].ToString()))
                    j++;

                if (j == vertices.Count)
                    flag = true;
                else
                    i++;
            }

            return i;
        }

        private void atualizaTudo()
        {
            showList();
            showMA();
            showMI();
            updateLabel();
        }

        private void updateLabel()
        {
            if (idx1 != -1)
                lbSel.Text = vertices[idx1].Label.Text;
            else
                lbSel.Text = "Nenhum";

            if (idxSel != -1)
                lbEx.Text = vertices[idxSel].Label.Text;
            else
                lbEx.Text = "Nenhum";

            lbTot.Text = vertices.Count + "";
        }
    }
}
