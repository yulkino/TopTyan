using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Restaurant
{
    class Interpretator
    {
        public IInterpretatable Logic;
        public IInterpretatable Visual;
        DispatcherTimer queueTimer;

        public Interpretator(IInterpretatable logic, IInterpretatable visual)
        {
            Logic = logic;
            Visual = visual;
            queueTimer = new DispatcherTimer();
            queueTimer.Interval = TimeSpan.FromMilliseconds(10);
            queueTimer.Tick += (sender, args) => QueueTimerTick();
            queueTimer.Start();
        }

        void QueueTimerTick()
        {
            while(Logic.EventQueue.Count != 0)
                Visual.InterpretatableAction[Logic.EventQueue.Dequeue()]();
            while (Visual.EventQueue.Count != 0)
                Logic.InterpretatableAction[Visual.EventQueue.Dequeue()]();
        }
    }
}
