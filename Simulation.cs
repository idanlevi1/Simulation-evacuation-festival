using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace SimulationEvacuationOfMusicFestivals
{
    public partial class Simulation : Form
    {
        Thread th1;
        Random rdm;
        int population, cripplePopulation, dropsPeople;
        string scenarioChecked;
        Stages stages = new Stages();
        int[] finishTime;
        List<Level> levels = new List<Level>();
        public Simulation()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            population = trackBar_population.Value + 20000;
            label_population.Text = population.ToString();
            label_cripple.Text = trackBar_cripple.Value.ToString() + "%";
            label_drops.Text = trackBar_drops.Value.ToString();
            cripplePopulation = trackBar_cripple.Value;
            dropsPeople = trackBar_drops.Value;
            listView.Items[0].Selected = true;
            listView.Select();
            scenarioChecked = listView.Items[listView.SelectedIndices[0]].Text;
            rdm = new Random();
            finishTime = new int[5];
            chartTimeLine.ChartAreas[0].AxisX.Minimum = 0;
            chartTimeLine.ChartAreas[0].AxisX.Maximum = 1.02;
            chartTimeLine.Visible = false;
            //panel timer - load Results
            panel_loadResults.Visible = false;
        }
        
        private void Form1_Closed(object sender, EventArgs e)
        {
            th1.Abort("Fim th1");
            Environment.Exit(0);
        }

        private void trackBar_population_Scroll(object sender, EventArgs e)
        {
            population = trackBar_population.Value + 20000;
            label_population.Text = population.ToString();
            listView.Select();
        }

        private void trackBar_defective_Scroll(object sender, EventArgs e)
        {
            label_cripple.Text = trackBar_cripple.Value.ToString() + "%";
            cripplePopulation = trackBar_cripple.Value;
            listView.Select();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
                scenarioChecked = listView.Items[listView.SelectedIndices[0]].Text;
            textBox_details.Text = "";
            switch (scenarioChecked)
            {
                case "תרחיש 1":
                    textBox_details.Text = "תרחיש 1";
                    textBox_details.AppendText(Environment.NewLine);
                    textBox_details.AppendText("תרחיש הכולל פינוי של חלק מאנשי הפסטיבל (15,309 במקרה של תכולה מלאה). \n בשל כלי שיט בנהר שעולה באש סמוך לחלק הצפון מערבי של המתחם. \nבמקרה זה יהיה צורך בפינוי של האנשים אשר נמצאים במתחמי הבמות 4,7,10,11. \n בשל העשן והאש היציאות הקרובות לאזור השריפה יסגרו.");
                    break;
                case "תרחיש a1":
                    textBox_details.Text = "תרחיש 1a";
                    textBox_details.AppendText(Environment.NewLine);
                    textBox_details.Text += "בדומה לתרחיש 1 - פינוי של חלק מאנשי הפסטיבל, זהו תרחיש דמה שאומר שכל היציאות פתוחות.";
                    break;
                case "תרחיש b1":
                    textBox_details.Text = "תרחיש 1b";
                    textBox_details.AppendText(Environment.NewLine);
                    textBox_details.Text += " תרחיש הדומה לתרחיש 1 רק שנפתחת יציאה רחבה של 9 מטר בסמוך לשריפה.";
                    break;
                case "תרחיש 2":
                    textBox_details.Text = "תרחיש 2 ";
                    textBox_details.AppendText(Environment.NewLine);
                    textBox_details.Text += "תרחיש הכולל פינוי כולל של כל אוכלוסיית הפסטיבל.\nבעוד פינוי 1 מתרחש הוחלט על פינוי כולל בשל איום של פיצוץ בעקבות אירוע טרור. ";
                    break;
                case "תרחיש 3":
                    textBox_details.Text = "תרחיש 3";
                    textBox_details.AppendText(Environment.NewLine);
                    textBox_details.Text += "תרחיש הכולל פינוי של כל אוכלוסיית הפסטיבל.\nבעוד פינוי 1 מתרחש הוחלט על פינוי כולל בשל התחממות המנוע של כלי השיט והתפוצצותו המנוע.";
                    break;
            }
        }
        public void thread1()  // Quadrados Vermelhos
        {
            /*
            for (int i = 0; i < 20; i++)
            {
                this.CreateGraphics().DrawEllipse(new Pen(Brushes.Red, 4), new Rectangle(rdm.Next(this.Width / 4, this.Width / 2), rdm.Next(this.Height / 4, this.Height / 2), 3, 3));
                Thread.Sleep(100);
            }*/
        }
        void Scenario1(List<Stages> listStages)
        {
            //scenario 1
            int mainPart = Convert.ToInt32(population * 0.2355) + 1;
            List<int> numStages = new List<int>() { 4, 7, 10, 11 };
            double[] peoplePerSec70pReg = { 10.842, 50.358, 39.871, 12.665, 0.201 };
            //Calaulate People Per Second By Cripple Population and regular population
            double[] peoplePerSec = CalaulatePeoplePerSecond(peoplePerSec70pReg);
            //Play the 5 parts of evacuation - 25% 50% 75% 98% 100%
            int[] time = Play5PartOfEcacuation(listStages, peoplePerSec, numStages, mainPart);
            finishTime = time;
        }
        void Scenario_a1(List<Stages> listStages)
        {
            //scenario a1
            int mainPart = Convert.ToInt32(population * 0.2355) + 1;
            List<int> numStages = new List<int>() { 4, 7, 10, 11 };
            double[] peoplePerSec70pReg = { 11.356, 57.123, 46.673, 18.63, 0.388 };
            //Calaulate People Per Second By Cripple Population and regular population
            double[] peoplePerSec = CalaulatePeoplePerSecond(peoplePerSec70pReg);
            //Play the 5 parts of evacuation - 25% 50% 75% 98% 100%
            int[] time = Play5PartOfEcacuation(listStages, peoplePerSec, numStages, mainPart);
            finishTime = time;
        }
        void Scenario_b1(List<Stages> listStages)
        {
            //scenario a1
            int mainPart = Convert.ToInt32(population * 0.2355) + 1;
            List<int> numStages = new List<int>() { 4, 7, 10, 11 };
            double[] peoplePerSec70pReg = { 10.935, 53.156, 42.525, 13.808, 0.244 };
            //Calaulate People Per Second By Cripple Population and regular population
            double[] peoplePerSec = CalaulatePeoplePerSecond(peoplePerSec70pReg);
            //Play the 5 parts of evacuation - 25% 50% 75% 98% 100%
            int[] time = Play5PartOfEcacuation(listStages, peoplePerSec, numStages, mainPart);
            finishTime = time;
        }
        void Scenario2(List<Stages> listStages)
        {
            //scenario 2
            List<int> numStages = new List<int>();
            for (int i = 0; i <= 11; i++)
                numStages.Add(i);
            double[] peoplePerSec70pReg = { 17.529, 13.062, 11.459, 11.447, 11.403 };
            //Calaulate People Per Second By Cripple Population and regular population
            double[] peoplePerSec = CalaulatePeoplePerSecond(peoplePerSec70pReg);
            //Play the 5 parts of evacuation - 25% 50% 75% 98% 100%
            int[] time = Play5PartOfEcacuation(listStages, peoplePerSec, numStages, population);
            finishTime = time;
        }
        void Scenario3(List<Stages> listStages)
        {
            //scenario 2
            List<int> numStages = new List<int>();
            for (int i = 0; i <= 11; i++)
                numStages.Add(i);
            double[] peoplePerSec70pReg = { 16.856, 13.286, 11.459, 11.447, 11.403 };
            //Calaulate People Per Second By Cripple Population and regular population
            double[] peoplePerSec = CalaulatePeoplePerSecond(peoplePerSec70pReg);
            //Play the 5 parts of evacuation - 25% 50% 75% 98% 100%
            int[] time = Play5PartOfEcacuation(listStages, peoplePerSec, numStages, population);
            finishTime = time;
        }
        public int[] Play5PartOfEcacuation(List<Stages> listStages, double[] peoplePerSec, List<int> numStages, int populationPart)
        {
            string stagesString = GetNumberOfStages(numStages);
            int[] time = new int[5];
            CalculateDropsTime(time);
            //--PART1 - 25%  
            time[0] += Evacuation(listStages, 0.25, peoplePerSec[0], numStages, populationPart);
            UpdateDataGrid("25%", listStages, time[0]);
            //--PART2 50 %
            time[1] += time[0] + Evacuation(listStages, 0.25, peoplePerSec[1], numStages, populationPart);
            UpdateDataGrid("50%", listStages, time[1]);
            //--PART2 75 %
            time[2] += time[1] + Evacuation(listStages, 0.25, peoplePerSec[2], numStages, populationPart);
            UpdateDataGrid("75%", listStages, time[2]);
            //--PART2 98 %
            time[3] += time[2] + Evacuation(listStages, 0.23, peoplePerSec[3], numStages, populationPart);
            UpdateDataGrid("98%", listStages, time[3]);
            //--PART2 100 %
            time[4] += time[3] + Evacuation(listStages, 0.02, peoplePerSec[4], numStages, populationPart);
            UpdateDataGrid("100%", listStages, time[4]);
            // Get call stack
            StackTrace stackTrace = new StackTrace();
            string fatherFunction = stackTrace.GetFrame(1).GetMethod().Name; // Get calling method name
            PrintEveuationDetails(stagesString, time[4],fatherFunction);
            return time;
        }
        public int Evacuation(List<Stages> listStages, double percentage, double peoplePerSec, List<int> numStages, int populationPart)
        {
            int part;
            if (percentage == 0.02) // PART5 100%
                part = SumPopulationleft(listStages, numStages);
            else
                part = Convert.ToInt32(Math.Ceiling(populationPart * percentage));
            return stages.CalculateEscapeTime(listStages, part, peoplePerSec, numStages);
        }
        public string GetNumberOfStages(List<int> numStages)
        {
            string stages = "";
            if (numStages.Count == 12)
                stages = "All stages ";
            else
                for (int i = 0; i < numStages.Count; i++)
                    stages += numStages[i].ToString() + " ";
            return stages;
        }
        public void PrintEveuationDetails(string stagesString, int time,string scenario)
        {
            textbox_result.Visible = true;
            textbox_result.Text = scenario + "- Evacuation Stages: " + stagesString + "| Number Of People: " + population + "| Finish Time: " + time + " second";
        }

        public double[] CalaulatePeoplePerSecond(double[] peoplePerSec70pReg)
        {
            double[] peoplePerSec100pReg = new double[peoplePerSec70pReg.Length];
            double[] peoplePerSec = new double[peoplePerSec70pReg.Length];
            double ratioRegCrip = 1.183729, ratioCripPercent = 0.48262;
            for (int i = 0; i < peoplePerSec70pReg.Length; i++)
            {
                peoplePerSec100pReg[i] = peoplePerSec70pReg[i] * ratioRegCrip;
                peoplePerSec[i] = peoplePerSec100pReg[i] * (1 - (cripplePopulation / (double)100) + (cripplePopulation / (double)100) * ratioCripPercent);
            }
            return peoplePerSec;
        }
        public int SumPopulationleft(List<Stages> listStages, List<int> numStages)
        {
            int sum = 0;
            for (int i = 0; i < numStages.Count; i++)
                sum += listStages[numStages[i]].numPeople;
            return sum;
        }
        public void DrawGraph()
        {
            chartTimeLine.ChartAreas[0].AxisX.CustomLabels.Add(0, 0.05, "0%");
            chartTimeLine.ChartAreas[0].AxisX.CustomLabels.Add(0.24, 0.26, "25%");
            chartTimeLine.ChartAreas[0].AxisX.CustomLabels.Add(0.49, 0.51, "50%");
            chartTimeLine.ChartAreas[0].AxisX.CustomLabels.Add(0.74, 0.76, "75%");
            chartTimeLine.ChartAreas[0].AxisX.CustomLabels.Add(0.95, 0.97, "98%");
            chartTimeLine.ChartAreas[0].AxisX.CustomLabels.Add(0.99, 1.02, "100%");

            chartTimeLine.Series["Time"].Points.AddXY(0, 0);
            chartTimeLine.Series["Time"].Points.AddXY(0.25, finishTime[0]);
            chartTimeLine.Series["Time"].Points.AddXY(0.50, finishTime[1]);
            chartTimeLine.Series["Time"].Points.AddXY(0.75, finishTime[2]);
            chartTimeLine.Series["Time"].Points.AddXY(0.98, finishTime[3]);
            chartTimeLine.Series["Time"].Points.AddXY(1.0, finishTime[4]);
            if (chartTimeLine.Series.FindByName("Data Points") == null)
                chartTimeLine.Series.Add("Data Points");
            else
                chartTimeLine.Series[1].Points.Clear();
            chartTimeLine.Series["Data Points"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chartTimeLine.Series["Data Points"].Color = Color.DarkRed;
            chartTimeLine.Series["Data Points"].Points.AddXY(0, 0);
            chartTimeLine.Series["Data Points"].Points.AddXY(0.25, finishTime[0]);
            chartTimeLine.Series["Data Points"].Points.AddXY(0.50, finishTime[1]);
            chartTimeLine.Series["Data Points"].Points.AddXY(0.75, finishTime[2]);
            chartTimeLine.Series["Data Points"].Points.AddXY(0.98, finishTime[3]);
            chartTimeLine.Series["Data Points"].Points.AddXY(1.00, finishTime[4]);
            chartTimeLine.Visible = true;
        }
        public void UpdateDataGrid(string percentage, List<Stages> listStages, int time){
              levels.Add(new Level(percentage, listStages, time));
            dataGridView.DataSource = null;
            dataGridView.Refresh();
            dataGridView.DataSource = levels;
        }
        public void CalculateDropsTime(int []time)
        {
            int numDrops;
            int tempDropsPeople = dropsPeople;
            for (int i = 0; i < 5; i++)
            {
                numDrops = rdm.Next(0, tempDropsPeople + 1);
                tempDropsPeople -= numDrops;
                time[i] += numDrops * 3;
            }
        }
        private void chartTimeLine_Click(object sender, EventArgs e)
        {

        }

        private void Loader_Tick(object sender, EventArgs e)
        {
            if (CircleProgressbarLoadFirst.Value < 100)
                CircleProgressbarLoadFirst.Value++;
            else
                splash.Visible = false;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_population_Click(object sender, EventArgs e)
        {

        }

        private void label_cripple_Click(object sender, EventArgs e)
        {

        }



        private void label_result_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void splash_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar_cripple_ValueChanged(object sender, EventArgs e)
        {
            label_cripple.Text = trackBar_cripple.Value.ToString() + "%";
            cripplePopulation = trackBar_cripple.Value;
            listView.Select();
        }

        private void trackBar_population_ValueChanged(object sender, EventArgs e)
        {
            population = trackBar_population.Value + 20000;
            label_population.Text = population.ToString();
            listView.Select();
        }

        private void trackBar_drops_ValueChanged(object sender, EventArgs e)
        {
            dropsPeople = trackBar_drops.Value;
            label_drops.Text = dropsPeople.ToString();
            listView.Select();
        }

        private void b_start_Click(object sender, EventArgs e)
        {
            CircleProgressbar.Value = 0;
            panel_loadResults.Visible = true;
            timer.Enabled = true;
            timer.Start();

            //Initialize stages     
            chartTimeLine.Series[0].Points.Clear();
            List<Stages> listStages = stages.BuildStages(population, cripplePopulation);

            int reg = Convert.ToInt32(Math.Ceiling(population * 0.7));
            int crip = population - reg;
            //Input datagrid first time (time 0)
            dataGridView.DataSource = null;
            dataGridView.Refresh();
            levels.Clear();
            levels.Add(new Level("0%", listStages, 0));
            dataGridView.DataSource = levels;
            //play scenario
            switch (scenarioChecked)
            {
                case "תרחיש 1":
                    Scenario1(listStages);
                    break;
                case "תרחיש a1":
                    Scenario_a1(listStages);
                    break;
                case "תרחיש b1":
                    Scenario_b1(listStages);
                    break;
                case "תרחיש 2":
                    Scenario2(listStages);
                    break;
                case "תרחיש 3":
                    Scenario3(listStages);
                    break;
            }
            DrawGraph();
            //Design datagrid 
            dataGridView.Columns[0].DefaultCellStyle.ForeColor = Color.DarkRed;
            dataGridView.Columns[dataGridView.ColumnCount - 1].DefaultCellStyle.ForeColor = Color.Blue;
            
            CircleProgressbar.Value += 90;
            //---PLAY THREADS 
            //   th1 = new Thread(thread1);
            //  th1.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (CircleProgressbar.Value < 100)
                CircleProgressbar.Value++;
            else {
                System.Threading.Thread.Sleep(200);
                panel_loadResults.Visible = false;
            }
        }
    }
}
