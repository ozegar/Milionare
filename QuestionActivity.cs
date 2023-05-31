﻿using Android.App;
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
using System.Threading.Tasks;
using static Java.Text.Normalizer;

namespace Milionare.Properties
{
    [Activity(Label = "QuestionActivity")]
    public class QuestionActivity : Activity
    {
        TextView txtJautView;
        Button btnA, btnB, btnC, btnD;
        LinearLayout lJ;
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
            //Android.Content.Intent frm = new Android.Content.Intent(this, typeof(MainActivity));
            //frm.PutExtra("Continue", "true");       //default BackResponse lai nav kludas
            btnA.Visibility = ViewStates.Visible;
            btnB.Visibility = ViewStates.Visible;
            btnC.Visibility = ViewStates.Visible;
            btnD.Visibility = ViewStates.Visible;
            lJ = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            //int skaits = lJ.ChildCount;
            int wi = lJ.Width;
            int pad = ll.PaddingLeft;
            int wdt = wi / 2 - 50;
            btnA.SetWidth(wdt);
            btnB.SetWidth(wdt);
            btnC.SetWidth(wdt);
            btnD.SetWidth(wdt);
            txtJautView.Text = Intent.GetStringExtra("jautajums");
            btnA.Text = "A: " + Intent.GetStringExtra("A");
            btnB.Text = "B: " + Intent.GetStringExtra("B");
            btnC.Text = "C: " + Intent.GetStringExtra("C");
            btnD.Text = "D: " + Intent.GetStringExtra("D");
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
            try
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
                            Toast.MakeText(this, "Pieņemu atbildi(pareiza)", ToastLength.Long).Show();
                            break;
                    }
                    //vispirms dialoga logs, ja ok tad neko(turpinām), ja atcelt, tad return un pogas krāsu reset
                    ShowDialog1(poga);
                    Notify();
                    //bool result = await AlertAsync(this, "My Title", "My Message", "Yes", "No");
                    //displayAlertDialogBox(1);
                    //Wait(100);
                    //Finish();
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
                    //Android.Content.Intent frm = new Android.Content.Intent(this, typeof(MainActivity));
                    //frm.PutExtra("Continue", "false");
                    SetContentView(Resource.Layout.activity_main);
                }
                Finish();
            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                //throw;
            }
        }

        private void ShowDialog1(string poga)
        {
            try
            {
                //var menuItems = new string[]
                //         {"Show file", "Show file in seperate form", "Delete file", "Share file" };

                //EventHandler<DialogClickEventArgs> handler = (s, o) =>  //s = sender
                //{
                    //var msg = menuItems[o.Which];
                    //Toast.MakeText(this, msg, ToastLength.Long).Show();
                    //switch (o.Which)
                    //{
                    //    case 0: //show file
                    //            //                       ShowFile(filename);
                    //        break;
                    //    case 1: //Show file in seperate form
                    //            //                        ShowFile2(filename);
                    //        break;
                    //    case 2: //Delete file
                    //            //                        DeleteFile(filename);
                    //        break;
                    //    case 3: //Share
                    //            //                        ShareFile(filename);
                    //        break;
                    //    default: break;
                    //}
                //};
                Android.App.AlertDialog.Builder alertDialog =
            new Android.App.AlertDialog.Builder(this);
                alertDialog.SetTitle("Vai esi pārliecināts par atbildi");
                //alertDialog.SetItems(menuItems, handler);
                alertDialog.SetPositiveButton("JĀ", (s, o) =>
                {
                    Toast.MakeText(this, "Positive answer", ToastLength.Long).Show();
                    switch (poga)
                    {       ////SetBackgroundColor(Android.Graphics.Color.Green);
                        case "1":
                            btnA.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                            break;
                        case "2":
                            btnB.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                            break;
                        case "3":
                            btnC.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                            break;
                        case "4":
                            btnD.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                            break;
                        default:
                            Toast.MakeText(this, "Atbilde ir pareiza", ToastLength.Long).Show();
                            break;
                    }
                    Finish();
                });
                alertDialog.SetNegativeButton("NĒ", (s, o) =>
                {
                    Toast.MakeText(this, "Negative answer", ToastLength.Long).Show();
                });
                //alertDialog.SetNeutralButton("Iziet",
                //    (sender, args) =>
                //    {
                //        Toast.MakeText(this, "Positive answer", ToastLength.Long).Show();
                //        //alertDialog.Dispose();
                //    });
                //alertDialog.Wait();
                //alertDialog.NotifyAll();
                alertDialog.Show();
            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                //throw;
            }
        }

        public Task<bool> AlertAsync(Context context, string title, string message, string positiveButtonTxt, string negativeButtonTxt)
        {
            var tcs = new TaskCompletionSource<bool>();

            using (var db = new AlertDialog.Builder(context))
            {
                db.SetTitle(title);
                db.SetMessage(message);
                db.SetPositiveButton(positiveButtonTxt, (sender, args) => { tcs.TrySetResult(true); });
                db.SetNegativeButton(negativeButtonTxt, (sender, args) => { tcs.TrySetResult(false); });
                db.Show();
            }
            return tcs.Task;
        }

        async Task displayAlertDialogBox(int loop)
        {
            int ret = 0;
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog dialog = builder.Create();
            dialog.SetTitle("ALERT DIALOG"); dialog.SetMessage(string.Format("COUNT {0} ITEM", loop));
            dialog.SetCancelable(true);
            dialog.SetButton("OK button", (z, ev) => 
            {
                Toast.MakeText(this, string.Format("Ok button {0}", loop), ToastLength.Long).Show();
                ret = 99; 
            });
            dialog.SetButton2("Cancel button", (z, ev) => 
            {
                Toast.MakeText(this, string.Format("Cancel button {0}", loop), ToastLength.Long).Show();
                ret = 1; 
            });

        }
    }
}