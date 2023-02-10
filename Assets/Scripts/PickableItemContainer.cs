using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PickableItemContainer : MonoBehaviour
{
    [SerializeField] private PickableItem _item;
    [SerializeField] private byte _amount = 1;

    public byte Amount => _amount;
    public PickableItem Item => _item;

    private void Start()
    {
        StartCoroutine(StartAnimation(GetComponent<Animator>()));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Inventory inventory))
        {
            inventory.TryCollectItem(this);
        }
    }

    private IEnumerator StartAnimation(Animator animator)
    {
        animator.enabled = false;

        yield return new WaitForSeconds(Random.Range(0, 4));

        animator.enabled = true;
    }
}
