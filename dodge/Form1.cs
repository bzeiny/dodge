using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dodge
{
    public partial class Form1 : Form
    {
        List<int> yLeftList = new List<int>();
        List<int> yRightList = new List<int>();

        int xLeft = 45;
        int xRight = 510;

        int speedLeft = 7;
        int speedRight = 7;

        int widthLeft = 15;
        int heightLeft = 25;

        int widthRight = 15;
        int heightRight = 25;

        int counterLeft = 0;
        int counterRight = 0;

        bool wPress;
        bool aPress;
        bool sPress;
        bool dPress;

        int heroX = 5;
        int heroY = 200;

        int heroSpeed = 6;
        int heroSize = 20;

        Pen heroPen = new Pen(Color.Red, 10);
        SolidBrush heroBrush = new SolidBrush(Color.Red);
        Pen obstaclePen = new Pen(Color.Blue, 10);
        SolidBrush obstacleBrush = new SolidBrush(Color.Blue);

        public Form1()
        {
            InitializeComponent();
            yLeftList.Add(0);
            yRightList.Add(400);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    aPress = false;
                    break;
                case Keys.D:
                    dPress = false;
                    break;
                case Keys.W:
                    wPress = false;
                    break;
                case Keys.S:
                    sPress = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    aPress = true;
                    break;
                case Keys.D:
                    dPress = true;
                    break;
                case Keys.W:
                    wPress = true;
                    break;
                case Keys.S:
                    sPress = true;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.FillRectangle(heroBrush, heroX, heroY, heroSize, heroSize);
            for (int i = 0; i < yLeftList.Count; i++)
            {
                e.Graphics.FillRectangle(obstacleBrush, xLeft, yLeftList[i], widthLeft, heightLeft);
            }
            for (int i = 0; i < yRightList.Count; i++)
            {
                e.Graphics.FillRectangle(obstacleBrush, xRight, yRightList[i], widthRight, heightRight);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (wPress == true && heroY > 0)
            {
                heroY -= heroSpeed;
            }
            if (aPress == true && heroX > 0)
            {
                heroX -= heroSpeed;               
            }
            if (dPress == true && heroX < this.Width - heroSize)
            {
                heroX += heroSpeed;
            }
            if (sPress == true && heroY < this.Height - heroSize)
            {
                heroY += heroSpeed;
            }

            //Move left obstacles
            for (int i = 0; i < yLeftList.Count(); i++)
            {
                yLeftList[i] += speedLeft;
            }
            //Move right obstacles
            for (int i = 0; i < yRightList.Count(); i++)
            {
                yRightList[i] -= speedRight;
            }
            
            //Add 1 to the counter for left column
            counterLeft++;
            if (counterLeft == 15)
            {
                yLeftList.Add(0);
                counterLeft = 0;
            }
            counterRight++;
            if (counterRight == 15)
            {
                yRightList.Add(400); 
                counterRight = 0;
            }
            Rectangle heroRectangle = new Rectangle(heroX, heroY, heroSize, heroSize);
            for (int i = 0; i < yLeftList.Count(); i++)
            { 
                Rectangle leftRectangle = new Rectangle(xLeft, yLeftList[i], widthLeft, heightLeft);
                if (heroRectangle.IntersectsWith(leftRectangle))
                {
                    timer1.Enabled = false;
                }
            }
            for (int i = 0; i < yLeftList.Count(); i++)
            { 
                Rectangle rightRectangle = new Rectangle(xRight, yRightList[i], widthRight, heightRight);
                 if (heroRectangle.IntersectsWith(rightRectangle))
                {
                    timer1.Enabled = false;
                }
            }
            
            if (heroX >= this.Width - 30)
            {
                timer1.Enabled = false;
            }
                Refresh();
        }
    }
}
