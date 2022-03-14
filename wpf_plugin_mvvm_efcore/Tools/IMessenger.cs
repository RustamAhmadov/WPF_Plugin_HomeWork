using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_plugin_mvvm_efcore.Messages;

namespace wpf_plugin_mvvm_efcore.Tools
{
    public interface IMessenger
    {
        void Send<TMessage>(TMessage message) where TMessage : IMessage;
        void Subscribe<TMessage>(Action<object> action) where TMessage : IMessage;

        public void Send<TMessage>(TMessage message, ViewModelTokens.Token token) where TMessage : IMessage;
        public void Subscribe<TMessage>(Action<object> action, ViewModelTokens.Token token) where TMessage : IMessage;
    }
}
