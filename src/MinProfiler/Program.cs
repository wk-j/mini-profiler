using System;
using StackExchange.Profiling;

namespace MinProfiler {
    class P {
        public int X { set; get; }
        public int Y { set; get; }
    }

    class Program {
        static int A(int k) {
            var a = 100 * 100 * k;
            return a;
        }
        static void Main(string[] args) {
            var profiler = MiniProfiler.StartNew("My Profile");
            using (profiler.Step("Hello, world!")) {
                var s = String.Format("{0} {1} {2}", 1, 2, 3);
                var d = DateTime.Now.ToString("yyyy MM dd");
            }

            using (profiler.Step("Go")) {
                A(200);
                var p = new P();
            }

            Console.WriteLine(MiniProfiler.Current.RenderPlainText());
        }
    }
}