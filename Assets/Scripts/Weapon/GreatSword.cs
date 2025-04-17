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
        PlayEffect(_qSkillPrefab, _player.transform.position + offset, _player.transform.forward);
    }

    public override void UseESkill()
    {
        Vector3 offset = new Vector3(0, 1.0f, 0);
        PlayEffect(_eSkillPrefab, _player.transform.position + offset, _player.transform.forward);
    }
}
