using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ComPact.Droid
{
	public class CustomToolbar : ViewGroup
	{
		int deviceWidth;
		Context _context;
		public CustomToolbar(Context context)
			: this(context, null, 0)
		{
		}

		public CustomToolbar(Context context, IAttributeSet attrs)
			: this(context, attrs, 0)
		{
		}

		public CustomToolbar(Context context, IAttributeSet attrs, int defStyleAttr)
			: base(context, attrs, defStyleAttr)
		{
			init(context);
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			int count = ChildCount;
			int curWidth, curHeight, curLeft, curTop, maxHeight;
			//get the available size of child view
			int childLeft = PaddingLeft;
			int childTop = PaddingTop;
			int childRight = MeasuredWidth - PaddingRight;
			int childBottom = MeasuredHeight - PaddingBottom;
			int childWidth = childRight - childLeft;
			int childHeight = childBottom - childTop;
			maxHeight = 0;
			curLeft = childLeft;
			curTop = childTop;
			for (int i = 0; i < count; i++)
			{
				View child = GetChildAt(i);
				if (child.Visibility == ViewStates.Gone)
					return;
				//Get the maximum size of the child
				child.Measure(MeasureSpec.MakeMeasureSpec(childWidth, MeasureSpecMode.AtMost), MeasureSpec.MakeMeasureSpec(childHeight, MeasureSpecMode.AtMost));
				curWidth = child.MeasuredWidth;
				curHeight = child.MeasuredHeight;
				//wrap is reach to the end
				if (curLeft + curWidth >= childRight)
				{
					curLeft = childLeft;
					curTop += maxHeight;
					maxHeight = 0;
				}
				//do the layout
				child.Layout(curLeft, curTop, curLeft + curWidth, curTop + curHeight);
				//store the max height
				if (maxHeight < curHeight)
					maxHeight = curHeight;
				curLeft += curWidth;
			}
		}

		protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			int count = ChildCount;
			// Measurement will ultimately be computing these values.
			int maxHeight = 0;
			int maxWidth = 0;
			int childState = 0;
			int mLeftWidth = 0;
			int rowCount = 0;
			// Iterate through all children, measuring them and computing our dimensions
			// from their size.
			for (int i = 0; i < count; i++)
			{
				View child = GetChildAt(i);
				if (child.Visibility == ViewStates.Gone)
					continue;
				// Measure the child.
				MeasureChild(child, widthMeasureSpec, heightMeasureSpec);
				maxWidth += Math.Max(maxWidth, child.MeasuredWidth);
				mLeftWidth += child.MeasuredWidth;
				if ((mLeftWidth / deviceWidth) > rowCount)
				{
					maxHeight += child.MeasuredHeight;
					rowCount++;
				}
				else
				{
					maxHeight = Math.Max(maxHeight, child.MeasuredHeight);
				}
				childState = CombineMeasuredStates(childState, child.MeasuredState);
			}
			// Check against our minimum height and width
			maxHeight = Math.Max(maxHeight, SuggestedMinimumHeight);
			maxWidth = Math.Max(maxWidth, SuggestedMinimumWidth);
			// Report our final dimensions.
			SetMeasuredDimension(ResolveSizeAndState(maxWidth, widthMeasureSpec, childState),
			                     ResolveSizeAndState(maxHeight, heightMeasureSpec, childState << MeasuredHeightAndState));
		}

		private void init(Context context)
		{
			_context = context;
			Display display = ((IWindowManager)context.GetSystemService(Context.WindowService)).DefaultDisplay;
			Point deviceDisplay = new Point();
			display.GetSize(deviceDisplay);
			deviceWidth = deviceDisplay.X;

		}
	}
}
