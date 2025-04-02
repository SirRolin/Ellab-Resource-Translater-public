using Ellab_Resource_Translater.Objects;

namespace Ellab_Resource_Translater.Util
{
    public static class FormUtils
    {
        /// <summary>
        /// Saves the CheckBoxList to a list using a localiser dictionary (shorthand, readable)
        /// Should be called on change.
        /// </summary>
        /// <param name="list">list to save changes to</param>
        /// <param name="checkedListBox"><see cref="CheckedListBox"/> that changed</param>
        /// <param name="localiser"><see langword="Dictionary"/> with key being the shorthand text and value being the readable text</param>
        public static void SaveCheckBoxListChangeLocalised(List<string> list, CheckedListBox checkedListBox, Dictionary<string, string> localiser)
        {
            var sindex = checkedListBox.SelectedIndex;
            var sitem = checkedListBox.SelectedItem;

            // Rare case of no selected item
            if (sitem == null)
                return;
            
            // Since this happens BEFORE the check is made, the check state is opposite of what it's gonna be
            if (!checkedListBox.GetItemChecked(sindex))
                list.Add(localiser.FirstOrDefault(predicate: x => sitem.Equals(x.Value)).Key);
            else
                list.RemoveAll(item => item.Equals(localiser.FirstOrDefault(predicate: x => sitem.Equals(x.Value)).Key));
        }

        /// <summary>
        /// Loads a all values in <paramref name="localiser"/> into <paramref name="CheckedListBox"/> and then checks them if <paramref name="list"/> contains the key.
        /// </summary>
        /// <param name="list">list of checked items</param>
        /// <param name="checkedListBox">CheckList</param>
        /// <param name="localiser"><see langword="Dictionary"/> with key being the shorthand text and value being the readable text.<br/>Fills out <paramref name="CheckedListBox"/></param>
        public static void LoadCheckboxListLocalised(List<string> list, CheckedListBox checkedListBox, Dictionary<string, string> localiser)
        {
            var checkitems = list.Select(x => localiser[x]).ToList();
            checkedListBox.Items.Clear();
            localiser.Values.ToList().ForEach(x => checkedListBox.Items.Add(x));
            int height = 0;
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                height += checkedListBox.GetItemHeight(i) + 1;
                if (checkitems.Contains(checkedListBox.Items[i]))
                    checkedListBox.SetItemChecked(i, true);
            }

            // Changing the height as there's no AutoSize on 
            var width = checkedListBox.Width *
                (checkedListBox.MaximumSize.Height <= 0 ? 1 : (height / checkedListBox.MaximumSize.Height));
            checkedListBox.Size = new System.Drawing.Size(width, height);
        }


        /// <summary>
        /// Shows <paramref name="processName"/> on the <paramref name="listView"/> while <paramref name="process"/> is running.<br/>
        /// Then it runs <paramref name="onStart"/>.
        /// </summary>
        /// <param name="onStart">Called before starting the process</param>
        /// <param name="listView"><see cref="ListView"/> to show the process name on</param>
        /// <param name="processName">Full name of the process</param>
        /// <param name="process">Process to execute</param>
        public static void ShowOnListWhileProcessing(Action onStart, ListView listView, string processName, Action process)
        {
            ShowOnListWhileProcessing((s) => s, onStart, listView, processName, process);
        }

        /// <summary>
        /// Shows <paramref name="processName"/> (shortened by <paramref name="pathLength"/>) on the <paramref name="listView"/> while <paramref name="process"/> is running.<br/>
        /// Then it runs <paramref name="onStart"/>.
        /// </summary>
        /// <param name="pathLength">How much to shorten the ProcessName by triming the start of the <paramref name="processName"/></param>
        /// <param name="onStart">Called before starting the process</param>
        /// <param name="listView"><see cref="ListView"/> to show the process name on</param>
        /// <param name="processName">Full name of the process</param>
        /// <param name="process">Process to execute</param>
        public static void ShowOnListWhileProcessing(int pathLength, Action onStart, ListView listView, string processName, Action process)
        {
            ShowOnListWhileProcessing((s) => s[(pathLength + 1)..], onStart, listView, processName, process);
        }

        /// <summary>
        /// Shows <paramref name="getName"/>(<paramref name="processName"/>) on the <paramref name="listView"/> while <paramref name="process"/> is running.<br/>
        /// Then it runs <paramref name="onStart"/>.
        /// </summary>
        /// <param name="getName">Function to get the name of the process</param>
        /// <param name="onStart">Called before starting the process</param>
        /// <param name="listView"><see cref="ListView"/> to show the process name on</param>
        /// <param name="processName">Full name of the process</param>
        /// <param name="process">Process to execute</param>
        public static void ShowOnListWhileProcessing(Func<string, string> getName, Action onStart, ListView listView, string processName, Action process)
        {
            string shortenedPath = getName(processName);
            ListViewItem listViewItem = listView.Invoke(() => listView.Items.Add(shortenedPath));
            onStart.Invoke();

            process();

            listView.Invoke(() => listView.Items.Remove(listViewItem));
        }

        /// <summary>
        /// Shows <paramref name="processName"/> (shortened by <paramref name="pathLength"/>) on the <paramref name="listView"/> while <paramref name="process"/> is running.<br/>
        /// Then it runs <paramref name="onStart"/>.<br/>
        /// Then Returns the result of <paramref name="process"/>.
        /// </summary>
        /// <param name="pathLength">How much to shorten the ProcessName by triming the start of the <paramref name="processName"/></param>
        /// <param name="onStart">Called before starting the process</param>
        /// <param name="listView"><see cref="ListView"/> to show the process name on</param>
        /// <param name="processName">Full name of the process</param>
        /// <param name="process">Process to execute</param>
        public static TResult ShowOnListWhileProcessing<TResult>(int pathLength, Action onStart, ListView listView, string processName, Func<ListViewItem, TResult> process)
        {
            return ShowOnListWhileProcessing((s) => s[(pathLength + 1)..], onStart, listView, processName, process);
        }

        /// <summary>
        /// Shows <paramref name="getName"/>(<paramref name="processName"/>) on the <paramref name="listView"/> while <paramref name="process"/> is running.<br/>
        /// Then it runs <paramref name="onStart"/>.<br/>
        /// Then Returns the result of <paramref name="process"/>.
        /// </summary>
        /// <param name="getName">Function to get the name of the process</param>
        /// <param name="onStart">Called before starting the process</param>
        /// <param name="listView"><see cref="ListView"/> to show the process name on</param>
        /// <param name="processName">Full name of the process</param>
        /// <param name="process">Process to execute</param>
        public static TResult ShowOnListWhileProcessing<TResult>(Func<string, string> getName, Action onStart, ListView listView, string processName, Func<ListViewItem, TResult> process)
        {
            string shortenedPath = getName(processName);
            ListViewItem listViewItem = listView.Invoke(() => listView.Items.Add(shortenedPath));
            onStart.Invoke();

            var output = process(listViewItem);

            listView.Invoke(() => listView.Items.Remove(listViewItem));

            return output;
        }

        /// <summary>
        /// Updates <paramref name="label"/>'s Text with a concatenated string of the given objects.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="texts"><see cref="Object"/>s to ToString() and add to <paramref name="label"/></param>
        public static void LabelTextUpdater(Label label, params object[] texts)
        {
            label.Invoke(() => label.Text = string.Concat(texts));
        }

        /// <summary>
        /// adds a link between a <see cref="TextBox"/> and a string varaible.
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="strongRef"></param>
        public static void LinkVariable(ref TextBox tb, Ref<string> strongRef)
        {
            LinkVariable(ref tb, (string v) => strongRef.value = v, strongRef.value);
        }

        /// <summary>
        /// Updates <paramref name="tb"/> and calls <paramref name="OnChange"/> with it's Text when it changes.
        /// </summary>
        public static void LinkVariable(ref TextBox tb, Action<string> OnChange, string startValue)
        {
            Ref<TextBox> strongRefTb = tb;
            tb.Invoke(() =>
            {
                strongRefTb.value.Text = startValue;
                strongRefTb.value.TextChanged += (Object? sender, EventArgs ea) =>
                {
                    OnChange(strongRefTb.value.Text);
                };
            });
        }

        /// <summary>
        /// adds a links between a <see cref="NumericUpDown"/> and a <see cref="int"/>
        /// </summary>
        public static void LinkVariable(ref NumericUpDown nud, Ref<int> strongRef)
        {
            LinkVariable(ref nud, (int v) => strongRef.value = v, strongRef.value);
        }

        /// <summary>
        /// Updates <paramref name="nud"/> and calls <paramref name="OnChange"/> with it's value when it changes.
        /// </summary>
        public static void LinkVariable(ref NumericUpDown nud, Action<int> OnChange, int startValue)
        {
            Ref<NumericUpDown> strongRefNud = nud;
            nud.Invoke(() =>
            {
                strongRefNud.value.Value = (decimal)startValue;
                strongRefNud.value.ValueChanged += (Object? sender, EventArgs ea) =>
                {
                    OnChange((int)strongRefNud.value.Value);
                };
            });
        }

        /// <summary>
        /// adds a links between a <see cref="NumericUpDown"/> and a <see cref="int"/>
        /// </summary>
        public static void LinkVariable(ref NumericUpDown nud, Ref<decimal> strongRef)
        {
            LinkVariable(ref nud, (decimal v) => strongRef.value = v, strongRef.value);
        }

        /// <summary>
        /// Updates <paramref name="nud"/> and calls <paramref name="OnChange"/> with it's value when it changes.
        /// </summary>
        public static void LinkVariable(ref NumericUpDown nud, Action<decimal> OnChange, decimal startvalue)
        {
            Ref<NumericUpDown> strongRefNud = nud;
            nud.Invoke(() =>
            {
                strongRefNud.value.Value = startvalue;
                strongRefNud.value.ValueChanged += (Object? sender, EventArgs ea) =>
                {
                    OnChange(strongRefNud.value.Value);
                };
            });
        }

        /// <summary>
        /// Updates <paramref name="cb"/> and calls <paramref name="OnChange"/> with it's value when it changes.
        /// </summary>
        public static void LinkVariable(ref CheckBox cb, Ref<bool> strongRef)
        {
            LinkVariable(ref cb, (bool v) => strongRef.value = v, strongRef.value);
        }


        /// <summary>
        /// Updates <paramref name="cb"/> and calls <paramref name="OnChange"/> with it's value when it changes.
        /// </summary>
        public static void LinkVariable(ref CheckBox cb, Action<bool> OnChange, bool startvalue)
        {
            Ref<CheckBox> strongRefCb = cb;
            cb.Invoke(() =>
            {
                strongRefCb.value.Checked = startvalue;
                strongRefCb.value.CheckedChanged += (Object? sender, EventArgs ea) =>
                {
                    OnChange(strongRefCb.value.Checked);
                };
            });
        }
    }
}
