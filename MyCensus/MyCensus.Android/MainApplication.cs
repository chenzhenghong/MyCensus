using System;
using System.Threading;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Content;
using Android.Views;
using Android.Widget;

using Plugin.CurrentActivity;

using MyCensus;
using HockeyApp.Android;

namespace MyCensus.Droid
{
    //�����ڸ�������ָ������Ӧ�ó�����Ϣ

//#if DEBUG
//    [Application(Debuggable = true)]
//#else
//	    [Application(Debuggable = false)]
//#endif

    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        //handle=javaReference
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {

        }



        public override void OnCreate()
        {

    
            base.OnCreate();
            //ע��HockeyApp �����ռ�ϵͳ������Ϣ
            RegisterActivityLifecycleCallbacks(this);
            //HockeyApp.Android.CrashManager.Register(this, GlobalSettings.HockeyAppAPIKeyForAndroid, new AutoReportingCrashManagerListener());
            //ע��δ�����쳣�¼�
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            //CrashHandler crashHandler = CrashHandler.GetInstance();
            //crashHandler.Init(ApplicationContext);
        }

        void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            UnhandledExceptionHandler(e.Exception, e);
        }

    
        /// <summary>
        /// ����δ�����쳣
        /// </summary>
        /// <param name="e"></param>
        private void UnhandledExceptionHandler(Exception ex, RaiseThrowableEventArgs e)
        {
            //������򣨼�¼ �쳣���豸��Ϣ��ʱ�����Ҫ��Ϣ��
            //**************
            //��ʾ
            Task.Run(() =>
            {
                Looper.Prepare();
                //���Ի��ɸ��Ѻõ���ʾ
                //Toast.MakeText(this, "�ܱ�Ǹ,��������쳣,�����˳�." + ex.Message + "; " + ex.StackTrace, ToastLength.Long).Show();
                Toast.MakeText(this, "�ܱ�Ǹ,��������쳣,�����˳�.", ToastLength.Long).Show();
                Looper.Loop();
            });

            //ͣһ�ᣬ��ǰ��Ĳ�������
            System.Threading.Thread.Sleep(2000);

            e.Handled = true;
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        protected override void Dispose(bool disposing)
        {
            AndroidEnvironment.UnhandledExceptionRaiser -= AndroidEnvironment_UnhandledExceptionRaiser;
            base.Dispose(disposing);
        }
    }

	internal class AutoReportingCrashManagerListener : CrashManagerListener 
	{
        /// <summary>
        /// �Զ��ϴ�������Ϣ
        /// </summary>
        /// <returns></returns>
		public override bool ShouldAutoUploadCrashes() => true;
	}
}