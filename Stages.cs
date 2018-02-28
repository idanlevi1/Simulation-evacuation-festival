using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SimulationEvacuationOfMusicFestivals
{
    public class Stages
    {
        public int numStage { get; set; }
        public int numPeople { get; set; }

        public List<Stages> BuildStages(int population)
        {
            int farFromStages = Convert.ToInt32(population * 0.1);
            int mainPart = Convert.ToInt32(population * 0.2355) + 1;  //Stages number 4 7 10 11    
            int otherPeople = population - (mainPart + farFromStages);
            int mainPartTemp = mainPart;
            int mainPartIncludeAll = 0, otherPeopleIncludeAll = 0;
            int otherPeopleTemp = otherPeople;
            double ratio;
            Random rnd = new Random();
            List<Stages> listStages = new List<Stages>();

            //Far away people
            Stages far = new Stages();
            far.numStage = 0;
            far.numPeople = farFromStages;
            listStages.Add(far);

            //Insert random people to all stages.
            for (int i = 1; i <= 11; i++)
            {
                Stages stage = new Stages();
                stage.numStage = i;
                if (i == 4 || i == 7 || i == 10 || i == 11)//main part people 
                {     
                    if(i==11) //last stage
                        stage.numPeople = mainPart - mainPartIncludeAll;
                    else
                    {
                        ratio = ((double)mainPart / 4) / ((double)mainPartTemp / 2);
                        if (ratio > 1)
                            ratio = 1;
                        stage.numPeople = Convert.ToInt32((rnd.Next(1, mainPartTemp)) * ratio);
                    }                                                              
                    mainPartTemp -= stage.numPeople;
                    mainPartIncludeAll += stage.numPeople;
                }
                else
                {
                    if (i == 9) // last stage
                        stage.numPeople = otherPeople - otherPeopleIncludeAll;
                    else { //other stages 
                        ratio = ((double)otherPeople / 7) / ((double)otherPeopleTemp / 2);
                        if (ratio > 1)
                            ratio = 1;
                        stage.numPeople = Convert.ToInt32((rnd.Next(1, otherPeopleTemp)) * ratio);
                    }
                    otherPeopleTemp -= stage.numPeople;
                    otherPeopleIncludeAll += stage.numPeople;
                }
                listStages.Add(stage);
            }
            //--print to console ---
            Console.WriteLine("Main part: " + mainPartIncludeAll + "  Other: " + otherPeopleIncludeAll);
            foreach (Stages s in listStages)
                Console.WriteLine("Stage: " + s.numStage + "  Number people: " + s.numPeople);
            return listStages;
        }

        public int CalculateEscapeTime(List<Stages> listStages, int partPeople, double PerSec,List<int> numStages)
        {
            Random rnd = new Random();
            int PerSecINT = Convert.ToInt32(Math.Floor(PerSec)), PerSecTemp = PerSecINT;
            double oneMoreTemp = 0.0;
            double addPartOfPerson = PerSec * 1000 % 1000 / (double)1000;
            int partPeopleTemp = partPeople;
            int peoplePerSec, from_stage, time, peopleOut,range;
            for (time = 0; partPeopleTemp > 0 && numStages.Count>0; time++)
            {
                oneMoreTemp += addPartOfPerson;
                if (oneMoreTemp >=1 )
                {
                    PerSecINT++;
                    oneMoreTemp -=1;
                }
                if (partPeopleTemp <= PerSecINT)
                {
                    PerSecINT = partPeopleTemp;
                    PerSecTemp = PerSecINT;
                }
                RemoveEmptyStages(listStages, numStages); //update numStages in only full stages
                peopleOut = 0;
                peoplePerSec = PerSecINT;
                //--Down people from all stages, run until all ParSecINT ecsape.
                for (int i = 0; i <= numStages.Count-1; i++)
                {
                    range = Convert.ToInt32(Math.Ceiling((peoplePerSec / (double)numStages.Count) * 2));
                    if (range < 2)
                        range++;
                    from_stage = rnd.Next(0,range);
                    if (from_stage>0 && from_stage <= listStages[numStages[i]].numPeople && (partPeopleTemp- from_stage)>=0)
                    {
                   //   Console.WriteLine("stage: " + i + " from stage: " + from_stage);
                        listStages[numStages[i]].numPeople -= from_stage;
                        partPeopleTemp -= from_stage;  
                        peopleOut += from_stage;      //Count the people out in this raund
                        peoplePerSec -= from_stage;
                        if (peoplePerSec <= 0)
                            break;
                    }
                    if (i == numStages.Count - 1)
                        if (peopleOut < PerSecINT)
                            i = -1;
                        
                }
                PerSecINT = PerSecTemp;
                Console.WriteLine("people Out: " + peopleOut + " Time: "+time);
            }
            return time;
        }

        public void RemoveEmptyStages(List<Stages> listStages, List<int> numStages)
        {
            List<int> removeList = new List<int>();
            foreach (int s in numStages) {
                if (listStages.Exists(x => x.numStage == s && x.numPeople == 0))
                    removeList.Add(s);   
            }
            foreach(int r in removeList)
                numStages.Remove(r);
        }

        static bool Contains(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i] == value)
                    return true;
            return false;
        }
        static void RandomStages(int[] randStages)
        {
            int next;
            Random rnd = new Random();
            if (randStages[0] == 0)
                for (int i = 0; i <= 11; i++)
                    randStages[i] = 14;
            for (int r = 0; r <= 11; r++)
            {
                next = 0;
                while (true)
                {
                    next = rnd.Next(0, 12);
                    if (!Contains(randStages, next))
                        break;
                }
                randStages[r] = next;
            }
        }

    }
}
