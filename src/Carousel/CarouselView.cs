using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Carousel
{
    public class CarouselView : ItemsControl
    {
        public class IndicatorItem : INotifyPropertyChanged
        {
            private bool _isSelected;
            public bool IsSelected
            {
                get => _isSelected;
                set
                {
                    if (_isSelected != value)
                    {
                        _isSelected = value;
                        PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (nameof (IsSelected)));
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }

        public double ViewportWidth
        {
            get { return (double)GetValue (ViewportWidthProperty); }
            set { SetValue (ViewportWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewportWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewportWidthProperty =
            DependencyProperty.Register ("ViewportWidth", typeof (double), typeof (CarouselView), new PropertyMetadata (30.0));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register ("SelectedItem", typeof (object), typeof (CarouselView));

        public object SelectedItem
        {
            get => GetValue (SelectedItemProperty);
            set => SetValue (SelectedItemProperty, value);
        }

        public static new readonly DependencyProperty ItemsSourceProperty =
                                   DependencyProperty.Register ("ItemsSource", typeof (IEnumerable), typeof (CarouselView),
                                       new PropertyMetadata (null, OnItemsSourceChanged));
        public new IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue (ItemsSourceProperty);
            set => SetValue (ItemsSourceProperty, value);
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CarouselView view && e.NewValue is IEnumerable items)
            {
                view._itemCache = items.Cast<object> ().ToList ();

                // 인디케이터 초기화
                view.UpdateIndicators ();

                if (view._itemCache.Any ())
                {
                    view._selectedIndex = 0;
                    view.SelectedItem = view._itemCache[0];
                }
            }
        }
        public ObservableCollection<IndicatorItem> IndicatorItems
        {
            get { return (ObservableCollection<IndicatorItem>)GetValue (IndicatorItemsProperty); }
            set { SetValue (IndicatorItemsProperty, value); }
        }

        public static readonly DependencyProperty IndicatorItemsProperty =
            DependencyProperty.Register (nameof (IndicatorItems),
                                        typeof (ObservableCollection<IndicatorItem>),
                                        typeof (CarouselView),
                                        new PropertyMetadata (new ObservableCollection<IndicatorItem> ()));

        static CarouselView()
        {
            DefaultStyleKeyProperty.OverrideMetadata (typeof (CarouselView), new FrameworkPropertyMetadata (typeof (CarouselView)));
        }

        private Grid _container;
        private ContentPresenter _oldPresenter;
        private ContentPresenter _newPresenter;
        private TranslateTransform _oldTrans;
        private TranslateTransform _newTrans;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate ();

            _container = GetTemplateChild ("PART_Container") as Grid;
            _oldPresenter = GetTemplateChild ("PART_OldPresenter") as ContentPresenter;
            _newPresenter = GetTemplateChild ("PART_NewPresenter") as ContentPresenter;

            // RenderTransform을 한 번만 생성해서 재사용
            if (_oldPresenter != null && !(_oldPresenter.RenderTransform is TranslateTransform))
            {
                _oldTrans = new TranslateTransform ();
                _oldPresenter.RenderTransform = _oldTrans;
            }
            else
            {
                _oldTrans = (TranslateTransform)_oldPresenter.RenderTransform;
            }

            if (_newPresenter != null && !(_newPresenter.RenderTransform is TranslateTransform))
            {
                _newTrans = new TranslateTransform ();
                _newPresenter.RenderTransform = _newTrans;
            }
            else
            {
                _newTrans = (TranslateTransform)_newPresenter.RenderTransform;
            }

            if (GetTemplateChild ("PART_Next") is Button nextBtn)
                nextBtn.Click += (s, e) => SlideTo (+1);
            if (GetTemplateChild ("PART_Prev") is Button prevBtn)
                prevBtn.Click += (s, e) => SlideTo (-1);
        }

        private List<object> _itemCache = new ();
        private int _selectedIndex = 0;
        private bool _isAnimating;
        private void SlideTo(int direction)
        {
            if (_isAnimating || _itemCache.Count < 2)
                return;

            _selectedIndex = (_selectedIndex + direction + _itemCache.Count) % _itemCache.Count;
            UpdateIndicatorSelectedIndex (_selectedIndex);
            object nextItem = _itemCache[_selectedIndex];
            _isAnimating = true;

            double width = ViewportWidth;

            _newPresenter.Content = nextItem;
            _newPresenter.Visibility = Visibility.Visible;

            // 애니메이션 시작 전 위치 설정
            _oldTrans.X = 0;
            _newTrans.X = direction > 0 ? width : -width;

            var duration = new Duration (TimeSpan.FromMilliseconds (300));
            var easing = new CubicEase { EasingMode = EasingMode.EaseInOut };

            var animOld = new DoubleAnimation
            {
                To = direction > 0 ? -width : width,
                Duration = duration,
                EasingFunction = easing
            };
            var animNew = new DoubleAnimation
            {
                To = 0,
                Duration = duration,
                EasingFunction = easing
            };

            animOld.Completed += (s, e) =>
            {
                SelectedItem = nextItem;

                // 애니메이션 제거 후 초기화
                _oldTrans.BeginAnimation (TranslateTransform.XProperty, null);
                _newTrans.BeginAnimation (TranslateTransform.XProperty, null);

                _oldTrans.X = 0;
                _newTrans.X = 0;

                _oldPresenter.Content = nextItem;
                _newPresenter.Visibility = Visibility.Collapsed;
                _newPresenter.Content = null;

                _isAnimating = false;
            };

            _oldTrans.BeginAnimation (TranslateTransform.XProperty, animOld);
            _newTrans.BeginAnimation (TranslateTransform.XProperty, animNew);
        }
        private void UpdateIndicators()
        {
            IndicatorItems.Clear ();
            if (_itemCache == null)
                return;

            foreach (var _ in _itemCache)
            {
                IndicatorItems.Add (new IndicatorItem ());
            }

            UpdateIndicatorSelectedIndex (_selectedIndex);
        }

        private void UpdateIndicatorSelectedIndex(int index)
        {
            for (int i = 0; i < IndicatorItems.Count; i++)
            {
                IndicatorItems[i].IsSelected = (i == index);
            }
        }
    }
}