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
                                    int qntLista = verificaLista(arestas[i1], vertices[idx2].Label.Text);
                                    if (qntLista != 2) //Impede adicionar mais de 2 arestas paralelas
                                    {
                                        int dx, dy;
                                        dx = vertices[idx2].Location.X - vertices[idx1].Location.X;
                                        dy = vertices[idx2].Location.Y - vertices[idx1].Location.Y;
                                        
                                        if (dx < dy)
                                        {
                                            dx = -7;
                                            dy = 7;
                                        }
                                        else
                                        {
                                            dx = 7;
                                            dy = -7;
                                        }

                                        // Pontos de controle para apagar as arestas posteriormente
                                        Point p1, p2;
                                        if (qntLista == 0)
                                        {
                                            p1 = new Point(vertices[idx1].Location.X + dx, vertices[idx1].Location.Y + dy);
                                            p2 = new Point(vertices[idx2].Location.X + dx, vertices[idx2].Location.Y + dy);
                                            tb.Location = new Point(Math.Abs((vertices[idx1].Location.X - vertices[idx2].Location.X)) / 2 + Math.Min(vertices[idx1].Location.X, vertices[idx2].Location.X) + dx - tb.Size.Width,
                                                        Math.Abs((vertices[idx1].Location.Y - vertices[idx2].Location.Y)) / 2 + Math.Min(vertices[idx1].Location.Y, vertices[idx2].Location.Y) + dy - tb.Size.Height);
                                        }
                                        else
                                        {
                                            ma = null;
                                            p1 = new Point(vertices[idx1].Location.X - dx, vertices[idx1].Location.Y - dy);
                                            p2 = new Point(vertices[idx2].Location.X - dx, vertices[idx2].Location.Y - dy);
                                            tb.Location = new Point(Math.Abs((vertices[idx1].Location.X - vertices[idx2].Location.X)) / 2 + Math.Min(vertices[idx1].Location.X, vertices[idx2].Location.X) - dx,
                                                        Math.Abs((vertices[idx1].Location.Y - vertices[idx2].Location.Y)) / 2 + Math.Min(vertices[idx1].Location.Y, vertices[idx2].Location.Y) - dy);
                                        }

                                        g.DrawLine(p, p1, p2);
                                        arestas[i1].Add(new Aresta(vertices[idx2], tb, p1, p2));
                                        if (ma != null)
                                            ma[i1, i2] = 1;

                                        if (!ckbDigrafo.Checked)
                                        {
                                            arestas[i2].Add(new Aresta(vertices[idx1], tb, p1, p2));
                                            if(ma != null)
                                                ma[i2, i1] = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    if (verificaLista(arestas[i1], vertices[idx2].Label.Text) != 2)
                                    {
                                        g.DrawEllipse(p, new Rectangle(vertices[idx1].Location.X - 55, vertices[idx1].Location.Y, 40, 40));
                                        arestas[i1].Add(new Aresta(vertices[idx2], tb));
                                        if(ma != null)
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

                if (!ckbDigrafo.Checked)
                {
                    //Deleta as arestas
                    for (int i = 0, qtd = arestas[id1].Count; i < qtd; i++)
                    {
                        if (arestas[id1][i].Vertice == vertices[idxSel])
                            g.DrawEllipse(p, new Rectangle(vertices[idxSel].Location.X - 55, vertices[idxSel].Location.Y, 40, 40));
                        else
                            g.DrawLine(p, arestas[id1][i].P1, arestas[id1][i].P2);

                        Controls.Remove(arestas[id1][i].Value); //Remove TB da tela

                        id2 = char.Parse(arestas[id1][i].Vertice.Label.Text) - 65;
                        //Corrige lista
                        int j = 0;
                        while (j < arestas[id2].Count && !arestas[id2][j].Vertice.Label.Equals(vertices[idxSel].Label))
                            j++;
                        if (j < arestas[id2].Count)
                            arestas[id2].RemoveAt(j);

                        //Corrige MA e MI
                        if (ma != null)
                        {
                            ma[id1, id2] = 0;
                            ma[id2, id1] = 0;
                        }
                    }

                    Controls.Remove(vertices[idxSel].Label);
                    arestas[id1] = null;
                    vertices.RemoveAt(idxSel);

                    idxSel = -1;
                }
                else //Digrafo
                {

                }
                
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
            if(ma != null)
            {
                String S = "    | ";
                for (int i = 0; i < MAXLEN; i++)
                    if (arestas[i] != null)
                        S += "  " + rotulos[i];

                lbxMA.Items.Add(S);
                S = "------";
                for (int i = 0; i < MAXLEN; i++)
                    if (arestas[i] != null)
                        S += "----";

                lbxMA.Items.Add(S);
                for (int i = 0; i < MAXLEN; i++)
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
        }

        private void showMI()
        {
            lbxMI.Items.Clear();
            miCol = 0;
            mi = new int[MAXLEN, MAXLEN * (MAXLEN - 1) / 2];
            for (int i = 0; i < MAXLEN; i++)
                if(arestas[i] != null)
                    foreach(Aresta a in arestas[i])
                        if(verificaMi(rotulos[i].ToString(), rotulos[a.Vertice.Label.Text[0] - 65].ToString()))
                        {
                            milabel[miCol] = "(" + rotulos[i].ToString() + "," + rotulos[a.Vertice.Label.Text[0] - 65].ToString() + ")";
                            int o = 1;
                            int.TryParse(a.Value.Text, out o);
                            mi[i, miCol] = o == 0 ? 1 : o;
                            mi[a.Vertice.Label.Text[0] - 65, miCol++] = o == 0 ? 1 : o; 
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

        private int verificaLista(List<Aresta> la, string label1)
        {
            int i = 0, cont = 0;
            string s;

            while (i < la.Count && cont < 2)
            {
                if (la[i].Vertice.Label.Text.Equals(label1))
                    cont++;
                i++;
            }

            return cont;
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

            if(ma != null)
                ma[i1, i2] = num;

            if(!ckbDigrafo.Checked && ma != null)
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

        private void LbxMA_MouseClick(object sender, MouseEventArgs e)
        {
            string s;
            int i;
            if(ma != null)
            {
                for (i = 0; i < MAXLEN; i++)
                    if (arestas[i] != null)
                        if (ma[i, i] != 0)
                            break;

                if (i == MAXLEN)
                {
                    int contant = 0, cont = 0;
                    int i2 = 0;
                    while (arestas[i2] == null)
                        i2++;

                    for (i = 0; i < MAXLEN && contant == cont; i++)
                        if(arestas[i] != null)
                        {
                            cont = 0;
                            for (int j = 0; j < MAXLEN; j++)
                                if (arestas[j] != null && ma[i, j] != 0)
                                {
                                    if (i == i2)
                                        contant++;
                                    cont++;
                                }
                        }

                    if (cont == contant)
                    {
                        if (contant == vertices.Count - 1)
                            s = "Grafo: " + (vertices.Count - 1) + "v";
                        else
                            s = "Grafo: " + contant + " - Regular";
                    }
                    else
                        s = "Grafo: Simples";
                }
                else
                    s = "Sem Classificação";

                MessageBox.Show(s);
            }
        }

        private void LbxMI_MouseClick(object sender, MouseEventArgs e)
        {
            int i, cont;
            string s;
            for (i = 0; i < miCol; i++) // Procura ciclo
            {
                cont = 0;
                for (int j = 0; j < MAXLEN; j++)
                    if (arestas[j] != null && mi[j, i] != 0)
                        cont++;

                if (cont == 1)
                    break;
            }

            if (i == miCol)
                for (i = 0; i < miCol; i++)
                    for (int j = i + 1; j < miCol; j++)
                        if (milabel[i].Equals(milabel[j]))
                            break;

            if (i == miCol)
            {
                int i2 = 0, qtdAnt = 0;
                while (i2 < MAXLEN && arestas[i2] == null)
                    i2++;

                
                for (i = 0; i < miCol; i++)
                    if (mi[i2, i] != 0)
                        qtdAnt++;

                for(i = i2 + 1; i < MAXLEN; i++)
                {
                    cont = 0;
                    if (arestas[i] != null)
                    {
                        for (int j = 0; j < miCol; j++)
                            if (mi[i, j] != 0)
                                cont++;
                        if (qtdAnt != cont)
                            break;
                    }
                }

                if (i == MAXLEN)
                {
                    if (qtdAnt == vertices.Count - 1)
                        s = "Grafo: " + qtdAnt + "v";
                    else
                        s = "Grafo: " + qtdAnt + " - Regular";
                }
                else
                    s = "Grafo: Simples";
            }
            else
                s = "Sem Classificação";
            MessageBox.Show(s);
        }

        private void LbxLista_MouseClick(object sender, MouseEventArgs e)
        {
            bool flag = false;
            string s;
            for(int i = 0; i < MAXLEN && !flag; i++)
            {
                if(arestas[i] != null)
                    foreach (Aresta a in arestas[i])
                        if (a.Vertice.Label.Text.Equals(rotulos[i].ToString()))
                            flag = true;
            }
            if (!flag)
            {
                int qtdAnt, i = 0;
                while (arestas[i] == null)
                    i++;
                qtdAnt = arestas[i].Count;

                for (i = 1; i < MAXLEN; i++)
                    if (arestas[i] != null)
                        if (arestas[i].Count != qtdAnt)
                            break;

                if (i == MAXLEN)
                {
                    if (qtdAnt == vertices.Count - 1)
                        s = "Grafo: " + (vertices.Count - 1) + "v";
                    else
                        s = "Grafo: " + qtdAnt + " - Regular";
                }
                else
                    s = "Grafo: Simples";
            }

            else
                s = "Sem Classificação";

            MessageBox.Show(s);
        }

        private bool verificaMi(string a, string b)
        {
            int i;
            string[] l;

            for(i = 0; i < miCol; i++)
            {
                l = milabel[i].Replace("(", "").Replace(")", "").Split(',');
                if (l[0].Equals(b) && l[1].Equals(a))
                    break;
            }
            return i == miCol;
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

            if (vertices.Count == 0)
                ckbDigrafo.Enabled = true;
            else
                ckbDigrafo.Enabled = false;

            lbTot.Text = vertices.Count + "";
        }
    }
}
