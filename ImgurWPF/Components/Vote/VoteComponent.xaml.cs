using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImgurWPF.Components.Vote
{
    /// <summary>
    /// VoteComponent.xaml 的互動邏輯
    /// </summary>
    public partial class VoteComponent : UserControl
    {
        public VoteViewModel VoteViewModel { get; set; }

        public VoteComponent()
        {
            InitializeComponent();
            this.VoteViewModel = new VoteViewModel();
            DataContext = this.VoteViewModel;
        }

        public int Score
        {
            get => (int)GetValue(ScoreProperty);
            set => SetValue(ScoreProperty, value);
        }

        public static readonly DependencyProperty ScoreProperty =
            DependencyProperty.Register(
                nameof(Score),
                typeof(int),
                typeof(VoteComponent),
                new PropertyMetadata(0, OnScoreChanged));

        private static void OnScoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (VoteComponent)d;

            if (control.VoteViewModel == null)
            {
                return;
            }

            var newValue = (int)e.NewValue;
            control.VoteViewModel.score = newValue;
            control.VoteViewModel.oldscore = newValue;
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(VoteComponent),
                new PropertyMetadata((d, e) =>
                {
                    var control = (VoteComponent)d;
                    var dataContext = (VoteViewModel)control.DataContext;
                    dataContext.ParentBindingCommand = (ICommand)e.NewValue;
                }));

        public VoteEnums Vote
        {
            get => (VoteEnums)GetValue(VoteProperty);
            set => SetValue(VoteProperty, value);
        }

        public static readonly DependencyProperty VoteProperty =
            DependencyProperty.Register(
                nameof(Vote),
                typeof(VoteEnums),
                typeof(VoteComponent),
                new PropertyMetadata(VoteEnums.VETO, OnVoteChanged));

        private static void OnVoteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (VoteComponent)d;
            var dataContext = (VoteViewModel)control.DataContext;
            dataContext.Vote = ((VoteEnums)e.NewValue);
        }

        //public void OnVoted(VoteEnums e)
        //{
        //    Vote = e;
        //    // 呼叫 xaml 綁定的 command 執行
        //    Command?.Execute(e);
        //}
    }
}