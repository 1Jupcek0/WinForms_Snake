using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Had {
    public class Food : Label {
        public int X { get; set; }
        public int Y { get; set; }
        public Food(int x, int y,Color color){
            this.X = x;
            this.Y = y;
            this.Location = new Point(X, Y);
            this.BackColor = color;
            this.Size = new Size(20, 20);
            this.Text = "";
        }
    }
}
