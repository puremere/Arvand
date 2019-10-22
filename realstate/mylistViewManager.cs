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

    public class MyElement2 : LightVisualElement
    {
        protected override void PaintElement(Telerik.WinControls.Paint.IGraphics graphics, float angle, SizeF scale)
        {
            object state = graphics.SaveState();

            Graphics g = (graphics.UnderlayGraphics as Graphics);

            Rectangle rect = new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height);
            g.SetClip(this.Shape.CreatePath(rect), System.Drawing.Drawing2D.CombineMode.Intersect);

            base.PaintElement(graphics, angle, scale);

            graphics.RestoreState(state);
        }
    }
    public  class mylistViewManager 
    {

        public static RadLabelElement titlelable = null;
        public static RadLabelElement arealable = null;
        public static RadLabelElement kindlable = null;
        public static RadLabelElement totallable = null;
        public static RadLabelElement vadielable = null;
        public static RadLabelElement ejarelable = null;
        public static RadPanelElement Picpanel = null;
        public static MyElement2 imp = null;
        public static RadPanelElement panel = null;
        private float width;





        public mylistViewManager(float width)
        {
            // TODO: Complete member initialization


           




            //arealable = new RadLabelElement();
            //arealable.AutoSize = false;
            //arealable.Size = new Size((int)cellwidth, 30);
            //arealable.Margin = new Padding((int)title, 50, 0, 0);
            //arealable.PositionOffset = new SizeF(0, 0);
            //arealable.RightToLeft = true;
            //arealable.TextAlignment = ContentAlignment.MiddleLeft;
            //arealable.Font = GlobalVariable.headerlistFONT;


            //kindlable = new RadLabelElement();
            //kindlable.AutoSize = false;
          
            //kindlable.PositionOffset = new SizeF(0, 0);
            //kindlable.RightToLeft = true;
            //kindlable.TextAlignment = ContentAlignment.MiddleLeft;
            //kindlable.Font = GlobalVariable.headerlistFONT;


            //totallable = new RadLabelElement();
            //totallable.AutoSize = false;
            //totallable.Size = new Size((int)totalcell, 30);
            //totallable.Margin = new Padding((int)totalmargin, 35, 0, 0);
            //totallable.PositionOffset = new SizeF(0, 0);
            //totallable.RightToLeft = true;
            //totallable.TextAlignment = ContentAlignment.MiddleLeft;
            //totallable.Font = GlobalVariable.headerlistFONT;


            //vadielable = new RadLabelElement();
            //vadielable.AutoSize = false;
       
            //vadielable.PositionOffset = new SizeF(0, 0);
            //vadielable.RightToLeft = true;
            //vadielable.TextAlignment = ContentAlignment.MiddleLeft;
            //vadielable.Font = GlobalVariable.headerlistFONT;


            //ejarelable = new RadLabelElement();
            //ejarelable.AutoSize = false;
         
            //ejarelable.PositionOffset = new SizeF(0, 0);
            //ejarelable.RightToLeft = true;
            //ejarelable.TextAlignment = ContentAlignment.MiddleLeft;
            //ejarelable.Font = GlobalVariable.headerlistFONT;

            //Picpanel = new RadPanelElement();
            //Picpanel.Size = new System.Drawing.Size(25, 25);
            //Picpanel.PanelBorder.Visibility = ElementVisibility.Collapsed;
            //Picpanel.Margin = new Padding(10, 35, (int)picpos, 35);
            //Picpanel.Shape = new RoundRectShape(4);



            imp = new MyElement2();
            string path = GlobalVariable.searchPicPath;
            imp.Image = Image.FromFile(path);
            imp.ImageLayout = ImageLayout.Zoom;
           // imp.Shape = Picpanel.Shape;




            panel = new RadPanelElement();
            panel.RightToLeft = true;
            panel.PanelBorder.Visibility = ElementVisibility.Visible;
            panel.PanelBorder.ForeColor = Color.White;
            panel.Shape = new RoundRectShape();
            panel.PanelFill.GradientStyle = GradientStyles.Solid;
            panel.PanelFill.BackColor = Color.WhiteSmoke; 
            this.width = width;
        }


        public  RadLabelElement gettitleLable() 
        {
            return titlelable;
        }
        public  RadLabelElement getarealable()
        {
            return arealable;
        }
        public  RadLabelElement getkindlable()
        {
            return kindlable;
        }
        public  RadLabelElement gettotallable()
        {
            return totallable;
        }
        public  RadLabelElement getvadielable()
        {
            return vadielable;
        }
        public  RadLabelElement getejarelable()
        {
            return ejarelable;
        }

        public  RadPanelElement getPicPanel()
        {
            return Picpanel;
        }
        public  RadPanelElement getPanel()
        {
            return panel;
        }
        public MyElement2 getimp()
        {
            return imp;
        }
    }
}
