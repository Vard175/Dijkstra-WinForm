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
        public Form1()
        {
            InitializeComponent();
        }
        
        protected override void OnPaint(PaintEventArgs e)  // e-n eventi paramn a 
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            #region All I 
            Graphics g = e.Graphics;
            Pen nodePen = new Pen(Brushes.Sienna,3);
            SolidBrush nodeBrush = new SolidBrush(Color.Sienna);
           // Brushes nodeBrush = new Brushes(Color.Sienna);
            Pen edgePen = new Pen(Brushes.LightSlateGray, 3);

            System.Drawing.Drawing2D.AdjustableArrowCap bigArrow = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
            edgePen.CustomStartCap = bigArrow;

            int nodeCount = 5;
            int angle = 72;
            int currentAngle = angle;
            int radius = 200;
            int squareWidth = 20;
            Random random = new Random();
            Point center = new Point(Width / 2, Height / 2);
            Point[] nodesCircle = new Point[nodeCount];
            Point[] nodesSquare = new Point[nodeCount];

            #region nodes on circle
            for (int i = 0; i < nodesSquare.Length; i++)
            {
                int a = (int)(radius * Math.Cos(currentAngle * 2 * Math.PI / 360));
                int b = (int)(radius * Math.Sin(currentAngle * 2 * Math.PI / 360));
                nodesSquare[i] = new Point(center.X + a, center.Y - b);
                nodesCircle[i] = new Point(nodesSquare[i].X + squareWidth / 2, nodesSquare[i].Y + squareWidth / 2);
                 //g.DrawEllipse(nodePen, center.X + a, center.Y - b, squareWidth, squareWidth);
                g.FillEllipse(nodeBrush, center.X + a, center.Y - b, squareWidth, squareWidth);
                currentAngle += angle;

                createLabel(e, $"{i}", Color.DarkRed, nodesCircle[i]);
            }

            #endregion


            #endregion

            #region Bezier
        //    edgePen.Color = Color.DarkSlateGray;
            drawEdge(e, edgePen, nodesCircle[3], nodesCircle[2]);
            drawEdge(e, edgePen, nodesCircle[2], nodesCircle[1]);
            drawEdge(e, edgePen, nodesCircle[1], nodesCircle[2]);
            drawEdge(e, edgePen, nodesCircle[0], nodesCircle[1]);
            drawEdge(e, edgePen, nodesCircle[3], nodesCircle[4]);
            drawEdge(e, edgePen, nodesCircle[4], nodesCircle[2]);
            drawEdge(e, edgePen, nodesCircle[3], nodesCircle[2]);




            #endregion

           


        }

        public void createLabel(PaintEventArgs e,String text,Color color,Point location)
        {
            // Creating and setting the label
            Label mylab = new Label();
            mylab.Text = text;
            mylab.Location = location;
            mylab.AutoSize = true;
            mylab.Font = new Font("Calibri", 18);
            mylab.ForeColor = color;
            mylab.BackColor = System.Drawing.Color.Transparent;

            // Adding this control to the form
            this.Controls.Add(mylab);
        }
        public void drawEdge(PaintEventArgs e,Pen pen,Point start,Point end)
        {
            #region edge from start to end
            int x1 = start.X;
            int y1 = start.Y;
            int x2 = end.X;
            int y2 = end.Y;

            var c = 0.2 * Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

            var signX = x1==x2 
                ? Math.Sign(y2 - y1)
                : Math.Sign(x2 - x1);

            var signY = y1==y2 
                ? Math.Sign(x2 - x1)
                : Math.Sign(y2 - y1);

            double angle = y1 == y2
                ? Math.Atan(Math.Abs(x2 - x1))
                : Math.Atan(Math.Abs(x2 - x1) / Math.Abs(y2 - y1));

            Point controlPoint=new Point();
            controlPoint.X= (int)((x1 + x2) / 2 - signX * c * Math.Cos(angle));
            controlPoint.Y = (int)((y1 + y2) / 2 + signY * c * Math.Sin(angle));

            e.Graphics.DrawBezier(pen, start, controlPoint, controlPoint, end);
            #endregion

            createLabel(e, "122", Color.Green, controlPoint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO my screen size
            Width = 2400;
            Height = 1600;

            //// Creating and setting the label
            //Label mylab = new Label();
            //mylab.Text = "GeeksforGeeks";
            //mylab.Location = new Point(222, 90);
            //mylab.AutoSize = true;
            //mylab.Font = new Font("Calibri", 18);
            //mylab.ForeColor = Color.Green;

            //// Adding this control to the form
            //this.Controls.Add(mylab);
        }
    }
}