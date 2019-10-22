using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace realstate
{
    class GradientPanel : Telerik.WinControls.UI.RadPanel
    {
        public Color topcolor { get; set; }
        public Color bottomColor { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, this.topcolor, this.bottomColor, 90F);
            Graphics g = e.Graphics;
            g.FillRectangle(lgb, this.ClientRectangle);
            base.OnPaint(e);

        }
    }
}
