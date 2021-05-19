using UnityEngine;
using UnityEngine.UI;

public class Binder : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D myRigidbody2D;
    [HideInInspector] public Transform myTransform;
    [HideInInspector] public Animator myAnim;
    public GameObject myBag;
    public GameObject pauspanel;
    public float speed;

    public Image health, water, hunger, invade;

    private void Update()
    {
        PlayerController.Instance.Update();
        
    }

    private void FixedUpdate()
    {
        PlayerController.Instance.InputControl();
        PlayerProperty.Instance.Update();
    }

    private void Start()
    {
        PlayerController.Instance._rigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        PlayerController.Instance._transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerController.Instance._anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        PlayerController.Instance.movespeed = speed;
        PlayerController.Instance._bag = myBag;
        PlayerController.Instance.pausepanel = pauspanel;
        myBag.SetActive(false);

        PlayerProperty.Instance.healthBar = health;
        PlayerProperty.Instance.waterBar = water;
        PlayerProperty.Instance.hungerBar = hunger;
        PlayerProperty.Instance.invadeBar = invade;
        
        
        PlayerProperty.Instance.InitProperties();
    }

    private void UpdatebyTenSec()
    {
        
    }
}
