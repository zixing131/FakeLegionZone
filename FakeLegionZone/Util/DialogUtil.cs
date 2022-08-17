using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace FakeLegionZone.Util
{
    public class DialogUtil
    {
        public static void sucess(string msg, int second = 3)
        {
            sucess(MainWindow.self.gridMsg, msg, second);
        }

        public static void sucess(Grid GrdContainer, string msg, int second = 3)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (lastTask != null)
                {
                    //取消上一个线程
                    cts.Cancel();
                    cts = new CancellationTokenSource();
                    token = cts.Token;
                    cts.Token.Register(() =>
                    {
                        Console.WriteLine("Grid删除线程退出");
                    });
                }
                Border border = new Border();
                border.Padding = new Thickness(10);
                border.Background = new SolidColorBrush(Colors.Black);
                border.VerticalAlignment = VerticalAlignment.Center;
                border.HorizontalAlignment = HorizontalAlignment.Center;
                border.Margin = new Thickness(0, 0, 0, 200);
                border.CornerRadius = new CornerRadius(5);
                border.BorderBrush = new SolidColorBrush(Colors.Gray); ;
                border.Background = new SolidColorBrush(Colors.White);
                border.Height = 50;
                border.BitmapEffect = new DropShadowBitmapEffect() { Softness = 0.8, Opacity = 0.8, ShadowDepth = 5, Direction = 300 };

                Grid gd = new Grid() { Background = new SolidColorBrush(Colors.White) };
                border.Child = gd;
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                TextBlock textBlock = new TextBlock()
                {
                    Foreground = new SolidColorBrush(Colors.Gray),
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20,
                    Text = msg
                };
                var url = new Uri("pack://application:,,,/images/成功.png", UriKind.RelativeOrAbsolute);
                var stream = Application.GetResourceStream(url).Stream;
                Image image = new Image();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                image.Source = bitmapImage;
                image.UseLayoutRounding = true;
                RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.Fant);
                gd.Children.Add(stackPanel);
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textBlock);
                GrdContainer.Children.Clear();
                GrdContainer.Children.Add(border);
                AnimationHelper.SetBeginTimeSeconds(border, 0);
                AnimationHelper.SetDurationSeconds(border, 0.5);
                AnimationHelper.SetFadeIn(border, true);
                AnimationHelper.SetSlideInFromTop(border, true);

                lastTask = Task.Delay(TimeSpan.FromSeconds(second)).ContinueWith(t =>
                {
                    GrdContainer.Dispatcher.Invoke(() =>
                    {
                        GrdContainer.Children.Clear();
                    });
                }, token);
            });
        }

        private static Task lastTask = null;
        static CancellationTokenSource cts = new CancellationTokenSource();
        static CancellationToken token;
        static DialogUtil()
        {
            token = cts.Token;
            cts.Token.Register(() =>
            {
                Console.WriteLine("Grid删除线程退出");
            });
        }

        public static void info(Grid GrdContainer, string msg, int second = 3, double marginBottom = 200)
        {
            App.Current.Dispatcher.Invoke(() =>
            {

                if (lastTask != null)
                {
                    //取消上一个线程
                    cts.Cancel();
                    cts = new CancellationTokenSource();
                    token = cts.Token;
                    cts.Token.Register(() =>
                    {
                        Console.WriteLine("Grid删除线程退出");
                    });
                }
                Border border = new Border();
                border.Padding = new Thickness(10);
                border.Background = new SolidColorBrush(Colors.Black);
                border.VerticalAlignment = VerticalAlignment.Center;
                border.HorizontalAlignment = HorizontalAlignment.Center;
                border.Margin = new Thickness(0, 0, 0, marginBottom);
                border.CornerRadius = new CornerRadius(5);
                border.Height = 50;
                TextBlock textBlock = new TextBlock()
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20,
                    Text = msg
                };
                border.Child = textBlock;
                GrdContainer.Children.Clear();
                GrdContainer.Children.Add(border);
                AnimationHelper.SetBeginTimeSeconds(border, 0);
                AnimationHelper.SetDurationSeconds(border, 0.5);
                AnimationHelper.SetFadeIn(border, true);
                AnimationHelper.SetSlideInFromTop(border, true);

                lastTask = Task.Delay(TimeSpan.FromSeconds(second)).ContinueWith(t =>
                {
                    GrdContainer.Dispatcher.Invoke(() =>
                    {
                        GrdContainer.Children.Clear();
                    });
                }, token);
            });
        }
        /// <summary>
        /// 显示消息到主界面 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="second"></param>
        public static void info(string msg, int second = 3)
        {
            info(MainWindow.self.gridMsg, msg, second);
        }
    }
}
