using System.Collections;
using UnityEngine;

namespace Telov.Utils
{
    public interface ICoroutinesHost
    {
        public Coroutine StartCoroutine(IEnumerator routine);
        public void StopCoroutine(IEnumerator routine);
        public void StopCoroutine(Coroutine routine);
        public void StopAllCoroutines();
    }
}