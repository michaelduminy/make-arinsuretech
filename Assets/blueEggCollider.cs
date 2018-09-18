using UnityEngine;
using System.Collections;
using Vuforia;
public class blueEggCollider : MonoBehaviour, IVirtualButtonEventHandler
{

    public GameObject startBtnObj;

    enum State { bomb, coin };

    private State state;
    private bool isOpen = false;

    // Use this for initialization
    void Start()
    {
        startBtnObj = GameObject.Find("StartButton");
        var behaviours = startBtnObj.GetComponents(typeof(VirtualButtonBehaviour));
        var behaviour = startBtnObj.GetComponent<VirtualButtonBehaviour>();
        startBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        state = State.coin;
    }

    void ChangeState()
    {
        var rand = Random.value * 2;
        if (rand < 1)
        {
            state = State.bomb;
            Debug.Log("bomb");
        }
        else
        {
            state = State.coin;
            Debug.Log("coin");
        }
    }

    IEnumerator animateChest() {
        GetComponent<Animation>().Play("box_open");
        yield return new WaitForSeconds(1);
        GetComponent<Animation>().Play("box_close");
    }


    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(animateChest());
        
        if (collision.gameObject.name == "PT_Medieval_Peasant_01_a")
        {
            if (state == State.bomb)
            {
                GameState.DecreaseRate(1.2f);
                ChangeState();
                StartCoroutine(animateExplosion());
            }
            else if (state == State.coin)
            {
                GameState.IncreaseMoney(10);
                ChangeState();
                StartCoroutine(animateGlow());
            }

        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("virtual button pressed");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("virtual button released");
    }

    IEnumerator animateGlow()
    {
        var exp = GameObject.Find("glowEffect").GetComponent<ParticleSystem>();
        exp.Play();
        yield return new WaitForSeconds(1);
        exp.Stop();
    }

    IEnumerator animateExplosion()
    {
        var exp = GameObject.Find("bombPartical").GetComponent<ParticleSystem>();
        exp.Play();
        yield return new WaitForSeconds(1);
        exp.Stop();
    }
}
