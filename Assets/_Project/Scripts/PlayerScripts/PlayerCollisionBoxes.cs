using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionBoxes : MonoBehaviour
{
    [SerializeField] private Transform _hurtboxesGroup = default;
    [SerializeField] private Transform _hitboxesGroup = default;
    private List<Hurtbox> _hurtboxes = new List<Hurtbox>();
    private List<Hitbox> _hitboxes = new List<Hitbox>();
    private Player _player;


    void Awake()
    {
        _player = GetComponent<Player>();
        foreach (Transform child in _hurtboxesGroup)
        {
            _hurtboxes.Add(child.GetComponent<Hurtbox>());
        }
        foreach (Transform child in _hitboxesGroup)
        {
            _hitboxes.Add(child.GetComponent<Hitbox>());
        }
    }

    public void SetHurtboxes(AnimationBox[] animationBoxes)
    {
        if (!_player.Invisible)
        {
            for (int i = 0; i < _hurtboxes.Count; i++)
            {
                _hurtboxes[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < animationBoxes.Length; i++)
            {
                _hurtboxes[i].gameObject.SetActive(true);
                _hurtboxes[i].SetBox(animationBoxes[i].size, animationBoxes[i].offset);
            }
        }
    }

    public void SetHitboxes(AnimationBox[] animationBoxes)
    {
        for (int i = 0; i < _hitboxes.Count; i++)
        {
            _hitboxes[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < animationBoxes.Length; i++)
        {
            _hitboxes[i].gameObject.SetActive(true);
            _hitboxes[i].SetBox(animationBoxes[i].size, animationBoxes[i].offset);
        }
    }
}
