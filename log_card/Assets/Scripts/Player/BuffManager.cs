using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuffStat
{
    damage = 0,
    rate,
    def,
    speed,
    absolDef,
    stop
};
public enum BuffType
{
    none = 0,
    charging,
    reloading,
    nextAtk,
    stopping,
    potionAtk,
};
public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    private int c = 0;
    public static float[] buffValue = new float[11];
    private List<Buff> buffs = new List<Buff>();
    private List<Buff> durationBuffs = new List<Buff>();
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < buffValue.Length; i++)
        {
            buffValue[i] = 1;
        }
        buffValue[(int)BuffStat.absolDef] = 0;
        //addBuff(BuffStat.damage, 0.1f);//공격력 0.1 상승
        //addBuff(BuffStat.def, 0.1f, 1f, BuffType.charging);//1초간 방어력 0.1 증가, 차징 끝나면 풀림
        //string str = addBuff(BuffStat.damage, 0.1f, 0f, BuffType.nextAtk);//다음 공격 0.1 증가, 지속시간은 없음
        //deleteBuff(str);
        //Debug.Log(BuffManager.buffValue[0]);
        //Debug.Log(BuffManager.buffValue[2]);
        //stopType(BuffType.charging);
        //Debug.Log(BuffManager.buffValue[2]);
    }


    void Update()
    {
        for (int i = 0; i < durationBuffs.Count; i++)
        {
            durationBuffs[i].duration -= Time.deltaTime;
        }
        for (int i = 0; i < durationBuffs.Count; i++)
        {
            if (durationBuffs[i].duration <= 0f) { durationBuffs[i].OnDelete(); durationBuffs.RemoveAt(i);}
        }
    }

    public string AddBuff(BuffStat buffStat, float value, float duration = 0f, BuffType buffType = BuffType.none, string name = "")
    {//올리는 스탯 종류, 수치, 지속시간(0이면 무제한), 차징같은 버프 종류, 이름
        if (name == "") name = "buffNum" + c++;
        if (duration == 0f)
        {
            buffs.Add(new Buff(buffStat, value, duration, buffType, name));
        }
        else
        {
            durationBuffs.Add(new Buff(buffStat, value, duration, buffType, name));
        }
        return name;
    }
    public void DeleteBuff(string name)
    {
        for (int i = 0; i < buffs.Count; i++) if (buffs[i].name == name) { buffs[i].OnDelete(); buffs.RemoveAt(i); return; }
        for (int i = 0; i < durationBuffs.Count; i++) if (durationBuffs[i].name == name) { durationBuffs[i].OnDelete(); durationBuffs.RemoveAt(i); return; }
    }
    public bool FindBuff(string name)
    {
        for (int i = 0; i < buffs.Count; i++) if (buffs[i].name == name) return true;
        for (int i = 0; i < durationBuffs.Count; i++) if (durationBuffs[i].name == name) return true;
        return false;
    }
    public void StopType(BuffType buffType)
    {
        for (int i = 0; i < buffs.Count; i++) if (buffs[i].buffType == buffType) { buffs[i].OnDelete(); buffs.RemoveAt(i--); }
        for (int i = 0; i < durationBuffs.Count; i++) if (durationBuffs[i].buffType == buffType) { durationBuffs[i].OnDelete(); durationBuffs.RemoveAt(i--); }
    }
    public float GetValue(BuffStat buffStat)
    {
        return buffValue[(int)buffStat];
    }
}
class Buff
{
    public BuffStat buffStat;
    public BuffType buffType;
    public float duration, value;
    public string name;
    public Buff(BuffStat buffStat, float value, float duration = 0, BuffType buffType = BuffType.none, string name = "")
    {
        this.buffStat = buffStat;
        this.buffType = buffType;
        this.duration = duration;
        this.value = value;
        this.name = name;
        if (value > 0) BuffManager.buffValue[(int)buffStat] += value;
        else BuffManager.buffValue[(int)buffStat] *= 1 + value;
    }
    public void OnDelete()
    {
        if (value > 0) BuffManager.buffValue[(int)buffStat] -= value;
        else BuffManager.buffValue[(int)buffStat] /= 1 + value;
    }
}
