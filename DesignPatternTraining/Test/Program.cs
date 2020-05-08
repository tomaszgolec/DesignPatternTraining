using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {

        public class Field
        {
            private readonly string _fieldName;
            private readonly string _fieldType;

            public Field(string fieldName, string fieldType)
            {
                this._fieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
                this._fieldType = fieldType ?? throw new ArgumentNullException(nameof(fieldType));
            }

            public override string ToString()
            {
                var i = new string(' ', 2);
                return $"public{i}{_fieldType}{i}{_fieldName};";
            }
        }

        public class Code
        {
            private readonly string _className;
            private readonly List<Field> _fields = new List<Field>();

            public Code(string className)
            {
                _className = className;
            }

            public void AddField(Field field)
            {
                _fields.Add(field);
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                var i = new string(' ', 2);

                sb.AppendLine($"public{i}class{i}{_className}");
                sb.AppendLine("{");

                foreach (var field in _fields)
                {
                    sb.AppendLine($"{i}{field.ToString()}");
                }

                sb.AppendLine("}");

                return sb.ToString();
            }
        }

        public class CodeBuilder
        {
            private readonly string _className;
            private readonly Code _code;

            public CodeBuilder(string className)
            {
                this._className = className;
                this._code = new Code(className);
            }

            public CodeBuilder AddField(string fieldName, string fieldType)
            {
                _code.AddField(new Field(fieldName,fieldType));
                return this;
            }

            public override string ToString()
            {
                return _code.ToString();
            }
        }

        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person")
                .AddField("Name", "string")
                .AddField("Age", "int");

            Console.WriteLine(cb);
            Console.ReadKey();
        }
    }
}
