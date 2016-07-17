using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenFader : MonoBehaviour
{
    public bool isFading = false;

    public Animator anim;
    public PlayerMovement player;
    public Octorok[] octoroks;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}

    public IEnumerator FadeToClear()
    {
        octoroks = GetComponents<Octorok>();
        isFading = true;
        ToggleMovement(false);
        anim.SetTrigger("FadeIn");

        while (isFading)
            yield return null;
    }

    public IEnumerator FadeToBlack()
    {
        octoroks = GetComponents<Octorok>();
        isFading = true;
        ToggleMovement(false);
        anim.SetTrigger("FadeOut");

        while (isFading)
            yield return null;
    }
	
	void AnimationComplete()
    {
        octoroks = GetComponents<Octorok>();
        isFading = false;
        ToggleMovement(true);

    }

    private void ToggleMovement(bool newMovement)
    {

        player.canMove = newMovement;
        for (int i = 0; i < octoroks.Length; i++)
        {
            if (newMovement == true)
            {
                octoroks[i].canMove = true;
            }
            else
            {
                octoroks[i].canMove = false;
            }
            
        }
    }
}
