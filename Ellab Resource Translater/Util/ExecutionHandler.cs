using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ellab_Resource_Translater.Util
{
    public class ExecutionHandler
    {
        /// <summary>
        /// Execute <paramref name="action"/> on <paramref name="threads"/> threads. If <paramref name="threads"/> is not positive, Execute <paramref name="action"/> on current Thread.
        /// </summary>
        /// <param name="action">integer is Thread number, -1 if in main Thread</param>
        public static void TryExecute<T>(int threads, Action<int> action, Action<T> onFailing, CancellationToken token) where T : Exception
        {
            try
            {
                if (threads > 1)
                {
                    List<Task> tasks = [];
                    for (var i = 0; i < threads; i++)
                    {
                        // localising it so it doesn't take the highest current threads instead of the thread index.
                        var localI = i;
                        tasks.Add(Task.Run(() => action(localI), token));
                    }
                    Task.WhenAll(tasks).Wait(token);
                }
                else
                {
                    action(-1);
                }
            }
            catch (T ex)
            {
                onFailing(ex);
            }
        }
        /// <summary>
        /// Execute <paramref name="action"/> on X threads. If X is not positive, Execute it on current Thread.<br/>
        /// X being the lower of <paramref name="threads"/> and <paramref name="predictedProcesses"/>.<br/>
        /// if any throws an exception of the same type as <paramref name="onFailing"/>
        /// </summary>
        /// <param name="action">integer is Thread number, -1 if in main (Executing) Thread</param>
        public static void TryExecute<T>(int threads, int predictedProcesses, Action<int> action, Action<T> onFailing, CancellationToken token) where T : Exception
        {
            TryExecute(Math.Min(threads, predictedProcesses), action, onFailing, token);
        }
        /// <summary>
        /// Execute <paramref name="action"/> on <paramref name="threads"/> threads. If <paramref name="threads"/> is not positive, Execute <paramref name="action"/> on current Thread.
        /// </summary>
        /// <param name="action">integer is Thread number, -1 if in main Thread</param>
        public static void TryExecute<T>(int threads, Action<int> action, Action<T> onFailing) where T : Exception 
        {
            try
            {
                Execute(threads, action);
            } catch (T ex)
            {
                onFailing(ex);
            }
        }
        /// <summary>
        /// Execute <paramref name="action"/> on X threads. If X is not positive, Execute it on current Thread.<br/>
        /// X being the lower of <paramref name="threads"/> and <paramref name="predictedProcesses"/>.<br/>
        /// if any throws an exception of the same type as <paramref name="onFailing"/>
        /// </summary>
        /// <param name="action">integer is Thread number, -1 if in main Thread</param>
        public static void TryExecute<T>(int threads, int predictedProcesses, Action<int> action, Action<T> onFailing) where T : Exception
        {
            TryExecute(Math.Min(threads, predictedProcesses), action, onFailing);
        }
        /// <summary>
        /// Execute <paramref name="action"/> on <paramref name="threads"/> threads. If <paramref name="threads"/> is not positive, Execute <paramref name="action"/> on current Thread.
        /// </summary>
        /// <param name="action">integer is Thread number, -1 if in main Thread</param>
        public static void Execute(int threads, Action<int> action)
        {
            if (threads > 1)
            {
                Task[] tasks = new Task[threads];
                for (var i = 0; i < threads; i++)
                {
                    // localising it so it doesn't take the highest current threads instead of the thread index.
                    var localI = i;
                    tasks[localI] = (Task.Run(() => action(localI)));
                }
                Task.WhenAll(tasks).Wait();
            } else
            {
                action(-1);
            }
        }
        /// <summary>
        /// Execute <paramref name="action"/> on X threads. If X is not positive, Execute it on current Thread.<br/>
        /// X being the lower of <paramref name="threads"/> and <paramref name="predictedProcesses"/>.
        /// </summary>
        /// <param name="action">integer is Thread number, -1 if in main Thread</param>
        public static void Execute(int threads, int predictedProcesses, Action<int> action)
        {
            Execute(Math.Min(threads, predictedProcesses), action);
        }
    }
}
