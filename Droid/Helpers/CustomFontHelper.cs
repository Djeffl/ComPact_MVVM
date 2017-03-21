using System;
using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Widget;

namespace ComPact.Droid.Helpers
{
	public class CustomFontHelper
	{

		/**
		 * Sets a font on a textview based on the custom com.my.package:font attribute
		 * If the custom font attribute isn't found in the attributes nothing happens
		 * @param textview
		 * @param context
		 * @param attrs
		 */
		//public static void SetCustomFont(TextView textview, Context context, IAttributeSet attrs)
		//{
		//	TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.CustomFont);
		//	String font = a.GetString(Resource.Styleable.CustomFont_font);
		//	setCustomFont(textview, font, context);
		//	a.recycle();
		//}

		///**
		// * Sets a font on a textview
		// * @param textview
		// * @param font
		// * @param context
		// */
		//public static void setCustomFont(TextView textview, String font, Context context)
		//{
		//	if (font == null)
		//	{
		//		return;
		//	}
		//	Typeface tf = FontCache.get(font, context);
		//	if (tf != null)
		//	{
		//		textview.setTypeface(tf);
		//	}
		//}

	}
}

//public class FontCache
//{

//	private static Hashtable<String, Typeface> fontCache = new Hashtable<String, Typeface>();

//	public static Typeface get(String name, Context context)
//	{
//		Typeface tf = fontCache.get(name);
//		if (tf == null)
//		{
//			try
//			{
//				tf = Typeface.createFromAsset(context.getAssets(), name);
//			}
//			catch (Exception e)
//			{
//				return null;
//			}
//			fontCache.put(name, tf);
//		}
//		return tf;
//	}
//}
