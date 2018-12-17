using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.Utility
{
    public class ObservableDictionary<TK, TV> : Dictionary<TK, TV>, INotifyCollectionChanged
    {
        #region Constructor

        public ObservableDictionary()
        {

        }

        #endregion

        public new void Add(TK key, TV value)
        {
            base.Add(key, value);
            NotifyCollectionChangedEventArgs e =
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

            OnCollectionChanged(e);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                try
                {
                    CollectionChanged(this, e);
                }
                catch (System.NotSupportedException)
                {
                    NotifyCollectionChangedEventArgs alternativeEventArgs =
                        new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                    OnCollectionChanged(alternativeEventArgs);
                }
        }
    }
}
