using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJGenerator : MonoBehaviour {

    public static PNJGenerator instance;

    [SerializeField]
    public GameObject vide;

    [System.Serializable]//show struct in editor
    public struct StatsMinMax
    {
        public int strMin;
        public int strMax;

        public int dexMin;
        public int dexMax;

        public int intelMin;
        public int intelMax;

        //public int witsMin;
        //public int witsMax;
    }

    //show enum in editor
    [System.Serializable]
    public enum Archetype
    {
        paysant,
        chevalier,
        prince,
        roi,
    }

    public StatsMinMax statsGiver;

    public GameObject pnj;

    public GameObject head1;
    public GameObject head2;
    //public GameObject head3;

    public GameObject chest1;
    public GameObject chest2;
    public GameObject chest3;

    public GameObject arms1;
    public GameObject arms2;
    public GameObject arms3;

    public GameObject[] spawnerPos;

    public Camera camera;

    //GameObject shoes1;
    //GameObject shoes2;
    //GameObject shoes3;
    [HideInInspector]
    public char[] consonnes;
    [HideInInspector]
    public char[] voyelles;
    public int nameLength;

    int elapsedTime;
    bool pnjCreated;
    //GameObject[] thatOnePNJ;
    List<GameObject> thatOnePNJ;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        pnjCreated = false;

        consonnes = new char[20] {  'z', 't', 'r','p', 'q', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'w', 'x', 'c', 'v', 'b', 'n' };
        voyelles = new char[6] { 'a', 'e', 'u', 'y', 'i', 'o' };
        thatOnePNJ = new List<GameObject>();
    }

	// Update is called once per frame
	void Update () {
        //elapsedTime += (int)Time.deltaTime;

        //if (elapsedTime%2 == 0)
        //{
        //    //faire pop un pnj tout beau tout plein
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (pnjCreated)
            //{
            //    Debug.Log("PNJ pop");
            //    thatOnePNJ = Instantiate(pnj);
            //    pnjCreated = true;
            //}
            //else
            //{
            //    Destroy(thatOnePNJ);
            //    thatOnePNJ = Instantiate(pnj);
            //}
            if (!pnjCreated)
            {
                for (int i = 0; i < spawnerPos.Length; i++)
                {
                    GameObject tempGo;
                    tempGo = Instantiate(pnj);
                    thatOnePNJ.Add(tempGo);
                    thatOnePNJ[i].transform.position = spawnerPos[i].transform.position;
                }
                pnjCreated = !pnjCreated;
            }
            else
            {
                for (int i = 0; i < spawnerPos.Length; i++)
                {

                    //if (thatOnePNJ[i] != null)
                    //{
                    //    thatOnePNJ.Remove(thatOnePNJ[i]);
                    //    Destroy(thatOnePNJ[i]);
                    //    //thatOnePNJ[i] = Instantiate(pnj);
                    //    //thatOnePNJ[i].transform.position = spawnerPos[i].transform.position;

                    //}
                    //else
                    //{
                    //    //GameObject tempGo;
                    //    //tempGo = Instantiate(pnj);
                    //    //thatOnePNJ.Add(tempGo);
                    //    //thatOnePNJ[i].transform.position = spawnerPos[i].transform.position;
                    //}
                    //thatOnePNJ.Remove(thatOnePNJ[i]);
                    Destroy(thatOnePNJ[i]);
                    Debug.Log(thatOnePNJ[i]);
                    GameObject tempGo;
                    tempGo = Instantiate(pnj);
                    thatOnePNJ[i] = tempGo;
                    thatOnePNJ[i].transform.position = spawnerPos[i].transform.position;

                }
            }
            

            Debug.Log("Count : " + thatOnePNJ.Count);
        }
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if (objectHit.parent != null && objectHit.parent.GetComponent<PNJ>() != null)
                {
                    TextManager.SetTexts(objectHit.parent.name, objectHit.parent.GetComponent<PNJ>().AccStats.str.ToString(), objectHit.parent.GetComponent<PNJ>().AccStats.dex.ToString(), objectHit.parent.GetComponent<PNJ>().AccStats.intel.ToString());
                }
            }
        }
	}    
}
