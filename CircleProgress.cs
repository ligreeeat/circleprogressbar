using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
namespace test
{
    public class CircleProgressBar : Control
    {
        float _progress = 0F;
        float _Wpen = 1;
        float _Npen = 5;
        float _Fwidth = 10;
        [Description("进度条颜色")]
        public Color CircleColor
        {
            get;
            set;
        }

        [Description("外圈粗度")]
        public float WpenThin
        {
            get { return _Wpen; }
            set { _Wpen = value; }
        }
        [Description("内圈粗度")]
        public float NpenThin
        {
            get { return _Npen; }
            set { _Npen = value; }
        }

        [Description("内心方形边长")]
        public float Fwitdh
        {
            get { return _Fwidth; }
            set { _Fwidth = value; }
        }

        public void PaintProgress(PaintEventArgs e)
        {
            float x = this.Width / 2;
            float y = this.Height / 2;//圆心坐标
            float Wr = x - WpenThin / 2;//外圈半径
            float Nr = x - NpenThin / 2;//内圈半径
            int Wx = (int)(x - Wr);
            int Wy = (int)(y - Wr);//外圈起始坐标

            int Nx = (int)(x - Nr);
            int Ny = (int)(y - Nr);//外圈起始坐标

            int Fy = (int)(y - Fwitdh/2);
            int Fx = (int)(x - Fwitdh/2);// 内心方形坐标

            Graphics dc = this.CreateGraphics();
            dc.Clear(this.BackColor);
            Pen Wpen = new Pen(CircleColor, WpenThin);
            Pen Npen = new Pen(CircleColor, NpenThin);
            Brush Fbrush = new SolidBrush(CircleColor);
            dc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            float startAngle = -90;
            float sweepAngle = Progress / 100 * 360;//起始角度
            Rectangle Wrec = new Rectangle(Wx, Wy, 2 * (int)Wr, 2 * (int)Wr);
            Rectangle Nrec = new Rectangle(Nx, Ny, 2 * (int)Nr, 2 * (int)Nr);
            Rectangle Frec = new Rectangle(Fx, Fy, (int)Fwitdh, (int)Fwitdh);
            dc.DrawEllipse(Wpen, Wrec);
            dc.FillRectangle(Fbrush, Frec);
            dc.DrawArc(Npen, Nrec, startAngle, sweepAngle);
        }

        public float Progress
        {
            get { return _progress; }
            set
            {
                if (_progress != value && value >= 0 && value <= 100)
                {
                    _progress = value;
                    OnProgressChanged();
                }
            }
        }
        protected virtual void OnProgressChanged()
        {
            this.Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            PaintProgress(e);
            base.OnPaint(e);
        }
    }
}
