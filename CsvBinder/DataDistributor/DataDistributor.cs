using System;
using System.Linq;

namespace DataCollector
{
    public abstract class DataDistributor
    {
        internal virtual void CreateInstance(string line, bool hasHeader = false, bool linearBinding = false, string[] header = null, string delim = ", ")
        {
            var data = line.Split(delim);
            var props = this.GetType().GetProperties();
            if (!hasHeader)
            {
                if (linearBinding)
                {
                    var size = props.Length;
                    for (int i = 0; i < size; i++)
                    {
                        props[i].GetSetMethod().Invoke(this, new object[] { data[i] });
                    }
                    return;
                }
                props[0].GetSetMethod().Invoke(this, new object[] { data[0] });
                props[1].GetSetMethod().Invoke(this, new object[] { data.Skip(1).ToArray() });
            }
            else
            {
                var size = Math.Min(header.Length, props.Length);
                for (int i = 0; i < size; i++)
                {
                    if (props[i].Name == header[i])
                        props[i].GetSetMethod().Invoke(this, new object[] { data[i] });
                }
            }
        }
    }
}
