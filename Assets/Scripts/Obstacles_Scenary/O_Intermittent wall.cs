using System.Collections;
using UnityEngine;

public class O_Intermittentwall : MonoBehaviour
{
    public GameObject objectWall;
    public float timeActive;
    private bool switchOn;

    private void Start()
    {
        switchOn = true;
        objectWall.SetActive(false);
    }

    private void Update()
    {
        if (switchOn)
        {
            StartCoroutine(StartTime());
        }
    }

    public IEnumerator StartTime()
    {
        switchOn = false;
        objectWall.SetActive(false);
        Debug.Log("Activado papu");
        yield return new WaitForSeconds (timeActive);
        objectWall.SetActive(true);
        switchOn = true;
        Debug.Log("Desactivado papu");
    }
}
