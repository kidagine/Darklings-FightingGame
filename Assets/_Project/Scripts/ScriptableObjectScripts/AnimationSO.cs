using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
[CreateAssetMenu(fileName = "AnimationSO", menuName = "Darklings-FightingGame/AnimationSO", order = 0)]
public class AnimationSO : ScriptableObject
{
    [SerializeField]
    public SpriteAtlas[] spriteAtlas;
    [SerializeField]
    public AnimationCelsGroup[] animationCelsGroup;

    public Sprite GetSprite(int skin, int group, int cel)
    {
        if (spriteAtlas.Length > 0)
        {
            return spriteAtlas[skin].GetSprite(animationCelsGroup[group].animationCel[cel].sprite.name);
        }
        else
        {
            return animationCelsGroup[group].animationCel[cel].sprite;
        }
    }

    public AnimationCelsGroup GetGroup(int group)
    {
        return animationCelsGroup[group];
    }

    public AnimationCel GetCel(int group, int cel)
    {
        try
        {
            return animationCelsGroup[group].animationCel[cel];
        }
        catch (System.Exception)
        {
            Debug.Log(group + "|" + cel);
            return animationCelsGroup[group].animationCel[0];
        }
    }

    public int GetGroupId(string name)
    {
        for (int i = 0; i < animationCelsGroup.Length; i++)
        {
            if (animationCelsGroup[i].celName == name)
            {
                return i;
            }
        }
        return 0;
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        // EditorUtility.SetDirty(this);
        // AssetDatabase.SaveAssets();
#endif
    }
}

[Serializable]
public struct AnimationCelsGroup
{
    public string celName;
    public bool loop;
    public CameraShakerSO cameraShake;
    public List<AnimationCel> animationCel;
}

[Serializable]
public class AnimationCel
{
    public int frames;
    public Sprite sprite;
    public AnimationEvent animationEvent;
    public List<AnimationBox> hitboxes;
    public List<AnimationBox> hurtboxes;
}

[Serializable]
public class AnimationEvent
{
    public bool jump;
    [ShowIf("jump")]
    public Vector2 jumpDirection;
    public bool footstep;
    public bool parry;
    public bool projectile;
    [ShowIf("projectile")]
    public Vector2 projectilePoint;
    public bool invisibile;
    public bool throwEnd;
    public bool throwArcanaEnd;
    public Vector2 grabPoint;
}

[Serializable]
public class AnimationBox
{
    public Vector2 size;
    public Vector2 offset;
}