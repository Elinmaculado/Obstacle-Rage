using System.Collections;
using UnityEngine;

public class O_Intermittentwall : MonoBehaviour
{
    public float visibleTime = 3f;
    public float invisibleTime = 2f;
    public float blinkTIme = 3f;
    public Renderer rend;

    private Collider col;
    private bool isVisible = true;
    private Animator anim;

    private void Start()
    {
        col = GetComponent<Collider>();
        anim = GetComponent<Animator>();

        StartCoroutine(SwitchRoutine());
    }

    private IEnumerator SwitchRoutine()
    {
        while (true)
        {
            SetWallState(true);
            yield return new WaitForSeconds(visibleTime);

            TriggerBlink();
            yield return new WaitForSeconds(blinkTIme);

            SetWallState(false);
            yield return new WaitForSeconds(invisibleTime);

            TriggerBlink();
            yield return new WaitForSeconds(blinkTIme);
        }
    }

    private void SetWallState(bool state)
    {
        isVisible = state;
        rend.enabled = state; 
        col.enabled = state;
    }

    private void TriggerBlink()
    {
        if (anim != null)
        {
            anim.SetTrigger("Blink");
        }
    }    
}
