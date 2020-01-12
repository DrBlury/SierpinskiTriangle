using System;
using System.Windows.Forms;
using System.Drawing;
class Sierpinski : Form
{
    private int iteration = 1;
    public Sierpinski()
    {
        Width = 600;
        Height = 600;
        Text = "Sierpinski-Dreieck";
    }

    static void Main()
    {
        Application.Run(new Sierpinski());
    }

    override
    protected void OnPaint(PaintEventArgs e)
    {
        RectangleF bounds = e.Graphics.VisibleClipBounds;
        PointF point1 = new PointF(bounds.X, bounds.Y);
        PointF point2 = new PointF(bounds.X+bounds.Width, bounds.Y);
        PointF point3 = new PointF(bounds.X+bounds.Width/2, bounds.Y+bounds.Height);
        PointF[] pointFArray = { point1, point2, point3 };
        SierpinskiTriangle(e.Graphics, Brushes.Black, pointFArray, iteration);
    }
    private void SierpinskiTriangle(Graphics g, Brush brush, PointF[] points, int iteration)
    {
        if(iteration == 1)
        {
            g.FillPolygon(brush, points);
        } else if (iteration > 1)
        {
            iteration--;
            PointF[] newPoints = {
                new PointF((points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2),
                new PointF( (points[1].X + points[2].X) / 2, (points[1].Y + points[2].Y) / 2 ),
                new PointF( (points[0].X + points[2].X) / 2, (points[0].Y + points[2].Y) / 2 )
            };

            // Subtriangles here
            PointF[] subtri1 = {
                new PointF(points[0].X, points[0].Y),
                new PointF( (points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2 ),
                new PointF( (points[0].X + points[2].X) / 2, (points[0].Y + points[2].Y) / 2 )
            };

            PointF[] subtri2 = {
                new PointF( (points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2 ),
                new PointF(points[1].X, points[1].Y),
                new PointF( (points[2].X + points[1].X) / 2, (points[2].Y + points[1].Y) / 2 )
            };

            PointF[] subtri3 = {
                new PointF(points[2].X, points[2].Y),
                new PointF( (points[2].X + points[1].X) / 2, (points[2].Y + points[1].Y) / 2 ),
                new PointF( (points[2].X + points[0].X) / 2, (points[2].Y + points[0].Y) / 2 )
            };

            SierpinskiTriangle(g, brush, subtri1, iteration);
            SierpinskiTriangle(g, brush, subtri2, iteration);
            SierpinskiTriangle(g, brush, subtri3, iteration);
        }
    }

    override
    protected void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Up)
        {
            this.iteration++;
            this.Refresh();
        } else if (e.KeyCode == Keys.Down && iteration > 1)
        {
            this.iteration--;
            this.Refresh();
        }
    }
}