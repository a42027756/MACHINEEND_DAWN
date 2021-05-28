using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowText : MonoBehaviour
{
    public float characterPerSecond = 0.2f;//字符出现速度
    private string words;
    private bool isActive = false;
    private float timer;
    private Text _text;
    private int currentPos = 0;
    private Animator _animator;//场景Animator
    public Animator _spaceAnimator;//空格Animator（空格本身具有自己的Animator所以没法用场景Animator一并管理）
    private AudioSource _audio;//音效
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _audio = GetComponent<AudioSource>();
        timer = 0;
        isActive = true;
        characterPerSecond = Mathf.Max(0.2f, characterPerSecond);
        _text = GetComponent<Text>();
        words = ">>#2121年....#$%#$..ABSO械智公司已经垄断几乎所有..&*行业^%...，人类社会的贫富差距日渐增大...ABSO作为富可敌国的巨大产业{>:**..几乎不管下层人民的死活...终于...ABSO无法掩埋剥削压榨的&^$真实面目##@$人民和垄断资本展开了正面对抗。ABSO启用:(*纳米智械^&(%..入侵人体义肢以及义脑，城市中的人们变成了受机械操控的>>行尸走肉<<......幸存的人类在末世下艰难的生活............";
        _text.text = "";
    }

    private void Update()
    {
        OnStartWriter();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnFinish();
        }
    }

    public void StartEffect()
    {
        isActive = true;
    }
    
    //结束播放
    public void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        _text.text = words;
        _audio.Stop();
        StartCoroutine(WaitSwitch(1.5f));
    }
    
    //播放写入
    void OnStartWriter()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= characterPerSecond)
            {
                timer = 0;
                currentPos++;
                _text.text = words.Substring(0, currentPos);
                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }
        }
    }
    
    //延迟
    IEnumerator WaitSwitch(float time)
    {
        yield return new WaitForSeconds(time);
        _spaceAnimator.SetBool("fade",true);
        _animator.SetBool("switch",true);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Game");
    }
}
