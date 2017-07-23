using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class VoltraceLogo : MonoBehaviour {

	float loadingOpacity;
	public SpriteRenderer voltracelogo;
    public SpriteRenderer whiteBack;


	IEnumerator<float> _ShowLogo(){
		float logoOpacity = 1f;
		logoOpacity = logoOpacity/20.0f;
		float start = 0;
		for(int i = 0; i < 25; i++){
			voltracelogo.color = new Color(1f, 1f ,1f, start);
            start += logoOpacity;
			yield return Timing.WaitForSeconds(0.04f);
		}

        yield return Timing.WaitForSeconds(1.0f);

        for (int i = 0; i < 25; i++)
        {
            voltracelogo.color = new Color(1f, 1f, 1f, start);
            whiteBack.color = new Color(1f, 1f, 1f, start);
            start -= logoOpacity;
            
            yield return Timing.WaitForSeconds(0.04f);
        }

        voltracelogo.gameObject.SetActive(false);
        whiteBack.gameObject.SetActive(false);


    }

	void Start(){
		Timing.RunCoroutine (_ShowLogo());
	}

}
