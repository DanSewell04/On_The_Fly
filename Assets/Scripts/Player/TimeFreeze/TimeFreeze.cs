using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Linq;

public class TimeFreeze : MonoBehaviour
{
    public float freezeDuration = 5f;
    public TMP_Text countdownText;
    private bool hasFrozenTime = false;
    private bool isFreezingTime = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !hasFrozenTime)
        {
            StartCoroutine(FreezeTime());
        }
    }

    IEnumerator FreezeTime()
    {
        hasFrozenTime = true;

        iFreezable[] freezables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<iFreezable>().ToArray();


        foreach (var obj in freezables)
        {
            obj.Freeze();
        }

        countdownText.gameObject.SetActive(true);
        float timer = freezeDuration;

        while (timer > 0f)
        {
            countdownText.text = "Time Frozen: " + timer.ToString("F1") + "s";
            yield return new WaitForSecondsRealtime(0.1f);
            timer -= 0.1f;
        }

        foreach (var obj in freezables)
        {
            obj.UnFreeze();
        }

        countdownText.gameObject.SetActive(false);
    }
}
