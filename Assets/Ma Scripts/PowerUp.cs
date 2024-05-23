using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float powerUpDuration;
    [SerializeField] TextMeshProUGUI powerUpDurationText;

    float twoTimesMultiplier = 2f;
    float scaleShrinkXZ = .5f;
    float scaleShrinkY = .25f;

    bool isSmaller = false;
    bool isPoweredUp = false;

     void Update()
     {
        if(isPoweredUp)
        {
            powerUpDuration -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(powerUpDuration / 60);
            int seconds = Mathf.FloorToInt(powerUpDuration % 60);
            powerUpDurationText.text = string.Format("{00}", seconds);
        }

        if(powerUpDuration <= 0)
        {
            isPoweredUp = false;
            powerUpDurationText.text = " ";
        }
     }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && gameObject.tag == "Jump Boost")
        {
            StartCoroutine(JumpBoost(other));
        }

        if (other.CompareTag("Player") && gameObject.tag == "Speed Boost")
        {
            StartCoroutine(SpeedBoost(other));
        }

        if (other.CompareTag("Player") && gameObject.tag == "Low Gravity")
        {
            StartCoroutine(LowGravity(other));
        }

        if (other.CompareTag("Player") && gameObject.tag == "Shrink")
        {
            if (isSmaller == true)
            {
                return;
            }
            StartCoroutine(Shrink(other));
        }
    }

    IEnumerator JumpBoost(Collider player)
    {
        isPoweredUp = true;
        PlayerMovement stats = player.GetComponent<PlayerMovement>();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        stats.jumpHeight *= twoTimesMultiplier;
        yield return new WaitForSeconds(powerUpDuration);
        stats.jumpHeight /= twoTimesMultiplier;
        Destroy(gameObject);
    }

    IEnumerator SpeedBoost(Collider player)
    {
        isPoweredUp = true;
        PlayerMovement stats = player.GetComponent<PlayerMovement>();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        stats.speed *= twoTimesMultiplier;
        yield return new WaitForSeconds(powerUpDuration);
        stats.speed /= twoTimesMultiplier;
        Destroy(gameObject);
    }

    IEnumerator LowGravity(Collider player)
    {
        isPoweredUp = true;
        PlayerMovement stats = player.GetComponent<PlayerMovement>();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        stats.gravity /= twoTimesMultiplier;
        stats.gravity2 /= twoTimesMultiplier;
        yield return new WaitForSeconds(powerUpDuration);
        stats.gravity *= twoTimesMultiplier;
        stats.gravity2 *= twoTimesMultiplier;
        Destroy(gameObject);
    }

    IEnumerator Shrink(Collider player)
    {
        isPoweredUp = true;
        PlayerMovement stats = player.GetComponent<PlayerMovement>();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        stats.transform.localScale -= new Vector3(scaleShrinkXZ, scaleShrinkY, scaleShrinkXZ);
        isSmaller = true;
        yield return new WaitForSeconds(powerUpDuration);
        stats.transform.localScale += new Vector3(scaleShrinkXZ, scaleShrinkY, scaleShrinkXZ);
        isSmaller = false;
        Destroy(gameObject);
    }
}
