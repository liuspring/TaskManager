using System.Reflection;

namespace Common
{
    public class PropertyHelper
    {
        public static void Copy(object from, object to)
        {
            PropertyInfo[] properties = from.GetType().GetProperties();
            PropertyInfo[] infoArray2 = to.GetType().GetProperties();
            int length = infoArray2.Length;
            int index = 0;
            int num3 = properties.Length;
            while (index < num3)
            {
                for (int i = 0; i < length; i++)
                {
                    if (infoArray2[i].Name == properties[index].Name)
                    {
                        infoArray2[i].SetValue(to, properties[index].GetValue(from, null), null);
                        break;
                    }
                }
                index++;
            }
        }
    }
}
