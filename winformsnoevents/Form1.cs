﻿using MetroFramework.Forms;
using ReactiveUI;
using System.Reactive.Concurrency;
using System.Windows.Forms;


namespace winformsnoevents
{
    public partial class Form1 : MetroForm, IViewFor<MyViewModel>
    {
        IScheduler scheduler;

        public Form1()
        {
            InitializeComponent();

            scheduler = new System.Reactive.Concurrency.ControlScheduler(this);

            VM = new MyViewModel(
                scheduler,
                this.WhenAnyValue(x => x.inputBox.Text)
            );

            this.Bind(VM, x => x.BackgroundTicker, x => x.tickerBox.Text);
            this.Bind(VM, x => x.WordCount, x => x.wordCountBox.Text);
        }

        public MyViewModel VM { get; private set; }

        #region IViewFor<MyViewModel>
        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (MyViewModel)value; }
        }

        MyViewModel IViewFor<MyViewModel>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }
        #endregion
    }
}
