using UnityEngine;
using UnityEngine.Rendering;

namespace _Scripts.Effects
{
    public class ChromaticAberrationEffect : MonoBehaviour
    {
        public static ChromaticAberrationEffect Instance { get; private set; }
        private Volume _volume;

        private void Awake()
        {
            Instance = this;
            _volume = GetComponent<Volume>();
        }

        private void Update()
        {
            if (!(_volume.weight > 0)) return;
            const float decreasedSpeed = 1f;
            _volume.weight -= Time.deltaTime * decreasedSpeed;
        }

        public void SetWeight(float weight)
        {
            _volume.weight = weight;
        }
    }
}
