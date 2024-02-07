using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace TheExpanseRPG.CustomControls
{
    public class CutCorner : Border
    {

        private PathGeometry _borderGeometry;
        static CutCorner()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(CutCorner), new FrameworkPropertyMetadata(typeof(CutCorner)));
        }
        public CutCorner()
        {
            _segments = new();
            _borderGeometry = new();
        }
        private int GetCornerParameter(int cornerIndex)
        {
            string[] cornersToCut = BorderCuts.Split(',');
            if (cornerIndex > cornersToCut.Length - 1)
            {
                return 0;
            }
            if (int.TryParse(cornersToCut[cornerIndex], out int retval))
            {
                if (retval > 1)
                {
                    return 0;
                }
                return retval;
            }
            return 0;
        }
        public int HasShadow
        {
            get { return (int)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        //public Brush BorderBrush
        //{
        //    get { return (Brush)GetValue(BorderBrushProperty); }
        //    set { SetValue(BorderBrushProperty, value); }
        //}
        public new int BorderThickness
        {
            get { return (int)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public int CutSize
        {
            get { return (int)GetValue(CutSizeProperty); }
            set { SetValue(CutSizeProperty, value); }
        }
        public string BorderCuts
        {
            get { return (string)GetValue(BorderCutsProperty); }
            set { SetValue(BorderCutsProperty, value); }
        }
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty CutSizeProperty = DependencyProperty.Register(
           nameof(CutSize),
           typeof(int),
           typeof(CutCorner),
           new PropertyMetadata(5));

        // public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(
        //nameof(BorderBrush),
        //typeof(Brush),
        //typeof(CutCorner),
        //new PropertyMetadata(Brushes.Black));


        public static readonly DependencyProperty BorderCutsProperty = DependencyProperty.Register(
            nameof(BorderCuts),
            typeof(string),
            typeof(CutCorner),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            nameof(Fill),
            typeof(Brush),
            typeof(CutCorner),
            new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(int),
            typeof(CutCorner),
            new PropertyMetadata(1));

        public static new readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
            nameof(BorderThickness),
            typeof(int),
            typeof(CutCorner),
            new PropertyMetadata(1));

        protected override void OnRender(DrawingContext drawingContext)
        {
            RecalculateBorder();
            DrawBorder(drawingContext);
            DrawShadow();
            ClipChildControl();

        }
        private void DrawShadow()
        {
            if (HasShadow == 1)
            {
                Effect = new DropShadowEffect()
                {
                    BlurRadius = 0,
                    ShadowDepth = 3,
                    Color = Colors.Gray
                };
            }
        }
        private void ClipChildControl()
        {
            var content = Child;
            if (content != null)
            {
                PathFigure pathFigure = new(GetBorderStartingPoint(), _segments, true);

                content.Clip = new PathGeometry(new List<PathFigure>() { pathFigure });
            }
        }


        private readonly List<LineSegment> _segments;
        private void RecalculateBorder()
        {
            CalculateSegments();
        }
        private void DrawBorder(DrawingContext drawingContext)
        {
            _borderGeometry = new PathGeometry(new[] { new PathFigure(GetBorderStartingPoint(), _segments, true) });
            drawingContext.DrawGeometry(Fill, new Pen(BorderBrush, BorderThickness), _borderGeometry);
        }

        private Point GetBorderStartingPoint()
        {
            return new Point(GetCornerParameter(0) * CutSize, 0);
        }
        private void CalculateSegments()
        {
            _segments.Clear();
            AddTopBorder();
            AddTopRightCorner();
            AddRightBorder();
            AddBottomRightCorner();
            AddBottomBorder();
            AddBottomLeftCorner();
            AddLeftBorder();
        }

        private void AddTopBorder()
        {
            _segments.Add(new LineSegment(new Point(ActualWidth - (GetCornerParameter(1) * CutSize), 0), true));
        }

        private void AddTopRightCorner()
        {
            if (GetCornerParameter(1) != 0)
            {
                _segments.Add(new LineSegment(new Point(ActualWidth, CutSize), true));
            }
        }

        private void AddRightBorder()
        {
            _segments.Add(new LineSegment(new Point(ActualWidth, ActualHeight - (GetCornerParameter(2) * CutSize)), true));
        }

        private void AddBottomRightCorner()
        {
            if (GetCornerParameter(2) != 0)
            {
                _segments.Add(new LineSegment(new Point(ActualWidth - CutSize, ActualHeight), true));
            }
        }
        private void AddBottomBorder()
        {
            _segments.Add(new LineSegment(new Point(GetCornerParameter(3) * CutSize, ActualHeight), true));
        }

        private void AddBottomLeftCorner()
        {
            if (GetCornerParameter(3) != 0)
            {
                _segments.Add(new LineSegment(new Point(0, ActualHeight - CutSize), true));
            }
        }

        private void AddLeftBorder()
        {
            _segments.Add(new LineSegment(new Point(0, GetCornerParameter(0) * CutSize), true));
        }



    }
}
