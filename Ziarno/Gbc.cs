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
    public class Gbc : Ziarno
    {
        private float threshold;
        public Gbc(int seeds, 
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
            int progMaxRadius,
            float threshold
            ) : base(seeds, pb, radius, neighType, randomType, periodicConds, width, height, progression, progressionCount, progMinRadius, progMaxRadius)
        {
            this.rand = new Random(DateTime.Now.Millisecond);
            this.nextCells = new Color[width, height];
            this.currentNeighbours = new List<Color>();
            this.threshold = threshold;
            
        }


        protected override Color[,] GetnextIterationCells()
        {

            this.newCells = new Color[nextCells.GetLength(0), nextCells.GetLength(1)];

            for (int i = 0; i < newCells.GetLength(0); i++)
            {
                for (int j = 0; j < newCells.GetLength(1); j++)
                {
                    
                    newCells[i, j] = checkRules(i, j);
                }
            }
            return newCells;
        }


        protected override CountColor changeState(int i, int j)
        {
            int count = 0;
            int count2 = 0;
            Color color = Color.Black;
            if (nextCells[i, j].ToArgb().Equals(Color.Black.ToArgb()))
            { 
                for (int k = 0; k < this.colors.Count; k++)
                {
                    count2 = 0;
                    for (int l = 0; l < this.currentNeighbours.Count; l++)
                    {
                        if (this.currentNeighbours[l].ToArgb().Equals(this.colors[k].ToArgb()))
                        {
                            count2++;
                        }
                    }
                    if (count < count2)
                    {
                        color = this.colors[k];
                        count = count2;
                    }
                }                
            }
            CountColor c;
            c.count = count;
            c.color = color;
            return c;
        }

        private CountColor changeStateMoore(int i, int j)
        {

            CountColor color;
            color.color = Color.Black;
            color.count = 0;
            int count2 = 0;
            if (nextCells[i, j].ToArgb().Equals(Color.Black.ToArgb()))
            {
                color.count = 0;
                if (this.currentNeighbours.Count > 0)
                {
                    int count = 0;
                    for (int k = 0; k < colors.Count; k++)
                    {
                        count2 = 0;
                        for (int l = 0; l < this.currentNeighbours.Count; l++)
                        {
                            if (this.currentNeighbours[l].ToArgb().Equals(this.colors[k].ToArgb()))
                            {
                                count2++;
                            }
                        }
                        if (count < count2)
                        {
                            color.color = colors[k];
                            count = count2;
                        }
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

        private Color checkRules(int i, int j)
        {
            Color col = Color.Black;

            if(!nextCells[i,j].ToArgb().Equals(Color.Black.ToArgb()))
            {
                return nextCells[i, j];
            }

            getNeighboursMoore(i, j);
            CountColor c = changeState(i, j);
            if (ruleOne(c.count))
            {
                this.rule1++;
                return c.color;
            }

            getRuleTwoNeighbourhood(i, j);
            c = changeState(i, j);
            if (ruleTwo(c.count))
            {
                this.rule2++;
                return c.color;
            }


            getRuleThreeNeighbourhood(i, j);
            c = changeState(i, j);
            if (ruleThree(c.count))
            {
                this.rule3++;
                return c.color;
            }

            getNeighboursMoore(i, j);
            c = changeStateMoore(i, j);
            if (ruleFour(i,j))
            {
                this.rule4++;
                return c.color;
            }

            return nextCells[i, j];
            
        }

        private bool ruleOne(int count)
        {
            return count >= 5;
        }

        private bool ruleTwo(int count)
        {
            return count >= 3;
        }

        private bool ruleThree(int count)
        {
            return count >= 3;
        }

        private bool ruleFour(int i, int j)
        {
            //Random rand = new Random();
            Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF + i * j + j + i);
            float probability = (float)rand.NextDouble() * 100;

            return probability <= this.threshold;
                         
        }



        protected new void getNeighboursMoore(int i, int j)
        {
            int ibound = this.nextCells.GetUpperBound(0);
            int jbound = this.nextCells.GetUpperBound(1);
            this.currentNeighbours.Clear();// = new List<Color>();

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
                    //impossible
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
                    //impossible
                }
                else if (i == ibound && j == 0)
                {
                    //impossible
                }
                else if (i == ibound && j == jbound)
                {
                    //impossible
                }
                else if (i == 0 && j == jbound)
                {
                    //impossible
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

        protected void getRuleTwoNeighbourhood(int i, int j)
        {
            int ibound = this.nextCells.GetUpperBound(0);
            int jbound = this.nextCells.GetUpperBound(1);
            this.currentNeighbours.Clear();// = new List<Color>();

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
                    //impossible
                }
                else if (j == 0 && i != 0 && i != ibound)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j+1]);
                }
                else if (j == jbound && i != 0 && i != ibound)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                }
                else if (i == 0 && j == 0)
                {
                    //impossible
                }
                else if (i == ibound && j == 0)
                {
                    //impossible
                }
                else if (i == ibound && j == jbound)
                {
                    //impossible
                }
                else if (i == 0 && j == jbound)
                {
                    //impossible
                }
                else if (i == ibound && j > 0 && j < jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                }
                else if (i > 0 && j > 0 && i < ibound && j < jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j]);
                    this.currentNeighbours.Add(this.nextCells[i, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j]);
                }
            }
        }

        protected void getRuleThreeNeighbourhood(int i, int j)
        {
            int ibound = this.nextCells.GetUpperBound(0);
            int jbound = this.nextCells.GetUpperBound(1);
            this.currentNeighbours.Clear();// = new List<Color>();

            if (!this.periodicConds)
            {
                if (i > 0 && j > 0 && i < ibound && j < jbound)
                {
                    this.currentNeighbours.Add(this.nextCells[i - 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j - 1]);
                    this.currentNeighbours.Add(this.nextCells[i - 1, j + 1]);
                    this.currentNeighbours.Add(this.nextCells[i + 1, j + 1]);
                }
            }
        }
    }
}
