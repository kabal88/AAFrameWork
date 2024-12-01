using System;
using System.Collections.Generic;
using System.Linq;
using AAFramework.Unity.Identifiers;
using UnityEngine;

namespace Services
{
    public class CooldownTimerService : IRepository<ICoolDownable>, IUpdatable
    {
        private readonly List<ICoolDownable> cooldowns = new();

        private Queue<ICoolDownable> addCooldowns = new();
        private Queue<ICoolDownable> removeCooldowns = new();

        public bool IsAlive { get; } = true;


        public void RegisterObject(ICoolDownable obj)
        {
            addCooldowns.Enqueue(obj);
        }

        public void UnRegisterObject(ICoolDownable obj)
        {
            removeCooldowns.Enqueue(obj);
        }

        public IEnumerable<ICoolDownable> GetObjectsByPredicate(Func<ICoolDownable, bool> predicate)
        {
            return cooldowns.Where(predicate);
        }

        public void UpdateLocal()
        {
            for (int i = 0; i < addCooldowns.Count; i++)
            {
                cooldowns.Add(addCooldowns.Dequeue());
            }

            for (int i = 0; i < removeCooldowns.Count; i++)
            {
                var remove = removeCooldowns.Dequeue();
                remove.Dispose();
                cooldowns.Remove(remove);
            }

            for (var i = 0; i < cooldowns.Count; i++)
            {
                var cooldown = cooldowns[i];

                if (cooldown.IsPaused)
                    continue;

                if (cooldown.CurrentTime > 0)
                {
                    cooldown.ChangeCooldown(-Time.deltaTime);
                }
                else
                {
                    cooldown.EndCooldown();
                    cooldown.Dispose();
                    UnRegisterObject(cooldown);
                }
            }
        }
    }
}

public interface ICoolDownable : IDisposable
{
    bool IsPaused { get; }
    float CurrentTime { get; }
    void ChangeCooldown(float value);
    void SetCooldown(float value);
    void EndCooldown();
}

public interface IAlive
{
    bool IsAlive { get; }
}

public class CoolDownTimer : ICoolDownable, IAlive
{
    private float cooldown;
    private Action onEnd;

    public bool IsPaused { get; private set; }
    public float CurrentTime => cooldown;
    public bool IsAlive { get; private set; }


    public CoolDownTimer(float cooldown, Action onEnd, bool isPaused = false)
    {
        this.cooldown = cooldown;
        this.onEnd = onEnd;
        IsPaused = isPaused;
        IsAlive = true;
    }

    public void ChangeCooldown(float value)
    {
        cooldown += value;
    }

    public void SetCooldown(float value)
    {
        cooldown = value;
    }

    public void EndCooldown()
    {
        onEnd?.Invoke();
    }

    public void SetPause(bool value)
    {
        IsPaused = value;
    }

    public void Dispose()
    {
        IsAlive = false;
        onEnd = null;
    }
}