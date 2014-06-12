using UnityEngine;
using System.Collections;

public enum AnimationPersonBehavior
{
    Looking,
    Walking,
    Dancing
}

public enum TypePersonBehavior
{
    Normal,
    Target,
    Stalker
}

public class PersonBehavior : MonoBehaviour
{
    public AnimationPersonBehavior animationPersonBehavior = AnimationPersonBehavior.Dancing;
    public TypePersonBehavior typePersonBehavior = TypePersonBehavior.Normal;

    public NavMeshAgent navigationAgent;
    public Vector3 targetPosition;
    private float distance = 10f;
    public bool bnIsDead = false;
    //public float damping = 0.5f;
    public Animation animationBody;

    //private TextMesh sign;

    void Start()
    {
        navigationAgent = GetComponent<NavMeshAgent>();

        animationBody = GetComponentInChildren<Animation>();
        animationBody.wrapMode = WrapMode.Loop;

        //sign = GetComponentInChildren<TextMesh>();
        //sign.renderer.enabled = false;
    }

    public void Update()
    {
        Debug.DrawLine(targetPosition, transform.position, Color.green);

        if (bnIsDead)
        {
            // Stop the person movement
            StopDestination();
            return;
        }

        FindPlayer(distance);

        RaycastHit hit2;
        if (Physics.Raycast(transform.position, transform.forward, out hit2, 1f))
        {
            //Debug.Log("WAYPOINT");
            if (hit2.transform.tag == "Waypoint")
            {
                StopDestination();
                animationPersonBehavior = AnimationPersonBehavior.Dancing;
            }
        }

        if (animationPersonBehavior == AnimationPersonBehavior.Dancing)
        {
            ExecuteAnimation("Dancing", "Walking", "Looking");
        }
        else if (animationPersonBehavior == AnimationPersonBehavior.Looking)
        {
            ExecuteAnimation("Looking", "Walking", "Dancing");
        }
        else if (animationPersonBehavior == AnimationPersonBehavior.Walking)
        {
            ExecuteAnimation("Walking", "Dancing", "Looking");
        }
    }

    public bool FindPlayer(float dist)
    {
        //switch (typePersonBehavior)
        //{
        //    case TypePersonBehavior.Normal:
        //        sign.color = Color.white;
        //        break;
        //    case TypePersonBehavior.Target:
        //        sign.color = Color.green;
        //        break;
        //    case TypePersonBehavior.Stalker:
        //        sign.color = Color.red;
        //        break;
        //}

        bool founded = false;

        RaycastHit hit1;
        if (Physics.Raycast(transform.position, transform.forward, out hit1, dist))
        {
            if (hit1.transform.tag == "Player")
            {
                transform.Rotate(Vector3.up, 180f);
                //navigationAgent.updatePosition = false;
                StopDestination();

                //ExecuteAnimation("Looking", "Walking", "Dancing");
                animationPersonBehavior = AnimationPersonBehavior.Looking;

                founded = true;
            }
        }

        return founded;
    }

    private void ExecuteAnimation(string newAnimationName, string oldAnimationName1, string OldAnimationName2)
    {
        //Debug.Log("1: " + oldAnimationName1 + " 2: " + OldAnimationName2 + " 3:" + newAnimationName);
        if (animationBody.IsPlaying(oldAnimationName1) || animationBody.IsPlaying(OldAnimationName2))
        {
            animationBody.CrossFade(newAnimationName);
        }
        if (!animationBody.IsPlaying(newAnimationName))
        {
            animationBody.Play(newAnimationName);
        }
    }

    public void SetDestination(Transform target)
    {
        if (bnIsDead)
        {
            return;
        }

        if (targetPosition != target.position)
        {
            transform.LookAt(target);
            targetPosition = target.position;

            navigationAgent.updatePosition = true;
            navigationAgent.SetDestination(targetPosition);

            //ExecuteAnimation("Walking", "Dancing", "Looking");
            animationPersonBehavior = AnimationPersonBehavior.Walking;
        }
    }

    public void StopDestination()
    {
        navigationAgent.updatePosition = false;
    }

    //void OnTriggerEnter(Collider hit)
    //{
    //    if (hit.collider.tag == "Waypoint")
    //    {
    //        Debug.Log("OK");
    //        ExecuteAnimation("Dancing", "Walking", "Looking");
    //    }
    //}
}
