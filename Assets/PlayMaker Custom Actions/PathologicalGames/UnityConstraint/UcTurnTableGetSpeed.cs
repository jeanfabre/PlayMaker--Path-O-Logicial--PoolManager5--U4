// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Constraints")]
	[Tooltip("Get the speed of a Turntable constraint. TurntableConstraint Component must be attached to the gameObject.")]
	public class UcTurntableGetSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(TurntableConstraint))]
		[Tooltip("The Game Object.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The current speed of the turnTable")]
		[UIHint(UIHint.Variable)]
		public FsmFloat speed;
		
		[ActionSection("")] 
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		private TurntableConstraint _turntable;
		
		private void _getTurntableConstraint()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_turntable =  go.GetComponent<TurntableConstraint>();

			
		}

		public override void Reset()
		{
			gameObject = null;
			speed = null;
			everyFrame = false;
		}

		public override void  OnEnter()
		{
			_getTurntableConstraint();
				
			DoGetSpeed();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void  OnUpdate()
		{
 			 DoGetSpeed();
		}

		void DoGetSpeed()
		{
			if (_turntable == null) 
			{
				return;
			}

			speed.Value = _turntable.speed;
						
		}

	}
}