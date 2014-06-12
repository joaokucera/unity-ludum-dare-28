using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class CrowdBehavior : MonoBehaviour
{
    private List<GameObject> listPeople = new List<GameObject>();
    private List<GameObject> listWaypoints = new List<GameObject>();
    private GameObject player;

    void Awake()
    {
        GameObject[] gameObjectTagPerson = GameObject.FindGameObjectsWithTag("Person");
        listPeople = gameObjectTagPerson.ToList();

        GameObject[] gameObjectTagWaypoint = GameObject.FindGameObjectsWithTag("Waypoint");
        listWaypoints = gameObjectTagWaypoint.ToList();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        if (listPeople.Count > 0 && listWaypoints.Count > 0)
        {
            for (int i = 5; i <= 20; i += 5)
            {
                InvokeRepeating("DoCrowdBehavior", i, i);
            }
        }
    }

    private void DoCrowdBehavior()
    {
        bool stalkerFindind = false;

        var total = Random.Range(1, listPeople.Count / 3);
        List<int> listIndexes = new List<int>();

        do
        {
            var indexPerson = Random.Range(1, listPeople.Count);
            if (!listIndexes.Contains(indexPerson))
            {
                listIndexes.Add(indexPerson);
            }
        }
        while (listIndexes.Count < total);

        foreach (var index in listIndexes)
        {
            var indexWaypoint = Random.Range(0, listWaypoints.Count);

            PersonBehavior script = listPeople[index].GetComponent<PersonBehavior>();

            if (script.typePersonBehavior == TypePersonBehavior.Normal)
            {
                script.SetDestination(listWaypoints[indexWaypoint].transform);
            }
            else if (script.typePersonBehavior == TypePersonBehavior.Stalker)
            {
                //if (script.FindPlayer(30f))
                //{
                script.SetDestination(player.transform);
                stalkerFindind = true;
                //}
                //else
                //{
                //    listPeople[index].GetComponent<PersonBehavior>().SetDestination(listWaypoints[indexWaypoint].transform);
                //}
            }
            //else if (script.typePersonBehavior == TypePersonBehavior.Target)
            //{
            //    if (script.FindPlayer(20f))
            //    {
            //        listPeople[index].GetComponent<PersonBehavior>().SetDestination(player.transform);
            //    }
            //    else
            //    {
            //        listPeople[index].GetComponent<PersonBehavior>().SetDestination(listWaypoints[indexWaypoint].transform);
            //    }
            //}
        }

        //if (!stalkerFindind)
        //{
        //    var stalker = listPeople.Where(p => p.GetComponent<PersonBehavior>().typePersonBehavior == TypePersonBehavior.Stalker).FirstOrDefault();

        //    if (stalker != null)
        //    {
        //        stalker.GetComponent<PersonBehavior>().SetDestination(player.transform);
        //    }
        //}
    }
}