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


namespace realstate
{
    class LableModel: RadLabelElement
    {
        public LableModel(float width, int type) {

           // float listwidth = (width * 100) / 100;
            float listwidth = width - (width * 5)/100;
            float cellwidth = listwidth / 27 ;
            float firstmargin = 27*cellwidth;
            //float title = (listwidth) + 100 - cellwidth;
            //float kindcell = (listwidth * 3) / 16;
            //float kindmargin = title - kindcell;
            //float totalcell = ((listwidth * 3) / 16) ;
            //float totalmargin = kindmargin - totalcell;


            if (type == 1)
            {
                this.Size = new Size(2*(int)cellwidth, 30);
                this.Margin = new Padding((int)(24 * cellwidth) , 0, 0, 0);
               // this.Padding = new Padding(0, 15, 0, 0);
                this.ForeColor = Color.DarkSlateGray;
                this.Font = new System.Drawing.Font("B Nazanin", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
                this.BorderVisible = true;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            }
            if (type == 2)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(22 * cellwidth), 0, 0, 0);
                this.ForeColor = Color.Black;
                this.BorderVisible = true;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            }
            else if (type == 3)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(20 * cellwidth), 0, 0, 0);
                this.ForeColor = Color.Black;
                this.BorderVisible = true;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            }
            else if(type == 4)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(18 * cellwidth) , 0, 0, 0);

                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                this.BorderVisible = true;

            }
            else if(type == 5)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(16 * cellwidth) , 0, 0, 0);
                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                this.BorderVisible = true;

            }
            else if (type == 6)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(14 * cellwidth) , 0, 0, 0);
                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                this.BorderVisible = true;
            }
            else if (type == 7)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(12 * cellwidth) , 0, 0, 0);
                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                this.BorderVisible = true;

            }
            else if (type == 8)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(10 * cellwidth) , 0, 0, 0);
                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                this.BorderVisible = true;

            }
            else if (type == 9)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(8 * cellwidth) , 0, 0, 0);
                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                this.BorderVisible = true;

            }
            else if (type == 10)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(6 * cellwidth), 0, 0, 0);
                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                this.BorderVisible = true;

            }
            else if (type == 11)
            {
                this.Size = new Size(2 * (int)cellwidth, 30);
                this.Margin = new Padding((int)(4 * cellwidth), 0, 0, 0);
                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                this.BorderVisible = true;

            }
            else if (type == 12)
            {
                this.Size = new Size((int)(4*cellwidth), 30);
                this.Margin = new Padding(0, 0, 0, 0);
                this.ForeColor = Color.Black;
                this.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                this.BorderVisible = true;

            }


            this.AutoSize = false;
            this.PositionOffset = new SizeF(0, 0);
            this.RightToLeft = true;
            this.TextAlignment = ContentAlignment.MiddleCenter;
            this.Font = GlobalVariable.headerlistFONT;
          
           
        }

    }
}
