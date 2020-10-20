using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace UiDrawing.Controls
{
    public class SliderSample : SKCanvasView
    {
        #region properties
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(propertyName: nameof(Progress),
                returnType: typeof(double),
                declaringType: typeof(SliderSample),
                defaultValue: 0.5,
                validateValue: (_, value) => (double)value >= 0 && (double)value <= 1,
                propertyChanged: InvalidateSurfaceOnChange);

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create(propertyName: nameof(LineColor),
                returnType: typeof(Color),
                declaringType: typeof(SliderSample),
                defaultValue: Color.DimGray,
                validateValue: (_, value) => value != null,
                propertyChanged: InvalidateSurfaceOnChange);

        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }
        #endregion

        public SliderSample()
        {
            EnableTouchEvents = true;
        }

        private static void InvalidateSurfaceOnChange(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (SliderSample)bindable;
            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                case SKTouchAction.Moved:
                    var xPosition = e.Location.X;
                    var progress = xPosition / CanvasSize.Width;
                    progress = Math.Min(1, progress);
                    progress = Math.Max(0, progress);
                    Progress = progress;
                    InvalidateSurface();
                    e.Handled = true;
                    break;
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var size = e.Info.Size;
 
            canvas.Clear();
            var lineWidth = 50f;
            var lineStart = new SKPoint(lineWidth/2, size.Height / 2);
            var lineEnd = new SKPoint(size.Width - lineWidth/2, size.Height/2);
            var thumbPosition = new SKPoint((float) (size.Width * Progress), size.Height/2);

            canvas.DrawLine(lineStart, lineEnd, new SKPaint
            {
                Color = LineColor.ToSKColor(),
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round,
                StrokeWidth = lineWidth
            });

            //canvas.DrawCircle(thumbPosition, 75f, new SKPaint
            //{
            //    Color = ThumbColor.ToSKColor(),
            //    Style = SKPaintStyle.Fill,
            //    IsAntialias = true
            //});

            canvas.DrawCircle(thumbPosition, 75f, new SKPaint
            {
                Color = SKColors.DarkGray,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            });

            var path = new SKPath();
            path.AddCircle(thumbPosition.X, thumbPosition.Y, 75);

            canvas.RotateDegrees((float)Progress * 360f, path.Bounds.MidX, path.Bounds.MidY);

            canvas.ClipPath(path, SKClipOperation.Intersect, true);

            canvas.DrawBitmap(SomeGuy, path.Bounds);
        }

        private static SKBitmap SomeGuy = Utility.LoadSomeGuy();
    }
}