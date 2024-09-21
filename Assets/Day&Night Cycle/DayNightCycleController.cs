using UnityEngine;

public class DayNightCycleController : MonoBehaviour
{
	public Transform RotationBase;

	public Light    Sun;
	public Gradient SunColour;

	public Light    Moon;
	public Gradient MoonColour;

	public Light          Indoor;
	public AnimationCurve IndoorIntensity;

	public float speed = 100;

	[Range(0, 24)]
	public double timeOfDay = 12.0f;


	
	public void Start()
	{
	}


	public void Update()
	{
		double deltaHour = (Time.deltaTime * speed) / 60.0 / 60.0;
		timeOfDay += deltaHour;
		while (timeOfDay <   0) { timeOfDay += 24; }
		while (timeOfDay >= 24) { timeOfDay -= 24; }
		UpdateElements();
	}


	public void OnValidate()
	{
		UpdateElements();
	}


	private void UpdateElements()
	{
		float t = (float)(timeOfDay / 24.0);

        if (RotationBase != null)
        {
            // rotate celestial bodies (+180: Objects in editor are set up for noon)
            RotationBase.localRotation = Quaternion.AngleAxis(t * 360.0f + 180.0f, Vector3.right);
        }

        if (Sun != null)
        {
            // set sun/moon colour and intensity
            Sun.color = SunColour.Evaluate(t);
            Sun.gameObject.SetActive(Sun.color.maxColorComponent > 0.01f);
        }

        if (Moon != null)
        {
            Moon.color = MoonColour.Evaluate(t);
            Moon.gameObject.SetActive(Moon.color.maxColorComponent > 0.01f);
        }

        if (Indoor != null)
        {
            // control indoor light
            Indoor.gameObject.SetActive(IndoorIntensity.Evaluate(t) > 0.5f);
        }

		// speed change keys
		if (Input.GetKeyDown(KeyCode.LeftBracket))  speed /= 10.0f;
		if (Input.GetKeyDown(KeyCode.RightBracket)) speed *= 10.0f;
	}
}
