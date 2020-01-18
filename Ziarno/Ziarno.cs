using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ziarno
{
    public class Ziarno
    {
        protected PictureBox pb;
        protected int seeds;
        public List<Color> colors;
        protected Bitmap bmap;
        protected int currentX;
        protected int currentY;
        protected Color currentColor;
        protected List<Color> currentNeighbours;
        protected Random rand;
        protected int width;
        protected int height;
        protected string neighType;
        protected string randomType;
        protected bool periodicConds;
        protected bool isRunning = false;
        protected System.Threading.Thread th;
        protected Color[,] nextCells;
        protected Color[,] newCells;
        protected List<Color> removedColors;
        protected int progressionCount;
        public Color progressionColor = Color.Red;
        protected int progMinRadius = 1;
        protected int progMaxRadius = 5;
        protected bool progression = false;
        private bool doRemovePoint = false;
        private Point pointToRemove;
        private bool refreshing = false;

        public int rule1, rule2, rule3, rule4;
        //BackgroundWorker bg = new BackgroundWorker();
        public bool dp = false;
        public Ziarno(int seeds, 
            PictureBox pb,
            int radius, 
            string neighType, 
            string randomType, 
            bool periodicConds, 
            int width, 
            int height, 
            bool progression,
            int progressionCount,
            int progMinRadius,
            int progMaxRadius
            )
        {
            this.seeds = seeds;
            this.pb = pb;
            this.rand = new Random(DateTime.Now.Millisecond);
            this.neighType = neighType;
            this.randomType = randomType;
            this.periodicConds = periodicConds;
            this.width = width;
            this.height = height;
            this.nextCells = new Color[width, height];
            this.currentNeighbours = new List<Color>();
            this.removedColors = new List<Color>();
            this.progression = progression;
            this.progressionCount = progressionCount;
            this.progMinRadius = progMinRadius;
            this.progMaxRadius = progMaxRadius;
            this.pointToRemove = new Point(-1, -1);
            this.rule1 = this.rule2 = this.rule3 = this.rule4 = 0;
        }

        public void saveFile()
        {
            this.pb.Image.Save(@"img.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public void stop()
        {
            MessageBox.Show(string.Format("{0}\n{1}\n{2}\n{3}\n", this.rule1, this.rule2, this.rule3, this.rule4).ToString());
            this.isRunning = false;
            this.th.Abort();
        }

        public Color[,] getGrid()
        {
            return this.nextCells;
        }
        public int getWidth()
        {
            return this.width;
        }
        public int getHeight()
        {
            return this.height;
        }
        public void addPoint(int x, int y)
        {
            Color c = Color.FromArgb(this.rand.Next(1, 255), this.rand.Next(1, 255), this.rand.Next(1, 255));
            colors.Add(c);
            this.nextCells[x, y] = c;
            refresh();
        }


        public void addProgression()
        {
            List<Point> tmpPoints = new List<Point>();
            Point tmpPoint = new Point(this.rand.Next(0, this.width - 1), this.rand.Next(0, this.height - 1));
            int r = this.rand.Next(this.progMinRadius, this.progMaxRadius);
            for (int i = 0; i < this.progressionCount; i++)
            {
                r = this.rand.Next(this.progMinRadius, this.progMaxRadius);
                while (tmpPoints.Count((point) => point.X == tmpPoint.X && point.Y == tmpPoint.Y) != 0)
                {
                    tmpPoint = new Point(this.rand.Next(0, this.width - 1), this.rand.Next(0, this.height - 1));
                }
                tmpPoints.Add(tmpPoint);
                for (int k = 0; k <= r; k++)
                {
                    for (int j = 0; j < 360; j++)
                    {
                        double x = tmpPoint.X - k * Math.Cos(2 * Math.PI / 360 * j);
                        double y = tmpPoint.Y - k * Math.Sin(2 * Math.PI / 360 * j);
                        if (x < this.width && x >= 0 && y < this.height && y >= 0)
                        {
                            this.bmap.SetPixel((int)x, (int)y, this.progressionColor);
                            this.nextCells[(int)x, (int)y] = this.progressionColor;
                        }
                    }
                }
            }
            refresh();
        }

        public void paintIt()
        {
            if (this.seeds == 0)
            {
                return;
            }

            this.bmap = new Bitmap(this.width, this.height);
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    this.bmap.SetPixel(i, j, Color.Black);
                    this.nextCells[i, j] = Color.Black;
                }
            }
            newCells = new Color[this.nextCells.GetLength(0), this.nextCells.GetLength(1)];
            if (this.randomType != "By click")
            {
                this.rand = new Random(DateTime.Now.Millisecond);
                this.colors = new List<Color>(this.seeds);
                Color tmp = Color.FromArgb(this.rand.Next(1, 255), this.rand.Next(1, 255), this.rand.Next(1, 255)); ;
                for (int i = 0; i < this.seeds; i++)
                {
                    tmp = Color.FromArgb(this.rand.Next(1, 255), this.rand.Next(1, 255), this.rand.Next(1, 255));
                    if (this.progression)
                    {
                        while (tmp == this.progressionColor)
                        {
                            tmp = Color.FromArgb(this.rand.Next(1, 255), this.rand.Next(1, 255), this.rand.Next(1, 255));
                        }
                    }
                    colors.Add(tmp);
                }
            }
            else
            {
                colors = new List<Color>();
            }
            if (this.progression)
            {
                addProgression();
            }
            addSeedsToBmp();

            this.pb.Invoke(new Action(() =>
            {
                this.pb.Image = this.bmap;
            }));
            
            this.bmap.SetResolution(this.pb.Width, this.pb.Height);
            refresh();
        }

        public void start()
        {

            th = new System.Threading.Thread(this.loop);
            //this.bg.DoWork += loop;
            //this.bg.RunWorkerAsync();
            this.isRunning = true;
            th.Start();
            //this.loop();
        }

        //protected void loop(object sender, DoWorkEventArgs e)
        protected void loop()
        {
            while (this.isRunning)
            {


                //this.currentColor = this.nextCells[this.currentX, this.currentY];
                try
                {
                    if (this.doRemovePoint)
                    {
                        Color currentColor = this.nextCells[this.pointToRemove.X, this.pointToRemove.Y];

                        for (int x = 0; x < this.width - 1; x++)
                        {
                            for (int y = 0; y < this.height - 1; y++)
                            {
                                if (this.nextCells[x, y] == currentColor)
                                {
                                    this.nextCells[x, y] = Color.Black;
                                }
                            }
                        }
                        this.doRemovePoint = false;
                    }

                    else
                    {
                        this.nextCells = GetnextIterationCells();
                    }
                    this.refresh();
                }
                catch (Exception e)
                {
                    //this.doRemovePoint = false;
                }
            }
           // this.th.Abort();
        }

        public void next()
        {
            this.isRunning = true;
            this.nextCells = GetnextIterationCells();
            this.refresh();
        }
        
        public virtual void removePoint(int i, int j)
        {
            this.doRemovePoint = true;
            this.pointToRemove.X = i;
            this.pointToRemove.Y = j;
            Color removedColor = this.nextCells[i, j];
            for(int k = 0; k < this.colors.Count; k++)
            {
                if(this.colors[k].ToArgb() == removedColor.ToArgb())
                {
                    this.removedColors.Add(this.colors[k]);
                    this.colors.RemoveAt(k);
                    break;
                }
            }
        }

        public void removePointRefresh() {
            if (this.doRemovePoint)
            {
                Color currentColor = this.nextCells[this.pointToRemove.X, this.pointToRemove.Y];

                for (int x = 0; x < this.width - 1; x++)
                {
                    for (int y = 0; y < this.height - 1; y++)
                    {
                        if (this.nextCells[x, y] == currentColor)
                        {
                            this.nextCells[x, y] = Color.Black;
                        }
                    }
                }
                this.doRemovePoint = false;
                this.refresh();
            }
        }

        public void dualPhase()
        {
            this.dp = true;
            List<Color> oldColors = new List<Color>();
            for(int i = 0; i < this.colors.Count; i++)
            {
                oldColors.Add(this.colors[i]);
            }

            this.colors.Clear();
            Color tmp;
            for (int i = 0; i < this.seeds; i++)
            {
                tmp = Color.FromArgb(this.rand.Next(1, 255), this.rand.Next(1, 255), this.rand.Next(1, 255));
                while (oldColors.Contains(tmp))
                {
                    tmp = Color.FromArgb(this.rand.Next(1, 255), this.rand.Next(1, 255), this.rand.Next(1, 255));
                }
                colors.Add(tmp);
            }

            List<Point> tmpPoints = new List<Point>();
            Point tmpPoint;
            for (int i = 0; i < this.colors.Count; i++)
            {
                tmpPoint = new Point(this.rand.Next(0, this.width - 1), this.rand.Next(0, this.height - 1));
                while (this.nextCells[tmpPoint.X, tmpPoint.Y] != Color.Black)
                {
                    tmpPoint = new Point(this.rand.Next(0, this.width - 1), this.rand.Next(0, this.height - 1));
                }
                tmpPoints.Add(tmpPoint);
                this.nextCells[tmpPoint.X, tmpPoint.Y] = this.colors[i];
                this.bmap.SetPixel(tmpPoint.X, tmpPoint.Y, this.colors[i]);
            }
            //this.colors.Clear();
            this.refresh();
        }

        public void markBoundaries()
        {
            for (int i = 0; i < newCells.GetLength(0); i++)
            {
                for (int j = 0; j < newCells.GetLength(1); j++)
                {
                    getNeighboursMoore(i, j);
                    int count = this.currentNeighbours.Count((x) => x.ToArgb() != newCells[i, j].ToArgb() && x.ToArgb() != Color.Black.ToArgb());
                    if (count > 0)
                    {
                        nextCells[i, j] = Color.Black;
                    }else
                    {
                        nextCells[i, j] = newCells[i, j];
                    }
                }
            }
            this.refresh();
        }

        protected virtual Color[,] GetnextIterationCells()
        {

            this.newCells = new Color[nextCells.GetLength(0), nextCells.GetLength(1)];

            switch (this.neighType)
            {
                case "Von Neumann":
                    for (int i = 0; i < newCells.GetLength(0); i++)
                    {
                        for (int j = 0; j < newCells.GetLength(1); j++)
                        {
                            getVonneumannNeigh(i, j);
                            newCells[i, j] = changeState(i, j).color;
                        }
                    }
                    break;
                case "Moore":
                    for (int i = 0; i < newCells.GetLength(0); i++)
                    {
                        for (int j = 0; j < newCells.GetLength(1); j++)
                        {
                            getNeighboursMoore(i, j);
                            newCells[i, j] = changeState(i, j).color;
                        }
                    }
                    break;
                case "Hexagonal left":
                    for (int i = 0; i < newCells.GetLength(0); i++)
                    {
                        for (int j = 0; j < newCells.GetLength(1); j++)
                        {
                            getNeighboursHexLeft(i, j);
                            newCells[i, j] = changeState(i, j).color;
                        }
                    }
                    break;
                case "Hexagonal right":
                    for (int i = 0; i < newCells.GetLength(0); i++)
                    {
                        for (int j = 0; j < newCells.GetLength(1); j++)
                        {
                            getNeighboursHexRight(i, j);
                            newCells[i, j] = changeState(i, j).color;
                        }
                    }
                    break;
                case "Hexagonal random":
                    Random random = new Random();
                    if (random.Next(1, 3) == 1)
                    {
                        for (int i = 0; i < newCells.GetLength(0); i++)
                        {
                            for (int j = 0; j < newCells.GetLength(1); j++)
                            {
                                getNeighboursHexLeft(i, j);
                                newCells[i, j] = changeState(i, j).color;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < newCells.GetLength(0); i++)
                        {
                            for (int j = 0; j < newCells.GetLength(1); j++)
                            {
                                getNeighboursHexRight(i, j);
                                newCells[i, j] = changeState(i, j).color;
                            }
                        }
                    }
                    break;
                case "Pentagonal random":
                    for (int i = 0; i < newCells.GetLength(0); i++)
                    {
                        for (int j = 0; j < newCells.GetLength(1); j++)
                        {
                            getNeighboursPentaRandom(i, j);
                            newCells[i, j] = changeState(i, j).color;
                        }
                    }
                    break;
                default:
                    break;
            }
            return newCells;
        }

        public void refresh()
        {
            if (!this.refreshing)
            {
                this.refreshing = true;
                try
                {
                    
                    this.pb.Invoke(new Action(() =>
                    {
                        for (int x = 0; x < this.width - 1; x++)
                        {
                            for (int y = 0; y < this.height - 1; y++)
                            {

                                ((Bitmap)this.pb.Image).SetPixel(x, y, nextCells[x, y]);
                                //this.bmap.SetPixel(x, y, nextCells[x, y]);
                            }
                        }
                        this.pb.Refresh();
                        //((Bitmap)this.pb.Image).SetPixel(x, y, nextCells[x, y]);// = this.bmap;
                        this.refreshing = false;
                    }));
                }
                catch (Exception e)
                {
                    this.refreshing = false;
                }
            }
        }

        protected void addSeedsToBmp()
        {
            if(this.randomType == "Random")
            {
                List<Point> tmpPoints = new List<Point>();
                Point tmpPoint = new Point(this.rand.Next(0, this.width-1), this.rand.Next(0, this.height-1));
                for (int i = 0; i < this.seeds; i++)
                {
                    while (tmpPoints.Count((point) => point.X == tmpPoint.X && point.Y == tmpPoint.Y && this.nextCells[tmpPoint.X, tmpPoint.Y] != this.progressionColor) != 0)
                    {
                        tmpPoint = new Point(this.rand.Next(0, this.width-1), this.rand.Next(0, this.height-1));
                    }
                    tmpPoints.Add(tmpPoint);
                    this.nextCells[tmpPoint.X, tmpPoint.Y] = this.colors[i];
                    this.bmap.SetPixel(tmpPoint.X, tmpPoint.Y, this.colors[i]);
                }
            }else if(this.randomType == "Evenly")
            {
                int spacingX = (this.width / this.seeds)-1;
                int spacingY = (this.height / this.seeds)-1;
                int startX = 10;
                int startY = 10;
                for(int i = 0; i < this.seeds; i++)
                {
                    for(int j = 0; j < this.seeds; j++)
                    {
                        this.nextCells[startX * j + spacingX, startY * i + spacingY] = this.colors[this.rand.Next(this.colors.Count())];
                        this.bmap.SetPixel(startX * j + spacingX, startY * i + spacingY, this.colors[this.rand.Next(this.colors.Count())]);
                    }
                }
            }
        }

        public struct CountColor
        {
            public int count;
            public Color color;
        }

        protected virtual CountColor changeState(int i, int j)
        {
        
            CountColor color;
            color.color = Color.Black;
            color.count = 0;
            if (nextCells[i, j].ToArgb() == Color.Black.ToArgb())
            {
                color.count = 0;

                int count = 0;
                if (this.dp)
                {
                    for (int k = 0; k < this.currentNeighbours.Count; k++)
                    {
                        for (int l = 0; l < this.removedColors.Count; l++)
                        {
                            if (this.currentNeighbours[k].ToArgb() == this.removedColors[l].ToArgb())
                            {
                                this.currentNeighbours[k] = Color.Black;
                                //break;
                            }
                        }
                    }
                    for (int l = 0; l < this.removedColors.Count; l++)
                    {
                        if (nextCells[i, j].ToArgb() == this.removedColors[l].ToArgb())
                        {
                            color.color = nextCells[i, j];
                            color.count = 0;
                            return color;
                        }
                    }
                }
                for (int k = 0; k < colors.Count; k++)
                {
                    if (count < this.currentNeighbours.Count((x) => x.ToArgb() == colors[k].ToArgb()))
                    {
                        color.color = colors[k];
                        count = this.currentNeighbours.Count((x) => x.ToArgb() == colors[k].ToArgb());
                    }
                }
                return color;
            }
            else
            {
                color.color = nextCells[i, j];
                return color;
            }
        }

        protected void getNeighboursMoore(int i, int j)
        {
            int ibound = this.nextCells.GetUpperBound(0);
            int jbound = this.nextCells.GetUpperBound(1);
            this.currentNeighbours.Clear();// = new List<Color>();

        
            if (this.periodicConds)
            {
                if (i == 0 && j != 0 && j != jbound)
                {
                    for (int k = j - 1; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound, k]);
                    for (int k = j - 1; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[i + 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i == ibound && j != 0 && j != jbound)
                {
                    for (int k = j - 1; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[0, k]);
                    for (int k = j - 1; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound - 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    for (int k = i - 1; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, jbound]);
                    for (int k = i - 1; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    for (int k = i - 1; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, 0]);
                    for (int k = i - 1; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (i == 0 && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, jbound]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, jbound]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[ibound, jbound]);

                }
                else if (i == ibound && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[0, j]);
                    this.currentNeighbours.Add(this.nextCells[0, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, jbound]);
                    this.currentNeighbours.Add(this.nextCells[i, jbound]);
                    this.currentNeighbours.Add(this.nextCells[0, jbound]);
                }
                else if (i == ibound && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, 0]);
                    this.currentNeighbours.Add(this.nextCells[i, 0]);
                    this.currentNeighbours.Add(this.nextCells[0, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[0, jbound]);
                    this.currentNeighbours.Add(this.nextCells[0, 0]);
                }
                else if (i == 0 && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j]);
                    this.currentNeighbours.Add(this.nextCells[i, 0]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, 0]);
                    this.currentNeighbours.Add(this.nextCells[ibound, 0]);

                }
                else
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                }

            }

            if (!this.periodicConds)
            {
                if (i == 0 && j != 0 && j != jbound)
                {
                    for (int k = j - 1; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[i + 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i == ibound && j != 0 && j != jbound)
                {
                    for (int k = j - 1; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound - 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    for (int k = i - 1; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    for (int k = i - 1; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (i == 0 && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (i == ibound && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i == ibound && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                }
                else if (i == 0 && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                }
            }
        }

        protected void getVonneumannNeigh(int i, int j)
        {
            int ibound = this.nextCells.GetUpperBound(0);
            int jbound = this.nextCells.GetUpperBound(1);
            this.currentNeighbours.Clear();// = new List<Color>();
           
                if (this.periodicConds)
                {
                    if (i == 0 && j != 0 && j != jbound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[ibound, j]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    }
                    else if (i == ibound && j != 0 && j != jbound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[0, j]);
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    }
                    else if (j == 0 && i != 0 && i != ibound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[i, jbound]);
                    }
                    else if (j == jbound && i != 0 && i != ibound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i, 0]);

                    }
                    else if (i == 0 && j == 0)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i, jbound]);
                        this.currentNeighbours.Add(this.nextCells[ibound, j]);

                    }
                    else if (i == ibound && j == 0)
                    {
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[i, jbound]);
                        this.currentNeighbours.Add(this.nextCells[0, j]);

                    }
                    else if (i == ibound && j == jbound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[ibound, 0]);
                        this.currentNeighbours.Add(this.nextCells[0, jbound]);
                    }
                    else if (i == 0 && j == jbound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                        this.currentNeighbours.Add(this.nextCells[ibound, j]);
                        this.currentNeighbours.Add(this.nextCells[i, 0]);
                    }
                    else
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    }
                }
                if (!this.periodicConds)
                {
                    if (i == 0 && j != 0 && j != jbound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    }
                    else if (i == ibound && j != 0 && j != jbound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    }
                    else if (j == 0 && i != 0 && i != ibound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    }
                    else if (j == jbound && i != 0 && i != ibound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    }
                    else if (i == 0 && j == 0)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);

                    }
                    else if (i == ibound && j == 0)
                    {
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);

                    }
                    else if (i == ibound && j == jbound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    }
                    else if (i == 0 && j == jbound)
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    }
                    else
                    {
                        this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                        this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                        this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                        this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    }
                }
            
        }


        protected void getNeighboursHexLeft(int i, int j)
        {
            int ibound = this.nextCells.GetUpperBound(0);
            int jbound = this.nextCells.GetUpperBound(1);
            this.currentNeighbours = new List<Color>();

            if (this.periodicConds)
            {
                if (i == 0 && j != 0 && j != jbound)
                {
                    for (int k = j - 1; k <= j; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound, k]);
                    for (int k = j; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[i + 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i == ibound && j != 0 && j != jbound)
                {
                    for (int k = j; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[0, k]);
                    for (int k = j - 1; k <= j; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound - 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    for (int k = i - 1; k <= i; k++)
                        this.currentNeighbours.Add(this.nextCells[k, jbound]);
                    for (int k = i; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    for (int k = i; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, 0]);
                    for (int k = i - 1; k <= i; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (i == 0 && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[jbound, i]);
                    this.currentNeighbours.Add(this.nextCells[ibound, jbound]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j]);

                }
                else if (i == ibound && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[0, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[0, j]);
                    this.currentNeighbours.Add(this.nextCells[ibound, jbound]);
                    this.currentNeighbours.Add(this.nextCells[ibound - 1, jbound]);

                }
                else if (i == ibound && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[ibound, 0]);
                    this.currentNeighbours.Add(this.nextCells[0, 0]);
                    this.currentNeighbours.Add(this.nextCells[0, jbound]);

                }
                else if (i == 0 && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j]);
                    this.currentNeighbours.Add(this.nextCells[i, 0]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, 0]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);

                }
                else
                {
                    this.currentNeighbours.Add(this.nextCells[i + 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                }
            }
            if (!this.periodicConds)
            {
                if (i == 0 && j != 0 && j != jbound)
                {
                    for (int k = j; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[i + 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i == ibound && j != 0 && j != jbound)
                {
                    for (int k = j - 1; k <= j; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound - 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    for (int k = i; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    for (int k = i - 1; k <= i; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (i == 0 && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);

                }
                else if (i == ibound && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);

                }
                else if (i == ibound && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                }
                else if (i == 0 && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else
                {
                    this.currentNeighbours.Add(this.nextCells[i + 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                }
            }


        }
        protected void getNeighboursHexRight(int i, int j)
        {

            int ibound = this.nextCells.GetUpperBound(0);
            int jbound = this.nextCells.GetUpperBound(1);
            this.currentNeighbours = new List<Color>();

            if (this.periodicConds)
            {
                if (i == 0 && j != 0 && j != jbound)
                {
                    for (int k = j; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound, k]);
                    for (int k = j - 1; k <= j; k++)
                        this.currentNeighbours.Add(this.nextCells[i + 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i == ibound && j != 0 && j != jbound)
                {
                    for (int k = j - 1; k <= j; k++)
                        this.currentNeighbours.Add(this.nextCells[0, k]);
                    for (int k = j; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound - 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    for (int k = i; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, jbound]);
                    for (int k = i - 1; k <= i; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    for (int k = i - 1; k <= i; k++)
                        this.currentNeighbours.Add(this.nextCells[k, 0]);
                    for (int k = i; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (i == 0 && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, jbound]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, jbound]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j + 1]);

                }
                else if (i == ibound && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[0, j]);
                    this.currentNeighbours.Add(this.nextCells[0, jbound]);
                    this.currentNeighbours.Add(this.nextCells[i, jbound]);

                }
                else if (i == ibound && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[ibound - 1, 0]);
                    this.currentNeighbours.Add(this.nextCells[ibound, 0]);
                    this.currentNeighbours.Add(this.nextCells[0, jbound]);
                    this.currentNeighbours.Add(this.nextCells[0, jbound - 1]);

                }
                else if (i == 0 && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[ibound, j]);
                    this.currentNeighbours.Add(this.nextCells[ibound, 0]);
                    this.currentNeighbours.Add(this.nextCells[i, 0]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j - 1]);
                }
                else
                {
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                }
            }
            if (!this.periodicConds)
            {
                if (i == 0 && j != 0 && j != jbound)
                {
                    for (int k = j - 1; k <= j; k++)
                        this.currentNeighbours.Add(this.nextCells[i + 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i == ibound && j != 0 && j != jbound)
                {
                    for (int k = j; k <= j + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[ibound - 1, k]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    for (int k = i - 1; k <= i; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    for (int k = i; k <= i + 1; k++)
                        this.currentNeighbours.Add(this.nextCells[k, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
                else if (i == 0 && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j]);
                }
                else if (i == ibound && j == 0)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i == ibound && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i , j]);
                }
                else if (i == 0 && j == jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i , j]);
                    this.currentNeighbours.Add(this.nextCells[i , j - 1]);
                }
                else
                {
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                }
            }


        }
        protected void getNeighboursPentaRandom(int i, int j)
        {
            Random random = new Random(DateTime.Now.Second);
            int ibound = this.nextCells.GetUpperBound(0);
            int jbound = this.nextCells.GetUpperBound(1);
            getNeighboursMoore(i,j);

            if (this.periodicConds)
            {
                if (i == 0 && j != 0 && j != jbound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(3);
                            break;
                    }
                }
                else if (i == ibound && j != 0 && j != jbound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(3);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                    }
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(3);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(5);
                            break;
                    }
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(3);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                    }
                }
                else if (i == 0 && j == 0)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(3);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(5);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            break;
                    }
                }
                else if (i == ibound && j == 0)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(5);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(3);
                            break;
                    }
                }
                else if (i == ibound && j == jbound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(3);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(5);
                            break;
                    }
                }
                else if (i == 0 && j == jbound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(5);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(3);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            break;
                    }
                }
                else
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(5);
                            break;
                    }
                }
            }
            else if (!this.periodicConds)
            {
                if (i == 0 && j != 0 && j != jbound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                    }
                }
                else if (i == ibound && j != 0 && j != jbound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            break;
                    }
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 2:
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                    }
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                    }
                }
                else if (i == 0 && j == 0)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            break;
                    }
                }
                else if (i == ibound && j == 0)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 2:
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            break;
                    }
                }
                else if (i == ibound && j == jbound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            break;
                        case 4:
                            break;
                    }
                }
                else if (i == 0 && j == jbound)
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            break;
                    }
                }
                else
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(4);
                            this.currentNeighbours.RemoveAt(2);
                            break;
                        case 2:
                            this.currentNeighbours.RemoveAt(5);
                            this.currentNeighbours.RemoveAt(3);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 3:
                            this.currentNeighbours.RemoveAt(2);
                            this.currentNeighbours.RemoveAt(1);
                            this.currentNeighbours.RemoveAt(0);
                            break;
                        case 4:
                            this.currentNeighbours.RemoveAt(7);
                            this.currentNeighbours.RemoveAt(6);
                            this.currentNeighbours.RemoveAt(5);
                            break;
                    }
                }
            }
        }

    
    }
}
