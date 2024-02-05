using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct GameSimulation : IGame
{
    public int Frames { get; private set; }
    public static int FramesStatic { get; private set; }
    public static int Timer { get; set; }
    public static int GlobalFreezeFrames { get; set; }
    public static int GlobalHitstop { get; set; }
    public static int Hitstop { get; set; }
    public static int IntroFrame { get; set; }
    public static bool Skip { get; set; }
    public static bool Start { get; set; }
    public static bool Run { get; set; }
    public int Checksum => GetHashCode();
    public static int _timerMax = 99;
    public static bool _introPlayed;
    public static int maxShadowGauge = 2400;
    public static PlayerNetwork[] _players;


    public void Serialize(BinaryWriter bw)
    {
        bw.Write(Frames);
        bw.Write(FramesStatic);
        bw.Write(GlobalFreezeFrames);
        bw.Write(GlobalHitstop);
        bw.Write(Timer);
        bw.Write(Hitstop);
        bw.Write(IntroFrame);
        bw.Write(Start);
        bw.Write(Run);
        bw.Write(_players.Length);
        for (int i = 0; i < _players.Length; ++i)
            _players[i].Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        Frames = br.ReadInt32();
        FramesStatic = br.ReadInt32();
        GlobalFreezeFrames = br.ReadInt32();
        GlobalHitstop = br.ReadInt32();
        Timer = br.ReadInt32();
        Hitstop = br.ReadInt32();
        IntroFrame = br.ReadInt32();
        Start = br.ReadBoolean();
        Run = br.ReadBoolean();
        int length = br.ReadInt32();
        if (length != _players.Length)
            _players = new PlayerNetwork[length];
        for (int i = 0; i < _players.Length; ++i)
            _players[i].Deserialize(br);
    }

    public NativeArray<byte> ToBytes()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(memoryStream))
                Serialize(writer);
            return new NativeArray<byte>(memoryStream.ToArray(), Allocator.Persistent);
        }
    }

    public void FromBytes(NativeArray<byte> bytes)
    {
        using (var memoryStream = new MemoryStream(bytes.ToArray()))
        {
            using (var reader = new BinaryReader(memoryStream))
                Deserialize(reader);
        }
    }

    public GameSimulation(PlayerStatsSO[] playerStats, AssistStatsSO[] assistStats)
    {
        Printer.Log($"Connection initialized");
        GlobalHitstop = 1;
        Frames = 0;
        Timer = _timerMax;
        Hitstop = 0;
        IntroFrame = 880;
        _introPlayed = false;
        _players = new PlayerNetwork[playerStats.Length];
        ObjectPoolingManager.Instance.PoolInitialize(playerStats[0]._effectsLibrary, playerStats[1]._effectsLibrary);
        ObjectPoolingManager.Instance.PoolParticlesInitialize(playerStats[0]._particlesLibrary, playerStats[1]._particlesLibrary);
        ObjectPoolingManager.Instance.PoolProjectileInitialize(playerStats[0]._projectilesLibrary, playerStats[1]._projectilesLibrary);
        ObjectPoolingManager.Instance.PoolAssistInitialize(assistStats[0].assistProjectile, assistStats[1].assistProjectile);
        ObjectPoolingManager.HasPooled = true;
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new PlayerNetwork();
            _players[i].resultAttack = new ResultAttack();
            _players[i].inputBuffer.triggers = new InputItemNetwork[2000];
            _players[i].inputBuffer.sequences = new InputItemNetwork[2000];
            _players[i].state = "Idle";
            _players[i].CurrentState = new IdleState();
            _players[i].flip = 1;
            _players[i].position = new DemonVector2((DemonFloat)GameplayManager.Instance.GetSpawnPositions()[i], DemonicsPhysics.GROUND_POINT);
            _players[i].playerStats = playerStats[i];
            _players[i].health = playerStats[i].maxHealth;
            _players[i].healthRecoverable = playerStats[i].maxHealth;
            _players[i].shadowGauge = maxShadowGauge;
            _players[i].attackInput = InputEnum.Neutral;
            _players[i].animation = "Idle";
            _players[i].sound = "";
            _players[i].soundGroup = "";
            _players[i].soundStop = "";
            _players[i].canJump = true;
            _players[i].canDoubleJump = true;
            _players[i].upHold = false;
            _players[i].downHold = false;
            _players[i].leftHold = false;
            _players[i].rightHold = false;
            _players[i].inputList = new InputList()
            {
                inputTriggers = new InputTrigger[]{
                    new() { inputEnum = InputEnum.Light },
                    new() { inputEnum = InputEnum.Medium },
                    new() { inputEnum = InputEnum.Assist },
                    new() { inputEnum = InputEnum.Heavy },
                    new() { inputEnum = InputEnum.Special },
                    new() { inputEnum = InputEnum.Throw },
                    new() { inputEnum = InputEnum.ForwardDash },
                    new() { inputEnum = InputEnum.BackDash },
                    new() { inputEnum = InputEnum.Up , sequence = true },
                    new() { inputEnum = InputEnum.Down, sequence = true  },
                    new() { inputEnum = InputEnum.Left, sequence = true  },
                    new() { inputEnum = InputEnum.Right, sequence = true },
                    new() { inputEnum = InputEnum.UpRight, sequence = true },
                    new() { inputEnum = InputEnum.UpLeft, sequence = true },
                    new() { inputEnum = InputEnum.DownRight, sequence = true },
                    new() { inputEnum = InputEnum.DownLeft, sequence = true },
                    new() { inputEnum = InputEnum.Neutral, sequence = true },
                },
                inputSequence = new InputSequence() { inputEnum = InputEnum.Neutral, inputDirectionEnum = InputDirectionEnum.Neutral },
            };
            _players[i].inputHistory.InputItems = new InputItemNetwork[1000];
            _players[i].attackNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "", moveName = "" };
            _players[i].attackHurtNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "", moveName = "" };
            _players[i].effects = new EffectNetwork[playerStats[i]._effectsLibrary._objectPools.Count];
            _players[i].particles = new EffectNetwork[playerStats[i]._particlesLibrary._objectPools.Count];
            _players[i].projectiles = new ProjectileNetwork[playerStats[i]._projectilesLibrary._objectPools.Count];
            _players[i].hitbox = new ColliderNetwork() { active = false };
            _players[i].hurtbox = new ColliderNetwork() { active = true };
            _players[i].pushbox = new ColliderNetwork() { active = true, size = new DemonVector2(22, 25), offset = new DemonVector2((DemonFloat)0, (DemonFloat)12.5) };
            _players[i].shadow = new ShadowNetwork();
            _players[i].shadow.projectile.name = assistStats[i].assistProjectile.groupName;
            _players[i].shadow.attack = assistStats[i].attackSO;
            _players[i].shadow.projectile.attackNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "", moveName = "" };
            _players[i].shadow.spawnPoint = new DemonVector2((DemonFloat)assistStats[i].assistPosition.x, (DemonFloat)assistStats[i].assistPosition.y);
            _players[i].shadow.spawnRotation = new DemonVector2((DemonFloat)assistStats[i].assistRotation.x, (DemonFloat)assistStats[i].assistRotation.y);
            _players[i].shadow.projectile.animationMaxFrames = ObjectPoolingManager.Instance.GetAssistPoolAnimation(i, _players[i].shadow.projectile.name).GetMaxAnimationFrames();
            _players[i].shadow.projectile.destroyOnHit = true;
            _players[i].shadow.projectile.speed = (DemonFloat)assistStats[i].assistSpeed;
            _players[i].shadow.projectile.priority = assistStats[i].assistPriority;
            for (int j = 0; j < _players[i].effects.Length; j++)
            {
                _players[i].effects[j] = new EffectNetwork();
                _players[i].effects[j].name = playerStats[i]._effectsLibrary._objectPools[j].groupName;
                _players[i].effects[j].animationMaxFrames = ObjectPoolingManager.Instance.GetObjectAnimation(i, _players[i].effects[j].name).GetMaxAnimationFrames();
                _players[i].effects[j].effectGroups = new EffectGroupNetwork[playerStats[i]._effectsLibrary._objectPools[j].size];
            }
            for (int j = 0; j < _players[i].particles.Length; j++)
            {
                _players[i].particles[j] = new EffectNetwork();
                _players[i].particles[j].name = playerStats[i]._particlesLibrary._objectPools[j].groupName;
                _players[i].particles[j].effectGroups = new EffectGroupNetwork[playerStats[i]._particlesLibrary._objectPools[j].size];
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

    public void PlayerLogic(int index, bool skip)
    {
        if (GameSimulation.Run)
        {
            _players[index].direction = InputDirectionTypes.GetDirectionValue(_players[index].inputBuffer.CurrentSequence().inputEnum);
            for (int i = 0; i < _players[index].inputList.inputTriggers.Length; i++)
                if (_players[index].inputList.inputTriggers[i].pressed)
                {
                    _players[index].inputBuffer.AddTrigger(new InputItemNetwork()
                    {
                        inputEnum = _players[index].inputList.inputTriggers[i].inputEnum,
                        frame = Frames,
                        time = Frames,
                        sequence = _players[index].inputList.inputTriggers[i].sequence,
                        pressed = true,
                        crouch = _players[index].direction.y == -1
                    });
                }


            if (_players[index].shadowGauge >= maxShadowGauge)
                _players[index].shadowGauge = maxShadowGauge;
            else if (_players[index].otherPlayer.combo == 0 || !_players[index].shadow.usedInCombo)
                _players[index].shadowGauge += 2;
        }
        else
            _players[index].direction = Vector2Int.zero;

        StateSimulation.SetState(_players[index]);
        _players[index].CurrentState.UpdateLogic(_players[index]);
        if (!_players[index].hitstop)
        {
            _players[index].player.PlayerAnimator.SpriteNormalEffect();
            if (!DemonicsPhysics.Collision(_players[index], _players[index].otherPlayer))
                _players[index].position = new DemonVector2(_players[index].position.x + _players[index].velocity.x, _players[index].position.y + _players[index].velocity.y);
        }
        if (Hitstop <= 0)
        {
            for (int i = 0; i < _players[index].projectiles.Length; i++)
                _players[index].projectiles[i].hitstop = false;
            _players[index].shadow.projectile.hitstop = false;
            _players[index].hitstop = false;
        }
        _players[index].position = DemonicsPhysics.Bounds(_players[index]);
        DemonicsPhysics.CameraHorizontalBounds(_players[0], _players[1]);
        if (_players[index].player.IsAnimationFinished(_players[index].animation, _players[index].animationFrames))
            if (_players[index].player.IsAnimationLoop(_players[index].animation))
                _players[index].animationFrames = 0;

        for (int i = 0; i < _players[index].effects.Length; i++)
            for (int j = 0; j < _players[index].effects[i].effectGroups.Length; j++)
                if (_players[index].effects[i].effectGroups[j].active)
                {
                    _players[index].effects[i].effectGroups[j].animationFrames++;
                    if (_players[index].effects[i].effectGroups[j].animationFrames >= _players[index].effects[i].animationMaxFrames)
                    {
                        _players[index].effects[i].effectGroups[j].animationFrames = 0;
                        _players[index].effects[i].effectGroups[j].active = false;
                    }
                }
        if (_players[index].shadow.isOnScreen)
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

        ProjectilesSimulation.HandleProjectileCollision(_players[index], index);
        AnimationBox[] animationHitboxes = _players[index].player.PlayerAnimator.GetHitboxes(_players[index].animation, _players[index].animationFrames);
        if (animationHitboxes.Length == 0)
        {
            _players[index].hitbox.enter = false;
            _players[index].hitbox.active = false;
        }
        else
        {
            _players[index].hitbox.size = new DemonVector2((DemonFloat)animationHitboxes[0].size.x, (DemonFloat)animationHitboxes[0].size.y);
            _players[index].hitbox.offset = new DemonVector2((DemonFloat)animationHitboxes[0].offset.x, (DemonFloat)animationHitboxes[0].offset.y);
            _players[index].hitbox.position = new DemonVector2(_players[index].position.x + (_players[index].hitbox.offset.x * _players[index].flip), _players[index].position.y + _players[index].hitbox.offset.y);
            _players[index].hitbox.active = true;
        }
        AnimationBox[] animationHurtboxes = _players[index].player.PlayerAnimator.GetHurtboxes(_players[index].animation, _players[index].animationFrames);
        if (animationHurtboxes.Length > 0)
        {
            _players[index].hurtbox.active = true;
            _players[index].hurtbox.size = new DemonVector2((DemonFloat)animationHurtboxes[0].size.x, (DemonFloat)animationHurtboxes[0].size.y);
            _players[index].hurtbox.offset = new DemonVector2((DemonFloat)animationHurtboxes[0].offset.x, (DemonFloat)animationHurtboxes[0].offset.y);
        }
        _players[index].hurtbox.position = _players[index].position + _players[index].hurtbox.offset;
        _players[index].pushbox.position = _players[index].position + _players[index].pushbox.offset;

        for (int i = 0; i < _players[index].inputBuffer.triggers.Length; i++)
        {
            if (_players[index].inputBuffer.triggers[i].frame < Frames)
                _players[index].inputBuffer.triggers[i].pressed = false;
            if (_players[index].inputBuffer.triggers[i].frame + 25 < Frames)
                _players[index].inputBuffer.triggers[i].frame = 0;
        }
        _players[index].gotHit = false;
        for (int i = 0; i < _players[index].inputList.inputTriggers.Length; i++)
            _players[index].inputList.inputTriggers[i].pressed = false;
        if (_players[index].shadow.isOnScreen)
            _players[index].shadow.animationFrames++;
    }

    public void Update(long[] inputs, int disconnect_flags)
    {
        if (Time.timeScale > 0 && GameplayManager.Instance)
        {
            if (Frames == 0)
                GameSimulation.Start = true;
            Frames++;
            FramesStatic = Frames;
            DemonicsWorld.Frame = Frames;
            if (GlobalFreezeFrames > 0)
            {
                GlobalFreezeFrames--;
                return;
            }
            if (Frames % GlobalHitstop == 0)
            {
                if (IntroFrame == 0 && !_introPlayed)
                {
                    _introPlayed = true;
                    _players[0].CurrentState.EnterState(_players[0], "Taunt");
                    _players[1].CurrentState.EnterState(_players[1], "Taunt");
                }
                if (!GameSimulation.Run)
                {
                    if (!SceneSettings.ReplayMode)
                    {
                        if ((inputs[0] & NetworkInput.SKIP_BYTE) != 0 || (inputs[1] & NetworkInput.SKIP_BYTE) != 0)
                            if (Frames > 200 && !_introPlayed)
                            {
                                _introPlayed = true;
                                _players[0].CurrentState.EnterState(_players[0], "Taunt");
                                _players[1].CurrentState.EnterState(_players[1], "Taunt");
                                GameplayManager.Instance.SkipIntro();
                            }
                    }
                    else
                    {
                        if (Skip && !_introPlayed)
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
                        if ((disconnect_flags & (1 << i)) == 0)
                            InputParser.ParseInput(inputs[i], out skip, ref _players[i].inputList);
                        PlayerLogic(i, skip);
                    }
                    if (GameSimulation.Run)
                        if (Frames % 60 == 0)
                            if (Timer > 0 && !SceneSettings.IsTrainingMode)
                            {
                                Timer--;
                                if (Timer == 0)
                                    if (_players[0].health == _players[1].health)
                                    {
                                        if (_players[0].player.Lives > 1 && _players[1].player.Lives == 1)
                                            _players[0].CurrentState.EnterState(_players[0], "Taunt");
                                        else
                                            _players[0].CurrentState.EnterState(_players[0], "GiveUp");

                                        if (_players[1].player.Lives > 1 && _players[0].player.Lives == 1)
                                            _players[0].CurrentState.EnterState(_players[1], "Taunt");
                                        else
                                            _players[1].CurrentState.EnterState(_players[1], "GiveUp");
                                    }
                                    else
                                    {
                                        if (_players[0].health > _players[1].health)
                                        {
                                            _players[0].CurrentState.EnterState(_players[0], "Taunt");
                                            _players[1].CurrentState.EnterState(_players[1], "GiveUp");
                                        }
                                        else
                                        {
                                            _players[0].CurrentState.EnterState(_players[0], "GiveUp");
                                            _players[1].CurrentState.EnterState(_players[1], "Taunt");
                                        }
                                    }
                            }
                    Hitstop--;
                }
            }
        }
    }

    public long ReadInputs(int id) => InputParser.GetInput(id);

    public void FreeBytes(NativeArray<byte> data)
    {
        if (data.IsCreated)
            data.Dispose();
    }

    public override int GetHashCode()
    {
        int hashCode = -1214587014;
        hashCode = hashCode * -1521134295 + Frames.GetHashCode();
        foreach (var player in _players)
            hashCode = hashCode * -1521134295 + player.GetHashCode();
        return hashCode;
    }

    public void LogInfo(string filename) => Debug.Log(filename);
}
