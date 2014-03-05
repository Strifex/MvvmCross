﻿using System;
using System.Linq;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using MonoMac.AppKit;
using MonoMac.Foundation;
using System.Collections;
using System.Collections.Generic;
using Loqu8.KVC.Mac;
using Cirrious.CrossCore.Platform;

namespace Cirrious.MvvmCross.Binding.Mac
{
	// https://developer.apple.com/library/mac/documentation/cocoa/Conceptual/CollectionViews/Introduction/Introduction.html

	public class MvxNSTreeControllerItemsSourceTargetBinding : MvxConvertingTargetBinding
	{
		protected NSTreeController Controller
		{
			get { return base.Target as NSTreeController; }
		}

		public MvxNSTreeControllerItemsSourceTargetBinding(object target)
			: base(target)
		{
			var treeController = Controller;
			if (treeController == null)
			{
				MvxBindingTrace.Trace(MvxTraceLevel.Error,
					"Error - NSTreeController is null in MvxNSNSTreeControllerItemsSourceTargetBinding");
			}
			else
			{
			}
		}

		public override Type TargetType {
			get {
				return typeof(NSTreeController);
			}
		}

		protected override void SetValueImpl (object target, object value)
		{
			if (value is IEnumerable) {
				// clear content (note that an alternative is an arraycontroller -> treecontroller -> outlineview)

				// TODO: This almost works but no good way of clearing out the old entries
				// without traversing the tree. Better to hook to NSArrayController

				var items = (IEnumerable)value;
				foreach (var item in items) {
					var wrapped = new KVCWrapper (item);		// not recursive, implement KVCWrapper in KVCWrapper...
					Controller.AddObject (wrapped);
				}
			}
		}

		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.OneWay; }
		}

		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			if (isDisposing)
			{
				var treeController = Controller;
				if (treeController != null)
				{
					// release
				}
			}
		}
	}
}

