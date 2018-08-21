using System;
using StackExchange.Profiling;

namespace MinProfiler {
    class Program {
        static void Main(string[] args) {
            var profiler = MiniProfiler.StartNew("My Profile");
            using (profiler.Step("Hello, world!")) {
                var s = String.Format("{0} {1} {2}", 1, 2, 3);
                var d = DateTime.Now.ToString("yyyy MM dd");
            }

            Console.WriteLine(MiniProfiler.Current.RenderPlainText());
        }
    }
}