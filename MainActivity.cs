using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Microsoft.Data.Sqlite;
using Milionare.Properties;
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Xamarin.Essentials;
using static Java.Text.Normalizer;
using static Xamarin.Essentials.Platform;

[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]

namespace Milionare
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    // set globals
    //    ((App) App.Current).ContinueOrNot = true;
    public class MainActivity : AppCompatActivity
    {
        Button btnStart;
        string connectionString;
        string pathToDB;
        string Jaut,A,B,C,D,Atb,test="";
        TextView txtTest;
        readonly string[] Permission =
        {
            Android.Manifest.Permission.WriteExternalStorage

        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            RequestPermissions(Permission, 1);
            Tiesibas();
            LinearLayout ll = new LinearLayout(this);
            var lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.MatchParent);
            txtTest = FindViewById<TextView>(Resource.Id.txtTest);
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnStart.Click += delegate
            {
                //kopēt uz external un atvērt DB
                var folders = this.GetExternalMediaDirs();
                var FolderPath = folders[0].AbsolutePath;
                pathToDB = Path.Combine(FolderPath, "million.db");
                connectionString = $"Data Source={pathToDB}"; //bez ;
                using (var source = Resources.OpenRawResource(Resource.Raw.million))
                {
                    using (var destination = File.Open(pathToDB, FileMode.Create))
                    {
                        source.CopyTo(destination);
                    }
                }
                Preferences.Set("KeepGoing", "1");  //Default globālā inicializācija, lai for ciklā būtu jau vērtība!
                try
                {
                    if (File.Exists(pathToDB))
                    {
                        //Toast.MakeText(this, "Database allready exists", ToastLength.Long).Show();
                        //string SQLDB= "select * from test where first_name like @param"
                        //GetData("select * from test");
                        //SELECT * FROM questansw where lvl=1 ORDER BY RANDOM() LIMIT 1;
                        Android.Content.Intent frm = new Android.Content.Intent(this, typeof(QuestionActivity));
                        for (var i = 10; i > 0 && Preferences.Get("KeepGoing", "1") == "1"; i--)    // && Intent.GetStringExtra("Continue") != "false"
                        {   //mainu ciklu otradi, jo vispirms izpildas viss onCreate un tikai tad attēlo intentus!
                            frm.PutExtra("filter", i.ToString());
                            GetData("SELECT * FROM questansw where lvl=@lvl ORDER BY RANDOM() LIMIT 1;", "lvl", i.ToString());
                            frm.PutExtra("jautajums", Jaut);
                            frm.PutExtra("A", A);
                            frm.PutExtra("B", B);
                            frm.PutExtra("C", C);
                            frm.PutExtra("D", D);
                            frm.PutExtra("Atb", Atb);
                            StartActivity(frm);
                            //if (Intent.GetStringExtra("Continue")!=null)
                            //{
                            //    test = Intent.GetStringExtra("Continue");
                            //}
                            txtTest.Text = test;
                            if (test == "false")  //Intent.GetBooleanExtra("Continue", true) == false
                            {
                                Toast.MakeText(this, "Spēles beigas parādam rezultātu formu", ToastLength.Long).Show();
                                //end
                                //return;
                                //piedāvā pogu ar Share:
                                //ShareFile(filename);
                            }
                        }

                        return; //Database file already exists in the external storage
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                    //throw;    //varbūt nevajag uzreiz visu aizvērt
                }

                

            };


        }

        public void GetData(string SQL, string ParamName, string ParamValue)   //, string ParamName, string ParamValue
        {
            //string data = "";
            try
            {
                using (var dbConn = new SqliteConnection(connectionString))
                {
                    dbConn.Open();
                    using (SqliteCommand cmd = new SqliteCommand(SQL, dbConn))
                    {
                        //cmd.ExecuteNonQuery();
                        //cmd.Parameters.AddWithValue("param", "U%");
                        cmd.Parameters.AddWithValue(ParamName, ParamValue);
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                //var fieldcount = dr.FieldCount;
                                //for(int i = 0; i < fieldcount; i++)
                                //{
                                //    data += dr[i] + "";
                                //}
                                //txtData.Text = data + "\n";
                                //data = "";

                                //txtData.Text += dr[0].ToString() + dr[1].ToString() +System.Environment. ...ToString()?
                                //txtData += $"{dr[0].ToString()} {dr[1].ToString()} \n";
                                Jaut = dr[1].ToString();
                                A = dr[2].ToString();
                                B = dr[3].ToString();
                                C = dr[4].ToString();
                                D = dr[5].ToString();
                                Atb = dr[6].ToString();
                                // System.Console.WriteLine(dr[2].ToString());
                            }
                        }
                    }
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }
        private void ShareFile(string Filename) //noderes lai dalitos ar rezultatu
        {
            Share.RequestAsync(new ShareFileRequest
            {
                //File = new ShareFile(System.IO.Path.Combine(FolderPath, Filename)),
                //Title = Filename
            });
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void Tiesibas()
        {
            if (CheckSelfPermission(Android.Manifest.Permission.WriteExternalStorage)
                != Android.Content.PM.Permission.Granted)
            {
                RequestPermissions(Permission, 0);
            }
        }
    }
}