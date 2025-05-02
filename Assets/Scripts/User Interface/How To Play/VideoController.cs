using UnityEngine;
using UnityEngine.UI;


public class VideoController : MonoBehaviour
{
    public RawImage[] pages; 
    private int currentIndex = 0;

    void Start()
    {
        ShowPage(currentIndex);
    }

    public void NextPage()
    {
        if (currentIndex < pages.Length - 1)
        {
            currentIndex++;
            ShowPage(currentIndex);
        }
    }

    public void PrevPage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowPage(currentIndex);
        }
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(i == index);
        }
    }


}
