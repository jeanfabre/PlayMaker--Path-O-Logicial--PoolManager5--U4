// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Constraints")]
	[Tooltip("Set the speed of a Turntable constraint. TurntableConstraint Component must be attached to the gameObject.")]
	public class UcTurntableSetSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(TurntableConstraint))]
		[Tooltip("The Game Object.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The speed of the turnTable")]
		public FsmFloat speed;
		
		[Tooltip("If true, speed is per second, else speed is per frame.")]
		public FsmBool perSecond;
		
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
			speed = 1f;
			perSecond = false;
			everyFrame = false;
		}

		public override void  OnEnter()
		{
			_getTurntableConstraint();
			
			DoSetSpeed();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void  OnUpdate()
		{
 			 DoSetSpeed();
		}

		void DoSetSpeed()
		{
			if (_turntable == null) 
			{
				return;
			}
			
			if (perSecond.Value){
				_turntable.speed = speed.Value* Time.deltaTime;
			}else{
				_turntable.speed = speed.Value;
			}
			
				
				
		}

	}
}