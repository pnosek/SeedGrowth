using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ziarno
{
    public partial class Form1 : Form
    {
        public bool running = false;
        public int seeds;
        public int radius;
        private Ziarno ziarno;
        
        public Form1()
        {
            InitializeComponent();
            this.cbNeighbourType.SelectedIndex = 0;
            this.cbRandom.SelectedIndex = 0;
        }

        public int getMonteSeeds()
        {
            if (this.txtMonteSeeds.Text == "")
            {
                return 0;
            }
            return Int32.Parse(this.txtMonteSeeds.Text);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int errors = 0;
            String errorMsgs = "";
            if (this.running)
            {
                this.btnStart.Text = "Start";
                this.enableFields(true);
                this.ziarno.stop();
                this.running = false;
                return;
            }
            else
            {
                try
                {
                   // this.pbSeed.Width = Int32.Parse(this.txtX.Text);
                }
                catch (Exception exception)
                {
                    errorMsgs += "X: " + exception.Message + "\n";
                    errors++;
                }
                try
                {
                    //this.pbSeed.Height = Int32.Parse(this.txtY.Text);
                }
                catch (Exception exception)
                {
                    errorMsgs += "Y: " + exception.Message + "\n";
                    errors++;
                }
                try
                {
                    this.radius = Int32.Parse(this.txtRadius.Text);
                }
                catch (Exception exception)
                {
                    errorMsgs += "Radius: " + exception.Message + "\n";
                    errors++;
                }
                try
                {
                    this.seeds = Int32.Parse(this.txtCount.Text);
                }
                catch (Exception exception)
                {
                    errorMsgs += "Seeds count: " + exception.Message + "\n";
                    errors++;
                }
                if (errors == 0)
                {
                    this.enableFields(false);
                    this.btnStart.Text = "Stop";
                }else
                {
                    MessageBox.Show(errorMsgs, "Error!");
                }



            }
            if (errors == 0)
            {
               
                ziarno.start();
                this.running = !running;
            }

        }

        private void enableFields(bool state)
        {
            this.txtCount.Enabled = state;
            this.txtX.Enabled = state;
            this.txtY.Enabled = state;
            this.txtRadius.Enabled = state;
            this.cbNeighbourType.Enabled = state;
            this.cbPbc.Enabled = state;
            this.cbRandom.Enabled = state;
            this.btnSaveImage.Enabled = state;
            this.btnSaveCsv.Enabled = state;
        }
        private bool monteStarted = false;
        private MonteCarlo mc;
        private void button1_Click(object sender, EventArgs e)
        {
            if (monteStarted == false)
            {
                this.mc = new MonteCarlo(getMonteSeeds(), this.pbSeed);
                this.mc.start();
                this.button1.Text = "Stop";
                this.monteStarted = true;
            }else
            {
                this.mc.stop();
                this.monteStarted = false;
                this.button1.Text = "Start";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int errors = 0;
            String errorMsgs = "";
            try
            {
                this.pbSeed.Width = Int32.Parse(this.txtX.Text);
            }
            catch (Exception exception)
            {
                errorMsgs += "X: " + exception.Message + "\n";
                errors++;
            }
            try
            {
                this.pbSeed.Height = Int32.Parse(this.txtY.Text);
            }
            catch (Exception exception)
            {
                errorMsgs += "Y: " + exception.Message + "\n";
                errors++;
            }
            try
            {
                this.radius = Int32.Parse(this.txtRadius.Text);
            }
            catch (Exception exception)
            {
                errorMsgs += "Radius: " + exception.Message + "\n";
                errors++;
            }
            try
            {
                this.seeds = Int32.Parse(this.txtCount.Text);
            }
            catch (Exception exception)
            {
                errorMsgs += "Seeds count: " + exception.Message + "\n";
                errors++;
            }
            if (errors != 0)
            {
                MessageBox.Show(errorMsgs, "Error!");
            }

            if (this.cbGBC.Checked)
            {
                ziarno = new Gbc(this.seeds,
                        this.pbSeed,
                        this.radius,
                        this.cbNeighbourType.SelectedItem.ToString(),
                        this.cbRandom.SelectedItem.ToString(),
                        this.cbPbc.Checked,
                        Int32.Parse(this.txtX.Text),
                        Int32.Parse(this.txtY.Text),
                        this.cbProgression.Checked,
                        Int32.Parse(this.tbProgressionCount.Text),
                        Int32.Parse(this.tbRadiusMin.Text),
                        Int32.Parse(this.tbRadiusMax.Text),
                        float.Parse(this.txtGBCTreshold.Text)
                        );
            }
            else
            {
                if (ziarno == null)
                {
                    ziarno = new Ziarno(this.seeds,
                        this.pbSeed,
                        this.radius,
                        this.cbNeighbourType.SelectedItem.ToString(),
                        this.cbRandom.SelectedItem.ToString(),
                        this.cbPbc.Checked,
                        Int32.Parse(this.txtX.Text),
                        Int32.Parse(this.txtY.Text),
                        this.cbProgression.Checked,
                        Int32.Parse(this.tbProgressionCount.Text),
                        Int32.Parse(this.tbRadiusMin.Text),
                        Int32.Parse(this.tbRadiusMax.Text)
                        );
                }
            }
            ziarno.paintIt();
            
        }

        private void pbSeed_Click(object sender, EventArgs e)
        {
            //if(this.cbRandom.SelectedItem.ToString() == "By click")
            //{
                MouseEventArgs ev = (MouseEventArgs)e;
                if (this.running)
                {
                    ziarno.removePoint(ev.X, ev.Y);
                }
                else
                {
                    if (this.cbDualPhase.Checked)
                    {
                        ziarno.removePoint(ev.X, ev.Y);
                        ziarno.removePointRefresh();
                    }
                    else
                    {
                        ziarno.addPoint(ev.X, ev.Y);
                    }
                }
            //}
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            this.ziarno.saveFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.ziarno.next();
        }

        private void btnSaveCsv_Click(object sender, EventArgs e)
        {
            Color[,] grid = this.ziarno.getGrid();
            var csv = new StringBuilder();
            var c = 0;
            //x,y,id,color
            for(int i = 0; i < ziarno.getWidth(); i++)
            {
                for(int j = 0; j < ziarno.getHeight(); j++)
                {
                     
                    var newLine = string.Format("{0},{1},{2},{3}", i, j, c, grid[i,j].ToArgb());
                    csv.AppendLine(newLine);
                    c++;
                }
            }
            File.WriteAllText(@"data.csv", csv.ToString());
        }

        private void cbGBC_CheckedChanged(object sender, EventArgs e)
        {
            this.cbNeighbourType.SelectedIndex = 1;
            this.cbNeighbourType.Enabled = !this.cbGBC.Checked;
        }

        private void btnDualPhase_Click(object sender, EventArgs e)
        {
            this.ziarno.dualPhase();
        }

        private void btnBoundaries_Click(object sender, EventArgs e)
        {
            ziarno.markBoundaries();

            Color[,] grid = this.ziarno.getGrid();
            var csv = new StringBuilder();
            var c = 0;
            int boundaries = 0;
            int[] colorsCount = new int[ziarno.colors.Count];
            for (int i = 0; i < ziarno.colors.Count; i++)
            {
                colorsCount[i] = 0;
            }

            for (int i = 0; i < ziarno.getWidth(); i++)
            {
                for (int j = 0; j < ziarno.getHeight(); j++)
                {
                    for (int k = 0; k < ziarno.colors.Count; k++)
                    {
                        if (grid[i, j].ToArgb() == ziarno.colors[k].ToArgb())
                        {
                            colorsCount[k]++;
                            break;
                        }
                    }
                }
            }

            //x,y,id,color
            for (int i = 0; i < ziarno.getWidth(); i++)
            {
                for (int j = 0; j < ziarno.getHeight(); j++)
                {
                    if (grid[i, j].ToArgb() == Color.Black.ToArgb())
                    {
                        boundaries++;
                    }
                    var newLine = string.Format("{0},{1},{2},{3},{4}", i, j, c, grid[i, j].ToArgb(), grid[i, j].ToArgb() == Color.Black.ToArgb());
                    csv.AppendLine(newLine);
                    c++;
                }
            }
            boundaries /= 2;
            string msg = "Seeds count:\n";
            for(int i = 0; i < ziarno.colors.Count; i++)
            {
                msg += string.Format("{0}: {1}\n", i + 1, colorsCount[i]);
            }
            msg += string.Format("\n\nBoundaries count: {0}", boundaries); 
            MessageBox.Show(msg.ToString());
            File.WriteAllText(@"data.csv", csv.ToString());
        }
    }
}
