using UnityEngine;

public class Grimoire : Weapon
{
    private void Start()
    {
        base.Start();

        SetSkill(201);
        SetSkill(202);
    }

    public override void UseQSkill()
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        Vector3 direction = mouseWorldPos - _player.transform.position;

        if (direction.magnitude > 3.0f)
            direction = direction.normalized * 3.0f;

        Vector3 offset = direction + Vector3.up * 0.5f;
        PlayEffectAtPosition(_qSkillPrefab, _player.transform.position + offset);
    }

    public override void UseESkill()
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        Vector3 direction = mouseWorldPos - _player.transform.position;

        if (direction.magnitude > 4.0f)
            direction = direction.normalized * 4.0f;

        Vector3 offset = direction + Vector3.up * 0.5f;
        PlayEffectAtPosition(_eSkillPrefab, _player.transform.position + offset);
    }
}
