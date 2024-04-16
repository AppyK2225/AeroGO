using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mapbox.Examples
{
	public class PlayerMotion : MonoBehaviour
	{
		public Material[] Materials;
		public Transform Target;
		public Animator CharacterAnimator;
		public float Speed;
		public GameObject RippleEffect;
		//AstronautMouseController _controller;

		//slowRot
		public Transform rotTarget;
		public float turnSpeed = 0.01f;
		Quaternion rotGoal;
		Vector3 direction;
		//

		void Start()
		{
			//_controller = GetComponent<AstronautMouseController>();
		}

		void Update()
		{


			foreach (var item in Materials)
			{
				item.SetVector("_CharacterPosition", transform.position);
			}

			var distance = Vector3.Distance(transform.position, Target.position);
			if (distance > 5.0f)
			{
				//transform.LookAt(Target.position);
				transform.Translate(Vector3.forward * Speed);

				//slowRot
				direction = (rotTarget.position - transform.position).normalized;
				rotGoal = Quaternion.LookRotation(direction);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
				//

				CharacterAnimator.SetBool("IsWalking", true);
				//RippleEffect.SetActive(false);
			}
			else
			{
				CharacterAnimator.SetBool("IsWalking", false);
				//RippleEffect.SetActive(true);
			}
		}
	}
}