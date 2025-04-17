using UnityEngine;

public class Shotgun : Weapon
{
    private void Start()
    {
        base.Start();

        SetSkill(205);
        SetSkill(206);
    }

    public override void UseQSkill()
    {
         Vector3 offset = _player.transform.forward * 1.2f + _player.transform.up * 1.0f;
        PlayEffectAttachedToPlayer(_qSkillPrefab, _player.transform.position + offset);
    }

    public override void UseESkill()
    {
        Vector3 offset = _player.transform.forward * 1.2f + _player.transform.up * 1.0f;
        PlayEffectAttachedToPlayer(_qSkillPrefab, _player.transform.position + offset);
    }
}
