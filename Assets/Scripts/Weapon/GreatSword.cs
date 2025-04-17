using UnityEngine;

public class GreatSword : Weapon
{
    private void Start()
    {
        base.Start();

        SetSkill(203);
        SetSkill(204);
    }

    public override void UseQSkill()
    {
        Vector3 offset = new Vector3(0, 1.0f, 0);
        PlayEffectAttachedToPlayer(_qSkillPrefab, _player.transform.position + offset);
    }

    public override void UseESkill()
    {
        Vector3 offset = _player.transform.forward * 1.1f + Vector3.up * 1.0f;
        PlayEffectAttachedToPlayer(_eSkillPrefab, _player.transform.position + offset);
    }
}
