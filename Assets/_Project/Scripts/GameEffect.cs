using UnityEngine;
public class GameEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;


    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (SceneSettings.Bit1)
        {
            _spriteRenderer.color = Color.white;
        }
    }
}
