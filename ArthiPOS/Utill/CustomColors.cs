using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArthiPOS.utill
{
    class CustomColors
    {
        public static Color[] colors = 
        {
            Color.FromArgb(153, 180, 51),
            Color.FromArgb(30,113,69),
            Color.FromArgb(255,196,13),
            Color.FromArgb(255,0,151),
        };
        public static Color getColor()
        {
            Random randNum = new Random();
            Color c= colors[randNum.Next(colors.Count())];
            return c;
        }
    }
     
    
}
