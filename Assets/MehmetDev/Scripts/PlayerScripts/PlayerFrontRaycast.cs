using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrontRaycast : MonoBehaviour
{
    public float detectionDistance = 10f;
    public float raycastHeightOffset = 1.0f;
    public Animator animator;

    private bool isAttack = false;
    private bool hasKey = false;
    private GameObject currentEnemy;
    private List<Color> keyColors = new List<Color>();
    [SerializeField] private GameController gameController;
    public void CheckForObjectsInFront(Vector3 direction)
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * raycastHeightOffset;
        Vector3 rayDirection = direction.normalized;
        Debug.DrawRay(rayOrigin, rayDirection * detectionDistance, Color.red);

        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance))
        {

            if (hit.collider.TryGetComponent<Key>(out Key key))
            {
                Debug.Log("Anahtar bulundu!");
                hasKey = true;
                keyColors.Add(key.Getcolor());
                Destroy(hit.collider.gameObject);

            }
            else if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (hasKey)
                {
                    if (keyColors.Contains(door.GetDoorColor()))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    else
                    {
                        Debug.Log("kapı rengi ile anahtar rengi uyuşmuyor");
                    }
                }
                else
                {
                    Debug.Log("Kapı açılmadı, anahtar gerekiyor.");
                }
            }

            else if (hit.collider.TryGetComponent<HingeJointDoor>(out HingeJointDoor hingeJointDoor))
            {
                if (hasKey)
                {
                    if (keyColors.Contains(hingeJointDoor.GetDoorColor()))
                    {
                        hingeJointDoor.ColliderTrue();
                    }
                    else
                    {
                        Debug.Log("kapı rengi ile anahtar rengi uyuşmuyor");
                    }

                }
                else
                {
                    Debug.Log("Kapı açılmadı, anahtar gerekiyor.");
                }
            }

            else if (hit.collider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
            {
                if (enemy.GetComponent<EnemyLevelController>().GetEnemyLevel() < this.GetComponent<PlayerLevelController>().GetCurrentLevel())
                {
                    isAttack = true;
                    animator.SetBool("isAttack", isAttack);
                    currentEnemy = hit.collider.gameObject;
                    enemy.GetComponent<EnemyLevelController>().enemyLevelText.enabled = false;
                    enemy.GetComponent<EnemyLevelController>().GetDeathImage();
                    StartCoroutine(HandleAttack());
                }
                else
                {
                    Debug.Log("oyuncu leveli yetmiyor");
                }
            }
            else if (hit.collider.TryGetComponent<PlayerBookUpgrade>(out PlayerBookUpgrade book))
            {
                PlayerLevelController playerLevelController = GetComponent<PlayerLevelController>();
                int levelIncrease = book.GetLevelIncrease();
                playerLevelController.IncreaseLevel(levelIncrease);
                Destroy(hit.collider.gameObject);
            }
            else if(hit.collider.TryGetComponent<WinnerDoor>(out WinnerDoor winner))
            {
                Debug.Log("winner winner chicken dinner");
                gameController.Winner();
            }
        }
    }

    private IEnumerator HandleAttack()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        if (currentEnemy != null)
        {
            Destroy(currentEnemy);
            currentEnemy = null;
        }

        isAttack = false;
        animator.SetBool("isAttack", isAttack);
    }
}
