using System;
using System.Linq.Expressions;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Helpers
{
	public class CustomBinding<TSource, TTarget> : Binding<TSource, TTarget>
	{
		public override event EventHandler ValueChanged;

		public CustomBinding(object source, string sourcePropertyName, object target, string targetPropertyName, BindingMode mode, TSource fallbackValue, TSource targetNullValue)
			:base(source, sourcePropertyName, target = null, targetPropertyName, mode, fallbackValue, targetNullValue){ 
		}

		public CustomBinding(object source, Expression<Func<TSource>> sourcePropertyExpression, object target, Expression<Func<TTarget>> targetPropertyExpression, BindingMode mode, TSource fallbackValue, TSource targetNullValue)
			:base(source, sourcePropertyExpression, target = null, targetPropertyExpression = null, mode = BindingMode.Default, fallbackValue, targetNullValue)
		{ 
		}

		public override void Detach()
		{
		}

		public override void ForceUpdateValueFromSourceToTarget()
		{
			ValueChanged?.Invoke(this, EventArgs.Empty);
		}

		public override void ForceUpdateValueFromTargetToSource()
		{
			ValueChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
