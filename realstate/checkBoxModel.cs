using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.Windows.Forms;
using System.Drawing;
using Telerik.WinControls;
using System.IO;
using Classes;


namespace realstate
{
    class checkBoxModel : RadCheckBoxElement
    {
        public checkBoxModel(float width)
        {

           // float listwidth = (width * 100) / 100;
            float listwidth = width - (width*5)/100;
            float cellwidth = listwidth / 27;
           
           

            this.Size = new Size(30, 30);
            this.Margin = new Padding((int)(26 * cellwidth) , 0, 0, 0);
            this.ForeColor = Color.DarkSlateGray;
            this.Font = new System.Drawing.Font("B Nazanin", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this)).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.GetChildAt(0))).BackColor3 = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            this.Click += new System.EventHandler( Classes.Class.Click);
            this.AutoSize = false;
            this.PositionOffset = new SizeF(0, 0);
            this.RightToLeft = true;
            this.TextAlignment = ContentAlignment.MiddleCenter;
           
            this.Font = GlobalVariable.headerlistFONT;
           
          
           
        }

    }
}
