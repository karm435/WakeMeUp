using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WakeMeUp.CustomControls
{
    public class WakeUpMap : Map
    {
        public static readonly BindableProperty CurrentPositionProperty = 
            BindableProperty.Create("CurrentPosition", typeof(Position), typeof(WakeUpMap), default(Position), propertyChanged: OnPositionChanged);

        private static void OnPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(newValue is Position newPostion && oldValue is Position oldPosition && bindable is Map map)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(newPostion, Distance.FromKilometers(10)));
            }
        }

        public Position CurrentPosition
        {
            get { return (Position)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }

    }
}
