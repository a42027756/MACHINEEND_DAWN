using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Binder : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D myRigidbody2D;
    [HideInInspector] public Transform myTransform;
    [HideInInspector] public Animator myAnim;
    public GameObject pauspanel;
    public float speed;
    public GameObject bleeding;

    public Image health, water, hunger, invade;

    private void Update()
    {
        PlayerController.Instance.Update();
        FlashColor();
        
    }

    private void FixedUpdate()
    {
        if (PlayerController.Instance.canControl && PlayerController.Instance.canMove)
        {
            PlayerController.Instance.InputControl();
            PlayerProperty.Instance.Update();   
        }
    }

    private void Start()
    {
        PlayerController.Instance._rigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        PlayerController.Instance._transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerController.Instance._anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        PlayerController.Instance.movespeed = speed;
        PlayerController.Instance.pausepanel = pauspanel;

        PlayerProperty.Instance.healthBar = health;
        PlayerProperty.Instance.waterBar = water;
        PlayerProperty.Instance.hungerBar = hunger;
        PlayerProperty.Instance.invadeBar = invade;
        
        
        PlayerProperty.Instance.InitProperties();
    }
    
    private void FlashColor()
    {
        if (PlayerController.Instance.isUnderAttack == true)
        {
            Debug.Log("play");
            PlayerController.Instance._transform.GetComponent<SpriteRenderer>().color = Color.black;
            bleeding.GetComponentInChildren<ParticleSystem>().Play();
            StartCoroutine(Flash());
            PlayerController.Instance.isUnderAttack = false;
        }
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerController.Instance._transform.GetComponent<SpriteRenderer>().color = Color.white;
    }
    
    private void UpdatebyTenSec()
    {
        
    }
}
