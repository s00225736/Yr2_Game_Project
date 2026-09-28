using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    //bow variables
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] SpriteRenderer ArrowGFX;
    [SerializeField] Transform Bow;
    [SerializeField] Transform Hand;
    [SerializeField] Slider BowPowerSlider;

    [Range(0, 10)]

    public float BowPower;

    [Range(0, 3)]

    public float MaxBowCharge;

    float BowCharge;
    bool CanFire = true;

    //follow mouse
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    private void Start()
    {
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = MaxBowCharge;
    }

    Vector3 mousePos;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1) && CanFire)
        {
            ChargeBow();
        } 
        //else
        //{
        //    if (BowCharge > 0f)
        //    {
        //        BowCharge -= 1f * Time.deltaTime;
        //    }
        //    else
        //    {
        //        BowCharge = 0f;
        //        CanFire = true;
        //    }

        //    BowPowerSlider.value = BowCharge;
        //}
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            FireBow();
            BowCharge = 0f;
            CanFire = true;
            BowPowerSlider.value = BowCharge;
        }

    }

    private void ChargeBow()
    {
        ArrowGFX.enabled = true;
        BowCharge += Time.deltaTime;

        BowPowerSlider.value = BowCharge;

        if (BowCharge > MaxBowCharge)
        {
            BowPowerSlider.value = MaxBowCharge;
        }
    }

    void FireBow()
    {
        if (BowCharge > MaxBowCharge) BowCharge = MaxBowCharge;

        float ArrowSpeed = BowCharge * BowPower;

        float angle = Utility.AngleTowardsMouse(Bow.position);

        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        Arrow arrow = Instantiate(ArrowPrefab, Bow.position, Quaternion.identity).GetComponent<Arrow>();

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        arrow.transform.up = mousePos - Bow.position;
        arrow.ArrowVelocity = ArrowSpeed;

        CanFire = false;
        ArrowGFX.enabled = false;
    }
}
