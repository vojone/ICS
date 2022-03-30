using System.IO;
using System.Text;
using Xunit.Abstractions;

namespace Carpool.DAL.Tests.Converters
{
    //Taken from Sample project 'CookBook'
    public class XUnitTestOutputConverter : TextWriter
    {
        private readonly ITestOutputHelper _output;
        public XUnitTestOutputConverter(ITestOutputHelper output) => _output = output;
        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string? message) => _output.WriteLine(message);

        public override void WriteLine(string format, params object?[] args) => _output.WriteLine(format, args);
    }
}
