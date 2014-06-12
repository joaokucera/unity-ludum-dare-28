using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GeneralExtensions : MonoBehaviour {

    public IList<T> GetAllEntities<T>(string tag)
    {
        IList<T> list = new List<T>();

        //GameObject[] gameObejctTagPerson = GameObject.FindGameObjectsWithTag(tag);
        
        //foreach (var item in gameObejctTagPerson)
        //{
        //    list.Add(new T());
        //    {
        //        Name = item.name,
        //        CurrentTransform = item.transform,
        //        NextTransform = item.transform,
        //        LastTransform = item.transform
        //    });
        //}

        return list;
    }
}
