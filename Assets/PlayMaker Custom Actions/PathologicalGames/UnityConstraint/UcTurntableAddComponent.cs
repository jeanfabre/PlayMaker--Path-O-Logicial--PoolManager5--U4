// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Constraints")]
	[Tooltip("Adds a TurntableConstraint Component to a Game Object. Use this to change the behaviour of objects on the fly. Optionally remove the Component on exiting the state.")]
	public class UcTurntableAddComponent : FsmStateAction
	{
		
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The Game Object to add the Component to.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Remove the Component when this State is exited.")]
		public FsmBool removeOnExit;
		
		[ActionSection("Turntable properties")]
		
		[Tooltip("The speed of the turnTable")]
		public FsmFloat speed;
		
		[Tooltip("If true, speed is per second, else speed is per frame.")]
		public FsmBool perSecond;

		[Tooltip("If true, the rotation of the GameObject starts at a random rotation")]
   		public FsmBool randomStart;

		private TurntableConstraint _turntable;

		public override void Reset()
		{
			gameObject = null;
			
			speed = 1f;
			
			perSecond = false;
			
			randomStart= false;
			
			_turntable = null;
		}

		public override void OnEnter()
		{
			DoAddTurntable();
			
			Finish();
		}

		public override void OnExit()
		{
			if (removeOnExit.Value && _turntable != null)
			{
				Object.Destroy(_turntable);
			}
		}

		void DoAddTurntable()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			//Record the current transform and reapply after having created the component, cause the default is true.
			Quaternion _rotation = go.transform.rotation;
			
			_turntable = go.AddComponent<TurntableConstraint>();
			
			TurntableConstraint turnTable = new TurntableConstraint() {randomStart = false, speed = 2f};
			Debug.Log(turnTable.randomStart);
			

			if (_turntable == null)
			{
				LogError("Can't add component: turntableConstraint");
				return;
			}
			
			if (! randomStart.Value)
			{
				// apply the stored quaternion, cause the default set it to random.
				go.transform.rotation = _rotation;
				_turntable.randomStart = randomStart.Value;	
			}			
			
			
			if (perSecond.Value){
				_turntable.speed = speed.Value* Time.deltaTime;
			}else{
				_turntable.speed = speed.Value;
			}
		}
	}
}