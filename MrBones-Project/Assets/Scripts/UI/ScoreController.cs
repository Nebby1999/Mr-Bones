using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI
{
    public class ScoreController : MonoBehaviour
    {
        public FloatReference score;
        public TextMeshProUGUI text;

        private float currentScore;

        private void Awake()
        {
            currentScore = score.Value;
        }

        private void Update()
        {
            text.text = $"SCORE: {score.Value}";
        }
    }
}
