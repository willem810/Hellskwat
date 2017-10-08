using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {


    public static AnimationManager instance;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayFireGun(Animation anim)
    {
        playAnim(anim, "fire", true);
    }

    public void PlayReload(Animation anim)
    {
        playAnim(anim, "reload", true);
    }

    public void PlayDraw(Animation anim)
    {
        playAnim(anim, "draw", true);
    }

    public void PlayIdle(Animation anim)
    {
       // playAnim(anim, "idle", true);
    }


    private void playAnim(Animation anim, string animName, bool force)
    {
        Debug.Log("Debugging: " + animName);
        if (anim.IsPlaying("idle")) anim.Stop();
        else if (force && anim.isPlaying) anim.Stop();
        anim.Play(animName);
    }
}
