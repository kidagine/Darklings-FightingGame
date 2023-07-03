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
    [SerializeField] private TMP_Dropdown _boxesDropdown = default;
    [SerializeField] private TMP_Dropdown _typeDropdown = default;
    [SerializeField] private Toggle _loopToggle = default;
    [SerializeField] private Toggle _boxesToggle = default;
    [SerializeField] private TMP_InputField _sizeXInputField = default;
    [SerializeField] private TMP_InputField _sizeYInputField = default;
    [SerializeField] private TMP_InputField _offsetXInputField = default;
    [SerializeField] private TMP_InputField _offsetYInputField = default;
    [SerializeField] private Button _playButton = default;
    [SerializeField] private SpriteRenderer _characterSpriteRenderer = default;
    private AnimationSO[] _animations;
    private List<string> _characterDropOptions = new List<string>();
    private List<string> _spriteDropdownOptions = new List<string>();
    private List<string> _skinDropdownOptions = new List<string>();
    private List<string> _boxesDropdownOptions = new List<string>();


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
            _typeDropdown.value = 0;
            AnimationEnded();
            UpdateBoxesFields();
            SetFrames();
            _characterSpriteRenderer.sprite = _animations[_characterDropdown.value].GetSprite(_skinDropdown.value, _spriteDropdown.value, _cel);
            _skinDropdown.ClearOptions();
            _spriteDropdown.ClearOptions();
            _skinDropdownOptions.Clear();
            _spriteDropdownOptions.Clear();
            for (int i = 0; i < _animations[_characterDropdown.value].spriteAtlas.Length; i++)
            {
                _skinDropdownOptions.Add(i.ToString());
            }
            for (int i = 0; i < _animations[_characterDropdown.value].animationCelsGroup.Length; i++)
            {
                _spriteDropdownOptions.Add(_animations[_characterDropdown.value].animationCelsGroup[i].celName);
            }
            _spriteDropdown.AddOptions(_spriteDropdownOptions);
            _skinDropdown.AddOptions(_skinDropdownOptions);
        });
        _spriteDropdown.onValueChanged.AddListener(delegate
        {
            _typeDropdown.value = 0;
            AnimationEnded();
            SetFrames();
            UpdateBoxesFields();
            _characterSpriteRenderer.sprite = _animations[_characterDropdown.value].GetSprite(_skinDropdown.value, _spriteDropdown.value, _cel);
            _frames[_cel].EnableFrameSelected();
        });
        _boxesDropdown.onValueChanged.AddListener(delegate
        {
            UpdateBoxesFields();
        });
        _typeDropdown.onValueChanged.AddListener(delegate
        {
            UpdateBoxesFields();
        });
        _loopToggle.onValueChanged.AddListener(delegate
        {
            if (_loopToggle.isOn)
            {
                _isPaused = false;
            }
        });
        TrainingSettings.ShowHitboxes = _boxesToggle.isOn;
        _boxesToggle.onValueChanged.AddListener(delegate
        {
            TrainingSettings.ShowHitboxes = _boxesToggle.isOn;
        });
        _playButton.onClick.AddListener(delegate
        {
            _isPlayOn = !_isPlayOn;
            _playButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _isPlayOn ? "Pause" : "Play";
        });
    }


    private AnimationBox _savedBox;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftShift))
        {
            _savedBox = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes[_boxesDropdown.value];
        }
        if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftShift))
        {
            if (_savedBox != null)
            {
                if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes.Count > 0)
                {
                    _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes[_boxesDropdown.value] = new AnimationBox() { size = _savedBox.size, offset = _savedBox.offset };
                    UpdateBoxesFields();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            _isPlayOn = false;
            _playButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _isPlayOn ? "Pause" : "Play";
            _cel--;
            if (_cel < 0)
            {
                _cel = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Count - 1;
            }
            GoToFrame(_cel);
        }
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            _isPlayOn = false;
            _playButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _isPlayOn ? "Pause" : "Play";
            _cel++;
            if (_cel > _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Count - 1)
            {
                _cel = 0;
            }
            GoToFrame(_cel);
        }
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            _isPlayOn = false;
            _playButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _isPlayOn ? "Pause" : "Play";
            _spriteDropdown.value = _spriteDropdown.value - 1;
        }
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            _isPlayOn = false;
            _playButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _isPlayOn ? "Pause" : "Play";
            _spriteDropdown.value = _spriteDropdown.value + 1;
        }
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
            _frames[_cel].EnableFrameSelected();
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
        UpdateBoxesFields();
    }

    public void UpdateAnimationBoxSizeX(string value)
    {
        float valueFixed = 0;
        if (float.TryParse(value, out valueFixed))
        {
            if (_typeDropdown.value == 0)
            {
                _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hurtboxes[_boxesDropdown.value].size.x = float.Parse(value);
            }
            else
            {
                if (_animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hitboxes.Count > 0)
                {
                    _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hitboxes[_boxesDropdown.value].size.x = float.Parse(value);
                }
                else
                {
                    _characterDropdown.value = 0;
                }
            }
        }
    }
    public void UpdateAnimationBoxSizeY(string value)
    {
        float valueFixed = 0;
        if (float.TryParse(value, out valueFixed))
        {
            if (_typeDropdown.value == 0)
            {
                _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hurtboxes[_boxesDropdown.value].size.y = float.Parse(value);
            }
            else
            {
                _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hitboxes[_boxesDropdown.value].size.y = float.Parse(value);
            }
        }
    }
    public void UpdateAnimationBoxOffsetX(string value)
    {
        float valueFixed = 0;
        if (float.TryParse(value, out valueFixed))
        {
            if (_typeDropdown.value == 0)
            {
                _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hurtboxes[_boxesDropdown.value].offset.x = valueFixed;
            }
            else
            {
                _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hitboxes[_boxesDropdown.value].offset.x = valueFixed;
            }
        }
    }
    public void UpdateAnimationBoxOffsetY(string value)
    {
        float valueFixed = 0;
        if (float.TryParse(value, out valueFixed))
        {
            if (_typeDropdown.value == 0)
            {
                _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hurtboxes[_boxesDropdown.value].offset.y = float.Parse(value);
            }
            else
            {
                _animations[_characterDropdown.value].GetCel(_spriteDropdown.value, _cel).hitboxes[_boxesDropdown.value].offset.y = float.Parse(value);
            }
        }
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
            if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[i].hitboxes?.Count > 0)
            {
                _frames[i].SetImage(Color.red);

            }
            else
            {
                bool isPriorFrameActive = false;
                for (int j = 0; j < _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel.Count; j++)
                {
                    if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[j].hitboxes?.Count > 0 && j < i)
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

    private void UpdateBoxesFields()
    {
        if (_typeDropdown.value == 0)
        {
            if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes.Count > 0)
            {
                _sizeXInputField.text = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes[0].size.x.ToString();
                _sizeYInputField.text = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes[0].size.y.ToString();
                _offsetXInputField.text = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes[0].offset.x.ToString();
                _offsetYInputField.text = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes[0].offset.y.ToString();
            }
        }
        else
        {
            if (_animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hitboxes.Count > 0)
            {
                _sizeXInputField.text = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hitboxes[0].size.x.ToString();
                _sizeYInputField.text = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hitboxes[0].size.y.ToString();
                _offsetXInputField.text = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hitboxes[0].offset.x.ToString();
                _offsetYInputField.text = _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hitboxes[0].offset.y.ToString();
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
        _cel = cel;
        _characterSpriteRenderer.sprite = _animations[_characterDropdown.value].GetSprite(_skinDropdown.value, _spriteDropdown.value, _cel);
        _isPlayOn = false;
        _playButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _isPlayOn ? "Pause" : "Play";
        _boxesDropdown.ClearOptions();
        _boxesDropdownOptions.Clear();
        for (int i = 0; i < _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes.Count; i++)
        {
            _boxesDropdownOptions.Add(i.ToString());
        }
        _frames[_cel].EnableFrameSelected();
        _boxesDropdown.AddOptions(_boxesDropdownOptions);
        UpdateBoxesFields();
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

    public void CreateHurtbox()
    {
        AnimationBox hurtbox = new AnimationBox();
        hurtbox.size = new Vector2(1, 1);
        _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hurtboxes.Add(hurtbox);
    }

    public void CreateHitbox()
    {
        AnimationBox hitbox = new AnimationBox();
        hitbox.size = new Vector2(1, 1);
        _animations[_characterDropdown.value].animationCelsGroup[_spriteDropdown.value].animationCel[_cel].hitboxes.Add(hitbox);
        SetFrames();
    }

    public void LoadFightScene()
    {
        SceneSettings.PlayerOne = 0;
        SceneManager.LoadScene(2);
    }
}
