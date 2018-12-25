using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour {

    [System.Serializable]
    public struct Stats
    {
        public string name;
        public int str;
        public int dex;
        public int intel;
        public int wits;
    }

    public bool hasBeenCreated = false;
    [SerializeField]
    Stats stats;
    

    public Stats AccStats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    private void Start()
    {
        Debug.Log(this.name);
        Debug.Log(hasBeenCreated);

        if (!hasBeenCreated)
        {
            generateStats();
            GiveDemBoody();
        }

    }

    void generateStats()
    {
        Stats tempStats = new Stats();

        tempStats.str = (int)Random.Range(PNJGenerator.instance.statsGiver.strMin, PNJGenerator.instance.statsGiver.strMax);
        tempStats.dex = (int)Random.Range(PNJGenerator.instance.statsGiver.dexMin, PNJGenerator.instance.statsGiver.dexMax);
        tempStats.intel = (int)Random.Range(PNJGenerator.instance.statsGiver.intelMin, PNJGenerator.instance.statsGiver.intelMax);
        //tempStats.wits = (int)Random.Range(PNJGenerator.instance.statsGiver.witsMin, PNJGenerator.instance.statsGiver.witsMax);

        stats = tempStats;
    }

    void GiveDemBoody()
    {
        GameObject hat;
        GameObject chest;
        GameObject arms;
        bool offSetOK = false;

        //str
         if (stats.str > (int)((PNJGenerator.instance.statsGiver.strMax * 66) / 100))
        {
            chest = Instantiate(PNJGenerator.instance.chest1, transform);
            offSetOK = true;
        }
        else if(stats.str > (int)((PNJGenerator.instance.statsGiver.strMax * 33) / 100))
        {
            chest = Instantiate(PNJGenerator.instance.chest2, transform);
        }
        else
        {
            chest = Instantiate(PNJGenerator.instance.chest3, transform);
        }

        //intel
        if (stats.intel > (int)((PNJGenerator.instance.statsGiver.intelMax * 66) / 100))
        {
            hat = Instantiate(PNJGenerator.instance.head1, transform);
        }
        else if (stats.intel > (int)((PNJGenerator.instance.statsGiver.intelMax * 33) / 100))
        {
            
            hat = Instantiate(PNJGenerator.instance.head2, transform);

            if (offSetOK)
            {
                hat.transform.position += new Vector3(0.0f, 0.25f, 0.0f);
            }
        }

        //dex
        if (stats.dex > (int)((PNJGenerator.instance.statsGiver.dexMax * 66) / 100))
        {
            arms = Instantiate(PNJGenerator.instance.arms1, transform);
        }
        else if (stats.dex > (int)((PNJGenerator.instance.statsGiver.dexMax * 33) / 100))
        {
            arms = Instantiate(PNJGenerator.instance.arms2, transform);
        }
        else
        {
            arms = Instantiate(PNJGenerator.instance.arms3, transform);
        }
        //this.name = "";
        stats.name = "";
        stats.name = RandomizeName(PNJGenerator.instance.nameLength);
        this.name = stats.name;
    } 

    static public string RandomizeName(int nbSyllabes = 3)
    {

        if (nbSyllabes > 6)
            nbSyllabes = 6;

        int randomizator = Random.Range(1, 3);
        bool isVoyelle = false;
        string name = "";

        if (randomizator % 2 == 0)
        {
            //this.name += PNJGenerator.instance.voyelles[(int)Random.Range(0, 6)];
            //this.name = this.name.ToUpper();
            name += PNJGenerator.instance.voyelles[(int)Random.Range(0, 6)];
            name = name.ToUpper();
            isVoyelle = true;
        }
        else
        {
            //this.name += PNJGenerator.instance.consonnes[(int)Random.Range(0, 20)];
            //this.name = this.name.ToUpper();
            name += PNJGenerator.instance.consonnes[(int)Random.Range(0, 20)]; ;
            name = name.ToUpper();
            isVoyelle = false;
        }

        for (int i = 0; i < nbSyllabes - 1; i++)
        {
            if (isVoyelle)
            {
                //this.name += PNJGenerator.instance.consonnes[(int)Random.Range(0, 20)];
                name += PNJGenerator.instance.consonnes[(int)Random.Range(0, 20)];
            }
            else
            {
                //this.name += PNJGenerator.instance.voyelles[(int)Random.Range(0, 6)];
                name += PNJGenerator.instance.voyelles[(int)Random.Range(0, 6)];
            }
            isVoyelle = !isVoyelle;
        }

        return name;
    }
}
