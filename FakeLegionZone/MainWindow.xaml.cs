using FakeLegionZone.Model;
using FakeLegionZone.Plugin;
using FakeLegionZone.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FakeLegionZone
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
		public static MainWindow self;
		public MainWindow()
        {
            InitializeComponent();
			self = this;
			this.Loaded += MainWindow_Loaded;
			
			this.Closing += MainWindow_Closing;
        }

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{ 
            ReloadData();
        }
        
        private string defaultSetting = "{\"FPS\":1,\"GPU\":{\"Clock\":1,\"Usage\":1,\"Temperature\":0,\"Power\":0,\"FanSpeed\":0,\"Mem_Usage\":0},\"CPU\":{\"Clock\":1,\"Usage\":1,\"Temperature\":0,\"Power\":0,\"FanSpeed\":0},\"MEM\":{\"Clock\":1,\"Usage\":1},\"Orientation\":0,\"Location\":1,\"FontSize\":20}";
		public void ReloadData()
		{
            try
            { 
				var isenbale = RegistryHelper.Instance.GetIsPerformMonitor();
				string data = RegistryHelper.Instance.GetPerformMonitorDBData();
				if(string.IsNullOrEmpty(data))
                {
					RegistryHelper.Instance.SetPerformMonitorDBData(defaultSetting);
					data = RegistryHelper.Instance.GetPerformMonitorDBData();
				}
				checkall.IsChecked = isenbale; 
			
				PerformMointorData config = JsonHelper.StringToObject<PerformMointorData>(data);
				logenable.IsChecked = RegistryHelper.Instance.GetEnableLog();   
				this.DataContext = config; 
			}
			catch(Exception ex)
            {
				DialogUtil.info("发生错误:" + ex.Message);
            }
		}

        /// <summary>
		/// 重载插件的hook
		/// </summary>
  //      public static void ReloadPlugHook()
		//{
  //          App.self.UnInjectAll();
  //          App.self.initFirstInject();
  //      }
        
		public static void SaveData()
        {
            try
            {
				var config = self.DataContext as PerformMointorData; 
				if(config!=null)
                {
					if (!RegistryHelper.Instance.SetPerformMonitorDBData(JsonHelper.ObjectToString(config)))
					{
						DialogUtil.info("保存配置失败");
						return;
					} 
					//ReloadPlugHook(); 
                } 
			}
			catch(Exception ex)
            { 
				DialogUtil.info("发生错误:" + ex.Message);
				LogHelper.Log("发生错误：" + ex.Message);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.self.UnInjectAll();
            
            //bool? flag = true;// new WindowExitConfirm().ShowDialog();
            //if (flag != null && flag.Value)
            //{
            //	Optimize.Instance.StartRecovery(null);
            //	Message message = App.self.message;
            //	if (message != null)
            //	{
            //		message.SendExitMessageToLZMain();
            //	}
            //	LogHelper.Log("[App] [Exit_MenuItem_Click] 通知 LZMain 退出。");
            //	Message message2 = App.self.message;
            //	if (message2 != null)
            //	{
            //		message2.SendExitMessage(); 
            //	}
            //	//PluginDll.Instance.StopInfobarData();

            //	LogHelper.Log("[App] [Exit_MenuItem_Click] 通知游戏内工具栏退出。");
            //	PluginDlls.Instance.UninitPlugins();
            //	LogHelper.Log("[App] [Exit_MenuItem_Click] 通知通知所有 plugin dll 退出。");
            //	PluginDll.Instance.UnInitDll();
            //	Application.Current.Shutdown();
            //} 
        }
        
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();  
		}
		 

        private void btn_close_Clik(object sender, RoutedEventArgs e)
        {
			this.Hide();
        }

        private void btn_min_Clik(object sender, RoutedEventArgs e)
        {
			this.WindowState = WindowState.Minimized;
        }

        private void checkall_Click(object sender, RoutedEventArgs e)
        {
			var isopen = checkall.IsChecked == true; 
			var ret = RegistryHelper.Instance.SetIsPerformMonitor(isopen); 
			if(ret ==false)
            {
				DialogUtil.info("设置失败！");
            }
            else
            {
                if(isopen)
                {
                    App.self.initFirstInject();
                }
                //else
                //{
                //    App.self.UnInjectAll();
                //}
			}
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
			Process.Start(new ProcessStartInfo("https://legionzone.lenovo.com/game"));
		}

        private void logenable_Click(object sender, RoutedEventArgs e)
        {
			var isopen = logenable.IsChecked == true;
			var ret = RegistryHelper.Instance.SetEnableLog(isopen);
			if (ret == false)
			{
				DialogUtil.info("设置失败！");
			}
			else
			{
				if (isopen)
				{
					DialogUtil.sucess("设置成功！日志保存在\n【" + LogHelper.pathDirectory + "】\n目录下",5);
				}
				else
				{
					DialogUtil.sucess("设置成功，已关闭日志！");
				}
			}
		}

        /// <summary>
        /// 重载配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogUtil.sucess("已请求重载，可能需要等待数秒！");
            Task.Run(() =>
            {
                App.self.UnInjectAll();
            }).ContinueWith((r) => {
                App.self.initFirstInject();
            });
        }
    }
}
