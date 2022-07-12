using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Inventories.Consumable
{
    public class ConsnumeItem : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        private void Start()
        {
            _input = GetComponentInParent<StarterAssetsInputs>();
        }

        private void Update()
        {
            if (transform.childCount > 0)
            {
                if (_input.consume)
                {
                    Use();
                    _input.consume = false;
                }
            }
        }

        public void Use()
        {
           transform.GetChild(0).GetComponent<Consumable>().UseConsumableItem();
        }
    }
}
