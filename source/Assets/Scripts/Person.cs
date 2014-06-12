using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Person
    {
        public BehaviorPerson CurrentType { get; set; }

        public BehaviorPerson LastType { get; set; }

        public BehaviorPerson NextType { get; set; }

        public string Name { get; set; }

        public Material Hair { get; set; }

        public Material Dorse { get; set; }

        public Material Leg { get; set; }

        public Material Foot { get; set; }

        public Transform CurrentTransform { get; set; }

        public Transform NextTransform { get; set; }

        public Transform LastTransform { get; set; }
    }
}
