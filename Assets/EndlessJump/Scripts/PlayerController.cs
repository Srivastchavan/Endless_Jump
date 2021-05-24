using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpDist = 1;
    public float jumpTime = 0.4f;
    public float colliderDistCheck = 1;
    public bool isIdle = true;
    public bool isHit = false;
    public bool isJumping = false;
    public bool isMoving = false;
    public bool startJump = false;
    public ParticleSystem particle = null;
    public GameObject chick = null;

    public ParticleSystem splash = null;
    public bool parentedToObject = false;

    public AudioClip audioIdle1 = null;
    public AudioClip audioIdle2 = null;
    public AudioClip audioHop = null;
    public AudioClip audioHit = null;
    public AudioClip audioSplash = null;

    private Renderer renderer = null;
    private bool isVisible = false;


    private void Start()
    {
        renderer = chick.GetComponent<Renderer>();
    }
    private void Update()
    {
        if (!Manager.instance.checkCanPlay())
        {
            return;
        }

        if (isHit)
        {
            return;
        }
        canIdle();
        canMove();
        checkVisible();

    }
    void canIdle()
    {
        if (isIdle)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                checkIfIdle(270, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                checkIfIdle(270, 180, 0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                checkIfIdle(270, -90, 0);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                checkIfIdle(270, 90, 0);
            }
        }
    }

    void checkIfIdle(float x, float y, float z)
    {
        chick.transform.rotation = Quaternion.Euler(x, y, z);

        checkIfMoveable();

        int a = Random.Range(0, 12);
        if (a < 4) playAudioClip(audioIdle1);
    }

    void checkIfMoveable()
    {
        // find if collision occurs
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -chick.transform.up, out hit, colliderDistCheck);

        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red,2);
        if(hit.collider == null)
        {
            setMove();
        }
        else
        {
            if(hit.collider.tag == "collider")
            {
                Debug.Log("Hit Something with Collider tag");
                isIdle = true;
            }
            else
            {
                setMove();
            }
        }
        
    }
    void setMove()
    {
        Debug.Log("Hit Nothing, Keep Moving");

        isIdle = false;
        isMoving = true;
        startJump = true;
    }
    void canMove()
    {
        if (isMoving)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + jumpDist));
                setMoveForwardState();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - jumpDist));
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                moving(new Vector3(transform.position.x - jumpDist, transform.position.y, transform.position.z));
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                moving(new Vector3(transform.position.x + jumpDist, transform.position.y, transform.position.z));
            }
        }

    }
    void moving(Vector3 pos)
    {
        isIdle = false;
        isMoving = false;
        isJumping = true;
        startJump = false;

        playAudioClip(audioHop);
        LeanTween.move(this.gameObject, pos, jumpTime).setOnComplete(moveComplete);
    }
    void moveComplete()
    {
        isJumping = false;
        isIdle = true;

        int r = Random.Range(0, 12);
        if (r < 4)
        {
            playAudioClip(audioIdle2);
        }
    }

    void setMoveForwardState()
    {
        Manager.instance.updateDistCount();
    }
    void checkVisible()
    {
        if (renderer.isVisible)
        {
            isVisible = true;
        }
        if(!renderer.isVisible && isVisible)
        {
            Debug.Log("Player Offscreen, apply gotHit()");
            gotHit();
        }
    }
    public void gotHit()
    {
        isHit = true;
        ParticleSystem.EmissionModule emi = particle.emission;
        emi.enabled = true;

        playAudioClip(audioHit);

        Manager.instance.gameOver();
    }

    void playAudioClip(AudioClip audio)
    {
        this.GetComponent<AudioSource>().PlayOneShot(audio);
    }

    public void gotSoaked()
    {
        isHit = true;
        ParticleSystem.EmissionModule em = splash.emission;
        em.enabled = true;

        playAudioClip(audioSplash);

        chick.SetActive(false);

        Manager.instance.gameOver();
    }



}
