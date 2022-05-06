using System;
using UnityEngine;

namespace Player
{
    public class DedScreem : MonoBehaviour
    {
        [SerializeField] private PlayerHelth _helth;
        [SerializeField] private CanvasGroup dathScen;
        [SerializeField] private Transform ledger;
        [SerializeField] private LedgerLine _ledgerLine;

        private bool ded;
        
        private void Start() => _helth.dedHa.AddListener(DeadTime);

        private void Update()
        {
            dathScen.alpha = Mathf.Lerp(dathScen.alpha, ded ? 1f : 0f, .25f);
        }

        private void DeadTime()
        {
            ded = true;
            foreach (string block in KillCounter.Instance.theBlockchain)
            {
                LedgerLine newLedger = Instantiate(_ledgerLine.gameObject, ledger).GetComponent<LedgerLine>();
                newLedger.ledger.SetText(block);
            }
        }
    }
}