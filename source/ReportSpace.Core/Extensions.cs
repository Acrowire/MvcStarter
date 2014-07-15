using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSpace
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;

    public static class Extensions
    {
        public static String SafeGet(this Dictionary<String, String> dictionar, String Key)
        {
            string result = "";

            if (dictionar.ContainsKey(Key))
            {
                result = dictionar[Key];
            }


            return result;
        }

        public static String GetMethodContext<T>(this T Instance)
        {
            // automatically detect the context name 
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            return String.Format("{0}.{1}.{2}", methodBase.DeclaringType.Namespace, methodBase.DeclaringType.Name, methodBase.Name);
        }

        public static MethodBase GetObjectContext<T>(this T Instance)
        {
            // automatically detect the context name 
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            return methodBase;
        }

        public static MethodBase GetPreviousObjectContext<T>(this T Instance)
        {
            // automatically detect the context name 
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(2);
            MethodBase methodBase = stackFrame.GetMethod();

            return methodBase;
        }

        public static String GetPreviousMethodContext<T>(this T Instance)
        {
            // automatically detect the context name 
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(2);
            MethodBase methodBase = stackFrame.GetMethod();

            return String.Format("{0}.{1}.{2}", methodBase.DeclaringType.Namespace, methodBase.DeclaringType.Name, methodBase.Name);
        }

        public static Int32 nInt(this Hashtable hastable, String Key)
        {
            return Int32.Parse(hastable[Key].ToString());
        }


        #region [ Security ]
        public static string GetSha1(this string value)
        {
            var hash = string.Empty;

            if (value != null)
            {
                var data = Encoding.ASCII.GetBytes(value);
                var hashData = new SHA1Managed().ComputeHash(data);


                foreach (var b in hashData)
                {
                    hash += b.ToString("X2");
                }
            }

            return hash;
        }
        #endregion

        #region [ Linq Extensions ]

        public static bool IsNullOrBLank<T>(this T obj)
        {
            return (obj == null);
        }


        // Usage (new List<String>).When(s => s.Length == 1, (e) => { throw new Exception("error" + e); });
        /// <summary>
        /// Invokes an Action once when the List meets the criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> When<T>(this IEnumerable<T> list, Func<IEnumerable<T>, bool> query, Action<IEnumerable<T>> action)
        {
            if (query.Invoke(list) == true)
            {
                action.Invoke(list);
            }

            return list;
        }

        #endregion

    }
}

