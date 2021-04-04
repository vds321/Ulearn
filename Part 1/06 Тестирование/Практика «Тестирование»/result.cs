[TestCase(@"'a'", new[] {"a"})]
[TestCase(@"'a' ", new[] {"a"})]
[TestCase(@"'a'  ", new[] {"a"})]
[TestCase(@"'a''b'", new[] {"a", "b"})]
[TestCase(@"''", new[] {""})]
[TestCase(@"b'a'", new[] {"b","a"})]
[TestCase(@"'a\\'", new[] {"a\\"})]
[TestCase(@"'\\'", new[] {"\\"})]
[TestCase("\"'\"", new[] {"'"})]
[TestCase("'\"'", new[] {"\""})]
[TestCase("'a", new[] {"a"})]
[TestCase("'a'b", new[] {"a", "b"})]
[TestCase(@" 'a'", new[] {"a"})]
[TestCase(@"' '", new[] {" "})]
[TestCase(@"'a ", new[] {"a "})]
[TestCase(@"'\''", new[] {"\'"})]
[TestCase("\"\\\"\\\"\"", new[] {"\"\""})]
[TestCase("", new string[0])]

        
// ���������� ���� ���� �����
public static void RunTests(string input, string[] expectedOutput)
{
    // ���� ������ �������� �� �����
    Test(input, expectedOutput);
}