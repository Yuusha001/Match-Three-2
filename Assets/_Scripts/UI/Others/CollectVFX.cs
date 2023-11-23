using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace MatchThree
{
    public class CollectVFX : MonoBehaviour
    {
        public int range = 150;
        public float moveTime = 0.5f;
        public float delayTime = 0.3f;
        public Transform endPos;
        public Sprite icon;

        [Button("Replace")]
        private void ReplaceIcon()
        {
            this.name = icon.name + " VFX";
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = icon;
            }
        }

        [Button("Test")]
        private void Test()
        {
            PlayFx(null);
        }
        public async void PlayFx(System.Action callBack)
        {
            /*if (GameManager.GameState == GameState.PLAYING) return;*/
            if (!gameObject.activeInHierarchy) return;
            await DelayPlayFx(callBack);
            
        }

        async UniTask DelayPlayFx(System.Action callBack)
        {
            await UniTask.WaitForSeconds(0.5f);
            /*AudioManager.Instance.PlayOneShot("eff_coin_ui", 0.8f);*/
            await GetComponent<Image>().DOFade(0.7f, moveTime / 2).OnComplete(() =>
            {
                GetComponent<Image>().DOFade(0, moveTime / 2).SetDelay(moveTime / 2);
            }).AsyncWaitForCompletion().AsUniTask();
            var tasks = new UniTask[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform curChild = transform.GetChild(i);
                curChild.gameObject.SetActive(true);
                int ranNumX = Random.Range(-range, range);
                int ranNumY = Random.Range(-range, range);
                curChild.localPosition = new Vector3(ranNumX, ranNumY);
                curChild.localScale = Vector3.zero;
                tasks[i] = curChild.DOScale(1, moveTime).SetEase(Ease.OutElastic).SetDelay(Random.Range(0, 0.3f)).OnComplete(() =>
                {
                    curChild.DOMove(endPos.position, moveTime).SetEase(Ease.InOutQuad).SetDelay(delayTime).OnComplete(() =>
                    {
                        curChild.gameObject.SetActive(false);
                        //AudioManager.Instance.PlayOneShot("eff_collect_coin", 0.8f);
                        callBack.Invoke();
                    });
                }).AsyncWaitForCompletion().AsUniTask();
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
