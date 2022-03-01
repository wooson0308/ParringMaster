using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Creature playableCreature;

    public Joystick moveJoystick;

    public Joystick attackJoystick;
    public Joystick skillJoysrick;
    public RectTransform skillStick;
    public Joystick parringJoystick;

    public Button skillButton;
    public Button cancelButton;
    public Button lootButton;

    private Weapon currentLootWepaon;

    public Image[] healthPointImgs;
    public Image[] parringPointImgs;

    public void SetHealthUI(float value)
    {
        for (int index = 0; index < healthPointImgs.Length; index++)
        {
            healthPointImgs[index].fillAmount = 0;
        }

        for (float fIndex = 0; fIndex < value; fIndex += 0.5f)
        {
            if (fIndex % 0.5f != 0)
            {
                fIndex -= 0.5f;
            }

            healthPointImgs[(int)fIndex].fillAmount += 0.5f;
        }
    }

    public void SetPPointUI(int value)
    {
        for (int index = 0; index < parringPointImgs.Length; index++)
        {
            parringPointImgs[index].fillAmount = 0;
        }

        for (int index = 0; index < value; index++)
        {
            parringPointImgs[index].fillAmount = 1;
        }
    }


    private void Awake()
    {
        playableCreature = GetComponent<Creature>();
        cancelButton.onClick.AddListener(() => playableCreature.attackController.Cancel());

        lootButton.onClick.AddListener(() => Pickup());
        lootButton.gameObject.SetActive(false);
    }

    public void Pickup()
    {
        playableCreature.PickupAndDrop(currentLootWepaon);
        //playableCreature.AddWeapon(currentLootWepaon);

        currentLootWepaon = null;
    }

    // TODO : 코드 정리
    private void FixedUpdate()
    {
        //cancelButton.gameObject.SetActive(playableCreature.RunningWeaponsMotion());
        lootButton.gameObject.SetActive(currentLootWepaon != null && !playableCreature.RunningWeaponsMotion());

        if(playableCreature.attackController.ParringPoint <= 0)
        {
            skillJoysrick.ResetJoystick();
            skillJoysrick.gameObject.SetActive(false);
        } 
        else
        {
            skillJoysrick.gameObject.SetActive(true);
        }

        Vector2 moveVec = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVec += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVec += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVec += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVec += Vector2.right;
        }

        bool canRotate = !playableCreature.RunningWeaponsMotion();

        if (moveVec.Equals(Vector2.zero))
        {
            playableCreature.moveController.Movement(moveJoystick.GetInputVector());

            if (canRotate) playableCreature.moveController.Rotate(moveJoystick.GetInputVector());
        }
        else
        {
            playableCreature.moveController.Movement(moveVec);

            if (canRotate) playableCreature.moveController.Rotate(moveVec);
        }

        playableCreature.moveController.Rotate(attackJoystick.GetInputVector());
        playableCreature.moveController.Rotate(parringJoystick.GetInputVector());

        if (!attackJoystick.GetInputVector().Equals(Vector3.zero))
        {
            playableCreature.attackController.NormalAttack(true);
        }

        else
        {
            playableCreature.attackController.NormalAttack(false);
        }

        if (!skillJoysrick.GetInputVector().Equals(Vector3.zero) && !playableCreature.RunningWeaponsMotion())
        {
            StartCoroutine(SkillControl());
        }

        if (!parringJoystick.GetInputVector().Equals(Vector3.zero) && !playableCreature.RunningWeaponsMotion())
        {
            playableCreature.attackController.Parry();
        }
    }

    bool runSkillCoroutine = false;

    private IEnumerator SkillControl() 
    {
        if (runSkillCoroutine) yield break;
        runSkillCoroutine = true;

        yield return new WaitForSeconds(0.1f);

        for(int index = 0; index < playableCreature.weapons.Count; index++)
        {
            playableCreature.attackController.UseSkill(skillJoysrick.GetInputVector(), playableCreature.weapons[index]);
        }

        runSkillCoroutine = false;
    }

    public void ActiveLootButton(Weapon lootWeapon, bool value)
    {
        if(value) currentLootWepaon = lootWeapon;
        else if(currentLootWepaon != null && currentLootWepaon.GetInstanceID().Equals(lootWeapon.GetInstanceID()))
        {
            currentLootWepaon = null;
        }
    }
}
