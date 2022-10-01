using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    [SerializeField] private FrameEditor[] _frames = default;
    [SerializeField] private TMP_Dropdown _characterDropdown = default;
    [SerializeField] private TMP_Dropdown _spriteDropdown = default;
    [SerializeField] private TMP_Dropdown _skinDropdown = default;
    [SerializeField] private Toggle _loopToggle = default;
    [SerializeField] private Button _playButton = default;
    [SerializeField] private SpriteRenderer _characterSpriteRenderer = default;
    private AnimationSO[] _animations;
    List<string> _characterDropOptions = new List<string>();
    List<string> _spriteDropdownOptions = new List<string>();
    List<string> _skinDropdownOptions = new List<string>();


    void Awake()
    {
        _animations = Resources.LoadAll<AnimationSO>("");
        _characterDropdown.ClearOptions();
        _spriteDropdown.ClearOptions();
        for (int i = 0; i < _animations.Length; i++)
        {
            _characterDropOptions.Add(_animations[i].name);
        }
        for (int i = 0; i < _animations[0].animationCelsGroup.Length; i++)
        {
            _spriteDropdownOptions.Add(_animations[0].animationCelsGroup[i].celName);
        }
        for (int i = 0; i < 5; i++)
        {
            _skinDropdownOptions.Add(i.ToString());
        }
        _spriteDropdown.AddOptions(_spriteDropdownOptions);
        _characterDropdown.AddOptions(_characterDropOptions);
        SetFrames();
        _characterDropdown.onValueChanged.AddListener(delegate
        {
            AnimationEnded();
            _characterSpriteRenderer.sprite = _animations[_characterDropdown.value].GetSprite(_skinDropdown.value, _spriteDropdown.value, _cel);
        });
        _spriteDropdown.onValueChanged.AddListener(delegate
        {
            AnimationEnded();
            SetFrames();
            _characterSpriteRenderer.sprite = _animations[_characterDropdown.value].GetSprite(_skinDropdown.value, _spriteDropdown.value, _cel);
        });
        _loopToggle.onValueChanged.AddListener(delegate
        {
            if (_loopToggle.isOn)
            {
                _isPaused = false;
            }
        });
        _playButton.onClick.AddListener(delegate
        {
            _isPlayOn = !_isPlayOn;
        });
    }



    private int _frame;
    private int _cel;
    private int _skin;
    private bool _isPaused;
    private bool _isPlayOn = true;
    void FixedUpdate()
    {
        if (!_isPaused && _isPlayOn)
        {
            if (_frame == _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).frames)
            {
                _cel++;
                if (_cel > _animations[_characterDropdown.value].GetGroup(_spriteDropdown.value).animationCel.Length - 1)
                {
                    AnimationEnded();
                    if (!_animations[_characterDropdown.value].GetGroup(_spriteDropdown.value).loop)
                    {
                        return;
                    }
                }
                _frame = 0;
            }
            _characterSpriteRenderer.sprite = _animations[_characterDropdown.value].GetSprite(_skinDropdown.value, _spriteDropdown.value, _cel);
            _frame++;
        }
    }
    private void AnimationEnded()
    {
        if (!_loopToggle.isOn)
        {
            _isPaused = true;
        }
        _frame = 0;
        _cel = 0;
    }

    private void SetFrames()
    {
        for (int i = 0; i < _frames.Length; i++)
        {
            _frames[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Length; i++)
        {
            _frames[i].gameObject.SetActive(true);
            _frames[i].SetDuration(_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[i].frames);
            if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[i].active)
            {
                _frames[i].SetImage(Color.red);
            }
            else
            {
                bool isPriorFrameActive = false;
                for (int j = 0; j < _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Length; j++)
                {
                    if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[j].active && j < i)
                    {
                        isPriorFrameActive = true;
                    }
                }
                if (!isPriorFrameActive)
                {
                    _frames[i].SetImage(Color.green);
                }
                else
                {
                    _frames[i].SetImage(Color.blue);
                }
            }
        }
    }
}
