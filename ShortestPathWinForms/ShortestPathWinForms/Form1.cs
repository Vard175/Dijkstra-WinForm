using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortestPathWinForms
{
    public partial class Form1 : Form
    {
        static int nodesCount;
        static int source;
        static int target;
        static bool isGenerateClicked = false;
        static Point[] nodesCircle;
        static Point[] nodesSquare;
        static int squareWidth ;

        MinHeap<int, int> heap = new MinHeap<int, int>();
        Dictionary<int, int> path = new Dictionary<int, int>();
        Dictionary<int, int> dist = new Dictionary<int, int>();
        HashSet<int> set = new HashSet<int>();

        public Form1()
        {
            InitializeComponent();
            squareWidth = 20;

            string[] lines = System.IO.File.ReadAllLines(@"D:\Projects\Master\DijkstraAlgorithm\input.txt");

            #region Graph input 

            int count = 0;
             //vertices
            int edgesCount; //edges

            while (!Int32.TryParse(lines[count], out nodesCount))
            {
                count++;
            }
            count++;

            while (!Int32.TryParse(lines[count], out edgesCount))
            {
                count++;
            }
            count++;

            while (!Int32.TryParse(lines[count], out source))
            {
                count++;
            }
            count++;

            while (!Int32.TryParse(lines[count], out target))
            {
                count++;
            }
            count++;

            AdjacencyList adj = new AdjacencyList(nodesCount);

            for (int i = count; i < lines.Length; ++i)
            {
                String[] input = lines[i].Split(' ');

                if (input.Length != 3)
                {
                    continue;
                }
                if (!Int32.TryParse(input[0], out int startVertex))
                {
                    continue;
                }
                if (!Int32.TryParse(input[1], out int endVertex))
                {
                    continue;
                }
                if (!Int32.TryParse(input[2], out int weight))
                {
                    continue;
                }
                adj.AddEdge(startVertex, endVertex, weight);

                set.Add(startVertex);
                set.Add(endVertex);
            }
            #endregion

            #region initialization
            foreach (var i in set)
            {
                path.Add(i, -1);
                heap.Add(i, int.MaxValue);
            }

            heap.ChangeValue(source, 0);
            #endregion

            #region Dijkstra Logic
            while (!heap.Empty())
            {
                var u = heap.Peek();
                if (u.Key == target)
                    break;
                dist[u.Key] = u.Value;
                var list = adj.GetAdjacences(u.Key);

                foreach (var v in list)
                {
                    int w = adj.GetWeight(u.Key, v.Key);
                    Relax(heap, path, u.Key, v.Key, w);
                }
                heap.RemoveMin();
            }
            #endregion

            #region Printing results
            // TODO Invalidate
            #endregion
        }

        public static void Relax(MinHeap<int, int> heap, Dictionary<int, int> path, int u, int v, int w)
        {
            if (heap.TryGetValue(v, out int vWeight))
            {
                heap.TryGetValue(u, out int uWeight);
                if (vWeight > uWeight + w)
                {
                    heap.ChangeValue(v, uWeight + w);
                    path[v] = u;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            //  PrintPath(path, source, target);
            
            #region Visual staff

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Graphics g = e.Graphics;
            SolidBrush nodeBrushBefore = new SolidBrush(Color.Sienna);
            Pen edgePenBefore = new Pen(Brushes.LightSlateGray, 3);
            SolidBrush nodeBrushAfter = new SolidBrush(Color.Crimson);
            Pen edgePenAfter = new Pen(Brushes.Red, 3);
            
            System.Drawing.Drawing2D.AdjustableArrowCap bigArrow = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
            edgePenBefore.CustomStartCap = bigArrow;
            edgePenAfter.CustomStartCap=bigArrow;
            
            int angle = 360/nodesCount;
            int currentAngle = angle;
            int radius = 200;
            
            Random random = new Random();
            Point center = new Point(Width / 2, Height / 2);
            nodesCircle= new Point[nodesCount];
            nodesSquare= new Point[nodesCount];

            #region nodes on circle
            for (int i = 0; i < nodesSquare.Length; i++)
            {
                int a = (int)(radius * Math.Cos(currentAngle * 2 * Math.PI / 360));
                int b = (int)(radius * Math.Sin(currentAngle * 2 * Math.PI / 360));
                nodesSquare[i] = new Point(center.X + a, center.Y - b);
                nodesCircle[i] = new Point(nodesSquare[i].X + squareWidth / 2, nodesSquare[i].Y + squareWidth / 2);
            
                g.FillEllipse(nodeBrushBefore, nodesSquare[i].X,nodesSquare[i].Y, squareWidth, squareWidth);
                currentAngle += angle;

                createLabel(e, $"{i}", Color.DarkRed, nodesCircle[i]);
            }

            #endregion

            // TODO անցնել հարևաններով ու նկարել կողերն ու կշիռները
            if (isGenerateClicked)
            {
               // if (source == vertex)

        //    {

                    //        Console.Write($" {source}");
                    //        return;
                    //    }
                    //    else if (parent[vertex] == -1)
                    //    {
                    //        Console.WriteLine($"There is no path from {source} to {vertex}");
                    //        return;
                    //    }
                    //    else PrintPath(e, parent, source, parent[vertex]);
                    //    Console.Write($" -> {vertex} ");
            }


            #endregion

            drawEdge(e, edgePenBefore, nodesCircle[3], nodesCircle[2],"jj");
            drawEdge(e, edgePenBefore, nodesCircle[2], nodesCircle[3], "a");            
            drawEdge(e, edgePenBefore, nodesCircle[2], nodesCircle[1], "a");
        }
   
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO my screen size
            Width = 2400;
            Height = 1600;

            
        }

        public static void PrintPath(PaintEventArgs e, Brush brush, Pen pen, Dictionary<int, int> parent,int source,int target)
        {
            Graphics g = e.Graphics;
            if (source == target)
            {
                g.FillEllipse(brush, nodesSquare[source].X, nodesSquare[source].Y, squareWidth, squareWidth);
                return;
            }

            else if (parent[target] == -1)
            {
                MessageBox.Show($"There is no path from {source} to {target}");
                return;
            }
            else PrintPath(e, brush, pen, parent, source, parent[target]);
            g.FillEllipse(brush, nodesSquare[target].X, nodesSquare[target].Y, squareWidth, squareWidth);
            drawEdge(e, pen, nodesCircle[], nodesCircle[2], "jj");

        }

        public void createLabel(PaintEventArgs e, String text, Color color, Point location)
        {
            Label mylab = new Label();
            mylab.Text = text;
            mylab.Location = location;
            mylab.AutoSize = true;
            mylab.Font = new Font("Calibri", 18);
            mylab.ForeColor = color;
            mylab.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(mylab);
        }

        public static void drawEdge(PaintEventArgs e, Pen pen, Point start, Point end, String weight)
        {
            #region edge from start to end
            int x1 = start.X;
            int y1 = start.Y;
            int x2 = end.X;
            int y2 = end.Y;

            var c = 0.2 * Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

            var signX = x1 == x2
                ? Math.Sign(y2 - y1)
                : Math.Sign(x2 - x1);

            var signY = y1 == y2
                ? Math.Sign(x2 - x1)
                : Math.Sign(y2 - y1);

            double angle = y1 == y2
                ? Math.Atan(Math.Abs(x2 - x1))
                : Math.Atan(Math.Abs(x2 - x1) / Math.Abs(y2 - y1));

            Point controlPoint = new Point();
            controlPoint.X = (int)((x1 + x2) / 2 - signX * c * Math.Cos(angle));
            controlPoint.Y = (int)((y1 + y2) / 2 + signY * c * Math.Sin(angle));

            e.Graphics.DrawBezier(pen, start, controlPoint, controlPoint, end);
            #endregion

            createLabel(e, weight, Color.Green, controlPoint);
        }

        private void buttonGeneratePath_Click(object sender, EventArgs e)
        {
            isGenerateClicked = true;
            Invalidate();
        }

        private void buttonShowDistance_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"distance of {target} from {source} is {dist[target]} ");
        }
    }
}