// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Constraints")]
	[Tooltip("Set the constraints of a Transform constraint. TransformConstraint Component must be attached to the gameObject.")]
	public class UcTransformSetConstraints : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(TransformConstraint))]
		[Tooltip("The Game Object.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("If TRUE, match the target's position")]
		public FsmBool constrainPosition;
		
		[Tooltip("If TRUE, match the target's rotation")]
		public FsmBool constrainRotation;
		
		[Tooltip("If TRUE, match the target's scale")]
		public FsmBool constrainScale;
		
		[ActionSection("")] 
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		
		private TransformConstraint _transformConstraint;
		
		private void _getTransformConstraint()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_transformConstraint =  go.GetComponent<TransformConstraint>();	
		}

		public override void Reset()
		{
			gameObject = null;
			
			constrainPosition = null;
			constrainRotation = null;
			constrainScale = null;
			
			everyFrame = false;
			
		}

		public override void  OnEnter()
		{
			_getTransformConstraint();
			
			DoSetConstraint();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void  OnUpdate()
		{
 			 DoSetConstraint();
		}

		void DoSetConstraint()
		{
			
			if (_transformConstraint == null) 
			{
				return;
			}
			
			if (_transformConstraint.constrainPosition != constrainPosition.Value)
			{
				_transformConstraint.constrainPosition = constrainPosition.Value;
			}
			if (_transformConstraint.constrainRotation != constrainRotation.Value)
			{
				_transformConstraint.constrainRotation = constrainRotation.Value;
			}			
			if (_transformConstraint.constrainScale != constrainScale.Value)
			{
				_transformConstraint.constrainScale = constrainScale.Value;
			}				
				
		}

	}
}