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
        words = _text.text;
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
