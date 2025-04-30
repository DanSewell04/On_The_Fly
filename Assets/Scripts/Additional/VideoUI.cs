using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoUI : MonoBehaviour
{
    public VideoPlayer player;
    public RawImage rawImage;
    public RenderTexture renderTexture;

    private void Start()
    {
        renderTexture = new RenderTexture(1920, 1080, 0);
        renderTexture.Create();

        player.targetTexture = renderTexture;
        rawImage.texture = renderTexture;
        player.Play();
    }
}
