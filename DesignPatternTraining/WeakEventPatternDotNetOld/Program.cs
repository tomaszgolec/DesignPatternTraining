using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace WeakEventPatternDotNetOld
{
    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window
    {
        public Window(Button button)
        {
            //this approach is better because is defending against memory leak
            WeakEventManager<Button, EventArgs>
                .AddHandler(button, "Clicked", ButtonClicked);
            //button.Clicked += ButtonClicked;
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            WriteLine("Button clicked (Window handler)");
        }

        ~Window()
        {
            WriteLine("Window finalized");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var button = new Button();
            var window = new Window(button);
            var windowRef = new WeakReference(window);
            button.Fire();

            WriteLine("Setting window to null");
            window = null;

            FireGC();
            WriteLine($"Is the windows alive after GC {windowRef.IsAlive}");
            button.Fire(); // windows is still alive :O this is leak 
            ReadKey();
        }

        private static void FireGC()
        {
            WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            WriteLine(" GC is done");
        }
    }
}
