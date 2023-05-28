using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using static Java.Text.Normalizer;

namespace Milionare.Properties
{
    [Activity(Label = "QuestionActivity")]
    public class QuestionActivity : Activity
    {
        TextView txtJautView;
        Button btnA, btnB, btnC, btnD;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            LinearLayout ll = new LinearLayout(this);
            var lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.MatchParent);
            SetContentView(Resource.Layout.Jautajumi);
            btnA = FindViewById<Button>(Resource.Id.btnA);
            btnB = FindViewById<Button>(Resource.Id.btnB);
            btnC = FindViewById<Button>(Resource.Id.btnC);
            btnD = FindViewById<Button>(Resource.Id.btnD);
            txtJautView = FindViewById<TextView>(Resource.Id.txtJautView);
            btnA.Visibility = ViewStates.Visible;
            btnB.Visibility = ViewStates.Visible;
            btnC.Visibility = ViewStates.Visible;
            btnD.Visibility = ViewStates.Visible;
            txtJautView.Text = Intent.GetStringExtra("jautajums");
            btnA.Text = Intent.GetStringExtra("A");
            btnB.Text = Intent.GetStringExtra("B");
            btnC.Text = Intent.GetStringExtra("C");
            btnD.Text = Intent.GetStringExtra("D");
            btnA.Click += delegate
            {
                CheckButton("1");
                return;
            };
            btnB.Click += delegate
            {
                CheckButton("2");
                return;
            };
            btnC.Click += delegate
            {
                CheckButton("3");
                return;
            };
            btnD.Click += delegate
            {
                CheckButton("4");
                return;
            };
            // Create your application here
        }

        private void CheckButton(string poga)
        {
            if (poga == Intent.GetStringExtra("Atb"))
            {   //pareiza atbilde

                switch (poga)
                {       ////SetBackgroundColor(Android.Graphics.Color.Green);
                    case "1":
                        btnA.Background.SetColorFilter(Android.Graphics.Color.Orange, Android.Graphics.PorterDuff.Mode.Multiply);
                        break;
                    case "2":
                        btnB.Background.SetColorFilter(Android.Graphics.Color.Orange, Android.Graphics.PorterDuff.Mode.Multiply);
                        break;
                    case "3":
                        btnC.Background.SetColorFilter(Android.Graphics.Color.Orange, Android.Graphics.PorterDuff.Mode.Multiply);
                        break;
                    case "4":
                        btnD.Background.SetColorFilter(Android.Graphics.Color.Orange, Android.Graphics.PorterDuff.Mode.Multiply);
                        break;
                    default:
                        Toast.MakeText(this, "Atbilde ir pareiza", ToastLength.Long).Show();
                        break;
                }
                Finish();
                //ja dialog logā yes un tā ir pareiza atbilde
                //switch (poga)
                //{       ////SetBackgroundColor(Android.Graphics.Color.Green);
                //    case "1":
                //        btnA.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                //        break;
                //    case "2": 
                //        btnB.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                //        break;
                //    case "3": 
                //        btnC.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                //        break;
                //    case "4": 
                //        btnD.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                //        break;
                //    default:    Toast.MakeText(this, "Atbilde ir pareiza", ToastLength.Long).Show();
                //        break;
                //}
            }
            else
            {   //kļūdaina atbilde - spēle zaudēta
                Toast.MakeText(this, "kļūdaina atbilde", ToastLength.Long).Show();
                Android.Content.Intent frm = new Android.Content.Intent(this, typeof(MainActivity));
                frm.PutExtra("Continue", "false");
                Finish();
                SetContentView(Resource.Layout.activity_main);
            }
        }
    }
}