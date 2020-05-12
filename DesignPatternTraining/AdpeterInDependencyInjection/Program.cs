using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Features.Metadata;
using static System.Console;

namespace AdapterInDependencyInjection
{
    interface ICommand
    {
        void Execute();
    }

    class SaveCommand : ICommand
    {
        public void Execute()
        {
            WriteLine("Saving a file");
        }
    }

    class OpenCommand : ICommand
    {
        public void Execute()
        {
            WriteLine("Opening a file");
        }
    }


    class Button
    {
        private ICommand command;
        private string name;

        public Button(ICommand command, string name)
        {
            this.command = command ?? throw new ArgumentNullException(paramName:nameof(command));
            this.name = name ?? throw new ArgumentNullException(paramName: nameof(name));
        }

        public void Click()
        {
            command.Execute();
        }

        public void PrintMe()
        {
            Console.WriteLine($"I am a button called {name}");
        }
    }

    class Editor
    {
        private IEnumerable<Button> buttons;

        public IEnumerable<Button> Buttons => buttons;

        public Editor(IEnumerable<Button> buttons)
        {
            this.buttons = buttons;
        }


        public void ClickAll()
        {
            foreach (var button in buttons)
            {
                button.Click();
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<SaveCommand>().As<ICommand>()
                .WithMetadata("Name", "Save");
            //last registration is default
            b.RegisterType<OpenCommand>().As<ICommand>()
                .WithMetadata("Name", "Open");
            //b.RegisterType<Button>();
            //now it automatically create instances of button with all classes which implement ICommand
            //end because of that when we resolve editor then autofac will pass collection of buttons instead of one default implementation 
            //b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));

            //int this approach we can even pass string parameter to each button-command variation
            b.RegisterAdapter<Meta<ICommand>, Button>(cmd =>
                new Button(cmd.Value, (string) cmd.Metadata["Name"])
            );

            b.RegisterType<Editor>();

            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();
                //editor.ClickAll();

                foreach (var btn in editor.Buttons)
                {
                    btn.PrintMe();
                }
            }

            ReadKey();
        }
    }
}
