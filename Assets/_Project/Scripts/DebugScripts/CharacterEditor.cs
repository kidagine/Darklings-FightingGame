using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterEditor : MonoBehaviour
{
    [SerializeField] private FrameEditor[] _frames = default;
    [SerializeField] private TMP_Dropdown _characterDropdown = default;
    [SerializeField] private TMP_Dropdown _spriteDropdown = default;
    [SerializeField] private TMP_Dropdown _skinDropdown = default;
    [SerializeField] private Toggle _loopToggle = default;
    [SerializeField] private Toggle _boxesToggle = default;
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
        _skinDropdown.ClearOptions();
        for (int i = 0; i < _animations.Length; i++)
        {
            _characterDropOptions.Add(_animations[i].name);
        }
        for (int i = 0; i < _animations[0].animationCelsGroup.Length; i++)
        {
            _spriteDropdownOptions.Add(_animations[0].animationCelsGroup[i].celName);
        }
        for (int i = 0; i < _animations[0].spriteAtlas.Length; i++)
        {
            _skinDropdownOptions.Add(i.ToString());
        }
        _skinDropdown.AddOptions(_skinDropdownOptions);
        _spriteDropdown.AddOptions(_spriteDropdownOptions);
        _characterDropdown.AddOptions(_characterDropOptions);
        SetFrames();
        _characterDropdown.onValueChanged.AddListener(delegate
        {
            AnimationEnded();
            _characterSpriteRenderer.sprite = _animations[_characterDropdown.value].GetSprite(_skinDropdown.value, _spriteDropdown.value, _cel);
            _skinDropdown.ClearOptions();
            _skinDropdownOptions.Clear();
            for (int i = 0; i < _animations[_characterDropdown.value].spriteAtlas.Length; i++)
            {
                _skinDropdownOptions.Add(i.ToString());
            }
            _skinDropdown.AddOptions(_skinDropdownOptions);
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
        _boxesToggle.onValueChanged.AddListener(delegate
        {
            TrainingSettings.ShowHitboxes = !TrainingSettings.ShowHitboxes;
        });
        _playButton.onClick.AddListener(delegate
        {
            _isPlayOn = !_isPlayOn;
            _playButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _isPlayOn ? "Pause" : "Play";
        });
    }



    private int _frame;
    private int _cel;
    private bool _isPaused;
    private bool _isPlayOn = true;
    void FixedUpdate()
    {
        if (!_isPaused && _isPlayOn)
        {
            if (_frame == _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).frames)
            {
                _cel++;
                if (_cel > _animations[_characterDropdown.value].GetGroup(_spriteDropdown.value).animationCel.Count - 1)
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

    private void CheckAnimationBoxes()
    {
        // if (_animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hitboxes.Length > 0)
        // {
        //     _player.SetHitbox(true, _animation.GetCel(_group, _cel).hitboxes[0]);
        //     _player.CreateEffect(false);
        // }
        // else
        // {
        //     _player.SetHitbox(false);
        // }
    }


    private void SetFrames()
    {
        for (int i = 0; i < _frames.Length; i++)
        {
            _frames[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Count; i++)
        {
            _frames[i].gameObject.SetActive(true);
            _frames[i].SetDuration(_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[i].frames);
            if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[i].hitboxes.Length > 0)
            {
                _frames[i].SetImage(Color.red);
            }
            else
            {
                bool isPriorFrameActive = false;
                for (int j = 0; j < _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Count; j++)
                {
                    if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[j].hitboxes.Length > 0 && j < i)
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

    public void SetFrameDuration(int cel, int duration)
    {
        _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[cel].frames = duration;
        AnimationEnded();
    }

    public void GoToFrame(int cel)
    {
        _characterSpriteRenderer.sprite = _animations[_characterDropdown.value].GetSprite(_skinDropdown.value, _spriteDropdown.value, cel);
        _isPlayOn = false;
        _playButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _isPlayOn ? "Pause" : "Play";
    }

    public void DeleteFrame(int cel)
    {
        AnimationCel animationCel = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[cel];
        _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Remove(animationCel);
        SetFrames();
        AnimationEnded();
    }

    public void AddFrame()
    {
        AnimationCel animationCel = new AnimationCel();
        animationCel.sprite = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Count - 1].sprite;
        animationCel.frames = 1;
        _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Add(animationCel);
        SetFrames();
    }

    public void LoadFightScene()
    {
        SceneSettings.PlayerOne = 0;
        SceneManager.LoadScene(3);
    }
}
