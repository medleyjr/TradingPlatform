using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Medley.Common.Logging;

namespace Medley.Common.Utils
{
    //create a key class to make sure the key for a sorted list is unique. DateTime is not alwyas unique
    class ScheduleKey : IComparable
    {
        public DateTime TimeOfExecute;
        public int Id;

        public ScheduleKey(DateTime timeOfExecute, int id)
        {
            TimeOfExecute = timeOfExecute;
            Id = id;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ScheduleKey otherKey = obj as ScheduleKey;
            return this.TimeOfExecute.CompareTo(otherKey.TimeOfExecute);
        }
    }

    //The Schedular class allow objects to be placed in a scheduled to list to be executed at a specific time
    public class Scheduler<T>
    {      
        public event GenericEventHandler<T> TaskNotify;
       
        private object syncObjList = new object();
        
        private SortedList<ScheduleKey, T> m_ObjList = new SortedList<ScheduleKey, T>();
        private Timer m_DataTimer = null;
        private int m_NextId = 0;

        public Scheduler()
        {
            m_DataTimer = new Timer(new TimerCallback(ExecTask), null, Timeout.Infinite, 0);
        }

        private int NextId
        {
            get
            {
                if (m_NextId == int.MaxValue)
                    m_NextId = 1;
                else
                    m_NextId++;

                return m_NextId;
            }
        }

        public void AddItem(DateTime execTime, T obj)
        {
            MedleyLogger.Instance.Debug("Scheduler. Add item for: " + execTime.ToString());
            lock (syncObjList)
            {
                m_ObjList.Add(new ScheduleKey(execTime, NextId), obj);
            }
            CalcNextExecTime();
        }

        private void ExecTask(Object param)
        {
            lock (syncObjList)
            {
                if (m_ObjList.Keys.Count > 0)
                {
                    KeyValuePair<ScheduleKey, T> pair = m_ObjList.ElementAt(0);
                    T obj = pair.Value;
                    if (TaskNotify != null)
                        TaskNotify(this, obj);

                    m_ObjList.Remove(pair.Key);
                }
            }

            CalcNextExecTime();
        }

        private void CalcNextExecTime()
        {
            lock (syncObjList)
            {
                if (m_ObjList.Keys.Count == 0)
                {
                    m_DataTimer.Change(Timeout.Infinite, 0);
                    return;
                }

                DateTime nextExecTime = m_ObjList.Keys[0].TimeOfExecute;
                TimeSpan timeDiff = nextExecTime - DateTime.Now;
                if (timeDiff < new TimeSpan(0))
                    m_DataTimer.Change(0, 0);
                else
                    m_DataTimer.Change(timeDiff, new TimeSpan(0));
            }
        }

    }
}
