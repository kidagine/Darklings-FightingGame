using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct GameSimulation : IGame
{
    public int Framenumber { get; private set; }
    public static int Timer { get; set; }
    public static int GlobalHitstop { get; set; }
    public static int Hitstop { get; set; }
    public static int IntroFrame { get; set; }
    public static bool Skip { get; set; }
    public static bool Start { get; set; }
    public static bool Run { get; set; }
    public int Checksum => GetHashCode();
    public static bool _introPlayed;
    public static PlayerNetwork[] _players;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(Framenumber);
        bw.Write(GlobalHitstop);
        bw.Write(Timer);
        bw.Write(Hitstop);
        bw.Write(IntroFrame);
        bw.Write(Start);
        bw.Write(Run);
        bw.Write(_players.Length);
        for (int i = 0; i < _players.Length; ++i)
        {
            _players[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        Framenumber = br.ReadInt32();
        GlobalHitstop = br.ReadInt32();
        Timer = br.ReadInt32();
        Hitstop = br.ReadInt32();
        IntroFrame = br.ReadInt32();
        Start = br.ReadBoolean();
        Run = br.ReadBoolean();
        int length = br.ReadInt32();
        if (length != _players.Length)
        {
            _players = new PlayerNetwork[length];
        }
        for (int i = 0; i < _players.Length; ++i)
        {
            _players[i].Deserialize(br);
        }
    }

    public NativeArray<byte> ToBytes()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                Serialize(writer);
            }
            return new NativeArray<byte>(memoryStream.ToArray(), Allocator.Persistent);
        }
    }

    public void FromBytes(NativeArray<byte> bytes)
    {
        using (var memoryStream = new MemoryStream(bytes.ToArray()))
        {
            using (var reader = new BinaryReader(memoryStream))
            {
                Deserialize(reader);
            }
        }
    }

    public GameSimulation(PlayerStatsSO[] playerStats, AssistStatsSO[] assistStats)
    {
        GlobalHitstop = 1;
        Framenumber = 0;
        Timer = 99;
        Hitstop = 0;
        IntroFrame = 880;
        _introPlayed = false;
        _players = new PlayerNetwork[playerStats.Length];
        ObjectPoolingManager.Instance.PoolInitialize(playerStats[0]._effectsLibrary, playerStats[1]._effectsLibrary);
        ObjectPoolingManager.Instance.PoolProjectileInitialize(playerStats[0]._projectilesLibrary, playerStats[1]._projectilesLibrary);
        ObjectPoolingManager.Instance.PoolAssistInitialize(assistStats[0].assistProjectile, assistStats[1].assistProjectile);
        ObjectPoolingManager.HasPooled = true;
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new PlayerNetwork();
            _players[i].resultAttack = new ResultAttack();
            _players[i].inputBuffer.inputItems = new InputItemNetwork[20];
            _players[i].state = "Idle";
            _players[i].CurrentState = new IdleState();
            _players[i].flip = 1;
            _players[i].position = new DemonicsVector2((DemonicsFloat)GameplayManager.Instance.GetSpawnPositions()[i], DemonicsPhysics.GROUND_POINT);
            _players[i].playerStats = playerStats[i];
            _players[i].health = playerStats[i].maxHealth;
            _players[i].healthRecoverable = playerStats[i].maxHealth;
            _players[i].shadowGauge = 2000;
            _players[i].attackInput = InputEnum.Direction;
            _players[i].animation = "Idle";
            _players[i].sound = "";
            _players[i].soundGroup = "";
            _players[i].soundStop = "";
            _players[i].canJump = true;
            _players[i].canDoubleJump = true;
            _players[i].attackNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "", moveName = "" };
            _players[i].attackHurtNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "", moveName = "" };
            _players[i].effects = new EffectNetwork[playerStats[i]._effectsLibrary._objectPools.Count];
            _players[i].projectiles = new ProjectileNetwork[playerStats[i]._projectilesLibrary._objectPools.Count];
            _players[i].hitbox = new ColliderNetwork() { active = false };
            _players[i].hurtbox = new ColliderNetwork() { active = true };
            _players[i].pushbox = new ColliderNetwork() { active = true, size = new DemonicsVector2(22, 25), offset = new DemonicsVector2((DemonicsFloat)0, (DemonicsFloat)12.5) };
            _players[i].shadow = new ShadowNetwork();
            _players[i].shadow.projectile.name = assistStats[i].assistProjectile.groupName;
            _players[i].shadow.attack = assistStats[i].attackSO;
            _players[i].shadow.projectile.attackNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "", moveName = "" };
            _players[i].shadow.spawnPoint = new DemonicsVector2((DemonicsFloat)assistStats[i].assistPosition.x, (DemonicsFloat)assistStats[i].assistPosition.y);
            _players[i].shadow.spawnRotation = new DemonicsVector2((DemonicsFloat)assistStats[i].assistRotation.x, (DemonicsFloat)assistStats[i].assistRotation.y);
            _players[i].shadow.projectile.animationMaxFrames = ObjectPoolingManager.Instance.GetAssistPoolAnimation(i, _players[i].shadow.projectile.name).GetMaxAnimationFrames();
            _players[i].shadow.projectile.destroyOnHit = true;
            _players[i].shadow.projectile.speed = (DemonicsFloat)assistStats[i].assistSpeed;
            _players[i].shadow.projectile.priority = assistStats[i].assistPriority;
            for (int j = 0; j < _players[i].effects.Length; j++)
            {
                _players[i].effects[j] = new EffectNetwork();
                _players[i].effects[j].name = playerStats[i]._effectsLibrary._objectPools[j].groupName;
                _players[i].effects[j].animationMaxFrames = ObjectPoolingManager.Instance.GetObjectAnimation(i, _players[i].effects[j].name).GetMaxAnimationFrames();
                _players[i].effects[j].effectGroups = new EffectGroupNetwork[playerStats[i]._effectsLibrary._objectPools[j].size];
            }
            for (int j = 0; j < _players[i].projectiles.Length; j++)
            {
                _players[i].projectiles[j] = new ProjectileNetwork();
                _players[i].projectiles[j].attackNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "", moveName = "" };
                _players[i].projectiles[j].name = playerStats[i]._projectilesLibrary._objectPools[j].groupName;
                _players[i].projectiles[j].animationMaxFrames = ObjectPoolingManager.Instance.GetObjectPoolAnimation(i, _players[i].projectiles[j].name).GetMaxAnimationFrames();
            }
        }
        _players[0].spriteOrder = 1;
        _players[1].spriteOrder = 0;
        _players[0].otherPlayer = _players[1];
        _players[1].otherPlayer = _players[0];
        _players[0].CurrentState.CheckTrainingGauges(_players[0]);
        _players[1].CurrentState.CheckTrainingGauges(_players[1]);
    }

    public void PlayerLogic(int index, bool skip, bool up, bool down, bool left, bool right, bool light, bool medium, bool heavy,
    bool arcana, bool grab, bool shadow, bool blueFrenzy, bool redFrenzy, bool dashForward, bool dashBackward)
    {
        if (GameSimulation.Run)
        {
            if (up && !_players[index].upHold)
            {
                _players[index].upHold = true;
                _players[index].inputDirection = InputDirectionEnum.Up;
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Direction, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
                _players[index].direction = new Vector2Int(_players[index].direction.x, 1);
            }
            else if (!up)
            {
                if (_players[index].upHold)
                {
                    _players[index].upHold = false;
                    _players[index].inputDirection = InputDirectionEnum.NoneVertical;
                    _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Direction, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
                    _players[index].direction = new Vector2Int(_players[index].direction.x, 0);
                }
            }
            if (down && !_players[index].downHold)
            {
                _players[index].downHold = true;
                _players[index].inputDirection = InputDirectionEnum.Down;
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Direction, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
                _players[index].direction = new Vector2Int(_players[index].direction.x, -1);
            }
            else if (!down)
            {
                if (_players[index].downHold)
                {
                    _players[index].downHold = false;
                    _players[index].inputDirection = InputDirectionEnum.NoneVertical;
                    _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Direction, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
                    _players[index].direction = new Vector2Int(_players[index].direction.x, 0);
                }
            }

            if (left && !_players[index].leftHold)
            {
                _players[index].leftHold = true;
                _players[index].inputDirection = InputDirectionEnum.Left;
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Direction, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
                _players[index].direction = new Vector2Int(-1, _players[index].direction.y);
            }
            else if (!left)
            {
                if (_players[index].leftHold)
                {
                    _players[index].leftHold = false;
                    _players[index].inputDirection = InputDirectionEnum.NoneHorizontal;
                    _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Direction, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
                    _players[index].direction = new Vector2Int(0, _players[index].direction.y);
                }
            }
            if (right && !_players[index].rightHold)
            {
                _players[index].rightHold = true;
                _players[index].inputDirection = InputDirectionEnum.Right;
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Direction, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
                _players[index].direction = new Vector2Int(1, _players[index].direction.y);
            }
            else if (!right)
            {
                if (_players[index].rightHold)
                {
                    _players[index].rightHold = false;
                    _players[index].inputDirection = InputDirectionEnum.NoneHorizontal;
                    _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Direction, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
                    _players[index].direction = new Vector2Int(0, _players[index].direction.y);
                }
            }


            if (dashForward)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.ForwardDash, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (dashBackward)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.BackDash, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (light)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Light, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (medium)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Medium, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (heavy)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Heavy, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (arcana)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Special, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (blueFrenzy)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Parry, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (redFrenzy)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.RedFrenzy, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (grab)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Throw, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }
            if (shadow)
            {
                _players[index].inputBuffer.AddInputItem(new InputItemNetwork() { inputEnum = InputEnum.Assist, inputDirection = _players[index].inputDirection, frame = Framenumber, pressed = true });
            }

            if (_players[index].arcanaGauge >= _players[index].playerStats.Arcana)
            {
                _players[index].arcanaGauge = _players[index].playerStats.Arcana;
            }
            else
            {
                _players[index].arcanaGauge += _players[index].playerStats.arcanaRecharge;
            }
            if (_players[index].shadowGauge >= 2000)
            {
                _players[index].shadowGauge = 2000;
            }
            else
            {
                _players[index].shadowGauge += 3;
            }
        }
        else
        {
            _players[index].direction = Vector2Int.zero;
        }

        _players[index].CurrentState.UpdateLogic(_players[index]);
        if (!_players[index].hitstop)
        {
            _players[index].player.PlayerAnimator.SpriteNormalEffect();
            if (!DemonicsPhysics.Collision(_players[index], _players[index].otherPlayer))
            {
                _players[index].position = new DemonicsVector2(_players[index].position.x + _players[index].velocity.x, _players[index].position.y + _players[index].velocity.y);
            }
        }
        if (Hitstop <= 0)
        {
            for (int i = 0; i < _players[index].projectiles.Length; i++)
            {
                _players[index].projectiles[i].hitstop = false;
            }
            _players[index].shadow.projectile.hitstop = false;
            _players[index].hitstop = false;
        }
        _players[index].position = DemonicsPhysics.Bounds(_players[index]);
        DemonicsPhysics.CameraHorizontalBounds(_players[0], _players[1]);
        if (_players[index].player.IsAnimationFinished(_players[index].animation, _players[index].animationFrames))
        {
            if (_players[index].player.IsAnimationLoop(_players[index].animation))
            {
                _players[index].animationFrames = 0;
            }
            else
            {
                _players[index].animationFrames = _players[index].animationFrames + 1;
            }
        }
        for (int i = 0; i < _players[index].effects.Length; i++)
        {
            for (int j = 0; j < _players[index].effects[i].effectGroups.Length; j++)
            {
                if (_players[index].effects[i].effectGroups[j].active)
                {
                    _players[index].effects[i].effectGroups[j].animationFrames++;
                    if (_players[index].effects[i].effectGroups[j].animationFrames >= _players[index].effects[i].animationMaxFrames)
                    {
                        _players[index].effects[i].effectGroups[j].animationFrames = 0;
                        _players[index].effects[i].effectGroups[j].active = false;
                    }
                }
            }
        }
        if (_players[index].shadow.isOnScreen)
        {
            if (_players[index].shadow.animationFrames >= 55)
            {
                _players[index].CurrentState.CheckTrainingGauges(_players[index]);
                _players[index].shadow.isOnScreen = false;
            }
            else
            {
                bool isProjectile = _players[index].player.Assist.GetProjectile("Attack", _players[index].shadow.animationFrames);
                if (isProjectile && _players[index].shadow.projectile.fire)
                {
                    _players[index].shadow.projectile.fire = false;
                    _players[index].SetAssist(_players[index].shadow.projectile.name, _players[index].shadow.position, _players[index].shadow.projectile.speed, false);
                }
            }
        }
        ProjectilesSimulation.HandleProjectileCollision(_players[index], index);
        AnimationBox[] animationHitboxes = _players[index].player.PlayerAnimator.GetHitboxes(_players[index].animation, _players[index].animationFrames);
        if (animationHitboxes.Length == 0)
        {
            _players[index].hitbox.enter = false;
            _players[index].hitbox.active = false;
        }
        else
        {
            _players[index].hitbox.size = new DemonicsVector2((DemonicsFloat)animationHitboxes[0].size.x, (DemonicsFloat)animationHitboxes[0].size.y);
            _players[index].hitbox.offset = new DemonicsVector2((DemonicsFloat)animationHitboxes[0].offset.x, (DemonicsFloat)animationHitboxes[0].offset.y);
            _players[index].hitbox.position = new DemonicsVector2(_players[index].position.x + (_players[index].hitbox.offset.x * _players[index].flip), _players[index].position.y + _players[index].hitbox.offset.y);
            _players[index].hitbox.active = true;
        }
        AnimationBox[] animationHurtboxes = _players[index].player.PlayerAnimator.GetHurtboxes(_players[index].animation, _players[index].animationFrames);
        if (animationHurtboxes.Length > 0)
        {
            _players[index].hurtbox.active = true;
            _players[index].hurtbox.size = new DemonicsVector2((DemonicsFloat)animationHurtboxes[0].size.x, (DemonicsFloat)animationHurtboxes[0].size.y);
            _players[index].hurtbox.offset = new DemonicsVector2((DemonicsFloat)animationHurtboxes[0].offset.x, (DemonicsFloat)animationHurtboxes[0].offset.y);
        }
        _players[index].hurtbox.position = _players[index].position + _players[index].hurtbox.offset;
        _players[index].pushbox.position = _players[index].position + _players[index].pushbox.offset;
        for (int i = 0; i < _players[index].inputBuffer.inputItems.Length; i++)
        {
            if (_players[index].inputBuffer.inputItems[i].frame < Framenumber)
            {
                _players[index].inputBuffer.inputItems[i].pressed = false;
            }
            if (_players[index].inputBuffer.inputItems[i].frame + 20 < Framenumber)
            {
                _players[index].inputBuffer.inputItems[i].frame = 0;
            }
        }


        if (_players[index].shadow.isOnScreen)
        {
            _players[index].shadow.animationFrames++;
        }
    }
    public void Update(long[] inputs, int disconnect_flags)
    {
        if (Time.timeScale > 0 && GameplayManager.Instance)
        {
            if (Framenumber == 0)
            {
                GameSimulation.Start = true;
            }
            Framenumber++;
            DemonicsWorld.Frame = Framenumber;
            if (Framenumber % GlobalHitstop == 0)
            {
                if (IntroFrame == 0 && !_introPlayed)
                {
                    _introPlayed = true;
                    _players[0].CurrentState.EnterState(_players[0], "Taunt");
                    _players[1].CurrentState.EnterState(_players[1], "Taunt");
                }
                if (!GameSimulation.Run)
                {
                    if ((inputs[0] & NetworkInput.SKIP_BYTE) != 0 || (inputs[1] & NetworkInput.SKIP_BYTE) != 0)
                    {
                        if ((!SceneSettings.ReplayMode || Skip) && Framenumber > 200 && !_introPlayed)
                        {
                            _introPlayed = true;
                            _players[0].CurrentState.EnterState(_players[0], "Taunt");
                            _players[1].CurrentState.EnterState(_players[1], "Taunt");
                            GameplayManager.Instance.SkipIntro();
                        }
                    }
                    IntroFrame--;
                }
                if (_players[0].player != null)
                {
                    for (int i = 0; i < _players.Length; i++)
                    {
                        bool skip = false;
                        bool up = false;
                        bool down = false;
                        bool left = false;
                        bool right = false;
                        bool light = false;
                        bool medium = false;
                        bool heavy = false;
                        bool arcana = false;
                        bool grab = false;
                        bool shadow = false;
                        bool blueFrenzy = false;
                        bool redFrenzy = false;
                        bool dashForward = false;
                        bool dashBackward = false;
                        if ((disconnect_flags & (1 << i)) != 0)
                        {
                            //Handle AI
                        }
                        else
                        {
                            InputSimulation.ParseInputs(inputs[i], i, out skip, out up, out down, out left, out right, out light, out medium, out heavy, out arcana,
                             out grab, out shadow, out blueFrenzy, out redFrenzy, out dashForward, out dashBackward);
                        }
                        PlayerLogic(i, skip, up, down, left, right, light, medium, heavy, arcana, grab, shadow, blueFrenzy, redFrenzy, dashForward, dashBackward);
                    }
                    if (GameSimulation.Run)
                    {
                        if (Framenumber % 60 == 0)
                        {
                            if (Timer > 0 && !SceneSettings.IsTrainingMode)
                            {
                                Timer--;
                                if (Timer == 0)
                                {
                                    _players[0].CurrentState.EnterState(_players[0], "GiveUp");
                                    _players[1].CurrentState.EnterState(_players[1], "GiveUp");
                                }
                            }
                        }
                    }
                    Hitstop--;
                }
            }

        }
    }

    public long ReadInputs(int id)
    {
        return InputSimulation.GetInput(id);
    }

    public void FreeBytes(NativeArray<byte> data)
    {
        if (data.IsCreated)
        {
            data.Dispose();
        }
    }

    public override int GetHashCode()
    {
        int hashCode = -1214587014;
        hashCode = hashCode * -1521134295 + Framenumber.GetHashCode();
        foreach (var player in _players)
        {
            hashCode = hashCode * -1521134295 + player.GetHashCode();
        }
        return hashCode;
    }

    public void LogInfo(string filename)
    {
        Debug.Log(filename);
    }
}
