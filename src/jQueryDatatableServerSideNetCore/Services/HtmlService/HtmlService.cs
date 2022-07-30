using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace jQueryDatatableServerSideNetCore.Services.HtmlService
{
    public class HtmlService : IHtmlService
    {
        public byte[] Write<T>(IList<T> registers)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append("<table>\n");
            sb.Append("\t<thead>\n");
            sb.Append("\t\t<tr>\n");

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                string value = properties[i].Name;

                var attribute = properties[i].GetCustomAttribute(typeof(DisplayAttribute));
                if (attribute != null)
                {
                    value = (attribute as DisplayAttribute).Name;
                }

                sb.Append("\t\t\t<td>");
                sb.Append(value);
                sb.Append("</td>\n");
            }

            sb.Append("\t\t</tr>\n");
            sb.Append("\t</thead>\n");
            sb.Append("\t<tbody>\n");

            foreach (var register in registers)
            {
                sb.Append("\t\t<tr>\n");

                for (var i = 0; i < properties.Length; i++)
                {
                    sb.Append("\t\t\t<td>");
                    sb.Append(properties[i].GetValue(register, null));
                    sb.Append("</td>\n");
                }
                    
                sb.Append("\t\t</tr>\n");
            }

            sb.Append("\t</tbody>\n");
            sb.Append("</table>");

            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}
