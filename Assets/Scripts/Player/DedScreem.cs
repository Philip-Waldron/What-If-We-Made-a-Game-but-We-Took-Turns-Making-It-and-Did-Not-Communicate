using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Player
{
    public class DedScreem : MonoBehaviour
    {
        [SerializeField] private PlayerHelth _helth;
        [SerializeField] private CanvasGroup dathScen;
        [SerializeField] private Transform ledger;
        [SerializeField] private LedgerLine _ledgerLine;
        [SerializeField] private TextMeshProUGUI results;
        [SerializeField] private float lodgerDuration = .25f;
        [SerializeField] private AudioSource _dedMusic;
        [SerializeField] private AudioSource _aliveMusic;
        [SerializeField] private float _fadeTime;
        [SerializeField] private ThinkGun _thinkGun;

        private bool ded;

        private void Start() => _helth.dedHa.AddListener(DeadTime);

        private string[] _stringchain;

        private void Update()
        {
            dathScen.alpha = Mathf.Lerp(dathScen.alpha, ded ? 1f : 0f, .25f);
        }

        private void DeadTime()
        {
            ded = true;
            _stringchain = KillCounter.Instance.theBlockchain.ToArray();
            results.SetText($"Despite your best efforts, NFTs have prevailed. You managed to hack {KillCounter.Instance.killerCount} NFTS and destroy multiple marriages, with overall {KillCounter.Instance.ecomomicIMPACT} ETH destroyed, all this in only {PlayerTimerAlive.Instance.levelTimer} seconds, well done!");
            StartCoroutine(LodgeLedger());
            _dedMusic.Play();
            StartCoroutine(FadeMusic(_fadeTime));
            _thinkGun.StopMeIfYouDare = true;
        }

        private IEnumerator LodgeLedger()
        {
            foreach (string block in _stringchain)
            {
                LedgerLine newLedger = Instantiate(_ledgerLine.gameObject, ledger).GetComponent<LedgerLine>();
                newLedger.transform.SetAsFirstSibling();
                newLedger.ledger.SetText(block);
                yield return new WaitForSeconds(lodgerDuration);
            }
        }

        private IEnumerator FadeMusic(float fadeTime)
        {
            float elapsedTime = 0;
            float startVolume = _aliveMusic.volume;
            while (elapsedTime < fadeTime)
            {
                print(Mathf.Lerp(startVolume, 0, elapsedTime / fadeTime));
                _aliveMusic.volume = Mathf.Lerp(startVolume, 0, elapsedTime / fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _aliveMusic.volume = 0;
            _aliveMusic.Stop();
        }
    }
}