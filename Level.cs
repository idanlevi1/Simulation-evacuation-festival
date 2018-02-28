using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace SimulationEvacuationOfMusicFestivals
{
    class Level
    {
        public string percentage { get; set; }
        public int farFromStages { get; set; }
        public int stage1 { get; set; }
        public int stage2 { get; set; }
        public int stage3 { get; set; }
        public int stage4 { get; set; }
        public int stage5 { get; set; }
        public int stage6 { get; set; }
        public int stage7 { get; set; }
        public int stage8 { get; set; }
        public int stage9 { get; set; }
        public int stage10 { get; set; }
        public int stage11 { get; set; }
        public int time { get; set; }

        public Level(string percentage, List<Stages> stagesList, int time)
        {
            this.percentage = percentage;
            this.farFromStages = stagesList[0].numPeople;
            this.stage1 = stagesList[1].numPeople;
            this.stage2 = stagesList[2].numPeople;
            this.stage3 = stagesList[3].numPeople;
            this.stage4 = stagesList[4].numPeople;
            this.stage5 = stagesList[5].numPeople;
            this.stage6 = stagesList[6].numPeople;
            this.stage7 = stagesList[7].numPeople;
            this.stage8 = stagesList[8].numPeople;
            this.stage9 = stagesList[9].numPeople;
            this.stage10 = stagesList[10].numPeople;
            this.stage11 = stagesList[11].numPeople;
            this.time = time;
        }
    }
}
