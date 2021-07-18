using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Animator anim;
    private bool shouldChase = false;

    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!agent.isStopped)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < 20)
            {
                if (!shouldChase)
                {
                    anim.SetTrigger("PlayerDetected");
                }
                shouldChase = true;


                agent.destination = player.transform.position;
                if (dist < 2f)
                {
                    anim.SetTrigger("AttackPlayer");
                    PlayerInteractions.Instance.GoToCheckpoint();
                    OrcRoom.Instance.GoToInitialPosition();
                    RoomB.Instance.GoToInitialPosition();
                }
            } 
        }
    }
    public void EnemeGotHit()
    {
        agent.isStopped = true;
        anim.SetTrigger("EnemyDied");
        OrcRoom.Instance.RemoveEnemy(gameObject);
        Destroy(gameObject, 1.5f);
    }
}