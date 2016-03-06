#region License
//   Copyright 2015 Brook Shi
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Effects;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XP
{
    public sealed class Shadow : ContentControl
    {
        const int MAX_Z_DEPTH = 5;
        const int MIN_Z_DEPTH = 1;

        CanvasControl _shadowCanvas;
        ContentPresenter _contentPresenter;
        Grid _topGrid;

        public int Z_Depth
        {
            get { return (int)GetValue(Z_DepthProperty); }
            set { SetValue(Z_DepthProperty, value); }
        }
        public static readonly DependencyProperty Z_DepthProperty =
            DependencyProperty.Register("Z_Depth", typeof(int), typeof(Shadow), new PropertyMetadata(2));

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(Shadow), new PropertyMetadata(0d));

        public bool IsCached { get; set; }


        public Shadow()
        {
            this.DefaultStyleKey = typeof(Shadow);
            SizeChanged += Shadow_SizeChanged;
            Unloaded += Shadow_Unloaded;
        }

        private void Shadow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (!IsCached && _shadowCanvas != null)
            {
                _shadowCanvas.RemoveFromVisualTree();
                _shadowCanvas = null;
            }
        }

        private void Shadow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_shadowCanvas != null && e.NewSize != e.PreviousSize)
            {
                _shadowCanvas.Invalidate();
            }
        }

        private void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (Z_Depth < MIN_Z_DEPTH)
                return;

            var shadowParams = ShadowConfig.GetShadowParamForZDepth(Math.Min(Z_Depth, MAX_Z_DEPTH));
            DrawShadow(sender, args.DrawingSession, shadowParams);
        }

        static void Log(string msg)
        {
            var path = ApplicationData.Current.LocalFolder + "\\log.txt";
            File.AppendAllText(path, msg+"\r\n");
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _shadowCanvas = (CanvasControl)GetTemplateChild("ShadowCanvas");
            _contentPresenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");
            _topGrid = (Grid)GetTemplateChild("PART_Grid");

            if (_shadowCanvas != null)
            {
                _shadowCanvas.Draw += OnDraw;
            }
        }

        void DrawShadow(CanvasControl canvasCtrl, CanvasDrawingSession drawSession, List<ShadowParam> shadowParams)
        {
            var canvasCommandList = new CanvasCommandList(canvasCtrl);
            var content = _contentPresenter.Content as FrameworkElement;
            var contentWidth = content.ActualWidth + content.Margin.Left + content.Margin.Right;
            var contentHeight = content.ActualHeight + content.Margin.Top + content.Margin.Bottom;
            var radius = GetActualCornerRadius(contentWidth);
            double maxOffset_Y = shadowParams.Max(param => param.Offset_Y);

            _shadowCanvas.VerticalAlignment = content.VerticalAlignment;
            _shadowCanvas.HorizontalAlignment = content.HorizontalAlignment;

            using (var ds = canvasCommandList.CreateDrawingSession())
            {
                ds.FillRoundedRectangle(new Rect(0, 0, contentWidth, contentHeight), radius, radius, Color.FromArgb(255, 0, 0, 0));
            }

            CompositeEffect compositeEffect = CreateEffects(shadowParams, canvasCommandList);

            var bound = compositeEffect.GetBounds(drawSession);
            double shadowWidth = Math.Abs(bound.X);
            double shadowHeight = Math.Abs(bound.Y);

            UpdateLayout(maxOffset_Y, bound, shadowWidth, shadowHeight);

            DrawEffect(drawSession, compositeEffect, shadowWidth, shadowHeight);
        }

        private void DrawEffect(CanvasDrawingSession drawSession, CompositeEffect compositeEffect, double shadowWidth, double shadowHeight)
        {
            drawSession.DrawImage(compositeEffect, (float)shadowWidth, (float)shadowHeight);
        }

        private CompositeEffect CreateEffects(List<ShadowParam> shadowParams, CanvasCommandList canvasCommandList)
        {
            CompositeEffect compositeEffect = new CompositeEffect();

            shadowParams.ForEach(param =>
            {
                var shadowEffect = CreateShadowEffect(canvasCommandList, param);
                compositeEffect.Sources.Add(shadowEffect);
            });
            return compositeEffect;
        }

        private void UpdateLayout(double maxOffset_Y, Rect bound, double shadowWidth, double shadowHeight)
        {
            _contentPresenter.Margin = new Thickness(shadowWidth, shadowHeight - maxOffset_Y, shadowWidth, shadowHeight + maxOffset_Y);
            _shadowCanvas.Height = bound.Height;
            _shadowCanvas.Width = bound.Width;
        }

        float GetActualCornerRadius(double length)
        {
            if (CornerRadius >= 1)
                return (float)CornerRadius;

            return (float)(length * CornerRadius);
        }

        Transform2DEffect CreateShadowEffect(IGraphicsEffectSource source, ShadowParam param)
        {
            return new Transform2DEffect
            {
                Source = new Transform2DEffect
                {
                    Source = new ShadowEffect
                    {
                        BlurAmount = param.Blur,
                        ShadowColor = Color.FromArgb(param.Alpha, 0, 0, 0),
                        Source = source
                    },
                },
            };
        }
    }
}
