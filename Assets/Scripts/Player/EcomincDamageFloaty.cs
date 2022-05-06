using TMPro;
using UnityEngine;

namespace Player
{
    public class EcomincDamageFloaty : MonoBehaviour
    {
        public TextMeshPro impact;

        public void CreateFlaoty(float damage)
        {
            impact.SetText($"{damage}.eth");
        }
    }
}