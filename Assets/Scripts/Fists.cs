using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : MeleeWeapon
{
    public float punchStrength = 5f;
    public float hitStrength = 5f;
    public GameObject box;
    public float attackRange = 3f;
    public ToolUser toolUser;
    public override void Use()
    {
        Vector3 userForward = toolUser.GetForward();
        Vector3 pos = toolUser.getCameraCenterPoint();
        //GameObject boxInstance = Instantiate(box, pos + userForward * attackRange / 2, Quaternion.LookRotation(userForward, Vector3.up));
        //Transform boxTransform = boxInstance.transform;
        //boxTransform.localScale += new Vector3(0f, 0f, attackRange);
        //Debug.DrawLine(pos, pos + userForward * attackRange, Color.red, 30f);
        Collider[] colliders = Physics.OverlapBox(pos + userForward * attackRange / 2, new Vector3(0f, 0f, attackRange) / 2, Quaternion.LookRotation(userForward, Vector3.up));
        IDamageable damageable;
        foreach (Collider collider in colliders)
        {
            damageable = collider.GetComponent<IDamageable>();
            if (damageable != null && collider.transform.root != transform.root)
            {
                damageable.TakeDamage(hitStrength, punchStrength, pos);
            }
        }
        //Debug.DrawRay(pos, userForward * 20f, Color.red, 30f);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
