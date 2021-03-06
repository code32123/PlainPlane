using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	public class LandingGear : MonoBehaviour
	{

		private enum GearState
		{
			Raised = -1,
			Lowered = 1
		}

		// The landing gear can be raised and lowered at differing altitudes.
		// The gear is only lowered when descending, and only raised when climbing.

		// this script detects the raise/lower condition and sets a parameter on
		// the animator to actually play the animation to raise or lower the gear.

		//public float raiseAtAltitude = 40;
		//public float lowerAtAltitude = 40;

		private GearState m_State = GearState.Lowered;
		private Animator m_Animator;
		public bool lowered = true;
		//private Rigidbody m_Rigidbody;
		//private AeroplaneController m_Plane;
		private float nextFire;

		public GameObject hud;
		public Transform MobilePanel;
		public MobileButtons GearBtn;
		public bool Mobile;

		// Use this for initialization
		private void Start()
		{
			//m_Plane = GetComponent<AeroplaneController>();
			m_Animator = GetComponent<Animator>();
			//m_Rigidbody = GetComponent<Rigidbody>();
			hud = GameObject.Find("/HUD");
			MobilePanel = hud.transform.Find("MobilePanel");
			GearBtn = MobilePanel.transform.Find("GearButton").GetComponent<MobileButtons>();
			GearBtn.toggledState = true;
			Mobile = GameObject.Find("INFO_OBJECT").GetComponent<INFO_SCRIPT>().MOBILE_CONTROLS_ENABLED;

		}

		// Update is called once per frame

		private void Update()
		{
			if (Mobile && Time.time > nextFire)
			{
				if (GearBtn.toggledState)
				{
					m_State = GearState.Lowered;
					lowered = true;
				}
				else
				{
					m_State = GearState.Raised;
					lowered = false;
				}
			}
			if (Input.GetButtonDown("LandingGear") && Time.time > nextFire) {
				if (m_State == GearState.Raised) {
					m_State = GearState.Lowered;
					lowered = true;
				} else {
					m_State = GearState.Raised;
					lowered = false;
				}
				nextFire = Time.time + 0.25f;
			}
			//if (m_State == GearState.Lowered && m_Plane.Altitude > raiseAtAltitude && m_Rigidbody.velocity.y > 0)
			//{
			//    m_State = GearState.Raised;
			//}

			//if (m_State == GearState.Raised && m_Plane.Altitude < lowerAtAltitude && m_Rigidbody.velocity.y < 0)
			//{
			//    m_State = GearState.Lowered;
			//}

			// set the parameter on the animator controller to trigger the appropriate animation
			m_Animator.SetInteger("GearState", (int) m_State);//LandingGear
		}
	}
}
