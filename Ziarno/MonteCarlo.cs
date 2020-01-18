using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Ziarno
{
    
    class MonteCarlo
    {
        private PictureBox pb;
        private int seeds;
        private List<Color> colors;
        private Bitmap bmap;
        private int currentX;
        private int currentY;
        private Color currentColor;
        private List<Color> currentNeighbours;
        Random rand;
        private int width = 50;
        private int height = 50;
        private System.Threading.Thread th;
        public MonteCarlo(int seeds, PictureBox pb)
        {
            this.seeds = seeds;
            this.pb = pb;
            this.rand = new Random(DateTime.Now.Millisecond);
        }
        public void stop()
        {
            this.th.Abort();
        }
        public void start()
        {
            if(this.seeds == 0)
            {
                return;
            }

            colors = new List<Color>(this.seeds);
            for(int i = 0; i < this.seeds; i++)
            {
                colors.Add(Color.FromArgb(this.rand.Next(256), this.rand.Next(256), this.rand.Next(256)));
            }
            this.bmap = new Bitmap(this.width, this.height);
            for (int i = 0; i < this.width; i++)
            {
                for(int j = 0; j < this.height; j++)
                {
                    this.bmap.SetPixel(i, j, colors[this.rand.Next(this.seeds)]);
                }
            }
            this.pb.Image = this.bmap;
            this.bmap.SetResolution(this.pb.Width, this.pb.Height);
            th = new System.Threading.Thread(this.loop);
            th.Start();
            //this.loop();
        }


        private void loop()
        {
            while(true) { 
                this.currentX = this.rand.Next(this.width - 1);
                this.currentY = this.rand.Next(this.height - 1);
                try
                {
                    this.pb.Invoke(new Action(() =>
                    {
                        this.currentColor = this.bmap.GetPixel(this.currentX, this.currentY);
                    }));
                }catch(Exception ex)
                {
                    th.Abort();
                }
                this.doShit();
                //System.Threading.Thread.Sleep(1000);
            }
        }

        private void doShit()
        {
            this.getNeighbours(this.currentX, this.currentY);
            int prevEnergy = (from x in this.currentNeighbours
                              where x != this.currentColor
                              select x).Count();
            if (prevEnergy == 0)
            {
                return;
            }

            Color nextColor;
            int nextEnergy;
            while (true)
            {
                nextColor = this.currentNeighbours[this.rand.Next(this.currentNeighbours.Count - 1)];
                nextEnergy = (from x in this.currentNeighbours
                              where x != nextColor
                              select x).Count();
                if(nextEnergy <= prevEnergy)
                {
                    try
                    {
                        this.pb.Invoke(new Action(() =>
                        {
                            this.bmap.SetPixel(this.currentX, this.currentY, nextColor);
                            this.pb.Image = this.bmap;
                        }));
                    }
                    catch (Exception) {
                        th.Abort();
                        Application.Exit();
                    }
                    return;
                }
            }

        }

        private void getNeighbours(int x, int y)
        {
            try
            {
                this.currentNeighbours = new List<Color>();
                if (x == 0 && y != 0 && y != this.height)
                {
                    for (int k = y - 1; k <= y + 1; k++)
                        this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, k));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y + 1));
                }
                else if (x == this.width && y != 0 && y != this.height)
                {
                    for (int k = y - 1; k <= y + 1; k++)
                        this.currentNeighbours.Add(this.bmap.GetPixel(this.width - 1, k));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y + 1));
                }
                else if (y == 0 && x != 0 && x != this.width)
                {
                    for (int k = x - 1; k <= x + 1; k++)
                        this.currentNeighbours.Add(this.bmap.GetPixel(k, y + 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y));
                }
                else if (y == this.height && x != 0 && x != this.width)
                {
                    for (int k = x - 1; k <= x + 1; k++)
                        this.currentNeighbours.Add(this.bmap.GetPixel(k, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y));
                }
                else if (x == 0 && y == 0)
                {
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y + 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y + 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y));
                }
                else if (x == this.width && y == 0)
                {
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y + 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y + 1));
                }
                else if (x == this.width && y == this.height)
                {
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y));
                }
                else if (x == 0 && y == this.height)
                {
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y));
                }
                else
                {
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x - 1, y + 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x, y + 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y - 1));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y));
                    this.currentNeighbours.Add(this.bmap.GetPixel(x + 1, y + 1));
                }
            }catch(Exception ex)
            {
                th.Abort();
            }
        }
    }
}
